using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AJ.DuplexClient.Common;
using AJ.DuplexClient.Model;

namespace AJ.DuplexClient.ViewModels
{
    /// <summary>The view model.</summary>
    public sealed class MainVM : BindingBase, IGuiCallback
    {
        #region data members

        /// <summary>The adapter providing access to the repositories and services.</summary>
        private readonly Adapters.ServiceAdapter _serviceAdapter;

        #endregion

        #region binding properties 

        public static string Title
        {
            get
            {
#if USE_HTTP
                return "Duplex Client (HTTP)";
#else
                return "Duplex Client (TCP/IP)";
#endif
            }
        }

        string _clientName;
        public string ClientName
        {
            get { return GetPropertyValue(ref _clientName); }
            set { SetPropertyValue(ref _clientName, value); }
        }

        string _message;
        public string Message
        {
            get { return GetPropertyValue(ref _message); }
            set { SetPropertyValue(ref _message, value); }
        }

        ObservableCollection<MessageItem> _messages;
        public ObservableCollection<MessageItem> Messages
        {
            get { return GetPropertyValue(ref _messages, () => new ObservableCollection<MessageItem>()); }
            set { SetPropertyValue(ref _messages, value); }
        }

        MessageItem _selectedMessage;
        public MessageItem SelectedMessage
        {
            get { return GetPropertyValue(ref _selectedMessage); }
            set { SetPropertyValue(ref _selectedMessage, value); }
        }

        ObservableCollection<MessageItem> _connectionStates;
        public ObservableCollection<MessageItem> ConnectionStates
        {
            get { return GetPropertyValue(ref _connectionStates, () => new ObservableCollection<MessageItem>()); }
            set { SetPropertyValue(ref _connectionStates, value); }
        }

        MessageItem _selectedConnectionState;
        public MessageItem SelectedConnectionState
        {
            get { return GetPropertyValue(ref _selectedConnectionState); }
            set { SetPropertyValue(ref _selectedConnectionState, value); }
        }

        ObservableCollection<string> _clients;
        public ObservableCollection<string> Clients
        {
            get { return GetPropertyValue(ref _clients, () => new ObservableCollection<string>()); }
            set { SetPropertyValue(ref _clients, value); }
        }

        bool _isSubscribed;
        public bool IsSubscribed
        {
            get { return GetPropertyValue(ref _isSubscribed); }
            set { SetPropertyValue(ref _isSubscribed, value); }
        }

        bool _isBusy;
        public bool IsBusy
        {
            get { return GetPropertyValue(ref _isBusy); }
            set { SetPropertyValue(ref _isBusy, value); }
        }

        #endregion

        #region commands

        public RelayCommand CommandSubscribe { get; }
        public RelayCommand CommandUnsubscribe { get; }
        public RelayCommand CommandSend { get; }

        #endregion

        #region c'tor

        public MainVM()
        {
            // members
            this._serviceAdapter = new Adapters.ServiceAdapter(this);

            /// commands
            this.CommandSubscribe = new RelayCommand(async o => await SubscribeAsync());
            this.CommandUnsubscribe = new RelayCommand(async o => await UnsubscribeAsync());
            this.CommandSend = new RelayCommand(async o => await SendAsync());

            // default values
            this.ClientName = "Client " + (DateTime.Now.Ticks % 1000);  // random name
            this.Message = "Message from client " + this.ClientName;

            // and initialize
            base.PropertyChanged += MainVM_PropertyChanged;
            UpdateGuiState();
        }

        #endregion

        #region state changes

        private void MainVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.ClientName):
                case nameof(this.Message):
                    UpdateGuiState();
                    break;
            }
        }

        private void UpdateGuiState()
        {
            switch (this.IsSubscribed)
            {
                case false:
                    this.CommandSubscribe.IsEnabled = !string.IsNullOrWhiteSpace(this.ClientName);
                    this.CommandUnsubscribe.IsEnabled = false;
                    this.CommandSend.IsEnabled = false;
                    break;

                case true:
                    this.CommandSubscribe.IsEnabled = false;
                    this.CommandUnsubscribe.IsEnabled = true;
                    this.CommandSend.IsEnabled = !string.IsNullOrWhiteSpace(this.Message);
                    break;
            }
        }

        #endregion

        #region command handlers

        private async Task SubscribeAsync()
        {
            using (ShowBusy())
            {
                await _serviceAdapter.SubscribeAsync(this.ClientName);
            }
        }

        private async Task UnsubscribeAsync()
        {
            using (ShowBusy())
            {
                await _serviceAdapter.UnsubscribeAsync();
                this.IsSubscribed = false;
            };
        }

        private async Task SendAsync()
        {
            using (ShowBusy())
            {
                var sent = await _serviceAdapter.SendMessageAsync(this.ClientName, this.Message);
                if (!sent)
                    await UnsubscribeAsync();
            };
        }

        #endregion

        #region IGuiCallback

        async Task IGuiCallback.SubscriptionChanged(bool isSubscribed)
        {
            this.IsSubscribed = isSubscribed;
            if (IsSubscribed)
            {
                using (ShowBusy())
                {
                    await LoadAllServiceInformationAsync();
                }
            }

            UpdateGuiState();
        }

        private async Task LoadAllServiceInformationAsync()
        {
            // get all clients & messages (in parallel!)
            var messagesTask = _serviceAdapter.GetAllMessagesAsync();
            var clientsTask = _serviceAdapter.GetRegisteredClientsAsync();
            var c = await clientsTask;
            var m = await messagesTask;

            this.Clients = c.ToObservableCollection();
            this.Messages = m.ToObservableCollection();
            this.SelectedMessage = this.Messages.LastOrDefault();
        }

        Task IGuiCallback.ClientAddedAsync(string clientName)
        {
            if (!this.Clients.Contains(clientName))
                this.Clients.Add(clientName);
            return Task.FromResult(0);
        }

        Task IGuiCallback.ClientRemovedAsync(string clientName)
        {
            this.Clients.Remove(clientName);
            return Task.FromResult(0);
        }

        async Task IGuiCallback.MessageAvailableAsync()
        {
            using (ShowBusy())
            {
                await LoadLatestMessagesAsync();
            }
        }

        private async Task LoadLatestMessagesAsync()
        {
            // request all new messages from the repository
            var newInfos = await _serviceAdapter.GetLatestMessagesAsync();
            this.Messages.AddRange(newInfos);
            this.SelectedMessage = this.Messages.LastOrDefault();
        }

        Task IGuiCallback.ConnectionStateChangedAsync(string category, string info)
        {
            var item = new MessageItem { Timestamp = DateTime.Now, ClientName = category, Message = info };
            this.ConnectionStates.Add(item);
            this.SelectedConnectionState = this.ConnectionStates.LastOrDefault();
            return Task.FromResult(0);
        }

        Task IGuiCallback.ServiceCallExceptionAsync(string operation, Exception exception)
        {
            Guard.AssertNotNull(exception, nameof(exception));

            var a = exception as AggregateException;
            if (a != null)
                exception = a.InnerException;

            var item = new MessageItem { Timestamp = DateTime.Now, ClientName = operation, Message = "EXCEPTION: " + exception.Message };
            this.ConnectionStates.Add(item);
            this.SelectedConnectionState = this.ConnectionStates.LastOrDefault();
            return Task.FromResult(0);
        }

        #endregion

        #region busy helpers

        private IDisposable ShowBusy()
        {
            return new Busy(this);
        }

        private class Busy : IDisposable
        {
            MainVM _vm;

            public Busy(MainVM vm)
            {
                _vm = vm;
                _vm.IsBusy = true;
            }

            public void Dispose()
            {
                _vm.IsBusy = false;
            }
        }

        #endregion
    }
}

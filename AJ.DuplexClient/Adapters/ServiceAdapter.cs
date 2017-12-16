using System;
using System.Threading.Tasks;
using AJ.DuplexClient.Model;
using AJ.DuplexClient.Repositories;

namespace AJ.DuplexClient.Adapters
{
    /// <summary>
    /// This class acts as adapter (or conductor) between the GUI and the repositories.
    /// Its sole purpose is to put repository calls on background tasks and marshal callbacks to the GUI thread,
    /// so that neither the GUI nor the repositories have to deal with treading issues.
    /// </summary>
    /// <seealso cref="AJ.DuplexClient.Model.IGuiCallback" />
    public sealed class ServiceAdapter : IGuiCallback
    {
        #region members

        /// <summary>Interface through which the proxy will talk to the GUI.</summary>
        private IGuiCallback _guiCallback;

        private SubscriptionRepository _subscription;
        private InformationRepository _information;

        #endregion

        #region c'tor

        /// <summary>Initializes a new instance of the <see cref="ServiceAdapter" /> class.</summary>
        /// <param name="guiCallback">Interface through which the adapter will talk to the GUI.</param>
        public ServiceAdapter(IGuiCallback guiCallback)
        {
            this._guiCallback = guiCallback;
            this._subscription = new SubscriptionRepository(this);
            this._information = new InformationRepository(this);
        }

        #endregion

        #region information calls

        public async Task<string[]> GetRegisteredClientsAsync()
        {
            return await Task.Run(async () => await _information.GetRegisteredClientsAsync());
        }

        public async Task<bool> SendMessageAsync(string clientName, string message)
        {
            return await Task.Run(async () => await _information.SendMessageAsync(clientName, message));
        }

        public async Task<MessageItem[]> GetAllMessagesAsync()
        {
            return await Task.Run(async () => await _information.GetAllMessagesAsync());
        }

        public async Task<MessageItem[]> GetLatestMessagesAsync()
        {
            return await Task.Run(async () => await _information.GetLatestMessagesAsync());
        }

        #endregion

        #region subsription calls

        public async Task<bool> SubscribeAsync(string clientName)
        {
            return await Task.Run(async () => await _subscription.SubscribeAsync(clientName));
        }

        public async Task UnsubscribeAsync()
        {
            await Task.Run(async () => await _subscription.UnsubscribeAsync());
        }

        #endregion

        #region subsctription callbacks 

        async Task IGuiCallback.ClientAddedAsync(string clientName)
        {
            await App.Current.Dispatcher.InvokeAsync(async () => await this._guiCallback.ClientAddedAsync(clientName));
        }

        async Task IGuiCallback.ClientRemovedAsync(string clientName)
        {
            await App.Current.Dispatcher.InvokeAsync(async () => await this._guiCallback.ClientRemovedAsync(clientName));
        }

        async Task IGuiCallback.ConnectionStateChangedAsync(string category, string info)
        {
            await App.Current.Dispatcher.InvokeAsync(async () => await this._guiCallback.ConnectionStateChangedAsync(category, info));
        }

        async Task IGuiCallback.SubscriptionChanged(bool isSubscribed)
        {
            await App.Current.Dispatcher.InvokeAsync(async () => await this._guiCallback.SubscriptionChanged(isSubscribed));
        }

        async Task IGuiCallback.MessageAvailableAsync()
        {
            await App.Current.Dispatcher.InvokeAsync(async () => await this._guiCallback.MessageAvailableAsync());
        }

        async Task IGuiCallback.ServiceCallExceptionAsync(string operation, Exception exception)
        {
            await App.Current.Dispatcher.InvokeAsync(async () => await this._guiCallback.ServiceCallExceptionAsync(operation, exception));
        }

        #endregion
    }
}

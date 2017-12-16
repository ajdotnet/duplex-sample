using System;
using System.ServiceModel;
using System.Threading.Tasks;
using AJ.DuplexClient.Model;

#if USE_HTTP
using AJ.DuplexClient.SubscriptionsServiceReferenceHttp;
#else
using AJ.DuplexClient.SubscriptionsServiceReferenceNetTcp;
#endif

namespace AJ.DuplexClient.Repositories
{
    /// <summary>
    /// Class maintaining the duplex channel.
    /// This includes maintaining the client and detecting errors.
    /// </summary>
    /// <seealso cref="AJ.DuplexClient.SubscriptionsServiceReferenceNetTcp.ISubscriptionServiceCallback" />
    public sealed class SubscriptionRepository : ISubscriptionServiceCallback
    {
        /// <summary>The service proxy.</summary>
        private SubscriptionServiceClient _client;

        /// <summary>Interface through which the GUI will be notified.</summary>
        private IGuiCallback _guiCallback;

        /// <summary>The subscribed state</summary>
        private bool _isSubscribed;

        /// <summary>Initializes a new instance of the <see cref="SubscriptionRepository"/> class.</summary>
        /// <param name="guiCallback">Interface through which the GUI will be notified.</param>
        public SubscriptionRepository(IGuiCallback guiCallback)
        {
            this._guiCallback = guiCallback;
        }

        /// <summary>Opens a connection and subscribes to notifications.</summary>
        /// <returns>The subscription state.</returns>
        public async Task<bool> SubscribeAsync(string clientName)
        {
            if (this._client != null)
                return false;

            if (!await OpenAsync())
                return false;

            return await DoSubscribeAsync(clientName);
        }

        /// <summary>Unsubscribes from notifications and closes the connection.</summary>
        /// <returns>The subscription state.</returns>
        public async Task UnsubscribeAsync()
        {
            await DoUnsubscribeAsync();

            // whether we need to call Close() depends on whether the channel closed implicitely or not...
            await CloseAsync();
        }

        /// <summary>Opens the connection.</summary>
        /// <returns>The result.</returns>
        private async Task<bool> OpenAsync()
        {
            try
            {
                this._client = new SubscriptionServiceClient(new InstanceContext(this));

                // state change information (non-essential)
                _client.InnerChannel.Opening += async (s, e) => await _guiCallback.ConnectionStateChangedAsync("event", "Connection opening...");
                _client.InnerChannel.Opened += async (s, e) => await _guiCallback.ConnectionStateChangedAsync("event", "Connection opened.");
                _client.InnerChannel.Closing += async (s, e) => await _guiCallback.ConnectionStateChangedAsync("event", "Connection closing...");

                // maintaining the connection!
                // Note: These events come for netTcpBinding, but NOT for wsDualHttpBinding!
                _client.InnerChannel.Closed += InnerChannel_ClosedAsync;
                _client.InnerChannel.Faulted += InnerChannel_FaultedAsync;

                _client.Open(); // failure to open will also raise a faulted event!
                return true;
            }
            catch (Exception ex)
            {
                await _guiCallback.ServiceCallExceptionAsync("Open", ex);
                return false;
            }
        }

        private async Task<bool> DoSubscribeAsync(string clientName)
        {
            try
            {
                await _client.SubscribeAsync(clientName);
                SetSubscription(true);
                return true;
            }
            catch (Exception ex)
            {
                await _guiCallback.ServiceCallExceptionAsync("SubscribeAsync", ex);
                await CloseAsync();
                return false;
            }
        }

        private async Task DoUnsubscribeAsync()
        {
            try
            {
                // if the server has gone, we will get a timeout exception (HTTP only)
                await _client.UnsubscribeAsync();
                SetSubscription(false);
            }
            catch (Exception ex)
            {
                await _guiCallback.ServiceCallExceptionAsync("UnsubscribeAsync", ex);
                await CloseAsync();
            }
        }

        private async Task CloseAsync()
        {
            if (_client == null)
                return;

            // in case we didn't get the event (HTTP only)
            if (_client.State == CommunicationState.Faulted)
            {
                _client.Abort();
                return;
            }

            try
            {
                // if the server has gone, we will get the Closed event _and_ a timeout exception (HTTP only)
                _client.Close();
            }
            catch (Exception ex)
            {
                await _guiCallback.ServiceCallExceptionAsync("Close", ex);
            }
        }

        /// <summary>Handles the closed event.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void InnerChannel_ClosedAsync(object sender, EventArgs e)
        {
            // connection got closed and client is no longer needed...
            // this happens implicitly if the unsubscribe operation has IsTerminating=true.
            await _guiCallback.ConnectionStateChangedAsync("EVENT", "Connection closed.");

            // cleanup
            SetSubscription(false);
            _client = null;
        }

        /// <summary>Handles the faulted event.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void InnerChannel_FaultedAsync(object sender, EventArgs e)
        {
            await _guiCallback.ConnectionStateChangedAsync("EVENT", "Connection faulted! Aborting...");

            // abort() will raise closing and closed event.
            _client.Abort();
        }

        /// <summary>Sets the subscription state.</summary>
        /// <param name="set">if set to <c>true</c> the cleint has subscribed to service notifications.</param>
        private void SetSubscription(bool set)
        {
            if (_isSubscribed != set)
            {
                _isSubscribed = set;
                _guiCallback.ConnectionStateChangedAsync("SUBSCRIPTION", "Subscribed=" + _isSubscribed);
                _guiCallback.SubscriptionChanged(_isSubscribed);
            }
        }

        /// <summary>Notifications from the service.</summary>
        /// <param name="notification">The type of the notification.</param>
        /// <param name="clientName">Name of the client.</param>
        async void ISubscriptionServiceCallback.Notify(Notification notification, string clientName)
        {
            switch (notification)
            {
                case Notification.ClientAdded: await _guiCallback.ClientAddedAsync(clientName); break;
                case Notification.ClientRemoved: await _guiCallback.ClientRemovedAsync(clientName); break;
                case Notification.MessageAvailable: await _guiCallback.MessageAvailableAsync(); break;
            }
        }
    }
}

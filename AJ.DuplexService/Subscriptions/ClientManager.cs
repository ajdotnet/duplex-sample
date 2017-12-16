using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using static AJ.DuplexService.Common.ConsoleHelper;

namespace AJ.DuplexService.Subscriptions
{
    /// <summary>
    /// Singleton class maintaining registered clients. 
    /// This includes maintaining callback channels, notifying clients, and detecting errors.
    /// </summary>
    public sealed class ClientManager
    {
        #region members

        /// <summary>Singelton instance.</summary>
        public static ClientManager Instance { get; } = new ClientManager();

        /// <summary>The list of currently registered clients (including name and callback).</summary>
        private List<ClientRegistration> _clients = new List<ClientRegistration>();

        #endregion

        #region public methods

        /// <summary>Adds the client registration.</summary>
        /// <param name="serviceProtocol">The service protocol.</param>
        /// <param name="clientCallback">The client callback.</param>
        /// <param name="clientName">The client name.</param>
        public void AddClient(string serviceProtocol, ISubscriptionServiceCallback clientCallback, string clientName)
        {
            lock (_clients)
            {
                RegisterClient(serviceProtocol, clientCallback, clientName);
            }
        }

        /// <summary>Removes the client registration.</summary>
        /// <param name="clientCallback">The client callback.</param>
        public void RemoveClient(ISubscriptionServiceCallback clientCallback)
        {
            lock (_clients)
            {
                UnregisterClient(clientCallback, false);
            }
        }

        /// <summary>Gets the registered clients' names.</summary>
        /// <returns>The registered clients' names.</returns>
        public string[] GetClients()
        {
            lock (_clients)
            {
                return _clients.Select(rc => rc.ClientName).ToArray();
            }
        }

        /// <summary>Notifies all clients about some event.</summary>
        /// <param name="notification">The type of the notification.</param>
        /// <param name="details">Details for the event.</param>
        public void NotifyClients(Notification notification, string clientName)
        {
            lock (_clients)
            {
                NotifyClientsParallel(notification, clientName);
            }
        }

        /// <summary>Unregisters the client, caused via an event.</summary>
        /// <param name="clientCallback">The client callback.</param>
        /// <param name="faulted">Set to <c>true</c> if the client faulted.</param>
        /// <returns>The result.</returns>
        public bool UnregisterClientViaEvent(ISubscriptionServiceCallback clientCallback, bool faulted)
        {
            lock (_clients)
            {
                return UnregisterClient(clientCallback, faulted);
            }
        }

        #endregion

        #region implementation

        private bool RegisterClient(string serviceProtocol, ISubscriptionServiceCallback clientCallback, string clientName)
        {
            // only if not yet registered...
            var existing = GetRegisteredClient(clientCallback);
            if (existing != null)
                return false;

            _clients.Add(new ClientRegistration(serviceProtocol, clientCallback, clientName));

            // register event handlers to handle lost connections
            // Note: These events come for netTcpBinding, but NOT for wsDualHttpBinding!
            var clientChannel = (IContextChannel)clientCallback;
            clientChannel.Closing += (s, e) => UnregisterClientViaEvent(clientCallback, faulted: false);
            clientChannel.Faulted += (s, e) => UnregisterClientViaEvent(clientCallback, faulted: true);

            // timeout on the callback channel (no way to do this via configuration)
            clientChannel.OperationTimeout = TimeSpan.FromSeconds(10);

            // and notify...
            this.WriteLine(ConsoleColor.White, "Client added: " + serviceProtocol + "/" + clientName);
            NotifyClients(Notification.ClientAdded, clientName);
            return true;
        }

        private bool UnregisterClient(ISubscriptionServiceCallback clientCallback, bool faulted)
        {
            // only if actually registered...
            var existing = GetRegisteredClient(clientCallback);
            if (existing == null)
                return false;

            _clients.Remove(existing);

            // and notify...
            if (faulted)
                this.WriteLine(ConsoleColor.Yellow, "Client faulted: " + existing.ServiceProtocol + "/" + existing.ClientName);
            else
                this.WriteLine(ConsoleColor.White, "Client removed: " + existing.ServiceProtocol + "/" + existing.ClientName);
            NotifyClients(Notification.ClientRemoved, existing.ClientName);
            return true;
        }

        private ClientRegistration GetRegisteredClient(ISubscriptionServiceCallback clientCallback)
        {
            return _clients.Where(c => c.ClientCallback == clientCallback).FirstOrDefault();
        }

        // this would be the simple approach, ignoring error conditions...
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "documentation purposes...")]
        private void NotifyClientsSimple(Notification notification, string clientName)
        {
            // In order to avoid issues with concurrently adding/removing clients, we copy the list beforehand.
            var clients = _clients.ToArray();
            foreach (var cr in clients)
            {
                cr.ClientCallback.Notify(notification, clientName);
            }
        }

        private void NotifyClientsParallel(Notification notification, string clientName)
        {
            // In order to avoid issues with concurrently adding/removing clients, we copy the list beforehand.
            var clients = _clients.ToArray();

            // With wsDualHttpBinding we won't get closing or faulted events, thus we need to handle channel exceptions!
            // These usually appear as timeouts.
            // In order to shield other clients from the delay, we run the notifications on different threads.
            // Note: this also may lead to problems when all clients time out at the same time (see notes on receiveTimeout in app.config)
            foreach (var cr in clients)
            {
                Task.Run(() => NotifyClient(notification, cr, clientName));
            }
        }

        private void NotifyClient(Notification notification, ClientRegistration cr, string clientName)
        {
            string msg = "Notifying " + cr.ServiceProtocol + "/" + cr.ClientName + " about " + notification + " for " + clientName;
            try
            {
                // If the callback channels all timed out at the same time (see notes on receiveTimeout in app.config),
                // we will come here with an invalid callback channel.
                var cc = ((IContextChannel)cr.ClientCallback);
                if (cc.State == CommunicationState.Opened)
                {
                    // HTTP only: If the client has gone out of business, we will usually get a timeout exception
                    // (depending on IsOneWay on the operation).
                    this.WriteLine(ConsoleColor.DarkGray, msg + "...");
                    cr.ClientCallback.Notify(notification, clientName);
                    this.WriteLine(ConsoleColor.DarkGray, msg + ". Done.");
                }
            }
            catch (Exception ex)
            {
                this.WriteLine(ConsoleColor.Yellow, msg + ": " + ex.Message);
                UnregisterClient(cr.ClientCallback, true);
            }
        }

        #endregion 
    }
}

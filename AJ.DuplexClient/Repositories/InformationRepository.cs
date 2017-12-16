using System;
using System.Linq;
using System.Threading.Tasks;
using AJ.DuplexClient.Common;
using AJ.DuplexClient.Model;

#if USE_HTTP
using InformationServiceReference = AJ.DuplexClient.InformationServiceReferenceHttp;
#else
using InformationServiceReference = AJ.DuplexClient.InformationServiceReferenceNetTcp;
#endif

namespace AJ.DuplexClient.Repositories
{
    /// <summary>Class maintaining access to clients and messages.</summary>
    internal sealed class InformationRepository
    {
        /// <summary>Interface through which the GUI will be notified.</summary>
        private IGuiCallback _guiCallback;

        /// <summary>The timestamp of that latest message item.</summary>
        private DateTime _lastTimestamp;

        /// <summary>Initializes a new instance of the <see cref="SubscriptionRepository"/> class.</summary>
        /// <param name="guiCallback">Interface through which the GUI will be notified.</param>
        public InformationRepository(IGuiCallback guiCallback)
        {
            Guard.AssertNotNull(guiCallback, nameof(guiCallback));
            this._guiCallback = guiCallback;
        }

        /// <summary>Gets the registered clients.</summary>
        /// <returns>The registered clients returned from the service.</returns>
        public async Task<string[]> GetRegisteredClientsAsync()
        {
            return await CallAsync(nameof(GetRegisteredClientsAsync),
                async client => await client.GetRegisteredClientsAsync());
        }

        /// <summary>Sends a message to the service.</summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="message">The message.</param>
        /// <returns>Task for the async operation.</returns>
        public async Task<bool> SendMessageAsync(string clientName, string message)
        {
            return await CallAsync(nameof(SendMessageAsync), async client =>
            {
                await client.SendMessageAsync(clientName, message);
                return true;
            });
        }

        /// <summary>Gets the latest message items from the service.</summary>
        /// <returns>The messages.</returns>
        public async Task<MessageItem[]> GetLatestMessagesAsync()
        {
            // use the latest timestamp
            return await GetMessagesAsync();
        }

        /// <summary>Gets all message items from the service.</summary>
        /// <returns>The messages.</returns>
        public async Task<MessageItem[]> GetAllMessagesAsync()
        {
            // reset the latest timestamp to get all message items
            _lastTimestamp = DateTime.MinValue;
            return await GetMessagesAsync();
        }

        private async Task<MessageItem[]> GetMessagesAsync()
        {
            // get and map the data
            var response = await ClientGetMessagesAsync(_lastTimestamp);
            var result = response.MapResponse().ToArray();

            // update the latest timestamp from the received messages
            UpdateLatestTimestamp(result);
            return result;
        }

        private void UpdateLatestTimestamp(MessageItem[] result)
        {
            if (result.Length > 0)
                _lastTimestamp = result[result.Length - 1].Timestamp;
        }

        private async Task<InformationServiceReference.MessageItem[]> ClientGetMessagesAsync(DateTime after)
        {
            return await CallAsync(nameof(InformationServiceReference.InformationServiceClient.GetMessagesAsync),
                async client => await client.GetMessagesAsync(after));
        }

        private async Task<TResult> CallAsync<TResult>(string operation, Func<InformationServiceReference.InformationServiceClient, Task<TResult>> call)
        {
            // basic error handling
            try
            {
                var client = new InformationServiceReference.InformationServiceClient();
                return await call(client);
            }
            catch (Exception ex)
            {
                await _guiCallback.ServiceCallExceptionAsync(operation, ex);
                return default(TResult);
            }
        }
    }
}

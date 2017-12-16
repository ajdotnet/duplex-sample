using System.ServiceModel;

namespace AJ.DuplexService.Subscriptions
{
    /// <summary>WCF Service class for duplex communication.</summary>
    /// <seealso cref="AJ.DuplexService.Subscriptions.ISubscriptionService" />
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public abstract class SubscriptionService : ISubscriptionService
    {
        /// <summary>Registers the client callback for further notifications.</summary>
        /// <param name="clientName">Name of the client.</param>
        public void Subscribe(string clientName)
        {
            ClientManager.Instance.AddClient(GetProtocol(), GetClientCallback(), clientName);
        }

        /// <summary>Unregisters the client callback.</summary>
        public void Unsubscribe()
        {
            ClientManager.Instance.RemoveClient(GetClientCallback());
        }

        /// <summary>Gets the client callback from the <see cref="OperationContext" />.</summary>
        /// <returns>The client callback.</returns>
        private static ISubscriptionServiceCallback GetClientCallback()
        {
            return OperationContext.Current.GetCallbackChannel<ISubscriptionServiceCallback>();
        }

        /// <summary>Gets the protocol from the <see cref="OperationContext" />.</summary>
        /// <returns>The protocol.</returns>
        private static string GetProtocol()
        {
            var url = OperationContext.Current.EndpointDispatcher.EndpointAddress.Uri.ToString();
            var index = url.IndexOf(':');
            return url.Substring(0, index);
        }
    }

    /// <summary>Actual service class, to pick up the correct configuration.</summary>
    /// <seealso cref="AJ.DuplexService.Subscriptions.SubscriptionService" />
    public class SubscriptionServiceHttp : SubscriptionService
    {
    }

    /// <summary>Actual service class, to pick up the correct configuration.</summary>
    /// <seealso cref="AJ.DuplexService.Subscriptions.SubscriptionService" />
    public class SubscriptionServiceNetTcp : SubscriptionService
    {
    }
}

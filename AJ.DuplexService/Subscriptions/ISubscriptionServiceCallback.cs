using System.ServiceModel;

namespace AJ.DuplexService.Subscriptions
{
    /// <summary>Callback interface for clients.</summary>
    /// <remarks>
    /// Note: The name should always be the name of the inbound interface + "Callback",
    /// because that is what the generated client proxy will use anyways!
    /// </remarks>
    public interface ISubscriptionServiceCallback
    {
        /// <summary>Notifies the client about some event.</summary>
        /// <param name="notification">The type of the notification.</param>
        /// <param name="clientName">Name of the client.</param>
        [OperationContract]
        void Notify(Notification notification, string clientName);
    }
}

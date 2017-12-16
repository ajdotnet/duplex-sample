using System.ServiceModel;

namespace AJ.DuplexService.Subscriptions
{
    /// <summary>Service interface for duplex communication.</summary>
    [ServiceContract(
        Namespace = "urn://ajdotnet.duplexservice/subscriptions",
        SessionMode = SessionMode.Required,
        CallbackContract = typeof(ISubscriptionServiceCallback))]
    public interface ISubscriptionService
    {
        /// <summary>Registers the client callback for further notifications.</summary>
        /// <param name="clientName">Name of the client.</param>
        [OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        void Subscribe(string clientName);

        /// <summary>Unregisters the client callback.</summary>
        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = true)]
        void Unsubscribe();
    }
}

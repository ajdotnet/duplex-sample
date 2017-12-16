using System;
using System.ServiceModel;

namespace AJ.DuplexService.Information
{
    /// <summary>Service to accept and provide information (i.e. clients and messages).</summary>
    [ServiceContract(Namespace = "urn://ajdotnet.duplexservice/information")]
    public interface IInformationService
    {
        /// <summary>Accept a message from a client.</summary>
        /// <param name="clientName">The client's name.</param>
        /// <param name="message">The message.</param>
        [OperationContract]
        void SendMessage(string clientName, string message);

        /// <summary>Gets all messages after a provided time.</summary>
        /// <param name="after">The timestamp.</param>
        /// <returns>Messages that have been accepted after the timestamp.</returns>
        [OperationContract]
        MessageItem[] GetMessages(DateTime after);

        /// <summary>Gets a list of registerd clients.</summary>
        /// <returns>The list of registerd clients.</returns>
        [OperationContract]
        string[] GetRegisteredClients();
    }
}

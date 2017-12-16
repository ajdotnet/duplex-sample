using System;
using System.ServiceModel;
using AJ.DuplexService.Subscriptions;

namespace AJ.DuplexService.Information
{
    /// <summary>WCF Service class for regular request/response communication.</summary>
    /// <seealso cref="AJ.DuplexService.Information.IInformationService" />
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public abstract class InformationService : IInformationService
    {
        /// <summary>Accept a message from a client.</summary>
        /// <param name="clientName">The client's name.</param>
        /// <param name="message">The message.</param>
        public void SendMessage(string clientName, string message)
        {
            MessageManager.Current.AddMessage(clientName, message);
        }

        /// <summary>Gets all messages after a provided time.</summary>
        /// <param name="after">The timestamp.</param>
        /// <returns>Messages that have been accepted after the timestamp.</returns>
        public MessageItem[] GetMessages(DateTime after)
        {
            return MessageManager.Current.GetMessages(after);
        }

        /// <summary>Gets a list of registerd clients.</summary>
        /// <returns>The list of registerd clients.</returns>
        public string[] GetRegisteredClients()
        {
            return ClientManager.Instance.GetClients();
        }
    }

    /// <summary>Actual service class, to pick up the correct configuration.</summary>
    /// <seealso cref="AJ.DuplexService.Information.InformationService" />
    public class InformationServiceHttp : InformationService
    {
    }

    /// <summary>Actual service class, to pick up the correct configuration.</summary>
    /// <seealso cref="AJ.DuplexService.Information.InformationService" />
    public class InformationServiceNetTcp : InformationService
    {
    }
}
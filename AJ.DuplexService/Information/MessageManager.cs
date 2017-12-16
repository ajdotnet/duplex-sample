using System;
using System.Collections.Generic;
using System.Linq;
using AJ.DuplexService.Subscriptions;

using static AJ.DuplexService.Common.ConsoleHelper;

namespace AJ.DuplexService.Information
{
    /// <summary>Singleton class that maintains received messages.</summary>
    public class MessageManager
    {
        /// <summary>Singelton instance.</summary>
        public static MessageManager Current { get; } = new MessageManager();

        /// <summary>The list of reveived messages.</summary>
        List<MessageItem> _messages = new List<MessageItem>();

        /// <summary>Adds a message from a client.</summary>
        /// <param name="clientName">The client.</param>
        /// <param name="message">The message.</param>
        public void AddMessage(string clientName, string message)
        {
            lock (_messages)
            {
                this._messages.Add(new MessageItem(clientName, message));
            }

            // and notify...
            this.WriteLine(ConsoleColor.Gray, "Message received: " + clientName + "; message: " + message);
            ClientManager.Instance.NotifyClients(Notification.MessageAvailable, clientName);
        }

        /// <summary>Gets the messages after a provided timestamp.</summary>
        /// <param name="after">The timestamp.</param>
        /// <returns>Messages the have been accepted after the timestamp.</returns>
        public MessageItem[] GetMessages(DateTime after)
        {
            lock (_messages)
            {
                return _messages.Where(ii => ii.Timestamp > after).ToArray();
            }
        }
    }
}
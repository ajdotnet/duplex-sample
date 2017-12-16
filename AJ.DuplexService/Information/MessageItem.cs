using System;
using System.Runtime.Serialization;

namespace AJ.DuplexService.Information
{
    /// <summary>Messages together with a timestamp and the client that sent it.</summary>
    [DataContract(Namespace = "urn://ajdotnet.duplexservice/datacontract")]
    public class MessageItem
    {
        /// <summary>Gets or sets the timestamp.</summary>
        /// <value>The timestamp.</value>
        [DataMember]
        public DateTime Timestamp { get; set; }

        /// <summary>Gets or sets the client's name.</summary>
        /// <value>The client's name.</value>
        [DataMember]
        public string ClientName { get; set; }

        /// <summary>Gets or sets the message.</summary>
        /// <value>The message.</value>
        [DataMember]
        public string Message { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MessageItem"/> class.</summary>
        public MessageItem()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="MessageItem"/> class.</summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="message">The message.</param>
        public MessageItem(string clientName, string message)
        {
            this.Timestamp = DateTime.Now;
            this.ClientName = clientName;
            this.Message = message;
        }
    }
}
using System;

namespace AJ.DuplexClient.Model
{
    /// <summary>Messages together with timestamp and the client that provided the message.</summary>
    public class MessageItem
    {
        /// <summary>Gets or sets the timestamp.</summary>
        /// <value>The timestamp.</value>
        public DateTime Timestamp { get; set; }

        /// <summary>Gets or sets the name of the client.</summary>
        /// <value>The name of the client.</value>
        public string ClientName { get; set; }

        /// <summary>Gets or sets the message.</summary>
        /// <value>The message.</value>
        public string Message { get; set; }
    }
}
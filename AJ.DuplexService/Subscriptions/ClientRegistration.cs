namespace AJ.DuplexService.Subscriptions
{
    /// <summary>Information about registered clients.</summary>
    public class ClientRegistration
    {
        /// <summary>Gets or sets the name of the client.</summary>
        /// <value>The name of the client.</value>
        public string ClientName { get; }

        /// <summary>Gets or sets the client callback.</summary>
        /// <value>The client callback.</value>
        public ISubscriptionServiceCallback ClientCallback { get; }

        /// <summary>Gets the service protocol.</summary>
        /// <value>The service protocol.</value>
        public string ServiceProtocol { get; }

        /// <summary>Initializes a new instance of the <see cref="ClientRegistration" /> class.</summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="clientCallback">The client callback.</param>
        public ClientRegistration(string serviceProtocol, ISubscriptionServiceCallback clientCallback, string clientName)
        {
            this.ClientName = clientName;
            this.ClientCallback = clientCallback;
            this.ServiceProtocol = serviceProtocol;
        }
    }
}
namespace AJ.DuplexClient.Proxies
{
     /// <summary>Notifications to pass to clients.</summary>
    public enum Notification
    {
        /// <summary>A client has been added to the list of registered clients.</summary>
        ClientAdded,

        /// <summary>A client has been removed from the list of registered clients.</summary>
        ClientRemoved,

        /// <summary>A new message is available.</summary>
        MessageAvailable,
    }
}
using System;
using System.Threading.Tasks;

namespace AJ.DuplexClient.Model
{
    /// <summary>Interface through which the repository will talk to the GUI (in lieu of events).</summary>
    public interface IGuiCallback
    {
        /// <summary>The connection state changed.</summary>
        /// <param name="isSubscribed">if set to <c>true</c> the client has susbcribed to server callbacks.</param>
        /// <returns>Task for async.</returns>
        Task SubscriptionChanged(bool isSubscribed);

        /// <summary>A client has been added on the service.</summary>
        /// <param name="clientName">Name of the client.</param>
        /// <returns>Task for async.</returns>
        Task ClientAddedAsync(string clientName);

        /// <summary>A client has been removed on the service.</summary>
        /// <param name="clientName">Name of the client.</param>
        /// <returns>Task for async.</returns>
        Task ClientRemovedAsync(string clientName);

        /// <summary>A message is available on the server.</summary>
        /// <returns>Task for async.</returns>
        Task MessageAvailableAsync();

        /// <summary>The service call resulted in an exception.</summary>
        /// <param name="operation">The operation.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>Task for async.</returns>
        Task ServiceCallExceptionAsync(string operation, Exception exception);

        /// <summary>The connection state changed.</summary>
        /// <param name="info">Information about the state change.</param>
        /// <returns>Task for async.</returns>
        Task ConnectionStateChangedAsync(string category, string info);
    }
}
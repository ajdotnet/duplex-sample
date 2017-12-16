using System.Collections.Generic;
using System.Linq;
using AJ.DuplexClient.Common;

#if USE_HTTP
using InformationServiceReference = AJ.DuplexClient.InformationServiceReferenceHttp;
#else
using InformationServiceReference = AJ.DuplexClient.InformationServiceReferenceNetTcp;
#endif

namespace AJ.DuplexClient.Repositories
{
    /// <summary>Helpers to map generated data types to model data types.</summary>
    public static class Mapper
    {
        public static IEnumerable<Model.MessageItem> MapResponse(this IEnumerable<InformationServiceReference.MessageItem> response)
        {
            Guard.AssertNotNull(response, nameof(response));

            // map to model entities and sort by timestamp
            return response.MapToModel().OrderBy(item => item.Timestamp);
        }

        /// <summary>Maps generated data to model entities.</summary>
        /// <param name="proxy">The generated proxy data.</param>
        /// <returns>The mapped model entities.</returns>
        public static IEnumerable<Model.MessageItem> MapToModel(this IEnumerable<InformationServiceReference.MessageItem> proxy)
        {
            Guard.AssertNotNull(proxy, nameof(proxy));
            return proxy.Select(MapToEntity);
        }

        /// <summary>Maps generated data to a model entity.</summary>
        /// <param name="proxy">The generated proxy data.</param>
        /// <returns>The mapped model entity.</returns>
        public static Model.MessageItem MapToEntity(this InformationServiceReference.MessageItem proxy)
        {
            Guard.AssertNotNull(proxy, nameof(proxy));

            return new Model.MessageItem
            {
                ClientName = proxy.ClientName,
                Message = proxy.Message,
                Timestamp = proxy.Timestamp,
            };
        }
    }
}

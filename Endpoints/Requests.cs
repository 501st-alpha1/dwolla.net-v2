using System.Collections.Generic;
using System.Linq;
using Dwolla.SerializableTypes;

namespace Dwolla
{
    public class Requests : Rest
    {
        /// <summary>
        ///     Requests money from a user for a user associated with
        ///     the current OAuth token.
        /// </summary>
        /// <param name="sourceId">Dwolla ID to request funds from</param>
        /// <param name="amount">Amount to request</param>
        /// <param name="aParams">Additional parameters</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>Request ID of submitted request</returns>
        public int Create(string sourceId, double amount, Dictionary<string, string> aParams = null,
            string altToken = null)
        {
            var data = new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.dwolla_access_token},
                {"sourceId", sourceId},
                {"amount", amount.ToString()}
            };

            if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return DwollaParse<int>(Post("/requests", data));
        }

        /// <summary>
        ///     Retrieves a list of pending money requests for the user
        ///     associated with the current OAuth token.
        /// </summary>
        /// <param name="aParams">Additional parameters</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>A List of Request objects</returns>
        public List<Request> Get(Dictionary<string, string> aParams = null, string altToken = null)
        {
            var data = new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.dwolla_access_token}
            };

            if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return DwollaParse<List<Request>>(Get("/requests", data));
        }

        /// <summary>
        ///     Retrieves additional information about a pending money
        ///     request.
        /// </summary>
        /// <param name="requestId">Request ID to retrieve</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>A Request object</returns>
        public Request Info(string requestId, string altToken = null)
        {
            return DwollaParse<Request>(Get("/requests/" + requestId,
                new Dictionary<string, string> {{"oauth_token", altToken ?? C.dwolla_access_token}}));
        }

        /// <summary>
        ///     Cancels a pending money request
        /// </summary>
        /// <param name="requestId">Request ID to cancel</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>Empty string (sorry)</returns>
        public string Cancel(string requestId, string altToken = null)
        {
            return DwollaParse<string>(Post("/requests/" + requestId + "/cancel",
                new Dictionary<string, string> {{"oauth_token", altToken ?? C.dwolla_access_token}}));
        }

        /// <summary>
        ///     Fulfills a pending money request
        /// </summary>
        /// <param name="requestId">Request ID to fulfill</param>
        /// <param name="amount">Amount to fulfill</param>
        /// <param name="aParams">Additional parameters</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <param name="altPin">Alternate PIN</param>
        /// <returns>RequestFulfilled object</returns>
        public RequestFulfilled Fulfill(string requestId, double amount, Dictionary<string, string> aParams = null,
            string altToken = null, int? altPin = null)
        {
            var data = new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.dwolla_access_token},
                {"pin", (altPin != null) ? altPin.ToString() : C.dwolla_pin.ToString()},
                {"amount", amount.ToString()}
            };

            if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return DwollaParse<RequestFulfilled>(Post("/requests/" + requestId + "/fulfill", data));
        }
    }
}
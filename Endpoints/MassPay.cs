using System.Linq;
using System.Collections.Generic;

using Dwolla.SerializableTypes;

namespace Dwolla
{
    public class MassPay : Rest
    {
        /// <summary>
        ///     Creates MassPay job
        /// </summary>
        /// <param name="fundingSource">Funding source for jobs</param>
        /// <param name="items">List of MassPayItem objects</param>
        /// <param name="aParams">Additional parameters</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <param name="altPin">Alternate PIN</param>
        /// <returns>MassPayJob object</returns>
        public MassPayJob Create(string fundingSource, List<MassPayItem> items, Dictionary<string, object> aParams = null, string altToken = null,
            int? altPin = null)
        {
            var data = new Dictionary<string, object>
            {
                {"oauth_token", altToken ?? C.dwolla_access_token},
                {"pin", (altPin != null) ? altPin.ToString() : C.dwolla_pin.ToString()},
                {"fundsSource", fundingSource},
                {"items", items}
            };

            if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return DwollaParse<MassPayJob>(PostSpecial("/masspay", data));
        }

        /// <summary>
        ///     Check the status of an existing MassPay job and
        ///     returns additional information.
        /// </summary>
        /// <param name="id">Target job ID</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>MassPayJob item</returns>
        public MassPayJob GetJob(string id, string altToken = null)
        {
            return DwollaParse<MassPayJob>(Get("/masspay/" + id,
            new Dictionary<string, string>
                {
                    {"oauth_token", altToken ?? C.dwolla_access_token}
                }));
        }

        /// <summary>
        ///     Gets all items for a created MassPay job
        /// </summary>
        /// <param name="id">Target MassPay job ID</param>
        /// <param name="aParams">Dictionary with additional parameters</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>A List of MassPayItem objects</returns>
        public List<MassPayRetrievedItem> GetJobItems(string id, Dictionary<string, string> aParams = null,
            string altToken = null)
        {
            var data = new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.dwolla_access_token},
            };

            if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return DwollaParse<List<MassPayRetrievedItem>>(Get("/masspay/" + id + "/items", data));
        }

        /// <summary>
        ///     Gets an item from a created MassPay job
        /// </summary>
        /// <param name="jobId">Target MassPay job ID</param>
        /// <param name="itemId">Target MassPay item ID</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>MassPayItem object</returns>
        public MassPayRetrievedItem GetItem(string jobId, string itemId, string altToken = null)
        {
            return DwollaParse<MassPayRetrievedItem>(Get("/masspay/" + jobId + "/items/" + itemId,
            new Dictionary<string, string>
                {
                    {"oauth_token", altToken ?? C.dwolla_access_token}
                }));
        }

        /// <summary>
        ///     Lists all MassPay jobs for the user
        ///     associated with the current OAuth token
        /// </summary>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>List of MassPayJob objects</returns>
        public List<MassPayJob> ListJobs(string altToken = null)
        {
            return DwollaParse<List<MassPayJob>>(Get("/masspay",
            new Dictionary<string, string>
                {
                    {"oauth_token", altToken ?? C.dwolla_access_token}
                }));
        }
    }
}

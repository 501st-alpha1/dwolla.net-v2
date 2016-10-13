using System.Collections.Generic;
using System.Linq;
using Dwolla.SerializableTypes;

namespace Dwolla
{
    public class FundingSources : Rest
    {
        /// <summary>
        ///     Retrieves information about a funding source by ID.
        /// </summary>
        /// <param name="fundingId">Funding ID of target account</param>
        /// <param name="altToken">Alternate OAuth Token</param>
        /// <returns>FundingSource object</returns>
        public FundingSource Info(string fundingId, string altToken = null)
        {
            return DwollaParse<FundingSource>(Get("/fundingsources/" + fundingId,
                new Dictionary<string, string> {{"oauth_token", altToken ?? C.dwolla_access_token}}));
        }

        /// <summary>
        ///     Returns a list of funding sources associated to the account
        ///     under the current OAuth token.
        /// </summary>
        /// <param name="aParams">Additional parameters</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>A list of FundingSource objects</returns>
        public List<FundingSource> Get(Dictionary<string, string> aParams = null, string altToken = null)
        {
            var data = new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.dwolla_access_token}
            };

            if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return DwollaParse<List<FundingSource>>(Get("/fundingsources", data));
        }

        /// <summary>
        ///     Adds a funding source to the account under the current
        ///     OAuth token.
        /// </summary>
        /// <param name="account">Account number</param>
        /// <param name="routing">Routing number</param>
        /// <param name="type">Account type</param>
        /// <param name="name">User defined account nickname</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>The FundingSource object of the newly created funding source</returns>
        public FundingSource Add(string account, string routing, string type, string name, string altToken = null)
        {
            return DwollaParse<FundingSource>(Post("/fundingsources", new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.dwolla_access_token},
                {"account_number", account},
                {"routing_number", routing},
                {"account_type", type},
                {"name", name}
            }));
        }

        /// <summary>
        ///     Verifies a funding source for the account associated
        ///     with the funding ID under the current OAuth token via
        ///     the two micro-deposits.
        /// </summary>
        /// <param name="d1">Microdeposit 1</param>
        /// <param name="d2">Microdeposit 2</param>
        /// <param name="fundingId">Target funding ID</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>Successful verification?</returns>
        public bool? Verify(double d1, double d2, string fundingId, string altToken = null)
        {
            var fS = DwollaParse<FundingSource>(Post("/fundingsources/" + fundingId + "/verify",
                new Dictionary<string, string>
                {
                    {"oauth_token", altToken ?? C.dwolla_access_token},
                    {"deposit1", d1.ToString()},
                    {"deposit2", d2.ToString()}
                }));
            return fS.Verified;
        }

        /// <summary>
        ///     Withdraws funds from a Dwolla account to the funding source
        ///     associated with the passed ID, under the account associated
        ///     with the current OAuth token.
        /// </summary>
        /// <param name="amount">Amount to withdraw</param>
        /// <param name="fundingId">Target funding ID</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <param name="altPin">Alternate PIN</param>
        /// <returns>Resulting Transaction object</returns>
        public Transaction Withdraw(double amount, string fundingId, string altToken = null, int? altPin = null)
        {
            return DwollaParse<Transaction>(Post("/fundingsources/" + fundingId + "/withdraw",
                new Dictionary<string, string>
                {
                    {"oauth_token", altToken ?? C.dwolla_access_token},
                    {"pin", (altPin != null) ? altPin.ToString() : C.dwolla_pin.ToString()},
                    {"amount", amount.ToString()},
                }));
        }

        /// <summary>
        ///     Withdraws funds from a Dwolla account to the funding source
        ///     associated with the passed ID, under the account associated
        ///     with the current OAuth token.
        /// </summary>
        /// <param name="amount">Amount to deposit</param>
        /// <param name="fundingId">Target funding ID</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <param name="altPin">Alternate PIN</param>
        /// <returns>Resulting Transaction object</returns>
        public Transaction Deposit(double amount, string fundingId, string altToken = null, int? altPin = null)
        {
            return DwollaParse<Transaction>(Post("/fundingsources/" + fundingId + "/deposit",
                new Dictionary<string, string>
                {
                    {"oauth_token", altToken ?? C.dwolla_access_token},
                    {"pin", (altPin != null) ? altPin.ToString() : C.dwolla_pin.ToString()},
                    {"amount", amount.ToString()},
                }));
        }
    }
}
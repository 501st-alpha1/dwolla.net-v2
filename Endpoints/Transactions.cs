using System;
using System.Linq;
using System.Collections.Generic;
using Dwolla.SerializableTypes;

namespace Dwolla
{
    public class Transactions : Rest
    {
        /// <summary>
        ///     Sends money to specified destination user
        /// </summary>
        /// <param name="destinationId">Destination Dwolla ID</param>
        /// <param name="amount">Amount to send</param>
        /// <param name="aParams">Additional parameters</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <param name="altPin">Alternate pin</param>
        /// <returns>Resulting transaction ID</returns>
        public int? Send(string destinationId, double amount, Dictionary<string, string> aParams = null,
            string altToken = null, int? altPin = null)
        {
            var data = new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.dwolla_access_token},
                {"pin", (altPin != null) ? altPin.ToString() : C.dwolla_pin.ToString()},
                {"destinationId", destinationId},
                {"amount", amount.ToString()}
            };

            if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return DwollaParse<int?>(Post("/transactions/send", data));
        }

        /// <summary>
        ///     Lists transactions for the user associated with
        ///     the currently set OAuth token.
        /// </summary>
        /// <param name="aParams">Additional parameters</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>A List of Transaction objects</returns>
        public List<Transaction> Get(Dictionary<string, string> aParams = null, string altToken = null)
        {
            var data = new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.dwolla_access_token},
                {"client_id", C.dwolla_key},
                {"client_secret", C.dwolla_secret}
            };

            if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return DwollaParse<List<Transaction>>(Get("/transactions", data));
        }

        /// <summary>
        ///     Returns transaction information for the transaction
        ///     associated with the passed transaction ID
        /// </summary>
        /// <param name="transactionId">Target transaction ID</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>Transaction object</returns>
        public Transaction Info(string transactionId, string altToken = null)
        {
            return DwollaParse<Transaction>(Get("/transactions/" + transactionId,
            new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.dwolla_access_token},
                {"client_id", C.dwolla_key},
                {"client_secret", C.dwolla_secret}
            }));
        }

        /// <summary>
        ///     Refunds (either completely or partially) funds to
        ///     the sending user for a transaction.
        /// </summary>
        /// <param name="transactionId">Transaction ID</param>
        /// <param name="fundingSource">Funding source to fund transaction</param>
        /// <param name="amount">Amount to refund</param>
        /// <param name="aParams">Additional parameters</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <param name="altPin">Alternate parameters</param>
        /// <returns>Refund object</returns>
        public Refund Refund(string transactionId, string fundingSource, double amount,
            Dictionary<string, string> aParams, string altToken = null, int? altPin = null)
        {
            var data = new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.dwolla_access_token},
                {"pin", (altPin != null) ? altPin.ToString() : C.dwolla_pin.ToString()},
                {"fundsSource", fundingSource},
                {"transactionId", transactionId},
                {"amount", amount.ToString()}
            };

            if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return DwollaParse<Refund>(Post("/transactions/refund", data));
        }

        /// <summary>
        ///     Retrieves transaction statistics for
        ///     the user associated with the current OAuth token.
        /// </summary>
        /// <param name="aParams">Additional parameters</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>TransactionStats object</returns>
        public TransactionStats Stats(Dictionary<string, string> aParams = null, string altToken = null)
        {
            var data = new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.dwolla_access_token}
            };

            if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return DwollaParse<TransactionStats>(Get("/transactions/stats", data));
        }

        /// <summary>
        ///     Schedule a payment to a Dwolla user
        /// </summary>
        /// <param name="destinationId">Destination Dwolla ID</param>
        /// <param name="amount">Amount to send</param>
        /// <param name="scheduleDate">Date on which to send funds</param>
        /// <param name="fundsSource">Funding source ID to fund transaction</param>
        /// <param name="recurrence">Details of recurring payment if desired</param>
        /// <param name="aParams">Additional parameters</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <param name="altPin">Alternate pin</param>
        /// <returns>ScheduledTransaction object</returns>
        public ScheduledTransaction Schedule(string destinationId, double amount, string scheduleDate, string fundsSource, ScheduledRecurrence recurrence = null,
            Dictionary<string, object> aParams = null, string altToken = null, int? altPin = null)
        {
            var data = new Dictionary<string, object>
            {
                {"oauth_token", altToken ?? C.dwolla_access_token},
                {"pin", (altPin != null) ? altPin.ToString() : C.dwolla_pin.ToString()},
                {"destinationId", destinationId},
                {"amount", amount.ToString()},
                {"scheduleDate", scheduleDate},
                {"fundsSource", fundsSource}
            };

            if (recurrence != null) data.Add("recurrence", recurrence);

            if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return DwollaParse<ScheduledTransaction>(PostSpecial("/transactions/scheduled", data));
        }

        /// <summary>
        ///     Retrieves data about all scheduled transactions
        /// </summary>
        /// <param name="aParams">Additional parameters</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>ScheduledTransactions object (plural, keep in mind)</returns>
        public ScheduledTransactions GetScheduled(Dictionary<string, string> aParams = null, string altToken = null)
        {
            var data = new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.dwolla_access_token}
            };

            if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return DwollaParse<ScheduledTransactions>(Get("/transactions/scheduled", data));
        }

        /// <summary>
        ///     Retrieves data about all scheduled transactions
        /// </summary>
        /// <param name="id">Scheduled transaction ID</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <returns>ScheduledTransactions object (plural, keep in mind)</returns>
        public ScheduledTransaction GetScheduledById(string id, string altToken = null)
        {
            return DwollaParse<ScheduledTransaction>(Get("/transactions/scheduled/" + id, new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.dwolla_access_token}
            }));
        }

        /// <summary>
        ///     Edit a previously made scheduled transaction. Pass in parameters
        ///     that you wish to edit in aParams. The recurrence object cannot be 
        ///     edited. 
        /// </summary>
        /// <param name="aParams">Additional parameters</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <param name="altPin">Alternate pin</param>
        /// <returns>ScheduledTransaction object</returns>
        public ScheduledTransaction EditScheduleById(string id, Dictionary<string, string> aParams = null, string altToken = null, int? altPin = null)
        {
            var data = new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.dwolla_access_token},
                {"pin", (altPin != null) ? altPin.ToString() : C.dwolla_pin.ToString()}
            };

            if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
            return DwollaParse<ScheduledTransaction>(Put("/transactions/scheduled/" + id, data));
        }

        /// <summary>
        ///     Delete scheduled transaction by ID
        /// </summary>
        /// <param name="id">Target scheduled transaction</param>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <param name="altPin">Alternate PIN</param>
        /// <returns>String with ID of deleted transaction</returns>
        public string DeleteScheduledById(string id, string altToken = null, int? altPin = null)
        {
            return DwollaParse<string>(Delete("/transactions/scheduled/" + id, new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.dwolla_access_token},
                {"pin", (altPin != null) ? altPin.ToString() : C.dwolla_pin.ToString()}
            }));
        }

        /// <summary>
        ///     Delete ALL scheduled transactions
        /// </summary>
        /// <param name="altToken">Alternate OAuth token</param>
        /// <param name="altPin">Alternate PIN</param>
        /// <returns>List of strings with IDs of deleted transactions</returns>
        public List<string> DeleteAllScheduled(string altToken = null, int? altPin = null)
        {
            return DwollaParse<List<string>>(Delete("/transactions/scheduled", new Dictionary<string, string>
            {
                {"oauth_token", altToken ?? C.dwolla_access_token},
                {"pin", (altPin != null) ? altPin.ToString() : C.dwolla_pin.ToString()}
            }));
        }

    }
}

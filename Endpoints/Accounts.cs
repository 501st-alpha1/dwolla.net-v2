using System.Collections.Generic;
using DwollaV2.SerializableTypes;

namespace DwollaV2
{
  public class Accounts : Rest
  {
    /// <summary>
    ///   Returns basic account info for the passed account ID.
    /// </summary>
    /// <param name="accountId">Account ID</param>
    /// <returns>UserBasic object</returns>
    public UserBasic Basic(string accountId)
    {
      var data = new Dictionary<string, string>
      {
        {"client_id", C.dwolla_key},
        {"client_secret", C.dwolla_secret},
      };

      return DwollaParse<UserBasic>(Get("/users/" + accountId, data)).Response;
    }

    /// <summary>
    ///   Returns full account information for the user associated
    ///   with the currently set OAuth token.
    /// </summary>
    /// <param name="altToken">Alternate OAuth token</param>
    /// <returns>UserFull object</returns>
    public UserFull Full(string altToken = null)
    {
      var data = new Dictionary<string, string>
      {
        {"oauth_token", altToken ?? C.dwolla_access_token}
      };

      return DwollaParse<UserFull>(Get("/users", data)).Response;
    }

    /// <summary>
    ///   Returns balance for the account associated with the
    ///   currently set OAuth token.
    /// </summary>
    /// <param name="altToken">Alternate OAuth token</param>
    /// <returns>Balance as double</returns>
    public double Balance(string altToken = null)
    {
      var data = new Dictionary<string, string>
      {
        {"oauth_token", altToken ?? C.dwolla_access_token}
      };

      return DwollaParse<double>(Get("/balance", data)).Response;
    }

    /// <summary>
    ///   Returns users and venues near a location.
    /// </summary>
    /// <param name="lat">Latitudinal coordinates</param>
    /// <param name="lon">Longitudinal coordinates</param>
    /// <returns>List of UserNearby objects</returns>
    public List<UserNearby> Nearby(double lat, double lon)
    {
      var data = new Dictionary<string, string>
      {
        {"client_id", C.dwolla_key},
        {"client_secret", C.dwolla_secret},
        {"latitude", lat.ToString()},
        {"longitude", lon.ToString()}
      };

      return DwollaParse<List<UserNearby>>(Get("/users/nearby", data)).Response;
    }

    /// <summary>
    ///   Gets auto withdrawal status for the account associated
    ///   with the currently set OAuth token.
    /// </summary>
    /// <param name="altToken">Alternate OAuth token</param>
    /// <returns>AutoWithdrawalStatus object</returns>
    public AutoWithdrawalStatus GetAutoWithdrawalStatus(string altToken = null)
    {
      var data = new Dictionary<string, string>
      {
        {"oauth_token", altToken ?? C.dwolla_access_token}
      };

      return DwollaParse<AutoWithdrawalStatus>(Get("/accounts/features/auto_withdrawl",
        data)).Response;
    }

    /// <summary>
    ///   Sets auto-withdrawal status of the account associated
    ///   with the current OAuth token under the specified
    ///   funding ID.
    /// </summary>
    /// <param name="status">Enable toggle</param>
    /// <param name="fundingId">Target funding ID</param>
    /// <param name="altToken">Alternate OAuth token</param>
    /// <returns>Account autowithdrawal status</returns>
    public bool ToggleAutoWithdrawalStatus(bool status, string fundingId, string altToken = null)
    {
      var data = new Dictionary<string, string>
      {
        {"oauth_token", altToken ?? C.dwolla_access_token}
      };
      var r = DwollaParse<string>(Post("/accounts/features/auto_withdrawl",
                                       data));

      // I figure this will be more useful than the string
      return r.Response == "Enabled";
    }
  }
}

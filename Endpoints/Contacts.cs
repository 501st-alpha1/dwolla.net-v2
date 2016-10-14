using System.Collections.Generic;
using System.Linq;
using Dwolla.SerializableTypes;

namespace Dwolla
{
  public class Contacts : Rest
  {
    /// <summary>
    ///   Gets contacts from user associated with OAuth token
    /// </summary>
    /// <param name="aParams">Dictionary with additional parameters</param>
    /// <param name="altToken">Alternate OAuth token</param>
    /// <returns>List of Contacts</returns>
    public List<Contact> Get(Dictionary<string, string> aParams = null, string altToken = null)
    {
      var data = new Dictionary<string, string>
      {
        {"oauth_token", altToken ?? C.dwolla_access_token}
      };

      if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
      return DwollaParse<List<Contact>>(Get("/contacts", data));
    }

    /// <summary>
    ///   Returns Dwolla spots near the specified geographical location
    /// </summary>
    /// <param name="lat">Latitudinal coordinates</param>
    /// <param name="lon">Longitudinal coordinates</param>
    /// <param name="aParams">Dictionary with additional parameters</param>
    /// <returns>List of UserNearby</returns>
    public List<UserNearby> Nearby(double lat, double lon, Dictionary<string, string> aParams = null)
    {
      var data = new Dictionary<string, string>
      {
        {"client_id", C.dwolla_key},
        {"client_secret", C.dwolla_secret},
        {"latitude", lat.ToString()},
        {"longitude", lon.ToString()}
      };

      if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
      return DwollaParse<List<UserNearby>>(Get("/contacts/nearby", data));
    }
  }
}

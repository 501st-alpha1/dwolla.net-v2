using System.Collections.Generic;
using System.Linq;
using DwollaV2.SerializableTypes;

namespace DwollaV2
{
  public class Customers : Rest
  {
    /// <summary>
    ///   Creates a new Customer.
    /// </summary>
    /// <param name="aParams">Additional parameters</param>
    /// <param name="altToken">Alternate OAuth token</param>
    /// <returns>Customer object</returns>
    public Customer Create(Dictionary<string, string> aParams = null, string altToken = null)
    {
      var data = new Dictionary<string, string>
      {
        {"oauth_token", altToken ?? C.dwolla_access_token},
      };

      if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
      return DwollaParse<Customer>(Post("/customers", data)).Response;
    }
  }
}

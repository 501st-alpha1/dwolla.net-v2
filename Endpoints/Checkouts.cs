using System.Linq;
using System.Collections.Generic;

using DwollaV2.SerializableTypes;

namespace DwollaV2
{
  public class Checkouts : Rest
  {
    /// <summary>
    ///   Creates an off-site gateway checkout session
    /// </summary>
    /// <param name="po">PurchaseOrder object</param>
    /// <param name="aParams">Additional parameters</param>
    /// <returns>Checkout URL</returns>
    public string Create(PurchaseOrder po, Dictionary<string, object> aParams = null)
    {
      var data = new Dictionary<string, object>
      {
        {"client_id", C.dwolla_key},
        {"client_secret", C.dwolla_secret},
        {"purchaseOrder", po}
      };

      if (aParams != null) data = aParams.Union(data).ToDictionary(k => k.Key, v => v.Value);
      return (C.dwolla_sandbox ? C.dwolla_sandbox_host : C.dwolla_production_host)
        + "payment/checkout/" + DwollaParse<CheckoutID>(PostSpecial("/offsitegateway/checkouts", data)).Response.CheckoutId;
    }

    /// <summary>
    ///   Retrieves information (status, etc.) from an existing checkout
    /// </summary>
    /// <param name="checkoutId">Checkout ID</param>
    /// <returns>Checkout object</returns>
    public Checkout Get(string checkoutId)
    {
      var data = new Dictionary<string, string>
      {
        {"client_id", C.dwolla_key},
        {"client_secret", C.dwolla_secret}
      };

      return DwollaParse<Checkout>(Get("/offsitegateway/checkouts/"
                                       + checkoutId, data)).Response;
    }

    /// <summary>
    ///   Completes an offsite-gateway "Pay Later" checkout session.
    /// </summary>
    /// <param name="checkoutId">Checkout ID</param>
    /// <returns>CheckoutComplete object</returns>
    public CheckoutComplete Complete(string checkoutId)
    {
      var data = new Dictionary<string, string>
      {
        {"client_id", C.dwolla_key},
        {"client_secret", C.dwolla_secret}
      };

      return DwollaParse<CheckoutComplete>(Post("/offsitegateway/checkouts/"
                                                + checkoutId + "/complete",
                                                data)).Response;
    }
  }
}

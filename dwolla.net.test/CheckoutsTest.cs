using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Dwolla;
using Dwolla.SerializableTypes;

namespace dwolla.net.test
{
  [TestClass]
  public class CheckoutsTest
  {
    public Checkouts c = new Checkouts();

    /// <summary>
    ///   We create a request, grab its info, then cancel it.
    /// </summary>
    [TestMethod]
    public void TestAll()
    {
      var order = new PurchaseOrder();
      order.destinationId = "812-174-9528";
      order.total = 5.50;

      var url = c.Create(order, new Dictionary<string, object>{{"redirect", "http://www.google.com"}});
      Assert.IsInstanceOfType(url, typeof(string));

      var info = c.Get(url.Substring(url.LastIndexOf('/') + 1));
      Assert.IsInstanceOfType(info, typeof(Checkout));

      // Verifying that this endpoint works is left as an 
      // exercise for the reader, mainly because it is difficult
      // to do this in a responsive manner with Travis-CI integration.
      /*
       * var complete = c.Complete(url.Substring(url.LastIndexOf('/') + 1));
       * Assert.IsInstanceOfType(info, typeof(CheckoutComplete));
       */
    }
  }
}

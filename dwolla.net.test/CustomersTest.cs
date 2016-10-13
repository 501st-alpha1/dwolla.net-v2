using System;
using System.Collections.Generic;
using Dwolla;
using Dwolla.SerializableTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dwolla.net.test
{
  [TestClass]
  public class CustomersTest
  {
    public Customers c = new Customers();

    [TestMethod]
    public void TestCreate()
    {
      Guid guid = Guid.NewGuid();
      var result = c.Create(new Dictionary<string, string>
      {
        {"firstName", "John"},
        {"lastName", "Smith"},
        {"email", "jsmith" + guid + "@example.com"},
        {"ipAddress", "127.0.0.1"},
      });
      Assert.IsInstanceOfType(result, typeof(Customer));
    }
  }
}

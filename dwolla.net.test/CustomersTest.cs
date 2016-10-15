﻿using System.Collections.Generic;
using DwollaV2;
using DwollaV2.SerializableTypes;
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
      var result = c.Create(new Dictionary<string, string>
      {
        {"firstName", "John"},
        {"lastName", "Smith"},
        {"email", "jsmith@example.com"},
        {"ipAddress", "127.0.0.1"},
      });
      Assert.IsInstanceOfType(result, typeof(Customer));
    }
  }
}
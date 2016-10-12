using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DwollaV2;
using DwollaV2.SerializableTypes;

namespace dwolla.net.test
{
  [TestClass]
  public class ContactsTest
  {
    public Contacts c = new Contacts();

    [TestMethod]
    public void TestGet()
    {
      var result = c.Get();
      Assert.IsInstanceOfType(result, typeof(List<Contact>));
    }

    [TestMethod]
    public void TestNearby()
    {
      var result = c.Nearby(0, 0);
      Assert.IsInstanceOfType(result, typeof(List<UserNearby>));
    }
  }
}

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dwolla;
using Dwolla.SerializableTypes;

namespace dwolla.net.test
{
  [TestClass]
  public class ContactsTest
  {
    public Contacts c = new Contacts();

    [TestMethod]
    public void TestGet()
    {
      var result = c.Get(altToken: "raopmI6N9UIq87uWqhXB5v7xIgi49EH3K3qSFwtoZ/CzcBCN+l");
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

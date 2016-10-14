using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DwollaV2;
using DwollaV2.SerializableTypes;

namespace dwolla.net.test
{
  [TestClass]
  public class AccountsTest
  {
    public Accounts a = new Accounts();

    [TestMethod]
    public void TestBasic()
    {
      var result = a.Basic("812-111-7219");
      Assert.IsInstanceOfType(result, typeof(UserBasic));
    }

    [TestMethod]
    public void TestFull()
    {
      var result = a.Full();
      Assert.IsInstanceOfType(result, typeof(UserFull));
    }

    [TestMethod]
    public void TestBalance()
    {
      var result = a.Balance();
      Assert.IsInstanceOfType(result, typeof(double));
    }

    [TestMethod]
    public void TestNearby()
    {
      var result = a.Nearby(25, 25);
      Assert.IsInstanceOfType(result, typeof(List<UserNearby>));
    }

    [TestMethod]
    public void TestAWS()
    {
      var result = a.GetAutoWithdrawalStatus();
      Assert.IsInstanceOfType(result, typeof(AutoWithdrawalStatus));
    }
  }
}

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dwolla;
using Dwolla.SerializableTypes;

namespace dwolla.net.test
{
  [TestClass]
  public class OAuthTest
  {
    public OAuth o = new OAuth();

    [TestMethod]
    public void TestGenUrl()
    {
      Assert.AreEqual(new Uri("https://uat.dwolla.com/oauth/v2/authenticate?client_id=HFpRm2QGHYOjqEeNug1BRGoTdtlhozgGchwmi29%2b2eKTEqaHze&response_type=code&scope=Send|Transactions|Balance|Request|Contacts|AccountInfoFull|Funding|ManageAccount"), o.GenAuthUrl());
    }
  }
}

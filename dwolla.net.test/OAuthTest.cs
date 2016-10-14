using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DwollaV2;
using DwollaV2.SerializableTypes;
using System.Web;
using System.Configuration;

namespace dwolla.net.test
{
  [TestClass]
  public class OAuthTest
  {
    public OAuth o = new OAuth();

    [TestMethod]
    public void TestGenUrl()
    {
      string key = ConfigurationManager.AppSettings["dwolla_key"];
      string id = HttpUtility.UrlEncode(key);
      Assert.AreEqual(new Uri("https://uat.dwolla.com/oauth/v2/authenticate?client_id=" + id + "&response_type=code&scope=Send|Transactions|Balance|Request|Contacts|AccountInfoFull|Funding|ManageAccount"), o.GenAuthUrl());
    }
  }
}

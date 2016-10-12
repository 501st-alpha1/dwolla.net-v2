using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DwollaV2;
using DwollaV2.SerializableTypes;

namespace dwolla.net.test
{
  [TestClass]
  public class FundingSourcesTest
  {
    public FundingSources f = new FundingSources();

    /// <summary>
    ///   Before running this test, please ensure that the test account
    ///   does not have any number of bank accounts added, or Add will fail.
    ///
    ///   Verify is left as an exercise for the reader, mainly because
    ///   you have to wait for the non-existent micro-deposits to clear.
    ///
    ///   For the same reasons, withdraw and deposit have been omitted
    ///   (no account to test with).
    /// </summary>
    [TestMethod]
    public void TestAddInfoGet()
    {
      var r = new Random();

      var newSource = f.Add(r.Next(99999999).ToString(), "021000021", "Checking", "My Bank");
      Assert.IsInstanceOfType(newSource, typeof(FundingSource));

      /*
       * var verify = f.Verify(0.01, 0.05, newSource.Id, "raopmI6N9UIq87uWqhXB5v7xIgi49EH3K3qSFwtoZ/CzcBCN+l");
       * Assert.IsInstanceOfType(verify, typeof(bool));
       */

      var info = f.Info(newSource.Id);
      Assert.IsInstanceOfType(info, typeof(FundingSource));

      var list = f.Get();
      Assert.IsInstanceOfType(list, typeof(List<FundingSource>));
    }
  }
}

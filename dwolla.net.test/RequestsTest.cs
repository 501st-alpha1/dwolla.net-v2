using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dwolla;
using Dwolla.SerializableTypes;

namespace dwolla.net.test
{
  [TestClass]
  public class RequestsTest
  {
    public Requests r = new Requests();

    /// <summary>
    ///   We create a request, grab its info, then cancel it.
    /// </summary>
    [TestMethod]
    public void TestCreateInfoCancel()
    {
      var create = r.Create("812-174-9528", 0.01, altToken: "raopmI6N9UIq87uWqhXB5v7xIgi49EH3K3qSFwtoZ/CzcBCN+l");
      Assert.IsInstanceOfType(create, typeof(int));

      var info = r.Info(create.ToString(), altToken: "raopmI6N9UIq87uWqhXB5v7xIgi49EH3K3qSFwtoZ/CzcBCN+l");
      Assert.IsInstanceOfType(info, typeof(Request));

      var cancel = r.Cancel(create.ToString(), altToken: "raopmI6N9UIq87uWqhXB5v7xIgi49EH3K3qSFwtoZ/CzcBCN+l");
      Assert.IsInstanceOfType(cancel, typeof(string));
    }

    [TestMethod]
    public void TestGet()
    {
      var result = r.Get(altToken: "raopmI6N9UIq87uWqhXB5v7xIgi49EH3K3qSFwtoZ/CzcBCN+l");
      Assert.IsInstanceOfType(result, typeof(List<Request>));
    }
  }
}

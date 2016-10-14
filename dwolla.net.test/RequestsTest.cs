using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DwollaV2;
using DwollaV2.SerializableTypes;

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
      var create = r.Create("812-174-9528", 0.01);
      Assert.IsInstanceOfType(create, typeof(int));

      var info = r.Info(create.ToString());
      Assert.IsInstanceOfType(info, typeof(Request));

      var cancel = r.Cancel(create.ToString());
      Assert.IsInstanceOfType(cancel, typeof(string));
    }

    [TestMethod]
    public void TestGet()
    {
      var result = r.Get();
      Assert.IsInstanceOfType(result, typeof(List<Request>));
    }
  }
}

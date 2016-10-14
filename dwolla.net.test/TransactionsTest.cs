using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dwolla;
using Dwolla.SerializableTypes;

namespace dwolla.net.test
{
  [TestClass]
  public class TransactionsTest
  {
    public Transactions t = new Transactions();

    /// <summary>
    ///   Instead of creating an ordered test, this is more readable
    ///   and makes sense to do chronologically (e.g send then check)
    /// </summary>
    [TestMethod]
    public void TestSendandInfo()
    {
      var send = t.Send("812-174-9528", 0.01, altToken: "raopmI6N9UIq87uWqhXB5v7xIgi49EH3K3qSFwtoZ/CzcBCN+l", altPin: 1337);
      Assert.IsInstanceOfType(send, typeof(int));

      var info = t.Info(send.ToString(), "raopmI6N9UIq87uWqhXB5v7xIgi49EH3K3qSFwtoZ/CzcBCN+l");
      Assert.IsInstanceOfType(info, typeof(Transaction));
    }

    [TestMethod]
    public void TestGet()
    {
      var result = t.Get(altToken: "raopmI6N9UIq87uWqhXB5v7xIgi49EH3K3qSFwtoZ/CzcBCN+l");
      Assert.IsInstanceOfType(result, typeof(List<Transaction>));
    }

    [TestMethod]
    public void TestStats()
    {
      var result = t.Stats(altToken: "raopmI6N9UIq87uWqhXB5v7xIgi49EH3K3qSFwtoZ/CzcBCN+l");
      Assert.IsInstanceOfType(result, typeof(TransactionStats));
    }
  }
}

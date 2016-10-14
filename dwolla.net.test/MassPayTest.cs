using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dwolla;
using Dwolla.SerializableTypes;

namespace dwolla.net.test
{
  [TestClass]
  public class MassPayTest
  {
    public MassPay m = new MassPay();

    [TestMethod]
    public void TestAll()
    {
      var items = new List<MassPayItem>()
      {
        {
          new MassPayItem()
          {
            amount = 5.50,
            destination = "812-174-9528",
            destinationType = "Dwolla",
            notes = "Mmm, a unit test!"
          }
        }
      };
      var job = m.Create("Balance", items, altToken: "raopmI6N9UIq87uWqhXB5v7xIgi49EH3K3qSFwtoZ/CzcBCN+l", altPin: 1337);
      Assert.IsInstanceOfType(job, typeof(MassPayJob));

      var retjob = m.GetJob(job.Id, "raopmI6N9UIq87uWqhXB5v7xIgi49EH3K3qSFwtoZ/CzcBCN+l");
      Assert.IsInstanceOfType(retjob, typeof(MassPayJob));

      var jobItems = m.GetJobItems(job.Id, altToken: "raopmI6N9UIq87uWqhXB5v7xIgi49EH3K3qSFwtoZ/CzcBCN+l");
      Assert.IsInstanceOfType(jobItems, typeof(List<MassPayRetrievedItem>));

      var item = m.GetItem(job.Id, jobItems[0].ItemId, altToken: "raopmI6N9UIq87uWqhXB5v7xIgi49EH3K3qSFwtoZ/CzcBCN+l");
      Assert.IsInstanceOfType(item, typeof(MassPayRetrievedItem));

      var jobList = m.ListJobs("raopmI6N9UIq87uWqhXB5v7xIgi49EH3K3qSFwtoZ/CzcBCN+l");
      Assert.IsInstanceOfType(jobList, typeof(List<MassPayJob>));
    }
  }
}

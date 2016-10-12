using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DwollaV2;
using DwollaV2.SerializableTypes;

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
      var job = m.Create("Balance", items);
      Assert.IsInstanceOfType(job, typeof(MassPayJob));

      var retjob = m.GetJob(job.Id);
      Assert.IsInstanceOfType(retjob, typeof(MassPayJob));

      var jobItems = m.GetJobItems(job.Id);
      Assert.IsInstanceOfType(jobItems, typeof(List<MassPayRetrievedItem>));

      var item = m.GetItem(job.Id, jobItems[0].ItemId);
      Assert.IsInstanceOfType(item, typeof(MassPayRetrievedItem));

      var jobList = m.ListJobs();
      Assert.IsInstanceOfType(jobList, typeof(List<MassPayJob>));
    }
  }
}

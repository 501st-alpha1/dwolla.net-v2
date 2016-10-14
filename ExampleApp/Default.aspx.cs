using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;

using DwollaV2;
using DwollaV2.SerializableTypes;

namespace ExampleApp
{
    public partial class _Default : Page
    {
        protected void tHistButton_Click(object sender, EventArgs e)
        {
            var t = new Transactions();
            foreach (Transaction z in t.Get(altToken: "raopmI6N9UIq87uWqhXB5v7xIgi49EH3K3qSFwtoZ/CzcBCN+l"))
            {
                Console.WriteLine("Transaction found");
                TransHistoryLB.Items.Add(new ListItem(String.Format("ID: {0}, Amount: {1}", z.Id, z.Amount.ToString())));
            }
        }

        protected void SendMoneyButton_Click(object sender, EventArgs e)
        {
            var t = new Transactions();
            try
            {
                t.Send(DestTB.Text, Convert.ToDouble(AmountTB.Text), new Dictionary<string, string> { { "destinationType", destDD.SelectedIndex == 0 ? "Dwolla" : "Email" } }, "raopmI6N9UIq87uWqhXB5v7xIgi49EH3K3qSFwtoZ/CzcBCN+l");
            }
            catch (ApiException a)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + a.Message + "')", true);
            }
        }
    }
}
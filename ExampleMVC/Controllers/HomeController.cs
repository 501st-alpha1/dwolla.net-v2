using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DwollaV2;
using DwollaV2.SerializableTypes;

namespace ExampleMVC.Controllers
{
    public class HomeController : Controller
    {

        public DwollaV2.OAuth o = new OAuth();
        public DwollaV2.Accounts a = new Accounts();
        public DwollaV2.Transactions t = new Transactions();

        public static OAuthResponse creds = null;

        //
        // GET: /Home/

        public ActionResult Index()
        {
            creds = null;
            return View();
        }

        /// <summary>
        /// Handles OAuth handshake
        /// </summary>

        public ActionResult OAuth()
        {
            if (Request.QueryString["code"] != null)
            {
                creds = o.Get(Request.QueryString["code"], "http://localhost:53902/Home/OAuth");
                return RedirectToAction("Send");
            }
            return Redirect(o.GenAuthUrl("http://localhost:53902/Home/OAuth").ToString());
        }

        public ActionResult Send()
        {
            if (Request.Form["dwolla_id_type"] != null)
            {
                int? sent = null;
                try
                {
                    sent = t.Send(Request.Form["dwolla_dest"], Convert.ToDouble(Request.Form["dwolla_amount"]),
                                  new Dictionary<string, string> { { "destinationType", Request.Form["dwolla_id_type"] } }, creds.access_token, Convert.ToInt16(Request.Form["dwolla_pin"]));
                }
                catch (DwollaV2.ApiException)
                {
                    ViewData["api_error"] = true;
                }
                ViewData["resulting_id"] = sent;
                ViewData["name"] = a.Full(creds.access_token).Name;
                return View("SendSuccessful");
            }

            if (creds == null) return RedirectToAction("Index");
            else
            {
                ViewData["name"] = a.Full(creds.access_token).Name;
                return View("Send");
            }
        }
    }
}

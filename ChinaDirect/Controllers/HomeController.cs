using ChinaDirect.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ChinaDirect.Controllers
{
    public class HomeController : Controller
    {
         [AllowAnonymous]
        public ActionResult Index()
        {
            double exchangeRate = GetConversionRate();
            HomeViewModel model = new HomeViewModel(exchangeRate);
            return View(model);
        }
         [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
         [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        private double GetConversionRate()
        {
            string txt;
            string url = "http://finance.yahoo.com/d/quotes.csv?e=.csv&f=sl1d1t1&s=USDCNY=X";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                txt = reader.ReadToEnd();
            }

            string[] arr = txt.Split(',');

            return double.Parse(arr[1]);
        }
    }
}
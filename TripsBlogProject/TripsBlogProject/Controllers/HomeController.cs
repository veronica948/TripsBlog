using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace TripsBlogProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ServiceReference1.MagicData mg = new ServiceReference1.MagicData { Phrase = User.Identity.Name, Number = 14 };
            ServiceReference1.Service1Client proxy = new ServiceReference1.Service1Client();
            ServiceReference1.MagicData newMg = proxy.UpdateNumber(mg);
            ViewBag.MagicNumber = "Random number: " + newMg.Number;
            ViewBag.Phrase = proxy.GetMessage(User.Identity.Name);
            ServiceReference2.GlobalWeatherSoapClient proxy2 = new ServiceReference2.GlobalWeatherSoapClient();
            var answer = proxy2.GetWeather("Minsk", "Belarus");
            ViewBag.Weather = parseXml(answer);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            

            return View();
        }
        public ActionResult Administration()
        {
            ViewBag.Message = "Admin and moderator page";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        private string parseXml(string origin)
        {
            StringBuilder output = new StringBuilder();

            String xmlString = origin;

            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                reader.ReadToFollowing("Temperature");
                reader.MoveToFirstAttribute();
                string temperature = reader.ReadElementContentAsString();
                output.AppendLine("The temperature: " + temperature);
            }

            return output.ToString();
        }
    }
}
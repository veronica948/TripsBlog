using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            return View();
        }

        public ActionResult Weather()
        {
            ViewBag.Message = "Your application description page.";

            //external soap service

            ServiceReference2.GlobalWeatherSoapClient proxy2 = new ServiceReference2.GlobalWeatherSoapClient();
            var answer = proxy2.GetWeather("Minsk", "Belarus");
            ViewBag.Weather = parseXml(answer);

            //external rest service
            string url = "http://services.groupkt.com/country/get/iso2code/";
            string code = "BY";
            var request = (HttpWebRequest)WebRequest.Create(url + code);
            request.Method = WebRequestMethods.Http.Get;
            HttpWebResponse responce = request.GetResponse() as HttpWebResponse;
            if (responce.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpException();
            }
            Stream respStream = responce.GetResponseStream();
            StreamReader reader = new StreamReader(respStream);
            string json = reader.ReadToEnd();

            JSONResponse resp = JsonConvert.DeserializeObject<JSONResponse>(json);
            ViewBag.Country = code + " - " + resp.RestResponse.Result.Name;
            return View();
        }
        public ActionResult Administration()
        {
            ViewBag.Message = "Admin and moderator page";

            return View();
        }
        [Authorize]
        public ActionResult Welcome()
        {
            ViewBag.Message = "Your welcome page.";
            //internal service
            
             ServiceReference1.MagicData mg = new ServiceReference1.MagicData { Phrase = User.Identity.Name, Number = 14 };
             ServiceReference1.Service1Client proxy = new ServiceReference1.Service1Client();
             ServiceReference1.MagicData newMg = proxy.UpdateNumber(mg);
             ViewBag.MagicNumber = "Random number: " + newMg.Number;
             ViewBag.Phrase = proxy.GetMessage(User.Identity.Name);

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

    public class JSONResponse
    {
        public RestResponse RestResponse { get; set; }
    }
    public class RestResponse {
        /*"RestResponse" : {
    "messages" : [ "More webservices are available at http://www.groupkt.com/post/f2129b88/services.htm", "Country found matching code [BY]." ],
    "result" : {
      "name" : "Belarus",
      "alpha2_code" : "BY",
      "alpha3_code" : "BLR"
    }*/

        public string[] Messages{get;set;}
        public Result Result{get;set;}
  }
    public class Result
    {
        public string Name { get; set; }
        public string Alpha2_code {get;set;}
        public string Alpha3_code {get;set;}
    }

}
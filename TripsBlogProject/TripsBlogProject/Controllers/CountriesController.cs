using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TripsBlogProject.Models;

namespace TripsBlogProject.Controllers
{
    public class CountriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string URL = "http://localhost:49485/Service1.svc/countries";

        // GET: Countries
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Index()
        {
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Posts(string name)
        {
            var posts = db.Posts.Where(u => u.Country == name).Include(p => p.Author).Distinct().ToList();
            return View(posts);
        }

        // GET: Countries/all
        [HttpGet]
        public ActionResult All()
        {
            return View("AllCountries");
        }

        // GET: Countries/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var request = (HttpWebRequest)WebRequest.Create(URL + "/" + id);
            request.Method = WebRequestMethods.Http.Get;
            HttpWebResponse responce = request.GetResponse() as HttpWebResponse;
            if (responce.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpException();
            }
            Stream respStream = responce.GetResponseStream();
            StreamReader reader = new StreamReader(respStream);
            string json = reader.ReadToEnd();
            if (json == null)
            {
                return HttpNotFound();
            }
            Country country = JsonConvert.DeserializeObject<Country>(json);
            
            return View(country);
        }


        // GET: Countries/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CountryId,Name,Description")] Country country)
        {
            if (ModelState.IsValid)
            {
                var request = (HttpWebRequest)WebRequest.Create(URL);
                request.Method = WebRequestMethods.Http.Post;
                request.ContentType = "application/json; charset=utf-8";
                string json = JsonConvert.SerializeObject(country);
                using (var requestStream = request.GetRequestStream())
                {
                    var bytes = Encoding.UTF8.GetBytes(json);
                    requestStream.Write(bytes, 0, bytes.Length);
                }
                //request.
                HttpWebResponse responce = request.GetResponse() as HttpWebResponse;
                if (responce.StatusCode != HttpStatusCode.Created)
                {
                    throw new HttpException();
                }
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        // GET: Countries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var request = (HttpWebRequest)WebRequest.Create(URL + "/" + id);
            request.Method = WebRequestMethods.Http.Get;
            HttpWebResponse responce = request.GetResponse() as HttpWebResponse;
            if (responce.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpException();
            }
            Stream respStream = responce.GetResponseStream();
            StreamReader reader = new StreamReader(respStream);
            string json = reader.ReadToEnd();
            if (json == null)
            {
                return HttpNotFound();
            }
            Country country = JsonConvert.DeserializeObject<Country>(json);
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CountryId,Name,Description")] Country country)
        {
            if (ModelState.IsValid)
            {
                var request = (HttpWebRequest)WebRequest.Create(URL);
                request.Method = WebRequestMethods.Http.Put;
                request.ContentType = "application/json; charset=utf-8";
                string json = JsonConvert.SerializeObject(country);
                using (var requestStream = request.GetRequestStream())
                {
                    var bytes = Encoding.UTF8.GetBytes(json);
                    requestStream.Write(bytes, 0, bytes.Length);
                }
                //request.
                HttpWebResponse responce = request.GetResponse() as HttpWebResponse;
                if (responce.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpException();
                }
                return RedirectToAction("Index");
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var request = (HttpWebRequest)WebRequest.Create(URL + "/" + id);
            request.Method = WebRequestMethods.Http.Get;
            HttpWebResponse responce = request.GetResponse() as HttpWebResponse;
            if (responce.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpException();
            }
            Stream respStream = responce.GetResponseStream();
            StreamReader reader = new StreamReader(respStream);
            string json = reader.ReadToEnd();
            if (json == null)
            {
                return HttpNotFound();
            }
            Country country = JsonConvert.DeserializeObject<Country>(json);
            return View(country);
        }

        // POST: Countries/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var request = (HttpWebRequest)WebRequest.Create(URL + "/" + id);
            request.Method = "DELETE";
            HttpWebResponse responce = request.GetResponse() as HttpWebResponse;
            if (responce.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpException();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

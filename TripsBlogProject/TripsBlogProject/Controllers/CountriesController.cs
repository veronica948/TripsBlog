using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TripsBlogProject.Models;

namespace TripsBlogProject.Controllers
{
    public class CountriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Countries
        public ActionResult Index()
        {
       
            return View(db.Countries.ToList());
        }
        // GET: Countries/all
        public ActionResult All()
        {
            return View("AllCountries",db.Countries.ToList());
        }
        // GET: Countries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // GET: Countries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CountryId,Name,Description")] Country country)
        {
            if (ModelState.IsValid)
            {
                db.Countries.Add(country);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(country);
        }

        //Get Countries/UploadImage/1
        [HttpGet]
        public ActionResult UploadImage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        //Post   Countries/UploadImage/1
        [HttpPost]
        ActionResult UploadImage(/*[Bind(Include = "CountryId")] Country country*/ int id, HttpPostedFileBase file)
        {
            string newFileName = "";
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                string format = file.ContentType;
                Console.Out.Write(format);
                newFileName = Guid.NewGuid().ToString(); //global identificator
                var path = Path.Combine(Server.MapPath("~/Images/"), newFileName); //~ - The root
                file.SaveAs(path);
                Country country = db.Countries.Find(id);
                country.ImageUrl = new CountryImage { ImageSrc = newFileName };
                db.Entry(country).State = EntityState.Modified;
                db.SaveChanges();
                return View(country);/*RedirectToAction("Details/" + country.CountryId);*/
            }
            else
            {
                Country country = db.Countries.Find(1);
                return View(country);
                //return RedirectToAction("UploadImage");
            }            
        }

        // GET: Countries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CountryId,Name,Description")] Country country)
        {
            if (ModelState.IsValid)
            {
                db.Entry(country).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Country country = db.Countries.Find(id);
            db.Countries.Remove(country);
            db.SaveChanges();
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

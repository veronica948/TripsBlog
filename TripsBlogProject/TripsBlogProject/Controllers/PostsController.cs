using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p=>p.Author).Include(c=>c.Country).ToList();
            //foreach (Post post in posts)
            //{
            //    post.Author = db.Users.Find();
            //}
            return View(posts);
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Include(p=>p.Author).Include(c=>c.Country).First(p=>p.PostId == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }


        private IEnumerable<SelectListItem> GetCountriesList()
        {
            List<Country> countries = db.Countries.ToList();
            var countriesList = countries.Select(x =>
                                new SelectListItem
                                {
                                    Value = x.CountryId.ToString(),
                                    Text = x.Name
                                });

            return new SelectList(countriesList, "Value", "Text");
        }



        // GET: Posts/Create
        public ActionResult Create()
        {
            var model = new CreatePostModel
            {
                AllCountries = new CountriesListViewModel
            {
                Countries = GetCountriesList()
            }
            };

             
            return View(model);
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostId,Title,AllCountries, Place, Description")] CreatePostModel CreatePost, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                Country choosenCountry = db.Countries.Find(CreatePost.AllCountries.SelectedCountryId);
                Post post = new Post { Title = CreatePost.Title, Country = choosenCountry, Place = CreatePost.Place, Description = CreatePost.Description };
                var currentUser = db.Users.First(u => u.UserName == User.Identity.Name);
                post.Author = currentUser;
                string newFileName = "";
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    string format = file.ContentType;
                    Console.Out.Write(format);
                    newFileName = Guid.NewGuid().ToString() + fileName; //global identificator
                    var path = Path.Combine(Server.MapPath("~/Images/"), newFileName); //~ - The root
                    file.SaveAs(path);
                    post.Image = newFileName;
                }
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(CreatePost);

        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostId,Title,Place,Description,Image")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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

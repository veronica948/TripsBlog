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
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;

namespace TripsBlogProject.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //cloud
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
    ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);


        // GET: Posts
        [HttpGet]
        public ActionResult Index()
        {
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("posts");
            TableQuery<Post> query = new TableQuery<Post>();
            var posts = table.ExecuteQuery(query);
            
            //var posts = db.Posts.Include(p=>p.Author).Include(c=>c.Country).ToList();
            return View(posts);
        }

        [Authorize]
        [HttpGet]
        public ActionResult MyPosts()
        {         
            ApplicationUser user = db.Users.First(p=>p.UserName == User.Identity.Name);
            string id = user.Id;

            //cloud
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("posts");
            TableQuery<Post> query = new TableQuery<Post>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, id));//.Include(p => p.Author).Include(c => c.Country);
            var posts = table.ExecuteQuery(query);

            //var posts = db.Posts.Where(u => u.Author.Id == id).Include(p => p.Author).Include(c => c.Country).ToList();
            return View(posts);
        }

        // GET: Posts/Details/5
        [HttpGet]
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
        [Authorize]
        [HttpGet]
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
        [HttpPost]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostId,Title,AllCountries, Place, Description")] CreatePostModel CreatePost, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                Country choosenCountry = db.Countries.Find(CreatePost.AllCountries.SelectedCountryId);
                Post post = new Post { Title = CreatePost.Title, Country = choosenCountry, Place = CreatePost.Place, Description = CreatePost.Description };
                var userName = User.Identity.Name;
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


                //cloud
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                CloudTable table = tableClient.GetTableReference("posts");
                TableOperation insertOperation = TableOperation.Insert(post);
                table.Execute(insertOperation);

                //local db
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(CreatePost);

        }


        // GET: Posts/Delete/5
        [Authorize]
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
        [Authorize]
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

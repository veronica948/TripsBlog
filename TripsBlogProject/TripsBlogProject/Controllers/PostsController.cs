using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        [HttpGet]
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p=>p.Author).ToList();
            return View(posts);
        }

        [Authorize]
        [HttpGet]
        public ActionResult MyPosts()
        {         
            ApplicationUser user = db.Users.First(p=>p.UserName == User.Identity.Name);
            string id = user.Id;
            var posts = db.Posts.Where(u => u.Author.Id == id).Include(p => p.Author).ToList();
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
            Post post = db.Posts.Include(p=>p.Author).First(p=>p.PostId == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreatePostModel();                                   
            return View(model);
        }

        // POST: Posts/Create
        [HttpPost]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostId,Title,Country, Place, Description")] CreatePostModel CreatePost, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post { Title = CreatePost.Title, Country = CreatePost.Country, Place = CreatePost.Place, Description = CreatePost.Description };
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
                    
                    post.Image = newFileName;

                    //cloud storage
                    try
                    {
                        //change AccountName and AccountKey
                        string connectionString = "DefaultEndpointsProtocol=https;AccountName=tripblog2016;AccountKey=oMyvt61c71AoPwO+1CfBC3p9iKPnCrP3ahUwC2ZMcB/rCBGBJw0f/NXXyVjwAjzw70dFwPhNRBM04VslKLjdmQ==";

                        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString
                           );
                        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                        CloudBlobContainer container = blobClient.GetContainerReference("images");
                        container.SetPermissions(
                            new BlobContainerPermissions
                            {
                                PublicAccess =
                                    BlobContainerPublicAccessType.Blob
                            });

                        CloudBlockBlob blockBlob = container.GetBlockBlobReference(newFileName);
                        blockBlob.UploadFromStream(file.InputStream);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Cannot connect to storage");
                    }
                    //local
                    file.SaveAs(path);

                }
               
                db.Posts.Add(post);
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
            try
            {
                if (post.Image != null)
                {
                    var path = Path.Combine(Server.MapPath("~/Images/"), post.Image);
                    System.IO.File.Delete(path);

                    //change AccountName and AccountKey
                    string connectionString = "DefaultEndpointsProtocol=https;AccountName=tripblog2016;AccountKey=oMyvt61c71AoPwO+1CfBC3p9iKPnCrP3ahUwC2ZMcB/rCBGBJw0f/NXXyVjwAjzw70dFwPhNRBM04VslKLjdmQ==";

                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString
                       );
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference("images");

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(post.Image);
                    blockBlob.Delete();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot connect to storage");
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

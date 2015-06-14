using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Posts.ToList());
        }

        // GET: Posts/Details/5
        [Authorize]
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "PostId,Title,Description")] CreatePostViewModel CreatePost)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post { Title = CreatePost.Title, Description = CreatePost.Description };
                var currentUser = db.Users.First(u => u.UserName == User.Identity.Name);
                post.Author = currentUser;
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(CreatePost);
        }

        // GET: Posts/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            EditPostViewModel editPost = new EditPostViewModel { PostId = post.PostId, Title = post.Title, Description = post.Description };
            if (post == null)
            {
                return HttpNotFound();
            }
            editPost.AvailableTags = db.Tags.ToList<Tag>();
            return View(editPost);
        }


        //
        [HttpPost]
        ActionResult UploadImage(int id, HttpPostedFileBase file)
        {
            string newFileName = "";
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                string format = file.ContentType;
                Console.Out.Write(format);
                newFileName = Guid.NewGuid().ToString(); //global identificator
                var path = Path.Combine(Server.MapPath ("~/Images/"), newFileName); //~ - The root
                file.SaveAs(path);
            }
            db.SiteImages.Add(new SiteImage
            {
                ImageSrc = newFileName
            });
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "PostId,Title,Description")] EditPostViewModel editPost)
        {

            if (ModelState.IsValid)
            {
                var post = db.Posts.Include(p=>p.Author).First(p=>p.PostId == editPost.PostId);
                post.Title = editPost.Title;
                post.Description = editPost.Description;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editPost);
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
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

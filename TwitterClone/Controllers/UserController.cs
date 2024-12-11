using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TwitterClone.DataAccess;
using TwitterClone.Models;

namespace TwitterClone.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private TwitterCloneDbContext db = new TwitterCloneDbContext();

        // GET: User
        public ActionResult Index()
        {
            return View(db.User.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User User = db.User.Find(id);
            if (User == null)
            {
                return HttpNotFound();
            }
            return View(User);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Password,Fullname,Email,Joined,Active")] User User)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(User);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(User);
        }

        // GET: User/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User User = db.User.Find(id);
            if (User == null)
            {
                return HttpNotFound();
            }
            return View(User);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Password,Fullname,Email,Joined,Active")] User User)
        {
            if (ModelState.IsValid)
            {
                db.Entry(User).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(User);
        }

        // GET: User/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User User = db.User.Find(id);
            if (User == null)
            {
                return HttpNotFound();
            }
            return View(User);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User User = db.User.Find(id);
            db.User.Remove(User);
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

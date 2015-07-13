using ChinaDirect.DAL;
using ChinaDirect.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ChinaDirect.Controllers
{
    public class UserInfoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserInfoes
        public ActionResult Index()
        {
            var userInfo = db.UserInfo.Find(Session["userID"]);
            //return View(userInfo.ToList());
            return View(userInfo);
        }

        // GET: UserInfoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.UserInfo.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // GET: UserInfoes/Create
        public ActionResult Create(string userID)
        {
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email");
            return View();
        }

        // POST: UserInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Name,Address,City,State,Zip,Country,Email,Mobile,Wechat")] UserInfo userInfo)
        {
            var userID = Session["userID"];
            if (userID == null)
            {
                return RedirectToAction("LogOff", "AccountController");
            }

            userInfo.UserId = (string)userID;
            if (ModelState.IsValid)
            {
                db.UserInfo.Add(userInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", userInfo.UserId);
            return View(userInfo);
        }

        // GET: UserInfoes/Edit/5
        public ActionResult Edit()
        {
            var id = Session["userID"];
            if (id == null)
            {
                return RedirectToAction("LogOff", "AccountController");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.UserInfo.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // POST: UserInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Name,Address,City,State,Zip,Country,Email,Mobile,Wechat")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", userInfo.UserId);
            return View(userInfo);
        }

        // GET: UserInfoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.UserInfo.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // POST: UserInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserInfo userInfo = db.UserInfo.Find(id);
            db.UserInfo.Remove(userInfo);
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
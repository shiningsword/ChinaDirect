using ChinaDirect.DAL;
using ChinaDirect.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ChinaDirect.Controllers
{
    public class RequestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Requests
        public ActionResult Index()
        {
            var id = Session["userID"];
            if (id == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.currentUser = (string)id;
            ViewBag.myRequests = false;
            //var requests = db.Requests.Where(r => r.UserId ==);
            RequestViewModel model = new RequestViewModel(db.Requests.ToList());
            return View(model);
        }

        // GET: Requests
        public ActionResult MyRequests()
        {
            var id = Session["userID"];
            if (id == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.currentUser = (string)id;
            ViewBag.myRequests = true;

            RequestViewModel model = new RequestViewModel(db.UserInfo.Find(id).requests);

            return View("Index", model);
        }

        // GET: Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RequestID,Message,AmountMin,AmountMax,Currency,ExpirationDate,ExchangeMode,NeedEscrow, CloseToZipcode, CompanyDomain")] Request request)
        {
            if (ModelState.IsValid)
            {
                var userId = Session["userID"];
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                request.UserId = (string)userId;
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FilterResult(
            FormCollection collection)
        //         [Bind(Include = "Query.Currency, Query.AmountMin,Query.AmountMax, Query.CloseToZipcode, Query.CompanyDomain")] Request request)
        {
            Request request = new Request();
            request.AmountMax = double.Parse(collection["Query.AmountMax"]);
            request.AmountMin = double.Parse(collection["Query.AmountMin"]);
            request.CloseToZipcode = int.Parse(collection["Query.CloseToZipcode"]);
            request.Currency = (CurrencyType)Enum.Parse(typeof(CurrencyType), collection["Query.Currency"]);
            request.CompanyDomain = collection["Query.CompanyDomain"];

            if (ModelState.IsValid)
            {
                var userId = Session["userID"];
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                var query = db.Requests.Where(r =>
                    r.Currency == request.Currency &&
                    !(r.AmountMax < request.AmountMin || r.AmountMin > request.AmountMax));
                if (request.CompanyDomain != null)
                {
                    query = query.Where(r => r.UserId.Contains(request.CompanyDomain));
                }
                if (request.CloseToZipcode != 0)
                {
                    query = query.Where(r => Math.Abs(r.User.Zip - request.CloseToZipcode) < 100);
                }

                ViewBag.myRequests = false;
                ViewBag.currentUser = (string)userId;
                RequestViewModel model = new RequestViewModel(query.ToList(), request);
                return View("Index", model);
            }

            return View(request);
        }

        // GET: Requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", request.UserId);
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequestID,Message,AmountMin,AmountMax,Currency, ExpirationDate,ExchangeMode,NeedEscrow,UserId")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", request.UserId);
            return View(request);
        }

        // GET: Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Requests/Delete/5
        public ActionResult Close(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Create", "Transactions", request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
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
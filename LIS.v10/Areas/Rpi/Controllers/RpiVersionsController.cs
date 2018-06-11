using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LIS.v10.Areas.Rpi.Models;

namespace LIS.v10.Areas.Rpi.Controllers
{
    public class RpiVersionsController : Controller
    {
        private RpiDBContainer1 db = new RpiDBContainer1();

        // GET: Rpi/RpiVersions
        public ActionResult Index()
        {
            return View(db.RpiVersions.ToList());
        }

        // GET: Rpi/RpiVersions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RpiVersion rpiVersion = db.RpiVersions.Find(id);
            if (rpiVersion == null)
            {
                return HttpNotFound();
            }
            return View(rpiVersion);
        }

        // GET: Rpi/RpiVersions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rpi/RpiVersions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,VersionNo,Description")] RpiVersion rpiVersion)
        {
            if (ModelState.IsValid)
            {
                db.RpiVersions.Add(rpiVersion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rpiVersion);
        }

        // GET: Rpi/RpiVersions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RpiVersion rpiVersion = db.RpiVersions.Find(id);
            if (rpiVersion == null)
            {
                return HttpNotFound();
            }
            return View(rpiVersion);
        }

        // POST: Rpi/RpiVersions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VersionNo,Description")] RpiVersion rpiVersion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rpiVersion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rpiVersion);
        }

        // GET: Rpi/RpiVersions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RpiVersion rpiVersion = db.RpiVersions.Find(id);
            if (rpiVersion == null)
            {
                return HttpNotFound();
            }
            return View(rpiVersion);
        }

        // POST: Rpi/RpiVersions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RpiVersion rpiVersion = db.RpiVersions.Find(id);
            db.RpiVersions.Remove(rpiVersion);
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

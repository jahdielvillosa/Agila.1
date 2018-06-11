using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LIS.v10.Areas.Rpi.Models;
using Newtonsoft.Json;

namespace LIS.v10.Areas.Rpi.Controllers
{
    public class RpiDatalogsController : Controller
    {
        
        private RpiDBContainer1 db = new RpiDBContainer1();

        // GET: Rpi/RpiDatalogs
        public ActionResult Index(int id)
        {
            var rpiDatalogs = db.RpiDatalogs.Include(r => r.RpiDevice).Where(r=>r.RpiDeviceId == id);
            return View(rpiDatalogs.ToList());
        }

        // GET: Rpi/RpiDatalogs
        public ActionResult LogDetails(int id)
        {
            var rpiDatalogs = db.RpiDatalogs.Include(r => r.RpiDevice).Where(r => r.RpiDeviceId == id);
            List<LogDetailLists> loglist = new List<LogDetailLists>();
            
            foreach (var logs in rpiDatalogs)
            {

                RpiData data = JsonConvert.DeserializeObject<RpiData>(logs.DataRead);
      
                loglist.Add(new LogDetailLists()
                {
                    Id = logs.Id,
                    DateTime = logs.DtRead,
                   Temp = data.Temp,
                   Humidity = data.Humidity,
                   Light = data.Light,
                   Fan = data.Fan,
                   Water = data.Water
                });
            }


            return View(loglist);
        }

        // GET: Rpi/RpiDatalogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RpiDatalog rpiDatalog = db.RpiDatalogs.Find(id);
            if (rpiDatalog == null)
            {
                return HttpNotFound();
            }
            return View(rpiDatalog);
        }

        // GET: Rpi/RpiDatalogs/Create
        public ActionResult Create()
        {
            ViewBag.RpiDeviceId = new SelectList(db.RpiDevices, "Id", "Description");
            return View();
        }

        // POST: Rpi/RpiDatalogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DtRead,DataRead,RpiDeviceId")] RpiDatalog rpiDatalog)
        {
            if (ModelState.IsValid)
            {
                db.RpiDatalogs.Add(rpiDatalog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RpiDeviceId = new SelectList(db.RpiDevices, "Id", "Description", rpiDatalog.RpiDeviceId);
            return View(rpiDatalog);
        }

        // GET: Rpi/RpiDatalogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RpiDatalog rpiDatalog = db.RpiDatalogs.Find(id);
            if (rpiDatalog == null)
            {
                return HttpNotFound();
            }
            ViewBag.RpiDeviceId = new SelectList(db.RpiDevices, "Id", "Description", rpiDatalog.RpiDeviceId);
            return View(rpiDatalog);
        }

        // POST: Rpi/RpiDatalogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DtRead,DataRead,RpiDeviceId")] RpiDatalog rpiDatalog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rpiDatalog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RpiDeviceId = new SelectList(db.RpiDevices, "Id", "Description", rpiDatalog.RpiDeviceId);
            return View(rpiDatalog);
        }

        // GET: Rpi/RpiDatalogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RpiDatalog rpiDatalog = db.RpiDatalogs.Find(id);
            if (rpiDatalog == null)
            {
                return HttpNotFound();
            }
            return View(rpiDatalog);
        }

        // POST: Rpi/RpiDatalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RpiDatalog rpiDatalog = db.RpiDatalogs.Find(id);
            db.RpiDatalogs.Remove(rpiDatalog);
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

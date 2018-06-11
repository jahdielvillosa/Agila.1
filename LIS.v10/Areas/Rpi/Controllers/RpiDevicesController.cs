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
    public class RpiDevicesController : Controller
    {
        private RpiDBContainer1 db = new RpiDBContainer1();
        private RpiClass db1 = new RpiClass();

        // GET: Rpi/RpiDevices
        public ActionResult Index()
        {
            var rpiDevices = db.RpiDevices.Include(r => r.RpiVersion);
            
            List<DeviceDetailsLists> details = new List<DeviceDetailsLists>();
            foreach (var devices in rpiDevices)
            {
                var versionNo = db.RpiVersions.Where(r => r.Id == devices.RpiVersionId).FirstOrDefault();

                RpiDatalog logs = new RpiDatalog();
                logs.DataRead = "{\"Temp\":0,\"Humidity\":0,\"Light\":0,\"Fan\":0,\"Water\":0}";
                logs = db.RpiDatalogs.Where(r => r.RpiDeviceId == devices.Id).OrderByDescending(r=>r.DtRead).FirstOrDefault() ;

                RpiData data = new RpiData();

                if (logs != null)
                {
                     data = JsonConvert.DeserializeObject<RpiData>(logs.DataRead);
                }
                else
                {
                    string empty = "{\"Temp\":0,\"Humidity\":0,\"Light\":0,\"Fan\":0,\"Water\":0}";
                    data = JsonConvert.DeserializeObject<RpiData>(empty);
                }

                details.Add(new DeviceDetailsLists() {
                    Id = devices.Id,
                    Description = devices.Description,
                    Version = versionNo.VersionNo,
                    Temp = double.Parse(data.Temp),
                    Humidity = double.Parse(data.Humidity),
                    Light = int.Parse(data.Light),
                    Fan = int.Parse(data.Fan),
                    Water = int.Parse(data.Water)
                });
            }

            return View(details);
        }

        // GET: Rpi/RpiDevices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RpiDevice rpiDevice = db.RpiDevices.Find(id);
            if (rpiDevice == null)
            {
                return HttpNotFound();
            }


            ViewBag.getComponentDetailLists = db1.getComponents((int)id);
            return View(rpiDevice);
        }

        // GET: Rpi/RpiDevices/Create
        public ActionResult Create()
        {
            ViewBag.RpiVersionId = new SelectList(db.RpiVersions, "Id", "VersionNo");
            return View();
        }

        // POST: Rpi/RpiDevices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,RpiVersionId")] RpiDevice rpiDevice)
        {
            if (ModelState.IsValid)
            {
                db.RpiDevices.Add(rpiDevice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RpiVersionId = new SelectList(db.RpiVersions, "Id", "VersionNo", rpiDevice.RpiVersionId);
            return View(rpiDevice);
        }

        // GET: Rpi/RpiDevices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RpiDevice rpiDevice = db.RpiDevices.Find(id);
            if (rpiDevice == null)
            {
                return HttpNotFound();
            }
            ViewBag.RpiVersionId = new SelectList(db.RpiVersions, "Id", "VersionNo", rpiDevice.RpiVersionId);
            return View(rpiDevice);
        }

        // POST: Rpi/RpiDevices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,RpiVersionId")] RpiDevice rpiDevice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rpiDevice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RpiVersionId = new SelectList(db.RpiVersions, "Id", "VersionNo", rpiDevice.RpiVersionId);
            return View(rpiDevice);
        }

        // GET: Rpi/RpiDevices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RpiDevice rpiDevice = db.RpiDevices.Find(id);
            if (rpiDevice == null)
            {
                return HttpNotFound();
            }
            return View(rpiDevice);
        }

        // POST: Rpi/RpiDevices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RpiDevice rpiDevice = db.RpiDevices.Find(id);
            db.RpiDevices.Remove(rpiDevice);
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

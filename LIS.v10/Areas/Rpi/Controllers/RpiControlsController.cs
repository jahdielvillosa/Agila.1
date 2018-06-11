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
    public class RpiControlsController : Controller
    {
        private RpiDBContainer1 db = new RpiDBContainer1();

        // GET: Rpi/RpiControls
        public ActionResult Index(int? id)
        {

            // var rpiControls = db.RpiControls.Include(r => r.RpiDevice).Where(r => r.RpiDeviceId == id);
            // return View(rpiControls.Where(r=>r.RpiDeviceId == id).ToList());
            //if (rpiControls != null)
            //{

            //List<DeviceSettingsDetails> settingDetails = new List<DeviceSettingsDetails>();

            //    foreach (var controls in rpiControls)
            //    {
            //        RpiData data = JsonConvert.DeserializeObject<RpiData>(controls.Data);

            //        settingDetails.Add(new DeviceSettingsDetails()
            //        {
            //            Id = controls.Id,
            //            Description = controls.RpiDevice.Description,
            //            DateSchedule = DateTime.Parse(controls.DtSchedule),
            //            Temp = double.Parse(data.Temp),
            //            Humidity = double.Parse(data.Humidity),
            //            Light = int.Parse(data.Light),
            //            Fan = int.Parse(data.Fan),
            //            Water = int.Parse(data.Water)
            //        });
            //    }
            //    return View(settingDetails);
            //}

            //return View();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var rpiControls = db.RpiControls.Include(r => r.RpiDevice).Where(r=>r.RpiDeviceId == id);

            List<DeviceSettingsDetails> details = new List<DeviceSettingsDetails>();
            foreach (var controls in rpiControls)
            {
                // var logs = db.RpiDatalogs.Where(r => r.RpiDeviceId == devices.Id).OrderByDescending(r => r.DtRead).FirstOrDefault();

                DeviceSettingsDetails data = JsonConvert.DeserializeObject<DeviceSettingsDetails>(controls.Data);
                DeviceSettingsDetails settings = new DeviceSettingsDetails();

                settings.Id = controls.Id;
                settings.DateSchedule = DateTime.Parse(controls.DtSchedule);
                settings.TempMin = data.TempMin;
                settings.TempMax = data.TempMax;
                settings.Socket01 = data.Socket01;
                settings.Socket02 = data.Socket02;
                settings.Light = data.Light;
                settings.rpiDeviceId = controls.RpiDeviceId;

                details.Add(settings);
            }

            return View(details);
        }

        // GET: Rpi/RpiControls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RpiControl rpiControl = db.RpiControls.Find(id);
            if (rpiControl == null)
            {
                return HttpNotFound();
            }
            return View(rpiControl);
        }

        // GET: Rpi/RpiControls/Create
        public ActionResult Create()
        {
            ViewBag.RpiDeviceId = new SelectList(db.RpiDevices, "Id", "Description");
            return View();
        }

        // POST: Rpi/RpiControls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]

        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,DtSchedule,Data,RpiDeviceId")] RpiControl rpiControl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.RpiControls.Add(rpiControl);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
            
        //    ViewBag.RpiDeviceId = new SelectList(db.RpiDevices, "Id", "Description", rpiControl.RpiDeviceId);
        //    return View(rpiControl);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DateSchedule,TempMin,TempMax,Light,Socket01,Socket02,RpiDeviceId")] DeviceSettingsDetails control)
        {
            RpiControl rpiControl = new RpiControl();
            if (ModelState.IsValid)
            {
                rpiControl.DtSchedule = control.DateSchedule.ToString();
                rpiControl.RpiDeviceId = control.rpiDeviceId;
                rpiControl.Data = "{\"TempMin\":"+ control.TempMin + ",\"TempMax\":" + control.TempMax + ",\"Light\":" + control.Light + ",\"Socket01\":" + control.Socket01 + ",\"Socket02\":" + control.Socket02 + "}";

                db.RpiControls.Add(rpiControl);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = control.rpiDeviceId});
            }

            ViewBag.RpiDeviceId = new SelectList(db.RpiDevices, "Id", "Description", rpiControl.RpiDeviceId);
            return View(rpiControl);
        }

        // GET: Rpi/RpiControls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RpiControl rpiControl = db.RpiControls.Find(id);
            if (rpiControl == null)
            {
                return HttpNotFound();
            }
            ViewBag.RpiDeviceId = new SelectList(db.RpiDevices, "Id", "Description", rpiControl.RpiDeviceId);
            return View(rpiControl);
        }

        // POST: Rpi/RpiControls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DtSchedule,Data,RpiDeviceId")] RpiControl rpiControl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rpiControl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RpiDeviceId = new SelectList(db.RpiDevices, "Id", "Description", rpiControl.RpiDeviceId);
            return View(rpiControl);
        }

        // GET: Rpi/RpiControls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RpiControl rpiControl = db.RpiControls.Find(id);
            if (rpiControl == null)
            {
                return HttpNotFound();
            }
            return View(rpiControl);
        }

        // POST: Rpi/RpiControls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RpiControl rpiControl = db.RpiControls.Find(id);
            db.RpiControls.Remove(rpiControl);
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

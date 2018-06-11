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
    public class RpiComponentsController : Controller
    {
        private RpiDBContainer1 db = new RpiDBContainer1();

        // GET: Rpi/RpiComponents
        public ActionResult Index()
        {
            
            var rpiComponents = db.RpiComponents.Include(r => r.RpiDataType);
            return View(rpiComponents.ToList());
        }

        public ActionResult ViewByVersion(int id)
        {
        //    var versionMap = rpidb.RpiVersionMaps.Where(m => m.RpiVersionId == 1).ToList();

        //    DataTable Dt = new DataTable("Table");

        //    Dt.Columns.Add("Id", typeof(int));
        //    Dt.Columns.Add("VersionId", typeof(int));
        //    Dt.Columns.Add("NameMap", typeof(string));
        //    Dt.Columns.Add("ComponentName", typeof(string));
        //    Dt.Columns.Add("PinNo", typeof(int));

        //    //get details of each failed items from recipientId
        //    foreach (var map in versionMap)
        //    {
        //        var compDetails = rpidb.RpiComponents.Where(c => c.Id == map.RpiComponentId).FirstOrDefault();

        //        int Id = compDetails.Id; //component id  
        //        int versionId = map.RpiVersionId;
        //        string nameMap = map.NameMap;
        //        string componentName = compDetails.ComponentName;
        //        string pinNo = map.PinNo;


        //        Dt.Rows.Add(Id, versionId, nameMap, componentName, pinNo);

        //    }


        //    var versionid = db.RpiDevices.Find(id).RpiVersionId;
        //    var mapLists = db.RpiVersionMaps.Where(m => m.RpiVersionId == versionid).ToList();

            //List<ComponentDetailLists> cdlist = new List<ComponentDetailLists>();

            //foreach (var map in mapLists)
            //{
            //    var compDetails = db.RpiComponents.Where(c => c.Id == map.RpiComponentId).FirstOrDefault();

            //    cdlist.Add(new ComponentDetailLists()
            //    {
            //        Id = req.Id,
            //        HisNotificationId = n.Id,
            //        RecType = n.RecType,
            //        DateRequest = req.dtRequested.ToString(),
            //        DateSchedule = req.dtSchedule.ToString(),
            //        DateSent = req.dtPerformed.ToString(),
            //        Recipient = RecipientsArray,
            //        Message = n.Message,
            //        Status = statusType
            //    });
            //}

            return View();
        }

        // GET: Rpi/RpiComponents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RpiComponent rpiComponent = db.RpiComponents.Find(id);
            if (rpiComponent == null)
            {
                return HttpNotFound();
            }
            return View(rpiComponent);
        }

        // GET: Rpi/RpiComponents/Create
        public ActionResult Create()
        {
            ViewBag.RpiDataTypeId = new SelectList(db.RpiDataTypes, "Id", "Description");
            return View();
        }

        // POST: Rpi/RpiComponents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ComponentName,Description,RpiDataTypeId")] RpiComponent rpiComponent)
        {
            if (ModelState.IsValid)
            {
                db.RpiComponents.Add(rpiComponent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RpiDataTypeId = new SelectList(db.RpiDataTypes, "Id", "Description", rpiComponent.RpiDataTypeId);
            return View(rpiComponent);
        }

        // GET: Rpi/RpiComponents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RpiComponent rpiComponent = db.RpiComponents.Find(id);
            if (rpiComponent == null)
            {
                return HttpNotFound();
            }
            ViewBag.RpiDataTypeId = new SelectList(db.RpiDataTypes, "Id", "Description", rpiComponent.RpiDataTypeId);
            return View(rpiComponent);
        }

        // POST: Rpi/RpiComponents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ComponentName,Description,RpiDataTypeId")] RpiComponent rpiComponent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rpiComponent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RpiDataTypeId = new SelectList(db.RpiDataTypes, "Id", "Description", rpiComponent.RpiDataTypeId);
            return View(rpiComponent);
        }

        // GET: Rpi/RpiComponents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RpiComponent rpiComponent = db.RpiComponents.Find(id);
            if (rpiComponent == null)
            {
                return HttpNotFound();
            }
            return View(rpiComponent);
        }

        // POST: Rpi/RpiComponents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RpiComponent rpiComponent = db.RpiComponents.Find(id);
            db.RpiComponents.Remove(rpiComponent);
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

﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LIS.v10.Areas.HIS10.Models;

namespace LIS.v10.Areas.HIS10.Controllers
{
    public class HisNotificationLogsController : Controller
    {
        private His10DBContainer db = new His10DBContainer();
        private Models.DBClasses db1 = new DBClasses();
        // GET: HIS10/HisNotificationLogs
        public ActionResult Index()
        {
            var hisNotificationLogs = db.HisNotificationLogs.Include(h => h.HisNotificationRecipient);
            return View(hisNotificationLogs.ToList());
        }

        // GET: HIS10/HisNotificationLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HisNotificationLog hisNotificationLog = db.HisNotificationLogs.Find(id);
            if (hisNotificationLog == null)
            {
                return HttpNotFound();
            }
            HisNotificationRecipient recipient = db.HisNotificationRecipients.Where(s => s.HisNotificationId == id).FirstOrDefault();

            ViewBag.getNotificationLogs = db.HisNotificationLogs.Where(s => s.HisNotificationRecipientId == recipient.Id).ToList();


            HisNotificationRecipient Recipients = db.HisNotificationRecipients.Where(s => s.HisNotificationId == id).FirstOrDefault();
            //ViewBag.getNotificationLogs = db.HisNotificationLogs.Where();

            ViewBag.getNotificationLogs = db.HisNotificationLogs.Where(s => s.HisNotificationRecipient.HisNotification.Id == id).First();
            
            return View(hisNotificationLog);
        }

        // GET: HIS10/HisNotificationLogs/Create
        public ActionResult Create(int? id)
        {
            ViewBag.HisNotificationId = new SelectList(db.HisNotifications, "Id", "RecType");

            int requestid = (int)id;
            ViewBag.RefId = requestid.ToString();
            HisProfileReq req = db.HisProfileReqs.Where(s => s.Id == requestid).FirstOrDefault();
            //Models.HisRequest req = db.HisRequests.Find((int)requestid);
            if (requestid != 0)
            {

                Models.HisNotification temp = new HisNotification();
                temp.DtSending = (DateTime)req.dtSchedule;
                temp.RefId = requestid;
                temp.RecType = "Client";
                temp.RefTable = "HisProfileReqs";
                temp.Message = db1.generateMessage((int)requestid);
                // temp.Message = requestid.ToString();
                return View(temp);
            }
            
                return View();
        }

        // POST: HIS10/HisNotificationLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HisNotificationId,DtSending,Status,Remarks")] HisNotificationLog hisNotificationLog)
        {
            if (ModelState.IsValid)
            {
                db.HisNotificationLogs.Add(hisNotificationLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HisNotificationId = new SelectList(db.HisNotifications, "Id", "RecType", hisNotificationLog.HisNotificationRecipientId);
            return View(hisNotificationLog);
        }

        // GET: HIS10/HisNotificationLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HisNotificationLog hisNotificationLog = db.HisNotificationLogs.Find(id);
            if (hisNotificationLog == null)
            {
                return HttpNotFound();
            }
            ViewBag.HisNotificationId = new SelectList(db.HisNotifications, "Id", "RecType", hisNotificationLog.HisNotificationRecipientId);
            return View(hisNotificationLog);
        }

        // POST: HIS10/HisNotificationLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HisNotificationId,DtSending,Status,Remarks")] HisNotificationLog hisNotificationLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hisNotificationLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HisNotificationId = new SelectList(db.HisNotifications, "Id", "RecType", hisNotificationLog.HisNotificationRecipientId);
            return View(hisNotificationLog);
        }

        // GET: HIS10/HisNotificationLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HisNotificationLog hisNotificationLog = db.HisNotificationLogs.Find(id);
            if (hisNotificationLog == null)
            {
                return HttpNotFound();
            }
            return View(hisNotificationLog);
        }

        // POST: HIS10/HisNotificationLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HisNotificationLog hisNotificationLog = db.HisNotificationLogs.Find(id);
            db.HisNotificationLogs.Remove(hisNotificationLog);
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

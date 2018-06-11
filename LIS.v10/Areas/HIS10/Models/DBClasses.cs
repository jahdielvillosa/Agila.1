using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LIS.v10.Areas.HIS10.Models
{
    public class SmsNotificationItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class SmsNotificationMsg
    {
        public int Id { get; set; }
        public string MessageBody { get; set; }
    }

    public class NotificationLog
    {
        public int Id { get; set; }
        public String HisNotificationId { get; set; }
        public string DtSending { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    }

    public class NotificationDetailsList
    {
        public int Id { get; set; }
        public string HisNotificationId { get; set; }
        public string Recipient { get; set; }
        public string Name { get; set; }
        public string DateSend { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        
    }


    public class NotifiedRequestLog
    {
        private int id1;
        private int id2;
        private DateTime? dtRequested;
        private DateTime? dtSchedule;
        private DateTime? dtPerformed;
        private string v;

        public NotifiedRequestLog(int id1, int id2, string recType, DateTime? dtRequested, DateTime? dtSchedule, DateTime? dtPerformed, string recipient, string message, string v)
        {
            this.id1 = id1;
            this.id2 = id2;
            RecType = recType;
            this.dtRequested = dtRequested;
            this.dtSchedule = dtSchedule;
            this.dtPerformed = dtPerformed;
            Recipient = recipient;
            Message = message;
            this.v = v;
        }

        public NotifiedRequestLog()
        {
        }

        public int Id { get; set; }
        public int HisNotificationId { get; set; }
        public string RecType { get; set; }
        public string DateRequest { get; set; }
        public string DateSchedule { get; set; }
        public string DateSent { get; set; }
        public string Recipient { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }



    public class DBClasses
    {
        public Models.His10DBContainer db = new His10DBContainer();

        public List<SmsNotificationItem> getList()
        {
            List<SmsNotificationItem> hisrequests = db.HisRequests
                .Select(s => new SmsNotificationItem { Id = s.Id, Description = s.Description })
                .ToList();

            return hisrequests;
        }

        public SmsNotificationMsg getAdminMessage(int id)
        {
            HisRequest request = db.HisRequests.Find(id);

            SmsNotificationMsg msg = new SmsNotificationMsg
            {
                    Id = request.Id,
                    MessageBody = request.Title
                };

            return msg;
        }

        

        public SmsNotificationMsg getClientMessage(int id)
        {
            HisRequest request = db.HisRequests.Find(id);
            SmsNotificationMsg msg = new SmsNotificationMsg
            {
                Id = request.Id,
                MessageBody = request.Title
            };

            return msg;
        }

        public int updateItemStatus(int status)
        {
            return 1;
        }


        //added by jahdiel
        public HisNotificationLog getNotificationLogs(int? id)
        {
            return db.HisNotificationLogs.Find(id);
        }


        public List<HisNotificationLog> getNotificationLogs()
        {
            return db.HisNotificationLogs.ToList();
            
        }

        public List<HisNotification> getNotifList()
        {

            var data = db.HisNotifications.Select(s => new {
                s.Id,
                s.RecType,
                s.Recipient,
                s.Message,
                s.DtSending
            });

            return db.HisNotifications.ToList();
        }

        public List<HisNotification> getAdminNotif()
        {
            return db.HisNotifications.Where(d => d.RecType == "Admin").ToList();
        }

        public List<HisNotification> getClientNotif()
        {
            return db.HisNotifications.Where(d => d.RecType == "Client").ToList();
        }

        public void insertNewLog(HisNotificationLog newNotifLog)
        {
            db.HisNotificationLogs.Add(newNotifLog);
            db.SaveChanges();
        }


        //for hisnotificationController

        //make from message template
        //---------------------------------------------
        //NOTIFICATION
        //For: <HisProfile.Name>
        //Request: <HisRequest.Title> <HisProfile.Remarks>
        //Scheduled on: <HisProfileReq.DtSchedule>
        //By: <HisPhysician.Name>
        //Assisted by: <HisIncharge.Name>
        //---------------------------------------------

        public string generateMessage(int requestId)
        {

            string messageTemplate = "";
            string Formsg = " ", Requestmsg = " ", Scheduledmsg = " ", Physicianmsg = " ", Assistedmsg = " ";

            HisProfileReq profilereq = db.HisProfileReqs.Where(s => s.Id == requestId).FirstOrDefault();

            //get profilename
            HisProfile profile = db.HisProfiles.Where(s => s.Id == profilereq.HisProfileId).FirstOrDefault();
            Formsg = profile.Name;

            //get request title and remarks
            HisRequest request = db.HisRequests.Where(s => s.Id == profilereq.HisRequestId).FirstOrDefault();

            Requestmsg = request.Title + "  " + profilereq.Remarks;

            //get schedule
            Scheduledmsg = profilereq.dtSchedule.ToString();

            //get physician from hisPhysician
            HisPhysician physician = db.HisPhysicians.Where(s => s.Id == profilereq.HisPhysicianId).FirstOrDefault();
            Physicianmsg = physician.Name;

            //get hisincharge name
            HisIncharge incharge = db.HisIncharges.Where(s => s.Id == profilereq.HisInchargeId).FirstOrDefault();
            Assistedmsg = incharge.Name;

            messageTemplate = "NOTIFICATION"   + Environment.NewLine + Environment.NewLine +
                "For: "         + Formsg       + Environment.NewLine +
                "Request: "     + Requestmsg   + Environment.NewLine +
                "Scheduled: "   + Scheduledmsg + Environment.NewLine +
                "By: "          + Physicianmsg + Environment.NewLine +
                "Assisted By: " + Assistedmsg  + Environment.NewLine;

            return messageTemplate;
        }

        public string generateContactList(int notificationId)
        {
            
            return "";
        }

        //Get list of failed notification 
        public List<HisProfileReq> getFailedNotification()
        {
            // List<HisNotification> failedList = new List<HisNotification>();
            List<HisProfileReq> failedreq = new List<HisProfileReq>();
            List<int> failedreqs = new List<int>();

            var failedlogs= db.HisNotificationLogs.Where(l => l.Status == "Failed");
           
            if (failedlogs != null)
            {
                foreach (var logs in failedlogs)
                {
                   if(db.HisNotificationLogs.Where(l=>l.HisNotificationRecipientId == logs.HisNotificationRecipientId).Count() > 1)
                    {
                        //if count is > 1
                        //check if the request have a sent status with the same id
                        int haveSent = db.HisNotificationLogs.Where(s => s.Status == "Sent" && s.HisNotificationRecipientId == logs.HisNotificationRecipientId).Count();
                       
                        //if the log have no sent message add to the list
                        if (haveSent == 0)
                        {
                            int itemid = logs.HisNotificationRecipientId;
                            failedreqs.Add(itemid);
                        }
                    }
                    else
                    {
                        //if there is only one entry. add to list
                        int itemid = logs.HisNotificationRecipientId;
                        failedreqs.Add(itemid);
                    }
                }
            //    db.HisNotificationLogs.Where(l=>l)
            }
            
            var failedRecipients = db.HisNotificationRecipients.Where(r => failedreqs.Contains(r.Id)).Select(r=>r.HisNotificationId);
            var failedList = db.HisNotifications.Where(f =>failedRecipients.Contains(f.Id)).Select(f=>f.RefId);
            var failedrequest = db.HisProfileReqs.Where(q => failedList.Contains(q.Id)).ToList();
            return failedrequest;
        }


        //Get list of failed notification 
        public DataSet getFailedNotification2()
        {
            // List<HisNotification> failedList = new List<HisNotification>();
            List<HisProfileReq> failedreq = new List<HisProfileReq>();
            List<int> failedreqs = new List<int>();

            var failedlogs = db.HisNotificationLogs.Where(l => l.Status == "Failed");

            if (failedlogs != null)
            {
                foreach (var logs in failedlogs)
                {
                    if (db.HisNotificationLogs.Where(l => l.HisNotificationRecipientId == logs.HisNotificationRecipientId).Count() > 1)
                    {
                        //if count is > 1
                        //check if the request have a sent status with the same id
                        int haveSent = db.HisNotificationLogs.Where(s => s.Status == "Sent" && s.HisNotificationRecipientId == logs.HisNotificationRecipientId).Count();

                        //if the log have no sent message add to the list
                        if (haveSent == 0)
                        {
                            int itemid = logs.HisNotificationRecipientId;
                            failedreqs.Add(itemid);
                        }
                    }
                    else
                    {
                        //if there is only one entry. add to list
                        int itemid = logs.HisNotificationRecipientId;
                        failedreqs.Add(itemid);
                    }
                }
                //    db.HisNotificationLogs.Where(l=>l)
            }

            var failedRecipients = db.HisNotificationRecipients.Where(r => failedreqs.Contains(r.Id)).Select(r => r.HisNotificationId);
            var failedList = db.HisNotifications.Where(f => failedRecipients.Contains(f.Id)).Select(f => f.RefId);
            var failedrequest = db.HisProfileReqs.Where(q => failedList.Contains(q.Id)).ToList();

            // List<NotifiedRequestLog> faileddetails = new List<NotifiedRequestLog>();

            //to list of failed items to dataset
            DataTable Dt = new DataTable("Table");

            Dt.Columns.Add("Id", typeof(int));
            Dt.Columns.Add("NotificationId", typeof(int));
            Dt.Columns.Add("ContactInfo", typeof(string));
            Dt.Columns.Add("Rectype", typeof(string));
            Dt.Columns.Add("Message", typeof(string));
            Dt.Columns.Add("DtSending", typeof(string));
            Dt.Columns.Add("RefId", typeof(int));

            //get details of each failed items from recipientId
            foreach (var recipientId in failedreqs)
            {
                string fRecipientNum = db.HisNotificationRecipients.Where(r => r.Id == recipientId).Select(r => r.ContactInfo).FirstOrDefault();
                string fDtSend = db.HisNotificationLogs.Where(l => l.HisNotificationRecipientId == recipientId).Select(l => l.DtSending).FirstOrDefault().ToString();
                int notifId = db.HisNotificationRecipients.Where(r => r.Id == recipientId).Select(r => r.HisNotificationId).FirstOrDefault();
                string fMessage = db.HisNotifications.Where(n => n.Id == notifId).Select(n => n.Message).FirstOrDefault();
                int fnotifId = db.HisNotifications.Where(n => n.Id == notifId).Select(n => n.Id).FirstOrDefault();
                string fRecType = db.HisNotifications.Where(n => n.Id == notifId).Select(n => n.RecType).FirstOrDefault();
                int fRefId = (int)db.HisNotifications.Where(n => n.Id == notifId).Select(n => n.RefId).FirstOrDefault();

                Dt.Rows.Add(recipientId, notifId, fRecipientNum, fRecType, fMessage, fDtSend,fRefId);

            }
            

            DataSet ds = new DataSet();
            ds.Tables.Add(Dt);
            ds.DataSetName = "Table";
            return ds;
        }


    }
}
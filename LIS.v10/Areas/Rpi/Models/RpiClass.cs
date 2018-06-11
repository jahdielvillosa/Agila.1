using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LIS.v10.Areas.Rpi.Models
{

    public class ComponentDetailLists
    {
        public int Id { get; set; } //component
        public int MapId { get; set; }
        public string ComponentName { get; set; }
        public string PinNo { get; set; }
        public string Description { get; set; }

    }

    public class DeviceDetailsLists
    {
        public int Id { get; set; } //component
        public string Description { get; set; }
        public string Version { get; set; }
        public double Temp { get; set; }
        public double Humidity { get; set; }
        public int Light { get; set; }
        public int Fan { get; set; }
        public int Water { get; set; }

    }


    public class DeviceSettingsDetails
    {
        public int Id { get; set; } //component
        public DateTime DateSchedule { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public int Light { get; set; }
        public int Socket01 { get; set; }
        public int Socket02 { get; set; }
        public int rpiDeviceId { get; set; }

    }


    public class LogDetailLists
    {
        public int Id { get; set; } //component
        public string DateTime { get; set; }
        public string Temp { get; set; }
        public string Humidity { get; set; }
        public string Light { get; set; }
        public string Fan { get; set; }
        public string Water { get; set; }
    }

    class RpiData
    {
        public string Temp { get; set; }
        public string Humidity { get; set; }
        public string Light { get; set; }
        public string Fan { get; set; }
        public string Water { get; set; }
    }



    public class RpiClass
    {
        public RpiDBContainer1 db = new RpiDBContainer1();
        public void getall()
        {

             var versionMap = db.RpiVersionMaps.Find(1);
        }

        public List<ComponentDetailLists> getComponents(int id)
        {
            var versionMap = db.RpiVersionMaps.Where(m => m.RpiVersionId == 1).ToList();

            DataTable Dt = new DataTable("Table");

            Dt.Columns.Add("Id", typeof(int));
            Dt.Columns.Add("VersionId", typeof(int));
            Dt.Columns.Add("NameMap", typeof(string));
            Dt.Columns.Add("ComponentName", typeof(string));
            Dt.Columns.Add("PinNo", typeof(int));

            //get details of each failed items from recipientId
            foreach (var map in versionMap)
            {
                var compDetails = db.RpiComponents.Where(c => c.Id == map.RpiComponentId).FirstOrDefault();

                int Id = compDetails.Id; //component id  
                int versionId = map.RpiVersionId;
                string nameMap = map.NameMap;
                string componentName = compDetails.ComponentName;
                string pinNo = map.PinNo;


                Dt.Rows.Add(Id, versionId, nameMap, componentName, pinNo);

            }
            
            var versionid = db.RpiDevices.Find(id).RpiVersionId;
            var mapLists = db.RpiVersionMaps.Where(m => m.RpiVersionId == versionid).ToList();

            List<ComponentDetailLists> cdlist = new List<ComponentDetailLists>();

            foreach (var map in mapLists)
            {
                var compDetails = db.RpiComponents.Where(c => c.Id == map.RpiComponentId).FirstOrDefault();

                cdlist.Add(new ComponentDetailLists()
                {
                    Id = compDetails.Id,
                    MapId = map.Id,
                    ComponentName = compDetails.ComponentName,
                    PinNo = map.PinNo,
                    Description = compDetails.Description
                });
            }

            return cdlist;
        }
    }
}
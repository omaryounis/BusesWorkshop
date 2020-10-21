using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusesWorkshop.VM
{
    public class DeviceInfoVM
    {
        public int Sn { get; set; }
        public int NetworkDeviceInformationID { get; set; }
        public string IpAddress { get; set; }
        public string DeviceName { get; set; }
        public string MacAddress { get; set; }
        public string Status { get; set; }
        public string CompName { get; set; }
        public string BranchAccName { get; set; }
        public string SectionName { get; set; }
        public string FloorName { get; set; }
        public string RoomName { get; set; }
        public string AssetMasterName { get; set; }
        public string SubAssetMasterName { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusesWorkshop.VM
{
    public class MobilesVM
    {
        public int MobileId { get; set; }
        public string MobileName { get; set; }
        public string  ServiceRequest  { get; set; }
        public int? MobileParentId { get; set; }
        public string MobileParentName { get; set; }
    }
}
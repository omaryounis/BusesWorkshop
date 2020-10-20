using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusesWorkshop.VM
{
    public class PhaseSettingVM
    {
        public int phases_Id { get; set; }
        public string phases_Name { get; set; }
       public string  Phase_Order { get; set; }
        public string requestType { get; set; }
        public string IsActive { get; set; }
        public string phase_Step { get; set; }
    }
}
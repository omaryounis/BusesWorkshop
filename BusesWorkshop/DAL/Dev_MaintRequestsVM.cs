using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusesWorkshop.DAL
{
    public class Dev_MaintRequestsVM
    {
        public int MaintReqId { get; set; }
        public bool? IsClosed { get; set; }
        public bool? PriorUrgent { get; set; }
        public bool? PriorHigh { get; set; }
        public bool? PriorLow { get; set; }
        public int? RequestedEmpId { get; set; }
        public int? CompanyId { get; set; }
        public DateTime  RequestDate { get; set; }
        public string CompName { get; set; }
        public string UserName { get; set; }
        public int? DaysNumber { get; set; }
        public string LocationName { get; set; }
        public string Priorty { get; set; }

    }
}
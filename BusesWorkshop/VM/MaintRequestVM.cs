using BusesWorkshop.DAL.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusesWorkshop.VM
{
    public class MaintRequestVM
    {
        public int EmpId { get; set; }
        public MaintRequest maintRequest { get; set; }
        public List<MaintReqDetail> maintReqDetailList { get; set; }
        public List<MaintReqPicture> maintReqPictureList { get; set; }
    }
}
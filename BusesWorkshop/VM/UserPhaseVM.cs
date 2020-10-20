using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusesWorkshop.VM
{
    [Serializable]
    public class UserPhaseVM
    {
        public int ID { get; set; }
        public int  phases_Id { get; set; }
        public  string Name { get; set; }
        public string phases_Name { get; set; }
        public int User_ID { get; set; }
        public int Users_ID { get; set; }
        public List<int> userIDS { get; set; }
        public bool IsActive { get; set; }
         
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using BusesWorkshop.DAL.Bus;

namespace BusesWorkshop.DAL
{
    public class UserGroups
    {
        #region Declaration

        public long? ID { get; set; }
        public int GroupID { get; set; }
        public int UserID { get; set; }
        public bool IsActive { get; set; }

        #endregion

        public UserGroups()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Methods

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="dc">dc</param>
        /// <returns></returns>
        public long Insert(WorkshopDataContext dc)
        {
            return dc.usp_UserGroups_Insert(this.GroupID, this.UserID, null);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="dc">dc</param>
        /// <returns></returns>
        public long Update(WorkshopDataContext dc)
        {
            return dc.usp_UserGroups_Update(this.ID, this.GroupID, this.UserID);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="dc">dc</param>
        /// <returns></returns>
        public long Delete(WorkshopDataContext dc)
        {
            return dc.usp_UserGroups_Delete(this.ID);
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <param name="dc">dc</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public static DataTable Select(WorkshopDataContext dc, long? id)
        {
            var ddd = from d in dc.usp_UserGroups_Select(id).Distinct()
                      select d;
            DataTable dt = ddd.CopyToDataTable();
            return dt;
        }

        #endregion
    }

}

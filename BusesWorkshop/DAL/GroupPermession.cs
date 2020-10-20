using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using BusesWorkshop.DAL.Bus;

namespace BusesWorkshop.DAL
{
    public class GroupPermession
    {

        #region Declaration

        public long? ID { get; set; }
        public int GroupID { get; set; }
        public int PageID { get; set; }
        public bool? Display { get; set; }
        public bool? InsertA { get; set; }
        public bool? UpdateA { get; set; }
        public bool? DeleteA { get; set; }
        public bool IsActive { get; set; }

        #endregion

        public GroupPermession()
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
            return dc.usp_GroupPermission_Insert(this.GroupID, this.PageID, this.Display, this.InsertA, this.UpdateA, this.DeleteA);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="dc">dc</param>
        /// <returns></returns>
        public int Update(WorkshopDataContext dc)
        {
            return dc.usp_GroupPermission_Update(this.ID, this.GroupID, this.PageID, this.Display, this.InsertA, this.UpdateA, this.DeleteA);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="dc">dc</param>
        /// <returns></returns>
        public long Delete(WorkshopDataContext dc)
        {
            return dc.usp_GroupPermission_Delete(this.ID);
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <param name="dc">dc</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public static DataTable Select(WorkshopDataContext dc, long? id)
        {
            DataTable dt = dc.usp_GroupPermission_Select(id).CopyToDataTable();
            return dt;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <param name="dc">dc</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public static DataTable SelectGroup(WorkshopDataContext dc, int? id)
        {
            DataTable dt = dc.usp_GroupPermission_GroupSelect(id).CopyToDataTable();
            return dt;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using BusesWorkshop.DAL.Bus;

namespace BusesWorkshop.DAL
{
    public class Group
    {
        #region Declaration

        public int? ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        #endregion

        public Group()
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
        public int Insert(WorkshopDataContext dc)
        {
            return dc.usp_Groups_Insert( this.Name, null);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="dc">dc</param>
        /// <returns></returns>
        public int Update(WorkshopDataContext dc)
        {
            return dc.usp_Groups_Update(this.ID, this.Name);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="dc">dc</param>
        /// <returns></returns>
        public int Delete(WorkshopDataContext dc)
        {
            return dc.usp_Groups_Delete(this.ID);
        }

        /// <summary>
        /// Delete Group Permisstion.
        /// </summary>
        /// <param name="dc">dc</param>
        /// <returns></returns>
        public int DeleteGroup(WorkshopDataContext dc)
        {
            return dc.usp_GroupPermission_GroupDelete(this.ID);
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <param name="dc">dc</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public static DataTable Select(WorkshopDataContext dc, int? id)
        {
            DataTable dt = dc.usp_Groups_Select(id).CopyToDataTable();
            return dt;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using BusesWorkshop.DAL.Bus;

namespace BusesWorkshop.DAL
{
    public class PageActions
    {
        #region Declaration

        public int? ID { get; set; }
        public int PageID { get; set; }
        public bool Display { get; set; }
        public bool InsertA { get; set; }
        public bool UpdateA { get; set; }
        public bool DeleteA { get; set; }

        #endregion

        public PageActions()
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
            return dc.usp_PageActions_Insert(this.PageID, this.Display, this.InsertA, this.UpdateA, this.DeleteA);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="dc">dc</param>
        /// <returns></returns>
        public int Update(WorkshopDataContext dc)
        {
            return dc.usp_PageActions_Update(this.ID, this.PageID, this.Display, this.InsertA, this.UpdateA, this.DeleteA);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="dc">dc</param>
        /// <returns></returns>
        public int Delete(WorkshopDataContext dc)
        {
            return dc.usp_PageActions_Delete(this.ID);
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <param name="dc">dc</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public static DataTable Select(WorkshopDataContext dc, int? id)
        {
            DataTable dt = dc.usp_PageActions_Select(id).CopyToDataTable();
            return dt;
        }

        #endregion
    }
}

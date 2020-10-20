using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BusesWorkshop.DAL.Bus;

namespace BusesWorkshop.DAL
{
    public class ClsBusServicesCheck
    {

        public DataTable Get_Service(int prmServiceID)
        {
            WorkshopDataContext dc = new WorkshopDataContext();
            DataTable dt = new DataTable();

            var query = from srv in dc.GetTable<ServicesSetting>().Where(sr => sr.ID == prmServiceID) select srv;

            return query.CopyToDataTable();
        }

        public DataTable Get_lastCheck(int prmServiceID, int prmBusID)
        {
            
            WorkshopDataContext dc = new WorkshopDataContext();
            DataTable dt = new DataTable();
            dt = null;

            // get last check ID
            int? lastCheckID = null;

            var val =    (from srv in dc.GetTable<BusesService>().Where(sr => sr.ServiceID == prmServiceID && sr.BusID == prmBusID) 
                         select srv).Max(x => (int?)x.ID);
            if (val != null)
            {
                lastCheckID = Convert.ToInt32(val);
            }
        

            // get Check Data by id
            if (lastCheckID != null)
            {
                var query = from rec in dc.GetTable<BusesService>().Where(sr => sr.ID == lastCheckID) select rec;

                dt = query.CopyToDataTable();
            
            
            }

            return dt; 
        }

        public int Check_Service(int prmServiceID, int prmBusID,int? prmKMcount ,DateTime prmCheckDate)
        {
                int result = 1;
                int monthesCount = 0;

                WorkshopDataContext dc = new WorkshopDataContext();
                DataTable dt = new DataTable();

                dt = Get_lastCheck(prmServiceID, prmBusID);

                DataTable dtt = new DataTable();
                dtt = Get_Service(prmServiceID);

                int? monthesFromSettings = null;
                if (!string.IsNullOrEmpty(dtt.Rows[0]["DateV"].ToString()))
                {
                    monthesFromSettings = Convert.ToInt32(dtt.Rows[0]["DateV"].ToString());

                }

                int? KMplus = null;
                if (!string.IsNullOrEmpty(dtt.Rows[0]["KVPlus"].ToString()))
                {
                    KMplus = Convert.ToInt32(dtt.Rows[0]["KVPlus"].ToString());

                }

                int? KMmin = null;
                if (!string.IsNullOrEmpty(dtt.Rows[0]["KVMinus"].ToString()))
                {
                    KMmin = Convert.ToInt32(dtt.Rows[0]["KVMinus"]);
                }


                #region if there is a previous Check ...
                if (dt!=null)
                {

                if (dt.Rows.Count > 0)
                {
                    DateTime lastCheckDate = Convert.ToDateTime(dt.Rows[0]["CheckDate"].ToString());
                    
                    int? lastCheckKiloM = null;
                    if (!string.IsNullOrEmpty(dt.Rows[0]["CheckKiloM"].ToString()))
                    {
                        lastCheckKiloM = Convert.ToInt32(dt.Rows[0]["CheckKiloM"].ToString());

                    }
                    
                    #region KMplus = KMmin = null, DateV has Value

                    if (monthesFromSettings != null && KMplus == null && KMmin == null)
                    {

                        if (prmCheckDate <= lastCheckDate)
                        {
                            // ---- new_date less than last_check_date
                            return -1;
                        }
                        else
                        {
                            monthesCount = ((prmCheckDate.Year - lastCheckDate.Year) * 12) + (prmCheckDate.Month - lastCheckDate.Month);

                            if (monthesCount < monthesFromSettings)
                            {
                                // Monthes less than the pre-required in settings
                                return -2;
                            }
                        }

                    }


                    #endregion

                    #region KMplus = null, KMmin has value, DateV has Value

                    else if (monthesFromSettings != null && KMmin != null && KMplus == null)
                    {
                                                   
                          if ((prmKMcount - lastCheckKiloM) < KMmin)
                            {
                                // (Current Inserted KM - last KM Checked) less than the pre-required in settings

                                return -4;
                            }

                          if (prmCheckDate <= lastCheckDate)
                          {
                              // ---- new_date less than last_check_date
                              return -1;
                          }
                          else
                          {
                              monthesCount = ((prmCheckDate.Year - lastCheckDate.Year) * 12) + (prmCheckDate.Month - lastCheckDate.Month);

                              if (monthesCount < monthesFromSettings)
                              {
                                  // Monthes less than the pre-required in settings
                                  return -2;
                              }
                          }
                              
                        
                          

                       // }

                    }


                    #endregion

                    #region KMplus & KMmin have values , DateV = null

                    else if (monthesFromSettings == null && KMmin != null && KMplus != null)
                    {
                        if ((prmKMcount - lastCheckKiloM ) < KMmin)
                        {
                            // prmKMcount less than the minimum value in settings
                            return -5;
                        }

                        if ((prmKMcount - lastCheckKiloM ) > KMplus)
                        {
                           // prmKMcount greater than the maximim value in settings
                            return -6;
                        }
                    }

                    #endregion

                }
                }
                #endregion

                #region if this is the first time to check
                
                else
                {
                    #region KMplus = KMmin = null, DateV has Value

                    if (monthesFromSettings != null && KMplus == null && KMmin == null)
                    {
                        // it is allowed to insert for first time
                        // there is no need to check validation here
                        return 1;
                    }


                    #endregion

                    #region KMplus = null, KMmin has value, DateV has Value

                    else if (monthesFromSettings != null && KMmin != null && KMplus == null)
                    {

                        if (prmKMcount < KMmin)
                        {
                            // (Current Inserted KM - last KM Checked) less than the pre-required in settings

                            return -7;
                        }

                     }


                    #endregion

                    #region KMplus & KMmin have values , DateV = null

                    else if (monthesFromSettings == null && KMmin != null && KMplus != null)
                    {
                        if (prmKMcount < KMmin)
                        {
                            // prmKMcount less than the minimum value in settings
                            return -5;
                        }

                        if (prmKMcount > KMplus)
                        {
                            // prmKMcount greater than the maximim value in settings
                            return -6;
                        }
                    }

                    #endregion

                }

                #endregion

                return result;
        }
    }
}

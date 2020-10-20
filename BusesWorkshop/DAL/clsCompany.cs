using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.Data.Linq;
using BusesWorkshop.DAL.Bus;
using BusesWorkshop.DAL.Accounting;

namespace Accounting.DAL
{
    public class clsCompany
    {
        public class BasicData
        {
            /// <summary>
            /// Insert
            /// </summary>
            /// <param name="companyName"></param>
            /// <param name="description"></param>
            /// <param name="address"></param>
            /// <param name="url"></param>
            /// <param name="logo"></param>
            /// <param name="phone"></param>
            /// <param name="fax"></param>
            /// <param name="status"></param>
            /// <param name="compID"></param>
            /// <returns></returns>
            public static int Insert(dcAccountingDataContext dc, string companyName, string description, string address, string url, Binary logo, string phone, string fax, bool status, int? compID, DateTime? starDate, DateTime? endDate, DataTable dtAccounting, bool isBankAccount, string docFooter, string accountantName, string fmName, string gmName)
            {
                //dcAccountingDataContext dc = new dcAccountingDataContext(DAL.Intro.ConnectionString);

                int company = dc.usp_COM_Insert(companyName, description, address, url, logo, phone, fax, status, compID, docFooter, accountantName, fmName, gmName);
                if (company > 0)
                {
                    //Company.CompanyID = company;

                    int fiscalYear = clsCompany.FiscalYear.Insert(starDate, endDate, company);

                    if (fiscalYear > 0)
                    {
                        int account = 0;
                        foreach (DataRow dr in dtAccounting.Rows)
                        {
                            account = dc.usp_BCD_Update(int.Parse(dr["ID"].ToString()), dr["Value"].ToString());
                        }
                        //  int account = clsChartOfAccount.COA_Insert(dtAccounting, isBankAccount, company);
                        if (account > 0)
                        {
                            return company;
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }

            /// <summary>
            /// Update
            /// </summary>
            /// <param name="companyID"></param>
            /// <param name="companyName"></param>
            /// <param name="description"></param>
            /// <param name="address"></param>
            /// <param name="url"></param>
            /// <param name="logo"></param>
            /// <param name="phone"></param>
            /// <param name="fax"></param>
            /// <param name="status"></param>
            /// <param name="compID"></param>
            /// <returns></returns>
            public static string Update(int? companyID, string companyName, string description, string address, string url, Binary logo, string phone, string fax, string docFooter, string accountantName, string fmName, string gmName)
            {
                string message = string.Empty;
                dcAccountingDataContext dc = new dcAccountingDataContext();
                int company = dc.usp_COM_Update(companyID, companyName, description, address, url, logo, phone, fax, docFooter, accountantName, fmName, gmName);
                if (company > 0)
                {
                    return "تم التعديل بنجاح";
                }
                else
                {
                    return "حدذ خطا أثناء الإدخال";
                }
            }

            /// <summary>
            /// Select
            /// </summary>
            /// <param name="companyID"></param>
            /// <returns></returns>
            public static DataTable Select(int? companyID)
            {
                dcAccountingDataContext dc = new dcAccountingDataContext();
                var dtCompany = dc.usp_COM_Select(companyID);
                if (dtCompany != null)
                {
                    return (dtCompany).CopyToDataTable();
                }
                else
                {
                    return null;
                }

            }

        }

        public static string[] GetFsyDates(int comID)
        {
            dcAccountingDataContext dc = new dcAccountingDataContext();
            string[] dateList = new string[2];
            if (dc.usp_FSY_Select(null).Where(f => f.COM_ID == comID && f.Is_Open == true).Any() == true)
            {
                var query = dc.usp_FSY_Select(null).Where(f => f.COM_ID == comID && f.Is_Open == true).SingleOrDefault();

                dateList[0] = query.FSYStartDate != null ? query.FSYStartDate.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");
                //dateList[1] = query.FSYEndDate != null ? query.FSYEndDate.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");
                dateList[1] = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                dateList[0] = DateTime.Now.ToString("dd/MM/yyyy");
                dateList[1] = DateTime.Now.ToString("dd/MM/yyyy");
            }

            return dateList;
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="companyName"></param>
        /// <param name="description"></param>
        /// <param name="address"></param>
        /// <param name="url"></param>
        /// <param name="logo"></param>
        /// <param name="phone"></param>
        /// <param name="fax"></param>
        /// <param name="status"></param>
        /// <param name="compID"></param>
        /// <returns></returns>
        public static int Insert(string companyName, string description, string address, string url, Binary logo, string phone, string fax, bool status, int? compID, string docFooter)
        {
            dcAccountingDataContext dc = new dcAccountingDataContext();
            int company = dc.usp_COM_Insert(companyName, description, address, url, logo, phone, fax, status, compID, docFooter, string.Empty, string.Empty, string.Empty);
            if (company > 0)
            {
                return company;
            }
            else
            {
                return -1;
            }
        }

        public static string Update(int? companyID, string companyName, string description, string address, string url, Binary logo, string phone, string fax, string docFooter)
        {
            string message = string.Empty;
            dcAccountingDataContext dc = new dcAccountingDataContext();
            int company = dc.usp_COM_Update(companyID, companyName, description, address, url, logo, phone, fax, docFooter, string.Empty, string.Empty, string.Empty);
            if (company > 0)
            {
                return "تم تعديل الشركه بنجاح";
            }
            else
            {
                return "حدذ خطا أثناء الإدخال";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public static DataTable Select(int? companyID)
        {
            dcAccountingDataContext dc = new dcAccountingDataContext();
            var dtCompany = dc.usp_COM_Select(companyID);
            if (dtCompany != null)
            {
                return (dc.usp_COM_Select(companyID)).CopyToDataTable();
            }
            else
            {
                return null;
            }
        }

        public static DataTable Select(int UserID)
        {
            dcAccountingDataContext dc = new dcAccountingDataContext();
            return dc.Usp_Company_Select_ByUserID(UserID).Distinct().CopyToDataTable();
            //if (dtCompany != null)
            //{
            //    return (dc.usp_COM_Select(companyID)).CopyToDataTable();
            //}
            //else
            //{
            //    return null;
            //}
        }

        public static string GetDefaultCompany(int userID)
        {
            dcAccountingDataContext dc = new dcAccountingDataContext();
            return dc.Fn_DefaultCompany(userID);
        }

        public static DataTable Select(int? companyID, int ParentID)
        {
            dcAccountingDataContext dc = new dcAccountingDataContext();
            return dc.usp_COM_Select(null).Where(m => m.ParentID == ParentID).CopyToDataTable();
            //if (dtCompany != null)
            //{
            //    return (dc.usp_COM_Select(companyID)).CopyToDataTable();
            //}
            //else
            //{
            //    return null;
            //}
        }

        public static DataTable Select()
        {
            dcAccountingDataContext dc = new dcAccountingDataContext();
            return dc.usp_COM_Select(null).Where(m => m.ParentID != null).CopyToDataTable();
            //if (dtCompany != null)
            //{
            //    return (dc.usp_COM_Select(companyID)).CopyToDataTable();
            //}
            //else
            //{
            //    return null;
            //}
        }




        public static DataTable SelectMain(int? companyID)
        {
            dcAccountingDataContext dc = new dcAccountingDataContext();
            //var dtCompany = dc.usp_COM_Select(companyID);
            //if (dtCompany != null)
            //{
            return (dc.usp_COM_Select(companyID).Where(m => m.ParentID == null)).CopyToDataTable();
            //}
            //else
            //{
            //    return null;
            //}
        }

        public class FiscalYear
        {
            public static int Insert(DateTime? startDate, DateTime? endDate, int? companyID)
            {
                dcAccountingDataContext dc = new dcAccountingDataContext();
                return dc.usp_FSY_Insert(startDate, endDate, true, companyID);
            }

            public static DataTable Select(int? fiscalID)
            {
                dcAccountingDataContext dc = new dcAccountingDataContext();
                var fiscalYear = from year in dc.usp_FSY_Select(fiscalID)
                                 select year;
                if (fiscalYear != null)
                {
                    return fiscalYear.CopyToDataTable();
                }
                else
                {
                    return null;
                }
            }
        }


        /// <summary>
        /// Employee Class
        /// </summary>
        public class Employee
        {
            /// <summary>
            /// Employee.
            /// </summary>
            /// <param name="dc"></param>
            /// <param name="contactID"></param>
            /// <param name="ContactName"></param>
            /// <param name="description"></param>
            /// <param name="compID"></param>
            /// <param name="url"></param>
            /// <param name="account"></param>
            /// <returns></returns>
            public static string CreateUser(dcAccountingDataContext dc, int? contactID, string userName, string password, string conPassword, int? companyID, int con_usr_ID)
            {
                int result = 0;
                try
                {
                    result = dc.usp_CON_USR_Update(contactID, userName, password, conPassword, "Admin", companyID, con_usr_ID);
                    if (result == -1)
                    {
                        return "هذا الاسم مستخدم من قبل .";
                    }
                    else
                    {
                        int res = dc.usp_CON_COM_Update(contactID, companyID);
                        if (res > 0)
                        {
                            return clsCommon.Message.sucess;
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                }
                catch
                {
                    return clsCommon.Message.failed;
                }
            }
        }



    }
}

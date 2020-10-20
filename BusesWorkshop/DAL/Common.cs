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
using System.Globalization;
using System.Threading;
using BusesWorkshop.DAL.Bus;

namespace BusesWorkshop.DAL
{
    public class Common
    {

        public static int UserIDForSec = 0;
        public static string Connection()
        {
            return ConfigurationManager.ConnectionStrings["BusWorkshopConnectionString"].ConnectionString;
        }

        public enum PagesEnum
        {
            BusCheck = 1,
            VehclesDefinition = 2,
            Drivers = 3,
            ServicesSettings = 4,
            SpareParts = 5,
            UserCompanies = 6,
            Brands = 7,
            Classes = 8,
            Employees = 9,
            Models = 10,
            GroupPermission = 11,
            Groups = 12,
            UserGroup = 13,
            ConfigurationPage = 14,
            Users = 15,
            PeriodicPlanToVehcle = 16,
            PeriodicalMaitenencePlan = 18,
            FuelCardsDefinition = 20,
            FuelCardsCustody = 21,
            CarFollowUp = 22,
            Alarms =23,
            PeriodicalPlanExecution = 32,
            rpt_FollowUpCards = 26,
            rpt_RequiredPlans = 27,
            Rpt_MaintenenceCost = 28,
            Rpt_SuperCardsBalance = 29,
            rpt_TotalCost = 30,
            rpt_Vecles1 = 31,
            rptBusesDrivers = 32,
            rptBusesRenew = 33,
            rptBusesServices = 34,
            rptReminder = 35, 
            Vehcles2 = 33,
            District = 34,
            Streets = 35,
            Buildings = 36,
            Material = 37,
            BuildingPlans = 38,
            PeriodicalPlanToBuilding = 39,
            BuildingMaint = 40,
            BuildingPeriocalExec = 41,
            AccAstLocations = 42,
            MaintRequest = 43,
            Mobiles = 44,
            MaintRequestProceeding = 46,
            rpt_FuelCardBalancies = 47,
            rpt_MaintRequests = 48,
            SearchFollowCars = 49,
            StaticIPAddressWithBranches=50
        }
        ;


        /// <summary>
        /// Get User Permission for specific page
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="userID"></param>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public static DataTable GetUserPermission(WorkshopDataContext dc, int userID, int pageID)
        {
            DataTable permission = (from per in dc.usp_User_PagesPermission(userID, pageID)
                                    select new
                                    {
                                        D = per.Display,
                                        I = per.InsertA,
                                        U = per.UpdateA,
                                        R = per.DeleteA
                                    }).CopyToDataTable();
            return permission;
        }
        public static string ConvertDateCalendar(DateTime DateConv, string Calendar, string DateLangCulture)
        {
            System.Globalization.DateTimeFormatInfo DTFormat;
            DateLangCulture = DateLangCulture.ToLower();
            /// We can't have the hijri date writen in English. We will get a runtime error - LAITH - 11/13/2005 1:01:45 PM -

            if (Calendar == "Hijri" && DateLangCulture.StartsWith("en-"))
            {
                DateLangCulture = "ar-SA";
            }

            /// Set the date time format to the given culture - LAITH - 11/13/2005 1:04:22 PM -
            DTFormat = new System.Globalization.CultureInfo(DateLangCulture, false).DateTimeFormat;

            /// Set the calendar property of the date time format to the given calendar - LAITH - 11/13/2005 1:04:52 PM -
            switch (Calendar)
            {
                case "Hijri":
                    DTFormat.Calendar = new HijriCalendar();
                    DateConv = DateConv.AddDays(-1);
                    break;

                case "Gregorian":
                    DTFormat.Calendar = new GregorianCalendar();
                    break;

                default:
                    return "";
            }

            /// We format the date structure to whatever we want - LAITH - 11/13/2005 1:05:39 PM -
            DTFormat.ShortDatePattern = "dd/MM/yyyy";
            //return (DateConv.Date.ToString("f", DTFormat));//This For Full date Formate In Details
            return (DateConv.Date.ToString("dd/MM/yyyy", DTFormat));

        }
        public static string GetGregorianDate(string hijriDate)
        {
            try
            {
                return DateTime.Parse(hijriDate, new CultureInfo("ar-SA")).ToString("dd/MM/yyyy", new CultureInfo("en-US"));
            }
            catch
            {
                return DateTime.Parse(hijriDate).ToString();
            }
            //Full Date return DateTime.Parse(hijriDate, new CultureInfo("ar-SA")).ToString("D", new CultureInfo("en-US"));

        }
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }



}

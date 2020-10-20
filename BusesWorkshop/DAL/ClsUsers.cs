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

public class ClsUsers
{
    //public string userName { get; set; }
    //public string Password { get; set; }
    //public int userID { get; set; }
    //public char contactType { get; set; }
    //public int? branchID { get; set; }
    //public string name { get; set; }
    //public bool IsActive { get; set; }

    //public int ID { get; set; }



    public ClsUsers()
    {
    }
    public int checkLogin()
    {
        //WorkshopDataContext dc = new WorkshopDataContext();
        //return dc.CheckLogin(userName, Password).Value;

        return 0;

    }
   
   
    public int Update_User(int prmUserID,string prmName, string prmUserName, string prmUserPassword ,bool prmIsActive)
    {
        WorkshopDataContext dc = new WorkshopDataContext();
        return dc.usp_Users_Update(prmUserID, prmName, prmUserName, prmUserPassword, prmIsActive);
        
    }

    public DataTable SelectUsers()
    {
        WorkshopDataContext dc = new WorkshopDataContext();
        return dc.usp_Users_Select().CopyToDataTable();
        
    }

    public DataTable SelectUsers(int prmUserID)
    {
        WorkshopDataContext dc = new WorkshopDataContext();
        return dc.usp_Users_Select().Where(u => u.ID == prmUserID).CopyToDataTable();

    }

    public int InsertUser(string prmName, string prmUserName, string prmUserPassword , bool prmIsActive)
    {

        WorkshopDataContext dc = new WorkshopDataContext();
        return dc.usp_Users_Insert(prmName, prmUserName, prmUserPassword, prmIsActive);
    }


    public int Delete(int prmUserID)
    {
        WorkshopDataContext dc = new WorkshopDataContext();
        return dc.usp_Users_Delete(prmUserID);

    }

}
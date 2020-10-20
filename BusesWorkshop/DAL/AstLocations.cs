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
using System.Collections.Generic;
using BusesWorkshop.DAL.Bus;
using BusesWorkshop.DAL.Accounting;

/// <summary>
/// Summary description for AstLocations
/// </summary>
public class AstLocations
{
    public int? ID { get; set; }
    public string LocationName { get; set; }
    public int? ParentID { get; set; }
    public bool? IsDefault { get; set; }
    public bool? IsShown { get; set; }
    public bool? IsMain { get; set; }
    public int Status { get; set; }
    public int UserID { get; set; }
    public int CompanyID { get; set; }


    public int SaveAstLocation(dcAccountingDataContext dc)
    {
        int result = 0;

        if (Status == 1)
        {
            result = dc.usp_Ast_Location_Insert(LocationName, ParentID, IsDefault, IsShown, IsMain, CompanyID);
        }
        else if (Status == 2)
        {
            result = dc.usp_Ast_Location_Update(ID, LocationName, ParentID, IsDefault, IsShown, IsMain, CompanyID);
        }
        return result;
    }

    public int DeleteAstLocation(dcAccountingDataContext dc)
    {
        if (CheckLocationIsUsed(dc) == false)
        {
            int result = 0;
            result = dc.usp_Ast_Location_Delete(ID);
            return result;
        }
        else
        {
            return -2;
        }
    }

    private bool CheckLocationIsUsed(dcAccountingDataContext dc)
    {
        return dc.usp_AST_Select().Where(a => a.Location == ID).Any();
    }


    public DataTable SelectAstLocation(dcAccountingDataContext dc)
    {
        return dc.usp_AstLocations_Select_ByUserID(UserID).CopyToDataTable();
    }

    public List<BusesWorkshop.DAL.Accounting.usp_AstLocations_Select_ByUserIDResult> SelectAstLocationByID(dcAccountingDataContext dc)
    {
        return dc.usp_AstLocations_Select_ByUserID(UserID).Where(s => s.ID == ID).ToList();
    }

    public DataTable SelectParentLocationByID(dcAccountingDataContext dc)
    {
        DataTable dt= dc.usp_AstLocations_Select_ByUserID(UserID).Where(s => s.IsMain == true).Select(s => new
        {
            s.ID,
            s.LocationName
        }).CopyToDataTable();

        return dt;
    }





}

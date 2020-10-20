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
using DevExpress.XtraEditors;
using DevExpress.Web;

namespace BusesWorkshop.DAL
{
    public static  class MyClasses
    {
      public static void  ClearControls(Control c)
      {
      foreach (Control c1 in c.Controls)
        {
            if(c1.GetType()== typeof(TextBox))
            {
                ((TextBox) c1).Text=string.Empty ;
            }
            if (c1.GetType() == typeof(ASPxComboBox))
            {
                ((ASPxComboBox)c1).Text = string.Empty ;
                ((ASPxComboBox)c1).Value =null;
            }
            if (c1.GetType() == typeof(CheckBox))
            {
                ((CheckBox)c1).Checked = false;
            }
            if(c1.HasControls())
            {
                ClearControls(c1);
            }
        }
      
      }
   


    }
}

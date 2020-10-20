using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR.Pages
{
    public partial class Rpt_Statment_All_Cases : System.Web.UI.Page
    {
           //Protection security = new Protection(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Div_EncryptionControl.Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Txt_Code.Text == "ns6%WJ%L#U92hGSJ")
        {
            Div_EncryptionControl.Visible = true;
            Div_CodeControls.Visible = false;
        }
    }
    protected void DecryptConnections(object sender, EventArgs e)
    {

        Txt_Msg.Text= UnProtectSection();

    }
    protected void EncryptConnections(object sender, EventArgs e)
    {

        Txt_Msg.Text = ProtectSection();

    }


    #region Protection Block

    static string provider = "RSAProtectedConfigurationProvider";
    static string sectionName = "connectionStrings";



    //call: ProtectSection("connectionStrings","DataProtectionConfigurationProvider"); 
    public string ProtectSection()
    {
        HttpContext.Current.Response.Write("");
        List<string> conns = new List<string>();
        Configuration config =
            WebConfigurationManager.
                OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

        ConfigurationSection section = config.GetSection(sectionName);

        if (section != null && !section.SectionInformation.IsProtected)
        {
            section.SectionInformation.ProtectSection(provider);
            config.Save();

        }
        return "Configuration Section is automatically encrypted";
    }

    //call: UnProtectSection("connectionStrings"); 
    public string UnProtectSection()
    {
        HttpContext.Current.Response.Write("");
        var conns = new List<string>();
        Configuration config =
            WebConfigurationManager.
                OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

        ConfigurationSection section = config.GetSection(sectionName);

        if (section != null && section.SectionInformation.IsProtected)
        {
            section.SectionInformation.UnprotectSection();
            config.Save();
        }

        //foreach (ConnectionStringSettings c in WebConfigurationManager.ConnectionStrings)
        //{
        //    string connection = c.Name + ":" + c.ConnectionString;
        //    conns.Add(connection);
        //}
        return "Configuration Section is automatically decrypted";

    }

    public string GetConnectionString(string conName)
    {

        string connectionString = "";
        Configuration config =
            WebConfigurationManager.
                OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

        ConfigurationSection section = config.GetSection(sectionName);
        if (section != null && section.SectionInformation.IsProtected)
        {
            section.SectionInformation.UnprotectSection();
            config.Save();
        }
        foreach (ConnectionStringSettings c in WebConfigurationManager.ConnectionStrings)
        {
            if (c.Name == conName)
            {
                connectionString = c.ConnectionString;
            }
        }
        section.SectionInformation.ProtectSection(provider);
        config.Save();

        return connectionString;
    }

    #endregion
    }
}
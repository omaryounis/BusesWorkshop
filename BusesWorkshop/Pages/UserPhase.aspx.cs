using BusesWorkshop.DAL;
using BusesWorkshop.DAL.Bus;
using BusesWorkshop.VM;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using PowerfulExtensions.Linq;
//using DevExpress.Web.Demos;
namespace BusesWorkshop.Pages
{
    [Serializable]
    public partial class UserPhase : System.Web.UI.Page
    {
        #region "Properties"
        WorkshopDataContext dc = new WorkshopDataContext();

       // public event ASPxGridViewDetailRowEventHandler DetailRowExpandedChanged;
        public event EventHandler onSelectedIndexChanged;

        private int ID
        {
            get
            {
                if (ViewState["ID"] != null)
                {
                    return Convert.ToInt16(ViewState["ID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("ID", value);
            }
        }

        private int User_ID
        {
            get
            {
                if (ViewState["User_ID"] != null)
                {
                    return Convert.ToInt32(ViewState["User_ID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("User_ID", value);
            }
        }
        #endregion
        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {

 
                    //if (Session["ID"] != null && Convert.ToInt32(Session["UserID"].ToString()) > 0)
                    //{
                   
                    permissions(dc);
                    FillgridPhases();

                    fillUsers();
                    FillPhases();


                
                //else
                //    {
                //        Response.Redirect(@"..\Pages\Login.aspx");
                //    }
                    Page.Title = "اسناد مراحل للمستخدمين";
               

            }
            //  DemoHelper.Instance.ControlAreaMinHeight = Unit.Pixel(230);


        }


        protected void BtnEditClick(object sender, EventArgs e)

        {

            var btn = sender as LinkButton;
            var gridContainer = btn.NamingContainer as GridViewDataItemTemplateContainer;
       
            DataTable dt = new DataTable();
            
           
            var editgrid =from p in dc.GetTable<User_Phase>().Where(c => c.phases_Id== int.Parse(DataBinder.Eval(gridContainer.DataItem, "phases_Id").ToString()))
                           select new {p.ID,p.phases_Id,p.User_ID,p.IsActive };
           var   gdPhase = from c in dc.Phases
                          join p in dc.User_Phases on (int)c.phases_Id equals (int)p.phases_Id
                          join u in dc.Users
                             on (int)p.User_ID equals (int)u.ID
                      

                    //      where (p.phases_Id == Convert.ToInt32(detailGridUsers.GetMasterRowKeyValue().ToString()))
                          select new UserPhaseVM
                          {
                              ID = p.ID,
                              phases_Id = p.phases_Id,
                              phases_Name = c.phases_Name,
                              Name = u.Name,
                              User_ID = p.User_ID
                              ,
                              //  User_ID = u.ID,
                              //     IsActive = c.IsActive == 0 ? true : c.IsActive == 1 ? false : false
                              IsActive = p.IsActive == 0 ? true : p.IsActive == 1 ? false : false

                          };
            dt = editgrid.CopyToDataTable();
            ASPxListBox MultiDDLListBox = (ASPxListBox)ddl_UserId.FindControl("listBox");
            UserPhaseVM d = (UserPhaseVM)ViewState["gdPhase"];
            MultiDDLListBox.SelectedIndex = -1;
            foreach (ListEditItem item in MultiDDLListBox.Items)
            {
                foreach (var userID in d.User_ID.ToString())
                {
                    if (userID == Convert.ToInt32(item.Value))
                    {
                        item.Selected = true;
                        //ddl.Text += item.Text + ";";
                    }
                }






                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dtrow in dt.Rows)

                    {
                        ddl_Phases.Value = dt.Rows[0]["phases_Id"].ToString();

                        rdbtn_IsActive.Value = dt.Rows[0]["IsActive"].ToString();

                        MultiDDLListBox.Value =
                       dtrow["User_ID"].ToString();

                    }



                }
            }
        
        }
  

        protected void BtnSave_Click(object sender, EventArgs e)
        {

            int result = 0;

            if (ID > 0)
            {

                User_Phase user_Phase1 = dc.User_Phases.Single(c => c.ID == ID);
                ASPxListBox MultiDDLListBox1 = (ASPxListBox)ddl_UserId.FindControl("listBox");

                foreach (var item2 in MultiDDLListBox1.SelectedValues)
                {
                    // user_Phase1 = new User_Phase();

                    if (rdbtn_IsActive.SelectedItem != null)
                    {
                        user_Phase1.IsActive = int.Parse(rdbtn_IsActive.SelectedItem.Value.ToString());
                    }

                    if (ddl_Phases.SelectedItem != null)
                    {

                        user_Phase1.phases_Id = int.Parse(ddl_Phases.SelectedItem.Value.ToString());

                    }



                    user_Phase1.User_ID = (int.Parse(item2.ToString()));


                    dc.SubmitChanges();
                    result = user_Phase1.ID;
                }
            }
            else
            {

              //  List<int> UsersIDs = new List<int>();

                ASPxListBox MultiDDLListBox = (ASPxListBox)ddl_UserId.FindControl("listBox");


                if (MultiDDLListBox.SelectedItems != null)
                {


                    foreach (var item2 in MultiDDLListBox.SelectedValues)
                    {
                        User_Phase user_Phase = new User_Phase();


                        user_Phase.User_ID = (int.Parse(item2.ToString()));

                        if (ddl_Phases.SelectedItem != null)
                        {
                            user_Phase.phases_Id = int.Parse(ddl_Phases.SelectedItem.Value.ToString());

                        }

                        else
                        {
                            user_Phase.phases_Id = 0;

                        }

                        if (rdbtn_IsActive.SelectedItem != null)
                        {
                            user_Phase.IsActive = int.Parse(rdbtn_IsActive.SelectedItem.Value.ToString());

                        }

                        else
                        {
                            user_Phase.IsActive = null;
                        }


                        dc.User_Phases.InsertOnSubmit(user_Phase);
                        dc.SubmitChanges();
                        result = user_Phase.ID;

                    }



                }

            }


            
            if (result > 0)
            {
                divMsg2.Attributes["class"] = "alert alert-success text-right";
                lblResult.Text = "تم الحفظ بنجاح";

                //    // txtNotes.Text = txtKMcount.Text = txtCheckDate.Text = string.Empty;

            }
            else
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "حدث خطأ أثنا الحفظ";

            }
            MyClasses.ClearControls(Page);
   //         empty_Controls();
              empty_Controls();
            FillgridPhases();

        }
        //protected void Grid_SelectionChanged(object sender, EventArgs e)
        //{
        //    ASPxGridView grid = sender as ASPxGridView;
        //    for (int i = 0; i < grid.VisibleRowCount; i++) // Loop through selected rows 
        //    {
        //        if (grid.Selection.IsRowSelected(i)) // do whatever you need to do with selected row values
        //        {
        //            // now use pre-initialized List<object> selectedList to save 
        //            .Add(Convert.ToInt32(grid.GetRowValues(i, "User_ID")));
        //        }
        //    }
        //    ViewState["SelectedList"] = ;
        //}
        protected void BtnDeleteClick(object sender, EventArgs e)
        {
            ASPxGridView gridView;

            gridView = sender as ASPxGridView;
            
            LinkButton lnk = (LinkButton)sender;
             //  GridViewRow row = (GridViewRow)lnk.NamingContainer;
         //       User_Phase phase = dc.User_Phases.Single(c => c.ID == int.Parse(grd_Phases.[row.RowIndex].Value.ToString()));
            var btn = sender as LinkButton;
            var gridContainer = btn.NamingContainer as GridViewDataItemTemplateContainer;
      //      var User_ID = Convert.ToInt64(gridView.GetSelectedFieldValues("User_ID")[0]);
            //    int.Parse(DataBinder.Eval(gridContainer.DataItem, "User_ID").ToString());
          //  ViewState["User_ID"] = User_ID;
       

            //  var Query 
            var query = from p in dc.User_Phases
                                 //Single(m => m.User_ID == int.Parse(DataBinder.Eval(gridContainer.DataItem, "User_ID").ToString()));
                             where p.User_ID ==
                             //Convert.ToInt32(e.GetHashCode.ToString())
                             int.Parse(DataBinder.Eval(gridContainer.DataItem, "User_ID").ToString())
                         //    Convert.ToInt64(gridView.GetSelectedFieldValues("User_ID")[0])
                            select p;
                        
            try
            {
                // foreach (var item in query)
                // {
                dc.User_Phases.DeleteAllOnSubmit(query);
                    dc.SubmitChanges();
             //   }
              
            }
            catch (SqlException ex)
            {

                if (ex.Errors[0].Number == 547)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفو السجل المراد حذفة مرتبط بمدخلات اخري";
                    return;
                }
            }
            FillgridPhases();

            }
        protected void detailGridUsers(object sender, GridViewRowEventArgs e)
        {
            ASPxGridView detailGridUsers = (ASPxGridView)grd_Phases.FindControl("grd_detailGridUsers");
            detailGridUsers.DataSource = ViewState["grd_Phases"];
            detailGridUsers.DataBind();

             
        }
      



        protected void btn_Close_Click(object sender, EventArgs e)
        {
            if (ddl_UserId.FindControl("listBox") is ASPxListBox vacsListBox)
            {
                ddl_UserId.Text = "";
                foreach (ListEditItem item in vacsListBox.SelectedItems)
                {
                    ddl_UserId.Text += ";" + item.Text;
                }
            }
            SelectedIndexChanged();
        }




        protected void grd_Phases_BeforePerformDataSelect(object sender, EventArgs e)
        {


            //m.MaintReqId == Convert.ToInt32(dxUserRequests.GetMasterRowKeyValue().ToString())
            ASPxGridView detailGridUsers = (ASPxGridView)sender;//grd_Phases.FindControl("grd_detailGridUsers");
            Session["ID"] = Convert.ToInt32(detailGridUsers.GetMasterRowKeyValue().ToString());


            var gdPhase = from c in dc.Phases
                          join p in dc.User_Phases on (int)c.phases_Id equals (int)p.phases_Id
                          join u in dc.Users
                             on (int)p.User_ID equals (int)u.ID

                          //    where p.User_ID == (int)Session["User_ID"]

                          where (p.phases_Id == Convert.ToInt32(detailGridUsers.GetMasterRowKeyValue().ToString()))
                          select new UserPhaseVM
                          {
                              ID = p.ID,
                              phases_Id = p.phases_Id,
                              phases_Name = c.phases_Name,
                              Name = u.Name,
                              User_ID = p.User_ID,
                              
                          
                              
                            //  User_ID = u.ID,
                              //     IsActive = c.IsActive == 0 ? true : c.IsActive == 1 ? false : false
                              IsActive = p.IsActive == 1 ? true :false

                          };
            ViewState["grd_Phases"] = gdPhase.Distinct().ToList();
               
            detailGridUsers.DataSource = ViewState["grd_Phases"];



        }


        protected void grd_Phases_DataBinding1(object sender, EventArgs e)
        {

            if (sender is ASPxGridView grid)
            {
                grid.DataSource = ViewState["grd_Phases"];
            }
        }
        #endregion
        #region "Methods"
        private void FillPhases()
        {

            var phasesource = from c in dc.Phases select new { c.phases_Id, c.phases_Name }
                                ;
            ddl_Phases.DataSource = phasesource;
            ddl_Phases.TextField = "phases_Name";
            ddl_Phases.ValueField = "phases_Id";
            ddl_Phases.DataBind();
        }
        public void FillgridPhases()
        {


            var gdPhase = from p in dc.Phases

                          join c in dc.User_Phases on (int)p.phases_Id equals (int)c.phases_Id
                          join u in dc.Users
                             on (int)c.User_ID equals (int)u.ID into j1
                          from j2 in j1.DefaultIfEmpty()
                          group new { j2, c.User_ID } by new { c.phases_Id, p.phases_Name } into g

                          select new UserPhaseVM
                          {

                              phases_Id = g.Key.phases_Id,//.Select(x=>x.phases_Id),
                              phases_Name = g.Key.phases_Name,
                              Users_ID = g.Count(t => t.User_ID != null)
                              
                          };
            
                          // if (ViewState["UPData"] != null)
                          //{
             ViewState["gdPhase"] = gdPhase.ToList();
            grd_Phases.DataSource = ViewState["gdPhase"];
            grd_Phases.DataBind();


        }
       



      
        public void fillUsers()
        {
            var Usersource = from p in dc.Users select new { p.ID, p.Name };

            if (ddl_UserId.FindControl("listBox") is ASPxListBox branchesListBox)
            {
                branchesListBox.DataSource = Usersource;
                branchesListBox.TextField = "Name";
                branchesListBox.ValueField = "ID";
                //branchesListBox.AutoPostBack = AutoPostBack;
                branchesListBox.DataBind();

            }
        }

        public SelectedValueCollection Get_SelectedValues()
        {
            ASPxListBox lstUsers = (ASPxListBox)ddl_UserId.FindControl("listBox");
            return lstUsers.SelectedValues;
        }


        public void Set_SelectedValues(List<int> UsersIDs)
        {
            ASPxListBox MultiDDLListBox = (ASPxListBox)ddl_UserId.FindControl("listBox");
            //ddl.Text = string.Empty;
            MultiDDLListBox.SelectedIndex = -1;
            foreach (ListEditItem item in MultiDDLListBox.Items)
            {
                foreach (var userID in UsersIDs)
                {
                    if (userID == Convert.ToInt32(item.Value))
                    {
                        item.Selected = true;
                        //ddl.Text += item.Text + ";";
                    }
                }
            }
        }
        public void Claer_SelectedItems()
        {
            ASPxListBox MultiDDLListBox = (ASPxListBox)ddl_UserId.FindControl("listBox");
            MultiDDLListBox.Items.Clear();
            ddl_UserId.Text = null;
        }

     
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.UserPhase.GetHashCode());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["D"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["D"].ToString()))
                    {
                        Response.Redirect(@"..\Pages\Login.aspx", false);
                    }
                    if (dt.Rows[0]["R"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["R"].ToString()))
                    {
                        ViewState["AllowDelete"] = 0;
                    }
                    else
                    {
                        ViewState["AllowDelete"] = 0;
                    }

                    if (dt.Rows[0]["U"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["U"].ToString()))
                    {
                        ViewState["AllowUpDate"] = 0;
                    }
                    else
                    {
                        ViewState["AllowUpDate"] = 1;
                    }
                    if (dt.Rows[0]["I"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["I"].ToString()))
                    {
                        ViewState["AllowInsert"] = 0;
                    }
                    else
                    {
                        ViewState["AllowInsert"] = 1;
                    }
                }
                else
                {
                    Response.Redirect(@"..\Pages\Login.aspx");
                }
            }
            catch
            {
                Response.Redirect(@"..\Pages\Login.aspx");
            }
        }


        private void SelectedIndexChanged()
        {
            if (onSelectedIndexChanged != null)
            {
                onSelectedIndexChanged(this, EventArgs.Empty);
            }
        }


        public List<int> Get_SelectedIDs()
        {
            List<int> UsersIDs = new List<int>();
            ASPxListBox MultiDDLListBox = (ASPxListBox)ddl_UserId.FindControl("listBox");
            foreach (var value in MultiDDLListBox.SelectedValues)
            {
                UsersIDs.Add(int.Parse(value.ToString()));
            }
            return UsersIDs;
        }
        void empty_Controls()
        {
            // Claer_SelectedItems();
         //   ASPxListBox MultiDDLListBox = (ASPxListBox)ddl_UserId.FindControl("listBox");
          //  MultiDDLListBox.Items.Clear();
        //    ddl_UserId.Text = null;
          //  ddl_UserId.Text = null;
            rdbtn_IsActive.Value = ddl_Phases.Value = 
                 string.Empty;
            Claer_SelectedItems();
           ID = 0;
        }



       


        #endregion



      
       


    }
}



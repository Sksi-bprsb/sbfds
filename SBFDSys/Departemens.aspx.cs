using FDS.Library.Model;
using BPRSB.Kuesioner.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SBFDSys

{
    public partial class Departemens : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUser"] == null)
                Response.Redirect("default.aspx");
            //LoadData();
            else if(!IsPostBack)
            {
                LoadData();
            }
        }
        public void LoadData()
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            UserGroup group = MySqlRepository.GetById<UserGroup>(Convert.ToInt32(user.UserGroupId));
            List<Departemen> das = MySqlRepository.GetAll<Departemen>("NamaDepartemen like '%" + TextBoxSearch.Text + "%' ");

            foreach (Departemen r in das)
                r.Active = group.GetBoolValue("departemen", "hapus");
            Session["List_Departemen"] = das;
            ListViewData.DataSource = das;
            ListViewData.DataBind();
        }

        public void ViewData()
        {
            // Response.Redirect("CabangsUpdate.aspx?Cabang_id=" + Cabang_id);
        }
        protected void ButtonDeleteDepartemen_Click(object sender, EventArgs e)
        { 
            Control button = (Control)sender;
            ListViewDataItem item = (ListViewDataItem)button.BindingContainer;
            List<Departemen> departemens = (List<Departemen>)Session["List_Departemen"];
            Departemen departemen =departemens[item.DataItemIndex];
            Session["Selected_Departemen"] = departemen;
            LabelConfirmation.Text = "Please confirm to delete Departemen '" + departemen.NamaDepartemen + "'.";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ModalConfirmation", "$(document).ready(function () {$('#ModalConfirmation').modal();});", true);
        }
        protected void ButtonUpdateDepartemen_Click(object sender, EventArgs e)
        {
            
            Control button = (Control)sender;
            ListViewDataItem item = (ListViewDataItem)button.BindingContainer;
            List<Departemen> departemens = (List<Departemen>)Session["List_Departemen"];
            Departemen departemen = departemens[item.DataItemIndex];
            Response.Redirect("DepartemenAddUpdate.aspx?DepartemenId=" + departemen.DepartemenId);
        }
        protected void ButtonOKConfirm_Click(object sender, EventArgs e)
        {
            try
            { 
                Departemen dpt = (Departemen)Session["Selected_Departemen"]; 
                MySqlRepository.Delete(dpt);
                LoadData();
            }
            catch (Exception x)
            {
                LabelError.Text = x.Message;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ModalError", "$(document).ready(function () {$('#ModalError').modal();});", true);
            }
        }
        protected void ListViewData_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            lvDataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            LoadData();
        }
        protected void ButtonSearchDepartemen_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void ButtonViewData_Click(object sender, EventArgs e)
        { 
            Control button = (Control)sender;
            ListViewDataItem item = (ListViewDataItem)button.BindingContainer;
            List<Departemen> das = (List<Departemen>)Session["List_Departemen"];
        }
    }
}
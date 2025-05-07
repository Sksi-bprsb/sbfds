using System;
using BPRSB.Kuesioner.Repository;
using FDS.Library.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SBFDSys
{
    public partial class UserGroups : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                if (Session["LoginUser"] == null)
                    Response.Redirect("default.aspx");
                else if (!IsPostBack)
                {
                    LoadData();
                }
        }
        public void LoadData()
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            UserGroup group = MySqlRepository.GetById<UserGroup>(Convert.ToInt32(user.UserGroupId));
            List<UserGroup> das = MySqlRepository.GetAll<UserGroup>("NamaUserGroup like '%" + TextBoxSearch.Text + "%' ");
            //foreach (UserGroup r in das)
            //    r.Active = group.GetBoolValue("usergroupsetting", "simpan");
            Session["List_Group"] = das;
            ListViewData.DataSource = das;
            ListViewData.DataBind();
        }
       
        protected void ButtonOKConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                UserGroup rom = (UserGroup)Session["Selected_Group"];
                MySqlRepository.Delete(rom);
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
        
        protected void ButtonViewData_Click(object sender, EventArgs e)
        {
            Control button = (Control)sender;
            ListViewDataItem item = (ListViewDataItem)button.BindingContainer;
            List<UserGroup> das = (List<UserGroup>)Session["List_Group"];
        }


        protected void ButtonDeleteGroup_Click(object sender, EventArgs e)
        {
            Control button = (Control)sender;
            ListViewDataItem item = (ListViewDataItem)button.BindingContainer;
            List<UserGroup> groups = (List<UserGroup>)Session["List_Group"];
            UserGroup groupsb = groups[item.DataItemIndex];
            Session["Selected_Group"] = groupsb;
            LabelConfirmation.Text = "Please confirm to delete usergroup '" + groupsb.UserGroupId + "'.";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ModalConfirmation", "$(document).ready(function () {$('#ModalConfirmation').modal();});", true);
        }

        protected void ButtonUpdateGroup_Click(object sender, EventArgs e)
        {
            Control button = (Control)sender;
            ListViewDataItem item = (ListViewDataItem)button.BindingContainer;
            List<UserGroup> groups = (List<UserGroup>)Session["List_Group"];
            UserGroup groupsb = groups[item.DataItemIndex];
            Response.Redirect("UserGroupAddUpdate.aspx?UserRoleId=" + groupsb.UserGroupId);
        }

        protected void ButtonSearchGroup_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
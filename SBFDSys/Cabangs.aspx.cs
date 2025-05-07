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
    public partial class Cabangs : System.Web.UI.Page
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
            List<Cabang> das = MySqlRepository.GetAll<Cabang>("NamaCabang like '%" + TextBoxSearch.Text + "%' ");

            foreach (Cabang r in das)
                r.Active = group.GetBoolValue("cabang", "hapus");
            Session["List_Cabang"] = das;
            ListViewData.DataSource = das;
            ListViewData.DataBind();
        }
        protected void ButtonDeleteCabang_Click(object sender, EventArgs e)
        {
            Control button = (Control)sender;
            ListViewDataItem item = (ListViewDataItem)button.BindingContainer;
            List<Cabang> cabangs = (List<Cabang>)Session["List_Cabang"];
            Cabang cabangsb = cabangs[item.DataItemIndex];
            Session["Selected_Cabang"] = cabangsb;
            LabelConfirmation.Text = "Please confirm to delete Cabang '" + cabangsb.NamaCabang + "'.";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ModalConfirmation", "$(document).ready(function () {$('#ModalConfirmation').modal();});", true);
        }
        protected void ButtonUpdateCabang_Click(object sender, EventArgs e)
        {

            Control button = (Control)sender;
            ListViewDataItem item = (ListViewDataItem)button.BindingContainer;
            List<Cabang> cabangs = (List<Cabang>)Session["List_Cabang"];
            Cabang cabang = cabangs[item.DataItemIndex];
            Response.Redirect("CabangAddUpdate.aspx?CabangId=" + cabang.CabangId);
        }
        protected void ButtonOKConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Cabang cbg = (Cabang)Session["Selected_Cabang"];
                MySqlRepository.Delete(cbg);
                LoadData();
            }
            catch (Exception x)
            {
                LabelError.Text = x.Message;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ErrorModal", "$(document).ready(function () {$('#ErrorModal').modal();});", true);

            }
        }
        protected void ListViewData_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            lvDataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            LoadData();
        }
        protected void ButtonSearchCabang_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void ButtonViewData_Click(object sender, EventArgs e)
        {
            Control button = (Control)sender;
            ListViewDataItem item = (ListViewDataItem)button.BindingContainer;
            List<Cabang> das = (List<Cabang>)Session["List_Cabang"];
        }
    }
}
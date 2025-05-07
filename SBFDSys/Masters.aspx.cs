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
    public partial class Masters : System.Web.UI.Page
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
            List<MasterUser> das = MySqlRepository.GetAll<MasterUser>("NamaKaryawan like '%" + TextBoxSearch.Text + "%' ");
            foreach (MasterUser r in das)
                r.Active = group.GetBoolValue("user", "hapus");
            Session["List_Master"] = das;
            ListViewData.DataSource = das;
            ListViewData.DataBind();
        }
        
        protected void ButtonOKConfirm_Click(object sender, EventArgs e)
        {
            try
            {
               
                MasterUser rom = (MasterUser)Session["Selected_Master"];
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
            List<MasterUser> das = (List<MasterUser>)Session["List_Master"];
        }


        protected void ButtonSearchMaster_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void ButtonDeleteMaster_Click(object sender, EventArgs e)
        {
            Control button = (Control)sender;
            ListViewDataItem item = (ListViewDataItem)button.BindingContainer;
            List<MasterUser> masters = (List<MasterUser>)Session["List_Master"];
            MasterUser mastersb = masters[item.DataItemIndex];
            Session["Selected_Master"] = mastersb;
            LabelConfirmation.Text = "Yakin ingin menonaktifkan user '" + mastersb.NamaKaryawan + "'.";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ModalConfirmation", "$(document).ready(function () {$('#ModalConfirmation').modal();});", true);
        }

        protected void ButtonUpdateMaster_Click(object sender, EventArgs e)
        {
            Control button = (Control)sender;
            ListViewDataItem item = (ListViewDataItem)button.BindingContainer;
            List<MasterUser> masters = (List<MasterUser>)Session["List_Master"];
            MasterUser mastersb = masters[item.DataItemIndex];
            Response.Redirect("MasterAddUpdate.aspx?MasterDataId=" + mastersb.MasterUserId);
        }

        protected void ButtonAktifkanMaster_Click(object sender, EventArgs e)
        {
            // Mendapatkan control yang mengirim event
            Control button = (Control)sender;
            ListViewDataItem item = (ListViewDataItem)button.BindingContainer;
            List<MasterUser> masters = (List<MasterUser>)Session["List_Master_TidakAktif"];
            MasterUser selectedMaster = masters[item.DataItemIndex];
            selectedMaster.Active = true;
            Session["Selected_Master"] = selectedMaster;

            // Menampilkan modal konfirmasi menggunakan script manager
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ModalConfirmationAktif", "$(document).ready(function () {$('#ModalConfirmationAktif').modal();});", true);
        }

        protected void ButtonOkAktif_Click(object sender, EventArgs e)
        {
            try
            {

                MasterUser rom = (MasterUser)Session["Selected_Master"];
                rom.Active = true;
                MySqlRepository.Update(rom);
                LoadData();

            }
            catch (Exception x)
            {
                LabelError.Text = x.Message;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ModalError", "$(document).ready(function () {$('#ModalError').modal();});", true);
            }
        }
    }
}
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
    public partial class UserGroupAddUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LoginUser"] == null)
                    Response.Redirect("default.aspx");
                else
                {
                    MasterUser user = (MasterUser)Session["LoginUser"];
                    UserGroup group = MySqlRepository.GetById<UserGroup>(Convert.ToInt32(user.UserGroupId));

                    TombolSimpan.Enabled = group.GetBoolValue("usergroupsetting", "simpan");
                }
                Session["Group"] = null;
                if (Request.QueryString["UserRoleId"] != null)
                {
                    string group_id = Request.QueryString["UserRoleId"];
                    loadUserGroup(group_id);
                }
            }
        }

        private void loadUserGroup(string group_id)
        {

            UserGroup group = MySqlRepository.GetById<UserGroup>(Convert.ToInt32(group_id));
            if (group != null)
            {

                TextBoxUserGroup.Text = group.NamaUserGroup;
                //BLOKIR
                CheckBoxTampilR.Checked = group.GetBoolValue("master", "tampil");
                CheckBoxSimpanR.Checked = group.GetBoolValue("master", "simpan");
                CheckBoxHapusR.Checked = group.GetBoolValue("master", "hapus");
                CheckBoxPrintR.Checked = group.GetBoolValue("master", "print");
                CheckBoxApproveR.Checked = group.GetBoolValue("master", "approve");

                //CABANG
                //CheckBoxTampilCab.Checked = group.GetBoolValue("cabang", "tampil");
                //CheckBoxSimpanCab.Checked = group.GetBoolValue("cabang", "simpan");
                //CheckBoxHapusCab.Checked = group.GetBoolValue("cabang", "hapus");
                //CheckBoxPrintCab.Checked = group.GetBoolValue("cabang", "print");
                //CheckBoxApproveCab.Checked = group.GetBoolValue("cabang", "approve");

                //Restoredb
                CheckBoxTampilRestore.Checked = group.GetBoolValue("restoredb", "tampil");
                CheckBoxSimpanRestore.Checked = group.GetBoolValue("restoredb", "simpan");
                CheckBoxHapusRestore.Checked = group.GetBoolValue("restoredb", "hapus");
                CheckBoxPrintRestore.Checked = group.GetBoolValue("restoredb", "print");
                CheckBoxApproveRestore.Checked = group.GetBoolValue("restoredb", "approve");

                //DEPARTEMEN
                //CheckBoxTampilDep.Checked = group.GetBoolValue("departemen", "tampil");
                //CheckBoxSimpanDep.Checked = group.GetBoolValue("departemen", "simpan");
                //CheckBoxHapusDep.Checked = group.GetBoolValue("departemen", "hapus");
                //CheckBoxPrintDep.Checked = group.GetBoolValue("departemen", "print");
                //CheckBoxApproveDep.Checked = group.GetBoolValue("departemen", "approve");
                //USER
                CheckBoxTampilUser.Checked = group.GetBoolValue("user", "tampil");
                CheckBoxSimpanUser.Checked = group.GetBoolValue("user", "simpan");
                CheckBoxHapusUser.Checked = group.GetBoolValue("user", "hapus");
                CheckBoxPrintUser.Checked = group.GetBoolValue("user", "print");
                CheckBoxApproveUser.Checked = group.GetBoolValue("user", "approve");
                //USER GROUP
                CheckBoxTampilUserGS.Checked = group.GetBoolValue("usergroupsetting", "tampil");
                CheckBoxSimpanUserGS.Checked = group.GetBoolValue("usergroupsetting", "simpan");
                CheckBoxHapusUserGS.Checked = group.GetBoolValue("usergroupsetting", "hapus");
                CheckBoxPrintUserGS.Checked = group.GetBoolValue("usergroupsetting", "print");
                CheckBoxApproveUserGS.Checked = group.GetBoolValue("usergroupsetting", "approve");
                
                
                //LAPORAN
                //CheckBoxTampilLap.Checked = group.GetBoolValue("laporan", "tampil");
                //CheckBoxSimpanLap.Checked = group.GetBoolValue("laporan", "simpan");
                //CheckBoxHapusLap.Checked = group.GetBoolValue("laporan", "hapus");
                //CheckBoxPrintLap.Checked = group.GetBoolValue("laporan", "print");
                //CheckBoxApproveLap.Checked = group.GetBoolValue("laporan", "approve");
            }
            Session["Group"] = group;
        }

        protected void TombolSimpan_Click(object sender, EventArgs e)
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            MySqlRepository.Insert(new Log() { Keterangan = "TAMBAH ATAU UBAH USER GROUP", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username });
            try
            {
                if (TextBoxUserGroup.Text == "")
                    throw new Exception("Nama userGroup tidak boleh kosong");

                UserGroup group = (UserGroup)Session["Group"];
                if (group == null)
                    group = new UserGroup();
                group.NamaUserGroup = TextBoxUserGroup.Text;
                group.Master = group.ConvertToString(CheckBoxTampilR.Checked, CheckBoxSimpanR.Checked, CheckBoxHapusR.Checked, CheckBoxPrintR.Checked, CheckBoxApproveR.Checked);
                //group.Cabang = group.ConvertToString(CheckBoxTampilCab.Checked, CheckBoxSimpanCab.Checked, CheckBoxHapusCab.Checked, CheckBoxPrintCab.Checked, CheckBoxApproveCab.Checked);
                group.RestoreDB = group.ConvertToString(CheckBoxTampilRestore.Checked, CheckBoxSimpanRestore.Checked, CheckBoxHapusRestore.Checked, CheckBoxPrintRestore.Checked, CheckBoxApproveRestore.Checked);

                //group.Departemen = group.ConvertToString(CheckBoxTampilDep.Checked, CheckBoxSimpanDep.Checked, CheckBoxHapusDep.Checked, CheckBoxPrintDep.Checked, CheckBoxApproveDep.Checked);

                group.User = group.ConvertToString(CheckBoxTampilUser.Checked, CheckBoxSimpanUser.Checked, CheckBoxHapusUser.Checked, CheckBoxPrintUser.Checked, CheckBoxApproveUser.Checked);
                group.UserGroupSetting = group.ConvertToString(CheckBoxTampilUserGS.Checked, CheckBoxSimpanUserGS.Checked, CheckBoxHapusUserGS.Checked, CheckBoxPrintUserGS.Checked, CheckBoxApproveUserGS.Checked);
                

                //group.Laporan = group.ConvertToString(CheckBoxTampilLap.Checked, CheckBoxSimpanLap.Checked, CheckBoxHapusLap.Checked, CheckBoxPrintLap.Checked, CheckBoxApproveLap.Checked);


                group.Active = true;
                MySqlRepository.InsertUpdate(group);
                Session["Group"] = null;
                Response.Redirect("UserGroups.aspx");
            }
            catch (Exception x)
            {
                LabelError.Text = x.Message;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ErrorModal", "$(document).ready(function () {$('#ErrorModal').modal();});", true);
            }

        }
    }
}
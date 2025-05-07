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
    public partial class MasterAddUpdate : System.Web.UI.Page
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

                    TombolSimpan.Enabled = group.GetBoolValue("User", "simpan");
                }
                loadCabang();
                loadDept();
                loadUserGroup();
                Session["MasterDataId"] = null;
                if (Request.QueryString["MasterDataId"] != null)
                {
                    string master_id = Request.QueryString["MasterDataId"];
                    loadMaster(master_id);
                }
            }

        }
        private void loadUserGroup()
        {
            List<UserGroup> ug = MySqlRepository.GetAll<UserGroup>("");
            DropDownListUserGroup.DataValueField = "UserGroupId";
            DropDownListUserGroup.DataTextField = "NamaUserGroup";
            DropDownListUserGroup.DataSource = ug;
            DropDownListUserGroup.DataBind();
        }
        private void loadCabang()
        {
            List<Cabang> cab = MySqlRepository.GetAll<Cabang>("");
            DropDownListCabang.DataValueField = "CabangId";
            DropDownListCabang.DataTextField = "NamaCabang";
            DropDownListCabang.DataSource = cab;
            DropDownListCabang.DataBind();
        }
        private void loadDept()
        {
            List<Departemen> dep = MySqlRepository.GetAll<Departemen>("");
            DropDownListDept.DataValueField = "DepartemenId";
            DropDownListDept.DataTextField = "NamaDepartemen";
            DropDownListDept.DataSource = dep;
            DropDownListDept.DataBind();
        }
        private void loadMaster(string master_id)
        {
            MasterUser master = MySqlRepository.GetById<MasterUser>(Convert.ToInt32(master_id));
            if (master != null)
            {
                TextBoxNik.Text = master.Nik;
                TextBoxNamaKar.Text = master.NamaKaryawan;
                TextBoxUser.Text = master.Username;
                TextBoxPass.Text = master.Password;
                TextBoxEmail.Text = master.Email;
                TextBoxNoHp.Text = master.NoHp;
                DropDownListDept.SelectedItem.Text = master.Departemen;
                DropDownListCabang.SelectedItem.Text = master.Cabang;
                DropDownListUserGroup.SelectedItem.Text = master.NamaUserGroup;
            }
            Session["MasterDataId"] = master;
        }
        protected void TombolSimpan_Click(object sender, EventArgs e)
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            MySqlRepository.Insert(new Log() { Keterangan = "TAMBAH ATAU UBAH USER", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username, Nik = user.Nik, NamaKaryawan = user.NamaKaryawan });
            try
            {

                if (TextBoxNik.Text == "")
                    throw new Exception("NIK tidak boleh kosong");
                if (TextBoxNamaKar.Text == "")
                    throw new Exception("Nama tidak boleh kosong");

                MasterUser master = (MasterUser)Session["MasterDataId"];
                //MasterUser user = (MasterUser)Session["LoginUser"];
                if (master == null)
                {
                    List<MasterUser> existingUser = MySqlRepository.GetAllBySql<MasterUser>("select Nik from MasterUser where Nik like '%" + TextBoxNik.Text + "%'");

                    if (existingUser.Count > 0)
                        throw new Exception("NIP sudah terdaftar.");

                    master = new MasterUser();
                    master.Nik = TextBoxNik.Text;

                }
                else
                {
                    if (master.Nik != TextBoxNik.Text)
                    {
                        List<MasterUser> existingUser = MySqlRepository.GetAllBySql<MasterUser>("select Nik from MasterUser where Nik like '%" + TextBoxNik.Text + "%'");
                        if (existingUser.Count > 0)
                            throw new Exception("NIP sudah terdaftar.");
                        master.Nik = TextBoxNik.Text;
                    }
                }

                master.NamaKaryawan = TextBoxNamaKar.Text;
                master.Username = TextBoxUser.Text;
                string message = "";
                if (TextBoxPass.Text.Length > 8)
                    message += "* Password Min 8 Digit. ";
                if (TextBoxPass.Text.Length < 8)
                    message += "* Password Max 8 Digit. ";
                if (!TextBoxPass.Text.Any(char.IsUpper))
                    message += "Password Harus Memiliki Minimal 1 Karakter Huruf Kapital.";
                if (!TextBoxPass.Text.Any(char.IsLower))
                    message += "Password Harus Memiliki Karakter Huruf Kecil.";
                master.Password = Tools.MD5Hash(TextBoxPass.Text.Trim());
                if (message.Length > 0)
                    throw new Exception(message);
                master.Email = TextBoxEmail.Text;
                master.NoHp = TextBoxNoHp.Text;
                master.Departemen = DropDownListDept.SelectedItem.Text;
                master.Cabang = DropDownListCabang.SelectedItem.Text;
                master.NamaUserGroup = DropDownListUserGroup.SelectedItem.Text;
                master.UserGroupId = Convert.ToInt32(DropDownListUserGroup.SelectedItem.Value);
                master.Active = true;

                MySqlRepository.InsertUpdate(master);

                Session["MasterDataId"] = null;

                //MasterUser user = (MasterUser)Session["LoginUser"];
                if (user.MasterUserId == master.MasterUserId)
                    Session["LoginUser"] = master;

                Response.Redirect("Masters.aspx");
            }
            catch (Exception x)
            {
                LabelError.Text = x.Message;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ErrorModal", "$(document).ready(function () {$('#ErrorModal').modal();});", true);
            }

        }
    }
}

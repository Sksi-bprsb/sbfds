using System;
using System.Web.UI;
using System.Net;
using System.Net.Mail;
using BPRSB.Kuesioner.Repository;
using FDS.Library.Model;
using System.Linq;
using Npgsql;

namespace SBFDSys
{
    public partial class _default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Session["LoginUser"] = null;
        }
        protected void ButtonLogin_Click(object sender, EventArgs e)
        {

            try
            {
                Session["LoginUser"] = null;
                string loginid = TextBoxLoginId.Text.Trim();
                string passwordHash = Tools.MD5Hash(TextBoxPassword.Text.Trim());
                //MySqlRepository.Insert(new Log() { Keterangan = "USER TRY LOGIN..", Tanggal = DateTime.Now, MasterUserId=0, Username = loginid,Nik="0",NamaKaryawan="0" });

                MasterUser user = MySqlRepository.GetTopOne<MasterUser>("Nik='" + loginid + "' and Password='" + passwordHash + "'");

                if (user == null)
                {
                    LabelError.Text = "No Karyawan atau Password Anda Salah";
                    MySqlRepository.Insert(new Log() { Keterangan = "USER FAILED TO LOGIN..", Tanggal = DateTime.Now, MasterUserId = 0, Username = loginid + LabelError.Text,Nik=loginid,NamaKaryawan="N/A" });
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ErrorModal", "$(document).ready(function () {$('#ErrorModal').modal();});", true);
                    return;
                }

                if (!user.Active)
                {
                    LabelError.Text = "No Karyawan tidak aktif";
                    MySqlRepository.Insert(new Log() { Keterangan = "USER FAILED TO LOGIN..", Tanggal = DateTime.Now, MasterUserId = 0, Username = loginid + LabelError.Text, Nik = user.Nik, NamaKaryawan = user.NamaKaryawan });
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ErrorModal", "$(document).ready(function () {$('#ErrorModal').modal();});", true);
                    return;
                }

                Session["LoginUser"] = user;
                if (passwordHash == "e64b78fc3bc91bcbc7dc232ba8ec59e0")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ModalGantiPassword", "$(document).ready(function () {$('#ModalGantiPassword').modal();});", true);
                }
                else
                {
                    if (user.Active)
                    {
                        MySqlRepository.Insert(new Log() { Keterangan = "USER LOGIN SUCCESSFULY", Tanggal = DateTime.Now, Username = user.Username, Nik = user.Nik, NamaKaryawan = user.NamaKaryawan });
                        Response.Redirect("Dashboards.aspx");
                    }
                    else
                    {
                        Response.Redirect("../Default.aspx");
                        LabelError.Text = "No Karyawan tidak aktif";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ErrorModal", "$(document).ready(function () {$('#ErrorModal').modal();});", true);

                    }
                }
            }
            catch (Exception x)
            {
                LabelError.Text = x.Message;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ErrorModal", "$(document).ready(function () {$('#ErrorModal').modal();});", true);
            }

        }

        protected void LinkButtonLupaPassword_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ErrorModal", "$(document).ready(function () {$('#ModalLupaPassword').modal();});", true);
        }

        protected void ButtonSendLupaPassword_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonUbahPassword_Click(object sender, EventArgs e)
        {

        }
    }
}
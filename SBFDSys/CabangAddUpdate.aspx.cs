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
    public partial class CabangAddUpdate : System.Web.UI.Page
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

                    TombolSimpan.Enabled = group.GetBoolValue("Cabang", "simpan");
                }
                if (Request.QueryString["CabangId"] != null)
                {
                    string cabang_id = Request.QueryString["CabangId"];
                    loadCabang(cabang_id);
                }
            }
        }

        private void loadCabang(string cabang_id)
        {
            Cabang cabang = MySqlRepository.GetById<Cabang>(Convert.ToInt32(cabang_id));
            if (cabang != null)
            {
                TextBoxKodeCabang.Text = cabang.KodeCabang;
                TextBoxNamaCabang.Text = cabang.NamaCabang;
            }
            Session["Cabang"] = cabang;
        }

        protected void TombolSimpan_Click(object sender, EventArgs e)
        {
            
                try
                {
                    if (TextBoxKodeCabang.Text == "")
                        throw new Exception("Kode Cabang cannot be blank");
                    if (TextBoxNamaCabang.Text == "")
                        throw new Exception("Nama Cabang cannot be blank");
                    Cabang cabang = (Cabang)Session["Cabang"];
                    if (cabang == null)
                        cabang = new Cabang();
                    cabang.KodeCabang = TextBoxKodeCabang.Text;
                    cabang.NamaCabang = TextBoxNamaCabang.Text;

                    cabang.Active = true;
                    MySqlRepository.InsertUpdate(cabang);
                    Session["Cabang"] = null;
                    Response.Redirect("Cabangs.aspx");
                }
                catch (Exception x)
                {
                    LabelError.Text = x.Message;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ErrorModal", "$(document).ready(function () {$('#ErrorModal').modal();});", true);
                }
            
        }
    }
}
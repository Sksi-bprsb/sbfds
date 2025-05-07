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
    public partial class DepartemenAddUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LoginUser"] == null)
                    Response.Redirect("default.aspx");
                Session["Departemen"] = null;
                if (Request.QueryString["DepartemenId"] != null)
                {
                    string departemen_id = Request.QueryString["DepartemenId"];
                    loadDepartemen(departemen_id);
                }
            }
        }

        private void loadDepartemen(string departemen_id)
        {
            Departemen departemen = MySqlRepository.GetById<Departemen>(Convert.ToInt32(departemen_id));
            if (departemen != null)
            {
                TextBoxKodeDepartemen.Text = departemen.KodeDepartemen;
                TextBoxNamaDepartemen.Text = departemen.NamaDepartemen;
            }
            Session["Departemen"] = departemen;
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBoxKodeDepartemen.Text == "")
                    throw new Exception("Kode Departemen cannot be blank");
                if (TextBoxNamaDepartemen.Text == "")
                    throw new Exception("Nama Departemen cannot be blank");
                Departemen departemen = (Departemen)Session["Departemen"];
                if (departemen == null)
                    departemen = new Departemen();
                departemen.KodeDepartemen = TextBoxKodeDepartemen.Text;
                departemen.NamaDepartemen = TextBoxNamaDepartemen.Text;
              
                departemen.Active = true;
                MySqlRepository.InsertUpdate(departemen);
                Session["Departemen"] = null;

                Response.Redirect("Departemens.aspx");
            }
            catch (Exception x)
            {
                LabelError.Text = x.Message;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ErrorModal", "$(document).ready(function () {$('#ErrorModal').modal();});", true);
            }
        }
    }
}
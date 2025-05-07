using FDS.Library.Model;
using SBFDSys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BPRSB.Kuesioner.Repository;
using System.Data;
using System.Drawing;
using System.IO;
using System.Collections;
using System.Text;
using Npgsql; // Corrected the namespace for PostgreSQL
using System.EnterpriseServices.CompensatingResourceManager;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Threading.Tasks;
using System.Net;

namespace SBFDSys
{
    public partial class ApprovalParameter : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
        }

        private void LoadData()
        {
            string id = Request["ParameterPendingId"];
            if(id!=null)
            {
                ParameterPending pending = MySqlRepository.GetById<ParameterPending>(Convert.ToInt32(id));
                LabelNamaParameter.Text = pending.Nama;
                LabelValue1.Text = pending.Value1;
                LabelValue2.Text = pending.Value2; 
                LabelValue3.Text = pending.Value3;
                LabelStatus.Text = pending.Status;
                Session["subject"] = pending;
                buttonSetuju.Visible = pending.Status == "PENDING";
                buttonTolak.Visible = pending.Status == "PENDING";
            }
        }

        protected void ButtonApprovalParameter_Click(object sender, EventArgs e)
        {

        }

        protected void buttonSetuju_Click(object sender, EventArgs e)
        {
            MasterUser user = (MasterUser)Session["LoginUser"];

            ParameterPending p = (ParameterPending)Session["subject"];
            p.Status = "APPROVED";
            MySqlRepository.Update(p);
            FDS.Library.Model.Parameter param = MySqlRepository.GetTopOne<FDS.Library.Model.Parameter>($"Nama='{p.Nama}'");
            param.Nama = p.Nama;
            param.Value1 = p.Value1;
            param.Value2 = p.Value2;
            param.Value3 = p.Value3;
            param.Status = p.Status;
            MySqlRepository.Update(param);
            MySqlRepository.Insert(new Log() { Keterangan = $"MELAKUKAN APPROVAL PARAMETER {param.Nama} ", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username, Nik = user.Nik, NamaKaryawan = user.NamaKaryawan });

            Response.Redirect("ApprovalViewParameter.aspx");
        }

        protected void buttonTolak_Click(object sender, EventArgs e)
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            MySqlRepository.Insert(new Log() { Keterangan = "TOLAK APPROVAL PARAMETER", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username , Nik = user.Nik, NamaKaryawan = user.NamaKaryawan });

            ParameterPending p = (ParameterPending)Session["subject"];
            p.Status = "REJECTED";
            MySqlRepository.Update(p);
            Response.Redirect("ApprovalViewParameter.aspx");
        }
    }
}

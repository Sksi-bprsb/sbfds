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
    public partial class ApprovalViewParameter : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                loadListPendingApproval();
        }

        private void loadListPendingApproval()
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            List<ParameterPending> pendings = MySqlRepository.GetAll<ParameterPending>($"Status='PENDING' and ApprovalNama='{user.NamaKaryawan}'");
            Session["ParameterPendings"] = pendings;
            ListViewData.DataSource = pendings;
            ListViewData.DataBind();
        }
       
        protected void ButtonApprovalParameter_Click(object sender, EventArgs e)
        {
            Control button = (Control)sender;
            ListViewDataItem item = (ListViewDataItem)button.BindingContainer;
            List<ParameterPending> pendings = (List<ParameterPending>)Session["ParameterPendings"];
            ParameterPending pending = pendings[item.DataItemIndex];
            Response.Redirect(@"ApprovalParameter.aspx?ParameterPendingId=" + pending.ParameterPendingId);
           
        }
    }
}

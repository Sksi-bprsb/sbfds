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
    public partial class Logs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Memastikan pengguna sudah login
            if (Session["LoginUser"] == null)
            {
                Response.Redirect("default.aspx");
                return;
            }

            // Memuat data hanya pada halaman pertama kali diakses
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        public void LoadData()
        {
            try
            {
                // Ambil data dari tabel Log tanpa filter
                List<FDS.Library.Model.Log> logs = MySqlRepository.GetAllBySql<FDS.Library.Model.Log>("Select MasterUserId,Nik,NamaKaryawan,Username,Tanggal,Keterangan from log order by Tanggal Desc");

                // Ikat data ke ListView
                ListViewData.DataSource = logs;
                ListViewData.DataBind();
            }
            catch (Exception ex)
            {
                // Logging error jika terjadi masalah
                System.Diagnostics.Debug.WriteLine($"Error loading data: {ex.Message}");
            }
        }

        protected void ListViewData_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            // Mengatur properti paging
            lvDataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);

            // Memuat ulang data
            LoadData();
        }

        protected void ButtonSearchMaster_Click(object sender, EventArgs e)
        {
            // Memuat ulang data saat tombol pencarian ditekan
            LoadData();
        }
    }


}
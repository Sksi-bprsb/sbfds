using System;
using Npgsql;
using BPRSB.Kuesioner.Repository;
using FDS.Library.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SBFDSys
{
    public partial class SalahPins : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUser"] == null)
                Response.Redirect("default.aspx");
            else if (!IsPostBack)
            {
                string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
                List<SalahPin> result = new List<SalahPin>();
                FDS.Library.Model.Parameter prSALAH_PASSWORD = MySqlRepository.GetTopOne<FDS.Library.Model.Parameter>("Nama='SALAH_PASSWORD'");

                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand($@"select c.cif_number, c.customer_name, c.customer_username, a.activity_type, a.activity_data, a.created, c.ib_status, c.mb_status FROM ebanking.t_activity_customer as a left join ebanking.m_customer as c on a.createdby = c.id where a.activity_type in ('LE', '1E') and a.activity_data > '{prSALAH_PASSWORD.Value1}' ORDER BY created ASC", conn))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SalahPin d = new SalahPin();
                                
                                d.customer_name= reader["customer_name"].ToString();
                                d.cif_number = reader["cif_number"].ToString();
                                d.customer_username = reader["customer_username"].ToString();
                                d.activity_type = reader["activity_type"].ToString();
                                d.activity_data = reader["activity_data"].ToString();
                                d.created = reader["created"].ToString();
                                d.ib_status = reader["ib_status"].ToString();
                                d.mb_status = reader["mb_status"].ToString();

                                result.Add(d);
                            }
                        }
                    }
                }
                ListViewData.DataSource = result;
                ListViewData.DataBind();

            }
        }

        protected void ButtonSearchTransaksi_Click(object sender, EventArgs e)
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            MySqlRepository.Insert(new Log() { Keterangan = "AKSES PENCARIAN MENU SALAH PIN ", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username,Nik = user.Nik, NamaKaryawan = user.NamaKaryawan });
            // Inisialisasi variabel tanggal dengan nilai default
            DateTime start;
            DateTime end = DateTime.MinValue;

            // Validasi input tanggal
            bool isDateRangeValid = DateTime.TryParse(TextBoxTanggalMulai.Text, out start) &&
                                    DateTime.TryParse(TextBoxTanggalAkhir.Text, out end);

            string customerName = TextBoxSearch.Text.Trim();
            FDS.Library.Model.Parameter prSALAH_PASSWORD = MySqlRepository.GetTopOne<FDS.Library.Model.Parameter>("Nama='SALAH_PASSWORD'");

            string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
            List<SalahPin> result = new List<SalahPin>();

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Query SQL
                string query = $@"select c.cif_number, c.customer_name, c.customer_username, a.activity_type, a.activity_data, a.created, c.ib_status, c.mb_status 
                FROM ebanking.t_activity_customer as a left join ebanking.m_customer as c on a.createdby = c.id 
                where a.activity_type in ('LE', '1E') and a.activity_data > '{prSALAH_PASSWORD.Value1}'";

                // Tambahkan kondisi berdasarkan input tanggal (jika valid)
                if (isDateRangeValid)
                {
                    query += " AND a.created::date BETWEEN @StartDate AND @EndDate ";
                }

                if (!string.IsNullOrEmpty(customerName))
                {
                    query += " AND LOWER(c.customer_name) LIKE LOWER(@CustomerName) ";
                }

                query += " ORDER BY a.created ASC";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    // Set parameter untuk rentang tanggal (jika valid)
                    if (isDateRangeValid)
                    {
                        cmd.Parameters.AddWithValue("@StartDate", NpgsqlTypes.NpgsqlDbType.Date, start);
                        cmd.Parameters.AddWithValue("@EndDate", NpgsqlTypes.NpgsqlDbType.Date, end);
                    }

                    if (!string.IsNullOrEmpty(customerName))
                    {
                        cmd.Parameters.AddWithValue("@CustomerName", "%" + customerName + "%");
                    }
                    // Eksekusi query dan baca hasilnya
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SalahPin d = new SalahPin();
                            d.customer_name = reader["customer_name"].ToString();
                            d.cif_number = reader["cif_number"].ToString();
                            d.customer_username = reader["customer_username"].ToString();
                            d.activity_type = reader["activity_type"].ToString();
                            d.activity_data = reader["activity_data"].ToString();
                            d.created = reader["created"].ToString();
                            d.ib_status = reader["ib_status"].ToString();
                            d.mb_status = reader["mb_status"].ToString();

                            result.Add(d);
                        }
                    }
                }
            }

            // Bind hasil ke ListView
            ListViewData.DataSource = result;
            ListViewData.DataBind();
        }


        protected void ListViewData_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {

        }

        protected void ButtonOKConfirm_Click(object sender, EventArgs e)
        {

        }
    }
}
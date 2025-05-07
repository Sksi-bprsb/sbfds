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
    public partial class AktifitasBerulangLoginSuksess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUser"] == null)
                Response.Redirect("default.aspx");
            else if (!IsPostBack)
            {
                string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
                List<AktifitasBerulangLoginSukses> result = new List<AktifitasBerulangLoginSukses>();
                FDS.Library.Model.Parameter prAKTIFITAS_BERULANG_LOGIN_SUKSES = MySqlRepository.GetTopOne<FDS.Library.Model.Parameter>("Nama='AKTIFITAS_BERULANG_LOGIN_SUKSES'");

                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand($@"select * from(select c.cif_number, c.customer_name, c.customer_username, t.activity_type, t.created, count(t.activity_type) 
                                    OVER(PARTITION BY c.cif_number, DATE(t.created)) AS Login_per_hari 
                                    FROM ebanking.t_activity_customer as t left join ebanking.m_customer as c on t.createdby = c.id 
                                    where t.activity_type = 'LS' ) AS subquery where Login_per_hari > {prAKTIFITAS_BERULANG_LOGIN_SUKSES.Value1} ORDER BY cif_number, created asc;", conn))
                    {

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AktifitasBerulangLoginSukses d = new AktifitasBerulangLoginSukses();

                                d.customer_name = reader["customer_name"].ToString();
                                d.customer_username = reader["customer_username"].ToString();
                                d.cif_number = reader["cif_number"].ToString();
                                d.activity_type = reader["activity_type"].ToString();
                                d.created = reader["created"].ToString();
                                d.Login_per_hari = reader["Login_per_hari"].ToString();

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
            MySqlRepository.Insert(new Log() { Keterangan = "AKSES PENCARIAN MENU BERULANG LOGIN SUKSES ", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username,Nik=user.Nik,NamaKaryawan=user.NamaKaryawan });
            // Inisialisasi variabel tanggal dengan nilai default
            DateTime start;
            DateTime end = DateTime.MinValue;

            // Validasi input tanggal
            bool isDateRangeValid = DateTime.TryParse(TextBoxTanggalMulai.Text, out start) &&
                                    DateTime.TryParse(TextBoxTanggalAkhir.Text, out end);

            string customerName = TextBoxSearch.Text.Trim();

            string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
            List<AktifitasBerulangLoginSukses> result = new List<AktifitasBerulangLoginSukses>();

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Query SQL
                string query = @"select* from(select c.cif_number, c.customer_name, c.customer_username, t.activity_type, t.created, count(t.activity_type) OVER(PARTITION BY c.cif_number, DATE(t.created)) AS Login_per_hari FROM ebanking.t_activity_customer as t left join ebanking.m_customer as c on t.createdby = c.id where t.activity_type = 'LS' ) AS subquery where Login_per_hari > 4 ";

                // Tambahkan kondisi berdasarkan input tanggal (jika valid)
                if (isDateRangeValid)
                {
                    query += " AND created::date BETWEEN @StartDate AND @EndDate ";
                }

                //if (!string.IsNullOrEmpty(customerName))
                //{
                //    query += " AND LOWER(c.customer_name) LIKE LOWER(@CustomerName) ";
                //}

                query += " ORDER BY cif_number, created asc";

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
                            AktifitasBerulangLoginSukses d = new AktifitasBerulangLoginSukses();
                            d.customer_name = reader["customer_name"].ToString();
                            d.customer_username = reader["customer_username"].ToString();
                            d.cif_number = reader["cif_number"].ToString();
                            d.activity_type = reader["activity_type"].ToString();
                            d.created = reader["created"].ToString();
                            d.Login_per_hari = reader["Login_per_hari"].ToString();

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
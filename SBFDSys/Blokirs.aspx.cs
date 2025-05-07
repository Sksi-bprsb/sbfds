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
    public partial class Blokirs : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUser"] == null)
                Response.Redirect("default.aspx");
            else if (!IsPostBack)
            {
                string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
                List<Blokir> result = new List<Blokir>();
                

                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select c.cif_number, c.customer_name, c.customer_username, c.ib_status, c.mb_status, a.activity_type, a.created, " +
                        "CASE WHEN a.delivery_channel = '11' THEN 'Android' " +
                        "WHEN a.delivery_channel = '12' THEN 'iOS' " +
                        "ELSE 'Internet Banking' " +
                        "END AS delivery_channel " +
                        "FROM ebanking.t_activity_customer as a " +
                        "left join ebanking.m_customer as c on a.createdby = c.id " +
                        "where(c.ib_status = 'B' or c.mb_status = 'B') and (a.activity_type = 'LS' or a.activity_type= 'LE') " +
                        "group by c.cif_number, c.customer_name, c.customer_username, c.ib_status, c.mb_status, c.last_login, a.activity_type, a.delivery_channel, a.created " +
                        "ORDER BY c.last_login ASC, c.cif_number", conn))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Blokir d = new Blokir();
                                
                                d.customer_name= reader["customer_name"].ToString();
                                d.cif_number = reader["cif_number"].ToString();
                                d.customer_username = reader["customer_username"].ToString();
                                d.ib_status = reader["ib_status"].ToString();
                                d.mb_status = reader["mb_status"].ToString();
                                d.activity_type = reader["activity_type"].ToString();
                                d.created = reader["created"].ToString();
                                d.delivery_channel = reader["delivery_channel"].ToString();

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
            MySqlRepository.Insert(new Log() { Keterangan = "AKSES PENCARIAN MENU BLOKIR ", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username,Nik=user.Nik,NamaKaryawan=user.NamaKaryawan });
            DateTime start = DateTime.MinValue;
            DateTime end = DateTime.MinValue;

            bool isDateRangeValid = DateTime.TryParse(TextBoxTanggalMulai.Text, out start) &&
                                    DateTime.TryParse(TextBoxTanggalAkhir.Text, out end);
            string customerName = TextBoxSearch.Text.Trim();

            string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
            List<Blokir> result = new List<Blokir>();
            FDS.Library.Model.Parameter prLOGIN_USER_BLOKIR = MySqlRepository.GetTopOne<FDS.Library.Model.Parameter>("Nama='LOGIN_USER_BLOKIR'");

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                // Query SQL
                string query = $@"
                select c.cif_number, c.customer_name, c.customer_username, c.ib_status, c.mb_status, a.activity_type, a.created, 
                CASE 
                        WHEN a.delivery_channel = '11' THEN 'Android'
                        WHEN a.delivery_channel = '12' THEN 'iOS'
                        ELSE 'Internet Banking' -- Jika ada kode selain 11 atau 12
                    END AS delivery_channel
                FROM ebanking.t_activity_customer as a
                left join ebanking.m_customer as c on a.createdby = c.id
                where (c.ib_status = '{prLOGIN_USER_BLOKIR.Value1}' or c.mb_status = '{prLOGIN_USER_BLOKIR.Value1}') and (a.activity_type = 'LS' or a.activity_type= 'LE') ";

                // Tambahkan kondisi berdasarkan input tanggal (jika valid)
                if (isDateRangeValid)
                {
                    query += " AND a.created::date BETWEEN @StartDate AND @EndDate ";
                }

                // Tambahkan kondisi berdasarkan customer_name (jika diisi)
                if (!string.IsNullOrEmpty(customerName))
                {
                    query += " AND LOWER(c.customer_name) LIKE LOWER(@CustomerName) ";
                }

                query += @"GROUP BY c.cif_number, c.customer_name, c.customer_username, 
                        c.ib_status, c.mb_status, c.last_login, a.activity_type, a.delivery_channel, a.created 
                        ORDER BY c.last_login ASC, c.cif_number";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    // Set parameter untuk rentang tanggal
                    if (isDateRangeValid)
                    {
                        cmd.Parameters.AddWithValue("@StartDate", NpgsqlTypes.NpgsqlDbType.Date, start);
                        cmd.Parameters.AddWithValue("@EndDate", NpgsqlTypes.NpgsqlDbType.Date, end);
                    }

                    // Set parameter untuk customer_name (jika diisi)
                    if (!string.IsNullOrEmpty(customerName))
                    {
                        cmd.Parameters.AddWithValue("@CustomerName", "%" + customerName + "%");
                    }

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Blokir d = new Blokir();

                            d.customer_name = reader["customer_name"].ToString();
                            d.cif_number = reader["cif_number"].ToString();
                            d.customer_username = reader["customer_username"].ToString();
                            d.ib_status = reader["ib_status"].ToString();
                            d.mb_status = reader["mb_status"].ToString();
                            d.activity_type = reader["activity_type"].ToString();
                            d.created = reader["created"].ToString();
                            d.delivery_channel = reader["delivery_channel"].ToString();
                            //d.created = reader["tanggal_login_sukses_sebelumnya"].ToString();

                            result.Add(d);
                        }
                    }
                }
            }
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
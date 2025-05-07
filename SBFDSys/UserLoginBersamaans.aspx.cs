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
    public partial class UserLoginBersamaans : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUser"] == null)
                Response.Redirect("default.aspx");
            else if (!IsPostBack)
            {
                string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
                List<UserLoginBersamaan> result = new List<UserLoginBersamaan>();
                FDS.Library.Model.Parameter prLOGIN_BERSAMAAN_CHANNEL_BERBEDA = MySqlRepository.GetTopOne<FDS.Library.Model.Parameter>("Nama='LOGIN_BERSAMAAN_CHANNEL_BERBEDA'");

                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand($@"WITH login_success AS (SELECT c.cif_number, c.customer_name, c.customer_username, a.activity_type, 
                    CASE WHEN a.delivery_channel = '11' THEN 'Android' WHEN a.delivery_channel = '12' THEN 'iOS' 
                    ELSE 'Internet Banking' END AS delivery_channel, a.created AS login_time, a.createdby, c.id from ebanking.t_activity_customer as a join ebanking.m_customer as c on a.createdby = c.id 
                    where a.activity_type = 'LS') select a1.cif_number, a1.customer_name, a1.delivery_channel 
                    AS first_channel, a1.login_time 
                    AS first_login, a2.delivery_channel 
                    AS second_channel, a2.login_time 
                    AS second_login, date(a1.login_time) 
                    FROM login_success a1 JOIN login_success a2 on a1.createdby = a2.id AND a1.delivery_channel<> a2.delivery_channel 
                    AND a2.login_time > a1.login_time 
                    AND a2.login_time <= a1.login_time + INTERVAL '{prLOGIN_BERSAMAAN_CHANNEL_BERBEDA.Value1} minutes' 
                    ORDER BY a1.cif_number, a1.login_time; ", conn))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserLoginBersamaan d = new UserLoginBersamaan();
                                d.cif_number = reader["cif_number"].ToString();
                                d.customer_name= reader["customer_name"].ToString();
                                d.first_channel = reader["first_channel"].ToString();
                                d.first_login = reader["first_login"].ToString();
                                d.second_channel = reader["second_channel"].ToString();
                                d.second_login = reader["second_login"].ToString();
                                d.date = reader["date"].ToString();

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
            DateTime start = DateTime.MinValue;

            // Validasi tanggal mulai saja
            bool isStartDateValid = DateTime.TryParse(TextBoxTanggalMulai.Text, out start);
            string cifNumber = TextBoxSearch.Text.Trim();

            string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
            List<UserLoginBersamaan> result = new List<UserLoginBersamaan>();
            FDS.Library.Model.Parameter prLOGIN_BERSAMAAN_CHANNEL_BERBEDA = MySqlRepository.GetTopOne<FDS.Library.Model.Parameter>("Nama='LOGIN_BERSAMAAN_CHANNEL_BERBEDA'");

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                // Query SQL
                string query = $@"
                 WITH login_success AS (
                     SELECT c.cif_number, c.customer_name, c.customer_username, a.activity_type, 
                            CASE WHEN a.delivery_channel = '11' THEN 'Android' 
                                 WHEN a.delivery_channel = '12' THEN 'iOS' 
                                 ELSE 'Internet Banking' END AS delivery_channel, 
                            a.created AS login_time, 
                            a.createdby, c.id 
                     FROM ebanking.t_activity_customer AS a 
                     JOIN ebanking.m_customer AS c ON a.createdby = c.id 
                     WHERE a.activity_type = 'LS'
                 ) 
                 SELECT a1.cif_number, 
                        a1.customer_name, 
                        a1.delivery_channel AS first_channel, 
                        a2.delivery_channel AS second_channel, 
                        a1.login_time AS first_login, 
                        a2.login_time AS second_login, 
                        DATE(a1.login_time) AS date
                 FROM login_success a1 
                 JOIN login_success a2 ON a1.createdby = a2.createdby 
                                     AND a1.delivery_channel <> a2.delivery_channel 
                                     AND a2.login_time > a1.login_time 
                                     AND DATE(a1.login_time) = DATE(a2.login_time) 
                                     AND EXTRACT(EPOCH FROM (a2.login_time - a1.login_time)) / 60 <= {prLOGIN_BERSAMAAN_CHANNEL_BERBEDA.Value1}
                                     AND EXTRACT(EPOCH FROM (a2.login_time - a1.login_time)) / 60 > 0 ";

                // Tambahkan kondisi berdasarkan input tanggal mulai (jika valid)
                if (isStartDateValid)
                {
                    query += " AND DATE(a1.login_time) = @StartDate ";
                }

                // Tambahkan kondisi berdasarkan cif_number (jika diisi)
                if (!string.IsNullOrEmpty(cifNumber))
                {
                    query += " AND a1.cif_number = @CifNumber ";
                }

                query += " ORDER BY a1.cif_number, a1.login_time;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    // Set parameter untuk tanggal mulai
                    if (isStartDateValid)
                    {
                        cmd.Parameters.AddWithValue("@StartDate", NpgsqlTypes.NpgsqlDbType.Date, start);
                    }

                    // Set parameter untuk cif_number (jika diisi)
                    if (!string.IsNullOrEmpty(cifNumber))
                    {
                        cmd.Parameters.AddWithValue("@CifNumber", cifNumber);
                    }

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Hanya tambahkan data sesuai dengan rentang waktu yang diminta
                            UserLoginBersamaan d = new UserLoginBersamaan
                            {
                                cif_number = reader["cif_number"].ToString(),
                                customer_name = reader["customer_name"].ToString(),
                                first_channel = reader["first_channel"].ToString(),
                                first_login = reader["first_login"].ToString(),
                                second_channel = reader["second_channel"].ToString(),
                                second_login = reader["second_login"].ToString(),
                                date = reader["date"].ToString()
                            };
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
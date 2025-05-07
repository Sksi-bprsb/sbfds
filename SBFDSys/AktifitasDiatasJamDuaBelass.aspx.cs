using System;
using Npgsql;
using BPRSB.Kuesioner.Repository;
using FDS.Library.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Runtime.InteropServices.ComTypes;

namespace SBFDSys
{
    public partial class AktifitasDiatasJamDuaBelass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUser"] == null)
                Response.Redirect("default.aspx");
            else if (!IsPostBack)
            {
                string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
                List<AktifitasDiatasJamDuaBelas> result = new List<AktifitasDiatasJamDuaBelas>();
                FDS.Library.Model.Parameter prAKTIFITAS_DIATAS_JAM_12_MALAM = MySqlRepository.GetTopOne<FDS.Library.Model.Parameter>("Nama='AKTIFITAS_DIATAS_JAM_12_MALAM'");

                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    string query = @"select c.cif_number, c.customer_name, c.customer_username, t.activity_type, t.activity_data, t.created " +
                        "FROM ebanking.t_activity_customer as t left join ebanking.m_customer as c on t.createdby = c.id " +
                        "where t.created::time BETWEEN @TimeStart::time AND @TimeEnd::time and activity_type <> 'AX' and activity_type <> 'RW' and activity_type <> 'RV'--kecuali AX RW RV " +
                        "ORDER BY t.created asc";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TimeStart", prAKTIFITAS_DIATAS_JAM_12_MALAM.Value1);
                        cmd.Parameters.AddWithValue("@TimeEnd", prAKTIFITAS_DIATAS_JAM_12_MALAM.Value2);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AktifitasDiatasJamDuaBelas d = new AktifitasDiatasJamDuaBelas();
                                
                                d.customer_name= reader["customer_name"].ToString();
                                d.customer_username = reader["customer_username"].ToString();
                                d.cif_number = reader["cif_number"].ToString();
                                d.activity_type = reader["activity_type"].ToString();
                                d.activity_data = reader["activity_data"].ToString();
                                d.created = reader["created"].ToString();

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
            MySqlRepository.Insert(new Log() { Keterangan = "AKSES PENCARIAN MENU AKTIFITAS DIATAS JAM 12 MALAM ", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username,Nik=user.Nik,NamaKaryawan=user.NamaKaryawan });
            DateTime dateStart;
            DateTime dateEnd;

            // Validasi input tanggal
            bool isStartDateValid = DateTime.TryParse(TextBoxTanggalMulai.Text, out dateStart);
            bool isEndDateValid = DateTime.TryParse(TextBoxTanggalAkhir.Text, out dateEnd);
            bool isDateRangeValid = isStartDateValid && isEndDateValid;
            
            // Mendapatkan input dari TextBox customer_name
            string customerName = TextBoxSearch.Text.Trim();
            dateEnd = dateEnd.AddHours(23).AddMinutes(59).AddSeconds(59);

            string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
            List<AktifitasDiatasJamDuaBelas> result = new List<AktifitasDiatasJamDuaBelas>();
            FDS.Library.Model.Parameter prAKTIFITAS_DIATAS_JAM_12_MALAM = MySqlRepository.GetTopOne<FDS.Library.Model.Parameter>("Nama='AKTIFITAS_DIATAS_JAM_12_MALAM'");

            // Pastikan parameter memiliki nilai yang valid
            if (prAKTIFITAS_DIATAS_JAM_12_MALAM == null ||
                string.IsNullOrEmpty(prAKTIFITAS_DIATAS_JAM_12_MALAM.Value1?.ToString()) ||
                string.IsNullOrEmpty(prAKTIFITAS_DIATAS_JAM_12_MALAM.Value2?.ToString()))
            {
                Console.WriteLine("Parameter for AKTIFITAS_DIATAS_JAM_12_MALAM is not valid.");
                return;
            }


            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string query = @"SELECT c.cif_number, c.customer_name, c.customer_username, 
                     t.activity_type, t.activity_data, t.created 
                     FROM ebanking.t_activity_customer AS t 
                     LEFT JOIN ebanking.m_customer AS c ON t.createdby = c.id 
                     WHERE 
                     cast((cast(date_part('hour',t.created) as varchar)||
                     LPAD(cast(date_part('minute',t.created)as varchar),2,'0') ) as int) between @timeStart and @timeEnd
                     AND t.activity_type NOT IN ('AX')";

                // Tambahkan kondisi berdasarkan rentang tanggal (jika valid)
                if (isDateRangeValid)
                {
                    query += $" AND t.created BETWEEN '{dateStart.ToString("yyyy-MM-dd HH:mm:ss")}' AND '{dateEnd.ToString("yyyy-MM-dd HH:mm:ss")}'";
                }

                // Kondisi pencarian berdasarkan nama
                if (!string.IsNullOrEmpty(customerName))
                {
                    query += " AND LOWER(c.customer_name) LIKE LOWER(@CustomerName)";
                }

                query += " ORDER BY t.created ASC";

                Console.WriteLine("Generated Query: " + query);
                Console.WriteLine("Customer Name: " + customerName);
                Console.WriteLine("Start Date: " + dateStart);
                Console.WriteLine("End Date: " + dateEnd);

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    // Set parameter customer_name (jika diisi)
                    if (!string.IsNullOrEmpty(customerName))
                    {
                        cmd.Parameters.AddWithValue("@CustomerName", "%" + customerName + "%");
                    }

                    // Set parameter rentang tanggal (jika valid)
                    if (isDateRangeValid)
                    {
                        cmd.Parameters.AddWithValue("@timeStart", Convert.ToInt32(prAKTIFITAS_DIATAS_JAM_12_MALAM.Value1.Replace(":", "")));
                        cmd.Parameters.AddWithValue("@timeEnd", Convert.ToInt32(prAKTIFITAS_DIATAS_JAM_12_MALAM.Value2.Replace(":", "")));
                    }

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AktifitasDiatasJamDuaBelas d = new AktifitasDiatasJamDuaBelas
                            {
                                customer_name = reader["customer_name"].ToString(),
                                customer_username = reader["customer_username"].ToString(),
                                cif_number = reader["cif_number"].ToString(),
                                activity_type = reader["activity_type"].ToString(),
                                activity_data = reader["activity_data"].ToString(),
                                created = reader["created"].ToString()
                            };

                            result.Add(d);
                        }
                    }
                }
            }

            if (result.Count == 0)
            {
                Console.WriteLine("No results found.");
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
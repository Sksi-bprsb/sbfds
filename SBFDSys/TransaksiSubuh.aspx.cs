using System;
using Npgsql;
using BPRSB.Kuesioner.Repository;
using FDS.Library.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace SBFDSys
{
    public partial class TransaksiSubuh : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUser"] == null)
                Response.Redirect("default.aspx");
            else if (!IsPostBack)
            {
                string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
                List<TransaksiSubuhs> result = new List<TransaksiSubuhs>();
                FDS.Library.Model.Parameter prTRANSAKSI_DIATAS_JAM_12_MALAM = MySqlRepository.GetTopOne<FDS.Library.Model.Parameter>("Nama='TRANSAKSI_DIATAS_JAM_12_MALAM'");

                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    string query = @"select c.cif_number, c.customer_name, c.customer_username, t.transaction_type,
                     m.description, t.transaction_amount, t.status, t.from_account_number, t.to_account_number, t.transaction_date 
                     FROM ebanking.t_transaction as t 
                     left join ebanking.m_customer as c on t.createdby = c.id 
                     left join ebanking.m_transaction_type as m on t.transaction_type = m.transaction_type 
                     where t.transaction_date::time BETWEEN @TimeStart::time AND @TimeEnd::time 
                     ORDER BY t.transaction_date asc";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TimeStart", prTRANSAKSI_DIATAS_JAM_12_MALAM.Value1);
                        cmd.Parameters.AddWithValue("@TimeEnd", prTRANSAKSI_DIATAS_JAM_12_MALAM.Value2); 

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            

                            while (reader.Read())
                            {
                                TransaksiSubuhs d = new TransaksiSubuhs();
                                
                                d.customer_name= reader["customer_name"].ToString();
                                d.cif_number = reader["cif_number"].ToString();
                                d.transaction_type = reader["transaction_type"].ToString();
                                d.description = reader["description"].ToString();
                                d.transaction_amount = reader["transaction_amount"] != DBNull.Value
                                      ? Convert.ToDecimal(reader["transaction_amount"]).ToString("C", new CultureInfo("id-ID"))
                                      : "0";
                                d.status = reader["status"].ToString();
                                d.from_account_number = reader["from_account_number"].ToString();
                                d.to_account_number = reader["to_account_number"].ToString();
                                d.transaction_date = reader["transaction_date"].ToString();

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
            MySqlRepository.Insert(new Log() { Keterangan = "AKSES PENCARIAN MENU TRANSAKSI DIATAS JAM 12 MALAM ", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username, Nik=user.Nik,NamaKaryawan=user.NamaKaryawan });
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;
            string tanggalMulai = TextBoxTanggalMulai.Text.Trim();
            string tanggalAkhir = TextBoxTanggalAkhir.Text.Trim();

            if (DateTime.TryParse(TextBoxTanggalMulai.Text, out DateTime parsedStartDate))
            {
                startDate = parsedStartDate;
            }

            if (DateTime.TryParse(TextBoxTanggalAkhir.Text, out DateTime parsedEndDate))
            {
                endDate = parsedEndDate.AddHours(23).AddMinutes(59).AddSeconds(59);
            }

            {
                string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
                List<TransaksiSubuhs> result = new List<TransaksiSubuhs>();
                FDS.Library.Model.Parameter prTRANSAKSI_DIATAS_JAM_12_MALAM = MySqlRepository.GetTopOne<FDS.Library.Model.Parameter>("Nama='TRANSAKSI_DIATAS_JAM_12_MALAM'");

                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    string query = $@" 
                    select c.cif_number, c.customer_name, c.customer_username, t.transaction_type,
                     m.description, t.transaction_amount, t.status, t.from_account_number, t.to_account_number, t.transaction_date 
                     FROM ebanking.t_transaction as t 
                     left join ebanking.m_customer as c on t.createdby = c.id 
                     left join ebanking.m_transaction_type as m on t.transaction_type = m.transaction_type
                     where
                     cast((cast(date_part('hour',t.transaction_date) as varchar)||
                    LPAD(cast(date_part('minute',t.transaction_date)as varchar),2,'0') ) as int) between @StartDate and @EndDate
                    and  t.transaction_date BETWEEN '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd HH:mm:ss")}'
                    ORDER BY t.transaction_date asc";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        //cmd.Parameters.AddWithValue("@StartDate", startDate);
                        //cmd.Parameters.AddWithValue("@EndDate", endDate);

                        cmd.Parameters.AddWithValue("@StartDate", Convert.ToInt32(prTRANSAKSI_DIATAS_JAM_12_MALAM.Value1.Replace(":","")));
                        cmd.Parameters.AddWithValue("@EndDate", Convert.ToInt32(prTRANSAKSI_DIATAS_JAM_12_MALAM.Value2.Replace(":", "")));

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TransaksiSubuhs d = new TransaksiSubuhs();

                                d.customer_name = reader["customer_name"].ToString();
                                d.cif_number = reader["cif_number"].ToString();
                                d.transaction_type = reader["transaction_type"].ToString();
                                d.description = reader["description"].ToString();
                                d.transaction_amount = reader["transaction_amount"] != DBNull.Value
                                      ? Convert.ToDecimal(reader["transaction_amount"]).ToString("C", new CultureInfo("id-ID"))
                                      : "0";
                                d.status = reader["status"].ToString();
                                d.from_account_number = reader["from_account_number"].ToString();
                                d.to_account_number = reader["to_account_number"].ToString();
                                d.transaction_date = reader["transaction_date"].ToString();

                                result.Add(d);
                            }
                        }
                    }
                }
                ListViewData.DataSource = result;
                ListViewData.DataBind();
            }

    }

        protected void ListViewData_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {

        }

        protected void ButtonOKConfirm_Click(object sender, EventArgs e)
        {

        }
    }
}
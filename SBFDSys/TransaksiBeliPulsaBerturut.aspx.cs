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
    public partial class TransaksiBeliPulsaBerturut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUser"] == null)
                Response.Redirect("default.aspx");
            else if (!IsPostBack)
            {
                string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
                List<TransaksiBeliPulsaBerturuts> result = new List<TransaksiBeliPulsaBerturuts>();
                FDS.Library.Model.Parameter prTRANSAKSI_BELI_PULSA = MySqlRepository.GetTopOne<FDS.Library.Model.Parameter>("Nama='TRANSAKSI_BELI_PULSA'");

                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand($@"SELECT * FROM(SELECT c.cif_number, c.customer_name, 
c.customer_username, t.transaction_type, m.description, t.from_account_number,
t.transaction_amount, t.status, t.transaction_date, COUNT(t.transaction_type) 
OVER(PARTITION BY c.cif_number, DATE(t.transaction_date)) AS transaksi_per_hari FROM ebanking.t_transaction AS t 
LEFT JOIN ebanking.m_customer AS c on t.createdby = c.id 
LEFT JOIN ebanking.m_transaction_type as m on t.transaction_type = m.transaction_type
WHERE t.transaction_type = '92') AS subquery WHERE  transaksi_per_hari > {prTRANSAKSI_BELI_PULSA.Value1} ORDER BY  cif_number, transaction_date ASC; ", conn))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TransaksiBeliPulsaBerturuts d = new TransaksiBeliPulsaBerturuts();
                                
                                d.customer_name= reader["customer_name"].ToString();
                                d.cif_number = reader["cif_number"].ToString();
                                d.customer_username = reader["customer_username"].ToString();
                                d.transaction_type = reader["transaction_type"].ToString();
                                d.description = reader["description"].ToString();
                                d.from_account_number = reader["from_account_number"].ToString();
                                d.transaction_amount = reader["transaction_amount"] != DBNull.Value
                                      ? Convert.ToDecimal(reader["transaction_amount"]).ToString("C", new CultureInfo("id-ID"))
                                      : "0";
                                d.status = reader["status"].ToString();
                                d.transaction_date = reader["transaction_date"].ToString();
                                d.transaksi_per_hari = reader["transaksi_per_hari"].ToString();
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
            MySqlRepository.Insert(new Log() { Keterangan = "AKSES PENCARIAN MENU TRANSAKSI BELI PULSA BERURUT ", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username ,Nik=user.Nik,NamaKaryawan = user.NamaKaryawan });
            DateTime start = DateTime.MinValue;
            DateTime end = DateTime.MinValue;

            bool isDateRangeValid = DateTime.TryParse(TextBoxTanggalMulai.Text, out start) &&
                                    DateTime.TryParse(TextBoxTanggalAkhir.Text, out end);
            string customerName = TextBoxSearch.Text.Trim();

            string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
            List<TransaksiBeliPulsaBerturuts> result = new List<TransaksiBeliPulsaBerturuts>();
            FDS.Library.Model.Parameter prTRANSAKSI_BELI_PULSA = MySqlRepository.GetTopOne<FDS.Library.Model.Parameter>("Nama='TRANSAKSI_BELI_PULSA'");

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string query = $@"
                                SELECT * FROM(SELECT c.cif_number, c.customer_name, c.customer_username, t.transaction_type, m.description, t.from_account_number, t.transaction_amount, t.status, t.transaction_date, COUNT(t.transaction_type) OVER(PARTITION BY c.cif_number, DATE(t.transaction_date)) AS transaksi_per_hari FROM ebanking.t_transaction AS t LEFT JOIN ebanking.m_customer AS c on t.createdby = c.id LEFT JOIN ebanking.m_transaction_type as m on t.transaction_type = m.transaction_type WHERE t.transaction_type = '92') AS subquery WHERE  transaksi_per_hari > "+prTRANSAKSI_BELI_PULSA.Value1;
                if (isDateRangeValid)
                {
                    query += " AND DATE(transaction_date) BETWEEN @StartDate AND @EndDate";
                }

                if (!string.IsNullOrEmpty(customerName))
                    {
                        query += " AND LOWER(c.customer_name) LIKE LOWER(@CustomerName) ";
                    }

                    query += @"
                            ORDER BY  cif_number, transaction_date ASC ";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        // Set parameter untuk customer_name (jika diisi)
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
                                TransaksiBeliPulsaBerturuts d = new TransaksiBeliPulsaBerturuts();

                                d.customer_name = reader["customer_name"].ToString();
                                d.cif_number = reader["cif_number"].ToString();
                                d.customer_username = reader["customer_username"].ToString();
                                d.transaction_type = reader["transaction_type"].ToString();
                                d.description = reader["description"].ToString();
                                d.from_account_number = reader["from_account_number"].ToString();
                                d.transaction_amount = reader["transaction_amount"].ToString();
                                d.status = reader["status"].ToString();
                                d.transaction_date = reader["transaction_date"].ToString();
                                d.transaksi_per_hari = reader["transaksi_per_hari"].ToString();
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
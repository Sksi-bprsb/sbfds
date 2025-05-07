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
    public partial class TotalTransaksiMelebihiTigaPuluhJutas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUser"] == null)
                Response.Redirect("default.aspx");
            else if (!IsPostBack)
            {
                string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
                List<TotalTransaksiMelebihiTigaPuluhJuta> result = new List<TotalTransaksiMelebihiTigaPuluhJuta>();
                FDS.Library.Model.Parameter prTRANSAKSI_MELEBIHI_30_JUTA = MySqlRepository.GetTopOne<FDS.Library.Model.Parameter>("Nama='TRANSAKSI_MELEBIHI_30_JUTA'");

                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand($@"select c.cif_number, c.customer_name, c.customer_username,
                        t.transaction_type, m.description, count(t.transaction_type), sum(t.transaction_amount), t.status, t.from_account_number, t.to_account_number, date(t.transaction_date)
                        FROM ebanking.t_transaction as t left join ebanking.m_customer as c on t.createdby = c.id
                        left join ebanking.m_transaction_type as m on t.transaction_type = m.transaction_type
                        where t.transaction_type <> '62' and t.transaction_type <> '61'
                        group by c.cif_number, c.customer_name, c.customer_username, t.transaction_type, m.description, t.status, t.from_account_number, t.to_account_number, date(t.transaction_date)
                        having sum(t.transaction_amount) >= {prTRANSAKSI_MELEBIHI_30_JUTA.Value1} ORDER BY date(t.transaction_date)asc, c.cif_number", conn))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TotalTransaksiMelebihiTigaPuluhJuta d = new TotalTransaksiMelebihiTigaPuluhJuta();
                                
                                d.customer_name= reader["customer_name"].ToString();
                                d.cif_number = reader["cif_number"].ToString();
                                d.customer_username = reader["customer_username"].ToString();
                                d.transaction_type = reader["transaction_type"].ToString();
                                d.description = reader["description"].ToString();
                                d.count = reader["count"].ToString();
                                d.sum = reader["sum"] != DBNull.Value
                                      ? Convert.ToDecimal(reader["sum"]).ToString("C", new CultureInfo("id-ID"))
                                      : "0";
                                d.status = reader["status"].ToString();
                                d.from_account_number = reader["from_account_number"].ToString();
                                d.to_account_number = reader["to_account_number"].ToString();
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
            DateTime end = DateTime.MinValue;

            bool isDateRangeValid = DateTime.TryParse(TextBoxTanggalMulai.Text, out start) &&
                                    DateTime.TryParse(TextBoxTanggalAkhir.Text, out end);
            string customerName = TextBoxSearch.Text.Trim();

            string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
            List<TotalTransaksiMelebihiTigaPuluhJuta> result = new List<TotalTransaksiMelebihiTigaPuluhJuta>();
            FDS.Library.Model.Parameter prTRANSAKSI_MELEBIHI_30_JUTA = MySqlRepository.GetTopOne<FDS.Library.Model.Parameter>("Nama='TRANSAKSI_MELEBIHI_30_JUTA'");

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string query = $@"
                select c.cif_number, c.customer_name, c.customer_username, t.transaction_type, m.description, count(t.transaction_type), sum(t.transaction_amount), t.status, t.from_account_number, t.to_account_number, date(t.transaction_date) 
                FROM ebanking.t_transaction as t 
                left join ebanking.m_customer as c on t.createdby = c.id 
                left join ebanking.m_transaction_type as m on t.transaction_type = m.transaction_type 
                where t.transaction_type <> '62' and t.transaction_type <> '61' 
                group by c.cif_number, c.customer_name, c.customer_username, t.transaction_type, m.description, t.status, t.from_account_number, t.to_account_number, date(t.transaction_date) 
                having sum(t.transaction_amount) >= {prTRANSAKSI_MELEBIHI_30_JUTA.Value1} ";
                if (isDateRangeValid)
                {
                    query += " AND DATE(transaction_date) BETWEEN @StartDate AND @EndDate";
                }

                if (!string.IsNullOrEmpty(customerName))
                {
                    query += " AND LOWER(c.customer_name) LIKE LOWER(@CustomerName) ";
                }

                query += @"
                            ORDER BY date(t.transaction_date)asc, c.cif_number ";

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
                            TotalTransaksiMelebihiTigaPuluhJuta d = new TotalTransaksiMelebihiTigaPuluhJuta();

                            d.customer_name = reader["customer_name"].ToString();
                            d.cif_number = reader["cif_number"].ToString();
                            d.customer_username = reader["customer_username"].ToString();
                            d.transaction_type = reader["transaction_type"].ToString();
                            d.description = reader["description"].ToString();
                            d.count = reader["count"].ToString();
                            d.sum = reader["sum"] != DBNull.Value
                                      ? Convert.ToDecimal(reader["sum"]).ToString("C", new CultureInfo("id-ID"))
                                      : "0";
                            d.status = reader["status"].ToString();
                            d.from_account_number = reader["from_account_number"].ToString();
                            d.to_account_number = reader["to_account_number"].ToString();
                            d.date = reader["date"].ToString();
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
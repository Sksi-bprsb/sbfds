using System;
using Npgsql;
using System.Globalization;
using BPRSB.Kuesioner.Repository;
using FDS.Library.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Org.BouncyCastle.Asn1.X509;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace SBFDSys
{
    public partial class JumlahTransaksiTidakSesuaiPolas : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUser"] == null)
                Response.Redirect("default.aspx");
            else if (!IsPostBack)
            {
                string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
                List<JumlahTransaksiTidakSesuaiPola> result = new List<JumlahTransaksiTidakSesuaiPola>();


                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    //using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                        string query = $@" WITH DailyTransactionCount AS (
                                SELECT 
                                    c.cif_number,
                                    t.transaction_type,
                                    DATE(t.transaction_date) AS transaction_day,
                                    COUNT(t.transaction_type) AS daily_transaction_count
                                FROM 
                                    ebanking.t_transaction AS t
                                LEFT JOIN 
                                    ebanking.m_customer AS c ON t.createdby = c.id
                                WHERE 
                                    t.status = 'SUCCEED'
                                     AND t.transaction_date BETWEEN DATE '2025-01-01' - INTERVAL '6 months' AND DATE '2025-01-02'
                                     
                                    --AND t.transaction_type = '71'
                                GROUP BY 
                                    c.cif_number, 
                                    t.transaction_type, 
                                    DATE(t.transaction_date)
                            ),
                            AggregatedTransactions AS (
                                SELECT 
                                    cif_number,
                                    transaction_type,
                                    MIN(daily_transaction_count) AS min_daily_transaction_count,
                                    MAX(daily_transaction_count) AS max_daily_transaction_count,
                                    SUM(daily_transaction_count) AS total_transaction_count,
                                    COUNT(transaction_day) AS transaction_days_count,
                                    COUNT(DISTINCT transaction_day) AS total_transaction_days,
                                    SUM(daily_transaction_count) / NULLIF(COUNT(transaction_day), 0) AS avg_transaction_per_day
                                FROM 
                                    DailyTransactionCount
                                GROUP BY 
                                    cif_number, 
                                    transaction_type
                            ),
                            LastTransactionOnDate AS (
                                SELECT 
                                    c.cif_number, 
                                    c.customer_name, 
                                    c.customer_username, 
                                    t.transaction_amount AS last_transaction_amount,
                                    t.transaction_date,
                                    t.transaction_type,
                                    m.description
                                FROM 
                                    ebanking.t_transaction AS t
                                LEFT JOIN 
                                    ebanking.m_customer AS c ON t.createdby = c.id
                                LEFT JOIN 
                                        ebanking.m_transaction_type AS m ON t.transaction_type = m.transaction_type
                                WHERE 
                                    t.status = 'SUCCEED'
                                   AND to_char(t.transaction_date, 'yyyy-mm-dd') = '2025-01-01'  
                                    --AND t.transaction_type = '71'
                            ),
                                TransactionCountOnDate AS (
                                    SELECT 
                                        c.cif_number,
                                        t.transaction_type,
                                        DATE(t.transaction_date) AS transaction_day,
                                        COUNT(*) AS transaction_count_on_date
                                    FROM 
                                        ebanking.t_transaction AS t
                                    LEFT JOIN 
                                        ebanking.m_customer AS c ON t.createdby = c.id
                                    WHERE 
                                        t.status = 'SUCCEED'
                                        AND DATE(t.transaction_date) =  '2025-01-01'
                                    GROUP BY 
                                        c.cif_number, 
                                        t.transaction_type, 
                                        DATE(t.transaction_date)
                                )
                            SELECT 
                                ltd.cif_number,
                                ltd.customer_name,
                                ltd.customer_username,
                                ltd.last_transaction_amount,
                                ltd.transaction_date,
                                ltd.description,
                                tcd.transaction_count_on_date AS transaction_count_on_specified_date,
                                at.min_daily_transaction_count,
                                at.max_daily_transaction_count,
                                at.avg_transaction_per_day,
                                at.total_transaction_count,
                                at.total_transaction_days,
                                at. transaction_type
                            FROM 
                                LastTransactionOnDate AS ltd
                            LEFT JOIN 
                                AggregatedTransactions AS at 
                                ON ltd.cif_number = at.cif_number 
                                AND ltd.transaction_type = at.transaction_type
                            LEFT JOIN 
                                TransactionCountOnDate AS tcd 
                                ON ltd.cif_number = tcd.cif_number
                                AND ltd.transaction_type = tcd.transaction_type
                                AND DATE(ltd.transaction_date) = tcd.transaction_day
                            where
                    (COALESCE(tcd.transaction_count_on_date, 0) > at.max_daily_transaction_count 
                    OR COALESCE(tcd.transaction_count_on_date, 0) > at.avg_transaction_per_day)";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                JumlahTransaksiTidakSesuaiPola d = new JumlahTransaksiTidakSesuaiPola();
                                {
                                    d.customer_name = reader["customer_name"].ToString();
                                    d.cif_number = reader["cif_number"].ToString();
                                    d.customer_username = reader["customer_username"].ToString();
                                    d.transaction_type = reader["transaction_type"].ToString();
                                    d.description = reader["description"].ToString();
                                    d.last_transaction_amount = Convert.ToDecimal(reader["last_transaction_amount"]).ToString("N0", new CultureInfo("id-ID"));  // Format nominal
                                    if (!(reader["avg_transaction_per_day"] is DBNull))
                                        d.avg_transaction_per_day = Convert.ToDecimal(reader["avg_transaction_per_day"]).ToString("F2", new CultureInfo("id-ID"));  // Format rata-rata transaksi per hari
                                    if (!(reader["min_daily_transaction_count"] is DBNull))
                                        d.min_daily_transaction_count = Convert.ToInt32(reader["min_daily_transaction_count"]).ToString();  // Jumlah minimum transaksi per hari
                                    if (!(reader["max_daily_transaction_count"] is DBNull))
                                        d.max_daily_transaction_count = Convert.ToInt32(reader["max_daily_transaction_count"]).ToString();  // Jumlah maksimum transaksi per hari
                                    if (!(reader["transaction_count_on_specified_date"] is DBNull))
                                        d.transaction_count_on_specified_date = Convert.ToInt32(reader["transaction_count_on_specified_date"]).ToString();  // Total transaksi per hari
                                    d.transaction_date = Convert.ToDateTime(reader["transaction_date"]).ToString("dd MMM yyyy", new CultureInfo("id-ID"));   // Formatting date
                                }
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

            DateTime start;
            DateTime end = DateTime.MinValue;

            // Validasi input tanggal
            bool isDateRangeValid = DateTime.TryParse(TextBoxTanggalMulai.Text, out start) &&
                                    DateTime.TryParse(TextBoxTanggalAkhir.Text, out end);

            string customerName = TextBoxSearch.Text.Trim();
            FDS.Library.Model.Parameter prPOLA_FREKUENSI_TRANSAKSI_TIDAK_SESUAI = MySqlRepository.GetTopOne<FDS.Library.Model.Parameter>("Nama='POLA_FREKUENSI_TRANSAKSI_TIDAK_SESUAI'");

            string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
            List<JumlahTransaksiTidakSesuaiPola> result = new List<JumlahTransaksiTidakSesuaiPola>();

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Query SQL
                string query = $@"WITH DailyTransactionCount AS(
                    SELECT
                        c.cif_number,
                        t.transaction_type,
                        DATE(t.transaction_date) AS transaction_day,
                        COUNT(t.transaction_type) AS daily_transaction_count
                    FROM
                        ebanking.t_transaction AS t
                    LEFT JOIN
                        ebanking.m_customer AS c ON t.createdby = c.id
                    WHERE
                        t.status = 'SUCCEED'
                        
                        AND t.transaction_date BETWEEN (DATE '{start.ToString("yyyy-MM-dd")}' - INTERVAL '1 day' - INTERVAL '{prPOLA_FREKUENSI_TRANSAKSI_TIDAK_SESUAI.Value1} months') 
                                    AND (DATE '{end.ToString("yyyy-MM-dd 23:59:59")}' - INTERVAL '1 day')

                    GROUP BY
                        c.cif_number,
                        t.transaction_type,
                        DATE(t.transaction_date)
                ),
                AggregatedTransactions AS(
                    SELECT
                        cif_number,
                        transaction_type,
                        COALESCE(MIN(daily_transaction_count), 0) AS min_daily_transaction_count,
                        COALESCE(MAX(daily_transaction_count), 0) AS max_daily_transaction_count,
                        COALESCE(SUM(daily_transaction_count), 0) AS total_transaction_count,
                        COUNT(transaction_day) AS transaction_days_count,
                        COUNT(DISTINCT transaction_day) AS total_transaction_days,
                        COALESCE(SUM(daily_transaction_count), 0) / NULLIF(COUNT(transaction_day), 0) AS avg_transaction_per_day
                    FROM
                        DailyTransactionCount
                    GROUP BY
                        cif_number, 
                        transaction_type
                ),
                LastTransactionOnDate AS(
                    SELECT
                        c.cif_number,
                        c.customer_name,
                        c.customer_username,
                        t.transaction_amount AS last_transaction_amount,
                        t.transaction_date,
                        t.transaction_type,
                        m.description
                    FROM
                        ebanking.t_transaction AS t
                    LEFT JOIN
                        ebanking.m_customer AS c ON t.createdby = c.id
                    LEFT JOIN
                        ebanking.m_transaction_type AS m ON t.transaction_type = m.transaction_type
                    WHERE
                        t.status = 'SUCCEED'
                        AND to_char(t.transaction_date, 'yyyy-mm-dd') = '{start.ToString("yyyy-MM-dd")}'
                ),
                TransactionCountOnDate AS(
                    SELECT
                        c.cif_number,
                        t.transaction_type,
                        DATE(t.transaction_date) AS transaction_day,
                        COUNT(*) AS transaction_count_on_date
                    FROM
                        ebanking.t_transaction AS t
                    LEFT JOIN
                        ebanking.m_customer AS c ON t.createdby = c.id
                    WHERE
                        t.status = 'SUCCEED'
                        AND DATE(t.transaction_date) = '{start.ToString("yyyy-MM-dd")}'
                    GROUP BY
                        c.cif_number,
                        t.transaction_type,
                        DATE(t.transaction_date)
                ),
                FilteredResult AS(
                    SELECT
                        ltd.*,
                        COALESCE(tcd.transaction_count_on_date, 0) AS transaction_count_on_date,
                        COALESCE(at.min_daily_transaction_count, 0) AS min_daily_transaction_count,
                        COALESCE(at.max_daily_transaction_count, 0) AS max_daily_transaction_count,
                        COALESCE(at.avg_transaction_per_day, 0) AS avg_transaction_per_day,
                        COALESCE(at.total_transaction_count, 0) AS total_transaction_count,
                        COALESCE(at.total_transaction_days, 0) AS total_transaction_days,
                        ROW_NUMBER() OVER(PARTITION BY ltd.cif_number, ltd.transaction_type ORDER BY ltd.transaction_date DESC) AS row_num
                    FROM
                        LastTransactionOnDate AS ltd
                    LEFT JOIN
                        AggregatedTransactions AS at
                        ON ltd.cif_number = at.cif_number
                        AND ltd.transaction_type = at.transaction_type
                    LEFT JOIN
                        TransactionCountOnDate AS tcd
                        ON ltd.cif_number = tcd.cif_number
                        AND ltd.transaction_type = tcd.transaction_type
                        AND DATE(ltd.transaction_date) = tcd.transaction_day
                    WHERE
                        (COALESCE(tcd.transaction_count_on_date, 0) > COALESCE(at.max_daily_transaction_count, 0)
                        OR COALESCE(tcd.transaction_count_on_date, 0) > COALESCE(at.avg_transaction_per_day, 0)) AND 
                       ltd.customer_name like '%{customerName}%'
                )
                SELECT
                    cif_number,
                    customer_name,
                    customer_username,
                    last_transaction_amount,
                    transaction_date,
                    description,
                    transaction_count_on_date as transaction_count_on_specified_date,
                    min_daily_transaction_count,
                    max_daily_transaction_count,
                    avg_transaction_per_day,
                    total_transaction_count,
                    total_transaction_days,
                    transaction_type
                FROM
                    FilteredResult
                WHERE
                    row_num = 1";

                
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            JumlahTransaksiTidakSesuaiPola d = new JumlahTransaksiTidakSesuaiPola();
                            {
                                d.customer_name = reader["customer_name"].ToString();
                                d.cif_number = reader["cif_number"].ToString();
                                d.customer_username = reader["customer_username"].ToString();
                                d.transaction_type = reader["transaction_type"].ToString();
                                d.description = reader["description"].ToString();
                                d.last_transaction_amount = Convert.ToDecimal(reader["last_transaction_amount"]).ToString("N0", new CultureInfo("id-ID"));  // Format nominal
                                if (!(reader["avg_transaction_per_day"] is DBNull))
                                    d.avg_transaction_per_day = Convert.ToDecimal(reader["avg_transaction_per_day"]).ToString("F2", new CultureInfo("id-ID"));  // Format rata-rata transaksi per hari
                                if (!(reader["min_daily_transaction_count"] is DBNull))
                                    d.min_daily_transaction_count = Convert.ToInt32(reader["min_daily_transaction_count"]).ToString();  // Jumlah minimum transaksi per hari
                                if (!(reader["max_daily_transaction_count"] is DBNull))
                                    d.max_daily_transaction_count = Convert.ToInt32(reader["max_daily_transaction_count"]).ToString();  // Jumlah maksimum transaksi per hari
                                if (!(reader["transaction_count_on_specified_date"] is DBNull))
                                    d.transaction_count_on_specified_date = Convert.ToInt32(reader["transaction_count_on_specified_date"]).ToString();  // Total transaksi per hari
                                d.transaction_date = Convert.ToDateTime(reader["transaction_date"]).ToString("dd MMM yyyy", new CultureInfo("id-ID"));   // Formatting date
                            }
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
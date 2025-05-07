using System;
using Npgsql;
using System.Globalization;
using BPRSB.Kuesioner.Repository;
using FDS.Library.Model;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SBFDSys
{
    public partial class PolaTransaksiDebetSemuas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUser"] == null)
                Response.Redirect("default.aspx");
            else if (!IsPostBack)
            { 
            string cifNumber = Request.QueryString["cif_number"];
            string transactionType = Request.QueryString["transaction_type"];
            string tanggalMulai = Request.QueryString["tanggalMulai"];



            if (!string.IsNullOrEmpty(cifNumber) && !string.IsNullOrEmpty(transactionType) &&
                !string.IsNullOrEmpty(tanggalMulai))
            {
                string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
                List<PolaTransaksiDebetSemua> result = new List<PolaTransaksiDebetSemua>();

                // Parsing tanggal
                DateTime dtMulai;

                // Menggunakan TryParseExact untuk menghindari exception
                if (DateTime.TryParseExact(tanggalMulai, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtMulai))
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                    {
                        conn.Open();
                        using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                           SELECT * FROM ebanking.t_transaction t
                            LEFT JOIN 
                                ebanking.m_customer AS c ON t.createdby = c.id
                            WHERE  t.status = 'SUCCEED' 
                            and cif_number=  @cif_number  
                            AND t.transaction_type =  @transaction_type  
                            AND t.transaction_date BETWEEN  @TanggalMulai::date AND @TanggalAkhir::date
                             ", conn))
                            {
                                // Menambahkan parameter
                                cmd.Parameters.AddWithValue("@cif_number", cifNumber);
                                cmd.Parameters.AddWithValue("@transaction_type", transactionType);
                                cmd.Parameters.AddWithValue("@TanggalMulai", dtMulai);

                                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        PolaTransaksiDebetSemua d = new PolaTransaksiDebetSemua();

                                        d.customer_name = reader["customer_name"].ToString();
                                        d.cif_number = reader["cif_number"].ToString();
                                        d.customer_username = reader["customer_username"].ToString();
                                        d.transaction_type = reader["transaction_type"].ToString();
                                        d.description = reader["description"].ToString();
                                        d.from_account_number = reader["from_account_number"].ToString();
                                        d.to_account_number = reader["to_account_number"].ToString();
                                        //d.count = reader["transaction_count"].ToString();
                                        //d.sum = Convert.ToDecimal(reader["total_transaction_amount"]).ToString("C", new CultureInfo("id-ID"));
                                        //d.min = Convert.ToDecimal(reader["min_transaction_amount"]).ToString("C", new CultureInfo("id-ID"));
                                        //d.max = Convert.ToDecimal(reader["max_transaction_amount"]).ToString("C", new CultureInfo("id-ID"));
                                        //d.avg = Convert.ToDecimal(reader["avg_transaction_amount"]).ToString("C", new CultureInfo("id-ID"));

                                        if (reader["transaction_date"] != DBNull.Value)
                                        {
                                            DateTime transactionDate = Convert.ToDateTime(reader["transaction_date"]);
                                            d.date = transactionDate.ToString("MM/dd/yyyy"); // Format date sesuai kebutuhan
                                        }
                                        result.Add(d);
                                    }
                                }
                            }
                    }

                    //ListViewData.DataSource = result;
                    //ListViewData.DataBind();
                }
            }
            else
            {
                if (!IsPostBack)
                {
                    TextBoxTanggalMulai.Text = DateTime.Today.ToString("MM-dd-yyyy");
                }
                // Jika parameter tidak ditemukan
                //Response.Write("Invalid parameter: CIF Number or Transaction Type missing.");
            }
            }
        }


    protected void ButtonSearchTransaksi_Click(object sender, EventArgs e)
    {

            string customerName = TextBoxSearch.Text.Trim();
            string tanggalMulai = TextBoxTanggalMulai.Text.Trim();
            //string tipeTransakasi = TextBoxTipeTransaksi.Text.Trim();
            DateTime parsedTanggalMulai;

            // Check if the input date is valid
            if (!DateTime.TryParse(tanggalMulai, out parsedTanggalMulai))
            {
                LabelError.Text = "Format tanggal salah. Masukkan tanggal yang valid.";
                return;
            }
            tanggalMulai = parsedTanggalMulai.ToString("yyyy-MM-dd");
            string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
            List<PolaTransaksiDebetSemua> result = new List<PolaTransaksiDebetSemua>();
            List<PolaTransaksiDebetSemua> resultperhari = new List<PolaTransaksiDebetSemua>();

            //string tipeQuery =  tipeTransakasi==string.Empty?"" : " AND t.transaction_type = '" + tipeTransakasi + "'";
            FDS.Library.Model.Parameter prPOLA_NOMINAL_TRANSAKSI_TIDAK_SESUAI = MySqlRepository.GetTopOne<FDS.Library.Model.Parameter>("Nama='POLA_NOMINAL_TRANSAKSI_TIDAK_SESUAI'");

            string cusQuery =   customerName==string.Empty?"":" AND c.customer_name like  '%"+customerName+"%'";

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // SQL Query
                string query = $@"
                                 WITH AggregatedTransactions AS (
                    SELECT 
                        c.cif_number,
                        t.transaction_type,
                        MIN(t.transaction_amount) AS min_transaction_amount,
                        MAX(t.transaction_amount) AS max_transaction_amount,
                        SUM(t.transaction_amount) AS total_transaction_amount,
                        COUNT(t.transaction_amount) AS transaction_count,
                        -- Calculate the average transaction amount for the same transaction_type over 6 months
                        SUM(t.transaction_amount) / NULLIF(COUNT(t.transaction_amount), 0) AS avg_transaction_amount
                    FROM 
                        ebanking.t_transaction AS t
                    LEFT JOIN 
                        ebanking.m_customer AS c ON t.createdby = c.id
                    WHERE 
                        t.status = 'SUCCEED'
                        
                        AND t.transaction_date BETWEEN (DATE '{tanggalMulai}' - INTERVAL '1 day' - INTERVAL '{prPOLA_NOMINAL_TRANSAKSI_TIDAK_SESUAI.Value1} months') 
                                    AND (DATE '{tanggalMulai}' - INTERVAL '1 day')
                    GROUP BY 
                        c.cif_number, 
                        t.transaction_type
                ),
                DailyTransactionAggregation AS (
                    SELECT
                        c.cif_number,
                        t.transaction_type,
                        DATE(t.transaction_date) AS transaction_date,
                        SUM(t.transaction_amount) AS daily_total_transaction
                    FROM 
                        ebanking.t_transaction AS t
                    LEFT JOIN 
                        ebanking.m_customer AS c ON t.createdby = c.id
                    WHERE 
                        t.status = 'SUCCEED'
                        
                        AND t.transaction_date BETWEEN (DATE '{tanggalMulai}' - INTERVAL '1 day' - INTERVAL '{prPOLA_NOMINAL_TRANSAKSI_TIDAK_SESUAI.Value1} months') 
                                    AND (DATE '{tanggalMulai}' - INTERVAL '1 day')
                    GROUP BY 
                        c.cif_number, t.transaction_type, DATE(t.transaction_date)
                ),
                AggregatedDailyTransactions AS (
                    SELECT 
                        d.cif_number,
                        d.transaction_type,
                        MIN(d.daily_total_transaction) AS min_daily_transaction_amount,
                        MAX(d.daily_total_transaction) AS max_daily_transaction_amount,
                        AVG(d.daily_total_transaction) AS avg_daily_transaction_amount
                    FROM 
                        DailyTransactionAggregation AS d
                    GROUP BY 
                        d.cif_number, d.transaction_type
                ),
                LastTransactionOnDate AS (
                    SELECT 
                        c.cif_number, 
                        c.customer_name, 
                        c.customer_username, 
                        t.transaction_amount AS last_transaction_amount,
                        t.transaction_date,
                        m.description,
                        t.transaction_type
                    FROM 
                        ebanking.t_transaction AS t
                    LEFT JOIN 
                        ebanking.m_customer AS c ON t.createdby = c.id
                    LEFT JOIN 
        			    ebanking.m_transaction_type AS m ON t.transaction_type = m.transaction_type
                    WHERE 
                        t.status = 'SUCCEED'
                        AND to_char(t.transaction_date, 'yyyy-mm-dd') = '{tanggalMulai}' 
                        
		                {cusQuery}

                )
                -- Final query to combine both sets of data, grouped by cif_number and transaction_type
                SELECT 
                   
                    ltd.cif_number,
                    ltd.customer_name 
                  ,  ltd.customer_username 
                 , sum(ltd.last_transaction_amount) last_transaction_amount,
                 --to_char(    ltd.transaction_date, 'yyyy-mm-dd') transaction_date,
                      at.min_transaction_amount ,
                       at.max_transaction_amount,
                     at.avg_transaction_amount,
                     at.total_transaction_amount,
                     at.transaction_count,
                     adt.min_daily_transaction_amount,
                     adt.max_daily_transaction_amount,
                     adt.avg_daily_transaction_amount,
                     ltd.description,
	                 at.transaction_type
                FROM 
                    LastTransactionOnDate AS ltd
                LEFT JOIN 
                    AggregatedTransactions AS at 
                    ON ltd.cif_number = at.cif_number 
                    AND ltd.transaction_type = at.transaction_type
                LEFT JOIN 
                    AggregatedDailyTransactions AS adt
                    ON ltd.cif_number = adt.cif_number
                    AND ltd.transaction_type = adt.transaction_type
                group by   ltd.cif_number,
                    ltd.customer_name 
                  ,  ltd.customer_username 
                 , ltd.last_transaction_amount ,  
                     at.min_transaction_amount ,
                     at.max_transaction_amount,
                     at.avg_transaction_amount,
                     at.total_transaction_amount,
                     at.transaction_count,
                     adt.min_daily_transaction_amount,
                     adt.max_daily_transaction_amount,
                     adt.avg_daily_transaction_amount, 
                    --ltd.transaction_date, 
                     ltd.description,
                    at.transaction_type
                HAVING
                    ltd.last_transaction_amount > COALESCE(at.max_transaction_amount, 0) 
                    OR ltd.last_transaction_amount > COALESCE(at.avg_transaction_amount, 0);
";


                string queryperhari = $@"
                WITH AggregatedTransactions AS (
                    SELECT 
                        c.cif_number,
                        t.transaction_type,
                        MIN(t.transaction_amount) AS min_transaction_amount,
                        MAX(t.transaction_amount) AS max_transaction_amount,
                        SUM(t.transaction_amount) AS total_transaction_amount,
                        COUNT(t.transaction_amount) AS transaction_count,
                        SUM(t.transaction_amount) / NULLIF(COUNT(t.transaction_amount), 0) AS avg_transaction_amount
                    FROM 
                        ebanking.t_transaction AS t
                    LEFT JOIN 
                        ebanking.m_customer AS c ON t.createdby = c.id
                    LEFT JOIN 
                        ebanking.m_transaction_type AS m ON t.transaction_type = m.transaction_type
                    WHERE 
                        t.status = 'SUCCEED'
                        
                        AND t.transaction_date BETWEEN (DATE '{tanggalMulai}' - INTERVAL '1 day' - INTERVAL '{prPOLA_NOMINAL_TRANSAKSI_TIDAK_SESUAI.Value1} months') 
                        AND (DATE '{tanggalMulai}' - INTERVAL '1 day')
                    GROUP BY 
                        c.cif_number, 
                        t.transaction_type
                ),
                DailyTransactionAggregation AS (
                    SELECT
                        c.cif_number,
                        t.transaction_type,
                        DATE(t.transaction_date) AS transaction_date,
                        SUM(t.transaction_amount) AS daily_total_transaction
                    FROM 
                        ebanking.t_transaction AS t
                    LEFT JOIN 
                        ebanking.m_customer AS c ON t.createdby = c.id
                    WHERE 
                        t.status = 'SUCCEED'
                        AND t.transaction_date BETWEEN (DATE '{tanggalMulai}' - INTERVAL '1 day' - INTERVAL '{prPOLA_NOMINAL_TRANSAKSI_TIDAK_SESUAI.Value1} months') 
                        AND (DATE '{tanggalMulai}' - INTERVAL '1 day')
                    GROUP BY 
                        c.cif_number, t.transaction_type, DATE(t.transaction_date)
                ),
                AggregatedDailyTransactions AS (
                    SELECT 
                        d.cif_number,
                        d.transaction_type,
                        MIN(d.daily_total_transaction) AS min_daily_transaction_amount,
                        MAX(d.daily_total_transaction) AS max_daily_transaction_amount,
                        AVG(d.daily_total_transaction) AS avg_daily_transaction_amount
                    FROM 
                        DailyTransactionAggregation AS d
                    GROUP BY 
                        d.cif_number, d.transaction_type
                ),
                LastTransactionOnDate AS (
                    SELECT 
                        c.cif_number, 
                        c.customer_name, 
                        c.customer_username, 
                        SUM(t.transaction_amount) AS last_transaction_amount,
                        t.transaction_date,
                        m.description,
                        t.transaction_type
                    FROM 
                        ebanking.t_transaction AS t
                    LEFT JOIN 
                        ebanking.m_customer AS c ON t.createdby = c.id
                    LEFT JOIN 
                        ebanking.m_transaction_type AS m ON t.transaction_type = m.transaction_type
                    WHERE 
                        t.status = 'SUCCEED'
                        AND to_char(t.transaction_date, 'yyyy-mm-dd') = '{tanggalMulai}' 
                    GROUP BY 
                        c.cif_number, 
                        c.customer_name, 
                        c.customer_username, 
                        t.transaction_date, 
                        m.description, 
                        t.transaction_type
                )
                SELECT 
                    ltd.cif_number,
                    ltd.customer_name,
                    ltd.customer_username,
                    SUM(ltd.last_transaction_amount) AS total_last_transaction_amount,
                    at.min_transaction_amount,
                    at.max_transaction_amount,
                    at.avg_transaction_amount,
                    at.total_transaction_amount,
                    at.transaction_count,
                    adt.min_daily_transaction_amount,
                    adt.max_daily_transaction_amount,
                    adt.avg_daily_transaction_amount,
                    ltd.description,
                    at.transaction_type
                FROM 
                    LastTransactionOnDate AS ltd
                LEFT JOIN 
                    AggregatedTransactions AS at 
                    ON ltd.cif_number = at.cif_number 
                    AND ltd.transaction_type = at.transaction_type
                LEFT JOIN 
                    AggregatedDailyTransactions AS adt
                    ON ltd.cif_number = adt.cif_number
                    AND ltd.transaction_type = adt.transaction_type
                GROUP BY 
                    ltd.cif_number,
                    ltd.customer_name,
                    ltd.customer_username,
                    at.min_transaction_amount,
                    at.max_transaction_amount,
                    at.avg_transaction_amount,
                    at.total_transaction_amount,
                    at.transaction_count,
                    adt.min_daily_transaction_amount,
                    adt.max_daily_transaction_amount,
                    adt.avg_daily_transaction_amount,
                    ltd.description,
                    at.transaction_type
                HAVING
                    SUM(ltd.last_transaction_amount) > COALESCE(adt.max_daily_transaction_amount, 0) 
                    OR SUM(ltd.last_transaction_amount) > COALESCE(adt.avg_daily_transaction_amount, 0);
";

   


                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PolaTransaksiDebetSemua d = new PolaTransaksiDebetSemua
                            {
                                customer_name = reader["customer_name"].ToString(),
                                cif_number = reader["cif_number"].ToString(),
                                customer_username = reader["customer_username"].ToString(),
                                description = reader["description"].ToString(),

                                transaction_type = reader["transaction_type"]?.ToString() ?? string.Empty,
                                sum = reader["last_transaction_amount"] != DBNull.Value
                                      ? Convert.ToDecimal(reader["last_transaction_amount"]).ToString("C", new CultureInfo("id-ID"))
                                      : "0",
                                min = reader["min_transaction_amount"] != DBNull.Value
                                      ? Convert.ToDecimal(reader["min_transaction_amount"]).ToString("C", new CultureInfo("id-ID"))
                                      : "0",
                                max = reader["max_transaction_amount"] != DBNull.Value
                                      ? Convert.ToDecimal(reader["max_transaction_amount"]).ToString("C", new CultureInfo("id-ID"))
                                      : "0",
                                avg = reader["avg_transaction_amount"] != DBNull.Value
                                      ? Convert.ToDecimal(reader["avg_transaction_amount"]).ToString("C", new CultureInfo("id-ID"))
                                      : "0",
                                min_daily = reader["min_daily_transaction_amount"] != DBNull.Value
                                      ? Convert.ToDecimal(reader["min_daily_transaction_amount"]).ToString("C", new CultureInfo("id-ID"))
                                      : "0",
                                max_daily = reader["max_daily_transaction_amount"] != DBNull.Value
                                      ? Convert.ToDecimal(reader["max_daily_transaction_amount"]).ToString("C", new CultureInfo("id-ID"))
                                      : "0",
                                avg_daily = reader["avg_daily_transaction_amount"] != DBNull.Value
                                      ? Convert.ToDecimal(reader["avg_daily_transaction_amount"]).ToString("C", new CultureInfo("id-ID"))
                                      : "0",
                                last_transaction_amount = reader["last_transaction_amount"] != DBNull.Value
                                      ? Convert.ToDecimal(reader["last_transaction_amount"]).ToString("C", new CultureInfo("id-ID"))
                                      : "0",
                                transaction_count = reader["transaction_count"] != DBNull.Value
                                      ? Convert.ToDecimal(reader["transaction_count"]).ToString("C", new CultureInfo("id-ID"))
                                      : "0"
                            };

                            result.Add(d);
                        }
                    }
                }
                using (NpgsqlCommand cmd = new NpgsqlCommand(queryperhari, conn))
                {
                  
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PolaTransaksiDebetSemua hari = new PolaTransaksiDebetSemua
                            {
                                customer_name = reader["customer_name"].ToString(),
                                cif_number = reader["cif_number"].ToString(),
                                customer_username = reader["customer_username"].ToString(),
                                description = reader["description"].ToString(),
                                transaction_type = reader["transaction_type"]?.ToString() ?? string.Empty,
                                
                                min_daily = reader["min_daily_transaction_amount"] != DBNull.Value
                                      ? Convert.ToDecimal(reader["min_daily_transaction_amount"]).ToString("C", new CultureInfo("id-ID"))
                                      : "0",
                                max_daily = reader["max_daily_transaction_amount"] != DBNull.Value
                                      ? Convert.ToDecimal(reader["max_daily_transaction_amount"]).ToString("C", new CultureInfo("id-ID"))
                                      : "0",
                                avg_daily = reader["avg_daily_transaction_amount"] != DBNull.Value
                                      ? Convert.ToDecimal(reader["avg_daily_transaction_amount"]).ToString("C", new CultureInfo("id-ID"))
                                      : "0",
                                total_last_transaction_amount = reader["total_last_transaction_amount"] != DBNull.Value
                                      ? Convert.ToDecimal(reader["total_last_transaction_amount"]).ToString("C", new CultureInfo("id-ID"))
                                      : "0",
                                transaction_count = reader["transaction_count"] != DBNull.Value
                                      ? Convert.ToDecimal(reader["transaction_count"]).ToString("C", new CultureInfo("id-ID"))
                                      : "0"
                            };

                            resultperhari.Add(hari);
                        }
                    }
                }
            }

            // Bind hasil ke ListView
            ListViewCari.DataSource = result;
            ListViewCari.DataBind();

            ListViewPerHari.DataSource = resultperhari;
            ListViewPerHari.DataBind();
        }

        protected void ListViewData_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {

        }

        protected void ButtonOKConfirm_Click(object sender, EventArgs e)
        {

        }
    }
}
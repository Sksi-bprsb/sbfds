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

namespace SBFDSys
{
    public partial class PolaTransaksiDebetSemuaDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
            List<PolaTransaksiDebetSemua> result = new List<PolaTransaksiDebetSemua>();
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                WITH CustomerAvg AS (
                        SELECT 
                            c.cif_number,
                            AVG(t.transaction_amount) AS avg_amount
                        FROM 
                            ebanking.t_transaction AS t
                        LEFT JOIN 
                            ebanking.m_customer AS c ON t.createdby = c.id
                        WHERE 
                            t.status = 'SUCCEED' 
                            AND t.transaction_date >= CURRENT_DATE - INTERVAL '6 months'
                        GROUP BY 
                            c.cif_number
                    ),
                    TransactionStats AS (
                        SELECT 
                            c.cif_number, 
                            c.customer_name, 
                            c.customer_username, 
                            t.transaction_type, 
                            m.description, 
                            COUNT(t.transaction_type) AS transaction_count,
                            SUM(t.transaction_amount) AS total_transaction_amount, 
                            MIN(t.transaction_amount) AS min_transaction_amount, 
                            MAX(t.transaction_amount) AS max_transaction_amount, 
                            AVG(t.transaction_amount) AS avg_transaction_amount,
                            ca.avg_amount AS customer_avg_amount
                        FROM 
                            ebanking.t_transaction AS t
                        LEFT JOIN 
                            ebanking.m_customer AS c ON t.createdby = c.id
                        LEFT JOIN 
                            ebanking.m_transaction_type AS m ON t.transaction_type = m.transaction_type
                        LEFT JOIN 
                            CustomerAvg AS ca ON c.cif_number = ca.cif_number
                        WHERE 
                            t.status = 'SUCCEED'
                            AND t.transaction_date >= CURRENT_DATE - INTERVAL '6 months'
                        GROUP BY 
                            c.cif_number, 
                            c.customer_name, 
                            c.customer_username, 
                            t.transaction_type, 
                            m.description, 
                            ca.avg_amount
                    )
                    SELECT *
                    FROM TransactionStats
                    WHERE total_transaction_amount > customer_avg_amount
                    ORDER BY cif_number ASC, transaction_type ASC;



                    ", conn))

                {
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
                            d.count = reader["transaction_count"].ToString();
                            d.sum = Convert.ToDecimal(reader["total_transaction_amount"]).ToString("C", new CultureInfo("id-ID"));
                                d.min = Convert.ToDecimal(reader["min_transaction_amount"]).ToString("C", new CultureInfo("id-ID"));
                                d.max = Convert.ToDecimal(reader["max_transaction_amount"]).ToString("C", new CultureInfo("id-ID"));
                                d.avg = Convert.ToDecimal(reader["avg_transaction_amount"]).ToString("C", new CultureInfo("id-ID"));
                            //d.date = reader["date"].ToString();
                            result.Add(d);
                        }
                    }
                }
            }
            ListViewData.DataSource = result;
            ListViewData.DataBind();
        }



        protected void ButtonSearchTransaksi_Click(object sender, EventArgs e)
        {

        }

        protected void ListViewData_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {

        }

        protected void ButtonOKConfirm_Click(object sender, EventArgs e)
        {

        }
    }
}
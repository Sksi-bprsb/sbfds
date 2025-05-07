using System;
using Npgsql;
using System.Net;
using System.Net.Mail;
using BPRSB.Kuesioner.Repository;
using FDS.Library.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SBFDSys
{
    public partial class Detections : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            {
                string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
                List<Detection> result = new List<Detection>();
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM ebanking.t_transaction", conn))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Detection d = new Detection();
                                
                                d.m_customer_id= Convert.ToInt32( reader["m_customer_id"]);
                                d.transaction_type = reader["transaction_type"].ToString();
                                d.transaction_amount = Convert.ToInt32(reader["transaction_amount"]);
                                d.transaction_date = Convert.ToDateTime(reader["transaction_date"]);
                                d.biller_name = reader["biller_name"].ToString();
                                d.Status = reader["Status"].ToString();
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

        }

        protected void ListViewData_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {

        }

        protected void ButtonOKConfirm_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonKirimData_Click(object sender, EventArgs e)
        {
            
        }
    }
}
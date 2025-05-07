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
    public partial class StatusLogins : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            {
                string connString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];
                List<StatusLogin> result = new List<StatusLogin>();
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM ebanking.m_customer order by customer_name ASC", conn))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StatusLogin d = new StatusLogin();
                                
                                d.customer_name= reader["customer_name"].ToString();
                                d.ib_status = reader["ib_status"].ToString();
                                d.mb_status = reader["mb_status"].ToString();
                                d.last_login = reader["last_login"].ToString();
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
    }
}
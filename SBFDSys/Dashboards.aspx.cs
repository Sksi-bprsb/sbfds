using FDS.Library.Model;
using SBFDSys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BPRSB.Kuesioner.Repository;
using System.Data;
using System.Drawing;
using System.IO;
using System.Collections;
using System.Text;
using Npgsql; // Corrected the namespace for PostgreSQL
using System.EnterpriseServices.CompensatingResourceManager;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Threading.Tasks;
using System.Net;

namespace SBFDSys
{
    public partial class Dashboards : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {

                //if (Request.QueryString["modalClosed"] == "1")
                
                    // Refresh halaman atau lakukan operasi tambahan
                    LoadLatestTransactionDate();
                    LoadLatestTransactionDate();

                    LoadTodayTransactionCount();

                    LoadMonthlyTransaction();


            }
        }
        
        public void RestoreDB(int milisecond)
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            MySqlRepository.Insert(new Log() { Keterangan = "Restore DB ", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username,Nik=user.Nik,NamaKaryawan=user.NamaKaryawan });
           
            
            System.Diagnostics.Process.Start("C:\\Users\\SKSI Dev\\Desktop\\SBFDSys\\SBFDSys\\RestoreDB\\Restore.bat");
            System.Threading.Thread.Sleep(milisecond);
            
        }
        
        private void LoadLatestTransactionDate()
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"]; // Replace with your actual connection string

            string query = "SELECT MAX(transaction_date) AS LatestDate FROM ebanking.t_transaction"; // Replace with the correct table and column names
            // Use NpgsqlConnection instead of SqlConnection
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn); // Use NpgsqlCommand instead of SqlCommand
                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    // Check if a result was returned, then update the label
                    if (result != DBNull.Value && result != null)
                    {
                        DateTime latestTransactionDate = Convert.ToDateTime(result);
                        lblLatestTransactionDate.Text = $"Latest Transaction Date : <br> {latestTransactionDate.ToString("yyyy-MM-dd")}";
                    }
                    else
                    {
                        lblLatestTransactionDate.Text = "No transactions found.";
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (optional)
                    lblLatestTransactionDate.Text = "Error retrieving data: " + ex.Message;
                }
            }
        }
        private void LoadTodayTransactionCount()
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];

            string query = "SELECT COUNT(*) AS TransactionCount FROM ebanking.t_transaction WHERE transaction_date::DATE = CURRENT_DATE - INTERVAL '1 day'";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    // Check if a result was returned, then update the label
                    if (result != DBNull.Value && result != null)
                    {
                        int transactionCount = Convert.ToInt32(result);
                        LoadCountTransactionDate.Text = $"Total Transactions Today : <br> {transactionCount} Transaction";
                    }
                    else
                    {
                        LoadCountTransactionDate.Text = "No transactions today.";
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (optional)
                    LoadCountTransactionDate.Text = "Error retrieving data: " + ex.Message;
                }
            }
        }
        private void LoadMonthlyTransaction()
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["PostgreSqlConnectionString"];

            string query = "SELECT COUNT(*) AS TransactionCount FROM ebanking.t_transaction WHERE transaction_date >= DATE_TRUNC('month', CURRENT_DATE)";


            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    // Check if a result was returned, then update the label
                    if (result != DBNull.Value && result != null)
                    {
                        int transactionCount = Convert.ToInt32(result);
                        LoadBulanTransaction.Text = $"Total Transactions This Month : <br> {transactionCount} Transaction";
                    }
                    else
                    {
                        LoadBulanTransaction.Text = "No transactions this Month.";
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (optional)
                    LoadBulanTransaction.Text = "Error retrieving data: " + ex.Message;
                }
            }
        }
        

    }
}

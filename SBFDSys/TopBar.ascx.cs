using FDS.Library.Model;
using BPRSB.Kuesioner.Repository;
using SBFDSys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using Npgsql;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace SBFDSys
{
    public partial class TopBar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUser"] != null)
                loadUserAccess();
        }
        private void loadUserAccess()
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            UserGroup group = MySqlRepository.GetById<UserGroup>(Convert.ToInt32(user.UserGroupId));

            menuRestore.Visible = group.GetBoolValue("restoredb", "tampil");
            //menuLog.Visible = group.GetBoolValue("user", "tampil");
            //LabelUserGroup.Visible = group.GetBoolValue("UserGroupSetting", "tampil");
            LabelUserKar.Text = user.NamaKaryawan;
        }

        protected void ButtonLogOut_Click(object sender, EventArgs e)
        {
            Session["LoginUser"] = null;
            Response.Redirect("default.aspx");
        }

        protected void ButtonUbahPassword_Click(object sender, EventArgs e)
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            MySqlRepository.Insert(new Log() { Keterangan = "UBAH PASSWORD ", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username, Nik=user.Nik,NamaKaryawan=user.NamaKaryawan });
            try
            {

                MasterUser master = (MasterUser)Session["LoginUser"];
                string newPasswordHash = Tools.MD5Hash(TextBoxPassBaru.Text.Trim());

                string message = "";

                if (newPasswordHash == master.Password)
                    message += " * Password tidak boleh sama dengan password lama.";
                if (TextBoxPassBaru.Text != TextBoxUlangPass.Text)
                    message += " * Password tidak sesuai, ulangi!";
                if (TextBoxPassBaru.Text.Length < 6)
                    message += "* Password Min 6 Digit. ";
                if (!TextBoxPassBaru.Text.Any(char.IsUpper))
                    message += "Password Harus Memiliki Minimal 1 Karakter Huruf Kapital.";
                if (!TextBoxPassBaru.Text.Any(char.IsLower))
                    message += "Password Harus Memiliki Karakter Huruf Kecil.";
                master.Password = Tools.MD5Hash(TextBoxPassBaru.Text.Trim());
                if (message.Length > 0)
                    throw new Exception(message);

                master.Active = true;
                MySqlRepository.InsertUpdate(master);

                //Session["LoginUser"] = master;
                Response.Redirect("default.aspx");
            }
            catch (Exception x)
            {
                Label1.Text = x.Message;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "errorpass", "$(document).ready(function () {$('#errorpass').modal();});", true);
            }

        }

        protected void LinkButtonUbahPassword_Click(object sender, EventArgs e)
        {

        }

        protected void buttonReTry_Click(object sender, EventArgs e)
        {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ModalGantiPassword", "$(document).ready(function () {$('#ModalGantiPassword').modal();});", true);
            
        }
        public void RestoreDB(int milisecond)
        { 
            MasterUser user = (MasterUser)Session["LoginUser"];
            MySqlRepository.Insert(new Log() { Keterangan = "Restore DB ", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username, Nik=user.Nik, NamaKaryawan=user.NamaKaryawan });

            try
            {
                //throw new Exception("Test error...");
                string restoreDB = ConfigurationManager.AppSettings["RestoreDB"];
                System.Diagnostics.Process.Start(restoreDB);

                //System.Diagnostics.Process.Start("C:\\Users\\SKSI Dev\\Documents\\FDS\\RestoreDB\\Restore.bat");
                //System.Diagnostics.Process.Start("C:\\Users\\SKSI Dev\\Documents\\FDS\\RestoreDB\\Restore.bat");

                System.Threading.Thread.Sleep(milisecond);
                //Process.WaitForExit();
            }
            catch (Exception ex)
            {
                MySqlRepository.Insert(new Log()
                {
                    Keterangan = $"Restore DB Error: {ex.Message}",
                    Tanggal = DateTime.Now,
                    MasterUserId = user.MasterUserId,
                    Username = user.Username,
                    Nik = user.Nik,
                    NamaKaryawan=user.NamaKaryawan
                    
                });

                // Tampilkan pesan error jika diperlukan
                throw new Exception("Proses restore database gagal. Silakan periksa file batch atau log untuk informasi lebih lanjut.");
            }
            Response.Redirect(Request.RawUrl);
        }
        protected void btnRestore_Click(object sender, EventArgs e)
        {
            if (fileBackup.HasFile)
            {
                try
                {
                    // Mendapatkan path file yang diupload
                    string filePath = Path.Combine(Server.MapPath("~/Uploads"), fileBackup.FileName);
                    string uploadedFile = Path.Combine(Server.MapPath("~/Uploads"), "newdata.dump");
                    fileBackup.SaveAs(uploadedFile);


                    RestoreDB(30000);
                    
                    return; 

                    // Informasi database PostgreSQL dan MySQL
                    string pgConnString = ConfigurationManager.ConnectionStrings["PgConnectionString"].ConnectionString;
                    string mysqlConnString = ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString;

                    // Koneksi ke PostgreSQL
                    using (var pgConn = new NpgsqlConnection(pgConnString))
                    {
                        pgConn.Open();

                        // Mengambil data dari PostgreSQL dan menulis ke file SQL
                        string selectQuery = "SELECT id, name, email FROM customer"; // Ganti query sesuai kebutuhan
                        using (var pgCmd = new NpgsqlCommand(selectQuery, pgConn))
                        using (var reader = pgCmd.ExecuteReader())
                        {
                            using (StreamWriter writer = new StreamWriter(filePath))
                            {
                                while (reader.Read())
                                {
                                    string insertQuery = $"INSERT INTO customer (id, name, email) VALUES ('{reader["id"]}', '{reader["name"]}', '{reader["email"]}');";
                                    writer.WriteLine(insertQuery); // Tulis query insert ke file
                                }
                            }
                        }
                    }

                    // Koneksi ke MySQL
                    using (var mysqlConn = new MySqlConnection(mysqlConnString))
                    {
                        mysqlConn.Open();

                        // Jalankan perintah restore menggunakan file SQL
                        string mysqlRestoreCommand = $"mysql -u {mysqlConnString} -p{mysqlConnString} {mysqlConn.Database} < {filePath}";
                        ProcessStartInfo processStartInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            Arguments = "/C " + mysqlRestoreCommand, // Menjalankan perintah di CMD
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        };

                        // Menjalankan proses restore ke MySQL
                        using (Process process = Process.Start(processStartInfo))
                        {
                            string output = process.StandardOutput.ReadToEnd();
                            string error = process.StandardError.ReadToEnd();
                            process.WaitForExit();

                            if (!string.IsNullOrEmpty(error))
                            {
                                DisplayStatusMessage($"Restore failed: {error}");
                                LogDetailedInfo($"Restore Error: {error}");
                            }
                            else
                            {
                                DisplayStatusMessage("Restore completed successfully.");
                            }
                        }
                        
                    }
                }
                
                catch (Exception ex)
                {
                    DisplayStatusMessage($"Error: {ex.Message}");
                    LogDetailedInfo($"Exception: {ex}");
                }
                
            }
            else
            {
                DisplayStatusMessage("Please select a backup file.");
            }
        }

        // Fungsi untuk menampilkan status
        private void DisplayStatusMessage(string message)
        {
            statusMessage.InnerText = message;
        }

        // Fungsi untuk logging detail
        private void LogDetailedInfo(string message)
        {
            string logPath = Server.MapPath("~/Logs/restore_log.txt");
            File.AppendAllText(logPath, $"{DateTime.Now}: {message}{Environment.NewLine}");
        }

    }
    
}
using System;
using BPRSB.Kuesioner.Repository;
using FDS.Library.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SBFDSys
{
    public partial class MasterParameters : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUser"] == null)
                Response.Redirect("default.aspx");
            else if (!IsPostBack)
            {
                loadApproval();
                loadApprovalTrx12();
                loadApprovalAktifitas12();
                loadApprovalAktBerulangLogin();
                loadApprovalTrxMelebihi30Juta();
                loadApprovalLoginBersamaDeviceBerbeda();
                loadApprovalSalahPasswor();
                loadApprovalUserBlokir();
                loadApprovalPolaNominalTransaksi();
                loadApprovalPolaFrekuensiTransaksi();
                LoadData();
            }

        }
        private void loadApproval()
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            List<MasterUser> d = MySqlRepository.GetAll<MasterUser>("");

            DropDownListApproval.DataValueField = "MasterUserId";
            DropDownListApproval.DataTextField = "NamaKaryawan";
            DropDownListApproval.DataSource = d;
            DropDownListApproval.DataBind();
        }
        private void loadApprovalTrx12()
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            List<MasterUser> d = MySqlRepository.GetAll<MasterUser>("");

            DropDownListApprovalTrx12.DataValueField = "MasterUserId";
            DropDownListApprovalTrx12.DataTextField = "NamaKaryawan";
            DropDownListApprovalTrx12.DataSource = d;
            DropDownListApprovalTrx12.DataBind();
        }
        private void loadApprovalAktifitas12()
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            List<MasterUser> d = MySqlRepository.GetAll<MasterUser>("");

            DropDownListAktifitas12.DataValueField = "MasterUserId";
            DropDownListAktifitas12.DataTextField = "NamaKaryawan";
            DropDownListAktifitas12.DataSource = d;
            DropDownListAktifitas12.DataBind();
        }
        private void loadApprovalAktBerulangLogin()
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            List<MasterUser> d = MySqlRepository.GetAll<MasterUser>("");

            DropDownListAktBerulangLogin.DataValueField = "MasterUserId";
            DropDownListAktBerulangLogin.DataTextField = "NamaKaryawan";
            DropDownListAktBerulangLogin.DataSource = d;
            DropDownListAktBerulangLogin.DataBind();
        }
        private void loadApprovalTrxMelebihi30Juta()
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            List<MasterUser> d = MySqlRepository.GetAll<MasterUser>("");

            DropDownListTrxMelebihi30Juta.DataValueField = "MasterUserId";
            DropDownListTrxMelebihi30Juta.DataTextField = "NamaKaryawan";
            DropDownListTrxMelebihi30Juta.DataSource = d;
            DropDownListTrxMelebihi30Juta.DataBind();
        }
        private void loadApprovalLoginBersamaDeviceBerbeda()
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            List<MasterUser> d = MySqlRepository.GetAll<MasterUser>("");

            DropDownListLoginBersamaDeviceBerbeda.DataValueField = "MasterUserId";
            DropDownListLoginBersamaDeviceBerbeda.DataTextField = "NamaKaryawan";
            DropDownListLoginBersamaDeviceBerbeda.DataSource = d;
            DropDownListLoginBersamaDeviceBerbeda.DataBind();
        }
        private void loadApprovalSalahPasswor()
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            List<MasterUser> d = MySqlRepository.GetAll<MasterUser>("");

            DropDownListApprovalSalahPassword.DataValueField = "MasterUserId";
            DropDownListApprovalSalahPassword.DataTextField = "NamaKaryawan";
            DropDownListApprovalSalahPassword.DataSource = d;
            DropDownListApprovalSalahPassword.DataBind();
        }
        private void loadApprovalUserBlokir()
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            List<MasterUser> d = MySqlRepository.GetAll<MasterUser>("");

            DropDownListApprovalUserBlokir.DataValueField = "MasterUserId";
            DropDownListApprovalUserBlokir.DataTextField = "NamaKaryawan";
            DropDownListApprovalUserBlokir.DataSource = d;
            DropDownListApprovalUserBlokir.DataBind();
            
        }
        private void loadApprovalPolaNominalTransaksi()
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            List<MasterUser> d = MySqlRepository.GetAll<MasterUser>("");

            DropDownListApprovalPolaNominal.DataValueField = "MasterUserId";
            DropDownListApprovalPolaNominal.DataTextField = "NamaKaryawan";
            DropDownListApprovalPolaNominal.DataSource = d;
            DropDownListApprovalPolaNominal.DataBind();
        }
        private void loadApprovalPolaFrekuensiTransaksi()
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            List<MasterUser> d = MySqlRepository.GetAll<MasterUser>("");

            DropDownListApprovalPolaFrekuensi.DataValueField = "MasterUserId";
            DropDownListApprovalPolaFrekuensi.DataTextField = "NamaKaryawan";
            DropDownListApprovalPolaFrekuensi.DataSource = d;
            DropDownListApprovalPolaFrekuensi.DataBind();

        }
        public void LoadData()
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            UserGroup group = MySqlRepository.GetById<UserGroup>(Convert.ToInt32(user.UserGroupId));
            //List<Cabang> das = MySqlRepository.GetAll<Cabang>("NamaCabang like '%" + TextBoxSearch.Text + "%' ");

            //foreach (Cabang r in das)
            //    r.Active = group.GetBoolValue("master", "hapus");
            //Session["List_Cabang"] = das;



            FDS.Library.Model.ParameterPending prTRANSAKSI_BELI_PULSA_Pending = MySqlRepository.GetTopOne<FDS.Library.Model.ParameterPending>("Nama='TRANSAKSI_BELI_PULSA'");
            Session["TRANSAKSI_BELI_PULSA"] = prTRANSAKSI_BELI_PULSA_Pending;
            TextboxBeliPulsa.Text = "TRANSAKSI_BELI_PULSA";
            TextboxValue1.Text = prTRANSAKSI_BELI_PULSA_Pending.Value1;
            //DropDownListApproval.SelectedItem.Text = DropDownListApproval.Text;

            FDS.Library.Model.ParameterPending prTRANSAKSI_DIATAS_JAM_12_MALAM = MySqlRepository.GetTopOne<FDS.Library.Model.ParameterPending>("Nama='TRANSAKSI_DIATAS_JAM_12_MALAM'");
            Session["TRANSAKSI_DIATAS_JAM_12_MALAM"] = prTRANSAKSI_DIATAS_JAM_12_MALAM;
            TextboxTrxMalam.Text = "TRANSAKSI_DIATAS_JAM_12_MALAM";
            TextboxJamDari.Text = prTRANSAKSI_DIATAS_JAM_12_MALAM.Value1;
            TextboxJamSampai.Text = prTRANSAKSI_DIATAS_JAM_12_MALAM.Value2;

            FDS.Library.Model.ParameterPending prAKTIFITAS_DIATAS_JAM_12_MALAM = MySqlRepository.GetTopOne<FDS.Library.Model.ParameterPending>("Nama='AKTIFITAS_DIATAS_JAM_12_MALAM'");
            Session["AKTIFITAS_DIATAS_JAM_12_MALAM"] = prAKTIFITAS_DIATAS_JAM_12_MALAM;
            TextboxAktifitasMalam.Text = "AKTIFITAS_DIATAS_JAM_12_MALAM";
            TextboxDariJam.Text = prAKTIFITAS_DIATAS_JAM_12_MALAM.Value1;
            TextboxSampaiJam.Text = prAKTIFITAS_DIATAS_JAM_12_MALAM.Value2;

            FDS.Library.Model.ParameterPending prAKTIFITAS_BERULANG_LOGIN_SUKSES = MySqlRepository.GetTopOne<FDS.Library.Model.ParameterPending>("Nama='AKTIFITAS_BERULANG_LOGIN_SUKSES'");
            Session["AKTIFITAS_BERULANG_LOGIN_SUKSES"] = prAKTIFITAS_BERULANG_LOGIN_SUKSES;
            TextboxAktifitasBerulangLogin.Text = "AKTIFITAS_BERULANG_LOGIN_SUKSES";
            TextboxHari.Text = prAKTIFITAS_BERULANG_LOGIN_SUKSES.Value1;

            FDS.Library.Model.ParameterPending prTRANSAKSI_MELEBIHI_30_JUTA = MySqlRepository.GetTopOne<FDS.Library.Model.ParameterPending>("Nama='TRANSAKSI_MELEBIHI_30_JUTA'");
            Session["TRANSAKSI_MELEBIHI_30_JUTA"] = prTRANSAKSI_MELEBIHI_30_JUTA;
            TextboxNamaTrx.Text = "TRANSAKSI_MELEBIHI_30_JUTA";
            TextboxMelebihi30Juta.Text = prTRANSAKSI_MELEBIHI_30_JUTA.Value1;

            FDS.Library.Model.ParameterPending prLOGIN_BERSAMAAN_CHANNEL_BERBEDA = MySqlRepository.GetTopOne<FDS.Library.Model.ParameterPending>("Nama='LOGIN_BERSAMAAN_CHANNEL_BERBEDA'");
            Session["LOGIN_BERSAMAAN_CHANNEL_BERBEDA"] = prLOGIN_BERSAMAAN_CHANNEL_BERBEDA;
            TextboxLoginBersama.Text = "LOGIN_BERSAMAAN_CHANNEL_BERBEDA";
            TextboxWaktu.Text = prLOGIN_BERSAMAAN_CHANNEL_BERBEDA.Value1;

            FDS.Library.Model.ParameterPending prSALAH_PASSWORD = MySqlRepository.GetTopOne<FDS.Library.Model.ParameterPending>("Nama='SALAH_PASSWORD'");
            Session["SALAH_PASSWORD"] = prSALAH_PASSWORD;
            TextboxSalahPassword.Text = "SALAH_PASSWORD";
            TextboxAktifitas.Text = prSALAH_PASSWORD.Value1;

            FDS.Library.Model.ParameterPending prLOGIN_USER_BLOKIR = MySqlRepository.GetTopOne<FDS.Library.Model.ParameterPending>("Nama='LOGIN_USER_BLOKIR'");
            Session["LOGIN_USER_BLOKIR"] = prLOGIN_USER_BLOKIR;
            TextboxNamaUserBlokir.Text = "LOGIN_USER_BLOKIR";
            DropDownListStatusIbMb.SelectedItem.Text = prLOGIN_USER_BLOKIR.Value1;

            FDS.Library.Model.ParameterPending prPOLA_NOMINAL_TRANSAKSI_TIDAK_SESUAI = MySqlRepository.GetTopOne<FDS.Library.Model.ParameterPending>("Nama='POLA_NOMINAL_TRANSAKSI_TIDAK_SESUAI'");
            Session["POLA_NOMINAL_TRANSAKSI_TIDAK_SESUAI"] = prPOLA_NOMINAL_TRANSAKSI_TIDAK_SESUAI;
            TextboxPolaTransaksi.Text = "POLA_NOMINAL_TRANSAKSI_TIDAK_SESUAI";
            TextboxPolaBulan.Text = prPOLA_NOMINAL_TRANSAKSI_TIDAK_SESUAI.Value1;

            FDS.Library.Model.ParameterPending prPOLA_FREKUENSI_TRANSAKSI_TIDAK_SESUAI = MySqlRepository.GetTopOne<FDS.Library.Model.ParameterPending>("Nama='POLA_FREKUENSI_TRANSAKSI_TIDAK_SESUAI'");
            Session["POLA_FREKUENSI_TRANSAKSI_TIDAK_SESUAI"] = prPOLA_FREKUENSI_TRANSAKSI_TIDAK_SESUAI;
            TextboxPolaFrekuensi.Text = "POLA_FREKUENSI_TRANSAKSI_TIDAK_SESUAI";
            TextboxPolaBulanFrekuensi.Text = prPOLA_FREKUENSI_TRANSAKSI_TIDAK_SESUAI.Value1;
        }


        public void LoadDataTidakAktif()
        {
            
        }
        protected void ButtonOKConfirm_Click(object sender, EventArgs e)
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            MySqlRepository.Insert(new Log() { Keterangan = "UBAH PARAMETER TRANSAKSI_BELI_PULSA", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username, Nik = user.Nik, NamaKaryawan = user.NamaKaryawan });

            FDS.Library.Model.ParameterPending p = (FDS.Library.Model.ParameterPending)Session["TRANSAKSI_BELI_PULSA"];
            p.Value1 = TextboxValue1.Text;
            p.Status = "PENDING";
            p.ApprovalNama = DropDownListApproval.SelectedItem.Text;
            MySqlRepository.Update(p);
        }
        
        protected void ButtonSearchMaster_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void ButtonDeleteMaster_Click(object sender, EventArgs e)
        {
            
        }

        protected void ButtonUpdateMaster_Click(object sender, EventArgs e)
        {
            
        }

        protected void ButtonAktifkanMaster_Click(object sender, EventArgs e)
        {
            
        }

        protected void ButtonOkAktif_Click(object sender, EventArgs e)
        {
           
        }

        protected void ButtonTrxMalam_Click(object sender, EventArgs e)
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            MySqlRepository.Insert(new Log() { Keterangan = "UBAH PARAMETER TRANSAKSI_DIATAS_JAM_12_MALAM", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username, Nik = user.Nik, NamaKaryawan = user.NamaKaryawan });
            
            FDS.Library.Model.ParameterPending p = (FDS.Library.Model.ParameterPending)Session["TRANSAKSI_DIATAS_JAM_12_MALAM"];
            p.Value1 = TextboxJamDari.Text;
            p.Value2 = TextboxJamSampai.Text;
            p.Status = "PENDING";
            p.ApprovalNama = DropDownListApprovalTrx12.SelectedItem.Text;
            MySqlRepository.Update(p);
            
        }

        protected void ButtonAktifitasMalam_Click(object sender, EventArgs e)
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            MySqlRepository.Insert(new Log() { Keterangan = "UBAH PARAMETER AKTIFITAS_DIATAS_JAM_12_MALAM", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username, Nik = user.Nik, NamaKaryawan = user.NamaKaryawan });
            
            FDS.Library.Model.ParameterPending p = (FDS.Library.Model.ParameterPending)Session["AKTIFITAS_DIATAS_JAM_12_MALAM"];
            p.Value1 = TextboxDariJam.Text;
            p.Value2 = TextboxSampaiJam.Text;
            p.Status = "PENDING";
            p.ApprovalNama = DropDownListAktifitas12.SelectedItem.Text;
            MySqlRepository.Update(p);
        }

        protected void ButtonAktifitasBerulangLogin_Click(object sender, EventArgs e)
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            MySqlRepository.Insert(new Log() { Keterangan = "UBAH PARAMETER AKTIFITAS_BERULANG_LOGIN_SUKSES", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username, Nik = user.Nik, NamaKaryawan = user.NamaKaryawan });
            
            FDS.Library.Model.ParameterPending p = (FDS.Library.Model.ParameterPending)Session["AKTIFITAS_BERULANG_LOGIN_SUKSES"];
            p.Value1 = TextboxHari.Text;
            p.Status = "PENDING";
            p.ApprovalNama = DropDownListAktBerulangLogin.SelectedItem.Text;
            MySqlRepository.Update(p);
        }

        protected void ButtonTrxMelebihi30Juta_Click(object sender, EventArgs e)
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            MySqlRepository.Insert(new Log() { Keterangan = "UBAH PARAMETER TRANSAKSI_MELEBIHI_30_JUTA", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username, Nik = user.Nik, NamaKaryawan = user.NamaKaryawan });
            
            FDS.Library.Model.ParameterPending p = (FDS.Library.Model.ParameterPending)Session["TRANSAKSI_MELEBIHI_30_JUTA"];
            p.Value1 = TextboxMelebihi30Juta.Text.Replace(".","");
            p.Status = "PENDING";
            p.ApprovalNama = DropDownListTrxMelebihi30Juta.SelectedItem.Text;
            MySqlRepository.Update(p);
        }

        protected void ButtonLoginBesamaan_Click(object sender, EventArgs e)
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            MySqlRepository.Insert(new Log() { Keterangan = "UBAH PARAMETER LOGIN_BERSAMAAN_CHANNEL_BERBEDA", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username , Nik = user.Nik, NamaKaryawan = user.NamaKaryawan });
            FDS.Library.Model.ParameterPending p = (FDS.Library.Model.ParameterPending)Session["LOGIN_BERSAMAAN_CHANNEL_BERBEDA"];
            p.Value1 = TextboxWaktu.Text;
            p.Status = "PENDING";
            p.ApprovalNama = DropDownListLoginBersamaDeviceBerbeda.SelectedItem.Text;
            MySqlRepository.Update(p);
        }

        protected void ButtonOkUserBlokir_Click(object sender, EventArgs e)
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            MySqlRepository.Insert(new Log() { Keterangan = "UBAH PARAMETER LOGIN USER BLOKIR", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username, Nik = user.Nik, NamaKaryawan = user.NamaKaryawan });
            FDS.Library.Model.ParameterPending p = (FDS.Library.Model.ParameterPending)Session["LOGIN_USER_BLOKIR"];
            p.Value1 = DropDownListStatusIbMb.SelectedItem.Text;
            p.Status = "PENDING";
            p.ApprovalNama = DropDownListApprovalUserBlokir.SelectedItem.Text;
            MySqlRepository.Update(p);
        }

        protected void ButtonOkSalahPassword_Click(object sender, EventArgs e)
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            MySqlRepository.Insert(new Log() { Keterangan = "UBAH PARAMETER SALAH PASSWORD", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username, Nik = user.Nik, NamaKaryawan = user.NamaKaryawan });
            FDS.Library.Model.ParameterPending p = (FDS.Library.Model.ParameterPending)Session["SALAH_PASSWORD"];
            p.Value1 = TextboxAktifitas.Text;
            p.Status = "PENDING";
            p.ApprovalNama = DropDownListApprovalSalahPassword.SelectedItem.Text;
            MySqlRepository.Update(p);
        }
        

        protected void ButtonPolaTransaksiBulan_Click(object sender, EventArgs e)
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            MySqlRepository.Insert(new Log() { Keterangan = "UBAH PARAMETER POLA_NOMINAL_TRANSAKSI_TIDAK_SESUAI", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username, Nik = user.Nik, NamaKaryawan = user.NamaKaryawan });
            FDS.Library.Model.ParameterPending p = (FDS.Library.Model.ParameterPending)Session["POLA_NOMINAL_TRANSAKSI_TIDAK_SESUAI"];
            p.Value1 = TextboxPolaBulan.Text;
            p.Status = "PENDING";
            p.ApprovalNama = DropDownListApprovalPolaNominal.SelectedItem.Text;
            MySqlRepository.Update(p);
        }

        protected void ButtonPolaFrekuensi_Click(object sender, EventArgs e)
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            MySqlRepository.Insert(new Log() { Keterangan = "UBAH PARAMETER POLA_FREKUENSI_TRANSAKSI_TIDAK_SESUAI", Tanggal = DateTime.Now, MasterUserId = user.MasterUserId, Username = user.Username, Nik = user.Nik, NamaKaryawan = user.NamaKaryawan });
            FDS.Library.Model.ParameterPending p = (FDS.Library.Model.ParameterPending)Session["POLA_FREKUENSI_TRANSAKSI_TIDAK_SESUAI"];
            p.Value1 = TextboxPolaBulanFrekuensi.Text;
            p.Status = "PENDING";
            p.ApprovalNama = DropDownListApprovalPolaFrekuensi.SelectedItem.Text;
            MySqlRepository.Update(p);
        }
    }
}
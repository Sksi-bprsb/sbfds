<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SideBarAdmin.ascx.cs" Inherits="SBFDSys.SideBarAdmin" %>

<ul class="navbar-nav bg-danger sidebar sidebar-dark accordion" id="accordionSidebar">
    <a class="sidebar-brand d-flex align-items-center justify-content-center" href="Dashboards.aspx">
        <div class="sidebar-brand-icon rotate-n-15">
            <i class="fas fa-laugh-wink"></i>
        </div>
        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/css/bootstrap.min.css">
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/js/bootstrap.bundle.min.js"></script>
        <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.3.1/css/all.css">
    </a>  

    <hr class="sidebar-divider my-0">

    <li class="nav-item">
        <a class="nav-link" href="Dashboards.aspx"><i class="fas fa-fw fa-table"></i><span>Dashboard</span></a>
    </li>

    <hr class="sidebar-divider">

    <li class="nav-item dropdown">
        <a class="nav-link dropdown-toggle" href="#" id="navbarScrollingDropdown" role="button" data-toggle="dropdown" aria-expanded="false">
                <i class="fas fa-fw fa-paper-plane"></i>
                <span>Master</span>
        </a>
        <ul class="dropdown-menu" aria-labelledby="navbarScrollingDropdown">
           <asp:Label runat="server" ID="LabelParameter"><a class="nav-link text-dark" href="MasterParameters.aspx"><i class="fas fa-fw fa-table text-dark"></i>Parameter</a></asp:Label>
           <asp:Label runat="server" ID="LabelUser"><a class="nav-link text-dark" href="Masters.aspx"><i class="fas fa-fw fa-table text-dark"></i>User</a></asp:Label>
           <asp:Label runat="server" ID="LabelUserGroup"><a class="nav-link text-dark" href="UserGroups.aspx"><i class="fas fa-fw fa-table text-dark"></i>User Group</a></asp:Label>
        </ul> 
    </li>
    <%--<li class="nav-item">
        <a class="nav-link" href="StatusLogins.aspx"><i class="fas fa-fw fa-table"></i><span>Login Status User</span></a>
    </li>--%>
    <li class="nav-item">
        <a class="nav-link" href="Blokirs.aspx"><i class="fas fa-fw fa-table"></i><span>Login User Blokir/Dorman</span></a>
    </li>
    <li class="nav-item">
        <a class="nav-link" href="SalahPins.aspx"><i class="fas fa-fw fa-table"></i><span>Salah Password/Pin</span></a>
    </li>
    <li class="nav-item">
        <a class="nav-link" href="TransaksiBeliPulsaBerturut.aspx"><i class="fas fa-fw fa-table"></i><span>Transaksi Beli Pulsa</span></a>
    </li>
    <li class="nav-item">
        <a class="nav-link" href="TransaksiSubuh.aspx"><i class="fas fa-fw fa-table"></i><span>Transaksi Diatas Jam 12</span></a>
    </li>
    <li class="nav-item">
        <a class="nav-link" href="AktifitasDiatasJamDuaBelass.aspx"><i class="fas fa-fw fa-table"></i><span>Aktifitas Diatas Jam 12</span></a>
    </li>
    <li class="nav-item">
        <a class="nav-link" href="AktifitasBerulangLoginSuksess.aspx"><i class="fas fa-fw fa-table"></i><span>Aktifitas Berulang Login Sukses</span></a>
    </li>
    <li class="nav-item">
        <a class="nav-link" href="PolaTransaksiDebetSemuas.aspx"><i class="fas fa-fw fa-table"></i><span>Nominal Transaksi Debet Tidak Sesuai Pola (Nominal)</span></span></a>
    </li>
    <li class="nav-item">
        <a class="nav-link" href="JumlahTransaksiTidakSesuaiPolas.aspx"><i class="fas fa-fw fa-table"></i><span>Jumlah Transaksi Debet Tidak Sesuai Pola (Frekuensi)</span></span></a>
    </li>
    <li class="nav-item">
        <a class="nav-link" href="TotalTransaksiMelebihiTigaPuluhJutas.aspx"><i class="fas fa-fw fa-table"></i><span>Total Transaksi Melebihi 30 Juta</span></span></a>
    </li>
    <li class="nav-item">
        <a class="nav-link" href="UserLoginBersamaans.aspx"><i class="fas fa-fw fa-table"></i><span>User Login Bersamaan</span></span></a>
    </li>
    <!-- Divider -->
    <hr class="sidebar-divider">
    <!-- Heading -->
    <div class="sidebar-heading">
        Hasil
    </div>
    <!-- Nav Item - Pages Collapse Menu -->
    
     <li class="nav-item dropdown">
        <a class="nav-link dropdown-toggle" href="#" id="laporan" role="button" data-toggle="dropdown" aria-expanded="false">
            <i class="fas fa-fw fa-paper-plane"></i> Laporan
        </a>
        
     </li>
     <li class="nav-item">
        <a class="nav-link" runat="server" id="ApprovalPar" href="ApprovalViewParameter.aspx"><i class="fas fa-fw fa-table"></i><span>Approval Parameter</span></span></a>
    </li>
    
    <hr class="sidebar-divider">
    <!-- Heading -->
    <div class="sidebar-heading">
        Administrator
    </div>
    
</ul>

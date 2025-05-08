<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterParameters.aspx.cs" Inherits="SBFDSys.MasterParameters" %>

<%@ Register Src="~/SideBarAdmin.ascx" TagPrefix="uc1" TagName="SideBarAdmin" %> 
<%@ Register Src="~/TopBar.ascx" TagPrefix="uc1" TagName="TopBar" %>

<link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.3.1/css/all.css">
<link href="css/sb-admin-2.min.css" rel="stylesheet" />
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/css/bootstrap.min.css">
<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/js/bootstrap.bundle.min.js"></script>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="../Content/Images/bprsblogo.png" />
    <title>Master Data</title>
    <%--<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.min.js"></script>--%>

</head>
<body id="page-top">
  <form runat="server" class="needs-validation" novalidate>
    <div id="wrapper">
        <uc1:SideBarAdmin runat="server" id="SideBarAdmin" />
        
        <!-- Content Wrapper -->
        <div id="content-wrapper" class="d-flex flex-column">
            <!-- Main Content -->
            <div id="content">
                <uc1:TopBar runat="server" ID="TopBar" />
                <!-- Begin Page Content -->
                    <div class="container-fluid">
                        <!-- Page Heading -->
                        <%--<h1 class="h3 mb-2 text-gray-800">Input Ruang Lokasi</h1>--%>
                        <%--<p class="mb-4">Tambah Parameter</p>--%>
                        <!-- DataTales Example -->
                        <div class="card shadow mb-4">
                            <div class="card-header py-3">
                                <h6 class="m-0 font-weight-bold text-primary">Daftar Parameter testing</h6>
                            </div>
                            <br>
                            <%--<a href="Cabangs.aspx">Cabangs.aspx</a>--%>
                            <div class="card-body">
                                <div class="row">
                                    
                                    <%--<div class="col-sm-6 d-flex justify-content-end">
                                        <div class="input-group">
                                            <asp:TextBox ID="TextBoxSearch" runat="server" type="text" class="form-control bg-light border-0 small" placeholder="Tulis Pencarian ..." aria-label="Search" aria-describedby="basic-addon2"></asp:TextBox>
                                            <div class="input-group-append">
                                                <asp:Button ID="ButtonSearchMaster" runat="server" class="btn btn-primary" type="button" Text="Cari" OnClick="ButtonSearchMaster_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>--%>
                                </div><br />

                                <div class="container">
                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <a href="#" class="btn btn-success btn-icon-split w-100" data-toggle="modal" data-target="#ModalParameterUserBlokir">
                                                <span class="icon text-white-50">
                                                    <i class="fas fa-plus"></i>
                                                </span>
                                                <span class="text">Login User Blokir</span>
                                            </a>
                                        </div>
                                        <div class="col-md-4">
                                            <a href="#" class="btn btn-success btn-icon-split w-100" data-toggle="modal" data-target="#ModalParameterSalahPassword">
                                                <span class="icon text-white-50">
                                                    <i class="fas fa-plus"></i>
                                                </span>
                                                <span class="text">Salah Password</span>
                                            </a>
                                        </div>
                                        <div class="col-md-4">
                                            <a href="#" class="btn btn-success btn-icon-split w-100" data-toggle="modal" data-target="#ModalParameterBeliPulsa">
                                                <span class="icon text-white-50">
                                                    <i class="fas fa-plus"></i>
                                                </span>
                                                <span class="text">Transaksi Beli Pulsa</span>
                                            </a>
                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <a href="#" class="btn btn-success btn-icon-split w-100" data-toggle="modal" data-target="#ModalParameterTrxMalam">
                                                <span class="icon text-white-50">
                                                    <i class="fas fa-plus"></i>
                                                </span>
                                                <span class="text">Transaksi Diatas Jam 12 Malam</span>
                                            </a>
                                        </div>
                                        <div class="col-md-4">
                                            <a href="#" class="btn btn-success btn-icon-split w-100" data-toggle="modal" data-target="#ModalParameterAktifitasMlm">
                                                <span class="icon text-white-50">
                                                    <i class="fas fa-plus"></i>
                                                </span>
                                                <span class="text">Aktifitas Diatas Jam 12 Malam</span>
                                            </a>
                                        </div>
                                        <div class="col-md-4">
                                            <a href="#" class="btn btn-success btn-icon-split w-100" data-toggle="modal" data-target="#ModalParameterAktifitasBerulangLogin">
                                                <span class="icon text-white-50">
                                                    <i class="fas fa-plus"></i>
                                                </span>
                                                <span class="text">Aktifitas Berulang Login Sukses</span>
                                            </a>
                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <a href="#" class="btn btn-success btn-icon-split w-100" data-toggle="modal" data-target="#ModalParameterPolaTransaksi">
                                                <span class="icon text-white-50">
                                                    <i class="fas fa-plus"></i>
                                                </span>
                                                <span class="text">Nominal Transaksi Debet</span>
                                            </a>
                                        </div>
                                        <div class="col-md-4">
                                            <a href="#" class="btn btn-success btn-icon-split w-100" data-toggle="modal" data-target="#ModalParameterPolaFrekuensi">
                                                <span class="icon text-white-50">
                                                    <i class="fas fa-plus"></i>
                                                </span>
                                                <span class="text">Frekuensi Transaksi Debet</span>
                                            </a>
                                        </div>
                                        <div class="col-md-4">
                                            <a href="#" class="btn btn-success btn-icon-split w-100" data-toggle="modal" data-target="#ModalParameterTrxMelebihi30Juta">
                                                <span class="icon text-white-50">
                                                    <i class="fas fa-plus"></i>
                                                </span>
                                                <span class="text">Total Transaksi Melebihi 30 Juta</span>
                                            </a>
                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <a href="#" class="btn btn-success btn-icon-split w-100" data-toggle="modal" data-target="#ModalParameterLoginBersamaDeviceBerbeda">
                                                <span class="icon text-white-50">
                                                    <i class="fas fa-plus"></i>
                                                </span>
                                                <span class="text">Login Bersamaan Channel Berbeda</span>
                                            </a>
                                        </div>
                                    </div>
                                </div>

                            
                        </div>
                    </div>
                    <!-- /.container-fluid -->
                    <!-- Modal Confirmation-->
                     <style>
                        .modal-body {
                            padding: 1.5rem;
                        }
                        .form-control {
                            width: 100%;
                            height: auto; /* Allows the textbox height to adjust */
                        }
                        .form-group {
                            margin-bottom: 1rem; /* Add some space between fields */
                        }
                    </style>
                        <!-- Modal Confirmation Parameter Login Bersamaan Device Berbeda-->
                      <div class="modal fade" id="ModalParameterLoginBersamaDeviceBerbeda" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title">Masukkan Parameter</h5>
                                        <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">×</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="form-group">
                                                <asp:Label ID="Label12" runat="server" Text="Nama"></asp:Label><br />
                                                <asp:TextBox ID="TextboxLoginBersama" runat="server" CssClass="form-control" Text=""></asp:TextBox><br />
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-md-12">
                                                <asp:Label ID="Label13" runat="server" Text="Waktu : (Dalam Menit)"></asp:Label><br />
                                                <asp:TextBox ID="TextboxWaktu" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-body">
                                             <div class="row">
                                                <div class="form-group col-md-6">
                                                    <asp:Label ID="label16" runat="server" Text="Nama Approval :"></asp:Label><br />
                                                    <asp:DropDownList ID="DropDownListLoginBersamaDeviceBerbeda" runat="server" class="form-control form-control-user"></asp:DropDownList>
                                                </div>
                                            </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button runat="server" ID="ButtonLoginBesamaan" class="btn btn-secondary" UseSubmitBehavior="false" type="button" data-dismiss="modal" Text="OK" OnClick="ButtonLoginBesamaan_Click" />
                                        <button class="btn btn-primary" type="button" data-dismiss="modal">Batal</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                       <!-- Modal Confirmation Parameter Total Transaksi Melebihi 30 Juta-->
                        <div class="modal fade" id="ModalParameterTrxMelebihi30Juta" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title">Masukkan Parameter</h5>
                                        <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">×</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="form-group">
                                                <asp:Label ID="Label10" runat="server" Text="Nama"></asp:Label><br />
                                                <asp:TextBox ID="TextboxNamaTrx" runat="server" CssClass="form-control" Text=""></asp:TextBox><br />
                                        </div>
                                    <div class="row">
                                        <div class="form-group col-md-12">
                                            <asp:Label ID="Label11" runat="server" Text="Nominal"></asp:Label><br />
                                            <asp:TextBox ID="TextboxMelebihi30Juta" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <script>
                                        $(document).ready(function() {
                                            $('#<%= TextboxMelebihi30Juta.ClientID %>').mask('000.000.000.000.000', {reverse: true});
                                        });
                                    </script>
                                    </div>
                                    <div class="modal-body">
                                             <div class="row">
                                                <div class="form-group col-md-6">
                                                    <asp:Label ID="labelTrxMelebihi30Juta" runat="server" Text="Nama Approval :"></asp:Label><br />
                                                    <asp:DropDownList ID="DropDownListTrxMelebihi30Juta" runat="server" class="form-control form-control-user"></asp:DropDownList>
                                                </div>
                                            </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button runat="server" ID="ButtonTrxMelebihi30Juta" class="btn btn-secondary" UseSubmitBehavior="false" type="button" data-dismiss="modal" Text="OK" OnClick="ButtonTrxMelebihi30Juta_Click" />
                                        <button class="btn btn-primary" type="button" data-dismiss="modal">Batal</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- Modal Confirmation Parameter Aktifitas Berulang Login Sukses-->
                        <div class="modal fade" id="ModalParameterAktifitasBerulangLogin" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title">Masukkan Parameter</h5>
                                        <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">×</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="form-group">
                                                <asp:Label ID="Label8" runat="server" Text="Nama"></asp:Label><br />
                                                <asp:TextBox ID="TextboxAktifitasBerulangLogin" runat="server" CssClass="form-control" Text=""></asp:TextBox><br />
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-md-12">
                                                <asp:Label ID="Label9" runat="server" Text="Login Per Hari : (lebih besar dari)"></asp:Label><br />
                                                <asp:TextBox ID="TextboxHari" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-body">
                                             <div class="row">
                                                <div class="form-group col-md-6">
                                                    <asp:Label ID="label15" runat="server" Text="Nama Approval :"></asp:Label><br />
                                                    <asp:DropDownList ID="DropDownListAktBerulangLogin" runat="server" class="form-control form-control-user"></asp:DropDownList>
                                                </div>
                                            </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button runat="server" ID="ButtonAktifitasBerulangLogin" class="btn btn-secondary" UseSubmitBehavior="false" type="button" data-dismiss="modal" Text="OK" OnClick="ButtonAktifitasBerulangLogin_Click" />
                                        <button class="btn btn-primary" type="button" data-dismiss="modal">Batal</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                    <!-- Modal Confirmation Parameter Aktifitas Malam Diatas Jam 12-->
                    <div class="modal fade" id="ModalParameterAktifitasMlm" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Masukkan Parameter</h5>
                                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                            <asp:Label ID="Label5" runat="server" Text="Nama"></asp:Label><br />
                                            <asp:TextBox ID="TextboxAktifitasMalam" runat="server" CssClass="form-control" Text=""></asp:TextBox><br /><br />
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <asp:Label ID="Label6" runat="server" Text="Dari Jam:"></asp:Label><br />
                                            <asp:TextBox ID="TextboxDariJam" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <asp:Label ID="Label7" runat="server" Text="Sampai Jam:"></asp:Label><br />
                                            <asp:TextBox ID="TextboxSampaiJam" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                                        </div>
                                    </div>
                                 </div>
                                    <div class="modal-body">
                                         <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="labelAktifitas12" runat="server" Text="Nama Approval :"></asp:Label><br />
                                                <asp:DropDownList ID="DropDownListAktifitas12" runat="server" class="form-control form-control-user"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                
                                <div class="modal-footer">
                                    <asp:Button runat="server" ID="ButtonAktifitasMalam" class="btn btn-secondary" UseSubmitBehavior="false" type="button" data-dismiss="modal" Text="OK" OnClick="ButtonAktifitasMalam_Click" />
                                    <button class="btn btn-primary" type="button" data-dismiss="modal">Batal</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Modal Confirmation Parameter Transaksi Malam Diatas Jam 12-->
                    <div class="modal fade" id="ModalParameterTrxMalam" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Masukkan Parameter</h5>
                                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                            <asp:Label ID="Label4" runat="server" Text="Nama"></asp:Label><br />
                                            <asp:TextBox ID="TextboxTrxMalam" runat="server" CssClass="form-control" Text=""></asp:TextBox><br /><br />
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <asp:Label ID="LabelJamDari" runat="server" Text="Dari Jam:"></asp:Label><br />
                                            <asp:TextBox ID="TextboxJamDari" runat="server" CssClass="form-control" Type="time"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <asp:Label ID="LabelJamSampai" runat="server" Text="Sampai Jam:"></asp:Label><br />
                                            <asp:TextBox ID="TextboxJamSampai" runat="server" CssClass="form-control" Type="time"></asp:TextBox>
                                        </div>
                                    </div>
                                  </div>
                                    <div class="modal-body">
                                         <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="label14" runat="server" Text="Nama Approval :"></asp:Label><br />
                                                <asp:DropDownList ID="DropDownListApprovalTrx12" runat="server" class="form-control form-control-user"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                
                                <div class="modal-footer">
                                    <asp:Button runat="server" ID="ButtonTrxMalam" class="btn btn-secondary" UseSubmitBehavior="false" type="button" data-dismiss="modal" Text="OK" OnClick="ButtonTrxMalam_Click" />
                                    <button class="btn btn-primary" type="button" data-dismiss="modal">Batal</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Modal Confirmation Parameter Transaksi Beli Pulsa-->
                    <div class="modal fade" id="ModalParameterBeliPulsa" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Masukkan Parameter</h5>
                                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                            <asp:Label ID="Label2" runat="server" Text="Nama"></asp:Label><br />
                                            <asp:TextBox ID="TextboxBeliPulsa" runat="server" CssClass="form-control" Text=""></asp:TextBox><br /><br />
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <asp:Label ID="Label3" runat="server" Text="Value1 (lebih besar dari: )"></asp:Label><br />
                                            <asp:TextBox ID="TextboxValue1" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                        </div>
                                    </div>
                                </div>    
                                <div class="modal-body">
                                         <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="labelApproval" runat="server" Text="Nama Approval :"></asp:Label><br />
                                                <asp:DropDownList ID="DropDownListApproval" runat="server" class="form-control form-control-user"></asp:DropDownList>
                                            </div>
                                        </div>
                                </div>
                                <%--<div class="row">
                                    <div class="form-group col-md-6">
                                        <asp:Label ID="labelStatus" runat="server"></asp:Label><br />
                                    </div>
                                </div>--%>
                          
                                 <div class="modal-footer">
                                     <asp:Button runat="server" ID="Button1" class="btn btn-secondary" UseSubmitBehavior="false" type="button" data-dismiss="modal" Text="OK" OnClick="ButtonOKConfirm_Click" />
                                     <button class="btn btn-primary" type="button" data-dismiss="modal">Batal</button>
                                 </div>
                            </div>
                        </div>
                   </div>


                        <!-- Modal Confirmation Parameter Salah Password-->
                         <div class="modal fade" id="ModalParameterSalahPassword" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                             <div class="modal-dialog" role="document">
                                 <div class="modal-content">
                                     <div class="modal-header">
                                         <h5 class="modal-title">Masukkan Parameter</h5>
                                         <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                             <span aria-hidden="true">×</span>
                                         </button>
                                     </div>
                                     <div class="modal-body">
                                         <div class="form-group">
                                                 <asp:Label ID="Label17" runat="server" Text="Nama"></asp:Label><br />
                                                 <asp:TextBox ID="TextboxSalahPassword" runat="server" CssClass="form-control" Text=""></asp:TextBox><br />
                                         </div>
                                     <div class="row">
                                         <div class="form-group col-md-12">
                                             <asp:Label ID="Label18" runat="server" Text="DATA AKTIFITAS (lebih besar dari)"></asp:Label><br />
                                             <asp:TextBox ID="TextboxAktifitas" runat="server" CssClass="form-control" />
                                         </div>
                                     </div>
                                     
                                     </div>
                                     <div class="modal-body">
                                              <div class="row">
                                                 <div class="form-group col-md-6">
                                                     <asp:Label ID="label19" runat="server" Text="Nama Approval :"></asp:Label><br />
                                                     <asp:DropDownList ID="DropDownListApprovalSalahPassword" runat="server" class="form-control form-control-user"></asp:DropDownList>
                                                 </div>
                                             </div>
                                     </div>
                                     <div class="modal-footer">
                                         <asp:Button runat="server" ID="ButtonOkSalahPassword" class="btn btn-secondary" UseSubmitBehavior="false" type="button" data-dismiss="modal" Text="OK" OnClick="ButtonOkSalahPassword_Click" />
                                         <button class="btn btn-primary" type="button" data-dismiss="modal">Batal</button>
                                     </div>
                                 </div>
                             </div>
                         </div>
                         <!-- Modal Confirmation Parameter User Blokir-->
                              <div class="modal fade" id="ModalParameterUserBlokir" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                  <div class="modal-dialog" role="document">
                                      <div class="modal-content">
                                          <div class="modal-header">
                                              <h5 class="modal-title">Masukkan Parameter</h5>
                                              <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                                  <span aria-hidden="true">×</span>
                                              </button>
                                          </div>
                                          <div class="modal-body">
                                              <div class="form-group">
                                                      <asp:Label ID="Label20" runat="server" Text="Nama"></asp:Label><br />
                                                      <asp:TextBox ID="TextboxNamaUserBlokir" runat="server" CssClass="form-control" Text=""></asp:TextBox><br />
                                              </div>
                                          <div class="row">
                                              <div class="form-group col-md-12">
                                                  <asp:Label ID="Label21" runat="server" Text="Status IB atau MB"></asp:Label><br />
                                                  <asp:DropDownList ID="DropDownListStatusIbMb" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="0" Text="Pilih Status"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="A"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="B"></asp:ListItem>
                                                  </asp:DropDownList>

                                              </div>
                                              
                                          </div>
              
                                          </div>
                                          <div class="modal-body">
                                                   <div class="row">
                                                      <div class="form-group col-md-6">
                                                          <asp:Label ID="label22" runat="server" Text="Nama Approval :"></asp:Label><br />
                                                          <asp:DropDownList ID="DropDownListApprovalUserBlokir" runat="server" class="form-control form-control-user"></asp:DropDownList>
                                                      </div>
                                                  </div>
                                          </div>
                                          <div class="modal-footer">
                                              <asp:Button runat="server" ID="ButtonOkUserBlokir" class="btn btn-secondary" UseSubmitBehavior="false" type="button" data-dismiss="modal" Text="OK" OnClick="ButtonOkUserBlokir_Click" />
                                              <button class="btn btn-primary" type="button" data-dismiss="modal">Batal</button>
                                          </div>
                                      </div>
                                  </div>
                              </div>

                        <!-- Modal Confirmation Parameter Nominal Transaksi Tidak Sesuai Pola-->
                             <div class="modal fade" id="ModalParameterPolaTransaksi" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                 <div class="modal-dialog" role="document">
                                     <div class="modal-content">
                                         <div class="modal-header">
                                             <h5 class="modal-title">Masukkan Parameter</h5>
                                             <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                                 <span aria-hidden="true">×</span>
                                             </button>
                                         </div>
                                         <div class="modal-body">
                                             <div class="form-group">
                                                     <asp:Label ID="Label24" runat="server" Text="Nama"></asp:Label><br />
                                                     <asp:TextBox ID="TextboxPolaTransaksi" runat="server" CssClass="form-control" Text=""></asp:TextBox><br />
                                             </div>
                                         
                                         </div>
                                         <div class="modal-body">
                                            <div class="form-group">
                                                    <asp:Label ID="Label25" runat="server" Text="Pola Dalam Bulan"></asp:Label><br />
                                                    <asp:TextBox ID="TextboxPolaBulan" runat="server" CssClass="form-control" Text=""></asp:TextBox><br />
                                            </div>

                                        </div>
                                         <div class="modal-body">
                                                  <div class="row">
                                                     <div class="form-group col-md-6">
                                                         <asp:Label ID="label27" runat="server" Text="Nama Approval :"></asp:Label><br />
                                                         <asp:DropDownList ID="DropDownListApprovalPolaNominal" runat="server" class="form-control form-control-user"></asp:DropDownList>
                                                     </div>
                                                 </div>
                                         </div>
                                         <div class="modal-footer">
                                             <asp:Button runat="server" ID="ButtonPolaTransaksiBulan" class="btn btn-secondary" UseSubmitBehavior="false" type="button" data-dismiss="modal" Text="OK" OnClick="ButtonPolaTransaksiBulan_Click" />
                                             <button class="btn btn-primary" type="button" data-dismiss="modal">Batal</button>
                                         </div>
                                     </div>
                                 </div>
                             </div>
                        <!-- Modal Confirmation Parameter Nominal Frekuensi Tidak Sesuai Pola-->
                             <div class="modal fade" id="ModalParameterPolaFrekuensi" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                 <div class="modal-dialog" role="document">
                                     <div class="modal-content">
                                         <div class="modal-header">
                                             <h5 class="modal-title">Masukkan Parameter</h5>
                                             <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                                 <span aria-hidden="true">×</span>
                                             </button>
                                         </div>
                                         <div class="modal-body">
                                             <div class="form-group">
                                                     <asp:Label ID="Label26" runat="server" Text="Nama"></asp:Label><br />
                                                     <asp:TextBox ID="TextboxPolaFrekuensi" runat="server" CssClass="form-control" Text=""></asp:TextBox><br />
                                             </div>
                 
                                         </div>
                                         <div class="modal-body">
                                            <div class="form-group">
                                                    <asp:Label ID="Label28" runat="server" Text="Pola Dalam Bulan"></asp:Label><br />
                                                    <asp:TextBox ID="TextboxPolaBulanFrekuensi" runat="server" CssClass="form-control" Text=""></asp:TextBox><br />
                                            </div>

                                        </div>
                                         <div class="modal-body">
                                                  <div class="row">
                                                     <div class="form-group col-md-6">
                                                         <asp:Label ID="label29" runat="server" Text="Nama Approval :"></asp:Label><br />
                                                         <asp:DropDownList ID="DropDownListApprovalPolaFrekuensi" runat="server" class="form-control form-control-user"></asp:DropDownList>
                                                     </div>
                                                 </div>
                                         </div>
                                         <div class="modal-footer">
                                             <asp:Button runat="server" ID="ButtonPolaFrekuensi" class="btn btn-secondary" UseSubmitBehavior="false" type="button" data-dismiss="modal" Text="OK" OnClick="ButtonPolaFrekuensi_Click" />
                                             <button class="btn btn-primary" type="button" data-dismiss="modal">Batal</button>
                                         </div>
                                     </div>
                                 </div>
                             </div>

                        <!-- Modal Confirmation -->
            </div>
                        
                    <div class="modal fade" id="ModalConfirmation" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Konfirmasi</h5>
                                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <asp:Label ID="LabelConfirmation" runat="server" Text="Please Confirm."></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button runat="server" ID="ButtonOKConfirm" class="btn btn-secondary" UseSubmitBehavior="false" type="button" data-dismiss="modal" Text="OK" OnClick="ButtonOKConfirm_Click" />
                                    <button class="btn btn-primary" type="button" data-dismiss="modal">Batal</button>
                                </div>
                            </div>
                        </div>
                    </div>
                <div class="modal fade" id="ModalConfirmationAktif" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Konfirmasi</h5>
                                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <asp:Label ID="Label1" runat="server" Text="Please Confirm."></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button runat="server" ID="ButtonOkAktif" class="btn btn-secondary" UseSubmitBehavior="false" type="button" data-dismiss="modal" Text="OK" OnClick="ButtonOkAktif_Click" />
                                    <button class="btn btn-primary" type="button" data-dismiss="modal">Batal</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- Error Modal-->
                    <div class="modal fade" id="ErrorModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="exampleModalLabel">Error Occured!</h5>
                                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <asp:Label ID="LabelError" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <button class="btn btn-secondary" type="button" data-dismiss="modal">OK</button>
                                </div>
                            </div>
                        </div>
                    </div>

            </div>
            <!-- End of Main Content -->
            <!-- #include file="Footer.html" -->
        </div>
        <!-- End of Content Wrapper -->
    </div>
    <!-- End of Page Wrapper -->
    <!-- Scroll to Top Button-->
    <a class="scroll-to-top rounded" href="#page-top">
        <i class="fas fa-angle-up"></i>
    </a>
  </form>

</body>
</html>

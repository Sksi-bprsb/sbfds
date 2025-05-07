<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatusLogins.aspx.cs" Inherits="SBFDSys.StatusLogins" %>

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
    <title>SB-FDS</title>
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
                        <%--<p class="mb-4">Detection</p>--%>
                        <!-- DataTales Example -->
                        <div class="card shadow mb-4">
                            <div class="card-header py-3">
                                <h6 class="m-0 font-weight-bold text-primary">Daftar Nasabah yang terdaftar IBMB</h6>
                            </div>
                            <br>
                            <%--<a href="Cabangs.aspx">Cabangs.aspx</a>--%>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <a href="#" class="btn btn-success btn-icon-split rounded-pill">
                                            <span class="icon text-white">
                                                <i class="fas fa-share"></i>
                                            </span>
                                            <span class="text">Kirim Data </span>
                                        </a>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="input-group">
                                            <asp:TextBox ID="TextBoxSearch" runat="server" type="text" class="form-control bg-light border-0 small" placeholder="Tulis Pencarian ..." aria-label="Search" aria-describedby="basic-addon2"></asp:TextBox>
                                            <div class="input-group-append">
                                                <asp:Button ID="ButtonSearchTransaksi" runat="server" class="btn btn-primary" type="button" Text="Cari" OnClick="ButtonSearchTransaksi_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br>
                                <div class="table-responsive">
                                    <asp:ListView runat="server" ID="ListViewData" OnPagePropertiesChanging="ListViewData_PagePropertiesChanging">
                                        <LayoutTemplate>
                                            <table class="table table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th>Nama Customer</th>
                                                        <th>Status IB</th>
                                                        <th>Status MB</th>
                                                        <th>Login Terakhir</th>


                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr runat="server" id="itemPlaceholder"></tr>
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("customer_name") %></td>
                                                <td><%# Eval("ib_status").ToString() == "I" ? "Belum Aktif" : (Eval("ib_status").ToString() == "B" ? "Blokir" : (Eval("ib_status").ToString()== "C" ? "Tutup Oleh Sistem" : (Eval("ib_status").ToString()== "A" ? "Aktif" : Eval("ib_status").ToString()))) %></td>
                                                <td><%# Eval("mb_status").ToString() == "I" ? "Belum Aktif" : (Eval("mb_status").ToString() == "B" ? "Blokir" : (Eval("mb_status").ToString()== "C" ? "Tutup Oleh Sistem" : (Eval("ib_status").ToString()== "A" ? "Aktif" : Eval("ib_status").ToString()))) %></td>
                                               <td><%# Eval("last_login") %></td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                                <label>Page</label>
                                <asp:DataPager ID="lvDataPager1" runat="server" PagedControlID="ListViewData" PageSize="20">
                                    <Fields>
                                        <asp:NumericPagerField ButtonType="Link" />
                                    </Fields>
                                </asp:DataPager>
                            </div>
                        </div>
                    </div>
                    <!-- /.container-fluid -->
                    <!-- Modal Confirmation-->
                    <div class="modal fade" id="ModalConfirmation" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Confirmation</h5>
                                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <asp:Label ID="LabelConfirmation" runat="server" Text="Please Confirm."></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button runat="server" ID="ButtonOKConfirm" class="btn btn-secondary" UseSubmitBehavior="false" type="button" data-dismiss="modal" Text="OK" OnClick="ButtonOKConfirm_Click" />
                                    <button class="btn btn-primary" type="button" data-dismiss="modal">Cancel</button>
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

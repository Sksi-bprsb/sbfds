<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterAddUpdate.aspx.cs" Inherits="SBFDSys.MasterAddUpdate" %>

<%@ Register Src="~/TopBar.ascx" TagPrefix="uc1" TagName="TopBar" %>
<%@ Register Src="~/SideBarAdmin.ascx" TagPrefix="uc1" TagName="SideBarAdmin" %>


<link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.3.1/css/all.css">
<link href="css/sb-admin-2.min.css" rel="stylesheet" />
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/css/bootstrap.min.css">
<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/js/bootstrap.bundle.min.js"></script>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="../Content/Images/bprsblogo.png" />
    <title>Administrator Master Data</title>
    <script>
        // Example starter JavaScript for disabling form submissions if there are invalid fields
        (function () {
            'use strict';
            window.addEventListener('load', function () {
                // Fetch all the forms we want to apply custom Bootstrap validation styles to
                var forms = document.getElementsByClassName('needs-validation');
                // Loop over them and prevent submission
                var validation = Array.prototype.filter.call(forms, function (form) {
                    form.addEventListener('submit', function (event) {
                        if (form.checkValidity() === false) {
                            event.preventDefault();
                            event.stopPropagation();
                        }
                        form.classList.add('was-validated');
                    }, false);
                });
            }, false);
        })();
    </script>
</head>
<body id="page-top">
    <form id="form1" runat="server">
        <div id="wrapper">
            <uc1:SideBarAdmin runat="server" ID="SideBarAdmin" />
            <!-- Content Wrapper -->
            <div id="content-wrapper" class="d-flex flex-column">
                <!-- Main Content -->
                <div id="content">
                    <uc1:TopBar runat="server" ID="TopBar" />
                    <!-- Begin Page Content -->
                    <div class="container-fluid">
                        <!-- DataTales Example -->
                        <div class="card shadow mb-4">
                            <div class="card-header py-3">
                                <h6 class="m-0 font-weight-bold text-primary">Buat Data Baru</h6>
                            </div>
                            <div class="card-body">
                                <div class="form-group row">
                                    <div class="col-sm-6 mb-3 mb-sm-2">
                                        <label for="TextBoxNik">User Id</label>
                                        <asp:TextBox ID="TextBoxNik" type="text" runat="server" class="form-control" placeholder="masukkan user id karyawan anda (contoh: 12345)" required></asp:TextBox>
                                    </div>
                                    <div class="col-sm-6 mb-3 mb-sm-2">
                                        <label for="TextBoxNamaKar">Nama Karyawan</label>
                                        <asp:TextBox ID="TextBoxNamaKar" type="text" runat="server" class="form-control" placeholder="masukkan nama" required></asp:TextBox>
                                    </div>
                                    <div class="col-sm-6 mb-3 mb-sm-2">
                                        <label for="TextBoxUser">Nama Panggilan</label>
                                        <asp:TextBox ID="TextBoxUser" type="text" runat="server" class="form-control" placeholder="masukkan nama panggilan karyawan" required></asp:TextBox>
                                    </div>
                                    <div class="col-sm-6 mb-3 mb-sm-2">
                                        <label for="TextBoxPass">Password</label>
                                        <asp:TextBox ID="TextBoxPass" type="text" runat="server" class="form-control" placeholder="masukkan password anda" required></asp:TextBox>
                                    </div>
                                    <div class="col-sm-6 mb-3 mb-sm-2">
                                        <label for="TextBoxEmail">Email</label>
                                        <asp:TextBox ID="TextBoxEmail" type="email" runat="server" class="form-control" placeholder="masukkan email anda yang aktif"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-6 mb-3 mb-sm-2">
                                        <label for="TextBoxNoHp">No Handphone</label>
                                        <asp:TextBox ID="TextBoxNoHp" type="number" runat="server" class="form-control" placeholder="masukkan no handphone anda yang aktif"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-6 mb-3 mb-sm-2"">
                                        <label for="DropDownListDept">Departemen</label>
                                        <asp:DropDownList ID="DropDownListDept" runat="server" class="form-control form-control-user"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-6 mb-3 mb-sm-2"">
                                        <label for="DropDownListCabang">Kantor Cabang</label>
                                        <asp:DropDownList ID="DropDownListCabang" runat="server" class="form-control form-control-user"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-6 mb-3 mb-sm-2"">
                                        <label for="DropDownListUserGroup">User Group</label>
                                        <asp:DropDownList ID="DropDownListUserGroup" runat="server" class="form-control form-control-user"></asp:DropDownList>
                                    </div>
                                   
                                </div>
                                
                                <asp:Button ID="TombolSimpan" runat="server" Text="Simpan" class="btn btn-primary" OnClick="TombolSimpan_Click" />
                            </div>
                        </div>

                    </div>
                </div>
                <!-- End of Main Content -->
                <!-- #include file="Footer.html" -->
                <!-- Error Modal-->
                <div class="modal fade" id="ErrorModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Pesan</h5>
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

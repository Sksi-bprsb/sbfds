<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartemenAddUpdate.aspx.cs" Inherits="SBFDSys.DepartemenAddUpdate" %>

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
    <title>Admin SB HRM Departemen</title>
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
               <uc1:SideBarAdmin runat="server" id="SideBarAdmin" />

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
                                <h6 class="m-0 font-weight-bold text-primary">Buat Departemen Baru</h6>
                            </div>
                            <div class="card-body">
                                <div class="form-group row">

                                    <div class="col-sm-6 mb-3 mb-sm-0">
                                        <label for="TextBoxKodeDepartemen">Kode Departemen</label>
                                        <asp:TextBox ID="TextBoxKodeDepartemen" type="text" runat="server" class="form-control" placeholder="Kode Departemen" required></asp:TextBox>
                                    </div>
                                    <div class="col-sm-6">
                                        <label for="TextBoxNamaDepartemen">Nama Departemen</label>
                                        <asp:TextBox ID="TextBoxNamaDepartemen" type="text" runat="server" class="form-control" placeholder="Nama Departemen" required></asp:TextBox>
                                    </div>
                                </div>
                               <%-- <div class="form-group row">
                                    <div class="col-sm-6 mb-3 mb-sm-0">
                                        <label for="TextBoxPassword">Password</label>
                                        <asp:TextBox ID="TextBoxPassword" type="Password" runat="server" class="form-control form-control-user" placeholder="Password"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-6">
                                        <label for="TextBoxEmailID">Email</label>
                                        <asp:TextBox ID="TextBoxEmailID" type="Email" runat="server" class="form-control form-control-user" placeholder="Email ID" required></asp:TextBox>
                                    </div>
                                </div>--%>

                                <%--<div class="form-group row">
                                    <div class="col-sm-6 mb-3 mb-sm-0">
                                        <asp:CheckBox runat="server" ID="CheckBoxAdminAccess" Text="Admin Access" />
                                    </div>
                                    <div class="col-sm-6">
                                    </div>
                                </div>--%>


                                <asp:Button ID="ButtonSave" runat="server" Text="Simpan" class="btn btn-primary" OnClick="ButtonSave_Click" />
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

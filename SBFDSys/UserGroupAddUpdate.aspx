<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserGroupAddUpdate.aspx.cs" Inherits="SBFDSys.UserGroupAddUpdate" %>

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
    <title>Administrator Kantor Cabang</title>
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
                                <h6 class="m-0 font-weight-bold text-primary">Buat User Group Baru</h6>
                            </div>
                            <div class="card-body">
                              <div class=" d-flex justify-content-around">
                                <div class="form-group row">
                                    <div class="col-sm-6 mb-3 mb-sm-2">
                                        <label for="TextBoxUserGroup">Nama User Group</label>
                                        <asp:TextBox ID="TextBoxUserGroup" type="text" runat="server" class="form-control" placeholder="Tulis Nama Group" required></asp:TextBox>
                                    </div>
                                    <div class="col-sm-10 mt-3">
                                        <div class="col-sm-0 font-weight-bold">
                                            <label for="lebelRuang">Parameter</label>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="CheckBoxTampilR" runat="server" />
                                                <label for="CheckBoxTampilR">Tampil</label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="CheckBoxSimpanR" runat="server" />
                                                <label for="CheckBoxSimpanR">Simpan</label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="CheckBoxHapusR" runat="server" />
                                                <label for="CheckBoxHapusR">Hapus</label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="CheckBoxPrintR" runat="server" />
                                                <label for="CheckBoxPrintR">Print</label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="CheckBoxApproveR" runat="server" />
                                                <label for="CheckBoxApproveR">Approve</label>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="col-sm-10">
                                        <div class="col-sm-0 font-weight-bold">
                                            <label for="TextBoxUserGroup">RestoreDB</label>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="CheckBoxTampilRestore" runat="server" />
                                                <label for="CheckBoxTampilRestore">Tampil</label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="CheckBoxSimpanRestore" runat="server" />
                                                <label for="CheckBoxSimpanRestore">Simpan</label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="CheckBoxHapusRestore" runat="server" />
                                                <label for="CheckBoxHapusRestore">Hapus</label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="CheckBoxPrintRestore" runat="server" />
                                                <label for="CheckBoxPrintRestore">Print</label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="CheckBoxApproveRestore" runat="server" />
                                                <label for="CheckBoxApproveRestore">Approve</label>
                                            </div>
                                        </div>
                                    </div>
                                    <%--<div class="col-sm-10">
                                        <div class="col-sm-0 font-weight-bold">
                                            <label for="TextBoxUserGroup">Cabang</label>
                                        </div>
                                        <div class="form-group row">
                                        <div class="col-sm-2">
                                            <asp:CheckBox ID="CheckBoxTampilCab" runat="server" />
                                            <label for="CheckBoxTampilCab">Tampil</label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:CheckBox ID="CheckBoxSimpanCab" runat="server" />
                                            <label for="CheckBoxSimpanCab">Simpan</label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:CheckBox ID="CheckBoxHapusCab" runat="server" />
                                            <label for="CheckBoxHapusCab">Hapus</label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:CheckBox ID="CheckBoxPrintCab" runat="server" />
                                            <label for="CheckBoxPrintCab">Print</label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:CheckBox ID="CheckBoxApproveCab" runat="server" />
                                            <label for="CheckBoxApproveCab">Approve</label>
                                        </div>
                                    </div>
                                    </div>--%>
                                     <%--<div class="col-sm-10">
                                         <div class="col-sm-0 font-weight-bold">
                                            <label for="TextBoxUserGroup">Departemen</label>
                                        </div>
                                        <div class="form-group row">
                                        <div class="col-sm-2">
                                            <asp:CheckBox ID="CheckBoxTampilDep" runat="server" />
                                            <label for="CheckBoxTampilDep">Tampil</label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:CheckBox ID="CheckBoxSimpanDep" runat="server" />
                                            <label for="CheckBoxSimpanDep">Simpan</label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:CheckBox ID="CheckBoxHapusDep" runat="server" />
                                            <label for="CheckBoxHapusDep">Hapus</label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:CheckBox ID="CheckBoxPrintDep" runat="server" />
                                            <label for="CheckBoxPrintDep">Print</label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:CheckBox ID="CheckBoxApproveDep" runat="server" />
                                            <label for="CheckBoxApproveDep">Approve</label>
                                        </div>
                                    </div>
                                    </div>--%>
                                    <div class="col-sm-10">
                                        <div class="col-sm-0 font-weight-bold">
                                            <label for="TextBoxUserGroup">User</label>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="CheckBoxTampilUser" runat="server" />
                                                <label for="CheckBoxTampilUser">Tampil</label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="CheckBoxSimpanUser" runat="server" />
                                                <label for="CheckBoxSimpanUser">Simpan</label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="CheckBoxHapusUser" runat="server" />
                                                <label for="CheckBoxHapusUser">Hapus</label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="CheckBoxPrintUser" runat="server" />
                                                <label for="CheckBoxPrintUser">Print</label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="CheckBoxApproveUser" runat="server" />
                                                <label for="CheckBoxApproveUser">Approve</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-10">
                                        <div class="col-sm-0 font-weight-bold">
                                            <label for="TextBoxUserGroup">User Group Setting</label>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="CheckBoxTampilUserGS" runat="server" />
                                                <label for="CheckBoxTampilUserGS">Tampil</label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="CheckBoxSimpanUserGS" runat="server" />
                                                <label for="CheckBoxSimpanUserGS">Simpan</label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="CheckBoxHapusUserGS" runat="server" />
                                                <label for="CheckBoxHapusUserGS">Hapus</label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="CheckBoxPrintUserGS" runat="server" />
                                                <label for="CheckBoxPrintUserGS">Print</label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="CheckBoxApproveUserGS" runat="server" />
                                                <label for="CheckBoxApproveUserGS">Approve</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-10">
                                        <asp:Button ID="TombolSimpan" runat="server" Text="Simpan" class="btn btn-primary" OnClick="TombolSimpan_Click" />
                                    </div>
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

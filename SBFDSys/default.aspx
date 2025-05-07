<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="SBFDSys._default" %>


<!DOCTYPE html>

<link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.3.1/css/all.css">
<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css">
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/css/bootstrap.min.css ">
<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/js/bootstrap.bundle.min.js"></script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="~/Content/Images/bprsblogo.png" />
    <title>SB FRAUD DETECTION SYSTEM</title> 
    
    	
    
    <!--Fontawesome CDN-->
	

	<!--Custom styles-->
	<%--<link rel="stylesheet" type="text/css" href="styles.css">--%>
    <link rel="stylesheet" type="text/css" href="~/css/loginstyle.css" />

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
<body>
    <%--<form id="form1" runat="server">--%>
    
        <div class="container">
	        <div class="d-flex justify-content-center h-100">
		        <div class="card">
			        <div class="card-header">
				        <h3>Masuk</h3>
				        <div class="d-flex justify-content-end social_icon">
				        </div>
			        </div>
                    <form id="form1" runat="server">
			            <div class="card-body">
					        <div class="input-group form-group">
						        <div class="input-group-prepend">
							        <span class="input-group-text"><i class="fas fa-user"></i></span>
						        </div>
						        <asp:TextBox ID="TextBoxLoginId" type="text" runat="server" class="form-control" placeholder="Username"  ></asp:TextBox>
						
					        </div>
					        <div class="input-group form-group">
						        <div class="input-group-prepend">
							        <span class="input-group-text"><i class="fas fa-key"></i></span>
						        </div>
						        <asp:TextBox ID="TextBoxPassword" type="password" runat="server" class="form-control" placeholder="Password"></asp:TextBox>
					        </div>
					        <div class="row align-items-center remember ml-3">
						        <input type="checkbox">&nbsp;Ingat Saya
					        </div>
					        <div class="form-group">
                                <asp:Button ID="ButtonLogin" runat="server" Text="Setuju" class="btn float-right login_btn" OnClick="ButtonLogin_Click" />
					        </div>
			            </div>

			            <div class="card-footer">
				            <div class="d-flex justify-content-center links">
					            Tidak Punya Akun?<a href="#">&nbsp;Hub Admin</a>
				            </div>
				            <div class="d-flex justify-content-center">
					            <asp:LinkButton runat="server" Text=" Lupa Password?" ID="LinkButtonLupaPassword" OnClick="LinkButtonLupaPassword_Click"> </asp:LinkButton>
				            </div>

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
                                 <div class="modal fade" id="ModalLupaPassword" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                    <div class="modal-dialog" role="document">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h5 class="modal-title" id="title">Lupa Password?</h5>

                                                <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">×</span>
                                                </button>
                                            </div>
                                            <div class="modal-body">
                                                <div class="card-body">
                                                    <div class="form-group row justify-content-center">
                                                        <div class="col-sm-12 mb-3 mb-sm-0">
                                                            <label>No Karyawan</label>
                                                            <asp:TextBox ID="TextBoxLoginIdLupaPassword" type="text" runat="server" class="form-control" placeholder="No Karyawan" autocomplete="off"  ></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <div class="form-group row justify-content-center">
                                                        <div class="col-sm-12 mb-3 mb-sm-0">
                                                            <label>Email</label>
                                                            <asp:TextBox ID="TextBoxEmail" type="email" runat="server" class="form-control" placeholder="Email" autocomplete="off"  ></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <button class="btn btn-secondary" type="button" data-dismiss="modal">Batal</button>
                                                <asp:Button runat="server" ID="ButtonSendLupaPassword" class="btn btn-primary" type="button" Text="Kirim Email" OnClick="ButtonSendLupaPassword_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            <div class="modal fade" id="ModalGantiPassword" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                    <div class="modal-dialog" role="document">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h5 class="modal-title" id="title">Ubah Password</h5>

                                                <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">×</span>
                                                </button>
                                            </div>
                                            <div class="modal-body">
                                                <div class="card-body">
                                                    <div class="form-group row justify-content-center">
                                                        <div class="col-sm-12 mb-3 mb-sm-0">
                                                            <label>Password Baru</label>
                                                            <asp:TextBox ID="TextBoxPassBaru" type="password" runat="server" class="form-control" placeholder="Password Baru" autocomplete="off"  ></asp:TextBox>
                                                            
                                                        </div>
                                                    </div>
                                                    <div class="form-group row justify-content-center">
                                                        <div class="col-sm-12 mb-3 mb-sm-0">
                                                            <label>Ulangi Password</label>
                                                            <asp:TextBox ID="TextBoxUlangPass" type="password" runat="server" class="form-control" placeholder="Ulangi Password" autocomplete="off"  ></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-body">
                                                <asp:Label ID="Label1" runat="server" Text="Anda masih menggunakan Password lama, Silahkan Ubah Password Anda"></asp:Label>
                                            </div>
                                            <div class="modal-footer">
                                                <button class="btn btn-secondary" type="button" data-dismiss="modal">Batal</button>
                                                <asp:Button runat="server" ID="ButtonUbahPassword" class="btn btn-primary" type="button" Text="Ubah" OnClick="ButtonUbahPassword_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            

			             </div>
                    </form>
		        </div>
	        </div>
        </div>



</body>
</html>

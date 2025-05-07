<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopBar.ascx.cs" Inherits="SBFDSys.TopBar" %>


 <!-- Topbar -->
        <nav class="navbar navbar-expand navbar-light bg-dark topbar mb-4 static-top shadow">

          <!-- Sidebar Toggle (Topbar) -->
          <button id="sidebarToggleTop" class="btn btn-link d-md-none rounded-circle mr-3">
            <i class="fa fa-bars"></i>
          </button>

          <!-- Topbar Search -->
            <form class="d-none d-sm-inline-block form-inline mr-auto ml-md-3 my-2 my-md-0 mw-100 navbar-search">
                <div class="input-group">
                
                    <h1 class="h3 mb-2 text-white">SB FRAUD DETECTION SYSTEM</h1>
                       
                </div>
            </form>

          <!-- Topbar Navbar -->
          <ul class="navbar-nav ml-auto">

             <div style="display: flex; align-items: center; gap: 10px;">
                 <i class="fas fa-fw fa-user" style="font-size: 18px; color: white;"></i>
                 <asp:Label runat="server" ID="LabelUserKar" Text="Nama" Font-Size="Medium" ForeColor="White" Style="white-space: nowrap; width: auto;"/>
             </div>

            <div class="topbar-divider d-none d-sm-block"></div>

            <!-- Nav Item - User Information -->
            <li class="nav-item dropdown no-arrow">

              <a class="nav-link dropdown-toggle" href="#" id="userDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <span class="mr-2 d-none d-lg-inline text-light small"><asp:Label runat="server" ID="LabelUserName"></asp:Label></span>
                <img class="img-profile rounded-circle" src="https://sbfds.bprsb-online.com/Content/Images/bprsblogo.png"">
              </a>
              <!-- Dropdown - User Information -->
              <div class="dropdown-menu dropdown-menu-right shadow animated--grow-in" aria-labelledby="userDropdown">

                  <a class="dropdown-item" href="#UbahPassword" data-toggle="modal" data-target="#ModalGantiPassword">
                        <i class="fas fa-user fa-sm fa-fw mr-2 text-white-400"></i>Ubah Password
                  </a>
          
                  
                  <a class="dropdown-item" runat="server" ID="menuRestore" href="#RestoreDB" data-toggle="modal" data-target="#ModalRestoreDB">
                        <i class="fas fa-cogs fa-sm fa-fw mr-2 text-white-400"></i>Restore Database
                  </a>

                  <a class="dropdown-item" runat="server" id="menuLog" href="Logs.aspx">
                        <i class="fas fa-list fa-sm fa-fw mr-2 text-white-400"></i>Log Aktifitas
                  </a>

                  <%--<div class="dropdown-divider"></div>--%>
                  <a class="dropdown-item" href="#Logout" data-toggle="modal" data-target="#logoutModal">
                        <i class="fas fa-sign-out-alt fa-sm fa-fw mr-2 text-light-400"></i>Keluar
                  </a> 

              </div>
            </li>

          </ul>

        </nav> 

<!-- Error Modal-->
                    <div class="modal fade" id="logoutModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="exampleModalLabel">Konfirmasi</h5>
                                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <asp:Label ID="LabelError" runat="server" Text="Yakin ingin keluar dari FDSys?"></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <button class="btn btn-secondary" type="button" data-dismiss="modal">Cancel</button>
                                    <asp:Button runat="server" CssClass="btn btn-primary" Text="OK" OnClick="ButtonLogOut_Click"></asp:Button>

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
                                            <div class="modal-footer">
                                                <button class="btn btn-secondary" type="button" data-dismiss="modal">Batal</button>
                                                <asp:Button runat="server" ID="ButtonUbahPassword" class="btn btn-primary" type="button" Text="Ubah" OnClick="ButtonUbahPassword_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                    <div class="modal fade" id="ModalRestoreDB" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="static">
                                        <div class="modal-dialog" role="document">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h5 class="modal-title" id="title">Restore DB</h5>

                                                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                                        <span aria-hidden="true">×</span>
                                                    </button>
                                                </div>
                                                <div class="modal-body">
                                                    <div>
                                                       <h2>Restore Database</h2><br />
                                                        <%--<form id="form1" runat="server">--%>
                                                            <div>
                                                                <label for="fileBackup">Choose Backup File:</label><br />
                                                                <asp:FileUpload ID="fileBackup" runat="server" />
                                                            </div><br />
                                                            <div>
                                                                <asp:Button ID="btnRestore" runat="server" BackColor="BlueViolet" ForeColor="White" Text="Restore Database" OnClick="btnRestore_Click" />
                                                            </div>
                                                            <div id="statusMessage" runat="server" style="color: green; margin-top: 20px;">
                                                                <!-- Status akan ditampilkan di sini -->
                                                            </div>
                                                       <%-- </form>--%>
                                                        
                                                    </div>
                                                </div>
                                                <div class="modal-body">
                                                    <%--<div>
                                                        <asp:Button ID="ButtonClose" runat="server" BackColor="RoyalBlue" ForeColor="White" Text="Close" OnClick="ButtonClose_Click" />
                                                    </div>--%>
                                                </div>
                                                
                                            </div>
                                        </div>
                                        

                                    </div>
                                    

                <div class="modal fade" id="errorpass" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Error Occured!</h5>
                                <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="Label1" runat="server" Text="Ada kesalahan, Silahkan menghubungi SKSI"></asp:Label>
                            </div>
                            <div class="modal-footer">
                                <%--<button class="btn btn-secondary" type="button" data-dismiss="modal">OK</button>--%>
                                <%--<asp:button runat="server" class="btn btn-secondary" type="button" data-dismiss="modal" Text="OK" ID="buttonReTry"  OnClick="buttonReTry_Click"> </asp:button>--%>
                                <asp:Button runat="server" Text="OK" OnClick="buttonReTry_Click" class="btn btn-secondary"/>
                            </div>
                        </div>
                    </div>
                </div>
        <!-- End of Topbar -->

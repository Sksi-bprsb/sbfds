<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboards.aspx.cs" Inherits="GreenArsip.Dashboards" %>--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboards.aspx.cs" Inherits="SBFDSys.Dashboards" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>



<%@ Register Src="SideBarAdmin.ascx" TagPrefix="uc1" TagName="SideBarAdmin" %> 
<%@ Register Src="TopBar.ascx" TagPrefix="uc1" TagName="TopBar" %>


<link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.3.1/css/all.css">
<link rel="stylesheet" href="css/sb-admin-2.min.css"  />

<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/css/bootstrap.min.css">
<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script> <%--Untuk Dropdown--%>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/js/bootstrap.bundle.min.js"></script>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="../Content/Images/bprsblogo.png" />
    <title>Administrator</title>

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
                             <!-- DataTales Example -->
                                <div class="flex-lg-column shadow mb-2">
                                       <div class="card-header py-3 bg-info">
                                            <h6 class="m-0 font-weight-bold text-white">Beranda</h6>
                                       </div>
                                            <br>
                                            <div class="card-body">
                                                <div class="d-flex justify-content-around">
                                                    <div class="flex-column">
                                                        <a href="#" class="btn btn-success btn-lg align-content-center align-bottom border-bottom" style="width: 400px; height: 100px;">
                                                            <span class="icon text-white-50">
                                                                <i class="fas fa-calendar-check"></i>
                                                            </span>
                                                            <span class="text"><asp:Label ID="lblLatestTransactionDate" runat="server" Text=""></asp:Label></span>
                                                        </a>
                                                    </div>
                                                    <div class="flex-column">
                                                        <a href="#" class="btn btn-danger btn-lg align-content-center align-bottom border-bottom" style="width: 400px; height: 100px;>
                                                            <span class="icon text-white-50">
                                                                <i class="fas fa-arrow-alt-circle-down"></i>
                                                            </span>
                                                            <span class="text"><asp:Label ID="LoadCountTransactionDate" runat="server"></asp:Label></span>
                                                        </a>
                                                    </div>
                                                    
                                                    <div class="flex-column">
                                                        <a href="#" class="btn btn-danger btn-lg align-content-center align-bottom border-bottom" style="width: 400px; height: 100px;>
                                                            <span class="icon text-white-50 text-center">
                                                                <i class="fas fa-book-reader"></i>
                                                            </span>
                                                            <span class="text"><asp:Label ID="LoadBulanTransaction" runat="server"></asp:Label></span>
                                                        </a>
                                                    </div>
                                                </div>
                                            </div>  
                                  </div>
                                       <div class="d-flex justify-content-around">
                                            <%--<div>
                                               <h2>Restore Database PostgreSQL</h2>
                                                    <div>
                                                        <label for="fileBackup">Choose Backup File:</label>
                                                        <asp:FileUpload ID="fileBackup" runat="server" />
                                                    </div><br />
                                                    <div>
                                                        <asp:Button ID="btnRestore" runat="server" Text="Restore Database" OnClick="btnRestore_Click" />
                                                    </div>
                                                    <div id="statusMessage" runat="server" style="color: green; margin-top: 20px;">
                                                        <!-- Status akan ditampilkan di sini -->
                                                    </div>
                                            </div>--%>
                                            <div>
                                                    <images>
                                                       <%--<p>Path Gambar: bprslogo.png</p>--%>
                                                        <img class="img-profile" src="Content/Images/trans.png" Height="500" width="900"">
                                                        <%--<img src="/Image/bprslogo.png" alt="Logo BPRS" width="100" />--%>

                                                    </images>
                                            </div>


                                            <%--<div>  
                                                <asp:Chart ID="ChartDok" runat="server" Height="500"  Width="800px">  
                                                    <series>  
                                                        <asp:Series Name="Series1" XValueMember="0" YValueMembers="2" ChartType="Pie">  
                                                        </asp:Series>  
                                                    </series>  
                                                    <chartareas>  
                                                        <asp:ChartArea Name="ChartArea1">  
                                                        </asp:ChartArea>  
                                                    </chartareas>  
                                                </asp:Chart>  
                                            </div>--%>
                                        </div>
                                        
                                </div>
                        <p class="card-text"></p>
                                

                    
                                            <!-- Error Modal-->
                                            <div class="modal fade" id="ErrorModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                                <div class="modal-dialog" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="exampleModalLabel">Information</h5>
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

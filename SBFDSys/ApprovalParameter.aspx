<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboards.aspx.cs" Inherits="GreenArsip.Dashboards" %>--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApprovalParameter.aspx.cs" Inherits="SBFDSys.ApprovalParameter" %>

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
                                            <h6 class="m-0 font-weight-bold text-white">Approval Parameter</h6>
                                       </div>
                                            <br>
                                    <div class="col-md-10 mt-5 pr-5 pt-5 p-5">
                                        <div class="card">
                                            <div class="card-header">
                                                PARAMETER BARU
                                            </div>
                                            <div class="card-body">
                                                
                                                
                                                <div class="form-group row">
                                                    <label for="LabelNamaParameter" class="col-sm-3 col-form-label">Nama Parameter</label>
                                                    <div class="col-sm-9 mt-1">
                                                        <asp:Label ID="LabelNamaParameter" type="text" runat="server" class="form-control"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="LabelValue1" class="col-sm-3 col-form-label">Value1</label>
                                                    <div class="col-sm-9 mt-1 mb-2">
                                                        <asp:Label ID="LabelValue1" type="text" runat="server" class="form-control"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="LabelValue2" class="col-sm-3 col-form-label">Value2</label>
                                                    <div class="col-sm-9 mt-1 mb-2">
                                                        <asp:Label ID="LabelValue2" type="date" runat="server" class="form-control"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="LabelValue3" class="col-sm-3 col-form-label">Value3</label>
                                                    <div class="col-sm-9 mt-1 mb-2">
                                                        <asp:Label ID="LabelValue3" type="date" runat="server" class="form-control"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="LabelStatus" class="col-sm-3 col-form-label">Status</label>
                                                    <div class="col-sm-9 mt-1 mb-2">
                                                        <asp:Label ID="LabelStatus" type="text" runat="server" class="form-control"></asp:Label>
                                                    </div>
                                                </div>
                                                
                                                <asp:Button runat="server" ID="buttonSetuju" class="btn btn-primary" Text="Setuju" OnClick="buttonSetuju_Click" />
                                                <asp:Button runat="server" ID="buttonTolak" class="btn btn-primary" Text="Tolak" OnClick="buttonTolak_Click" />
                                            </div>
                                        </div>

                                    </div>
                                           
                                </div>
                                       
                         </div>
                        
                                
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

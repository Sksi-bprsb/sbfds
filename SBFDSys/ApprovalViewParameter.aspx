<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboards.aspx.cs" Inherits="GreenArsip.Dashboards" %>--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApprovalViewParameter.aspx.cs" Inherits="SBFDSys.ApprovalViewParameter" %>

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
                                    <div class="table-responsive">
                                        <asp:ListView runat="server" ID="ListViewData">
                                            <LayoutTemplate>
                                                <table class="table table-bordered">
                                                    <thead>
                                                        <tr>
                                                            <th>Name Parameter</th>
                                                            <th>Value1</th>
                                                            <th>Value2</th>
                                                            <th>Value3</th>
                                                            <th>Approver</th>
                                                            <th>Status</th>
                                                            <th>Function</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr runat="server" id="itemPlaceholder"></tr>
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td><a href="ApprovalViewParameter.aspx?ParameterPendingId=<%# Eval("ParameterPendingId") %>">
                                                        <%# Eval("Nama") %></a></td>
                                                    <td><%# Eval("Value1") %></td>
                                                    <td><%# Eval("Value2") %></td>
                                                    <td><%# Eval("Value3") %></td>
                                                    <td><%# Eval("ApprovalNama") %></td>
                                                    <td><%# Eval("Status") %></td>

                                                    <td>
                                                        <asp:Button ID="ButtonApprovalParameter" runat="server" Text="Tampilkan" CausesValidation="false"
                                                            Visible="true" CssClass="btn btn-danger" OnClick="ButtonApprovalParameter_Click" />
                                                    </td>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
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

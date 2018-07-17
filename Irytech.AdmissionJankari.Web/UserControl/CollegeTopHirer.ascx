<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeTopHirer.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeTopHirer" %>
<%@ Register Src="/UserControl/CustomPaging.ascx" TagPrefix="AJ" TagName="ucCustomPaging" %>
<div class="box1" runat="server" id="divTopHirer">
                    <h3 class="clgSearchH2 h2margin">Top Hirer</h3>
 <div class="box">
                             <asp:Repeater ID="rptTopHirer" runat="server"  >
        <HeaderTemplate>
                                            <table class="grdView">
                                                <tr>
                                                    <td >
                                                       Company Name
                                                    </td>
                                                    <td >
                                                       Student Hired
                                                    </td>
                                                    <td >
                                                        Package
                                                    </td>
                                                   
                                                </tr>
                                           
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           
                                                <tr >
                                                   
                                                        <td >
                                                        <%#Eval("CollegeBranchCoursePlacementCompanyName")%>
                                                    </td>
                                                    <td >
                                                        <%#Eval("CollegeBranchCoursePlacementNoOfStudentHired")%>
                                                    </td>
                                                  
                                                    <td >
                                                        <%#Eval("CollegeBranchCoursePlacementAvgSalaryOffered")%>
                                                    </td>
                                                    
                                                </tr>
                                          
                                        </ItemTemplate><FooterTemplate>
                         
                            </table></FooterTemplate>
                                    </asp:Repeater>
                                    </div>
<AJ:ucCustomPaging runat="server" ID="pagerHighLightsCollege"
    />
                                    </div>
                             
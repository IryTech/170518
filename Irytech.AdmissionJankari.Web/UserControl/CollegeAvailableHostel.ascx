<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeAvailableHostel.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeAvailableHostel" %>
<asp:Label runat="server" Visible="false"   ID="lblHostel" ></asp:Label>

<div class="box1" runat="server" id="divHostel">
<h3 class="streamCompareH3" >Hostel</h3>
<hr class="hrline" />
    
<div class="box">
<asp:Repeater ID="rptCollegeHostel" runat="server" >
                                        <HeaderTemplate>
                                           <table class="grdView">
                                                <tr style="background-color:#eff2f7;">
                                                    <td >
                                                        Hostel
                                                    </td>
                                                    <td >
                                                        Location
                                                    </td>
                                                    <td >
                                                        Charge
                                                    </td>
                                                    <td >
                                                       Internet
                                                    </td>
                                                        <td>
                                                        Ac
                                                    </td>
                                                    <td >
                                                        Laundry
                                                    </td>
                                                    <td >
                                                        PowerBackUp
                                                    </td>
                                                </tr>
                                           
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                      
                                                <tr>
                                                    <td >
                                                     
                                                        <%#Eval("HostelCategoryName")%>
                                                     
                                                    </td>
                                                        <td >
                                                        <%#Eval("CollegeBranchCourseHostelLocation")%>
                                                    </td>
                                                   
                                                    <td >
                                                            <%#Eval("CollegeBranchCourseHostelCharge")%>
                                                    </td>
                                                    <td >
                                                    <%# ChangeValue((bool)Eval("IsCollegeBranchCourseHostelHasInternet"))%>
                                                       
                                                    </td>
                                                     <td >
                                                       <%# ChangeValue((bool)Eval("IsCollegeBranchCourseHostelHasAC"))%>
                                                     
                                                    </td>
                                                       <td >
                                                        <%# ChangeValue((bool)Eval("IsCollegeBranchCourseHostelHasLoundry"))%>
                                                      
                                                    </td>
                                                       <td >
                                                       <%# ChangeValue((bool)Eval("IsCollegeBranchCourseHostelHasPowerBackup"))%>
                                                       
                                                    </td>
                                                    
                                                </tr>
                                                 
                                        </ItemTemplate><FooterTemplate>
                         
                            </table></FooterTemplate>
                                    </asp:Repeater>
                                    </div>
                                    </div>
                                 
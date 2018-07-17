<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeAvailabelCourseExam.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeAvailabelCourseExam" %>
<asp:Label runat="server" ID="lblExmResult" Visible="false"></asp:Label>
<div class="box1" runat="server" id="divExamResult">

                    <h3 class="streamCompareH3" >
                        Admission Criteria</h3>
                        <hr class="hrline" />
 <div class="box">
                             <asp:Repeater ID="rptEntranceExam" runat="server"  >
        <HeaderTemplate>
                                            <table class="grdView">
                                                <tr style="background-color:#eff2f7;">
                                                    <td >
                                                       Exam Name
                                                    </td>
                                                    <td >
                                                       Popular Name
                                                    </td>
                                                    <td >
                                                        Eligibility
                                                    </td>
                                                   
                                                </tr>
                                           
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           
                                                <tr >

                                                        <td >
                                                        <%#Eval("ExamName")%>
                                                    </td>
                                                    <td> <%# !string.IsNullOrEmpty(Convert.ToString(Eval("ExamPopularName"))) ? Eval("ExamPopularName") : "N/A"%>
                                                  
                                                    </td>
                                                  
                                                    <td >
                                                        <%# !string.IsNullOrEmpty(Convert.ToString(Eval("CollegeExamEligibilty"))) ? Eval("CollegeExamEligibilty") : "N/A"%>
                                                    </td>
                                                    
                                                </tr>
                                          
                                        </ItemTemplate><FooterTemplate>
                         
                            </table></FooterTemplate>
                                    </asp:Repeater>
                                    </div>
                                  
                                    </div>
                             
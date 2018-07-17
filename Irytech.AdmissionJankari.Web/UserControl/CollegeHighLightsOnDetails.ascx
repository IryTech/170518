<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeHighLightsOnDetails.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeHighLightsOnDetails" %>
<%@ Register TagPrefix="AJ" TagName="CustomPaging" Src="~/UserControl/CustomPaging.ascx" %>
<div class="box1"  runat="server" id="divHighLights" visible="false" >
 <h3 class="streamCompareH3">HighLights</h3>
 <hr class="hrline" />
 <div class="box">

 <asp:Repeater ID="rptHighLights" runat="server">
                            <ItemTemplate>
	                                  <h3>
	                                      <%#Convert.ToString(Eval("CollegeBranchCourseHighlight")).Length > 25 ? Convert.ToString(Eval("CollegeBranchCourseHighlight")).Substring(0, 25) : Convert.ToString(Eval("CollegeBranchCourseHighlight"))%> 
                              </h3>
                            
                              <%#Eval("CollegeBranchCourseHighlight")%>
                           
                            </ItemTemplate>
                            </asp:Repeater>
                            </div>
                                <AJ:CustomPaging ID="ucHighLightPager" runat="server" />
                            </div>
                     
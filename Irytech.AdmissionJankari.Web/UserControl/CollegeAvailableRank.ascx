<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeAvailableRank.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeAvailableRank" %>
 <asp:Label runat="server" ID="lblCollegeRankResult" Visible="false"></asp:Label>
<div class="box1" runat="server" id="divCollegeRank">
   
                    <h3 class="streamCompareH3" >
                        College Rank</h3>
 <div class="box">
                             <asp:Repeater ID="rptCollegeRank" runat="server"  >
        <HeaderTemplate>
                                            <table class="grdView">
                                                <tr style="background-color:#eff2f7;">
                                                    <td >
                                                      Rank Source
                                                    </td>
                                                    <td >
                                                       Rank Year
                                                    </td>
                                                    <td >
                                                        Rank Over All
                                                    </td>
                                                   
                                                </tr>
                                           
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           
                                                <tr >
                                                   
                                                        <td >
                                                        <%#Eval("RankSourceName")%>
                                                    </td>
                                                    <td >
                                                        <%#Eval("CollegeRankYear")%>
                                                    </td>
                                                  
                                                    <td >
                                                        <%#Eval("CollegeOverAllRank")%>
                                                    </td>
                                                    
                                                </tr>
                                          
                                        </ItemTemplate><FooterTemplate>
                         
                            </table></FooterTemplate>
                                    </asp:Repeater>
                                    </div>
                                  
                                    </div>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeCoursePlacement.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeCoursePlacement" %>

 <asp:Panel ID="pnlCollegePlacement" runat="server" Visible="false">
                       <div class="column" id="Place" style="min-height:0px;">
                           <div class="dragbox" id="Div2">
                                <h2 class="headerBackStrip">Placements</h2>
                                    <br />
                                <div class="dragbox-content" style="display:inline-block;">
                               <div style=" float:right; width:485px;">
                                   <span style="float: left; padding-left: 6px; width: auto">
                                       <asp:Chart ID="chartPlacement" runat="server" Palette="None" Width="480px" BorderlineColor="White">
                                           <Series>
                                               <asp:Series Name="Series111" ChartType="Column" Font="Arial, 12pt"
                                                    CustomProperties="DrawingStyle=Cylinder"
                                                   IsValueShownAsLabel="True" BackSecondaryColor="Transparent" Color="#fad00d" XAxisType="Primary">
                                               </asp:Series>
                                           </Series>

                                         <ChartAreas>
                                               <asp:ChartArea Name="ChartArea3" IsSameFontSizeForAllAxes="true" BackColor="#efffec">
                                                   <Area3DStyle Enable3D="true" LightStyle="Realistic" ></Area3DStyle>
                                                   <AxisX Interval="1" Title="Year" TitleForeColor="Teal" TitleFont="Arial, 12pt, style=Bold">
                                                    <MajorGrid Enabled="false" />
                                                   </AxisX>
                                                   <AxisY Interval="20" Title="Placement in %" TitleForeColor="Teal" TitleFont="Arial, 12pt, style=Bold">
                                                    <MajorGrid Enabled="false" />
                                                   </AxisY>
                                                   <InnerPlotPosition Auto="true" /> 
                                               </asp:ChartArea>
                                           </ChartAreas>
                                           <Titles>
                                               <asp:Title BackColor="Transparent" 
                                                   Font="Microsoft Sans Serif, 16pt, style=Bold" ForeColor="ForestGreen" 
                                                   Name="Title1">
                                               </asp:Title>
                                           </Titles>
                                       </asp:Chart>
                                   </span>
                               </div>
                                </div>
                            </div>
                       </div>
                   </asp:Panel>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ColleegRankChart.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.ColleegRankChart" %>


                        <div id="CollegeRank" runat="server" class="box1">       
                                <h3 class="streamCompareH3">College Rank</h3>
                               
                                  <label>Year:</label>
                                                   <asp:DropDownList ID="ddlYear"  Width="20%" runat="server"  AutoPostBack="true" 
                                                     Font-Bold="true" 
                                                    onselectedindexchanged="ddlYear_SelectedIndexChanged"> 
                                                   </asp:DropDownList>
                                              
                                              <div class="boxPlane"  runat="server">
                                                   <center>
                                                       <asp:Chart ID="rankChart" ImageStorageMode="UseImageLocation" runat="server" SuppressExceptions="True" 
                                                ImageLocation="~/Image/tempImage/ChartPic_#SEQ(1000,30)" Palette="None" Width="500px" Height="400px" BorderlineColor="White">
                                                           <Series>
                                                               <asp:Series Name="rankSeries" ChartType="Column" Font="Arial, 12pt"
                                                                    CustomProperties="DrawingStyle=Cylinder" 
                                                                   IsValueShownAsLabel="True" BackSecondaryColor="Transparent" Color="teal" XAxisType="Primary">
                                                               </asp:Series>
                                                           </Series>

                                                         <ChartAreas>
                                                               <asp:ChartArea Name="ChartArea2" IsSameFontSizeForAllAxes="true" BackColor="#eff2f7">
                                                                   <Area3DStyle Enable3D="true" LightStyle="Realistic" ></Area3DStyle>
                                                                   <AxisX Title="Source Name" TitleForeColor="#415983" TitleFont="Arial, 12pt, style=Bold">
                                                                    <MajorGrid Enabled="false" />
                                                                   </AxisX>
                                                                   <AxisY Interval="30" Title="Overall Rank" TitleForeColor="Maroon" TitleFont="Arial, 12pt, style=Bold">
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
                                                       </center>
                                                   </div>
                                               
                                  
                           </div>
                      
                 
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeAvailablePlaceMent.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeAvailablePlaceMent" %>
     <%@ Register src="CustomPaging.ascx" tagname="CustomPaging" tagprefix="AJ" %>
    <asp:UpdatePanel runat="server">
<ContentTemplate>

    <asp:Label runat="server" ID="lblCollegePlacement" Visible="false"></asp:Label>

<div class="box1" runat="server" id="divCollegePlacement">

                    <h3 class="streamCompareH3" >
                        College Placement</h3>
                        <hr class="hrline" />
 <div class="box">
                             <asp:Repeater ID="rptCollegePlacement" runat="server"  >
        <HeaderTemplate>
                                            <table class="grdView">
                                                <tr style="background-color:#eff2f7;">
                                                    <td >
                                                     Company
                                                    </td>
                                                    <td >
                                                       Year
                                                    </td>
                                                    <td >
                                                      Student Hired
                                                    </td>
                                                    <td >
                                                      Avgerage Salary
                                                    </td>
                                                </tr>
                                           
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           
                                                <tr >
                                                   
                                                        <td >
                                                        <%#Eval("CollegeBranchCoursePlacementCompanyName")%>
                                                    </td>
                                                    <td >
                                                        <%#Eval("CollegeBranchCoursePlacementYear")%>
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
                                  
                                    </div>
                                    <AJ:CustomPaging ID="PlacementPager" runat="server" />
<div id="divImage" class="hide" >
<img src="/image.axd?Common=LoadingImage.gif" alt="Loading"  />
</div>      

<asp:Label ID="lblText" runat="server" Text=""></asp:Label></div>
</ContentTemplate>
</asp:UpdatePanel>
   <script type="text/javascript">
       // Get the instance of PageRequestManager.
       var prm = Sys.WebForms.PageRequestManager.getInstance();
       // Add initializeRequest and endRequest
       prm.add_initializeRequest(prm_InitializeRequest);
       prm.add_endRequest(prm_EndRequest);

       // Called when async postback begins
       function prm_InitializeRequest(sender, args) {
           // get the divImage and set it to visible

           $("#divImage").show();

           // reset label text
           var lbl = $get('<%= this.lblText.ClientID %>');
           lbl.innerHTML = '';

       }

       // Called when async postback ends
       function prm_EndRequest(sender, args) {
           // get the divImage and hide it again


           $("#divImage").hide();

       }
         </script>
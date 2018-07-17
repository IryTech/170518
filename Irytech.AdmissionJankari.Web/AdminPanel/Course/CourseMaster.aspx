<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" ValidateRequest="true" CodeBehind="CourseMaster.aspx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Course.CourseMaster" %>

<%@ Register Src="../../UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="aj" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
 <ul class="addPage_utility">
        <li class="fright" style="width: 123px !important;">
            <div class="navbar-inner">
                <a href="AddCourse.aspx"  class="insertIco">Add Course</a>
                <div class="clear">
                </div>
            </div>
        </li>
        </ul>
        
         <fieldset>
         
         <legend>Course Search</legend>
         <ul class="options-bar">
                        <li >
                            <label class="searchlabel">
                                Course Name</label>
                            <asp:TextBox runat="server" CssClass="autocomplete" placeholder="Enter Course Name" TabIndex="1" Width="63%"></asp:TextBox>
                            <asp:Button  runat="server" Text="Search" CssClass="searchbtn " TabIndex="2"></asp:Button>
                        </li>
                        
                    </ul>
         
         </fieldset>

          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                
              
                <asp:Label ID="lblInform" runat="server" Text="" Visible="false" CssClass="info">
                </asp:Label>
               
                
                <div class="grdOuterDiv">
                    
                        <%--<asp:Label runat="server" Text="" ID="lblEditStatus"></asp:Label> --%>
                    <asp:Repeater ID="rptCourseCategoryData" runat="server" >
                        <HeaderTemplate>
                            <table class="grdView">
                                <tr>
                                    <th>
                                        S.No
                                    </th>
                                    <th>
                                        Course Name
                                    </th>
                                    <th>
                                        ShortName
                                    </th>
                                    <th>
                                        Popular Name
                                    </th>
                                    <th>
                                        Image
                                    </th>
                                    <th>
                                        URL
                                    </th>
                                    <th>
                                        Status
                                    </th>
                                    <th>
                                        Action
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# Eval("SrNo") %>
                                </td>
                                <td>
                                    <%# Eval("CourseName")%>
                                </td>
                                <td>
                                    <%# Eval("CourseShortName")%>
                                </td>
                                <td>
                                    <%# Eval("CoursePopularName")%>
                                </td>
                                <td>
                                    <img src='<%# String.Format("{0}{1}","/image.axd?Course=",string.IsNullOrEmpty(Eval("CourseImage").ToString()) ?"NoImage.jpg":Eval("CourseImage")) %>'
                                        width="60px" height="60" alt='<%# Eval("CourseName")%>' title='<%# Eval("CourseName")%>' />
                                </td>
                                <td>
                                    <%# Eval("CourseUrl")%>
                                </td>
                                <td>
                                    <%# Eval("CourseStatus")%>
                                </td>
                                <td>
                                <a  title="Click to Update the course" id="lnlEdit" href="AddCourse.aspx?courseId=<%# Eval("CourseId")%>" >
                                    <img src="../Images/CommonImages/editIcon.png" width="12px" class="editIconmargin" alt="Edit" title="Edit" /></a>
                                  
                                </td>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                    <Aj:CustomPaging ID="ucCustomPaging" runat="server" />
                </div>
        
             
            </ContentTemplate>
            
        </asp:UpdatePanel>
    
   
    <div id="divImage" class="loading">
                <img src="/image.axd?Common=Loading.gif" alt="Loading" title="Loading" />
            </div>
    
    <script type="text/javascript">
    function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                close();
            }
        }
        function close() {

            $("#fade").hide();
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        // Add initializeRequest and endRequest
        prm.add_initializeRequest(prm_InitializeRequest);
        prm.add_endRequest(prm_EndRequest);

        // Called when async postback begins
        function prm_InitializeRequest(sender, args) {
            // get the divImage and set it to visible
            $("#fade").show();
            $("#divImage").show();
        }
        // Called when async postback ends
        function prm_EndRequest(sender, args) {
            $("#fade").hide();
            $("#divImage").hide();
        }

    </script>
</asp:Content>

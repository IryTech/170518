<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="AddCollegeBanner.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.AddCollegeBanner" %>

<%@ Register Src="~/UserControl/AjaxFileUpload.ascx" TagName="FileUpload" TagPrefix="Aj" %>
<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="Aj" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel runat="server" ID="updateBanner">
    <ContentTemplate>
    <asp:Label runat="server" ID="lblResult" Visible="False"></asp:Label>
    <asp:HiddenField runat="server" ID="hdnBannerId" Value="0"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="hfcollegeBranchCourseId" Value="0"></asp:HiddenField>
    <asp:HiddenField ID="hfCourseId" runat="server" Value="0" />
         
          <ul class="addPage_utility">
        <li class="fright" style="width: 173px !important;">
            <div class="navbar-inner">
               <a href="#" id='sndAddCollegeBanner' class="insertIco" onclick="OpenPoup('divRankSourceInsert','650px','sndAddCollegeBanner');return false;">
                    Add Display Ads</a>
                <div class="clear">
                </div>
            </div>
        </li>
        </ul>
         
         
          <fieldset>
                    <legend>Search</legend>
                    <ul class="options-bar">
                        <li>
                            <label >
                                College:</label>
                            <asp:TextBox runat="server" CssClass="autocomplete" TabIndex="1" ID="txtCollegeSearch" Width="63%"></asp:TextBox>
                             <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="searchbtn " TabIndex="2"
                CausesValidation="false" onclick="btnSearch_Click" ></asp:Button>
                        </li>
                         
                
        <ul>
            <li><label></label> 
                <asp:DropDownList ID="ddlCourseList" runat="server" TabIndex="3" Width="60%" ToolTip="Please Select Course"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlCourseList_SelectedIndexChanged">
                </asp:DropDownList>
            
                <asp:DropDownList ID="ddlAdsType" runat="server" TabIndex="6" ToolTip="Please Select Ads Type"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlAdsType_SelectedIndexChanged">
                </asp:DropDownList>
             </li>
        </ul>
    </fieldset>
    <fieldset>
        <legend>Display ads Master</legend>
        <div>
            <asp:Repeater ID="rptCollegeList" runat="server" OnItemCommand="RptCollegeListItemCommand">
                <HeaderTemplate>
                    <table class="grdView">
                        <tr>
                            <th>
                                College Name
                            </th>
                            <th>
                                Course
                            </th>
                            <th>
                                Ads Code
                            </th>
                            <th>
                                Ads Start Date
                            </th>
                            <th>
                                Ads End Date
                            </th>
                            <th>
                                Ads Status
                            </th>
                            <th>
                                Action
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Eval("AjCollegeBranchName")%>
                        </td>
                        <td>
                            <%# Eval("AjCourseName")%>
                        </td>
                        <td>
                            <%# Eval("AjBannerPosition")%>
                        </td>
                        <td>
                            <%# String.Format("{0}", string.IsNullOrEmpty(Convert.ToString(Eval("AjAdsBannerStartDate"))) ? "NA" : Convert.ToDateTime(Eval("AjAdsBannerStartDate")).ToString("dd/MM/yyyy"))%>
                        </td>
                        <td>
                            <%# String.Format("{0}", string.IsNullOrEmpty(Convert.ToString(Eval("AjAdsBannerEndDate"))) ? "NA" : Convert.ToDateTime(Eval("AjAdsBannerEndDate")).ToString("dd/MM/yyyy"))%>
                        </td>
                        <td>
                            <%# Eval("AjBannerStatus")%>
                        </td>
                        <td>
                            <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("AjBannerId")%>'
                                CommandName="Edit" CausesValidation="false" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </div>
        <Aj:CustomPaging ID="ucCustomPaging" runat="server" />
    </fieldset>
    <div id="divRankSourceInsert" class="popup_block width54perc">
        <fieldset>
            <legend id="lblHeading" runat="server">Add Display Ads </legend>
            <ul>
                <li>
                    <label>
                        College Name:
                    </label>
                    <asp:TextBox ID="txtCollegeName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvCollege" ValidationGroup="register"
                        Display="Dynamic" SetFocusOnError="True" CssClass="error1" ControlToValidate="txtCollegeName">
                       
                 College name can not be blank

                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        <%=Resources.label.Course %>
                    </label>
                    <asp:DropDownList runat="server" ID="ddlCourse" ToolTip="Please select course" TabIndex="1">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvcourseName" ValidationGroup="register"
                        Display="Dynamic" SetFocusOnError="True" CssClass="error1" ControlToValidate="ddlCourse"
                        InitialValue="0"> 
                  Course can not be blank

                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        Ads Priority:
                    </label>
                    <asp:TextBox runat="server" ID="txtPriority" ToolTip="Please enter priorty" TabIndex="3">
                    </asp:TextBox>
                    <Ajex:NumericUpDownExtender ID="txtMinutes_NumericUpDownExtender" runat="server"
                        Enabled="True" Maximum="9" Minimum="1" RefValues="" ServiceDownMethod="" ServiceDownPath=""
                        ServiceUpMethod="" Tag="" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtPriority"
                        Width="50">
                    </Ajex:NumericUpDownExtender>
                    <asp:RequiredFieldValidator runat="server" ID="rfvPrior" ValidationGroup="register"
                        Display="Dynamic" SetFocusOnError="True" CssClass="forerror" ControlToValidate="txtPriority"> 
                    Ads Priority can not be blank

                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        Ads Code:
                    </label>
                    <asp:DropDownList runat="server" ID="ddlBannerPosition" ToolTip="Please select Ads Code"
                        TabIndex="3">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvUserName" ValidationGroup="register"
                        Display="Dynamic" SetFocusOnError="True" CssClass="error1" ControlToValidate="ddlBannerPosition"
                        InitialValue="0"> 
                     Ads Code can not be blank

                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        Ads tool tip:
                    </label>
                    <asp:TextBox runat="server" ID="txtToolTip" ToolTip="Please enter tooltip" TabIndex="4">
                      
                    </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvToolTip" ValidationGroup="register"
                        Display="Dynamic" SetFocusOnError="True" CssClass="error1" ControlToValidate="txtToolTip"> 
                      Ads tool tip can not be blank

                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        Ads URL:
                    </label>
                    <asp:TextBox runat="server" ID="txtUrl" ToolTip="Please enter url" TabIndex="4">
                      
                    </asp:TextBox><span style="line-height: 6px !important;">(Provide complete url, else
                        leave blank)</span> </li>
                <li>
                    <label>
                        Ads Start Date:
                    </label>
                    <asp:TextBox runat="server" ID="txtStartDate" ToolTip="Please enter the Ads Start Date"
                        TabIndex="5">
                    </asp:TextBox>(MM/dd/YYYY)
                    <asp:RequiredFieldValidator runat="server" ID="rfvBannerStartDate" ValidationGroup="register"
                        Display="Dynamic" SetFocusOnError="True" CssClass="error1" ControlToValidate="txtStartDate"> 
                     Ads Start Date can not be blank
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        Ads End Date:
                    </label>
                    <asp:TextBox runat="server" ID="txtEndDate" ToolTip="Please enter the Ads End Date"
                        TabIndex="6">
                    </asp:TextBox>(MM/dd/YYYY)
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ValidationGroup="register"
                        Display="Dynamic" SetFocusOnError="True" CssClass="error1" ControlToValidate="txtEndDate"> 
                     Ads End Date can not be blank
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        Publish Ads:
                    </label>
                    <asp:CheckBox ID="chkbannerStatus" runat="server" Checked="true" />
                </li>
                <li>
                    <label>
                        Ads image</label>
                    <Aj:FileUpload ID="FileUpload1" runat="server" />
                    <asp:HiddenField ID="hdnImageFile" runat="server" />
                    <asp:Image ID="Imgbanner" runat="server" Height="90px" Width="270px" Visible="false" />
                </li>
                <li>
                    <label>
                    </label>
                    <asp:Button ID="btnUpload" runat="server" Text="Save" OnClick="BtnUploadClick" ValidationGroup="register" />
                </li>
            </ul>
        </fieldset>
    </div>
    </ContentTemplate>
      <Triggers>
      <asp:PostBackTrigger ControlID="btnUpload" />
      </Triggers>
    </asp:UpdatePanel>
    <script src="../../Scripts/Autocomplete.js" type="text/javascript"></script>
    <link href="../StyleSheets/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />
    <script src="../JS/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="../JS/jquery.ui.core.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#<%=txtStartDate.ClientID %>").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $("#<%=txtEndDate.ClientID %>").datepicker({
                changeMonth: true,
                changeYear: true
            });
        });
        var url = "../../WebServices/CommonWebServices.asmx/BindCollegesByCourse";
        BindDropDownCommonForAdminAutoComplete($("#<%=txtCollegeSearch.ClientID %>"), "../../WebServices/CommonWebServices.asmx/GetBannerCollegeList")
        $("#<%=ddlCourse.ClientID %>").change(function () {
            var courseid = $("#<%=ddlCourse.ClientID %>").val();
            BindDropDownCommonForAdminAutoCompletebyCourseID($("#<%=txtCollegeName.ClientID %>"), courseid, url);
        })
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                var courseid = $("#<%=ddlCourse.ClientID %>").val();


                BindDropDownCommonForAdminAutoCompletebyCourseID($("#<%=txtCollegeName.ClientID %>"), courseid, url);
                BindDropDownCommonForAdminAutoComplete($("#<%=txtCollegeSearch.ClientID %>"), "../../WebServices/CommonWebServices.asmx/GetBannerCollegeList")

            }
        }

    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="ManageCollegeContactDetails.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.ManageCollegeContactDetails" %>

<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div id="fade">
            </div>
            <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
            <div id="divImage" class="loading">
                <img src="/image.axd?Common=Loading.gif" />
            </div>
            <asp:HiddenField ID="hdnQuery" runat="server" />
            <asp:HiddenField ID="hdnCollegeId" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hdnCollegeCourseId" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hdnFacalityId" runat="server"></asp:HiddenField>
           
                <asp:Label ID="lblInfo" runat="server" Visible="false"></asp:Label>
                <fieldset>
                    <legend>Search </legend>
                    <ul>
                        <li>
                            <label>
                                Course</label>
                            <asp:DropDownList ID="ddlCourse" runat="server" ToolTip="Please Select Course" OnSelectedIndexChanged="ddlCourseList_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                         
                            <%--<label>
                                State</label>--%><asp:DropDownList ID="ddlState" runat="server" ToolTip="Please Select State" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                        <%--<label>
                                City</label>--%>
                            <asp:DropDownList ID="ddlCity" runat="server" ToolTip="Please Select City" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                        </li>
                        <%-- <li>
                    <label>
                        College
                    </label>
                    <asp:TextBox ID="txtName" runat="server" ToolTip="Enter College Name"></asp:TextBox>
                </li>--%>
                        <%--  <li>
                    <label>
                    </label>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" />
                </li>--%>
                    </ul>
               
            <div class="clear" style="margin:0px 5px;">
                <asp:Repeater ID="rptCollegeContactDetails" runat="server" OnItemDataBound="rptCollegeContactDetails_ItemDataBound" OnItemCommand="rptCollegeContactDetails_ItemCommand">
                    <HeaderTemplate>
                        <table class="grdView">
                            <tr>
                                <th>
                                    Contact Details
                                </th>
                                <th>
                                    CollegeName
                                </th>
                                <th>
                                    Action
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <div class="accordion">
                                    <h2 class="accord">
                                        View Contact Details
                                    </h2>
                                    <div>
                                        <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label>
                                        <asp:Repeater ID="rptContactDetails" runat="server" OnItemCommand="rptContactDetails_ItemCommand">
                                            <HeaderTemplate>
                                                <table class="grdView">
                                                    <tr>
                                                        <th>
                                                            Persona Name
                                                        </th>
                                                        <th>
                                                            Desgination
                                                        </th>
                                                        <th>
                                                            Mobile
                                                        </th>
                                                        <th>
                                                            Phone No
                                                        </th>
                                                        <th>
                                                            Email Id
                                                        </th>
                                                        <th>
                                                            Department
                                                        </th>
                                                        <th>
                                                            Action
                                                        </th>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("AJCollegeContactPersonName")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("AjCollegePersonDegisnation")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("AjCollegeContactPersonMobile")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("AjCollegeContactPersonPhoneNo")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("AjCollegeContactPersonEmail")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("AjCollegeContactPersonDept")%>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("AjCollegeContacPersonId")%>' CommandName="Edit" CausesValidation="false" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table></FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <asp:HiddenField ID="hndCollegeBranchCourseId" Value='<%# Eval("CollegeBranchCourseId")%>' runat="server"></asp:HiddenField>
                                <%# Eval("CollegeBranchName")%>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkInsert" Text="Insert" runat="server" CommandName="Insert" CommandArgument='<%# Eval("CollegeBranchCourseId")%>'></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table></FooterTemplate>
                </asp:Repeater>
                <asp:Panel runat="server" ID="pnlPager" CssClass="pagination">
                </asp:Panel>
        </div></fieldset>
            <div id="divCollegeContactInsert" class="popup_block width62perc">
                <fieldset id="basicInfo">
                   <legend id="collegeName" runat="server">
                    </legend>
                    <ul>
                    <li>
                            <label>
                                Person Name</label>
                            <asp:TextBox ID="txtName" runat="server" TabIndex="2" Width="75%" ToolTip="Please Enter Name"></asp:TextBox>
                        </li>
                       
                        <li class="width48perc fleft">
                            <label>
                                Group</label>
                            <asp:DropDownList runat="server" ID="ddlCollegeGroup" TabIndex="1" ToolTip="Please Select Group">
                            </asp:DropDownList>
                        </li>
                         <li class="width48perc fleft">
                            <label>
                                Department</label>
                            <asp:TextBox ID="txtDepartment" runat="server" TabIndex="6" ToolTip="Please Enter Department"></asp:TextBox>
                        </li>
                         
                        <li class="width48perc fleft clear">
                            <label>
                                Designation</label>
                            <asp:TextBox ID="txtDesignation" runat="server" TabIndex="3" ToolTip="Please Enter Designation"></asp:TextBox>
                        </li>
                        <li class="width48perc fleft">
                            <label>
                                EmailId</label>
                            <asp:TextBox ID="txtEmailId" runat="server" TabIndex="4" ToolTip="Please Enter EmailId"></asp:TextBox>
                            <asp:RegularExpressionValidator ValidationExpression="^([a-zA-Z0-9_\-\.']+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" runat="server" ID="revEmailId" Display="Dynamic" ValidationGroup="register" SetFocusOnError="True" CssClass="error" ControlToValidate="txtEmailId"> 
                 Incorrect Email format, please try again

                            </asp:RegularExpressionValidator>
                        </li>
                        <li class="width48perc fleft">
                            <label>
                                Phone</label>
                            <asp:TextBox ID="txtCollegePhone" runat="server" TabIndex="5" ToolTip="Please Enter Phone"></asp:TextBox>
                        </li>
                        <li class="width48perc fleft">
                            <label>
                                Mobile</label>
                            <asp:TextBox ID="txtCollegeMobile" runat="server" TabIndex="5" ToolTip="Please Enter MobileNo"></asp:TextBox>
                            <asp:RegularExpressionValidator ValidationExpression="[1-9][0-9]{9}$" runat="server" ID="revMobile" Display="Dynamic" ValidationGroup="register" SetFocusOnError="True" CssClass="error" ControlToValidate="txtCollegeMobile">  
                Provide 10 digit mobile number

                            </asp:RegularExpressionValidator>
                        </li>
                        
                        <li class="width48perc fleft">
                            <label>
                                Fax</label>
                            <asp:TextBox ID="txtCollegeFax" runat="server" TabIndex="7" ToolTip="Please Enter Fax"></asp:TextBox>
                        </li>
                        <li class="width48perc fleft">
                            <label>
                                Display</label>
                            <asp:CheckBox ID="chkStatus" runat="server" TabIndex="8" ToolTip="Please check status"></asp:CheckBox>
                        </li>
                        <li>
                            <label>
                            </label>
                            <asp:Button ID="btnContactInsert" runat="server" TabIndex="9" Text="Insert" ValidationGroup="register" ToolTip="Click to finish contact details" OnClick="btnContactInsert_Click"></asp:Button>
                        </li>
                    </ul>
                </fieldset>
            </div>
            <a href="#" id="sndCollege"></a>
        </ContentTemplate>
    </asp:UpdatePanel>
    <style type="text/css">.accordion{width: 100%;box-shadow: none;margin-left: 0px;margin-bottom: 0px;min-width:650px;background-color:#fff;}
        .accord{background: #f1f1f1 url(../../Image/CommonImages/arrow-square.gif) no-repeat right -43px;text-transform: capitalize;}
        .accord.active{background-position: right 13px !important;}
        .accordion div{margin-left:3px;margin-bottom:5px;}
        .grdView{margin-left: 0px !important;}
    </style>
    <script src="../JS/commonscripts.js" type="text/javascript"></script>
    <script type="text/javascript" defer="defer">
        $(document).ready(function () {

            $(".accordion h2:first").addClass("active");
            $(".accordion div:not(:first)").hide();

            $(".accordion h2").click(function () {
                $(".accordion div").hide();
                $(this).next("div").slideToggle("fast")
		.siblings("div:visible").slideUp("fast");
                $(this).toggleClass("active");
                $(this).siblings("h2").removeClass("active");
            });


        });

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                $(".accordion h2:first").addClass("active");
                $(".accordion div:not(:first)").hide();

                $(".accordion h2").click(function () {
                    $(".accordion div").hide();
                    $(this).next("div").slideToggle("fast")
		.siblings("div:visible").slideUp("fast");
                    $(this).toggleClass("active");
                    $(this).siblings("h2").removeClass("active");
                });

            }
        }
        function OpenPopForContact() {

            OpenPoup("divCollegeContactInsert", 750, 'sndCollege');
        }

        function Close() {
            $("#fade").hide();
            $("#divCollegeContactInsert").hide();
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

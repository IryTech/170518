<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="CollegeTopHirer.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.CollegeTopHirer" %>

<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdncourseid" Value="" runat="server" />
            <asp:HiddenField ID="hndCollegeTopHirer" Value="" runat="server" />
            <asp:HiddenField ID="hdncollegename" runat="server" />
         
                <ul class="addPage_utility">
                    <li class="fright" style="width: 215px !important;">
                        <div class="navbar-inner">
                            <a href="#" id='sndAddCollegePlacement' class="insertIco" onclick="OpenPoup('divUniversityCategoryInsert','650px','sndAddCollegePlacement');return false;">Insert College Placement </a>
                            <div class="clear">
                            </div>
                        </div>
                    </li>
                </ul>
                <fieldset>
                    <legend>Search Placement Master</legend>
                    <ul>
                        <li>
                            <label>
                                College:
                            </label>
                            <asp:TextBox ID="txtCollegeTopHirer" placeholder="Enter College Name" CssClass="autocomplete" Width="63%" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                Course</label>
                            <asp:DropDownList CssClass="smalltextbox" ID="ddlCourse" TabIndex="1" AppendDataBoundItems="true" AutoPostBack="false" ValidationGroup="President" runat="server">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label>
                            </label>
                            <asp:Button ID="btnSearch" Text="Search" runat="server" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnReset" Text="Reset" runat="server" OnClick="btnReset_Click" />
                        </li>
                    </ul>
                    <asp:Label ID="lblInfo" runat="server" Visible="false"></asp:Label>
                    <asp:Label runat="server" Text="" ID="lblRecordsInserted"></asp:Label>
                    <asp:Repeater ID="rptCollegeTopHirer" runat="server" OnItemCommand="rptCollegeList_ItemCommand">
                        <HeaderTemplate>
                            <table class="grdView">
                                <tr>
                                    <th>
                                        S.No
                                    </th>
                                    <th>
                                        College
                                    </th>
                                    <th>
                                        Company
                                    </th>
                                    <th>
                                        Placement Year
                                    </th>
                                    <th>
                                        Students Hired
                                    </th>
                                    <th>
                                        Salary Offered
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
                                    <%# Eval("Srno") %>
                                </td>
                                <td>
                                    <%# Eval("CollegeBranchName")%>
                                </td>
                                <td>
                                    <%# Eval("CollegeBranchCoursePlacementCompanyName")%>
                                </td>
                                <td>
                                    <%# Eval("CollegeBranchCoursePlacementYear")%>
                                </td>
                                <td>
                                    <%# Eval("CollegeBranchCoursePlacementNoOfStudentHired")%>
                                </td>
                                <td>
                                    <%# Eval("CollegeBranchCoursePlacementAvgSalaryOffered")%>
                                </td>
                                <td>
                                    <%# Eval("CollegeBranchCoursePlacementStatus")%>
                                </td>
                                <td>
                                    <asp:HiddenField ID="hndCollegeBranchCourseID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "CollegeBranchCourseId") %>' />
                                    <asp:LinkButton ID="BtnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("CollegeBranchCoursePlacementId")%>' CommandName="Edit" CausesValidation="false" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <AJ:CustomPaging ID="UcCustomPaging" runat="server" />
                    <asp:HiddenField runat="server" ID="hdnFileName"></asp:HiddenField>
                    <asp:Label ID="lblMsg" CssClass="success" runat="server" Visible="false"></asp:Label>
                </fieldset>
           
            <asp:HiddenField ID="hdnAssociation" runat="server" />
            <asp:HiddenField ID="hdnBranchCourseid" runat="server" />
            <div id="divUniversityCategoryInsert" class="popup_block width43perc">
                <fieldset id="basicInfo">
                    <legend>
                        <asp:Label ID="lblCollegePlacement" Font-Size="13px" ForeColor="#268099" runat="server"></asp:Label></legend>
                    <ul class="pouplist clear">
                        <li style="width: 99% !important;">
                            <label>
                                Course</label>
                            <asp:DropDownList ID="ddlInsertCourse" TabIndex="1" AppendDataBoundItems="true" AutoPostBack="false" ValidationGroup="President" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlInsertCourse" Display="Dynamic" runat="server" ErrorMessage="Please Select Course" ForeColor="Red" Font-Names="verdana" Font-Size="X-Small" InitialValue="0" ValidationGroup="submitPlacement"></asp:RequiredFieldValidator>
                        </li>
                        <li id="filterList" runat="server" style="width: 99% !important;">
                            <label>
                                Filter College :</label>
                            <asp:RadioButtonList ID="rbtnFilterCollege" runat="server" CssClass="RadioButtonList" RepeatDirection="Horizontal" Font-Bold="true">
                                <asp:ListItem Value="0" Selected="True" Text="Normal"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Online Participated"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Sponser"></asp:ListItem>
                            </asp:RadioButtonList>
                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                College:
                            </label>
                            <asp:TextBox ID="txtSelectCollegeFiltered" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtSelectCollegeFiltered" Display="Dynamic" runat="server" ErrorMessage="Please Enter College Name" ForeColor="Red" Font-Names="verdana" Font-Size="X-Small" ValidationGroup="submitPlacement"></asp:RequiredFieldValidator>
                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                Company Name:
                            </label>
                            <asp:TextBox ID="txtPlacementCompanyName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtPlacementCompanyName" Display="Dynamic" runat="server" ErrorMessage="Please Enter Company Name" ForeColor="Red" Font-Names="verdana" Font-Size="X-Small" ValidationGroup="submitPlacement"></asp:RequiredFieldValidator>
                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                Placement Year:
                            </label>
                            <asp:TextBox ID="txtCompanyPlacementYear" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtCompanyPlacementYear" Display="Dynamic" runat="server" ErrorMessage="Please Enter Year" ForeColor="Red" Font-Names="verdana" Font-Size="X-Small" ValidationGroup="submitPlacement"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="validateYear" runat="server" ErrorMessage="Please enter valid year" Type="Integer" MinimumValue="1900" Display="Dynamic" ControlToValidate="txtCompanyPlacementYear" ForeColor="Red" Font-Names="verdana" Font-Size="X-Small" ValidationGroup="submitPlacement"></asp:RangeValidator>
                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                Number of Student Hired:
                            </label>
                            <asp:TextBox ID="txtnoOfStudentHired" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter number of hired students" Display="Dynamic" ControlToValidate="txtnoOfStudentHired" ForeColor="Red" Font-Names="verdana" Font-Size="X-Small" ValidationGroup="submitPlacement"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Please enter between 0 and 2500" MinimumValue="0" MaximumValue="2500" Type="Integer" Display="Dynamic" ControlToValidate="txtnoOfStudentHired" ForeColor="Red" Font-Names="verdana" Font-Size="X-Small" ValidationGroup="submitPlacement"></asp:RangeValidator>
                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                Salary Offered:
                            </label>
                            <asp:TextBox ID="txtCompanySalryOffer" runat="server"></asp:TextBox>
                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                Placement Status:
                            </label>
                            <asp:CheckBox ID="ChkStatus" runat="server" />
                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                &nbsp;</label>
                            <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_Click" ValidationGroup="submitPlacement" />
                            <asp:Button ID="btnCancel" Text="Cancel" runat="server" OnClick="btnCancel_Click" />
                        </li>
                    </ul>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
     <div id="divImage" class="loading">
        <img src="/image.axd?Common=Loading.gif" />
    </div>
    <script src="../../Scripts/Autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript">



        bindbyCourse();
        bindAutoCompletebyCourseIDParticipated();

        function pageLoad(sender, args) {


            $("#<%=txtCollegeTopHirer.ClientID %>").flushCache();

            if (args.get_isPartialLoad()) {
                urlcoursefilter = "../../WebServices/CommonWebServices.asmx/GetCollegeDetailsbyCourseID";
                var e = document.getElementById("<%=ddlCourse.ClientID %>");

                var course = e.options[e.selectedIndex].value;

                $('#<%=txtCollegeTopHirer.ClientID %>').flushCache();
                BindDropDownCommonForAdminAutoCompletebyCourseID($("#<%=txtCollegeTopHirer.ClientID %>"), course, urlcoursefilter);
                BindDropDownCommonForAdminAutoCompletebyCourseID($("#<%=txtSelectCollegeFiltered.ClientID %>"), 0, urlcoursefilter);
            }
            $(document).ready(function () {

                $("#<%=ddlCourse.ClientID %>").change(function () {
                    var textb = document.getElementById("<%=txtCollegeTopHirer.ClientID %>");
                    textb.value = "";

                    bindbyCourse();

                });


                $("#<%=ddlInsertCourse.ClientID %>").change(function () {
                    var textb = document.getElementById("<%=txtSelectCollegeFiltered.ClientID %>");
                    textb.value = "";

                    bindAutoCompletebyCourseIDParticipated();

                });

                $('#<%=rbtnFilterCollege.ClientID %> input:radio').change(function () {
                    var textb = document.getElementById("<%=txtSelectCollegeFiltered.ClientID %>");
                    textb.value = "";
                    bindAutoCompletebyCourseIDParticipated();
                });
                $('#<%=lblMsg.ClientID %>').fadeOut(12000, function () {
                    $(this).html(""); //reset the label after fadeout
                });

            });
        }


        function bindbyCourse() {
            urlcoursefilter = "../../WebServices/CommonWebServices.asmx/GetCollegeDetailsbyCourseID";
            var e = document.getElementById("<%=ddlCourse.ClientID %>");

            var course = e.options[e.selectedIndex].value;

            $('#<%=txtCollegeTopHirer.ClientID %>').flushCache();
            BindDropDownCommonForAdminAutoCompletebyCourseID($("#<%=txtCollegeTopHirer.ClientID %>"), course, urlcoursefilter);
        }

        function bindAutoCompletebyCourseIDParticipated() {

            urlcoursefilter = "../../WebServices/CommonWebServices.asmx/GetCollegeDetailsbyCourseIDParticipated";
            var e = document.getElementById("<%=ddlInsertCourse.ClientID %>");

            var course = e.options[e.selectedIndex].value;

            var radioButtons = document.getElementById("<%=rbtnFilterCollege.ClientID %>");
            var radio = radioButtons.getElementsByTagName("input");
            var filter;
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {

                    filter = radio[i].value;
                    break;
                }
            }
            $('#<%=txtSelectCollegeFiltered.ClientID %>').flushCache();
            BindDropDownCommonForAdminAutoCompletebyCourseIDParticipated($("#<%=txtSelectCollegeFiltered.ClientID %>"), filter, course, urlcoursefilter);
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

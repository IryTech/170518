<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="CollegeOnlineParticipationStatus.aspx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.CollegeOnlineParticipationStatus" %>

<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="dfdpanel" runat="server">
<ContentTemplate>
    <asp:HiddenField ID="hdnQuery" runat="server" />
    <asp:HiddenField ID="hdnDbQuery" runat="server" />
    <div class="grdOuterDiv">
        <%--<h4>
            Online College Participation Status</h4>--%>
        <asp:Label ID="lblUpdate" runat="server" Visible="false"></asp:Label><asp:Label ID="lblSuccess"
            runat="server" Visible="false"></asp:Label>
        
        <div>
            <asp:Label ID="lblInfo" runat="server" Visible="false"></asp:Label><fieldset>
                <legend>Search  College Participation </legend>
                <ul>
                 <li>
                        <label>
                            College:</label>
                        <asp:TextBox runat="server" CssClass="autocomplete" Width="66.5%" placeholder="Enter College Name" ID="txtCollegeName"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Course</label>
                        <asp:DropDownList ID="ddlCourseList" runat="server" ToolTip="Please Select Course"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlCourseList_SelectedIndexChanged">
                        </asp:DropDownList>
                    <asp:DropDownList ID="ddlState" runat="server" ToolTip="Please Select State"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                            </asp:DropDownList>
                    <asp:DropDownList ID="ddlCity" runat="server" ToolTip="Please Select City"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                            </asp:DropDownList>
                    </li>
                   
                    <li>
                        <label>
                        </label>
                        <asp:Button runat="server" Text="Search" OnClick="BtnSearchClick" ValidationGroup="search">
                        </asp:Button>
                    </li>
                </ul>
            </fieldset>
        </div>
        <asp:Repeater ID="rptCollegeList" runat="server" OnItemCommand="RptCollegeListItemCommand">
            <HeaderTemplate>
                <table class="grdView">
                    <tr>
                        <th>
                            CollegeName
                        </th>
                        <th>
                            OnlineParticipation
                        </th>
                        <th>
                            VirtualParticipation
                        </th>
                        <th>
                            Rating
                        </th>
                        <th>
                        Action
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Eval("CollegeBranchName")%>
                    </td>
                    <td>
                        <%# Eval("CollegeOnlineParticipateStatus")%>
                    </td>
                    <td>
                        <%# Eval("CollegeBranchCourseVirtualOnlineStatus")%>
                    </td>
                    <td>
                        <%# String.Format("{0}", !string.IsNullOrEmpty(Convert.ToString(Eval("CollegeOverallRating")))? Math.Round(Convert.ToDecimal(Eval("CollegeOverallRating")),2):0)%>
                    </td>
                    <td>
                        <a id="lnkUpdate" href="#" onclick="OpenAssociationPoup('<%# Eval("CollegeBranchCourseId")%>');return false;">
                           Edit Ranking </a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table></FooterTemplate>
        </asp:Repeater>
        <asp:Panel runat="server" ID="pnlPager" CssClass="pagination">
        </asp:Panel>
    </div>
    <div id="divAssociation" class="popup_block width62perc">
        <fieldset>
            <legend>College Counselling</legend>
            <ul>
                <li >
                    <label>
                        Admission Till</label>
                    <asp:TextBox ID="txtAdmissionDate" runat="server" />
                </li>
                 <li class="width48perc fleft">
                    <label>
                        SPO</label>
                    <input type="checkbox" id="chkOnlineparticipation" onclick="showFactors()" />
                </li>
                <li class="virtual width48perc fleft">
                    <label>
                        Virtual Participation</label>
                    <input type="checkbox" id="chkOnlineParticipationVirtual" checked="checked" />
                </li>
                <li>
                    <ul class="facotrInsert">
                    </ul>
                </li>
                <li><label></label>
                    <input type="button" id="btnUpdate" value="Update" onclick="UpdateFactor()" /></li>
            </ul>
            <span style="display: none" id="progress">
                <img src="/image.axd?Common=LoadingImage.gif" alt="Loading" />
            </span>
        </fieldset>
    </div>
    <asp:HiddenField ID="hdnAssociation" runat="server" />
    <input type="hidden" id="hdnTotalValue" value="0" />
    <input type="hidden" id="hdnFactorIds" />
    <input type="hidden" id="hdnFactorsValue" />
    <asp:HiddenField ID="hdnBranchCourseid" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel><div id="fade">
    </div>
    <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
    <div id="divImage" class="loading">
        <img src="/image.axd?Common=Loading.gif" />
    </div>
   
    <script src="../../Scripts/Autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(".grdView tr:even").css("background-color", "#f4f4f8");
        $(". grdView tr:odd").css("background-color", "#ffffff");
        $(document).ready(function () {
            var url = "../../WebServices/CommonWebServices.asmx/GetCollegeDetails";
            BindDropDownCommonForAdminAutoComplete($("#<%=txtCollegeName.ClientID %>"), url);
        });
        
        function OpenAssociationPoup(branchCourseId) {
           $("#<%=hdnBranchCourseid.ClientID %>").val(branchCourseId);
            GetStatus(branchCourseId);

        }
        function GetStatus(branchCourseId) {
            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/GetCollegeBranchCourseValuesForOnlineStatus",
                data: '{collegeBranchCourseId:"' + branchCourseId + '"}',
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    CheckStatus(msg);
                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        }
        function CheckStatus(data) {

            if (data.d.length > 0) {
                var ds = new Date();

                var dateheck = new Date(parseInt(data.d[0].AdmissionDate.substr(6)));
                if (isValidDate(dateheck.format("dd/MM/yyyy"))) {
                    ds = dateheck.format("dd/MM/yyyy");
                }


                                                      
                data.d[0].CollegeOnlineParticipateStatus == true ? $("#chkOnlineparticipation").attr('checked', true) : $("#chkOnlineparticipation").attr('checked', false);
                isValidDate(dateheck.format("dd/MM/yyyy")) == true ? $("#<%=txtAdmissionDate.ClientID %>").val(dateheck.format("dd/MM/yyyy")) : $("#<%=txtAdmissionDate.ClientID %>").val('');
              
                    showFactors();
               
            }
            else {
                $("#chkOnlineparticipation").attr('checked', false);

            }
            if ($("#chkOnlineparticipation").attr('checked') == false) {
                $(".facotrInsert").html('');
            }
            //fillFactorValues();
            OpenPoup('divAssociation', '550', 'lnkUpdate');
        }
        function isValidDate(dateString) {
            // First check for the pattern
            if (!/^\d{2}\/\d{2}\/\d{4}$/.test(dateString))
                return false;

            // Parse the date parts to integers
            var parts = dateString.split("/");
            var day = parseInt(parts[1], 10);
            var month = parseInt(parts[0], 10);
            var year = parseInt(parts[2], 10);

            // Check the ranges of month and year
            if (year < 1000 || year > 3000 || month == 0 || month > 12)
                return false;

            var monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

            // Adjust for leap years
            if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
                monthLength[1] = 29;

            // Check the range of the day
            return day > 0 && day <= monthLength[month - 1];
        };
        function showFactors() {
            if ($("#chkOnlineparticipation").attr('checked') == true) {
                $.ajax({
                    type: "POST",
                    url: "/WebServices/CommonWebServices.asmx/GetFactor",
                    data: "{}",
                    async: true,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        BindFactor(msg);
                    },
                    error: function (xml, textStatus, errorThrown) {
                        //alert(xml.status + "||" + xml.responseText);
                    }
                });
            }
            else {
                $(".facotrInsert").html('');
            }
        }
        function BindFactor(result) {
            var data = "";
            var i;
            if (result.d.length > 0) {
                $(".facotrInsert").html('');
                for (i = 0; i < result.d.length; i++) {

                    $(".facotrInsert").append(" <li class='width48perc fleft'><label id='lbl" + i + "'>" + result.d[i].FactorName + "</label><input type='text' style='min-width:222px !important;'  id='txtFacotr" + i + "'  /><input type='hidden' id='hdn" + i + "' value='" + result.d[i].FactorId + "' /></li>");


                }
                $("#hdnTotalValue").val(i);
                fillFactorValues();
            }

        }
        //to update values in CollegeBranchCourseMaster...

        function UpdateFactor() {
            $("#progress").show();
            var commaSeperatedDataId = ""; var commaSeperatedDataValue = "";
            var check = $("#chkOnlineparticipation").attr('checked');
            if (check == true) {
                var countLength = $("#hdnTotalValue").val();

                if (countLength > 0) {
                    for (var j = 0; j < countLength; j++) {
                        commaSeperatedDataValue += $("#txtFacotr" + j).val() + ",";
                        commaSeperatedDataId += $("#hdn" + j).val() + ",";

                    }
                    $("#hdnFactorIds").val(commaSeperatedDataId);
                    $("#hdnFactorsValue").val(commaSeperatedDataValue);
                }

                if (commaSeperatedDataValue.length > 6) {
                    var dataQuery = '{"onlineParticipation":"' + $("#chkOnlineparticipation").attr('checked') + '","virtualParticipation":"' + $("#chkOnlineParticipationVirtual").attr('checked') + '","collegeBranchCourseId":"' + $("#<%=hdnBranchCourseid.ClientID %>").val() + '","factorIds":"' + $("#hdnFactorIds").val() + '","factorValues":"' + $("#hdnFactorsValue").val() + '","AdmissionDate":"' + $("#<%=txtAdmissionDate.ClientID %>").val() + '"}';

                    $.ajax({
                        type: "POST",
                        url: "/WebServices/CommonWebServices.asmx/InsertFactorValuesAndUpdateRating",
                        data: dataQuery,
                        async: true,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            $("#progress").hide();
                            ClearControl();
                            __doPostBack('reload', 'reload');
                            //location.href = "../College/CollegeOnlineParticipationStatus.aspx";
                        },
                        error: function (xml, textStatus, errorThrown) {
                            // alert(xml.status + "||" + xml.responseText);
                        }
                    });
                } else {
                    alert("Please Enter Somevalues in factor");
                    return false;
                }
            }
            else {
                var dataQuery = '{"onlineParticipation":"' + $("#chkOnlineparticipation").attr('checked') + '","virtualParticipation":"' + $("#chkOnlineParticipationVirtual").attr('checked') + '","collegeBranchCourseId":"' + $("#<%=hdnBranchCourseid.ClientID %>").val() + '","AdmissionDate":"' + $("#<%=txtAdmissionDate.ClientID %>").val() + '"}';

                $.ajax({
                    type: "POST",
                    url: "/WebServices/CommonWebServices.asmx/UpdateCollegeOnlineParticipation",
                    data: dataQuery,
                    async: true,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d[0].Result > 0) {
                            ClearControl();
                            $("#progress").hide();
                            location.href = "../College/CollegeOnlineParticipationStatus.aspx";
                        }
                    },
                    error: function (xml, textStatus, errorThrown) {
                        //alert(xml.status + "||" + xml.responseText);
                    }
                });
            }
        }
        function ClearControl() {
            var countLength = $("#hdnTotalValue").val();

            if (countLength > 0) {
                for (var j = 0; j < countLength; j++) {
                    $("#txtFacotr" + j).val('');
                }
            } $("#<%=txtAdmissionDate.ClientID %>").val('');
        }
        function fillFactorValues() {
            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/FillFactorValues",
                data: '{collegeBranchCourseId:"' + $("#<%=hdnBranchCourseid.ClientID %>").val() + '"}',
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    BindFactorValues(response);
                },
                error: function (xml, textStatus, errorThrown) {
                    alert(xml.status + "||" + xml.responseText);
                }
            });
        }

        var factor = [];

        function BindFactorValues(response) {
            factor = [];
            for (var i = 0; i < response.d.length; i++) {

                factor.push(
               response.d[i].FactorId);

                factor.push(
               'va-' + response.d[i].FactorValues);


            }

            FillFactor(factor);
        }

        AdmissionjankariIry = {
            $: function (id) {
                return document.getElementById(id);
            }
        };
        function FillFactor(factor) {

            for (var i = 0; i < factor.length; i++) {

                if ($("#hdn" + i).val() != 'undefined') {
                    var value = $("#hdn" + i).val();
                    var split = factor[parseInt(jQuery.inArray(parseInt(value), factor)) + 1].split("va-");


                    $("#txtFacotr" + i).val(split[1]);
                }
            }
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


        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                BindDropDownCommonForAdminAutoComplete($("#<%=txtCollegeName.ClientID %>"), url);
                $(".grdView tr:even").css("background-color", "#f4f4f8");
                $(". grdView tr:odd").css("background-color", "#ffffff");
            }
        }

    </script>
</asp:Content>

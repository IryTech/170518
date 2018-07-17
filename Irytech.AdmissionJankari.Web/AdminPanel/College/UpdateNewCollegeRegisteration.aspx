<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="UpdateNewCollegeRegisteration.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.UpdateNewCollegeRegisteration" %>

<%@ Register TagPrefix="AJ" TagName="CustomPaging" Src="~/UserControl/CustomPaging.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
            <fieldset>
                <legend>Search </legend>
                <ul class="options-bar">
                    <li>
                        <label>
                            College:</label>
                        <asp:TextBox ID="txtCollege" runat="server" CssClass="autocomplete" TabIndex="1"
                            Width="63%" ToolTip="Please enter college">
                        </asp:TextBox>
                        <asp:Button runat="server" ID="btnSearch" CssClass="searchbtn" TabIndex="2" ToolTip="Click to search"
                            OnClick="BtnSearchClick" Text="Search" ValidationGroup="collegeUpdate" />
                    </li>
                </ul>
                <div id="divImage" class="loading">
                    <img src="/image.axd?Common=Loading.gif" alt="Loading_Image" title="Loading Image" />
                </div>
            </fieldset>
            <asp:Repeater ID="rptCollegeList" runat="server" OnItemCommand="RptCollegeListItemCommand">
                <HeaderTemplate>
                    <table class="grdView">
                        <tr>
                            <th>
                                S.No
                            </th>
                            <th>
                                User Name
                            </th>
                            <th>
                                College Name
                            </th>
                            <th>
                                Mobile Number
                            </th>
                            <th>
                                login status
                            </th>
                            <th>
                                View Applicant
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                   
                        <td>
                            <%# Eval("SrNo") %>
                        </td>
                        <td>
                            <%# Eval("AjUserFullName") %>
                        </td>
                        <td>
                            <%# Eval("AjCollegeBranchName")%>
                        </td>
                        <td>
                            <%# Eval("AjUserMobile")%>
                        </td>
                        <td>
                            <a id="lnkUpdateLoginStatus" href="#" onclick="OpenVerifyPopUp(<%# Eval("AjUserId")%>); return false;" > <%# GetModerateCollege(Eval("AjUserId")) %></a>
                        </td>
                        <td>
                            <a id="lnkViewDetails" onclick='GetUserDetails("<%# Eval("AjUserId")%>","<%# Convert.ToString(Eval("AjCollegeBranchName"))%>")'
                                href="#">View Details</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
            <AJ:CustomPaging ID="ucCollegeList" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divCourseCategoryInsert" class="popup_block width43perc">
        <fieldset id="basicInfo">
            <legend style="font-size: 15px !important; color: #006699;"><span id="spnCollegeName">
            </span></legend>
            <div>
                <table id="userDetails" class="grdView">
                </table>
            </div>
        </fieldset>
    </div>
    <asp:HiddenField ID="hndCollegeBranchId" runat="server" />
    <div id="divLoginStatus" class="popup_block width43perc">
     <fieldset id="Fieldset1">
            <legend>Verify College</legend>
            <ul class="pouplist">
                <li style="width: 99% !important;">
                   <asp:RadioButtonList ID="rbtLoginStatus" runat="server" RepeatDirection="Horizontal" >
                   <asp:ListItem Text="Allow" Value="true" ></asp:ListItem>
                   <asp:ListItem Text="Disallow" Value="false" ></asp:ListItem>
                   </asp:RadioButtonList>
                
                   <asp:RequiredFieldValidator ID="rfvVerification" runat="server" ControlToValidate="rbtLoginStatus"
                   ValidationGroup="course" ErrorMessage="Select your option " SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                </li>
                <li style="width: 99% !important;">
                    <label>
                        &nbsp;</label>
                    <asp:Button ID="btnUniversityCategoryName" runat="server" Text="Save" OnClick="btnUniversityCategoryName_Click" ValidationGroup="course"
                        CausesValidation="true" TabIndex="3" ToolTip="Please Verify"  />
                   
                </li>
            </ul>
        </fieldset>
    </div>
    <a id="lnkViewDetails"></a>
    <script src="../../Scripts/Autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript" defer="defer">
        var collegeUrl = "../../WebServices/CommonWebServices.asmx/GetRegisteredCollegeList";
        BindDropDownCommonForAdminAutoComplete($("#<%=txtCollege.ClientID %>"), collegeUrl);

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                BindDropDownCommonForAdminAutoComplete($("#<%=txtCollege.ClientID %>"), collegeUrl);
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
        function ValidateStatus() {
            if (confirm("Are you sure you want to update the status?"))
                return true;
            else
                return false;
        }

        function GetUserDetails(userId, collegeName) {

            $.ajax({
                type: "POST",
                url: "../../WebServices/CommonWebServices.asmx/GetCollegeContactPerson",
                data: '{"userId":"' + userId + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: true,
                async: true,
                success: function (msg) {

                    BindUserDetails(msg, collegeName);

                },
                error: function (xml, textStatus, errorThrown) {

                    alert(xml.status + "||" + xml.responseText);
                }
            });
        }

        function BindUserDetails(data, collegeName) {

            $('#userDetails').html('');

            $("#spnCollegeName").text("Contact Person Details of  " + collegeName);
            $.each($.parseJSON(data.d), function (i, client) {
                           
                data = data + "<tr><td style='width:80px;'>Name:  </td> <td class='tabStrong'>" + client.AjUserFullName + "</td></tr><tr><td> Email Id :</td><td class='tabStrong'>" + client.AjUserEmail + "</td></tr><tr><td>Mobile No : </td><td class='tabStrong'>" + client.AjUserMobile + "</td></tr><tr><td>Designation</td><td class='tabStrong'>" + client.AjCollegePersonDegisnation + "</td> </tr>"
            });


            $('#userDetails').append(data);
            OpenPoup('divCourseCategoryInsert', '800px', 'lnkViewDetails')
        }

        function OpenVerifyPopUp(collegeId) {

            $("#<%=hndCollegeBranchId.ClientID %>").val(collegeId);
            OpenPoup('divLoginStatus', '800px', 'lnkUpdateLoginStatus')
        }

    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="PresidentSpeechDetails.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.PresidentSpeech.PresidentSpeechDetails" %>
    <%@ Register Src="~/UserControl/FckEditorCostomize.ascx" TagName="ExamDesc" TagPrefix="AJ" %>
<%@ Register Src="~/UserControl/AjaxFileUpload.ascx" TagName="FileUploader" TagPrefix="Aj" %>
<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="Aj" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:HiddenField ID="hndCollegeSpeechID" runat="server" />
    <asp:HiddenField runat="server" ID="hdnFileName"></asp:HiddenField>
    <asp:UpdatePanel ID="updSpeechDetails" runat="server">
        <ContentTemplate>
            <div id="fade">
            </div>
             <asp:Label ID="lblSeccessMsg" CssClass="success" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblErorrMsg" CssClass="error" runat="server" Visible="false"></asp:Label>
            <div id="divImage" class="loading">
                <img src="/image.axd?Common=Loading.gif" />
            </div>

            <ul class="addPage_utility">
        <li class="fright" style="width: 213px !important;">
            <div class="navbar-inner">
                <a href="#" id='sndAddCollegePresedentSpeech' class="insertIco" onclick="OpenPoup('divUniversityCategoryInsert','650px','sndAddCollegePresedentSpeech');return false;">
                                    Add College Placement </a>
                <div class="clear">
                </div>
            </div>
        </li>
        </ul>

            <fieldset >
                <legend>Search College Placement</legend>
                    
                      
                    <ul>
                    <li>
                            <label>
                                College:
                            </label>
                            <asp:TextBox ID="txtCollege" CssClass="autocomplete" placeholder="Enter College Name" Width="63%" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                Select Course:
                            </label>
                            <asp:DropDownList ID="ddlCourse" TabIndex="1" runat="server">
                            </asp:DropDownList>
                        </li>
                        
                        <li>
                            <label>
                            </label>
                            <asp:Button ID="btnSearch" Text="Search" runat="server" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnReset" Text="Reset" runat="server" OnClick="btnReset_Click" />
                        </li>
                         
                    </ul>
                     
                        <asp:HiddenField ID="hdncollegename" runat="server" Value="" />
                    <asp:HiddenField ID="hdncourseid" runat="server" Value="" />
               

                            <asp:Label ID="lblErrorMessage" runat="server" CssClass="err_msg" Visible="false"></asp:Label>
                            <asp:Repeater ID="rptPresidentDetails" runat="server" OnItemCommand="rptPresidentDetails_ItemCommand">
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
                                                Speaker
                                            </th>
                                            <th>
                                                Designation
                                            </th>
                                            <th>
                                                About
                                            </th>
                                            <th>
                                                Image
                                            </th>
                                            <th>
                                                Status
                                            </th>
                                            <th>
                                                Action
                                            </th>
                                            <th>
                                            </th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("SrNo")%>
                                        </td>
                                        <td>
                                            <%# Eval("CollegeName")%>
                                        </td>
                                        <td>
                                            <%# Eval("CollegeSpeechPersonName")%>
                                        </td>
                                        <td>
                                            <%# Eval("CollegeSpeechPersonDesignation")%>
                                        </td>
                                        <td>
                                            <%# Eval("AboutKeyPerson")%>
                                        </td>
                                        <td>
                                            <img src='<%# String.Format("{0}{1}","/image.axd?College=",string.IsNullOrEmpty(Eval("CollegeSpeechPersonImage").ToString()) ?"NoImage.jpg":Eval("CollegeSpeechPersonImage")) %>'
                                                width="55px" height="60" alt='<%# Eval("CollegeName")%>' title='<%# Eval("CollegeName")%>' />
                                        </td>
                                        <td>
                                            <%# Eval("SpeechStatus")%>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="BtnEdit" runat="server" CssClass="roundedFormat Link_Btn" Text="Edit"
                                                CommandArgument='<%# Eval("CollegeSpeechId")%>' CommandName="Edit" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                            <Aj:CustomPaging ID="ucCustomPaging" runat="server" />
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    <div id="Progress" class="pop">
                                        <img src="/image.axd?Common=LoadingImage.gif" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                             </fieldset>
                        



                 
            <div id="divUniversityCategoryInsert" class="popup_block width60perc">
                <fieldset id="basicInfo">
                    <legend>
                        <asp:Label ID="lblCollegePlacement"  runat="server"></asp:Label></legend>
                    <ul class="pouplist">
                        <li style="width: 99% !important;">
                            <label>
                                College Type:</label>
                            <asp:RadioButtonList ID="rbtnFilterCollege" runat="server" TabIndex="1" CssClass="RadioButtonList" RepeatDirection="Horizontal"
                                Font-Bold="true">
                                <asp:ListItem Value="0" Selected="True" Text="Normal"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Online Participated"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Sponser"></asp:ListItem>
                            </asp:RadioButtonList>
                        </li>
                        <li id="filterList" runat="server" style="width:48% !important; float:left;">
                            <label>
                                Course</label>
                            <asp:DropDownList ID="ddlCourseList" TabIndex="2" runat="server" Style="min-width: 217px !important;" ToolTip="Please Select Course">
                            </asp:DropDownList>
                        </li>
                        <li style="width:48% !important; float:left;">
                            <label>
                                College:
                            </label>
                            <asp:TextBox ID="txtSelectCollegeFiltered" runat="server" Style="min-width: 207px !important;" TabIndex="3" ValidationGroup="President"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvCollegeName" ErrorMessage="Select college"
                                ControlToValidate="txtSelectCollegeFiltered" Display="Dynamic" SetFocusOnError="True"
                                ValidationGroup="speech">Enter College name</asp:RequiredFieldValidator>
                        </li>
                        <li style="width:48% !important; float:left;">
                            <label>
                                Person Name:
                            </label>
                            <asp:TextBox ID="txtSpeechPersonName" Style="min-width: 213px !important;" TabIndex="4" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvPersonName" ErrorMessage="Enter Person name"
                                ControlToValidate="txtSpeechPersonName" Display="Dynamic" SetFocusOnError="True"
                                 ValidationGroup="speech">
                    
                            </asp:RequiredFieldValidator>
                        </li>
                        <li style="width:48% !important; float:left;">
                            <label>
                                Person Designation:
                            </label>
                            <asp:TextBox ID="txtPersonDesignation" Style="min-width: 207px !important;" TabIndex="5" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvCollegeTitle" ErrorMessage="Enter desgination"
                                ControlToValidate="txtPersonDesignation" Display="Dynamic" SetFocusOnError="True"
                                ValidationGroup="speech">
                    
                            </asp:RequiredFieldValidator>
                        </li>
                        <li style="width:48% !important; float:left;">
                            <label>
                                About Person:
                            </label>
                            <asp:TextBox ID="txtAbouKeyPerson" Style="min-width: 213px !important;" TabIndex="6" runat="server"></asp:TextBox>
                        </li>
                         <li style="width:48% !important; float:left;">
                            <label id="personImage" runat="server">
                                Person Image:
                            </label>
                            <asp:FileUpload ID="uploadFile" style="width: 208px !important;" TabIndex="7" runat="server" />
                            <%--  <Aj:FileUploader ID="imgUploader" runat="server" TabIndex="6" uploadToDirectory="../../Image/Colleges/" />--%>
                            <img id="personImagetag" runat="server" class="personImageright" />
                        </li>
                        <li style="width:48% !important; float:left;">
                            <label>
                                State</label>
                            <asp:DropDownList ID="ddlState" runat="server" TabIndex="8" Style="min-width: 211px !important;" ToolTip="Please Select State" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li style="width:48% !important; float:left;">
                            <label>
                                City</label>
                            <asp:DropDownList ID="ddlSelectCity" TabIndex="9" runat="server" Style="min-width: 211px !important;" ToolTip="Please select city">
                            </asp:DropDownList>
                        </li>
                                                
                        
                        <li style="width: 99% !important; height:192px !important;">
                            <label>
                                Speech Description:
                            </label><span class="fleft" style="margin:4px 5px;">
                            <Aj:ExamDesc ID="txtDescription" runat="server" TabIndex="10" /></span>
                        </li>
                       
                         <li>
                            <label>
                                Display:
                            </label>
                            <asp:CheckBox ID="chkStatus" runat="server" TabIndex="11" />
                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                &nbsp;</label>
                           <asp:Button ID="btnSubmit" runat="server" TabIndex="12" CausesValidation="true" Text="Insert"
                                ValidationGroup="speech" OnClick="btnSubmit_Click"  />
                            <asp:Button ID="btnClear" TabIndex="13" runat="server"  Text="Cancel" OnClick="btnCancel_Click" />
                        </li>
                    </ul>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit"/>
        </Triggers>
    </asp:UpdatePanel>
    <script src="../../Scripts/Autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript">

        bindbyCourse();
        function pageLoad(sender, args) {

            if (args.get_isPartialLoad()) {
                urlcoursefilter = "../../WebServices/CommonWebServices.asmx/GetCollegeDetailsbyCourseID";
                var e = document.getElementById("<%=ddlCourse.ClientID %>");

                var course = e.options[e.selectedIndex].value;

                $('#<%=txtCollege.ClientID %>').flushCache();
                BindDropDownCommonForAdminAutoCompletebyCourseID($("#<%=txtCollege.ClientID %>"), course, urlcoursefilter);



            }
            $(document).ready(function () {

                $("#<%=ddlCourse.ClientID %>").change(function () {
                    var textb = document.getElementById("<%=txtCollege.ClientID %>");

                    textb.value = "";

                    bindbyCourse();

                });
                $('#<%=lblErrorMessage.ClientID %>').fadeOut(12000, function () {
                    $(this).html(""); //reset the label after fadeout
                });

            });

        }
        function bindbyCourse() {
            urlcoursefilter = "../../WebServices/CommonWebServices.asmx/GetCollegeDetailsbyCourseID";
            var e = document.getElementById("<%=ddlCourse.ClientID %>");

            var course = e.options[e.selectedIndex].value;

            $('#<%=txtCollege.ClientID %>').flushCache();
            BindDropDownCommonForAdminAutoCompletebyCourseID($("#<%=txtCollege.ClientID %>"), course, urlcoursefilter);
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
    <script type="text/javascript">

        bindAutoCompletebyquery();

        function pageLoad(sender, args) {

            if (args.get_isPartialLoad()) {

                var radioButtons = document.getElementById("<%=rbtnFilterCollege.ClientID %>");
                var radio = radioButtons.getElementsByTagName("input");
                var filter;
                for (var i = 0; i < radio.length; i++) {
                    if (radio[i].checked) {

                        filter = radio[i].value;
                        break;
                    }
                }
                var e = document.getElementById("<%=ddlCourseList.ClientID %>");

                course = e.options[e.selectedIndex].value;

                var state = document.getElementById("<%=ddlState.ClientID %>");
                stateid = state.options[state.selectedIndex].value;
                var city = document.getElementById("<%=ddlSelectCity.ClientID %>");
                cityid = city.options[city.selectedIndex].value;
                $('#<%=txtSelectCollegeFiltered.ClientID %>').flushCache();
                BindDropDownCommonForAdminAutoCompletebySponserCourseStateCity($("#<%=txtSelectCollegeFiltered.ClientID %>"), filter, course, stateid, cityid, "../../WebServices/CommonWebServices.asmx/GetCollegeDetailsbySponserCourseStateCity");
            }

            $(document).ready(function () {
                $("#<%=ddlCourseList.ClientID %>").change(function () {

                    var textb = document.getElementById("<%=txtSelectCollegeFiltered.ClientID %>");
                    textb.value = "";
                    bindAutoCompletebyquery();

                });

                $('#<%=rbtnFilterCollege.ClientID %> input:radio').change(function () {
                    var textb = document.getElementById("<%=txtSelectCollegeFiltered.ClientID %>");
                    textb.value = "";
                    bindAutoCompletebyquery();
                });

                $("#<%=ddlState.ClientID %>").change(function () {

                    var textb = document.getElementById("<%=txtSelectCollegeFiltered.ClientID %>");
                    textb.value = "";

                    bindAutoCompletebyquery();

                });

                $('#<%=ddlSelectCity.ClientID %>').change(function () {
                    var textb = document.getElementById("<%=txtSelectCollegeFiltered.ClientID %>");
                    textb.value = "";
                    bindAutoCompletebyquery();
                });
                $('#<%=lblSeccessMsg.ClientID %>').fadeOut(12000, function () {
                    $(this).html(""); //reset the label after fadeout
                });

                $('#<%=lblErorrMsg.ClientID %>').fadeOut(12000, function () {
                    $(this).html(""); //reset the label after fadeout
                });
            });

        }



        

        function bindAutoCompletebyquery() {

            var radioButtons = document.getElementById("<%=rbtnFilterCollege.ClientID %>");
            var radio = radioButtons.getElementsByTagName("input");
            var filter;
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {

                    filter = radio[i].value;
                    break;
                }
            }
            var e = document.getElementById("<%=ddlCourseList.ClientID %>");

            course = e.options[e.selectedIndex].value;

            var state = document.getElementById("<%=ddlState.ClientID %>");
            stateid = state.options[state.selectedIndex].value;
            var city = document.getElementById("<%=ddlSelectCity.ClientID %>");
            cityid = city.options[city.selectedIndex].value;
            $('#<%=txtSelectCollegeFiltered.ClientID %>').flushCache();
            BindDropDownCommonForAdminAutoCompletebySponserCourseStateCity($("#<%=txtSelectCollegeFiltered.ClientID %>"), filter, course, stateid, cityid, "../../WebServices/CommonWebServices.asmx/GetCollegeDetailsbySponserCourseStateCity");

        }
        

    </script>


</asp:Content>

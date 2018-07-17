<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="ExamMaster.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Exam.ExamMaster" %>


<%@ Register Src="~/UserControl/FckEditorCostomize.ascx" TagName="ExamDesc" TagPrefix="AJ" %>
<%@ Register Src="../../UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<%@ Register Src="~/UserControl/AjaxFileUpload.ascx" TagName="FileUpload" TagPrefix="Aj" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UdtpnlExam" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="fade">
            </div>
         
            <div id="divImage" class="loading">
                <img src="/image.axd?Common=Loading.gif" />
            </div>

           



            
                <asp:Label ID="lblSeccessMsg" CssClass="success" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblErorrMsg" CssClass="error" runat="server" Visible="false"></asp:Label>
               
                <asp:HiddenField runat="server" ID="hdnExamMaster"></asp:HiddenField>
                <asp:HiddenField runat="server" ID="hdnFileName"></asp:HiddenField>
                 <ul class="addPage_utility">
                <li class="fright" style="width: 142px !important;">
                    <div class="navbar-inner">
                        <a href="#" id='sndAddExam' class="insertIco" onclick="OpenPoup('divInstituteTypeInsert','650px','sndAddExam');return false;">Insert Exam</a>
                        <div class="clear">
                        </div>
                    </div>
                </li>
                <li class="fright" style="width: 72px !important;">
                    <asp:ImageButton ID="btnUpload" runat="server" ImageUrl="~/AdminPanel/Images/CommonImages/uploadExcel.png" ToolTip="Upload Excel Format" ValidationGroup="GrUpload" OnClick="BtnUploadClick" />
                    <asp:ImageButton ID="btnPreview" runat="server" ImageUrl="~/AdminPanel/Images/CommonImages/downloadExcel2.png" ToolTip="Download Excel Format" OnClick="btnPreview_Click" />
                    <asp:Label runat="server" ID="lblRecordsInserted"></asp:Label>
                </li>
            </ul>
                <fieldset>
                    <legend>Exam Search </legend>
                    <ul class="options-bar">
                        <li class="list75width ">
                            <label class="searchlabel">
                                Exam Name:</label>
                            <asp:TextBox ID="txtExamNameSearch" runat="server" CssClass="autocomplete" Width="63%" onfocus="ClearLabel()" TabIndex="2" ValidationGroup="Exam"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                            </label>
                            <asp:DropDownList ID="ddlCourseNameSearch" onfocus="ClearLabel()" runat="server" TabIndex="1" ValidationGroup="Exam" AutoPostBack="True" OnSelectedIndexChanged="ddlCourseNameSearch_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label>
                            </label>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" TabIndex="12" OnClick="btnSearch_Click" /></li>
                         
                    </ul>
                </fieldset>
               



            
                    <asp:Repeater ID="rptExamMaster" runat="server" OnItemCommand="RptExamMasterItemCommand">
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
                                        Exam Name
                                    </th>
                                    <th>
                                        Exam Full Name
                                    </th>
                                    <th>
                                        Eligiblity Criteria
                                    </th>
                                    <th>
                                        Exam Logo
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
                                    <%# Eval("SrNo")%>
                                </td>
                                <td>
                                    <%# Eval("CourseName")%>
                                </td>
                                <td>
                                    <%# Eval("ExamName")%>
                                </td>
                                <td>
                                    <%# Eval("ExamFullName")%>
                                </td>
                                <td>
                                    <%# Eval("ExamEligiblityCriteria")%>
                                </td>
                                <td>
                                    <img id="Exam" alt='<%# Eval("ExamLogo")%>' height="50px;" width="50px;" src='<%# String.Format("{0}{1}","/image.axd?Exam=",string.IsNullOrEmpty(Eval("ExamLogo").ToString()) ?"NoCarImage.PNG":Eval("ExamLogo")) %>' />
                                </td>
                                <td>
                                    <%# Eval("ExamStatus")%>
                                </td>
                                <td>
                                    <asp:LinkButton ID="BtnEdit" runat="server"  Text="Edit"
                                        CommandArgument='<%# Eval("ExamId")%>' CommandName="Edit" CausesValidation="false" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                    <Aj:CustomPaging ID="ucCustomPaging" runat="server" />
               
                <div id="divInstituteTypeInsert" class="popup_block width60perc">
                    <fieldset id="basicInfo">
                        <legend>
                            <asp:Label ID="lblInsertUpdate" runat="server" Text="Add Exam"></asp:Label>
                        </legend>
                        <ul class="pouplist">
                        <li style="width: 48% !important; float: left;">
                                <label>
                                    Exam Name:</label>
                                <asp:TextBox ID="txtExamName" runat="server" Style="min-width: 201px !important;" TabIndex="1" ValidationGroup="Exam"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvExamName1" runat="server" ControlToValidate="txtExamName"
                                    SetFocusOnError="True" ValidationGroup="Exam"></asp:RequiredFieldValidator>
                            </li>
                          
                            
                           <li style="width: 48% !important; float: left;">
                                <label>
                                    Exam Full Name:</label>
                                <asp:TextBox ID="txtExamFullName" runat="server" Style="min-width: 201px !important;" TabIndex="2" ValidationGroup="Exam"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvExamFullName" runat="server" ControlToValidate="txtExamFullName"
                                    SetFocusOnError="True" ValidationGroup="Exam"></asp:RequiredFieldValidator>
                            </li>
                            <li style="width: 48% !important; float: left;">
                                <label>
                                    Exam Popular Name:</label>
                                <asp:TextBox ID="txtPopularName" runat="server" Style="min-width: 201px !important;" TabIndex="3" ValidationGroup="Exam"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPopularName" runat="server" ControlToValidate="txtPopularName"
                                    SetFocusOnError="True" ValidationGroup="Exam"></asp:RequiredFieldValidator>
                            </li>
                             <li style="width: 48% !important; float: left;">
                                <label>
                                    Course Name:</label>
                                <asp:DropDownList ID="ddlCourseName" Style="min-width: 205px !important;" runat="server" TabIndex="4" ValidationGroup="Exam">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourseName"
                                    SetFocusOnError="True" InitialValue="0" ValidationGroup="Exam"></asp:RequiredFieldValidator>
                            </li>
                           <li style="width: 48% !important; float: left;">
                                <label>
                                    Eligiblity Criteria:</label>
                                <asp:TextBox ID="txtEligiblityCriteria" Style="min-width: 201px !important;"  runat="server" TabIndex="5"
                                    ValidationGroup="Exam"></asp:TextBox>
                            </li>
                            
                            <li style="width: 48% !important; float: left;" id="uploadImagePanel" class="overlaypanel">
                                <label>
                                    Exam Logo:</label>
                                <asp:HiddenField runat="server" ID="hndfileName"></asp:HiddenField>
                                <asp:FileUpload runat="server" TabIndex="6" Style="width: 201px !important;"  ID="flUploadImage" />
                                <asp:Button runat="server" ID="btnUploadImage" Visible="false" Text="Upload" ValidationGroup="imageupload"
                                    OnClientClick="colorboxDialogSubmitClicked('ExamUpload', 'uploadImagePanel');" />
                                <asp:Image runat="server" ID="imgExamLogo" Width="100px" Height="100px" Visible="False">
                                </asp:Image>
                            </li>
                           <li style="width: 48% !important; float: left;">
                                <label>
                                    Website:</label>
                                <asp:TextBox ID="txtWebSite" runat="server" Style="min-width: 201px !important;" TabIndex="7" ValidationGroup="Exam"></asp:TextBox>
                            </li>
                            <li style="width: 48% !important; float: left;">
                                <label>
                                    Status:</label>
                                <asp:CheckBox ID="chkStatus" runat="server" TabIndex="8" />
                            </li>
                            <li style="width: 99% !important; height:185px !important;">
                                <label>
                                    Exam Desc:</label><span class="fleft" style="margin:3px 5px;">
                                <Aj:ExamDesc ID="txtExamDescription" TabIndex="9"  runat="server" /></span>
                            </li>
                            
                            <li style="width: 99% !important;">
                                <label>
                                    &nbsp;</label>
                                <asp:Button ID="btnCourse" runat="server" Text="Add" TabIndex="10" CausesValidation="true"
                                    ValidationGroup="Exam" OnClick="btnExam_Click" />
                                <input id="btnReset" type="button" value="Reset" TabIndex="11" onclick="ClearAllFields();" title="Please Reset" />
                            </li>
                        </ul>
                    </fieldset>
                </div>

                 <fieldset style="display:none;">
                    <legend>Exam Master </legend>
                    <ul class="options-bar">
                        <li class="opt-barlist">
                            <label>
                                Upload File:
                            </label>
                            <asp:FileUpload ID="fileUploadExcel" runat="server"  />
                            <asp:RequiredFieldValidator ID="rfvUploadExcel" runat="server" ControlToValidate="fileUploadExcel" ErrorMessage="?" CssClass="error1" ValidationGroup="GrUpload"></asp:RequiredFieldValidator>
                        </li>
                    </ul>
                </fieldset>
        </ContentTemplate>
       
    </asp:UpdatePanel>
    <script type="text/javascript" src="../../Scripts/Autocomplete.js"></script>
    <script type="text/javascript">
        var examUrl = "../../WebServices/CommonWebServices.asmx/GetAllExamList";
        BindDropDownCommonForAdminAutoComplete($("#<%=txtExamNameSearch.ClientID %>"), examUrl);

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                BindDropDownCommonForAdminAutoComplete($("#<%=txtExamNameSearch.ClientID %>"), examUrl);
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
         

    </script>
</asp:Content>

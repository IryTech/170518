<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="AddExamFormMaster.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Exam.AddExamFormMaster" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
   
    <%--<asp:UpdatePanel ID="updExamFormMaster" runat="server">
<ContentTemplate>--%>
    <asp:HiddenField ID="hdnExamUrl" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdnExamTag" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdnExamTitle" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdnExamMetaDesc" runat="server"></asp:HiddenField>
    <asp:Label ID="lblSeccessMsg" CssClass="success" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblErorrMsg" CssClass="error" runat="server" Visible="false"></asp:Label>
    <ul class="addPage_utility">
        <li class="fright" style="width: 171px !important;">
            <div class="navbar-inner">
                <a href="ManageExamFormMaster.aspx" class="viewIco">Exam Form Master</a>
                <div class="clear">
                </div>
            </div>
        </li>
        <li class="fright" style="width: 72px !important;">
            <asp:ImageButton ID="btnUpload" runat="server" ImageUrl="~/AdminPanel/Images/CommonImages/uploadExcel.png" ToolTip="Upload Excel Format" ValidationGroup="GrUpload" OnClick="btnUpload_Click" />
            <asp:ImageButton ID="btnPreview" runat="server" ImageUrl="~/AdminPanel/Images/CommonImages/downloadExcel2.png" ToolTip="Download Excel Format" OnClick="btnPreview_Click" />
        </li>
    </ul>
    <asp:Label ID="lblHeader" runat="server"></asp:Label>
    <fieldset>
        <legend>
            <asp:Label ID="lblInsertUpdate" runat="server" Text="Add Exam Form"></asp:Label></legend>
        <ul class="width48perc fleft">
            <li>
                <label>
                    Form Subject:</label>
                <asp:TextBox ID="txtFormSubject" runat="server" TabIndex="2" ValidationGroup="Exam" onkeyup="keyup(this)" ToolTip="Please Enter Form Subject"></asp:TextBox>
            </li>
            <li>
                <label>
                    Form year:</label>
                <asp:TextBox ID="txtFormYear" runat="server" TabIndex="7" ValidationGroup="Exam" ToolTip="Please Enter Form Year"></asp:TextBox>
            </li>
            <li>
                <label>
                    Form Website:</label>
                <asp:TextBox ID="txtFormWebsite" runat="server" TabIndex="8" ValidationGroup="Exam" ToolTip="Please Enter Form Web site"></asp:TextBox>
            </li>
            <li>
                <label>
                    Form Result website:</label>
                <asp:TextBox ID="txtFormResultWebsite" runat="server" TabIndex="7"></asp:TextBox>
            </li>
            <li>
                <label>
                    Exam Name:</label>
                <asp:DropDownList ID="ddlExamName" runat="server" TabIndex="1" ValidationGroup="Exam" ToolTip="Please Select Exam Name">
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ValidationGroup="Exam" ControlToValidate="ddlExamName" InitialValue="Select" ErrorMessage="?"></asp:RequiredFieldValidator>
            </li>
            <li>
                <label>
                    Course:</label>
                <asp:DropDownList ID="ddlCourse" runat="server" TabIndex="1" ValidationGroup="Exam" ToolTip="Please Select Course" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ID="rfvCourse" ValidationGroup="Exam" ControlToValidate="ddlCourse" InitialValue="Select" ErrorMessage="?"></asp:RequiredFieldValidator>
            </li>
            <li>
                <label>
                    Exam Form Price:</label>
                <asp:TextBox ID="txtExamFormPrice" runat="server" TabIndex="7"></asp:TextBox>
            </li>
            <li>
                <label>
                    Exam Form DD:</label>
                <asp:TextBox ID="txtExamFormDD" runat="server" TabIndex="7"></asp:TextBox>
            </li>
            <li>
                <label>
                    Exam Form Syllabus:</label>
                <asp:TextBox ID="txtExamFormSyllabus" TextMode="MultiLine" runat="server" TabIndex="7"></asp:TextBox>
            </li>
            <li>
                <label>
                    Exam Form Center:</label>
                <asp:TextBox ID="txtExamFormCenter" TextMode="MultiLine" runat="server" TabIndex="7"></asp:TextBox>
            </li>
            <li>
                <label>
                    Exam Form Store:</label>
                <asp:TextBox ID="txtExamFormStore" TextMode="MultiLine" runat="server" TabIndex="7"></asp:TextBox>
            </li>
        </ul>
        <ul class="width48perc fleft">
            <li>
                <label>
                    Form sale Start date:</label>
                <asp:RadioButtonList ID="rbtFormSaleDate" RepeatDirection="Horizontal" CssClass="RadioButtonList" TabIndex="9" runat="server">
                    <asp:ListItem Value="1" Text="Exect" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Not Exect"></asp:ListItem>
                </asp:RadioButtonList>
            </li>
            <li><a id="pnlExect">
                <label>
                    Exact Start date:</label>
                <asp:TextBox ID="txtSaleExectStartDate1" runat="server" ValidationGroup="Exam"></asp:TextBox>
                <Ajex:CalendarExtender ID="ajaxExactStartDate" TargetControlID="txtSaleExectStartDate1" Format="dd/MM/yyyy" PopupPosition="Right" runat="server" CssClass="CalendarCSS" PopupButtonID="txtSaleExectStartDate1">
                </Ajex:CalendarExtender>
            </a><a id="pnlNotExect">
                <label>
                    Start date:</label>
                <asp:TextBox ID="txtSaleNotExectStartDate1" Style="width: 50px !important; min-width: 73px !important; font-size: 10px !important;" runat="server"></asp:TextBox>
                <Ajex:CalendarExtender ID="ajaxStartDate" TargetControlID="txtSaleNotExectStartDate1" Format="dd/MM/yyyy" PopupPosition="Right" runat="server" CssClass="CalendarCSS" PopupButtonID="txtSaleNotExectStartDate1">
                </Ajex:CalendarExtender>
                <label style="width: 60px !important;">
                    End date:</label>
                <asp:TextBox ID="txtSaleNotExectEndDate1" Style="width: 50px !important; min-width: 73px !important; font-size: 10px !important;" runat="server"></asp:TextBox>
                <Ajex:CalendarExtender ID="ajaxEndDate" TargetControlID="txtSaleNotExectEndDate1" Format="dd/MM/yyyy" PopupPosition="Right" runat="server" CssClass="CalendarCSS" PopupButtonID="txtSaleNotExectEndDate1">
                </Ajex:CalendarExtender>
            </a></li>
            <li>
                <label>
                    sale start date remark:</label>
                <asp:TextBox ID="txtFormSaleStartDateRemark" runat="server" TabIndex="7" ValidationGroup="Exam" ToolTip="Please Enter Form Start Sale Date Remark"></asp:TextBox>
            </li>
            <li>
                <label>
                    Form sale End date:</label>
                <asp:RadioButtonList ID="rbtFormSaleEndDate" RepeatDirection="Horizontal" CssClass="RadioButtonList" TabIndex="8" runat="server">
                    <asp:ListItem Value="1" Text="Exect" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Not Exect"></asp:ListItem>
                </asp:RadioButtonList>
            </li>
            <li><a id="pnlFormSaleExectEndDate">
                <label>
                    Exect date:</label>
                <asp:TextBox ID="txtSaleExectEndDate2" runat="server"></asp:TextBox>
                <Ajex:CalendarExtender ID="ajexSaleExectEndDate2" TargetControlID="txtSaleExectEndDate2" Format="dd/MM/yyyy" PopupPosition="Right" runat="server" CssClass="CalendarCSS" PopupButtonID="txtSaleExectEndDate2">
                </Ajex:CalendarExtender>
            </a><a id="pnlFormSaleNotExectEndDate">
                <label>
                    Start date:</label>
                <asp:TextBox ID="txtSaleNotExectStartDate2" Style="width: 50px !important; min-width: 73px !important; font-size: 10px !important;" runat="server"></asp:TextBox>
                <Ajex:CalendarExtender ID="ajexSaleNotExectStartDate2" TargetControlID="txtSaleNotExectStartDate2" Format="dd/MM/yyyy" PopupPosition="Right" runat="server" CssClass="CalendarCSS" PopupButtonID="txtSaleNotExectStartDate2">
                </Ajex:CalendarExtender>
                <label style="width: 60px !important;">
                    End date:</label>
                <asp:TextBox ID="txtSaleNotExectEndDate2" Style="width: 50px !important; min-width: 73px !important; font-size: 10px !important;" runat="server"></asp:TextBox>
                <Ajex:CalendarExtender ID="ajexSaleNotExectEndDate2" TargetControlID="txtSaleNotExectEndDate2" Format="dd/MM/yyyy" PopupPosition="Right" runat="server" CssClass="CalendarCSS" PopupButtonID="txtSaleNotExectEndDate2">
                </Ajex:CalendarExtender>
            </a></li>
            <li>
                <label>
                    sale end date remark:</label>
                <asp:TextBox ID="txtFormSaleEndDateRemark" runat="server" TabIndex="7"></asp:TextBox>
            </li>
            <li>
                <label>
                    Form submit date:</label>
                <asp:RadioButtonList ID="rbtFormSubmitDate" RepeatDirection="Horizontal" CssClass="RadioButtonList" TabIndex="8" runat="server" ToolTip="Please Enter Form Submit Date">
                    <asp:ListItem Value="1" Text="Exect" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Not Exect"></asp:ListItem>
                </asp:RadioButtonList>
            </li>
            <li><a id="pnlExectFormSubmitDate">
                <label>
                    Submit exect date:</label>
                <asp:TextBox ID="txtFormSubmitExectDate1" runat="server"></asp:TextBox>
                <Ajex:CalendarExtender ID="ajexFormSubmitExectDate1" TargetControlID="txtFormSubmitExectDate1" Format="dd/MM/yyyy" PopupPosition="Right" runat="server" CssClass="CalendarCSS" PopupButtonID="txtFormSubmitExectDate1">
                </Ajex:CalendarExtender>
            </a><a id="pnlNotExectFormSubmitDate">
                <label>
                    Start date:</label>
                <asp:TextBox ID="txtFormSubmitStartDate1" Style="width: 50px !important; min-width: 73px !important; font-size: 10px !important;" runat="server"></asp:TextBox>
                <Ajex:CalendarExtender ID="ajexFormSubmitStartDate" BehaviorID="calender10" TargetControlID="txtFormSubmitStartDate1" Format="dd/MM/yyyy" PopupPosition="Right" runat="server" CssClass="CalendarCSS" PopupButtonID="txtFormSubmitStartDate1">
                </Ajex:CalendarExtender>
                <label style="width: 60px !important;">
                    End date:</label>
                <asp:TextBox ID="txtFormSubmitEndDate1" Style="width: 50px !important; min-width: 73px !important; font-size: 10px !important;" runat="server"></asp:TextBox>
                <Ajex:CalendarExtender ID="ajexEndDate" TargetControlID="txtFormSubmitEndDate1" Format="dd/MM/yyyy" PopupPosition="Right" runat="server" CssClass="CalendarCSS" PopupButtonID="txtFormSubmitEndDate1">
                </Ajex:CalendarExtender>
            </a></li>
            <li>
                <label>
                    Submit date remark:</label>
                <asp:TextBox ID="txtFormSubmitDateRemark" runat="server" TabIndex="7"></asp:TextBox>
            </li>
            <li>
                <label>
                    Result date:</label>
                <asp:RadioButtonList ID="rbtResultDate" RepeatDirection="Horizontal" CssClass="RadioButtonList" TabIndex="8" runat="server">
                    <asp:ListItem Value="1" Text="Exect" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Not Exect"></asp:ListItem>
                </asp:RadioButtonList>
            </li>
            <li><a id="ExectResultDate">
                <label>
                    exect date:</label>
                <asp:TextBox ID="txtResultExactDate" runat="server"></asp:TextBox>
                <Ajex:CalendarExtender ID="ajexExectResultDate" TargetControlID="txtResultExactDate" Format="dd/MM/yyyy" PopupPosition="Right" runat="server" CssClass="CalendarCSS" PopupButtonID="txtResultExactDate">
                </Ajex:CalendarExtender>
            </a><a id="NotExectResultDate">
                <label>
                    Start date:</label>
                <asp:TextBox ID="txtResultStartDate" Style="width: 50px !important; min-width: 73px !important; font-size: 10px !important;" runat="server"></asp:TextBox>
                <Ajex:CalendarExtender ID="ajextStardate" TargetControlID="txtResultStartDate" Format="dd/MM/yyyy" PopupPosition="Right" runat="server" CssClass="CalendarCSS" PopupButtonID="txtResultStartDate">
                </Ajex:CalendarExtender>
                <label style="width: 60px !important;">
                    End date:</label>
                <asp:TextBox ID="txtResultEndtDate" Style="width: 50px !important; min-width: 73px !important; font-size: 10px !important;" runat="server"></asp:TextBox>
                <Ajex:CalendarExtender ID="ajexEnd" TargetControlID="txtResultEndtDate" Format="dd/MM/yyyy" PopupPosition="Right" runat="server" CssClass="CalendarCSS" PopupButtonID="txtResultEndtDate">
                </Ajex:CalendarExtender>
            </a></li>
            <li>
                <label>
                    Result date remark:</label>
                <asp:TextBox ID="txtResultDateRemark" runat="server" TabIndex="8"></asp:TextBox>
            </li>
            <li>
                <label>
                    Display:</label>
                <asp:CheckBox ID="chkFormStatus" runat="server" />
            </li>
        </ul>
    </fieldset>
    <fieldset style="background: none !important;">
        <h5 style="font-size: 17px !important; font-weight: normal !important; border-bottom: 1px dashed #e1e1e1; padding: 3px 60px !important;">
            SEO Tool</h5>
        <ul>
            <li>
                <label>
                    Form Url:</label>
                <asp:TextBox ID="txtExamFormUrl" runat="server" CssClass="autocomplete" style="background:none !important; text-indent:5px !important;" Width="60%" TabIndex="3" ValidationGroup="Exam" onkeyup="urlkeyup(this,'examUrl')" ToolTip="Please Enter Exam Form Url"></asp:TextBox>
                <span id="msgExamUrl" class="error1"></span>
                <div id="examUrl">
                </div>
            </li>
            <li>
                <label>
                    Form title:</label>
                <asp:TextBox ID="txtFormtitle" runat="server"  CssClass="autocomplete" style="background:none !important; text-indent:5px !important;" Width="60%" TabIndex="4" ValidationGroup="Exam" onkeyup="titlekeyup(this,'examTitle')" ToolTip="Please Enter Form title"></asp:TextBox>
                <div id="examTitle">
                </div>
            </li>
            <li>
                <label>
                    Form meta desc:</label>
                <asp:TextBox ID="txtFormMetaDesc" runat="server" style="width:59.5%;max-width: 100%;" TextMode="MultiLine" TabIndex="6" ValidationGroup="Exam" onkeyup="tagdesckeyup(this,'examMetaDesc')" ToolTip="Please Enter Form Meta Dscription"></asp:TextBox>
                <div id="examMetaDesc">
                </div>
            </li>
            <li>
                <label>
                    Form meta tag:</label>
                <asp:TextBox ID="txtFormMataTag" runat="server" style="width:59.5%;max-width: 100%;" TextMode="MultiLine" TabIndex="5" ValidationGroup="Exam" onkeyup="tagkeyup(this,'examMetaTag')" ToolTip="Please Enter Form Mata Tag"></asp:TextBox>
                <div id="examMetaTag">
                </div>
            </li>
            
            <li>
                <label>
                    &nbsp;</label>
                <asp:Button ID="btnExamForm" runat="server" Text="Add" TabIndex="9" ValidationGroup="Exam" CausesValidation="false" OnClientClick="return validate();" OnClick="btnExamForm_Click" />
                <input id="btnReset" type="button" value="Reset" onclick="ClearAllFields();" title="Please Reset" />
            </li>
        </ul>
    </fieldset>
    <%--     </ContentTemplate>
            </asp:UpdatePanel>--%>
    <fieldset style="display:none;">
        <legend>Upload an excel sheet of Exam Master</legend>
        <ul>
            <li>
                <label>
                    Upload File:
                </label>
                <asp:FileUpload ID="fileUploadExcel" runat="server" TabIndex="11" />
                <%-- <asp:RequiredFieldValidator ID="rfvUpload" runat="server" ControlToValidate="fileUploadExcel" ValidationGroup="GrUpload"></asp:RequiredFieldValidator>--%>
                <asp:RequiredFieldValidator ID="rfvExcelUpload" runat="Server" ControlToValidate="fileUploadExcel" ValidationGroup="ExcelUpload" />
                <asp:RegularExpressionValidator ID="revExcelUpload" runat="server" ControlToValidate="fileUploadExcel" ValidationGroup="GrUpload"></asp:RegularExpressionValidator>
            </li>
        </ul>
    </fieldset>
    <script type="text/javascript">
        function ClearAllFields() {
            document.getElementById('ctl00_ContentPlaceHolderMain_ddlExamName').selectedIndex = 0;
            document.getElementById('ctl00_ContentPlaceHolderMain_txtExamFormUrl').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtFormtitle').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtFormMetaDesc').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtFormMataTag').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtFormSubject').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtFormYear').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtFormWebsite').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtSaleExectStartDate1').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtSaleNotExectStartDate1').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtSaleNotExectEndDate1').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtFormSaleStartDateRemark').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtSaleExectEndDate2').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtSaleNotExectStartDate2').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtSaleNotExectEndDate2').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtFormSaleEndDateRemark').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtFormSubmitExectDate1').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtFormSubmitStartDate1').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtFormSubmitEndDate1').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtFormSubmitDateRemark').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtResultExactDate').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtResultStartDate').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtResultEndtDate').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtResultDateRemark').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtFormResultWebsite').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtExamFormPrice').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtExamFormStore').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtExamFormCenter').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtExamFormDD').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_txtExamFormSyllabus').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_chkFormStatus').checked = false;
            document.getElementById('ctl00_ContentPlaceHolderMain_btnExamForm').value = 'Add';
            window.scrollTo(0, 0);
        }
    </script>
    <script language="javascript" type="text/javascript">
        function resetAllUserRegistrationValidation() {
            $('#<%=ddlExamName.ClientID %>').removeClass("mandatory");
            $('#<%=txtExamFormUrl.ClientID %>').removeClass("mandatory");
            $('#<%=txtFormtitle.ClientID %>').removeClass("mandatory");
            $('#<%=txtFormMetaDesc.ClientID %>').removeClass("mandatory");
            $('#<%=txtFormMataTag.ClientID %>').removeClass("mandatory");
            $('#<%=txtFormSubject.ClientID %>').removeClass("mandatory");
            $('#<%=txtFormYear.ClientID %>').removeClass("mandatory");
            $('#<%=txtFormWebsite.ClientID %>').removeClass("mandatory");
            $('#<%=txtSaleExectStartDate1.ClientID %>').removeClass("mandatory");
            $('#<%=txtSaleNotExectStartDate1.ClientID %>').removeClass("mandatory");
            $('#<%=txtSaleNotExectEndDate1.ClientID %>').removeClass("mandatory");
            $('#<%=txtFormSaleStartDateRemark.ClientID %>').removeClass("mandatory");
            $('#<%=txtSaleExectEndDate2.ClientID %>').removeClass("mandatory");
            $('#<%=txtSaleNotExectStartDate2.ClientID %>').removeClass("mandatory");
            $('#<%=txtSaleNotExectEndDate2.ClientID %>').removeClass("mandatory");
            $('#<%=txtFormSaleEndDateRemark.ClientID %>').removeClass("mandatory");
            $('#<%=txtFormSubmitExectDate1.ClientID %>').removeClass("mandatory");
            $('#<%=txtFormSubmitStartDate1.ClientID %>').removeClass("mandatory");
            $('#<%=txtFormSubmitEndDate1.ClientID %>').removeClass("mandatory");
            $('#<%=txtFormSubmitDateRemark.ClientID %>').removeClass("mandatory");
            $('#<%=txtResultExactDate.ClientID %>').removeClass("mandatory");
            $('#<%=txtResultStartDate.ClientID %>').removeClass("mandatory");
            $('#<%=txtResultEndtDate.ClientID %>').removeClass("mandatory");
            $('#<%=txtResultDateRemark.ClientID %>').removeClass("mandatory");
            $('#<%=txtFormResultWebsite.ClientID %>').removeClass("mandatory");
            $('#<%=txtExamFormPrice.ClientID %>').removeClass("mandatory");
            $('#<%=txtExamFormStore.ClientID %>').removeClass("mandatory");
            $('#<%=txtExamFormCenter.ClientID %>').removeClass("mandatory");
            $('#<%=txtExamFormDD.ClientID %>').removeClass("mandatory");
            $('#<%=txtExamFormSyllabus.ClientID %>').removeClass("mandatory");
        }
        function validate() {
            resetAllUserRegistrationValidation();
            var ExamName = $('#<%=ddlExamName.ClientID %>').val();
            var ExamFormUrl = $('#<%=txtExamFormUrl.ClientID %>').val();
            var ExamFromTitle = $('#<%=txtFormtitle.ClientID %>').val();
            var FormMetaDesc = $('#<%=txtFormMetaDesc.ClientID %>').val();
            var FormMataTag = $('#<%=txtFormMataTag.ClientID %>').val();
            var FormSubject = $('#<%=txtFormSubject.ClientID %>').val();
            var FormYear = $('#<%=txtFormYear.ClientID %>').val();
            var FormWebsite = $('#<%=txtFormWebsite.ClientID %>').val();
            var FormSaleExectDate = $('#<%=txtSaleExectStartDate1.ClientID %>').val();
            var FormSaleNotExectStartDate = $('#<%=txtSaleNotExectStartDate1.ClientID %>').val();

            var FromSaleNotExectEndDate = $('#<%=txtSaleNotExectEndDate1.ClientID %>').val();
            var FormSaleStartDateRemark = $('#<%=txtFormSaleStartDateRemark.ClientID %>').val();
            var FromSaleExectEndDate = $('#<%=txtSaleExectEndDate2.ClientID %>').val();
            var FromSaleNotExectStartDate2 = $('#<%=txtSaleNotExectStartDate2.ClientID %>').val();
            var FormSaleNotExectEndDate2 = $('#<%=txtSaleNotExectEndDate2.ClientID %>').val();
            var FormSaleEndDateRemark = $('#<%=txtFormSaleEndDateRemark.ClientID %>').val();
            var FormSubmitExectDate = $('#<%=txtFormSubmitExectDate1.ClientID %>').val();
            var FormSubmitStartDate = $('#<%=txtFormSubmitStartDate1.ClientID %>').val();
            var FormSubmitEndDate = $('#<%=txtFormSubmitEndDate1.ClientID %>').val();
            var FormSubmitDateRemark = $('#<%=txtFormSubmitDateRemark.ClientID %>').val();
            var ResultExactDate = $('#<%=txtResultExactDate.ClientID %>').val();
            var ResultStartDate = $('#<%=txtResultStartDate.ClientID %>').val();
            var ResultEndtDate = $('#<%=txtResultEndtDate.ClientID %>').val();

            var ResultDateRemark = $('#<%=txtResultDateRemark.ClientID %>').val();
            var FormResultWebsite = $('#<%=txtFormResultWebsite.ClientID %>').val();
            var ExamFormPrice = $('#<%=txtExamFormPrice.ClientID %>').val();
            var ExamFormStore = $('#<%=txtExamFormStore.ClientID %>').val();
            var ExamFormCenter = $('#<%=txtExamFormCenter.ClientID %>').val();
            var ExamFormDD = $('#<%=txtExamFormDD.ClientID %>').val();
            var ExamFormSyllabus = $('#<%=txtExamFormSyllabus.ClientID %>').val();

            if (document.getElementById('<%=ddlExamName.ClientID %>').selectedIndex == 0) {
                $('#<%=ddlExamName.ClientID %>').addClass("mandatory");
                $('#<%=ddlExamName.ClientID %>').focus().select();
                return false;
            }

            if (ExamFormUrl.length == 0) {
                $('#<%=txtExamFormUrl.ClientID %>').addClass("mandatory");
                $('#<%=txtExamFormUrl.ClientID %>').focus().select();
                // $("#msgExamUrl").html("Exam form url is Required!");
                return false;
            }
            if (ExamFromTitle.length == 0) {
                $('#<%=txtFormtitle.ClientID %>').addClass("mandatory");
                $('#<%=txtFormtitle.ClientID %>').focus().select();
                return false;
            }

            if (FormMetaDesc.length == 0) {

                $('#<%=txtFormMetaDesc.ClientID %>').addClass("mandatory");
                $('#<%=txtFormMetaDesc.ClientID %>').focus().select();
                $('#<%=txtFormMetaDesc.ClientID %>').class("border", "1 px solid red");
                return false;
            }

            if (FormMataTag.length == 0) {
                $('#<%=txtFormMataTag.ClientID %>').addClass("mandatory");
                $('#<%=txtFormMataTag.ClientID %>').focus().select();
                return false;
            }
            if (FormSubject.length == 0) {
                $('#<%=txtFormSubject.ClientID %>').addClass("mandatory");
                $('#<%=txtFormSubject.ClientID %>').focus().select();
                return false;
            }

            if (FormYear.length == 0) {

                $('#<%=txtFormYear.ClientID %>').addClass("mandatory");
                $('#<%=txtFormYear.ClientID %>').focus().select();
                return false;
            }
            //=======================
            var digits1 = "0123456789";
            var temp1;

            for (var i = 0; i < document.getElementById("<%=txtFormYear.ClientID %>").value.length; i++) {
                temp1 = document.getElementById("<%=txtFormYear.ClientID%>").value.substring(i, i + 1);
                if (digits1.indexOf(temp1) == -1) {
                    alert("Please enter year in digits");
                    document.getElementById("<%=txtFormYear.ClientID%>").focus();
                    return false;
                }
            }
            //========================

            if (FormWebsite.length == 0) {
                $('#<%=txtFormWebsite.ClientID %>').addClass("mandatory");
                $('#<%=txtFormWebsite.ClientID %>').focus().select();
                return false;
            }
            else if (!checkURL(FormWebsite)) {
                $('#<%=txtFormWebsite.ClientID %>').addClass("mandatory");
                $('#<%=txtFormWebsite.ClientID %>').val("Please check website format");
                $('#<%=txtFormWebsite.ClientID %>').focus().select();
                return false;
            }

            //====================================
            var rblSelectedValue = $("#<%= rbtFormSaleDate.ClientID %> input:checked");
            var radioValue = rblSelectedValue.val();
            if (radioValue == '1') {
                if (FormSaleExectDate.length == 0) {

                    $('#<%=txtSaleExectStartDate1.ClientID %>').addClass("mandatory");
                    $('#<%=txtSaleExectStartDate1.ClientID %>').focus().select();
                    return false;
                }
            }
            else if (radioValue == '2') {
                if (FormSaleNotExectStartDate.length == 0) {
                    $('#<%=txtSaleNotExectStartDate1.ClientID %>').addClass("mandatory");
                    $('#<%=txtSaleNotExectStartDate1.ClientID %>').focus().select();
                    return false;
                }
                if (FromSaleNotExectEndDate.length == 0) {
                    $('#<%=txtSaleNotExectEndDate1.ClientID %>').addClass("mandatory");
                    $('#<%=txtSaleNotExectEndDate1.ClientID %>').focus().select();
                    return false;
                }
            }

            //====================================


            if (FormSaleStartDateRemark.length == 0) {

                $('#<%=txtFormSaleStartDateRemark.ClientID %>').addClass("mandatory");
                $('#<%=txtFormSaleStartDateRemark.ClientID %>').focus().select();
                return false;
            }
            //====================================
            var rblSelectedEndSaleValue = $("#<%= rbtFormSaleEndDate.ClientID %> input:checked");
            var radioValue1 = rblSelectedEndSaleValue.val();
            if (radioValue1 == '1') {
                if (FromSaleExectEndDate.length == 0) {

                    $('#<%=txtSaleExectEndDate2.ClientID %>').addClass("mandatory");
                    $('#<%=txtSaleExectEndDate2.ClientID %>').focus().select();
                    return false;
                }
            }
            else if (radioValue1 == '2') {
                if (FromSaleNotExectStartDate2.length == 0) {
                    $('#<%=txtSaleNotExectStartDate2.ClientID %>').addClass("mandatory");
                    $('#<%=txtSaleNotExectStartDate2.ClientID %>').focus().select();
                    return false;
                }
                if (FormSaleNotExectEndDate2.length == 0) {
                    $('#<%=txtSaleNotExectEndDate2.ClientID %>').addClass("mandatory");
                    $('#<%=txtSaleNotExectEndDate2.ClientID %>').focus().select();
                    return false;
                }
            }

            //====================================


            if (FormSaleEndDateRemark.length == 0) {
                $('#<%=txtFormSaleEndDateRemark.ClientID %>').addClass("mandatory");
                $('#<%=txtFormSaleEndDateRemark.ClientID %>').focus().select();
                return false;
            }
            //====================================
            var rblSelectedSubmitDateValue = $("#<%= rbtFormSubmitDate.ClientID %> input:checked");
            var radioSubmitDate = rblSelectedSubmitDateValue.val();
            if (radioSubmitDate == '1') {
                if (FormSubmitExectDate.length == 0) {

                    $('#<%=txtFormSubmitExectDate1.ClientID %>').addClass("mandatory");
                    $('#<%=txtFormSubmitExectDate1.ClientID %>').focus().select();
                    return false;
                }
            }
            else if (radioSubmitDate == '2') {
                if (FormSubmitStartDate.length == 0) {
                    $('#<%=txtFormSubmitStartDate1.ClientID %>').addClass("mandatory");
                    $('#<%=txtFormSubmitStartDate1.ClientID %>').focus().select();
                    return false;
                }
                if (FormSubmitEndDate.length == 0) {
                    $('#<%=txtFormSubmitEndDate1.ClientID %>').addClass("mandatory");
                    $('#<%=txtFormSubmitEndDate1.ClientID %>').focus().select();
                    return false;
                }
            }

            //====================================


            if (FormSubmitDateRemark.length == 0) {

                $('#<%=txtFormSubmitDateRemark.ClientID %>').addClass("mandatory");
                $('#<%=txtFormSubmitDateRemark.ClientID %>').focus().select();
                return false;
            }
            //=============Result date Validation=======================
            var rblSelectedResultDateValue = $("#<%= rbtResultDate.ClientID %> input:checked");
            var radioResultDate = rblSelectedResultDateValue.val();
            if (radioResultDate == '1') {
                if (ResultExactDate.length == 0) {

                    $('#<%=txtResultExactDate.ClientID %>').addClass("mandatory");
                    $('#<%=txtResultExactDate.ClientID %>').focus().select();
                    return false;
                }
            }
            else if (radioResultDate == '2') {
                if (ResultStartDate.length == 0) {
                    $('#<%=txtResultStartDate.ClientID %>').addClass("mandatory");
                    $('#<%=txtResultStartDate.ClientID %>').focus().select();
                    return false;
                }
                if (ResultEndtDate.length == 0) {
                    $('#<%=txtResultEndtDate.ClientID %>').addClass("mandatory");
                    $('#<%=txtResultEndtDate.ClientID %>').focus().select();
                    return false;
                }
            }

            //====================================

            if (ResultDateRemark.length == 0) {
                $('#<%=txtResultDateRemark.ClientID %>').addClass("mandatory");
                $('#<%=txtResultDateRemark.ClientID %>').focus().select();
                return false;
            }
            if (FormResultWebsite.length == 0) {
                $('#<%=txtFormResultWebsite.ClientID %>').addClass("mandatory");
                $('#<%=txtFormResultWebsite.ClientID %>').focus().select();
                return false;
            } else if (!checkURL(FormResultWebsite)) {
                $('#<%=txtFormResultWebsite.ClientID %>').addClass("mandatory");
                $('#<%=txtFormResultWebsite.ClientID %>').val("Please check website format");
                $('#<%=txtFormResultWebsite.ClientID %>').focus().select();
                return false;
            }

            if (ExamFormPrice.length == 0) {

                $('#<%=txtExamFormPrice.ClientID %>').addClass("mandatory");
                $('#<%=txtExamFormPrice.ClientID %>').focus().select();
                return false;
            }

            //========================
            if (ExamFormStore.length == 0) {
                $('#<%=txtExamFormStore.ClientID %>').addClass("mandatory");
                $('#<%=txtExamFormStore.ClientID %>').focus().select();
                return false;
            }
            if (ExamFormCenter.length == 0) {
                $('#<%=txtExamFormCenter.ClientID %>').addClass("mandatory");
                $('#<%=txtExamFormCenter.ClientID %>').focus().select();
                return false;
            }

            if (ExamFormDD.length == 0) {

                $('#<%=txtExamFormDD.ClientID %>').addClass("mandatory");
                $('#<%=txtExamFormDD.ClientID %>').focus().select();
                return false;
            }

            if (ExamFormSyllabus.length == 0) {

                $('#<%=txtExamFormSyllabus.ClientID %>').addClass("mandatory");
                $('#<%=txtExamFormSyllabus.ClientID %>').focus().select();
                return false;
            }
            else {
                return true;
            }


        }

        $(document).ready(function () {
            $("#ctl00_ContentPlaceHolderMain_rbtResultDate").change(function () {

                var rblSelectedValue = $("#<%= rbtResultDate.ClientID %> input:checked");
                var radioValue = rblSelectedValue.val();
                if (radioValue == '1') {
                    $("#ExectResultDate").show("slow");
                    $("#NotExectResultDate").hide();
                }
                else if (radioValue == '2') {
                    $("#NotExectResultDate").show("slow");
                    $("#ExectResultDate").hide();
                }
            });

            $("#ExectResultDate").show();
            $("#NotExectResultDate").hide();
            $("#<%=btnExamForm.ClientID %>").click(function () {
                var iserror = false;
                if (!validateDetails())
                    iserror = true;
                if (iserror) return false;
            });
            $("#ctl00_ContentPlaceHolderMain_rbtFormSubmitDate").change(function () {

                var rblSelectedValue = $("#<%= rbtFormSubmitDate.ClientID %> input:checked");
                var radioValue = rblSelectedValue.val();
                if (radioValue == '1') {
                    $("#pnlExectFormSubmitDate").show("slow");
                    $("#pnlNotExectFormSubmitDate").hide();
                }
                else if (radioValue == '2') {
                    $("#pnlNotExectFormSubmitDate").show("slow");
                    $("#pnlExectFormSubmitDate").hide();
                }
            });

            $("#pnlExectFormSubmitDate").show();
            $("#pnlNotExectFormSubmitDate").hide();
            $("#ctl00_ContentPlaceHolderMain_rbtFormSaleEndDate").change(function () {

                var rblSelectedValue = $("#<%= rbtFormSaleEndDate.ClientID %> input:checked");
                var radioValue = rblSelectedValue.val();
                if (radioValue == '1') {
                    $("#pnlFormSaleExectEndDate").show("slow");
                    $("#pnlFormSaleNotExectEndDate").hide();
                }
                else if (radioValue == '2') {
                    $("#pnlFormSaleNotExectEndDate").show("slow");
                    $("#pnlFormSaleExectEndDate").hide();
                }
            });

            $("#pnlFormSaleExectEndDate").show();
            $("#pnlFormSaleNotExectEndDate").hide();

            $("#ctl00_ContentPlaceHolderMain_rbtFormSaleDate").change(function () {
                var rblSelectedValue = $("#<%= rbtFormSaleDate.ClientID %> input:checked");
                var radioValue = rblSelectedValue.val();
                if (radioValue == '1') {
                    $("#pnlExect").show("slow");
                    $("#pnlNotExect").hide();
                }
                else if (radioValue == '2') {
                    $("#pnlNotExect").show("slow");
                    $("#pnlExect").hide();
                }
            });

            $("#pnlExect").show();
            $("#pnlNotExect").hide();
        });



        function keyup(control) {
            CopyContent(control, $("#<%=txtExamFormUrl.ClientID %>"), $("#<%=txtFormMataTag.ClientID %>"), $("#<%=txtFormtitle.ClientID %>"), $("#<%=txtFormMetaDesc.ClientID %>"));
        }
        function urlkeyup(control, cssName) {
            GetUrlLength(control, cssName, $("#<%=hdnExamUrl.ClientID %>"));
        }
        function tagkeyup(control, cssName) {
            GetUrlLength(control, cssName, $("#<%=hdnExamTag.ClientID %>"));
        }
        function tagdesckeyup(control, cssName) {
            GetUrlLength(control, cssName, $("#<%=hdnExamMetaDesc.ClientID %>"));
        }
        function titlekeyup(control, cssName) {
            GetUrlLength(control, cssName, $("#<%=hdnExamTitle.ClientID %>"));
        };

        var blnflag = false;

        function checkURL(value) {
            var urlregex = new RegExp("^(http:\/\/www.|https:\/\/www.|ftp:\/\/www.|www.){1}([0-9A-Za-z]+\.)");
            if (urlregex.test(value)) {
                return (true);
            }
            return (false);
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        // Add initializeRequest and endRequest
        prm.add_initializeRequest(uploadpartial);
        function uploadpartial() {

            $("#ctl00_ContentPlaceHolderMain_rbtResultDate").change(function () {

                var rblSelectedValue = $("#<%= rbtResultDate.ClientID %> input:checked");
                var radioValue = rblSelectedValue.val();
                if (radioValue == '1') {
                    $("#ExectResultDate").show("slow");
                    $("#NotExectResultDate").hide();
                }
                else if (radioValue == '2') {
                    $("#NotExectResultDate").show("slow");
                    $("#ExectResultDate").hide();
                }
            });

            $("#ExectResultDate").show();
            $("#NotExectResultDate").hide();
            $("#<%=btnExamForm.ClientID %>").click(function () {
                var iserror = false;
                if (!validateDetails())
                    iserror = true;
                if (iserror) return false;
            });
            $("#ctl00_ContentPlaceHolderMain_rbtFormSubmitDate").change(function () {

                var rblSelectedValue = $("#<%= rbtFormSubmitDate.ClientID %> input:checked");
                var radioValue = rblSelectedValue.val();
                if (radioValue == '1') {
                    $("#pnlExectFormSubmitDate").show("slow");
                    $("#pnlNotExectFormSubmitDate").hide();
                }
                else if (radioValue == '2') {
                    $("#pnlNotExectFormSubmitDate").show("slow");
                    $("#pnlExectFormSubmitDate").hide();
                }
            });

            $("#pnlExectFormSubmitDate").show();
            $("#pnlNotExectFormSubmitDate").hide();
            $("#ctl00_ContentPlaceHolderMain_rbtFormSaleEndDate").change(function () {

                var rblSelectedValue = $("#<%= rbtFormSaleEndDate.ClientID %> input:checked");
                var radioValue = rblSelectedValue.val();
                if (radioValue == '1') {
                    $("#pnlFormSaleExectEndDate").show("slow");
                    $("#pnlFormSaleNotExectEndDate").hide();
                }
                else if (radioValue == '2') {
                    $("#pnlFormSaleNotExectEndDate").show("slow");
                    $("#pnlFormSaleExectEndDate").hide();
                }
            });

            $("#pnlFormSaleExectEndDate").show();
            $("#pnlFormSaleNotExectEndDate").hide();

            $("#ctl00_ContentPlaceHolderMain_rbtFormSaleDate").change(function () {
                var rblSelectedValue = $("#<%= rbtFormSaleDate.ClientID %> input:checked");
                var radioValue = rblSelectedValue.val();
                if (radioValue == '1') {
                    $("#pnlExect").show("slow");
                    $("#pnlNotExect").hide();
                }
                else if (radioValue == '2') {
                    $("#pnlNotExect").show("slow");
                    $("#pnlExect").hide();
                }
            });

            $("#pnlExect").show();
            $("#pnlNotExect").hide();
        }
           
    </script>
</asp:Content>

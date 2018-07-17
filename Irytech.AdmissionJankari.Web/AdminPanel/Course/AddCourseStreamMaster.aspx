<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="AddCourseStreamMaster.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Course.AddCourseStreamMaster" %>

<%@ Register Src="../../UserControl/FckEditorCostomize.ascx" TagName="FckEditorCostomize" TagPrefix="AJ" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:HiddenField ID="hdnCourseStreamUrl" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdnCourseStreamMetaTag" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdnCourseStreamTitle" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdnCourseStreamMetaDesc" runat="server"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="hdnCourseStreamMaster"></asp:HiddenField>
    <asp:Label ID="lblSuccess" runat="server" Text="" Visible="false" CssClass="success">
    </asp:Label>
    <asp:Label ID="lblInform" runat="server" Text="" Visible="false" CssClass="info">
    </asp:Label>
    <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="error">
    </asp:Label>
    <ul class="addPage_utility">
        <li class="fright" style="width: 142px !important;">
            <div class="navbar-inner">
                <a class="viewIco" href="CourseStreamMaster.aspx">Stream Master</a>
                <div class="clear">
                </div>
            </div>
        </li>
        <li class="fright" style="width: 72px !important;">
            <asp:ImageButton ID="btnUpload" runat="server" ImageUrl="~/AdminPanel/Images/CommonImages/uploadExcel.png" ToolTip="Upload Excel" ValidationGroup="GrUpload" OnClick="BtnUploadClick" />
            <asp:ImageButton ID="btnSeeExcelFormat" runat="server" ImageUrl="~/AdminPanel/Images/CommonImages/downloadExcel2.png" ToolTip="Download Excel" OnClick="btnSeeExcelFormat_Click" />
        </li>
    </ul>
    <fieldset>
        <legend>
            <asp:Label runat="server" Text="Add Stream" ID="lblInsertUpdate"></asp:Label></legend>
        <ul>
            <li>
                <label>
                    Stream Name</label>
                <asp:TextBox ID="txtCourseStreamName" runat="server" TabIndex="1" CssClass="autocomplete" style="background:none !important; text-indent:5px !important;" Width="69.5%" ToolTip="Please Enter Stream Name" onkeyup="keyup(this)"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCourseStreamName" SetFocusOnError="true" runat="server" Display="Dynamic" ValidationGroup="course" ControlToValidate="txtCourseStreamName">
                </asp:RequiredFieldValidator>
            </li>
            <li>
                <label>
                    Course</label>
                <asp:DropDownList ID="ddlCourseId" runat="server" TabIndex="2">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvCourseId" SetFocusOnError="true" runat="server" InitialValue="0" Display="Dynamic" ValidationGroup="course" ControlToValidate="ddlCourseId">
                </asp:RequiredFieldValidator>
            </li>
            <li>
                <label>
                    Display
                </label>
                <asp:CheckBox ID="chkbCourseStreamStatus" runat="server" TabIndex="3" ToolTip="Please Enter Course Stream Status" />
            </li>
        </ul>
        <ul>
            <li>
                <label>
                    Stream Description</label><span class="fleft" style="margin: 3px 5px;">
                        <AJ:FckEditorCostomize ID="fckStreamDesc" runat="server" TabIndex="4" FckValue="Write stream description in brief." />
                    </span></li>
        </ul>
    </fieldset>
    <details style="outline: none; margin-bottom: 5px;"><summary style="font-size:18px;color: #3f3f3f; border:1px solid #e1e1e1; background-color:#f1f1f1; padding:3px 5px; outline:none;" >Other Information</summary>
    <fieldset>
        <legend></legend>
        <ul >
             <li>
                <label>
                    Stream History</label>
                <asp:TextBox ID="txtCourseStreamHistory" runat="server" style="width:59.5%;max-width: 100%;" TabIndex="5"   TextMode="MultiLine" ToolTip="Please Enter Stream History"></asp:TextBox>
            </li>
             <li>
                <label>
                    Stream Future</label>
                <asp:TextBox ID="txtCourseStreamFuture" runat="server" style="width:59.5%;max-width: 100%;" TabIndex="6" TextMode="MultiLine"  ToolTip="Please Enter Future"></asp:TextBox>
            </li>
             <li >
                <label>
                    Stream Core Companies</label>
                <asp:TextBox ID="txtCourseStreamCoreCompanies" runat="server" style="width:59.5%;max-width: 100%;" TextMode="MultiLine"   TabIndex="7" ToolTip="Please Enter Core Companies"></asp:TextBox>
            </li>
            <li >
                <label>
                    Stream Related Industry</label>
                <asp:TextBox ID="txtCourseStreamRelatedIndustry" runat="server" style="width:59.5%;max-width: 100%;" TextMode="MultiLine"   TabIndex="8" ToolTip="Please Enter Stream Future"></asp:TextBox>
            </li>
        </ul>
    </fieldset>
    </details>
    <details style="outline: none; margin-bottom: 5px;"><summary style="font-size:18px;color: #3f3f3f; border:1px solid #e1e1e1; background-color:#f1f1f1; padding:3px 5px; outline:none;" >SEO Tool</summary >
    <fieldset>
        <legend></legend>

        <ul>
            <li >
                <label>
                    Sream Title</label>
                <asp:TextBox ID="txtCourseStreamTitle" runat="server" TabIndex="10"  CssClass="autocomplete" style="background:none !important; text-indent:5px !important;" Width="60%" ToolTip="Please Enter Stream Title" onkeyup="titlekeyup(this,'courseStreamTitle')"></asp:TextBox>
                <div id="courseStreamTitle">
                </div>
            </li>
            <li>
                <label>
                    Stream Url</label>
                <asp:TextBox ID="txtCourseStreamUrl" runat="server" TabIndex="9" CssClass="autocomplete" style="background:none !important; text-indent:5px !important;" Width="60%" ToolTip="Please Enter Course Type" onkeyup="urlkeyup(this,'courseStreamUrl')">
                </asp:TextBox>
            </li>
           
             <li>
                <label>
                    Stream Meta description</label>
                <asp:TextBox ID="txtStreamMetaDesc" runat="server" TabIndex="11"  ToolTip="Please Enter Stream Meta description" style="width:59.5%;max-width: 100%;" TextMode="MultiLine" onkeyup="tagdesckeyup(this,'courseStreamMetaDesc')"></asp:TextBox>
                <div id="courseStreamMetaDesc">
                </div>
            </li>
             <li>
                <label>
                    Stream Meta Tag</label>
                <asp:TextBox ID="txtStreamMetaTag" runat="server" TabIndex="12"  TextMode="MultiLine" style="width:59.5%;max-width: 100%;"  ToolTip="Please Enter Stream Meta Tag" onkeyup="tagkeyup(this,'courseStreamTag')"></asp:TextBox>
                <div id="courseStreamTag">
                </div>
            </li>
            
        </ul>
    </fieldset>
    </details>
    <fieldset style="background: none !important;">
        <ul>
            <li>
                <label>
                    &nbsp;</label>
                <asp:Button ID="btntCourseCategory" runat="server" Text="Save" ValidationGroup="course" TabIndex="13" CausesValidation="true" ToolTip="Please Submit" OnClick="BtntCourseCategoryClick" />
                <input id="btncancel" type="button" value="Cancel" onclick="ClearAllFields();" tabindex="14" />
            </li>
        </ul>
    </fieldset>
    <fieldset style="display:none;" >
        <legend>
            <asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
        <ul class="options-bar">
            <li class="opt-barlist">
                <label>
                    Upload File :</label>
                <asp:FileUpload ID="fulUploadExcel" runat="server" />
                <asp:RequiredFieldValidator ID="rfvExcelUpload" runat="server" Display="Dynamic" ControlToValidate="fulUploadExcel" ValidationGroup="GrUpload">
                </asp:RequiredFieldValidator>
            </li>
        </ul>
    </fieldset>
    <script src="../JS/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script src="../JS/CollegeBranch.js" type="text/javascript"></script>
    <script type="text/javascript">
        function keyup(control) {
            CopyContent(control, $("#<%=txtCourseStreamUrl.ClientID %>"), $("#<%=txtStreamMetaTag.ClientID %>"), $("#<%=txtCourseStreamTitle.ClientID %>"), $("#<%=txtStreamMetaDesc.ClientID %>"));
        }
        function urlkeyup(control, cssName) {
            GetUrlLength(control, cssName, $("#<%=hdnCourseStreamUrl.ClientID %>"));
        }
        function tagkeyup(control, cssName) {
            GetUrlLength(control, cssName, $("#<%=hdnCourseStreamMetaTag.ClientID %>"));
        }
        function tagdesckeyup(control, cssName) {
            GetUrlLength(control, cssName, $("#<%=hdnCourseStreamMetaDesc.ClientID %>"));
        }
        function titlekeyup(control, cssName) {
            GetUrlLength(control, cssName, $("#<%=hdnCourseStreamTitle.ClientID %>"));
        };

        function ClearAllFields() {
            $("#<%=txtCourseStreamName.ClientID %>").val('');
            $("#<%=txtCourseStreamUrl.ClientID %>").val('');
            $("#<%=txtCourseStreamTitle.ClientID %>").val('');
            $("#<%=txtStreamMetaTag.ClientID %>").val('');
            $("#<%=txtStreamMetaDesc.ClientID %>").val('');
            $("#<%=txtCourseStreamHistory.ClientID %>").val('');
            $("#<%=txtCourseStreamFuture.ClientID %>").val('');
            $("#<%=txtCourseStreamCoreCompanies.ClientID %>").val('');
            $("#<%=txtCourseStreamName.ClientID %>").val('');
            $("#<%=txtCourseStreamRelatedIndustry.ClientID %>").val('');
            $("#<%=chkbCourseStreamStatus.ClientID %>").removeAttr('checked');
            $("#<%=ddlCourseId.ClientID %>").val(0);
        }
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="AddCourse.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Course.AddCourse" %>
<%@ Register Src="../../UserControl/FckEditorCostomize.ascx" TagName="FckEditorCostomize"
    TagPrefix="AJ" %>
  <%@ Register Src="~/UserControl/AjaxFileUpload.ascx" TagName="FileUploader" TagPrefix="Aj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
 
   <asp:HiddenField ID="hdnCourseId" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hdnCourseUrl" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hdnCourseMetaTag" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hdnCourseTitle" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hdnCourseMetaDesc" runat="server"></asp:HiddenField>
                  <asp:Label ID="lblSuccess" runat="server" Text="" Visible="false" CssClass="success">
                </asp:Label>
                 <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="error">
                </asp:Label>
    <ul class="addPage_utility">
        <li class="fright" style="width: 142px !important;">
            <div class="navbar-inner">
                <a class="viewIco" href="CourseMaster.aspx">Course Master</a>
                <div class="clear">
                </div>
            </div>
        </li>
        <li class="fright" style="width: 72px !important;">
            <asp:ImageButton ID="btnUpload" runat="server" ImageUrl="~/AdminPanel/Images/CommonImages/uploadExcel.png" ToolTip="Upload Excel"  />
            <asp:ImageButton ID="btnSeeExcelFormat" runat="server" OnClick="BtnUploadClick" ImageUrl="~/AdminPanel/Images/CommonImages/downloadExcel2.png" ToolTip="Download Format" />
        </li>
    </ul>


    <fieldset>
        <legend>
            <asp:Label runat="server" Text="Add Course" ID="lblInsertUpdate" /></legend>
        <ul>
       <li >
                <label>
                    Course Name</label>
                <asp:TextBox ID="txtCourseName" runat="server" TabIndex="1" CssClass="autocomplete" style="background:none !important; text-indent:5px !important;" Width="69.5%"  ToolTip="Please Enter Course Name" onkeyup="keyup(this)"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvCourseName" ControlToValidate="txtCourseName" ValidationGroup="course" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
            </li>
          <li class="width48perc fleft">
                <label>
                    Popular Name</label>
                <asp:TextBox ID="txtPopularName" runat="server" TabIndex="2" ToolTip="Please Select Popular Name"></asp:TextBox>
            </li>
            <li class="width48perc fleft">
                <label>
                    Short Name</label>
                <asp:TextBox ID="txtShortName" runat="server" TabIndex="3" ToolTip="Please Select Course Short Name"></asp:TextBox>
            </li>
             <li class="width48perc fleft">
                <label>
                    Helpline No</label>
                <asp:TextBox ID="txtHelplineNo" runat="server" TabIndex="4" ToolTip="Please Enter Helpline No"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvHelplineNo" ControlToValidate="txtHelplineNo" ValidationGroup="course" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
            </li>
           <li class="width48perc fleft">
                <label>
                    Course Category</label>
                <asp:DropDownList ID="ddlCourseCategory" runat="server" TabIndex="5" ToolTip="Please Select Course Category" onfocus="ClearLabel()">
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ID="rfvCourseCategory" ControlToValidate="ddlCourseCategory" InitialValue="0" ValidationGroup="course" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
            </li>
            <li class="width48perc fleft">
                <label>
                    Course Eligibilty</label>
                <asp:DropDownList ID="ddlCourseEligibility" runat="server" TabIndex="6" ToolTip="Please Select Course Eligibilty">
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ID="rfvCourseEligibilty" ControlToValidate="ddlCourseEligibility" InitialValue="0" ValidationGroup="course" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
            </li>
            <li class="width48perc fleft">
                <label>
                    CourseImage:
                </label>
                <asp:HiddenField ID="hdnFileName" runat="server" />
                <Aj:FileUploader ID="impUploader" TabIndex="9" runat="server" uploadToDirectory="../../Image/Course/" />
            </li>
            
           
            
            
            <li class="width48perc fleft">
                <label>
                    Display</label>
                <asp:CheckBox ID="chkCourseStatus" runat="server" TabIndex="7" ToolTip="Please Select Course Status"></asp:CheckBox>
            </li>
            <li class="width48perc fleft">
                <label>
                    IsBookSeatVisible</label>
                <asp:CheckBox ID="ckbIsBookSeatVisible" runat="server" TabIndex="8" ToolTip="Please Select IsBookSeatVisible"></asp:CheckBox>
            </li>
           
            <li>
                <label>
                    Description</label><span class="fleft" style="margin: 3px 5px;">
                        <Aj:fckeditorcostomize ID="fckCourseDecsription" TabIndex="10" runat="server" /></span> </li>
        </ul>
        <h5 style="font-size:17px !important; font-weight:normal !important; border-bottom:1px dashed #e1e1e1; padding:3px 60px !important;">
            SEO Tool</h5>
        <ul>
        <li>
                <label>
                    Course Title</label>
                <asp:TextBox ID="txtCourseTitle" runat="server" TabIndex="12" CssClass="autocomplete" style="background:none !important; text-indent:5px !important;" Width="60%"  ToolTip="Please Enter Course Title" onkeyup="titlekeyup(this,'charTitleLeft')"></asp:TextBox>
                <div id="charTitleLeft">
                </div>
            </li>
        <li>
                <label>
                    Course Url</label>
                <asp:TextBox ID="txtCourseUrl" runat="server" TabIndex="11" CssClass="autocomplete" style="background:none !important; text-indent:5px !important;" Width="60%" ToolTip="Please Enter Course Url" onkeyup="urlkeyup(this,'countCharacter')"></asp:TextBox>
                <%--                  
                --%>
                <div class="charLeft" id="countCharacter">
                </div>
            </li>
            
           <li>
                <label>
                    MetaDesciption</label>
                <asp:TextBox ID="txtCourseMetaDesc" runat="server" TabIndex="13" style="width:59.5%;max-width: 100%;" TextMode="MultiLine" ToolTip="Please Enter Course MetaTagDesc" onkeyup="tagdesckeyup(this,'charMetaDescLeft')"></asp:TextBox>
                <div id="charMetaDescLeft">
                </div>
            </li>
            
           <li>
                <label>
                    Course MetaTag</label>
                <asp:TextBox ID="txtCourseMetaTag" runat="server" TabIndex="14" style="width:59.5%;max-width: 100%;" TextMode="MultiLine" ToolTip="Please Enter Course MetaTag" onkeyup="tagkeyup(this,'charMetaTagLeft')"></asp:TextBox>
                <div id="charMetaTagLeft">
                </div>
            </li>
            
            <li>
                <label>
                    &nbsp;</label>
                <asp:Button ID="btntCourse" runat="server" TabIndex="15" Text="Save" ValidationGroup="course" CausesValidation="True" ToolTip="Please Submit" OnClick="BtnCourseClick" />
                <input id="btnClear" type="button" value="Clear" tabindex="16" title="Please Clear" onclick="ClearFields()" />
            </li>
        </ul>
    </fieldset>
    <ul style="display:none;">
        <li>
            <asp:Label ID="lblExcelSuccess" runat="server" Visible="false"></asp:Label></li>
        <li>
            <label>
                See Excel</label>
        </li>
        <li>
            <label>
                Upload File :</label>
            <asp:FileUpload ID="fulUploadExcel" runat="server" />
            <asp:RequiredFieldValidator ID="rfvExcelUpload" runat="Server" ControlToValidate="fulUploadExcel" ValidationGroup="ExcelUpload" />
            <asp:RegularExpressionValidator ID="revExcelUpload" runat="server" ControlToValidate="fulUploadExcel" ValidationGroup="GrUpload">
            </asp:RegularExpressionValidator>
        </li>
        <li>
            <label>
            </label>
        </li>
    </ul>
 

    <script type="text/javascript">

        function ClearFields() {
            $("#<%=txtCourseName.ClientID %>").val(''); $("#<%=lblInsertUpdate.ClientID %>").text('Insert');
            $("#<%=txtShortName.ClientID %>").val('');
            $("#<%=txtHelplineNo.ClientID %>").val('');
            $("#<%=txtPopularName.ClientID %>").val('');
            $("#<%=txtCourseMetaDesc.ClientID %>").val('');
            $("#<%=txtCourseTitle.ClientID %>").val('');
            $("#<%=txtCourseMetaTag.ClientID %>").val('');
            $("#<%=txtCourseMetaDesc.ClientID %>").val('');
            $("#<%=txtCourseUrl.ClientID %>").val('');
            $("#<%=btntCourse.ClientID %>").attr('value', 'Save');
            $("#<%=chkCourseStatus.ClientID %>").attr('checked', false);
            $("#<%=ddlCourseCategory.ClientID %>").val(0);
            $("#<%=ddlCourseEligibility.ClientID %>").val(0);
            window.scrollTo(0, 0);
        }


        function ClearLabel() {
            document.getElementById('<%=lblSuccess.ClientID %>').style.display = "none";
            document.getElementById('<%=lblError.ClientID %>').style.display = "none";
        }

        function keyup(control) {
            CopyContent(control, $("#<%=txtCourseUrl.ClientID %>"), $("#<%=txtCourseMetaTag.ClientID %>"), $("#<%=txtCourseTitle.ClientID %>"), $("#<%=txtCourseMetaDesc.ClientID %>"));
        }

        function urlkeyup(control, cssName) {
            GetUrlLength(control, cssName, $("#<%=hdnCourseUrl.ClientID %>"));
        }
        function tagkeyup(control, cssName) {
            GetUrlLength(control, cssName, $("#<%=hdnCourseMetaTag.ClientID %>"));
        }
        function tagdesckeyup(control, cssName) {
            GetUrlLength(control, cssName, $("#<%=hdnCourseMetaDesc.ClientID %>"));
        }
        function titlekeyup(control, cssName) {
            GetUrlLength(control, cssName, $("#<%=hdnCourseTitle.ClientID %>"));
        };

        function FocusLabel() {

            window.scrollTo(0, 0);
        }

        </script>
</asp:Content>

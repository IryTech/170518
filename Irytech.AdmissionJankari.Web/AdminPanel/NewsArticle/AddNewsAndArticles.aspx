<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="AddNewsAndArticles.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.NewsArticle.AddNewsAndArticles" %>

<%@ Register Src="../../UserControl/FckEditorCostomize.ascx" TagName="FckEditorCostomize" TagPrefix="AJ" %>
<%@ Register Src="~/UserControl/AjaxFileUpload.ascx" TagName="FileUpload" TagPrefix="Aj" %>
<asp:Content ID="cntAddNewsAndArticles" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel runat="server" ID="upPnlNotice" UpdateMode="conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnNewsUrl" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hdnNewsMetaTag" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hdnNewsTitle" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hdnNewsMetaDesc" runat="server"></asp:HiddenField>
            <ul class="addPage_utility">
                <li class="fright" style="width: 131px !important;">
                    <div class="navbar-inner" style="margin-right: 1%;">
                        <a href="NewsAndArticles.aspx" class="viewIco">News Master</a>
                        <div class="clear">
                        </div>
                    </div>
                </li>
                <li class="fright" style="width: 72px !important;">
                    <asp:ImageButton ID="btnUpload" runat="server" ImageUrl="~/AdminPanel/Images/CommonImages/uploadExcel.png" ToolTip="Upload Excel Format" OnClientClick="colorboxDialogSubmitClicked('ExcelUpload', 'uploadImagePanel'); FocusLabel();" TabIndex="2" OnClick="BtnUploadClick1" />
                    <asp:ImageButton ID="btnSeeExcelFormat" runat="server" ImageUrl="~/AdminPanel/Images/CommonImages/downloadExcel2.png" ToolTip="Download Excel Format" OnClick="BtnUploadClick" />
                </li>
            </ul>
            <fieldset>
                <legend>
                    <asp:Label ID="lblHeader" runat="server"></asp:Label><asp:Label runat="server" Text="" ID="lblInsertUpdate"></asp:Label></legend>
                <ul>
                    <li>
                        <label>
                            Subject</label>
                        <asp:TextBox runat="server" ID="txtNewsSubject" TabIndex="2" CssClass="autocomplete" style="background:none !important; text-indent:5px !important;" Width="60%" placeholder="Enter News Subject" ToolTip="Please Enter News Subject" onkeyup="keyup(this)">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNewsSubject" SetFocusOnError="true" CssClass="forerror" runat="server" Display="Dynamic" ValidationGroup="notice" ControlToValidate="txtNewsSubject">
                        </asp:RequiredFieldValidator>
                    </li>
                    
                    <li class="width48perc fleft">
                        <label>
                            News Date</label>
                        <asp:TextBox runat="server" ID="txtNewsDate" TabIndex="8" ToolTip="Please Enter News Date">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNewsDate" runat="server" SetFocusOnError="true" ValidationGroup="notice" CssClass="forerror" ControlToValidate="txtNewsDate"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label>
                            News By</label>
                        <asp:TextBox runat="server" ID="txtNewsBy" TabIndex="7" ToolTip="Please Enter News Source">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNewsBy" SetFocusOnError="true" CssClass="forerror" runat="server" Display="Dynamic" ValidationGroup="notice" ControlToValidate="txtNewsBy">
                       
                        </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label>
                            Image</label>
                        <asp:HiddenField runat="server" ID="hdnFileName" />
                        <Aj:FileUpload ID="FlUpload" runat="server" />
                        <asp:Image runat="server" ID="imgNews" Width="100px" Height="100px" Visible="False"></asp:Image>
                    </li>
                    <li class="width48perc fleft">
                        <label>
                            Display</label>
                        <asp:CheckBox runat="server" ID="chkStatus" TabIndex="9" ToolTip="Please Check Status"></asp:CheckBox>
                    </li>
                    <li>
                        <label>
                            Short Description</label>
                        <asp:TextBox runat="server" ID="txtShortDesc" TabIndex="10" ToolTip="Please Enter Short Description" style="max-width: 100%;" Width="34%" TextMode="MultiLine">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNewsShortDesc" SetFocusOnError="true" CssClass="forerror" runat="server" Display="Dynamic" ValidationGroup="notice" ControlToValidate="txtShortDesc">
                       
                        </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label>
                            Description</label><span class="fleft" style="margin: 3px 5px;">
                                <Aj:FckEditorCostomize ID="fckNewsDesc" runat="server" /></span> </li>
                   
                    <li>
                        <asp:UpdateProgress AssociatedUpdatePanelID="upPnlNotice" runat="server" ID="updateprogress">
                            <ProgressTemplate>
                                <img src='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot%>image.axd?Common=LoadingImage.gif' alt="Please Wait Updateing" />Please Wait Updateing</ProgressTemplate>
                        </asp:UpdateProgress>
                    </li>
                </ul>
                <h5 style="font-size:17px !important; font-weight:normal !important; border-bottom:1px dashed #e1e1e1; padding:3px 60px !important;">SEO Tool</h5>
                <ul>
                <li>
                        <label>
                            Title</label>
                        <asp:TextBox runat="server" ID="txtNewsTitle" TabIndex="4" CssClass="autocomplete" style="background:none !important; text-indent:5px !important;" Width="60%" ToolTip="Please Enter News Title" onkeyup="titlekeyup(this,'newsTitle')">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNewsTitle" SetFocusOnError="true" CssClass="forerror" runat="server" Display="Dynamic" ValidationGroup="notice" ControlToValidate="txtNewsTitle">
                       
                        </asp:RequiredFieldValidator><div id="newsTitle">
                        </div>
                    </li>
                    <li>
                        <label>
                            Url</label>
                        <asp:TextBox runat="server" ID="txtNewsUrl" TabIndex="3" CssClass="autocomplete" style="background:none !important; text-indent:5px !important;" Width="60%" ToolTip="Please Enter News Url" onkeyup="urlkeyup(this,'newsUrl')">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNewsUrl" SetFocusOnError="true" CssClass="forerror" runat="server" Display="Dynamic" ValidationGroup="notice" ControlToValidate="txtNewsUrl">
                       
                        </asp:RequiredFieldValidator><div id="newsUrl">
                        </div>
                    </li>
                    <li>
                        <label>
                            Meta Desc</label>
                        <asp:TextBox runat="server" ID="txtNewsMetaDesc" TabIndex="6" style="width:59.5%;max-width: 100%;" ToolTip="Please Enter News Meta Desc" TextMode="MultiLine" onkeyup="tagdesckeyup(this,'newsMetaDesc')">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNewsMetaDesc" SetFocusOnError="true" CssClass="forerror" runat="server" Display="Dynamic" ValidationGroup="notice" ControlToValidate="txtNewsMetaDesc">
                       
                        </asp:RequiredFieldValidator><div id="newsMetaDesc">
                        </div>
                    </li>
                    <li>
                        <label>
                            Meta Tag</label>
                        <asp:TextBox runat="server" ID="txtNewsMetaTag" TabIndex="5" style="width:59.5%;max-width: 100%;" TextMode="MultiLine" ToolTip="Please Enter News Meta Tag" onkeyup="tagkeyup(this,'newsTag')">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNewsMetaTag" SetFocusOnError="true" CssClass="forerror" runat="server" Display="Dynamic" ValidationGroup="notice" ControlToValidate="txtNewsMetaTag">
                       
                        </asp:RequiredFieldValidator><div id="newsTag">
                        </div>
                    </li>
                    
                    <li><label>
                        </label>
                        <asp:Button runat="server" Text="Save" ID="btnSave" TabIndex="12" ValidationGroup="notice" ToolTip="Please Submit" OnClick="BtnSaveClick" />
                        <%--<asp:Button runat="server" Text="Cancel" TabIndex="13" ID="btnCancel"  OnClick="ClearFields()"/>--%>
                        <input id="btnCncel" type="button" value="Cancel" onclick="ClearFields()" /></li>
                </ul>
            </fieldset>
            <fieldset style="display:none;">
                <ul>
                    <li>
                        <label>
                            Upload File :</label>
                        <asp:FileUpload ID="fulUploadExcel" runat="server" TabIndex="1" />
                        <asp:RequiredFieldValidator ID="rfvExcelUpload" runat="Server" ControlToValidate="fulUploadExcel" ValidationGroup="ExcelUpload" />
                        <asp:RegularExpressionValidator ID="revExcelUpload" runat="server" ControlToValidate="fulUploadExcel" ValidationGroup="GrUpload">
                        </asp:RegularExpressionValidator>
                    </li>
                </ul>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="../JS/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script src="../JS/CollegeBranch.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ClearFields() {
            $("#<%=txtNewsSubject.ClientID %>").val('');
            $("#<%=txtNewsUrl.ClientID %>").val('');
            $("#<%=txtNewsTitle.ClientID %>").val('');
            $("#<%=txtNewsMetaTag.ClientID %>").val('');
            $("#<%=txtNewsMetaDesc.ClientID %>").val('');
            $("#<%=txtNewsBy.ClientID %>").val('');
            $("#<%=txtNewsDate.ClientID %>").val('');
            $("#<%=txtShortDesc.ClientID %>").val('');
            $("#<%=chkStatus.ClientID %>").attr('checked', false);
            window.scrollTo(0, 0);
        } </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#uploadImage").colorbox({ width: "550px", inline: true, href: "#uploadImagePanel" });
        });

        function closeOverlay() {
            $.colorbox.close();
        }

        function colorboxDialogSubmitClicked(validationGroup, panelId) {

            if (typeof Page_ClientValidate !== 'undefined') {
                if (!Page_ClientValidate(validationGroup)) {
                    return true;
                }
            }
            $.colorbox.close();
            $("form").append($("#" + panelId));
            return true;
        }
        function keyup(control) {
            CopyContent(control, $("#<%=txtNewsUrl.ClientID %>"), $("#<%=txtNewsMetaTag.ClientID %>"), $("#<%=txtNewsTitle.ClientID %>"), $("#<%=txtNewsMetaDesc.ClientID %>"));
        }
        function urlkeyup(control, cssName) {
            GetUrlLength(control, cssName, $("#<%=hdnNewsUrl.ClientID %>"));
        }
        function tagkeyup(control, cssName) {
            GetUrlLength(control, cssName, $("#<%=hdnNewsMetaTag.ClientID %>"));
        }
        function tagdesckeyup(control, cssName) {
            GetUrlLength(control, cssName, $("#<%=hdnNewsMetaDesc.ClientID %>"));
        }
        function titlekeyup(control, cssName) {
            GetUrlLength(control, cssName, $("#<%=hdnNewsTitle.ClientID %>"));
        };
    </script>
</asp:Content>

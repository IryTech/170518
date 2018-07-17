<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="AddNoticeDetails.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.NewsArticle.AddNoticeDetails" %>
<%@ Register src="../../UserControl/FckEditorCostomize.ascx" tagname="FckEditorCostomize" tagprefix="AJ" %>
<%@Register Src="~/UserControl/AjaxFileUpload.ascx" TagName="FileUpload" TagPrefix="Aj" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
   <asp:HiddenField ID="hdnNoticeUrl" runat="server"> </asp:HiddenField>  
        <asp:HiddenField ID="hdnNoticeMetaTag" runat="server"> </asp:HiddenField>  
          <asp:HiddenField ID="hdnNoticeTitle" runat="server"> </asp:HiddenField>  
            <asp:HiddenField ID="hdnNoticeMetaDesc" runat="server"> </asp:HiddenField>  
  
    <ul class="addPage_utility">
        <li class="fright" style="width: 138px !important;">
            <div class="navbar-inner" style="margin-right: 1%;">
                <a href="NewsAndArticles.aspx" class="viewIco">Notice Master</a>
                <div class="clear">
                </div>
            </div>
        </li>
        <li class="fright" style="width: 72px !important;">
            <asp:ImageButton ID="btnUpload" runat="server" ImageUrl="~/AdminPanel/Images/CommonImages/uploadExcel.png" ToolTip="Upload Excel Format" OnClick="BtnUploadExcel" ValidationGroup="GrUpload" TabIndex="2" />
            <asp:ImageButton ID="btnSeeExcelFormat" runat="server" ImageUrl="~/AdminPanel/Images/CommonImages/downloadExcel2.png" ToolTip="Download Excel Format" OnClick="BtnSeeExcel" />
        </li>
    </ul>
  
  
    <asp:UpdatePanel runat="server" ID="upPnlNotice"  >
        <ContentTemplate>
      
    <fieldset>
    <legend><asp:Label ID="lblHeader" runat="server"></asp:Label>
    <asp:Label ID="lblInsertUpdate" runat="server" Text=""></asp:Label>
    </legend>
    <ul><li>
     <label>Notice Subject</label>
            <asp:TextBox runat="server" ID="txtNoticeSubject" TabIndex="1"  CssClass="autocomplete" style="background:none !important; text-indent:5px !important;" Width="60%" placeholder="Enter Notice Subject" ToolTip="Please Enter Subject" onkeyup="keyup(this)">
            </asp:TextBox>
             <asp:RequiredFieldValidator ID="rfvNoticeSubject" SetFocusOnError="true" runat="server"
                        Display="Dynamic" ValidationGroup="notice" ControlToValidate="txtNoticeSubject"
                        ErrorMessage="Please Enter Notice Subject">
                    </asp:RequiredFieldValidator>
                    
        </li>
     <li><label>Notice Related</label>
         <asp:RadioButtonList ID="rbtNoticeType" runat="server" name="NoticeType" RepeatDirection="Horizontal" CssClass="RadioButtonList" ><asp:ListItem Value="0"   Text="Colleges"></asp:ListItem>
         <asp:ListItem Value="1" Text="Individual" Selected="True"></asp:ListItem>
         </asp:RadioButtonList>
     
    </li>
    <li>
    <label>Notice Category</label>
            <asp:DropDownList runat="server" ID="ddlNoticeType" TabIndex="7" ToolTip="Please Select Notice">
                <asp:ListItem Value="0" Text="Please Select" ></asp:ListItem>
                
            </asp:DropDownList>
             <asp:RequiredFieldValidator ID="rfvNoticeType" SetFocusOnError="true" runat="server"
                        Display="Dynamic" ValidationGroup="notice" ControlToValidate="ddlNoticeType" InitialValue="0"
                        ErrorMessage="Please Enter  NoticeType">
                    </asp:RequiredFieldValidator>
        </li>
        
       
        
         <li> 
            <label>Notice Image</label>
                 <asp:HiddenField runat="server" ID="hdnFileName"/>
           
                      <AJ:FileUpload ID="FlUpload" runat="server"   /> 
            <asp:Image runat="server" ID="imgNews" Width="100px" Height="100px" Visible="False"></asp:Image>
                    
         
        </li>
        <li><label>Display</label>
            <asp:CheckBox runat="server" ID="chkNoticeStatus" TabIndex="6" ToolTip="Please Check Status">
            </asp:CheckBox>
        </li>
        <li class="liRelated" id="liRelated" runat="server"><label>Related College</label>
            <asp:DropDownList runat="server" ID="ddlRelatedCollege" TabIndex="7" ToolTip="Please Select College">
              
       </asp:DropDownList>
        </li>
        
         <li><label>Short Desc</label>
            <asp:TextBox runat="server" ID="txtNoticeShortDesc" TabIndex="5" ToolTip="Please Enter MetaDesc" style="max-width: 100%;" Width="34%" TextMode="MultiLine" >
            </asp:TextBox>
           </li>
        <li><label>Notice Desc</label><span class="fleft" style="margin:3px 5px;">
        <AJ:FckEditorCostomize ID="fckNoticeDesc" runat="server" /></span>
        </li>
        
       <li>
        <asp:UpdateProgress AssociatedUpdatePanelID="upPnlNotice"  runat="server" ID="updateprogress">
                <ProgressTemplate>  <img src='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot%>image.axd?Common=LoadingImage.gif'
                     alt="Please Wait Updateing"/>Please Wait Updateing</ProgressTemplate>  
                  </asp:UpdateProgress>
       </li>
    </ul>
     <h5 style="font-size:17px !important; font-weight:normal !important; border-bottom:1px dashed #e1e1e1; padding:3px 60px !important;">SEO Tool</h5>
    <ul><li><label>Notice Tilte</label>
            <asp:TextBox runat="server" ID="txtNoticeTitle" TabIndex="2" CssClass="autocomplete" style="background:none !important; text-indent:5px !important;" Width="60%"  ToolTip="Please Enter Title" onkeyup="titlekeyup(this,'noticeTitle')">
            </asp:TextBox> <div id="noticeTitle" ></div>
             
             <asp:RequiredFieldValidator ID="rfvNoticeTitle" SetFocusOnError="true" runat="server"
                        Display="Dynamic" ValidationGroup="notice" ControlToValidate="txtNoticeTitle"
                        ErrorMessage="Please Enter Notice Title">
                    </asp:RequiredFieldValidator> 
        </li>
        
        <li><label>Notice Url</label>
            <asp:TextBox runat="server" ID="txtNoticeUrl" TabIndex="3" CssClass="autocomplete" style="background:none !important; text-indent:5px !important;" Width="60%"  ToolTip="Please Enter Url" onkeyup="urlkeyup(this,'noticeUrl')">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNoticeUrl" SetFocusOnError="true" runat="server"
                        Display="Dynamic" ValidationGroup="notice" ControlToValidate="txtNoticeUrl"
                        ErrorMessage="Please Enter Notice Url">
                    </asp:RequiredFieldValidator><div id="noticeUrl" ></div>
        </li>
        <li><label>Meta Desc</label>
            <asp:TextBox runat="server" ID="txtMetaDesc" TabIndex="5" style="width:59.5%;max-width: 100%;" TextMode="MultiLine" ToolTip="Please Enter MetaDesc" onkeyup="tagdesckeyup(this,'noticeDesc')">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNoticeDesc" SetFocusOnError="true" runat="server"
                        Display="Dynamic" ValidationGroup="notice" ControlToValidate="txtMetaDesc"
                        ErrorMessage="Please Enter Notice Desc">
                    </asp:RequiredFieldValidator><div id="noticeDesc" ></div>
        </li>
        <li ><label>Meta Tag</label>
            <asp:TextBox runat="server" ID="txtNoticeTag" TabIndex="4"  style="width:59.5%;max-width: 100%;" TextMode="MultiLine" ToolTip="Please Enter Meta Tag" onkeyup="tagkeyup(this,'noticeTag')">
            </asp:TextBox>
             <asp:RequiredFieldValidator ID="rfvNoticeTag" SetFocusOnError="true" runat="server"
                        Display="Dynamic" ValidationGroup="notice" ControlToValidate="txtNoticeTag"
                        ErrorMessage="Please Enter Notice Tag">
                    </asp:RequiredFieldValidator><div id="noticeTag" ></div>
        </li>
        
        <li><label></label>
       <asp:Button runat="server" Text="Save" ID="btnSave" onclick="BtnSaveClick" TabIndex="11" ValidationGroup="notice" ToolTip="Please Submit Details"/>
      <input id="btnCancel" type="button" value="Cancel" onclick="ClearFields()" title="Please Reset" />

       </li>
        </ul>
    </fieldset>   </ContentTemplate>
    </asp:UpdatePanel>

      <fieldset style="display:none;"><ul> 
        <li>
            <label>
                Upload File :</label>
            <asp:FileUpload ID="fulUploadExcel" runat="server" TabIndex="1" />
            <asp:RequiredFieldValidator ID="rfvUpload" runat="server"  ControlToValidate="fulUploadExcel"
                ValidationGroup="GrUpload">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revExcelUpload" runat="server" 
                ControlToValidate="fulUploadExcel" ValidationGroup="GrUpload">
            </asp:RegularExpressionValidator>
        </li>
           
        </ul>
    </fieldset>
    <script src="../JS/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script src="../JS/CollegeBranch.js" type="text/javascript"></script>
   <script type="text/javascript">
       function keyup(control) {
           CopyContent(control, $("#<%=txtNoticeUrl.ClientID %>"), $("#<%=txtNoticeTag.ClientID %>"), $("#<%=txtNoticeTitle.ClientID %>"), $("#<%=txtMetaDesc.ClientID %>"));
       }
       function urlkeyup(control, cssName) {
           GetUrlLength(control, cssName, $("#<%=hdnNoticeUrl.ClientID %>"));
       }
       function tagkeyup(control, cssName) {
           GetUrlLength(control, cssName, $("#<%=hdnNoticeMetaTag.ClientID %>"));
       }
       function tagdesckeyup(control, cssName) {
           GetUrlLength(control, cssName, $("#<%=hdnNoticeMetaDesc.ClientID %>"));
       }
       function titlekeyup(control, cssName) {
           GetUrlLength(control, cssName, $("#<%=hdnNoticeTitle.ClientID %>"));
       };
                </script>

<script type="text/javascript">
    function ClearFields() {
        $("#<%=txtNoticeSubject.ClientID %>").val('');
        $("#<%=txtNoticeTitle.ClientID %>").val('');
        $("#<%=txtNoticeUrl.ClientID %>").val('');
        $("#<%=txtNoticeTag.ClientID %>").val('');
        $("#<%=txtMetaDesc.ClientID %>").val('');
        window.scrollTo(0, 0);
    }
    </script>
<script type="text/javascript">
    $(document).ready(function () {
        var $radChecked;
        var url = location.href.indexOf("?");
        if (url > 0) {
            $(".liRelated").show();
        }
        else {
            $(".liRelated").hide();
        }
        var collegeMgt = $("table.tbl input:radio");
        collegeMgt.click(function () {
            radChecked = $(':radio:checked').val();
            if (radChecked == 1) {
                $(".liRelated").hide(400);
            } else { $(".liRelated").show(400); }
        });
    });
  
</script>


    </asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" EnableViewState="true" CodeBehind="NoticeMaster.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.NewsArticle.NoticeMaster" %>

<%@ Register src="~/UserControl/CustomPaging.ascx" Tagname="CustomPaging" Tagprefix="AJ" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">

<asp:UpdatePanel ID="updNoticeMaster" runat="server">
<ContentTemplate>

<ul class="addPage_utility">
                <li class="fright" style="width: 170px !important;">
                    <div class="navbar-inner" style="margin-right: 1%;">
                        <a href="AddNoticeDetails.aspx" class="insertIco">Add Notice Master</a>
                        <div class="clear">
                        </div>
                    </div>
                </li>
            </ul>



 <asp:Label ID="lblInform" runat="server" Text="" Visible="false" CssClass="info"></asp:Label>
   
   
   
    <fieldset ><legend>Search Notice</legend>
    <ul>
    <li class="list75width"><label>Notice Subject</label>
             <asp:TextBox runat="server" ID="txtNoticeName" CssClass="autocomplete" Width="63%" TabIndex="3" placeholder="Please Enter Notice Subject" ToolTip="Please Enter Notice Name">
             </asp:TextBox>
        </li>

        <li><label>Related College</label>
            <asp:DropDownList runat="server" ID="ddlRelatedCollege" TabIndex="1" ToolTip="Please Select Related College Search">
            </asp:DropDownList>
        <%--<label>Notice Category</label>--%>
             <asp:DropDownList runat="server" ID="ddlNoticeCategory" TabIndex="2" ToolTip="Please Select Notice Category">
             </asp:DropDownList>
        </li>
         
       
        <li><label></label>
            <asp:Button runat="server" Text="Search" ID="BtnSearch" 
                ToolTip="Please Submit To Search" onclick="BtnSearchClick" />
                
            </li>
        </ul>
    
    
        <asp:Repeater ID="rptNoticeDetails" runat="server" >

        <HeaderTemplate>
      <table class="grdView" width="100%"><tr><th>S.NO</th>
              <th>Subject</th><th>Title</th><th>Status</th> <th> Image </th> <th>Url</th><th></th></tr>
        </HeaderTemplate>
    <ItemTemplate>
         <tr>     <td><%#Eval("SrNo") %></td>
                  <td >
                 <%# Eval("NoticeSubject")%>
                    </td>
                    <td>
                 <%# Eval("NoticeTitle")%>
                    </td>
                    <td>
                 <%# Eval("NoticeStatus")%>
                    </td>
                    <td>
                    
                    <img src='<%# String.Format("{0}{1}","/image.axd?Notice=",string.IsNullOrEmpty(Eval("NoticeImage").ToString()) ?"NoImage.jpg":Eval("NoticeImage")) %>'  width="60px" height="60" alt='<%# Eval("NoticeSubject")%>'  title='<%# Eval("NoticeSubject")%>' />
                    
                    </td>

                    <td>
                   <%# Eval("NoticeUrl")%>
                    </td>
                        <td><a href="AddNoticeDetails.aspx?NoticeId=<%# Eval("NoticeId")%>" title="Edit Notice">Edit</a>
                 
                   </td>
           </tr>
    </ItemTemplate>
    <FooterTemplate></table></FooterTemplate>
        </asp:Repeater>
        <AJ:CustomPaging ID="ucCustomPaging" runat="server" />
    </div>
    </fieldset>  <div id="fade"></div>
<asp:Label ID="lblText" runat="server"  Text=""></asp:Label> 
<div id="divImage" class="loading" >
<img src="/image.axd?Common=Loading.gif"  />
 
 </div>    
</ContentTemplate>
</asp:UpdatePanel> 
    <link href="../../Styles/autoCompliteCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Autocomplete.js" type="text/javascript"></script>
<script type="text/javascript" src="../JS/commonscripts.js"></script>
    <script type="text/javascript">
        var noticeUrl = "../../WebServices/CommonWebServices.asmx/GetNoticeDetails";
        BindDropDownCommonForAdminAutoComplete($("#<%=txtNoticeName.ClientID %>"), noticeUrl);

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                BindDropDownCommonForAdminAutoComplete($("#<%=txtNoticeName.ClientID %>"), noticeUrl);
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

<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="CourseStreamMaster.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Course.CourseStreamMaster" %>
    <%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="Aj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">

    <link href="../../Styles/autoCompliteCSS.css" rel="stylesheet" type="text/css" />
<asp:UpdatePanel ID="updCourseStreamMaster" runat="server">
<ContentTemplate>
 <div id="fade"></div>
<asp:Label ID="lblText" runat="server"  Text=""></asp:Label> 
<div id="divImage" class="loading" >
<img src="/image.axd?Common=Loading.gif"  />
 
 </div>    
    <asp:HiddenField runat="server" ID="hdnCourseCategorName">
    </asp:HiddenField>
    <asp:Label ID="lblSuccess" runat="server" Text="" Visible="false" CssClass="success">
    </asp:Label>
    <asp:Label ID="lblInform" runat="server" Text="" Visible="false" CssClass="info">
    </asp:Label>
    <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="error">
    </asp:Label>
     <ul class="addPage_utility">
        <li class="fright" style="width: 123px !important;">
            <div class="navbar-inner">
                <a class="insertIco" href="AddCourseStreamMaster.aspx">Add Stream </a>
                <div class="clear">
                </div>
            </div>
        </li>
        </ul>
       
        <fieldset>
            <legend>Stream Search</legend>
             
                    <ul>
                        <li>
                            <label>
                                Stream Name</label>
                            <asp:TextBox ID="txtStreamName" CssClass="autocomplete" placeholder="Enter Stream Name " Width="63%" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                Course
                            </label>
                            <asp:DropDownList ID="ddlCourseId" runat="server">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label>
                            </label>
                            <asp:Button ID="Button1" runat="server" Text="Search" OnClick="BtnSreachClick" /></li>
                    </ul>
                 
        
        <asp:Repeater ID="rptCourseStreamData" runat="server" >
            
                <HeaderTemplate>
                <table class="grdView">
                <tr>
                <th>S.No</th>
                <th>Course Name</th>
                <th>Course StreamTitle</th>
                <th>Stream Name</th>
                <th>Stream Related Industry</th>
                <th> Status</th>
                <th>Action</th>
                </tr>
           
       </HeaderTemplate>
            <ItemTemplate>
            <tr>
            <td><%# Eval("SrNo")%></td>
            <td><%# Eval("CourseName")%></td>   
            <td><%# Eval("CourseStreamTitle")%></td>
            <td><%# Eval("CourseStreamName")%></td>
            <td><%# Eval("CourseStreamRelatedIndustry")%></td>      
            <td><%# Convert.ToBoolean( Eval("CourseStreamStatus"))%></td>
            <td><a  href='AddCourseStreamMaster.aspx?StreamId=<%# Eval("StreamId")%>' title="Edit Stream" >
                <img src="../Images/CommonImages/editIcon.png" class="editIconmargin" alt="Edit" title="Edit" width="12px" /></a></td>
            </tr>

     </ItemTemplate>
     <FooterTemplate>                       
                            </table></FooterTemplate>
          
 
        </asp:Repeater>
        <Aj:CustomPaging ID="ucCustomPaging" runat="server" />
         
    </fieldset>

    

</ContentTemplate>
</asp:UpdatePanel>
    

    <script src="../../Scripts/Autocomplete.js" type="text/javascript"></script>
    <script src="../JS/commonscripts.js" type="text/javascript"></script>
    <script type="text/javascript">
        var streamUrl="../../WebServices/CommonWebServices.asmx/GetStreamList";
        BindDropDownCommonForAdminAutoComplete($("#<%=txtStreamName.ClientID %>"), streamUrl);

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
                BindDropDownCommonForAdminAutoComplete($("#<%=txtStreamName.ClientID %>"), streamUrl);
            }
        }

    </script>
</asp:Content>

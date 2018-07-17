<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="AddCollegeGallery.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.AddCollegeGallery" %>

<%@ Register TagPrefix="Aj" TagName="CustomPaging" Src="~/UserControl/CustomPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:HiddenField ID="hdnImage" runat="server" />
   
    <ul class="addPage_utility">

    <li class="fright" style="width: 181px !important;">
        <div class="navbar-inner">
            <a href="CollegeGallery.aspx" id='sndAddUserType' class="insertIco">View College Gallery</a>
            <div class="clear">
            </div>
        </div>
    </li>
     

</ul>
   
   
   
   
    <fieldset>
        <legend>Insert The College Gallery</legend>
        
        
        
        
        
        <ul class="options-bar">
            
                <li>
                        <label>
                            College</label>
                        <asp:TextBox ID="txtCollegeSearch" CssClass="autocomplete" Width="63%" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfvCollegeName" ErrorMessage="Enter college name"
                            Style="margin-left: 160px;" ControlToValidate="txtCollegeSearch" Display="Dynamic"
                            SetFocusOnError="True" CssClass="error1" ValidationGroup="collegegallery"></asp:RequiredFieldValidator>
                    </li>
                  
                     <li >
                        <label>
                            Course</label>
                        <asp:DropDownList ID="ddlCourseList" runat="server" ToolTip="Please Select Course">
                        </asp:DropDownList>
                    <%--</li>
                     <li class="width48perc fleft" ><label>
                          College Type</label>--%>
                        <asp:DropDownList ID="rbtSponser" runat="server" ToolTip="Please Select college Type">
                         <asp:ListItem Value="0" Selected="True" Text="Un Sponsered"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Sponsered"></asp:ListItem>
                        </asp:DropDownList>
                   
                    </li>
                                     
                    <li>
                        <label>
                            Gallery Image:
                        </label>
                        <asp:FileUpload ID="file_upload" class="multi" runat="server" />
                        <asp:Image runat="server" CssClass="uploadImage" Visible="false" ID="imgGallery">
                        </asp:Image>
                    </li>
                    <li>
                        <label>
                            Image Title:
                        </label>
                        <asp:TextBox ID="txtImageTitle" TextMode="MultiLine" Style="max-width: 450px !important;"
                            runat="server">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfvCollegeTitle" ErrorMessage="Enter image title"
                            Style="margin-left: 160px;" ControlToValidate="txtImageTitle" Display="Dynamic"
                            SetFocusOnError="True" CssClass="error1" ValidationGroup="collegegallery">
                    
                        </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label>
                            Display:
                        </label>
                        <asp:CheckBox ID="chkStatus" runat="server" />
                    </li>
                    <li>
                        <label>
                            &nbsp;
                        </label>
                        <asp:Button ID="btnUpload" runat="server" Text="Insert" ValidationGroup="collegegallery"
                            OnClick="BtnUploadClick" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="BtnResetClck" />
                        <asp:Label ID="lblSeccessMsg" runat="server" Visible="false"></asp:Label>
                 
            </li>
        </ul>
    </fieldset>
    <script src="../../Scripts/Autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript">

        function GetParticipatedCollege(course, sponserStatus) {
            var stat = false;
            if (sponserStatus == 1) {
                stat = true;
            } else { stat = false; }
            var url = "../../WebServices/CommonWebServices.asmx/GetSponseredOrNonSponseredCollege";
            var dataQuery = '{"course":"' + course + '","sponserStatus":"' + stat + '"}';
            CommonAutoComplete($("#<%=txtCollegeSearch.ClientID %>"), $("#<%=txtCollegeSearch.ClientID %>"), url, dataQuery);
        }
        $("#<%=ddlCourseList.ClientID %>").change(function () {
            GetCollege();

        });
        $("#<%=rbtSponser.ClientID %>").change(function () {
            GetCollege();

        });
        function GetCollege() {
            var dataQuery = "";
            var sponser = $('#<%=rbtSponser.ClientID %>').val();
            var course = $('#<%=ddlCourseList.ClientID %>').val();
            var url;
            if (sponser.trim() == "1") {
                $(".ac_results").html("");
                dataQuery = '{"course":"' + course + '","sponserStatus":"' + true + '"}';
                url = "../../WebServices/CommonWebServices.asmx/GetSponseredOrNonSponseredCollege";
                CommonAutoComplete($("#<%=txtCollegeSearch.ClientID %>"), $("#<%=txtCollegeSearch.ClientID %>"), url, dataQuery);

            } else {
                $(".ac_results").html("");

                dataQuery = '{"course":"' + course + '","sponserStatus":"' + false + '"}';
                url = "../../WebServices/CommonWebServices.asmx/GetSponseredOrNonSponseredCollege";
                CommonAutoComplete($("#<%=txtCollegeSearch.ClientID %>"), $("#<%=txtCollegeSearch.ClientID %>"), url, dataQuery);
            }
        }
    </script>
</asp:Content>

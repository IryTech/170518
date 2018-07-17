<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="CollegeGallery.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.CollegeGallery" %>

<%@ Register Src="~/UserControl/AjaxFileUpload.ascx" TagName="FileUploader" TagPrefix="Aj" %>
<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="Aj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UpdCollegeGallery" runat="server">
        <ContentTemplate>
            <ul class="addPage_utility">
                <li class="fright" style="width: 178px !important;">
                    <div class="navbar-inner">
                        <a href="AddCollegeGallery.aspx" id='sndAddUserType' class="insertIco">Add College Gallery</a>
                        <div class="clear">
                        </div>
                    </div>
                </li>
            </ul>
            <fieldset>
                <legend>Search Image Gallery </legend>
                <asp:Label runat="server" Text="" ID="lblRecordsInserted"></asp:Label>
                <asp:Label ID="lblErorrMsg" CssClass="error" runat="server" Visible="false"></asp:Label>
                <ul class="options-bar">
                    <li>
                        <label>
                            College Gallery
                        </label>
                        <asp:TextBox ID="txtCollegeData" CssClass="autocomplete" placeholder="Enter College Gallery"
                            Width="63%" runat="server"></asp:TextBox>
                        <asp:Button ID="btnSearch" Text="Search" CssClass="searchbtn" runat="server" OnClick="btnSearch_Click" />
                    </li>
                </ul>
                <ul>
                    <li>
                        <asp:Repeater ID="rptCollegeList" runat="server">
                            <HeaderTemplate>
                                <table class="grdView">
                                    <tr>
                                        <th>
                                            S.No
                                        </th>
                                        <th>
                                            CollegeName
                                        </th>
                                        <th>
                                            Image Count
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#Eval("SrNo") %>
                                    </td>
                                    <td>
                                        <%# Eval("AjCollegeBranchName")%>
                                    </td>
                                    <td>
                                        <a href="#" title="Click to View Details" id="viewImage" onclick='BindCollegeGallery(<%# Eval("AjCollegeBranchId")%>)'>
                                            <%# Eval("TotalCount")%>
                                        </a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table></FooterTemplate>
                        </asp:Repeater>
                        <Aj:CustomPaging ID="ucCustomPaging" runat="server" />
                    </li>
                </ul>
            </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divCourseCategoryInsert" class="popup_block width43perc">
        <fieldset id="basicInfo" style="max-height: 400px; overflow-y: scroll; overflow-x: hidden;">
            <legend><span id="spnCollegeName"></span></legend>
            <div>
                <table class="grdView">
                    <tr>
                        <th>
                            Image
                        </th>
                        <th>
                            Image Title
                        </th>
                        <th>
                            Image Status
                        </th>
                        <th>
                            Edit
                        </th>
                    </tr>
                    <tbody id="imageData">
                    </tbody>
                </table>
            </div>
        </fieldset>
    </div>
    <script src="../../Scripts/Autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript">
        BindDropDownCommonForAdminAutoComplete($("#<%=txtCollegeData.ClientID %>"), "../../WebServices/CommonWebServices.asmx/GetCollegeGalleryList");

        function BindCollegeGallery(collegeBranchId) {

            GetCollegeImageGallery(collegeBranchId);

        }

        function GetCollegeImageGallery(collegeId) {
            $.ajax({
                type: "POST",
                url: "../../WebServices/CommonWebServices.asmx/GetCollegeImageGalleryByCollegeBranchId",
                data: '{"collegeBranchId":"' + collegeId + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: true,
                async: true,
                success: function (msg) {

                    BindImageGallery(msg);

                },
                error: function (xml, textStatus, errorThrown) {

                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        }

        function BindImageGallery(response) {

            $('#imageData').html('');
            var data = "";
            $.each(response.d, function (i, client) {

                $("#spnCollegeName").text("Image Gallery Of " + client.CollegeBranchName);
                data = data + "<tr><td>" + "<img class='uploadImage' alt='" + client.CollegeBranchName + "' src='/image.axd?CollegeGallery=" + client.CollegeBranchGalleryImageName + "' height='80px' width='70px' /><td>" + client.CollegeBranchGalleryImageTitle + "</td><td>" + client.CollegeBranchGalleryImageStatus + "</td><td><a rel='canonical' href=/AdminPanel/College/AddCollegeGallery.aspx?collegeGalleryId=" + client.CollegeBranchGalleryId + " id='participate' class='rightImglink borderbtn' title='" + client.CollegeBranchName + "'>Edit</a></td> </tr>"
            });


            $('#imageData').append(data);
            OpenPoup('divCourseCategoryInsert', '750px', 'viewImage')
        }

    </script>
</asp:Content>

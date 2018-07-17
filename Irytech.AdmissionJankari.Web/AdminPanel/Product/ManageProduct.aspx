<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="ManageProduct.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Product.ManageProduct" %>

<%@ Import Namespace="IryTech.AdmissionJankari.BL" %>
<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="divImage" class="loading">
                <img src="/image.axd?Common=Loading.gif" alt="Loading" title="Loading" />
            </div>
            <ul class="addPage_utility">
                <li class="fright" style="width: 160px !important;">
                    <div class="navbar-inner">
                        <a href="AddAdsProduct.aspx" class="viewIco">Add Product</a>
                        <div class="clear">
                        </div>
                    </div>
                </li>
            </ul>
            <fieldset>
                <legend>Search</legend>
                <ul class="options-bar">
                    <li>
                        <label>
                            Product:</label>
                        <asp:TextBox runat="server" CssClass="autocomplete" TabIndex="1" ID="txtProductSearch"
                            Width="63%"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="searchbtn " TabIndex="2"
                            OnClick="btnSearch_Click"></asp:Button>
                    </li>
                </ul>
            </fieldset>
            <asp:Repeater ID="rptProduct" runat="server" OnItemCommand="rptProduct_ItemCommand">
                <HeaderTemplate>
                    <table class="grdView">
                        <tr>
                            <th>
                                Product Name
                            </th>
                            <th>
                                Amount
                            </th>
                                                      
                            <th>
                                Priority
                            </th>
                            <th>
                            Total Duration
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
                            <asp:HiddenField ID="hdnProductMasterId" Value='<%# Eval("AjProductMasterId")%>'
                                runat="server"></asp:HiddenField>
                            <a title="Go to details of <%# Eval("AjProductName")%>" href="/adminpanel/product/productdetails.aspx?productid=<%# Eval("AjProductMasterId")%>">
                                <%# Eval("AjProductName")%></a>
                        </td>
                        <td>
                            <%# Common.ConvertRupee(Convert.ToString(Eval("AjProductAmount")))%>
                        </td>
                                               
                        <td>
                            <%# Eval("AjProductDisplayPriority")%>
                        </td>
                        <td>
                            <%# Eval("AjProductStatus")%>
                        </td>
                        <td>
                         <a href="#" id="lnkViewdiscount"  onclick="GetAdvstDiscount(<%# Eval("AjProductMasterId")%>);return false;" >   <%# GetDurationNumer(Eval("AjProductMasterId")) %></a>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkUpdate" Text="Update" runat="server" CommandName="Update"
                                CommandArgument='<%# Eval("AjProductMasterId")%>'></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
            <AJ:CustomPaging ID="ucCustomPaging" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divAdvstDiscount" class="popup_block width43perc">
        <table class="grdView">
            <tr>
                <th>
                  Duration
                </th>
                <th>
                    Discount
                </th>
                <th>
                  Validity Start Date
                </th>
                 <th>
                  Validity End Date
                </th>
                <th>
                  Default Selection
                </th>
                <th>
                    Status
                </th>
            </tr>
            <tbody id="imageData">
            </tbody>
        </table>
    </div>
    <script src="../../Scripts/Autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {

                div.style.display = "inline";
                img.src = "/image.axd?Common=minus.png";
                img.alt = "-";


            } else {

                div.style.display = "none";
                img.src = "/image.axd?Common=plus.png";
                img.alt = "+";
            }
        }

        var productUrl = "../../WebServices/CommonWebServices.asmx/GetProductNameList";
        BindDropDownCommonForAdminAutoComplete($("#<%=txtProductSearch.ClientID %>"), productUrl);

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                BindDropDownCommonForAdminAutoComplete($("#<%=txtProductSearch.ClientID %>"), productUrl);
            }
        }

        $("#<%=txtProductSearch.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=btnSearch.ClientID %>").click();
            }
        });
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
        function GetAdvstDiscount(advstTypeId) {
         
            var queryId = 1;
            var json = "{'advstTypes':" + queryId + ",'advstTypeIds':" + advstTypeId + "}";
                    
            $.ajax({
                type: "POST",
                url: "../../WebServices/CommonWebServices.asmx/GetDetailsOfAdvertimentDiscount",
                data: json,
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                      BindAdvstType(response);
                },
                error: function (xml, textStatus, errorThrown) {

                    alert(errorThrown);
                }
            });
        }

        function BindAdvstType(response) {
            $('#imageData').html('');
            var data = "";
            var parsed = $.parseJSON(response.d);
            $.each(parsed, function (i, client) {
                data = data + "<tr><td>" + client.AjMonthValue + "</td><td>" + client.AjDiscount + "</td><td>" +DateFormate(client.AjDiscountValidityStart) + "</td><td>" + DateFormate(client.AjDiscountValidityEnd) + "</td><td>" + client.AjDefaultSelection + "</td><td>" + client.AjDiscountStatus + "</td> </tr>"
            });
           
            $('#imageData').append(data);
            OpenPoup('divAdvstDiscount', '800', 'lnkViewdiscount')
        }
        function DateFormate(dateString) {
            dateString = dateString.substr(6);
            var currentTime = new Date(parseInt(dateString));
            var month = currentTime.getMonth() + 1;
            var day = currentTime.getDate();
            var year = currentTime.getFullYear();
            return day + "/" + month + "/" + year;

        };
    </script>
</asp:Content>

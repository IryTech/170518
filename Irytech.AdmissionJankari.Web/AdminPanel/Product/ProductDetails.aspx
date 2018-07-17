<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Product.ProductDetails" %>

<%@ Import Namespace="IryTech.AdmissionJankari.BL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<asp:Label ID="lblInsertUpdate" runat="server"></asp:Label>
    <ul class="addPage_utility">
        <li class="fright" style="width: 160px !important;">
            <div class="navbar-inner">
                <a href="ManageProduct.aspx" class="viewIco">Manage Product</a>
                <div class="clear">
                </div>
            </div>
        </li>
        <li class="fright" style="width: 160px !important;">
            <div class="navbar-inner">
                <a href="AddAdsProduct.aspx" class="viewIco">Add Product</a>
                <div class="clear">
                </div>
            </div>
        </li>
    </ul>
    <div>
        <span class="fright">
            <asp:LinkButton ID="lnkUpdate" runat="server" OnClick="lnkUpdate_Click">
                        <img src="../Images/CommonImages/editIcon.png"  id="imgEdit" alt="Edit-Icon" class="editIconmargin" width="20px" />
            </asp:LinkButton>
        </span>
        <h1 id="hDetails" runat="server">
        </h1>
    </div>
    <div class="accordion1">
        <h2 class="accord1">
            Overview
        </h2>
        <div>
            <asp:Repeater ID="rptProduct" runat="server">
                <HeaderTemplate>
                    <table class="grdView">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <strong>ProductName-:</strong>
                            <%# Eval("AjProductName")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Amount-:</strong>
                            <%# Common.ConvertRupee(Convert.ToString(Eval("AjProductAmount")))%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Is Best Product-:</strong>
                            <%# Eval("AjIsBestValue")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Priority-:</strong>
                            <%# Eval("AjProductDisplayPriority")%>
                        </td>
                        <td>
                            <strong>Display Status-:</strong>
                            <%# Eval("AjProductStatus")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div class="accordion1">
        <h2 class="accord1" runat="server" id="spnProductCount">
        </h2>
        <div>
            <asp:Repeater ID="rptProductCategory" runat="server">
                <HeaderTemplate>
                    <table class="grdView">
                        <tr>
                            <th>
                                Product Category
                            </th>
                            <th>
                                Amount
                            </th>
                            <th>
                                DisplayStatus
                            </th>
                            <th>
                                IsBanner?
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Eval("Category")%>
                        </td>
                        <td>
                            <%# Eval("Amount")%>
                        </td>
                        <td>
                            <%# Eval("AjProductAdStatus")%>
                        </td>
                        <td>
                            <%# Eval("AjProductIsDisplayBanner")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
        <div class="accordion1">
        <h2 class="accord1">
        Product Discount and Duration
        </h2>
        <div>
               <asp:Repeater ID="rptProductDiscount" runat="server">
                    <HeaderTemplate>
                        <table class="grdView">
                            <tr>
                                <th>
                                    S.No
                                </th>
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
                                <th>
                                    Action
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Container.ItemIndex+1 %>
                            </td>
                            <td>
                                <%# Eval("AjMonthValue")%>
                            </td>
                            <td>
                                  <%# Eval("AjDiscount")%>
                            </td>
                            <td>
                                <%# Convert.ToDateTime(Eval("AjDiscountValidityStart")).ToString("dd/MM/yyyy")%>
                            </td>
                            <td>
                               <%# Convert.ToDateTime(Eval("AjDiscountValidityEnd")).ToString("dd/MM/yyyy")%>
                            </td>
                            <td>
                                <%# Eval("AjDefaultSelection")%>
                            </td>
                            <td>
                                <%# Eval("AjDiscountStatus")%>
                            </td>
                            <td>
                            <a href="#" id="lnkDiscount" onclick="GetDiscountDetails(<%# Eval("AjAdvertismentDiscountId")%>);return false;" > <img alt="Edit the discount Details" src="/AdminPanel/Images/CommonImages/editIcon.png" /> </a>
                               
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table></FooterTemplate>
                </asp:Repeater>
        </div>
    </div>
    <div class="accordion1">
        <h2 class="accord1">
            Description</h2>
        <div>
            <span runat="server" id="spnDescription" style="font-size: 14px!important; color: navy">
            </span>
        </div>
    </div>
    <div class="accordion1">
        <h2 class="accord1" id="spnBenefits" runat="server">
        </h2>
        <div>
            <asp:Repeater ID="rptFeatures" runat="server">
                <HeaderTemplate>
                    <table class="grdView">
                        <tr>
                            <th>
                                Product Features
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Eval("AjProductFeatures")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
      <div id="ProductDuration" class="popup_block width43perc">
        <fieldset id="basicInfo">
            <legend> Product discount and duration  </legend>
            <ul>
                <li>
                    <label>
                        Product Duration
                    </label>
                    <asp:TextBox ID="txtProductDuration" runat="server" ClientIDMode="Static" TabIndex="3" ToolTip="Please Enter Product Duration"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvProductDuration" runat="server" ValidationGroup="AddProductDuration"
                        ControlToValidate="txtProductDuration" CssClass="error1" ErrorMessage="Field Product Duration"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revProductDuration" CssClass="error1" SetFocusOnError="True"
                        ValidationGroup="AddProductDuration" ControlToValidate="txtProductDuration" ValidationExpression="\d+"
                        Display="Dynamic" ErrorMessage="Please enter numbers only" runat="server" />
                </li>
                <li>
                    <label>
                        Product Discount ( % )</label>
                    <asp:TextBox ID="txtProDiscount" runat="server" ClientIDMode="Static" TabIndex="3" ToolTip="Please Enter Product Discount"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="rgDiscount" CssClass="error1" SetFocusOnError="True"
                        ValidationGroup="AddProductDuration" ControlToValidate="txtProDiscount" ValidationExpression="\d+"
                        Display="Dynamic" ErrorMessage="Please enter numbers only" runat="server" />
                </li>
                <li>
                    <label>
                        Validity Start Date
                    </label>
                    <asp:TextBox ID="txtValidityStartTime" runat="server" ClientIDMode="Static" TabIndex="4" ToolTip="Please Enter Product Validity Start Time"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rvStartTime" Display="Dynamic" ControlToValidate="txtValidityStartTime"
                        CssClass="error1" SetFocusOnError="True" ErrorMessage="Please  enter product validity start time"
                        ValidationGroup="AddProductDuration">
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        Validity End Date</label>
                    <asp:TextBox ID="txtProEndTime" runat="server" ClientIDMode="Static" TabIndex="5" ToolTip="Please Enter Product Validity End Time"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rvEndTime" Display="Dynamic" ControlToValidate="txtProEndTime"
                        CssClass="error1" SetFocusOnError="True" ErrorMessage="Please  enter product validity end time"
                        ValidationGroup="AddProductDuration">
                    </asp:RequiredFieldValidator>
                </li>
                 <li>
                    <label>
                        Is Default Selection</label>
                        <asp:CheckBox ID="chkDefaultSelection" ClientIDMode="Static" runat="server" />
                </li>
                <li>
                    <label>
                        Publish</label>
                        <asp:CheckBox ID="chkDiscountStatus" ClientIDMode="Static" runat="server" />
                </li>
                <li>
                    <label>
                        &nbsp;</label>
                    <asp:Button runat="server" Text="Save" ID="btnSave"  ClientIDMode="Static" TabIndex="4" ValidationGroup="AddProductDuration"
                        ToolTip="Please Submit" onclick="btnSave_Click"  />
                    
                </li>
            </ul>
        </fieldset>
    </div>
     <asp:HiddenField ID="hndDiscountId" ClientIDMode="Static" runat="server" />
     <link href="../StyleSheets/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />
    <script src="../JS/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="../JS/jquery.ui.core.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#<%=txtValidityStartTime.ClientID %>").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $("#<%=txtProEndTime.ClientID %>").datepicker({
                changeMonth: true,
                changeYear: true
            });
          
        });
        $(document).ready(function () {
            $(".accordion1 div:not(:first)").hide();
            $(".accordion1 h2").click(function () {
                $(this).next("div").removeClass("hide");
                $(this).next("div").slideToggle("fast")
                    .siblings("div:visible").slideUp("fast");
                $(this).toggleClass("active");
                $(this).siblings("h2").removeClass("active");
            });
        });
        function GetDiscountDetails(advstDiscountId) {
            var json = "{'advstDiscountId':" + advstDiscountId + "}";

            $.ajax({
                type: "POST",
                url: "../../WebServices/CommonWebServices.asmx/GetDetailsOfAdvertimentDiscountByDiscountId",
                data: json,
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $("#hndDiscountId").val(advstDiscountId);
                    BindAdvstDetails(response.d);
                },
                error: function (xml, textStatus, errorThrown) {

                    alert(errorThrown);
                }
            });
        }

        function BindAdvstDetails(list) {
            list = $.parseJSON(list);
            if (list != null) {
                if (list.length > 0) {
                    $.each(list, function () {
                        $("#txtProductDuration").val(this['AjMonthValue']);
                        $("#txtProDiscount").val(this['AjDiscount']);
                        $("#txtValidityStartTime").val(DateFormate(this['AjDiscountValidityStart']));
                        $("#txtProEndTime").val(DateFormate(this['AjDiscountValidityEnd']));
                        $('#chkDiscountStatus').attr('checked', this['AjDiscountStatus']);
                        $('#chkDefaultSelection').attr('checked', this['AjDefaultSelection']);
                        $("#btnSave").attr('value', 'Update');
                        OpenPoup('ProductDuration', '650px', 'lnkDiscount')
                    });
                } else {
                    alert('No Record found for the course');
                }
            } else {
                alert('No Record found for the course');
            }
        }
        function DateFormate(dateString) {
            dateString = dateString.substr(6);
            var currentTime = new Date(parseInt(dateString));
            var month = currentTime.getMonth() + 1;
            var day = currentTime.getDate();
            var year = currentTime.getFullYear();
            return month + "/" + day + "/" + year;

        };
    </script>
</asp:Content>

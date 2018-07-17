<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="AddAdsProduct.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Product.AddAdsProduct" %>

<%@ Register Src="../../UserControl/FckEditorCostomize.ascx" TagName="FckEditorCostomize"
    TagPrefix="AJ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<asp:UpdatePanel ID="updateDiscount" runat="server">
    <ContentTemplate>
    <ul class="addPage_utility">
        <li class="fright" style="width: 160px !important;">
            <div class="navbar-inner">
                <a href="ManageProduct.aspx" class="viewIco">Product Master</a>
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
    <div class="grdOuterDiv">
        <fieldset>
            <legend>
                <asp:Label ID="lblHeader" runat="server">
                    
                </asp:Label>
                <asp:Label ID="lblInsertUpdate" runat="server" Text="">
                    
                </asp:Label>
            </legend>
            <fieldset>
                <legend>Product Basic Details </legend>
                <ul class="width48perc fleft">
                    <li>
                        <label>
                            Product Name</label>
                        <asp:TextBox ID="txtProductName" runat="server" TabIndex="1" ToolTip="Please Enter Product Name"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rvProductName" Display="Dynamic" ControlToValidate="txtProductName"
                            CssClass="error1" SetFocusOnError="True" ErrorMessage="Please  enter product name"
                            ValidationGroup="AdsProduct">
                        </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label>
                            Product Prioirty</label>
                        <asp:TextBox ID="txtProPriority" runat="server" TabIndex="2" ToolTip="Please Enter Product Prioirty"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rvProPriority" Display="Dynamic" ControlToValidate="txtProPriority"
                            CssClass="error1" SetFocusOnError="True" ErrorMessage="Please  enter product priority"
                            ValidationGroup="AdsProduct">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rgPriority" CssClass="error1" SetFocusOnError="True"
                            ValidationGroup="AdsProduct" ControlToValidate="txtProPriority" ValidationExpression="\d+"
                            Display="Dynamic" ErrorMessage="Please enter numbers only" runat="server" />
                    </li>
                    <li style="width: 100% !important;">
                        <label>
                            Product Description
                        </label>
                        <div class="fleft" style="margin: 3px 5px;">
                            <AJ:FckEditorCostomize ID="fckProductDesc" tabindex="8" runat="server" />
                        </div>
                    </li>
                    <li>
                        <label for="<%=txtFeatures1.ClientID %>">
                            Features</label>
                        <asp:TextBox runat="server" ID="txtFeatures1" TextMode="MultiLine" TabIndex="9" ToolTip="Please enter features"
                            placeholder="Enter features" />
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Display="Dynamic"
                            ControlToValidate="txtFeatures1" CssClass="error1" SetFocusOnError="True" ErrorMessage="Please  enter product feature"
                            ValidationGroup="AdsProduct">
                        </asp:RequiredFieldValidator>
                        <br />
                        <a href="javascript:;" onclick="ShowAment(2)" id="addMore" runat="server" visible="True">
                            Add more</a> </li>
                    <li id="liFeatures2" style="display: none">
                        <label for="<%=txtFeatures2.ClientID %>">
                            Features</label>
                        <asp:TextBox runat="server" ID="txtFeatures2" TextMode="MultiLine" TabIndex="10"
                            ToolTip="Please enter features" placeholder="Enter features" />
                        <a href="javascript:;" onclick="ShowAment(3)">Add more</a>&nbsp;&nbsp; <a href="javascript:;"
                            onclick="HideAmenty(liFeatures2,<%=txtFeatures2.ClientID %>)">Hide </a></li>
                    <li id="liFeatures3" style="display: none">
                        <label for="<%=txtFeatures2.ClientID %>">
                            Features</label>
                        <asp:TextBox runat="server" ID="txtFeatures3" TextMode="MultiLine" TabIndex="11"
                            ToolTip="Please enter features" placeholder="Enter features" />
                        <br />
                        <a href="javascript:;" onclick="ShowAment(4)">Add more</a>&nbsp;&nbsp; <a href="javascript:;"
                            onclick="HideAmenty(liFeatures3,<%=txtFeatures3.ClientID %>)">Hide </a></li>
                    <li id="liFeatures4" style="display: none">
                        <label for="<%=txtFeatures2.ClientID %>">
                            Features</label>
                        <asp:TextBox runat="server" ID="txtFeatures4" TextMode="MultiLine" TabIndex="12"
                            ToolTip="Please enter features" placeholder="Enter features" />
                        <br />
                        <a href="javascript:;" onclick="ShowAment(5)">Add more</a>&nbsp;&nbsp; <a href="javascript:;"
                            onclick="HideAmenty(liFeatures4,<%=txtFeatures4.ClientID %>)">Hide </a></li>
                    <li id="liFeatures5" style="display: none">
                        <label for="<%=txtFeatures5.ClientID %>">
                            Features</label>
                        <asp:TextBox runat="server" ID="txtFeatures5" TextMode="MultiLine" TabIndex="13"
                            ToolTip="Please enter features" placeholder="Enter features" />
                        <br />
                        <a href="javascript:;" onclick="HideAmenty(liFeatures5,<%=txtFeatures5.ClientID %>)">
                            Hide </a></li>
                </ul>
            </fieldset>
            <fieldset>
                <legend>Define Product Element </legend>
                <ul class="width48perc fleft">
                    <li>
                        <label>
                            Banner Ads
                        </label>
                        <asp:Repeater runat="server" ID="rptBannerPosition">
                            <HeaderTemplate>
                                <ul class="banner">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li class="fleft">
                                    <input type="checkbox" class="bannerposition" id="<%#Eval("AjBannerPositionId") %>"
                                        value='<%#Eval("AjBannerPostAmount") %>' onclick="CalculateTotalCost(this,'<%#Eval("AjBannerPostAmount") %>')" />
                                    <label>
                                        <%# Eval("AjBannerPosition")%>
                                        (Price:
                                        <%# Eval("AjBannerPostAmount")%>
                                        )
                                    </label>
                                </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>
                    </li>
                    <li>
                        <label>
                            Text Ads
                        </label>
                        <asp:Repeater runat="server" ID="rptTextAds">
                            <HeaderTemplate>
                                <ul class="assoc">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li>
                                    <input type="checkbox" class="association" id="<%#Eval("AssociationCategoryTypeId") %>"
                                        value='<%#Eval("AssociationCategoryAmount") %>' onclick="CalculateTotalCost(this,'<%#Eval("AssociationCategoryAmount") %>')" />
                                    <label>
                                        <%# Eval("AssociationCategoryType")%>
                                        (Price:
                                        <%# Eval("AssociationCategoryAmount")%>
                                        )
                                    </label>
                                </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>
                    </li>
                </ul>
            </fieldset>
            <fieldset>
                <legend>Product Cost </legend>
                <ul class="width48perc fleft">
                    <li>
                        <label>
                            Total Cost:</label>
                        <asp:TextBox ID="txtProductCots" runat="server" TabIndex="6"></asp:TextBox>
                        <p>
                            The Cost define here is per month</p>
                    </li>
                    <li>
                        <label>
                            Is Best Value</label>
                        <asp:CheckBox runat="server" ID="chkBestvalue" TabIndex="7" />
                    </li>
                    <li>
                        <label>
                            Publish</label>
                        <asp:CheckBox runat="server" ID="chkStatus" TabIndex="8" />
                    </li>
                </ul>
            </fieldset>
            <fieldset id="divProductDuration" runat="server" visible="false" >
                <legend>
                  Product Duration and Discount  <a href="#" id="lnkAddProductDiscount" onclick="OpenPoup('ProductDuration','650px','lnkAddProductDiscount');return false;" >Add More </a>
                </legend>
                <ul>
                <li>
                <label> </label>

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
                
                </li>
                
                </ul>
            
            </fieldset>
            <ul class="width48perc fleft">
                <li>
                    <label>
                        &nbsp;</label>
                    <asp:Button ID="btnAdsProduct" ValidationGroup="AdsProduct" runat="server" Text="Save"
                        TabIndex="14" ToolTip="Please Submit" OnClick="btnAdsProduct_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="15" ToolTip="Please Submit To Cancel"
                        OnClick="btnCancel_Click" />
                    <asp:HiddenField runat="server" ID="hdnBanner" />
                    <asp:HiddenField runat="server" ID="hdnAssociationId" />
                    <asp:HiddenField runat="server" ID="hdnAmount" Value="0" />
                </li>
            </ul>
        </fieldset>
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
    </ContentTemplate>
    </asp:UpdatePanel>
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
            $("#<%=txtProductName.ClientID %>").focus();
        });

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                $("#<%=txtValidityStartTime.ClientID %>").datepicker({
                    changeMonth: true,
                    changeYear: true
                });
                $("#<%=txtProEndTime.ClientID %>").datepicker({
                    changeMonth: true,
                    changeYear: true
                });
            }
        }

        function CalculateTotalCost(chkControl, amount) {
            var status = $(chkControl).is(":checked");

            var cost = $("#<%=txtProductCots.ClientID %>").val() != '' ? parseInt($("#<%=txtProductCots.ClientID %>").val()) : 0;

            if (status) {

                cost = cost + parseInt(amount);
            } else {
                cost = cost - parseInt(amount);
            }


            $("#<%=txtProductCots.ClientID %>").val(cost);

        }

        $('#<%=btnAdsProduct.ClientID %>').click(function () {
            if (Page_IsValid != null && Page_IsValid == true) {
                $('.bannerposition:checked').each(function (i) {
                    var value = $(this).attr("id") + "," + $("#<%=hdnBanner.ClientID %>").val();
                    $("#<%=hdnBanner.ClientID %>").val(value);

                });
                $('.association:checked').each(function (i) {
                    var value = $(this).attr("id") + "," + $("#<%=hdnAssociationId.ClientID %>").val();
                    $("#<%=hdnAssociationId.ClientID %>").val(value);

                });
                if ($("#<%=hdnAssociationId.ClientID %>").val().length > 0 || $("#<%=hdnBanner.ClientID %>").val().length > 0) {
                    if (confirm("Do you really want to save this product having amount " + $("#<%=txtProductCots.ClientID %>").val())) {

                        return true;
                    } else
                        return false;
                } else {
                    alert("Please select at least one product either banner or text ads");
                    return false;
                }
            }
        });


        function SelectList(bannerIds, associationIds, amount, featuresCount) {

            var items = $('.banner input:checkbox');

            for (var i = 0; i < items.length; i++) {

                if (jQuery.inArray(items[i].id, bannerIds.split(',')) > -1) {
                    items[i].checked = true;

                }
            }

            SelectAssociation(associationIds);
            $("#<%=txtProductCots.ClientID %>").val(amount);

            for (var i1 = 2; i1 <= featuresCount; i1++) {
                $('#liFeatures' + i1).show();
            }

        }

        function SelectAssociation(associationIds) {

            var items = $('.assoc input:checkbox');

            for (var i = 0; i < items.length; i++) {

                if (jQuery.inArray(items[i].id, associationIds.split(',')) > -1) {
                    items[i].checked = true;

                }
            }
        }

        ShowAment = function (val) {

            $("#liFeatures" + val).slideToggle();
        };
        HideAmenty = function (showControl, txtControl) {
            $(showControl).hide();
            $(txtControl).val('');

        };

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
                        OpenPoup('ProductDuration', '650px', 'lnkAddProductDiscount')
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

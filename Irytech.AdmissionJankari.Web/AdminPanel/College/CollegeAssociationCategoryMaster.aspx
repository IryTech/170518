<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="CollegeAssociationCategoryMaster.aspx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.CollegeAssociationCategoryMaster" %>
    <%@ Register Src="../../UserControl/FckEditorCostomize.ascx" TagName="FckEditorCostomize" TagPrefix="AJ" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UdtpnlCity" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <ul class="addPage_utility">
        <li class="fright" style="width: 142px !important;">
            <div class="navbar-inner">
                <a href="#" id='sndAddCollegeAssociation' class="insertIco" onclick="OpenPoup('divRankSourceInsert','650px','sndAddCollegeAssociation');return false;">
                                    Add Ads Code</a>
                <div class="clear">
                </div>
            </div>
        </li>
        <li class="fright" style="width: 72px !important;">
            <asp:ImageButton ID="btnUpload" runat="server" ImageUrl="~/AdminPanel/Images/CommonImages/uploadExcel.png" ToolTip="Upload Excel Format" TabIndex="12" OnClick="btnUpload_Click1" />
                            <asp:Label ID="lblRecordsInserted" runat="server"></asp:Label>
            <asp:ImageButton ID="btnPreview" runat="server" ImageUrl="~/AdminPanel/Images/CommonImages/downloadExcel2.png" ToolTip="Download Excel Format" TabIndex="13" OnClick="btnPreview_Click" />
        </li>
    </ul>


            <div class="grdOuterDiv">
                <asp:Label ID="lblSeccessMsg" CssClass="success hide" runat="server" ></asp:Label>
                <fieldset style="display:none;">
                    
                    <ul class="options-bar">
                        <li class="opt-barlist">
                            <label>
                                Upload File:
                            </label>
                            <asp:FileUpload ID="fileUploadExcel" runat="server" TabIndex="11" />
                            <asp:RequiredFieldValidator ID="rfvExcelUpload" runat="Server" ControlToValidate="fileUploadExcel"
                                ValidationGroup="ExcelUpload" />
                            <asp:RegularExpressionValidator ID="revExcelUpload" runat="server" ControlToValidate="fileUploadExcel"
                                ValidationGroup="GrUpload"></asp:RegularExpressionValidator>
                        </li>
                         
                    </ul>
                </fieldset>
                <fieldset>
                <legend>Ads Master</legend>
               <fieldset>
               <legend>Text Ads Master</legend>
                    <asp:Repeater ID="rptCollegeAssociate" runat="server" 
                       onitemcommand="rptCollegeAssociate_ItemCommand" >
                        <HeaderTemplate>
                            <table class="grdView">
                                <tr>
                                    <th>
                                        S.No
                                    </th>
                                    <th>
                                        Ads Code
                                    </th>
                                    <th>
                                    Ads Amount
                                    </th>
                                    <th>
                                      Ads Status
                                    </th>
                                    <th>
                                        Edit Basic Details
                                    </th>
                                     <th>
                                        Discount And Duration
                                    </th>
                                    <th>
                                        Add Discount and Duration
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id='<%# Eval("AssociationCategoryTypeId")%>' >
                                <td>
                                    <%#Container.ItemIndex+1 %>
                                </td>
                                <td>
                                   <span id="spnAssociatiponType">  <%# Eval("AssociationCategoryType")%></span>
                                </td>
                                <td>
                                  <%# Eval("AssociationCategoryAmount")%>
                                </td>
                                <td>
                                   <span id="spnAssociatiobStatus">   <%# Eval("AssociationCategoryStatus")%></span>
                                </td>
                                <td>
                                      <asp:ImageButton ImageUrl="/AdminPanel/Images/CommonImages/editIcon.png" title="Edit" alt="Edit-Icon" ID="btnTextEdit" runat="server" CommandName="Edit" CausesValidation="false" CommandArgument='<%# Eval("AssociationCategoryTypeId")%>' class="editIconmargin" width="12px" />
                                </td>
                                <td>
                               <a href="#" id="lnkViewTextDiscountDetails"  title="View Discount Details" onclick="GetAdvstDiscount(3,<%# Eval("AssociationCategoryTypeId")%>);return false">    <%# GetCountDiscountAndDuration(Convert.ToInt16(IryTech.AdmissionJankari.BO.AdvertismentType.TextDisplay), Convert.ToInt16(Eval("AssociationCategoryTypeId")))%></a>
                                </td>
                                <td>
                                 <a href="#"  id="lnkAddTextAdds" onclick="AddAdvst(3,<%# Eval("AssociationCategoryTypeId")%>);return false;">Add Discount </a>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                   </fieldset>
                   <fieldset>
                   <legend> Display Ads Master</legend>
                   <asp:Repeater ID="rptDisplayAds" runat="server" 
                           onitemcommand="rptDisplayAds_ItemCommand">
                    <HeaderTemplate>
                            <table class="grdView">
                                <tr>
                                    <th>
                                        S.No
                                    </th>
                                    <th>
                                        Ads Code
                                    </th>
                                    <th>
                                    Ads Amount
                                    </th>
                                    <th>
                                      Ads Status
                                    </th>
                                    <th>
                                        Edit Basic Details
                                    </th>
                                    <th>
                                        Discount And Duration
                                    </th>
                                    <th>
                                        Add Discount and Duration
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id= >
                                <td>
                                    <%#Container.ItemIndex+1 %>
                                </td>
                                <td>
                                    <%# Eval("AssociationCategoryType")%>
                                </td>
                                <td>
                                  <%# Eval("AssociationCategoryAmount")%>
                                </td>
                                <td>
                                   <span id="spnAssociatiobStatus">   <%# Eval("AssociationCategoryStatus")%></span>
                                </td>
                                <td>
                                  <asp:ImageButton ImageUrl="/AdminPanel/Images/CommonImages/editIcon.png" title="Edit" alt="Edit-Icon" ID="btndisplaytEdit" runat="server" CommandName="Edit" CausesValidation="false" CommandArgument='<%# Eval("AssociationCategoryTypeId")%>' class="editIconmargin" width="12px" />
                                </td>
                                 <td>
                                  <a href="#" id="lnkViewDiscountDetails"  title="View Discount Details" onclick="GetAdvstDiscount(2,<%# Eval("AssociationCategoryTypeId")%>);return false">     <%# GetCountDiscountAndDuration(Convert.ToInt16(IryTech.AdmissionJankari.BO.AdvertismentType.Banner), Convert.ToInt16(Eval("AssociationCategoryTypeId")))%></a>
                                </td>
                                <td>
                                   <a href="#"  id="lnkAddDisplayAdds" onclick="AddAdvst(2,<%# Eval("AssociationCategoryTypeId")%>);return False;">Add Discount </a>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                   
                   </asp:Repeater>
                   
                   </fieldset>
                </fieldset>
            </div>
            <div id="divRankSourceInsert" class="popup_block width43perc">
                <fieldset id="basicInfo">
                    <legend>
                        <asp:Label ID="lblInsertUpdate" runat="server" Text="Add ads Code"></asp:Label>
                    </legend>
                    <ul>
                    <li>
                            <label>
                                 Product Type:</label>
                            <asp:DropDownList ID="ddlProductType" runat="server" ValidationGroup="CollegeCat" >
                            <asp:ListItem Text="Select Product Type" Value="0" Selected="True"></asp:ListItem>
                             <asp:ListItem Text="Text Ads" Value="T" ></asp:ListItem>
                              <asp:ListItem Text="Display Ads" Value="D" ></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" Display="Dynamic"
                                runat="server" ControlToValidate="ddlProductType" InitialValue="0" ValidationGroup="CollegeCat"></asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label>
                                 Product Amount:</label>
                            <asp:TextBox ID="txtAmount"  runat="server"
                                TabIndex="2" ValidationGroup="CollegeCat"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAmount" SetFocusOnError="true" Display="Dynamic"
                                runat="server" ControlToValidate="txtAmount" ValidationGroup="CollegeCat"></asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label>
                                 Product Description:</label>
                             <div class="fleft" style="margin: 3px 5px;">
                                <Aj:FckEditorCostomize ID="fckProductDesc" tabindex="3" runat="server"  />
                           </div>
                        </li>
                        <li>
                            <label>
                                 Code:</label>
                            <asp:TextBox ID="txtAssociationCategoryName"  runat="server"
                                TabIndex="2" ValidationGroup="CollegeCat"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAssociationCategoryType" SetFocusOnError="true" Display="Dynamic"
                                runat="server" ControlToValidate="txtAssociationCategoryName" ValidationGroup="CollegeCat"></asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label>
                                Publish :</label>
                            <asp:CheckBox ID="chkAssociationStatus" runat="server" />
                        </li>
                        <li>
                            <label>
                                &nbsp;</label>
                            <asp:Button ID="btnCollegeAssociat" runat="server" Text="Add" TabIndex="9" CausesValidation="true"
                                ValidationGroup="CollegeCat" OnClick="btnCollegeAssociat_Click" />
                            <input id="btnReset" type="button" value="Reset" onclick="ClearAllFields();" title="Please Reset" />
                        </li>
                    </ul>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
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
                <th>
                   Edit
                </th>
            </tr>
            <tbody id="imageData">
            </tbody>
        </table>
    </div>
    <div id="ProductDuration" class="popup_block width43perc">
        <fieldset id="Fieldset1">
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
                    <asp:Button runat="server" Text="Update" ID="btnSave"  OnClick="UpdateDiscountDetails"  ClientIDMode="Static" TabIndex="4" ValidationGroup="AddProductDuration"
                        ToolTip="Please Submit"   />
                    
                </li>
            </ul>
        </fieldset>
    </div>
    <asp:HiddenField ID="hndDiscountId" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hndAdvstType" runat="server" ClientIDMode="Static"  />
    <asp:HiddenField ID="hndAdvstYpeId" runat="server" ClientIDMode="Static"  />
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
       function ClearAllFields() {
            document.getElementById('ctl00_ContentPlaceHolderMain_txtAssociationCategoryName').value = '';
            document.getElementById('ctl00_ContentPlaceHolderMain_btnCollegeAssociat').value = 'Add';
            document.getElementById('ctl00_ContentPlaceHolderMain_chkAssociationStatus').checked = false;
            window.scrollTo(0, 0);
        }
        function GetAdvstDiscount(advstType, advstTypeId) {
           
              var json = "{'advstTypes':" + advstType + ",'advstTypeIds':" + advstTypeId + "}";

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
                data = data + "<tr><td>" + client.AjMonthValue + "</td><td>" + client.AjDiscount + "</td><td>" + DateFormate(client.AjDiscountValidityStart) + "</td><td>" + DateFormate(client.AjDiscountValidityEnd) + "</td><td>" + client.AjDefaultSelection + "</td><td>" + client.AjDiscountStatus + "</td><td> <a href='#'  title='View Discount Details' onClick='GetDetailsDiscount(" + client.AjAdvertismentDiscountId + "," +client.AjAdvertisementType +"," +  client.AjAdvertisementTypeId+")'>Edit</a></td> </tr>"
            });

            $('#imageData').append(data);
            OpenPoup('divAdvstDiscount', '800', 'lnkViewDiscountDetails')
        }
        function DateFormate(dateString) {
            dateString = dateString.substr(6);
            var currentTime = new Date(parseInt(dateString));
            var month = currentTime.getMonth() + 1;
            var day = currentTime.getDate();
            var year = currentTime.getFullYear();
            return day + "/" + month + "/" + year;

        }
        
        function GetDetailsDiscount(discountId,advstType,advstTypeId) {
            var json = "{'advstDiscountId':" + discountId + "}";
            $("#hndAdvstType").val(advstType);
            $("#hndAdvstYpeId").val(advstTypeId);
             $.ajax({
                type: "POST",
                url: "../../WebServices/CommonWebServices.asmx/GetDetailsOfAdvertimentDiscountByDiscountId",
                data: json,
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    GetParticularDiscoutDetails(response);
                },
                error: function (xml, textStatus, errorThrown) {

                    alert(errorThrown);
                }
            });
            
        }
        function GetParticularDiscoutDetails(response) {
             var list = $.parseJSON(response.d);
            if (list != null) {
                if (list.length > 0) {
                    $.each(list, function () {

                        $("#txtProductDuration").val(this['AjMonthValue']);
                        $("#hndDiscountId").val(this['AjAdvertismentDiscountId']);
                        $("#txtProDiscount").val(this['AjDiscount']);
                        $("#txtValidityStartTime").val(DateFormate(this['AjDiscountValidityStart']));
                        $("#txtProEndTime").val(DateFormate(this['AjDiscountValidityEnd']));
                        $('#chkDefaultSelection').attr('checked', this['AjDefaultSelection']);
                        $('#chkDiscountStatus').attr('checked', this['AjDiscountStatus']);
                        OpenPoup('ProductDuration', '800', 'lnkViewDiscountDetails')
                     
                    });
                } else {
                    alert('No Record found for the course');
                }
            } else {
                alert('No Record found for the course');
            }
        }
        function AddAdvst(advstType, advstTypeId) {
            $("#hndAdvstType").val(advstType);
            $("#hndAdvstYpeId").val(advstTypeId);
            $("#btnSave").val('Insert');
            OpenPoup('ProductDuration', '800', 'lnkViewDiscountDetails')
        }

    </script>
</asp:Content>

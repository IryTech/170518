<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="PackageCreation.aspx.cs" Inherits="Irytech.AdmissionJankari.Web.AdminPanel.Form_Package.PackageCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="updSpeechDetails" runat="server">
        <ContentTemplate>
            <div id="fade">
            </div>
            <asp:Label ID="lblSeccessMsg" CssClass="success" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblErorrMsg" CssClass="error" runat="server" Visible="false"></asp:Label>
            <div id="divImage" class="loading">
                <img src="/image.axd?Common=Loading.gif" />
            </div>

            <ul class="addPage_utility">
                <li class="fright" style="width: 213px !important;">
                    <div class="navbar-inner">
                        <a href="#" id='sndAddCollegePresedentSpeech' class="insertIco" onclick="OpenPoup('divUniversityCategoryInsert','450px','sndAddCollegePresedentSpeech');return false;">Create Package </a>
                        <div class="clear">
                        </div>
                    </div>
                </li>
            </ul>

            <fieldset>
                <legend>Form Package Management</legend>

                <asp:Label ID="lblErrorMessage" runat="server" CssClass="err_msg" Visible="false"></asp:Label>
                <asp:Repeater ID="rptPresidentDetails" runat="server" OnItemDataBound="rptPresidentDetails_ItemDataBound" OnItemCommand="rptPresidentDetails_ItemCommand">
                    <HeaderTemplate>
                        <table class="grdView">
                            <tr>

                                <th>Package Name
                                </th>
                                 <th>Course Name
                                </th>
                                <th>Factors Name
                                </th>
                                <th>Is Chargeable 
                                </th>
                                <th>Is Visible 
                                </th>
                                <th>Amount
                                </th>
                                <th>Action
                                </th>
                                <th></th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# Eval("PackageName")%>
                                <asp:HiddenField ID="hndPackageId" runat="server" Value='<%#Eval("PackageId")%>' />
                            </td>
                            <td>
                                <%# Eval("CourseName")%>
                                
                            </td>
                            <td>
                                <asp:Repeater ID="rptFactorDetails" runat="server">
                                    <HeaderTemplate>

                                        <table class="grdView">
                                            <tr>

                                                <th>Factor Name
                                                </th>

                                               
                                                 </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <tr>
                                            <td>
                                                <%# Eval("FactorName")%>

                                            </td>
                                            <%--<td>
                                                <asp:LinkButton ID="BtnEdit" runat="server" CssClass="roundedFormat Link_Btn" Text="Edit"
                                                    CommandArgument='<%# Eval("FactorID")%>' CommandName="Edit" CausesValidation="false" />

                                            </td>--%>
                                        </tr>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </td>
                            <td>
                                <%# Eval("IsChargeable")%>
                            </td>
                            <td>
                                <%# Eval("IsVisible")%>
                            </td>
                            <td>
                                <%# Eval("PackageAmount")%>
                            </td>
                            <td>
                                <asp:LinkButton ID="BtnEdit" runat="server" CssClass="roundedFormat Link_Btn" Text="Edit"
                                    CommandArgument='<%# Eval("PackageId")%>' CommandName="Edit" CausesValidation="false" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>

                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div id="Progress" class="pop">
                            <img src="/image.axd?Common=LoadingImage.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </fieldset>
            <asp:HiddenField ID="hndPackageId" runat="server" />

            <div id="divUniversityCategoryInsert" class="popup_block width68perc">
                <fieldset id="basicInfo">
                    <legend>
                        <asp:Label ID="lblCollegePlacement" runat="server"></asp:Label></legend>
                    <ul class="pouplist">
                        <li style="width: 99% !important;">
                            <label>
                                Course Name:</label>
                            <asp:DropDownList ID="ddlCourse" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvCourseName" ControlToValidate="ddlCourse" InitialValue="0" Display="Dynamic" SetFocusOnError="True"
                                ValidationGroup="President">Please Select Course </asp:RequiredFieldValidator>

                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                Package Name:</label>
                            <asp:TextBox ID="txtPackageName" runat="server" ToolTip="Enter Factor Name" Style="min-width: 207px !important;" TabIndex="1" ValidationGroup="President"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvCollegeName" ControlToValidate="txtPackageName" Display="Dynamic" SetFocusOnError="True"
                                ValidationGroup="President">Enter factor name</asp:RequiredFieldValidator>
                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                Factor Name :</label>
                           
                            <asp:CheckBoxList ID="chkFactorDetails" RepeatDirection="Horizontal" RepeatColumns="3" TabIndex="2" runat="server"></asp:CheckBoxList>
                             <asp:CustomValidator ID="CustomValidator1" ErrorMessage="Please select at least one Factor."
                            Display="Dynamic" SetFocusOnError="True" ClientValidationFunction="ValidateCheckBoxList" runat="server" ValidationGroup="President"/>
                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                Is Chargeable:
                            </label>
                            <asp:CheckBox ID="chkChargeable"  ClientIDMode="Static" runat="server" ToolTip="Is Factor is Chargeable" TabIndex="3" />
                           

                        </li>
                        <li style="width: 99% !important; display:none;" id="liAmount">
                            <label>
                                Amount:
                            </label>
                            <asp:TextBox ID="txtAmount" runat="server" ToolTip="Enter the package amount" Style="min-width: 207px !important;" TabIndex="4"></asp:TextBox>

                        </li>
                        <li style="width: 99% !important;" >
                            <label>
                                Is Visible:
                            </label>
                            <asp:CheckBox ID="chkVisible" runat="server" a ToolTip="Is Factor is Chargeable" TabIndex="5" />

                        </li>

                        <li style="width: 99% !important;">
                            <label>
                                &nbsp;</label>
                            <asp:Button ID="btnSubmit" runat="server" TabIndex="5" CausesValidation="true" OnClick="btnSubmit_Click" Text="Create"
                                ValidationGroup="President" />
                            <asp:Button ID="btnClear" TabIndex="6" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </li>
                    </ul>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="rptPresidentDetails" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
    function ValidateCheckBoxList(sender, args) {
        var checkBoxList = document.getElementById("<%=chkFactorDetails.ClientID %>");
        var checkboxes = checkBoxList.getElementsByTagName("input");
        var isValid = false;
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].checked) {
                isValid = true;
                break;
            }
        }
        args.IsValid = isValid;
    }
        $(document).ready(function () {
           
            $('#chkChargeable').change(function () {
            
                if (this.checked) {

                    $("#liAmount").show();

                }

                else {

                    $("#liAmount").hide();

                }

        });
        });
       
</script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="FactorMaster.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.FactorMaster" %>

<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="Aj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UdtpnlState" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnFactorId" runat="server" />
            <ul class="addPage_utility">
        <li class="fright" style="width: 165px !important;">
            <div class="navbar-inner">
              <a href="#" id='sndAddCollegeFactor' class="insertIco" onclick="OpenPoup('divRankSourceInsert','650px','sndAddCollegeFactor');return false;">
                                    Add Rank Source</a>
                <div class="clear">
                </div>
            </div>
        </li>
        </ul>
            <div class="grdOuterDiv">
                <asp:Label ID="lblSuccess" CssClass="success hide"  runat="server"></asp:Label>
                   
                
                <fieldset>
                    <legend>Factor List</legend>
                   
                        <asp:Repeater ID="rptFactor" runat="server">
                            <HeaderTemplate>
                                <table class="grdView">
                                    <tr>
                                        <th>
                                            S.No
                                        </th>
                                        <th>
                                            Factor Name
                                        </th>
                                        <th>
                                            Action
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr id="<%# Eval("AjFactorId")%>">
                                    <td>
                                        <%# Eval("SrNo")%>
                                    </td>
                                    <td>
                                        <span id="rankFactor">
                                            <%# Eval("AjFactorName")%></span>
                                    </td>
                                    <td>
                                        <img src="/AdminPanel/Images/CommonImages/editIcon.png" title="Edit" alt="Edit-Icon"
                                            id="edit" class="editIconmargin" width="12px" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table></FooterTemplate>
                        </asp:Repeater>
                        <Aj:CustomPaging ID="ucCustomPaging" runat="server" />
                    
                </fieldset>
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divRankSourceInsert" class="popup_block width43perc">
        <fieldset>
            <legend>Add College Ranking Factor </legend>
            <ul>
                <li>
                    <label>
                        Factor Name:</label>
                    <asp:TextBox ID="txtFactorName" runat="server" ValidationGroup="vldFactor" TabIndex="1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCountryName" Display="Dynamic" runat="server"
                        SetFocusOnError="true" ControlToValidate="txtFactorName" ValidationGroup="vldFactor"></asp:RequiredFieldValidator>
                </li>
                <li><label></label>
                    <asp:Button ID="btnFactorInsertUpdate" runat="server" Text="Save" CausesValidation="true"
                        ValidationGroup="vldFactor" TabIndex="2" OnClick="btnFactorInsertUpdate_Click" />
                    <input id="btnReset" type="button" value="Clear" tabindex="3" onclick="ClearFields()" />
                </li>
            </ul>
        </fieldset>
    </div>
    <script type="text/javascript">

        $(document).ready(function () {
            // Use live so when adding new records the events will
            // automatically be bounde
           
            $("[id*='edit']").live('click', OnEdit);

        });
        function OnEdit() {
            // Get the row this button is within
            var tr = $(this).closest("tr");
            // Get the first and last name controls in this row
            var rankFactor = tr.find("span[id='rankFactor']");


            // Insert an input element before the labels
            // and set the value to the label text
            // Then hide the label

            rankFactor.before("<input id='rankfactorEdit' type='text' value='" + rankFactor.text().trim() + "'/>").hide();


            // Hide the existing buttons and add a save button in there place
            tr.find("[id*='edit']").before("<img id='save' class='editIconmargin' src='/AdminPanel/Images/CommonImages/base_floppydisk_32.png' width='14px' title='Save' alt='Save' />")
         .hide();

            tr.find("[id*='save']").one('click', OnSave);
            $("#<%=lblSuccess.ClientID%>").removeClass("success");
            $("#<%=lblSuccess.ClientID%>").removeClass("show");
            $("#<%=lblSuccess.ClientID%>").addClass("hide");
        }
        function OnSave() {
            // Get the row this button is within
            var tr = $(this).closest("tr");

            var firstName = tr.find("[id='rankfactorEdit']");
            // Set the text of the labels from the input elements and show them
            tr.find("span[id='rankFactor']").text(firstName.val()).show();
            // Remove the input elements
            firstName.remove();
            // Show the buttons again and remove the save
            tr.find("[id*='edit']").show();
            tr.find("[id*='save']").remove();

            // update the contact on the server
            UpdateContact(tr.attr("id"), firstName.val())
        }

        function UpdateContact(id, factorName) {

            var data = "{'factorId':" + id + ",'factorName':'" + factorName +"'}";
            
            $.ajax({
                type: "POST",
                url: "FactorMaster.aspx/UpdateCollegeRankingFactor",
                data: data,
                contentType: "application/json",
                dataType: "json",
                success: function (response) {
                   $("#<%=lblSuccess.ClientID%>").addClass("success");
                   $("#<%=lblSuccess.ClientID%>").removeClass("hide");
                   $("#<%=lblSuccess.ClientID%>").text(response.d);
                },
                error: OnAjaxError
            });
        }
        
        function ClearFields() {
            $("#<%=txtFactorName.ClientID %>").val('');
            $("#<%=lblSuccess.ClientID %>").text('');
        }
    </script>
</asp:Content>

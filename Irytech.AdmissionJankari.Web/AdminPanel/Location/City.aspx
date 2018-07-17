<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AdminPanel/Controls/Admin.Master" CodeBehind="City.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Location.City" %>
<%@ Register src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>


<asp:Content ID="ContentCountry" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">

    <asp:UpdatePanel ID="UdtpnlCity" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:HiddenField runat="server" ID="hdnStateId"></asp:HiddenField>
           
            <asp:Label ID="lblMsg" runat="server" CssClass="success" Visible="false"></asp:Label>
            <asp:Label ID="lblwarningMsg" CssClass="warning" runat="server" Visible="false"></asp:Label>
            
             <ul class="addPage_utility">
        <li class="fright" style="width: 105px !important;">
            <div class="navbar-inner">
                <a href="#" id='sndAddCity' class="insertIco" onclick="OpenPoup('divCourseCategoryInsert','650px','sndAddCity');return false;">
                            Add City</a>
                <div class="clear">
                </div>
            </div>
        </li>
        <li class="fright" style="width: 72px !important;">
            <asp:ImageButton ID="btnUpload"  runat="server" ToolTip="Upload Excel" ImageUrl="~/AdminPanel/Images/CommonImages/uploadExcel.png"  ValidationGroup="GrUpload"
                            TabIndex="4" OnClick="btnUpload_Click" />
            <asp:ImageButton ID="btnPreview" ImageUrl="~/AdminPanel/Images/CommonImages/downloadExcel2.png" runat="server" ToolTip="Preview Excel"  TabIndex="5" OnClick="btnPreview_Click" />
        </li>
    </ul>
            
            
            
            <fieldset style="display:none;">
            <legend> City Master </legend>
            <ul class="options-bar">
                <li class="opt-barlist">
                   <label> Upload File: </label>
                        <asp:FileUpload ID="fileUploadExcel" runat="server" OnClientClick="FocusLabel();" TabIndex="4" /></label>
                          <asp:RequiredFieldValidator ID="rfvExcelUpload" runat="Server" ControlToValidate="fileUploadExcel" Display="Dynamic"
                                ValidationGroup="ExcelUpload" />
                        <asp:RegularExpressionValidator ID="revUploadExcel" Display="Dynamic" runat="server" ControlToValidate="fileUploadExcel" ValidationGroup="GrUpload" SetFocusOnError="true"></asp:RegularExpressionValidator>
                </li>
               <li class="width12perc fleft">
                     <asp:Button  runat="server" Text="Upload Excel"  CssClass="uploadbtn"   OnClientClick="colorboxDialogSubmitClicked('ExcelUpload', 'uploadImagePanel');"
                        TabIndex="5" onclick="btnUpload_Click"/></li>
                 <li class="width24perc fleft">
                    <label style="width: 80px !important;">
                        See Excel:</label>
                     <asp:Button  runat="server" Text="Preview Excel" CssClass="downloadbtn" TabIndex="6" onclick="btnPreview_Click"/>
                </li>
               
                <li class="width25perc fleft">
                    <div class="navbar-inner">
                        
                        <div class="clear">
                        </div>
                    </div>
                </li>
            </ul>
        </fieldset>
            <fieldset>
                <legend>Filter City</legend>
                <ul>
                <li><label></label> 
                        <%--<label>Country Name:</label>--%>
                       <asp:DropDownList ID="ddlCountryList" onfocus="ClearLabel()" TabIndex="7" Width="80%" AutoPostBack="true" 
                            runat="server" onselectedindexchanged="ddlCountryList_SelectedIndexChanged"></asp:DropDownList>
                   
                        <%--<label>Zone Name:</label>--%>
                       <asp:DropDownList ID="ddlZoneList" onfocus="ClearLabel()" TabIndex="8" AutoPostBack="true" 
                            runat="server" onselectedindexchanged="ddlZoneList_SelectedIndexChanged"></asp:DropDownList>
                   
                      <%--  <label>City Name:</label>--%>
                       <asp:DropDownList ID="ddlCityList" onfocus="ClearLabel()" TabIndex="9" AutoPostBack="true" 
                            runat="server" onselectedindexchanged="ddlCityList_SelectedIndexChanged"></asp:DropDownList>
                            
                    </li>
                    
                   
                </ul>
            </fieldset>
            <fieldset>
               <legend>City List</legend>
                <asp:Label ID="lblRecordMsg" runat="server" Visible="false" CssClass="warning"></asp:Label>
                <asp:Repeater ID="rptCity" runat="server" 
                        onitemcommand="rptCity_ItemCommand" >
                     
                       <headertemplate>
                          <table class="grdView"> 
                            <tr>
                                <th>S.No</th>
                                <th>City Name</th>
                                <th>Zone Name</th>
                                <th>State Name</th>
                                <th>Action</th>
                             </tr>
      
                        </headertemplate>
                        <itemtemplate>
                           <tr>
                                <td><%# Eval("SrNo")%> </td>
                                <td><%# Eval("CityName")%></td>
                                <td><%# Eval("ZoneName")%></td>
                                <td><%# Eval("StateName")%></td>
                                <td>
                                    <asp:LinkButton ID="BtnEdit" runat="server" CssClass="roundedFormat Link_Btn" OnClientClick="return FocusLabel();" Text="Edit" CommandArgument='<%# Eval("CityId")%>' CommandName="Edit" CausesValidation="false" />
                                 </td>
                             </tr>
                         </itemtemplate> 
                         <FooterTemplate>
                        </table></FooterTemplate>
                     </asp:Repeater>
               <AJ:CustomPaging ID="ucCustomPaging"  runat="server" />
            </fieldset>



        <div id="divCourseCategoryInsert" class="popup_block width43perc">
        <fieldset id="basicInfo">
            <legend>Add City</legend>
            <ul class="pouplist">
                <li style="width: 99% !important;">
                     <label>State Name:</label>
                        <asp:DropDownList ID="ddlStateName" TabIndex="1" onfocus="ClearLabel()" runat="server" ></asp:DropDownList>
                   <asp:RequiredFieldValidator ID="rfvStateName" runat="server" InitialValue="0" ControlToValidate="ddlStateName" SetFocusOnError="true" ValidationGroup="City">
                       Please Enter State Name
                   </asp:RequiredFieldValidator>
                       
                  
                </li>
                <li style="width: 99% !important;">
                   <label>City Name:</label>
                     
                        <asp:TextBox ID="txtCityName" runat="server" onfocus="ClearLabel()" TabIndex="2" ValidationGroup="City"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCityName" runat="server" ControlToValidate="txtCityName" SetFocusOnError="true" ValidationGroup="City"></asp:RequiredFieldValidator>
                </li>
                <li style="width: 99% !important;">
                    <label>
                        &nbsp;</label>
                      <asp:Button ID="btnCity" runat="server" Text="Add"  OnClick="btnCity_Click" CausesValidation="true" ValidationGroup="City" TabIndex="3"/>
                        <input id="btnReset" type="button" value="Reset" onclick="ClearAllFields();" title="Please Reset" />
                </li>
            </ul>
        </fieldset>
    </div>
           <script type="text/javascript">
                    function ClearAllFields() {
                        document.getElementById('ctl00_ContentPlaceHolderMain_ddlStateName').selectedIndex = 0;
                        document.getElementById('ctl00_ContentPlaceHolderMain_txtCityName').value = '';
                    
                        window.scrollTo(0, 0);
                    }
            </script>
             <script language="javascript" type="text/javascript">
                 function ClearLabel() {
                     document.getElementById('<%=lblMsg.ClientID %>').style.display = "none";
                     document.getElementById('<%=lblwarningMsg.ClientID %>').style.display = "none";
                     document.getElementById('<%=lblRecordMsg.ClientID %>').style.display = "none";
                 }
 
            </script>
        <script type="text/javascript">
            function FocusLabel() {

                window.scrollTo(0, 0);
            }
            </script>
           
     </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
</asp:UpdatePanel>
 <div id="divImage" class="loading">
        <img src="/image.axd?Common=Loading.gif" alt="Loading_Image" title="Loading Image" />
    </div>
<script type="text/javascript" src="../../Scripts/Autocomplete.js"></script>
<script type="text/ecmascript">
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
    function ValidateStatus() {
        if (confirm("Are you sure you want to update the status?"))
            return true;
        else
            return false;
    }
</script>
</asp:Content>
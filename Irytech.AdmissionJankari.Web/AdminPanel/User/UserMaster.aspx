<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" EnableEventValidation="false" MaintainScrollPositionOnPostback="false" AutoEventWireup="true" CodeBehind="UserMaster.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.User.UserMaster" %>
<%@ Register src="../../UserControl/CustomPaging.ascx" tagname="CustomPaging" tagprefix="AJ" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<asp:UpdatePanel ID="UdtpnlCity" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
     <fieldset>
                <legend> User Search </legend>
                <ul>
                <li>
                        <label>User Name</label>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="autocomplete" placeholder="Please Enter User Name" Width="63%" onfocus="ClearLabel()" TabIndex="2" ValidationGroup="UserMaster"></asp:TextBox>
                            
                    </li>
                   <li class="width45perc fleft">
                        <label>Email Id & Mobile</label>
                            <asp:TextBox ID="txtUserEmailId" runat="server" CssClass="autocomplete" placeholder="Please Enter Email ID" Width="66%" onfocus="ClearLabel()" TabIndex="2" ValidationGroup="UserMaster"></asp:TextBox>
                            
                    </li>
                   <li class="width45perc fleft">
                        <%--<label>Mobile No:</label>--%>
                            <asp:TextBox ID="txtUserMobileNo" runat="server" onfocus="ClearLabel()" placeholder="Please Enter Mobile Number"  CssClass="autocomplete" Width="56.5%" TabIndex="2" ValidationGroup="UserMaster"></asp:TextBox>
                           <asp:Button ID="btnSearch" runat="server" CssClass="searchbtn" Text="Search" TabIndex="12" 
                            onclick="btnSearch_Click"/> 
                    </li>
                    <li> 
                        
                        
                    </li>
                </ul>
                                
                <ul class="options-bar">
                  <li class="opt-barlist">
                        <label>Course Name</label>
                            <asp:DropDownList ID="ddlCourseName" runat="server" 
                            onselectedindexchanged="ddlCourseName_SelectedIndexChanged" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                            
                    </li>
                    <li class="opt-barlist">
                        <label>State Name</label>
                            <asp:DropDownList ID="ddlStateName" runat="server" 
                            onselectedindexchanged="ddlStateName_SelectedIndexChanged" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                            
                    </li>
                    <li class="opt-barlist">
                        <label>City Name</label>
                        <asp:DropDownList ID="ddlCityName" runat="server" 
                            onselectedindexchanged="ddlCityName_SelectedIndexChanged" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                            
                    </li>
                    <li class="opt-barlist">
                        <label>Category</label>
                            <asp:DropDownList ID="ddlUSerCategoryName" runat="server" 
                            AutoPostBack="True" 
                            onselectedindexchanged="ddlUSerCategoryName_SelectedIndexChanged">
                        </asp:DropDownList>
                            
                    </li>
                    
                    
                 </ul>
        
            <asp:Label ID="lblSeccessMsg" CssClass="success" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblErorrMsg" CssClass="error" runat="server" Visible="false"></asp:Label>
           
                
                <asp:Repeater ID="rptUserMaster" runat="server">
                       <headertemplate>
                          <table class="grdView"> 
                            <tr>
                                <th>S.No</th>
                                <th>Category</th>
                                <th>EmailId</th>
                                <%--<th>Password</th>--%>
                                <th>Mobile No</th>
                                <th>User Name</th>
                                <th>Course</th>
                                <th>Password</th>
                                <th>City</th>
                                <th>Action</th>
                                
                            </tr>
      
                        </headertemplate>
                        <itemtemplate>
                           <tr>
                                <td><%# Eval("SrNo") %> </td>
                                <td><%# Eval("UserCategoryName")%></td>
                                <td><%# Eval("UserEmailid")%></td>
                             
                                <td><%# Eval("MobileNo")%></td>
                                <td><%# Eval("UserFullName")%></td>
                                <td><%# Eval("CourseName")%></td>
                                <td><%# Eval("UserPassword")%></td>
                                <td><%# Eval("CityName")%></td>
                                <td>
                                    <a href='/AdminPanel/User/EditUserDetails.aspx?UserId=<%# Eval("UserId")%>' style="cursor:pointer;"  >Edit</a>
                                   
                                   
                                  </td>
                             </tr>
                         </itemtemplate> 
                         <FooterTemplate>
                        </table></FooterTemplate>
                     </asp:Repeater>
                <AJ:CustomPaging ID="ucCustomPaging"  runat="server" />
            </fieldset>


      <div id="pop_User" class="popup_block" >
      <asp:Label ID="lblPopupMessage" cssClass="success" runat="server" Visible="false"></asp:Label>
        <h2>
            User Edit Update</h2>
       
        
        <asp:HiddenField ID="hndUserId" runat="server" />  <asp:HiddenField ID="hdnGenderId" runat="server" />
        <asp:HiddenField ID="hndCourseId" runat="server" />
        <asp:HiddenField ID="hndCategoryId" runat="server" />
        <asp:HiddenField ID="hndStateId" runat="server" />
        <asp:HiddenField ID="hndCityId" runat="server" />
        <asp:HiddenField ID="hndCountryId" runat="server" />
        <asp:HiddenField ID="hndEmailID" runat="server" />
        <fieldset style="width:800px;">
            <span>
                <asp:Label ID="lblMsg" runat="server" CssClass="alert_success"  Text="" ForeColor="Green"></asp:Label>
            </span>
            <ul style="float:left; width:47%;">
                    
                    <li>
                        <label>User Name</label>
                            <asp:TextBox ID="txtPopupUserName" runat="server" width="120px" onfocus="ClearLabel()" TabIndex="2" ValidationGroup="UserMaster"></asp:TextBox>
                            
                    </li>
                     <li>
                        <label>Gender</label>
                      
                        <asp:RadioButtonList runat="server" ID="rbtGender" name="gender">
                            
                        <asp:ListItem Text="Male" Value="1" Selected="True" ></asp:ListItem>  <asp:ListItem Text="FeMale" Value="2" ></asp:ListItem>
                        </asp:RadioButtonList>
                    </li>
                    <li>
                      
                        <label>D.O.B</label>
                       <asp:TextBox ID="txtDOB" runat="server" Width="120px" ValidationGroup="Exam"></asp:TextBox>
                            
                    </li>
                    <li>
                        <label>Password No:</label>
                            <asp:TextBox ID="txtPopupUserPassword" runat="server" width="120px" onfocus="ClearLabel()" TabIndex="2" ValidationGroup="UserMaster"></asp:TextBox>
                            
                    </li>
                    <li>
                        <label>Mobile No:</label>
                            <asp:TextBox ID="txtPopupUserMobileNo" runat="server" width="120px" onfocus="ClearLabel()" TabIndex="2" ValidationGroup="UserMaster"></asp:TextBox>
                            
                    </li>
                    <li>
                        <label>Phone No:</label>
                        <asp:TextBox ID="txtPopupUserPhoneNo" runat="server" width="120px" onfocus="ClearLabel()" TabIndex="2" ValidationGroup="UserMaster"></asp:TextBox>
                            
                    </li>
                    <li>
                        <label>Co-Address:</label>
                            <asp:TextBox ID="txtPopupCorresPondence" runat="server" width="120px" onfocus="ClearLabel()" TabIndex="2" ValidationGroup="UserMaster"></asp:TextBox>
                            
                    </li>
                   
                    
                 </ul>
                 <ul style="float:right; width:47%;">
                    
                     <li>
                        <label>Per-Address</label>
                            <asp:TextBox ID="txtPopupUserPermanentAddress" runat="server" width="120px" onfocus="ClearLabel()" TabIndex="2" ValidationGroup="UserMaster"></asp:TextBox>
                            
                    </li>
                    <li>
                        <label>Pin Code</label>
                            <asp:TextBox ID="txtPopupUserPinCode" runat="server" width="120px" onfocus="ClearLabel()" TabIndex="2" ValidationGroup="UserMaster"></asp:TextBox>
                            
                    </li>
                    <li>
                        <label>Course Name</label>
                            <asp:DropDownList ID="ddlPopupCourseName" runat="server" width="120px">
                        </asp:DropDownList>
                            
                    </li>
                    <li>
                        <label>Country Name</label>
                            <asp:DropDownList ID="ddlPopupCountryName" runat="server" width="120px">
                        </asp:DropDownList>
                            
                    </li>
                    <li>
                        <label>State Name</label>
                            <asp:DropDownList ID="ddlPopupStateName" runat="server" width="120px">
                        </asp:DropDownList>
                            
                    </li>
                    <li>
                        <label>City Name</label>
                        <asp:DropDownList ID="ddlPopupCityName" runat="server" width="120px"></asp:DropDownList>
                            
                    </li>
                    <li>
                        <label>Category</label>
                            <asp:DropDownList ID="ddlPopupUSerCategoryName" runat="server" width="120px"></asp:DropDownList>
                            
                    </li>
                   
                    <li>
                        <label>User Status:</label>
                       <asp:CheckBox ID="chkPopupUserStatus" runat="server">
                        </asp:CheckBox>
                        
                    </li>
                     
                    <li>
                        <label>&nbsp;</label>
                        <asp:Button ID="btnEdit" runat="server" Text="Update" TabIndex="12" 
                            onclick="btnEdit_Click"/>
                        
                    </li>
                 </ul>
                 </fieldset>
        </div>

 <div id="fade"></div>
<asp:Label ID="lblText" runat="server"  Text=""></asp:Label> 
<div id="divImage" class="loading" >
<img src="/image.axd?Common=Loading.gif"  />
 
 </div>  
   
          </ContentTemplate>
</asp:UpdatePanel>
    <script src="../../Scripts/jquery-1.5.2.min.js" type="text/javascript"></script>
 <script src="../JS/commonscripts.js" type="text/javascript"></script>
        <script type="text/javascript" src="../../Scripts/Autocomplete.js"></script>
        <link rel="stylesheet" type="text/css" href="../../Styles/autoCompliteCSS.css"/>
      
    <script language="javascript" type="text/javascript">

       
        var userListUrl = "../../WebServices/CommonWebServices.asmx/GetUserManagerList";
        var mobileListUrl = "../../WebServices/CommonWebServices.asmx/GetMobileNoList";
        var emailListUrl = "../../WebServices/CommonWebServices.asmx/GetUserEmailIDList";
        BindDropDownCommonForAdminAutoComplete($("#<%=txtUserMobileNo.ClientID %>"), mobileListUrl);
        BindDropDownCommonForAdminAutoComplete($("#<%=txtUserName.ClientID %>"), userListUrl);
        BindDropDownCommonForAdminAutoComplete($("#<%=txtUserEmailId.ClientID %>"), emailListUrl);
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                $(".grdView tr:even").css("background-color", "#f4f4f8");
                $(". grdView tr:odd").css("background-color", "#ffffff");
                BindDropDownCommonForAdminAutoComplete($("#<%=txtUserMobileNo.ClientID %>"), mobileListUrl);
                BindDropDownCommonForAdminAutoComplete($("#<%=txtUserName.ClientID %>"), userListUrl);
                BindDropDownCommonForAdminAutoComplete($("#<%=txtUserEmailId.ClientID %>"), emailListUrl);
               
            }
        }

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
    $(document).ready(function () {
        $(".grdView tr:even").css("background-color", "#f4f4f8");
        $(". grdView tr:odd").css("background-color", "#ffffff");
    });
</script>      
</asp:Content>

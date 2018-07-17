<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="EditUserDetails.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.User.EditUserDetails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<ul class="addPage_utility">
        <li class="fright" style="width: 125px !important;">
            <div class="navbar-inner" style="margin-right: 1%;">
                <a href="UserMaster.aspx" class="viewIco">User Master</a>
                <div class="clear">
                </div>
            </div>
        </li>
    </ul>
    <fieldset>
     <legend>User Details</legend>
               <asp:Image runat="server" ID="imgUser" Width="150px" Height="160px"/>
            <ul class="width48perc fleft">
          
                     <li >
                        <label>Category</label>
                            <asp:DropDownList ID="ddlUserCategory" runat="server"  ></asp:DropDownList>
                           
                            
                    </li>
                    <li>
                        <label>Course</label>
                            <asp:DropDownList ID="ddlCourse" runat="server" >
                        </asp:DropDownList>
           
                            
                    </li>



                    <li>
                        <label>User Name</label>
                            <asp:TextBox ID="txtUserName" runat="server"  TabIndex="2"  ></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvUserName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="User" CssClass="error1" ControlToValidate="txtUserName">
                        Field user name can't be blank
                    </asp:RequiredFieldValidator>        
                    </li>
                    <li>
                        <label>Father Name</label>
                            <asp:TextBox ID="txtFatherName" runat="server"  TabIndex="2" ></asp:TextBox>
                        
                    </li>
                     
                    <li>
                         <asp:UpdatePanel ID="UpdatePanel1" runat="server" ><ContentTemplate>
                        <label>D.O.B</label>
                       <asp:TextBox ID="txtDOB" runat="server" ></asp:TextBox>
                            <asp:ImageButton ID="imgCal" runat="server" Height="20px" Width="20px" ImageUrl="/image.axd?Common=Calendar-icon.png" OnClick="BtnCalenderClick" />
                            <asp:Calendar ID="Calendar1" runat="server" BackColor="#FFFFCC" Visible="False" OnSelectionChanged="Calendar1SelectionChanged"
                                DayNameFormat="Shortest" 
                                Font-Names="Verdana"  ForeColor="#663399" Height="100px" 
                                 ShowGridLines="True" 
                                Width="254px">
                                <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                                <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                <OtherMonthDayStyle ForeColor="#CC9966" />
                                <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                <SelectorStyle BackColor="#FFCC66" />
                                <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" 
                                ForeColor="#FFFFCC" />
                                <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                               </asp:Calendar>
                       
                         <asp:RequiredFieldValidator runat="server" ID="rfvDOB" Display="Dynamic" SetFocusOnError="True" ValidationGroup="User" CssClass="error1" ControlToValidate="txtDOB">
                        Field user dob can't be blank
                    </asp:RequiredFieldValidator>   </ContentTemplate> </asp:UpdatePanel>
                    </li>
                    <li>
                        <label>Gender</label>
                      
                        <asp:RadioButtonList runat="server" ID="rbtGender" RepeatDirection="Horizontal" CssClass="RadioButtonList" name="gender">
                            
                        <asp:ListItem Text="Male" Value="1" Selected="True" >
                            
                        </asp:ListItem>  <asp:ListItem Text="Female" Value="2" ></asp:ListItem>
                        </asp:RadioButtonList>
                    </li></ul>
                    <ul class="width48perc fleft">
                    <li>
                        <label>Email</label>
                            <asp:TextBox ID="txtEmailId" runat="server"  TabIndex="2" ></asp:TextBox>
     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Display="Dynamic" SetFocusOnError="True" ValidationGroup="User" CssClass="error1" ControlToValidate="txtEmailId">
                        Field user email can't be blank
                    </asp:RequiredFieldValidator> 
                    </li>
                    <li>
                        <label>Password:</label>
                            <asp:TextBox ID="txtPassword" runat="server"   TabIndex="2" ></asp:TextBox>
                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" Display="Dynamic" SetFocusOnError="True" ValidationGroup="User" CssClass="error1" ControlToValidate="txtPassword">
                        Field user password can't be blank
                    </asp:RequiredFieldValidator>             
                    </li>
                    <li>
                        <label>Mobile:</label>
                            <asp:TextBox ID="txtMobileNo" runat="server" TabIndex="2" ></asp:TextBox>
                            
                    </li>
                    <li>
                        <label>Phone:</label>
                        <asp:TextBox ID="txtPhoneNo" runat="server" TabIndex="2" ></asp:TextBox>
                            
                    </li>
                     <li>
                        <label>Image:</label>
                        <asp:FileUpload ID="flpImage" runat="server"   TabIndex="2" ></asp:FileUpload>
                            
                    </li></ul>




                    <ul class="width48perc fleft clear">
                    <li >
                        <label>Correspondence Address:</label>
                            <asp:TextBox ID="txtCorrsepondenceaddress" TextMode="MultiLine" runat="server"  TabIndex="2" ></asp:TextBox>
                            
                    </li>
                   
              <li>
                        <label>Permanent-Address</label>
                            <asp:TextBox ID="txtPopupUserPermanentAddress" runat="server" TextMode="MultiLine" TabIndex="2" ></asp:TextBox>
                            
                    </li>
                    <li>
                        <label>Status:</label>
                       <asp:CheckBox ID="chkPopupUserStatus" runat="server">
                        </asp:CheckBox>
                        
                    </li>
                     
                    <li>
                        <label>&nbsp;</label>
                        <asp:Button ID="btnSave" runat="server" Text="Update" TabIndex="12" OnClick="BtnSaveClick" ValidationGroup="User"  
                            />
                        
                    </li>
                    </ul>
                   
                    <ul class="width48perc fleft">
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server" ><ContentTemplate>
                    <li>
                        <label>Country</label>
                            <asp:DropDownList ID="ddlCountry" runat="server" style="min-width: 229px !important;width: 150px;"  AutoPostBack="True">
                        </asp:DropDownList>
                            
                    </li><li>
                        <label>State</label>
                            <asp:DropDownList ID="ddlState" runat="server"   AutoPostBack="True" OnSelectedIndexChanged="DdlStateSelectedIndexChanged">
                        </asp:DropDownList>
                            
                    </li><li>
                        <label>City</label>
                        <asp:DropDownList ID="ddlCity" runat="server"   ></asp:DropDownList>
                            
                    </li>
                   
                       </ContentTemplate> </asp:UpdatePanel>
                        <li>
                        <label>PinCode</label>
                            <asp:TextBox ID="txtPopupUserPinCode" runat="server" TabIndex="2" ></asp:TextBox>
                            
                    </li>
                    
                 </ul>
                 <asp:HiddenField runat="server" ID="hdnUserImage"/>
                 </fieldset>


</asp:Content>

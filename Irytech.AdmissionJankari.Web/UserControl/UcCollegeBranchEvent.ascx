<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcCollegeBranchEvent.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcCollegeBranchEvent" %>
<div class="box1" id="Event" runat="server">
    <h3>
        Event</h3>
        <hr class="hrline" />
    <div id="features" class="boxPlane">
        <asp:Repeater ID="rptCollegeEvent" runat="server">
            <ItemTemplate>
               <div class="divevent">
                    <ol >
                        <li class="width16Percent fleft"><strong class="strongvies">Event Name:</strong></li>
                         <li class="width30Percent fleft" style="border-left:0px solid #fff; "><strong style="color:#000; text-transform:uppercase;"><%# Eval("AjCollegeEventName")%></strong>
                        </li>
                        <li class="width30Percent fleft">
                        
                           <strong style="width:80px;">Event Date:</strong>
                          <span style="font-weight:bold; font-style:italic; font-size:11px;"><%#Convert.ToDateTime(Eval("AjCollegeEventDate")).ToString("dd-MMM,yyyy")%></span>
                      </li>
                        <li class="clearBoth fleft width16Percent">
                        
                             <strong class="strongvies">Event Location: </strong>
                                
                        
                        
                        </li>
                         <li class="width80Percent fleft" style="border-left:0px solid #fff;">
                         
                         <%# Eval("AjCollegeEventLocation")%>
                         <a href="#" id="sndEvent" class="rightImglink fright" onclick='OpenEventAttendence("<%#Convert.ToDateTime(Eval("AjCollegeEventDate")).ToString("dd-MMM,yyyy")%>","<%# Eval("AjCollegeEventName")%>");ClearEventControl();return false;' title="Attending Event">Attending</a>
                         </li>
                           <li class="fleft width16Percent clearBoth">
                        
                             <strong class="strongvies">Event Description:</strong>

                        </li>
                        <li class="width80Percent fleft" style="border-left:0px solid #fff;"><%# Eval("AjCollegeBranchEventDesc")%></li>
                        
                    </ol>
                    <div class="clearBoth"></div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>

<div class="popup_block" id="divCollegeEvent">
<fieldset>
        <ul class="horizontal">
  <li class="fleft">
            <strong class="dspblock">
              <%=Resources.label.Name%></strong>  
            <asp:TextBox ID="txtEventUser" runat="server" TabIndex="2" ToolTip="Enter your name" placeholder="Enter your name"></asp:TextBox>
            <span class="errormsgSpan1">
             <asp:RequiredFieldValidator runat="server" ID="rfvEvetUser" ValidationGroup="register"
                Display="Dynamic" SetFocusOnError="True" CssClass="error" ControlToValidate="txtEventUser"> 
                  Field Name cannot be blank

            </asp:RequiredFieldValidator></span>
        </li>
        <li class="fleft">
           <strong class="dspblock">
               <%=Resources.label.Mobile%></strong> 
            <asp:TextBox ID="txtMobileEvent" runat="server" TabIndex="3" ToolTip="Enter your 10 digit mobile number" placeholder="Enter your 10 digit mobile number"></asp:TextBox>
           <span class="errormsgSpan1">  <asp:RequiredFieldValidator runat="server" ID="rfvEventMobile" ValidationGroup="register"
                Display="Dynamic" CssClass="error" ControlToValidate="txtMobileEvent">
                Field Mobile Number cannot be blank

                </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ValidationExpression="[1-9][0-9]{9}$" runat="server" ID="revMobileEvent" Display="Dynamic" ValidationGroup="register"
                SetFocusOnError="True" CssClass="error" ControlToValidate="txtMobileEvent">  
                Provide 10 digit mobile number

            </asp:RegularExpressionValidator> </span></li> 
            <li class="fleft">
            <strong class="dspblock">
                <%=Resources.label.Email%></strong>
            <asp:TextBox ID="txtEmailIdEvent" runat="server" TabIndex="4" ToolTip="Enter your email id" placeholder="Enter your email id" ></asp:TextBox>
            <span class="errormsgSpan1"> <asp:RequiredFieldValidator runat="server" ID="rfvEmailIdEvent" CssClass="error" ValidationGroup="register"
                Display="Dynamic" ControlToValidate="txtEmailIdEvent"> 
                Field Email cannot be blank

            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ValidationExpression="^([a-zA-Z0-9_\-\.']+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" runat="server" ID="revEmailId" Display="Dynamic"
                ValidationGroup="register" SetFocusOnError="True" CssClass="error" ControlToValidate="txtEmailIdEvent"> 
                 Incorrect Email format, please try again

            </asp:RegularExpressionValidator></span>
             </li> 
             <li class="fleft lftbor" style="font-size:11px;color:Gray; "><input type="checkbox" checked="checked" />I agree T&amp;C and Privacy Policy</li>
             <li class="fleft lftbor">
             <asp:Button runat="server"  Text="I am attending" ID="btnRegister" TabIndex="7" ValidationGroup="register" style="padding:6px 35px;" class="button"
                ToolTip="Please Submit To Register" OnClick="BtnRegisterClick" />
                </li>
                 
        </ul> 
        </fieldset>
        </div>

<asp:HiddenField runat="server" ID="hdnCollegeEvent">
</asp:HiddenField>
<asp:HiddenField runat="server" ID="hdnCollegeDate">
</asp:HiddenField>
        <script type="text/javascript">
            function ClearEventControl() {
                
                $("#<%=txtEventUser.ClientID %>").val("");
                $("#<%=txtMobileEvent.ClientID %>").val("");
                $("#<%=txtEmailIdEvent.ClientID %>").val("");
                document.getElementById("<%=rfvEvetUser.ClientID%>").style.display = "none";
                document.getElementById("<%=rfvEventMobile.ClientID%>").style.display = "none";
                document.getElementById("<%=revMobileEvent.ClientID%>").style.display = "none";
                document.getElementById("<%=rfvEmailIdEvent.ClientID%>").style.display = "none";
                document.getElementById("<%=revEmailId.ClientID%>").style.display = "none";


            }
            function EventMessage() {
                alert("Thank you for showing interest,College will revert you soon");
            }
            function OpenEventAttendence(eventDate, eventName) {
                             $("#<%=hdnCollegeDate.ClientID %>").val(eventDate);
                $("#<%=hdnCollegeEvent.ClientID %>").val(eventName);
                OpenPoup("divCollegeEvent", "550", "sndEvent");
            }
        </script>


<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminProfile.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.AdminProfile" %>
 
<fieldset>
<h2>Profile:</h2>
    <ul class="marginleft style">
        <li>
        
            <asp:Image runat="server" ID="imgAdmin" Width="150px" Height="150px">
            </asp:Image>
        </li>
        <li>
          <strong> <%=Resources.label.Name%></strong>
            <asp:Label runat="server" ID="lblName"></asp:Label>
        </li>
        
        <li>
           <strong> <%=Resources.label.Email%> </strong>  
            <asp:Label runat="server" ID="lblEmailId"></asp:Label>
         </li>
         <li >
            <strong> <%=Resources.label.Mobile%></strong>
              <asp:Label runat="server" ID="lblMobile"></asp:Label>
        </li>
        </ul>
      
   </fieldset>
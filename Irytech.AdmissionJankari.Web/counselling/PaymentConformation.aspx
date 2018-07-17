<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentConformation.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.counselling.PaymentConformation" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">
<div>
<ul>
<li>
<span id="spnMsg" title="Transction Message" runat="server" class="sucess">
 Dear <label id="lblUserName" runat="server"></label>,<br /><br /> We have received your payment against application form: <label id="lblFormNumber" runat="server"></label>.
</span>
</li>

<br />
<li>
<i style=" color:Navy;">You can email us a soft copy of the photograph and document at
                    to  <a class="aColor" href="mailto:info@admissionjankari.com">info@admissionjankari.com.</a>
       </i>
       </li>
       <br />
<li>
 <a class="aColor" href="/Account/UserProfile.aspx"> click here </a> to go your profile and participate in online counselling.

</li>

</ul>
</div>
</asp:Content>
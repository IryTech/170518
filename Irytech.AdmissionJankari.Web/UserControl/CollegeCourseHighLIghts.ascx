<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeCourseHighLIghts.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeCourseHighLIghts" %>
<div class="box1">
<h3 class="clgSearchH2 h2margin">HighLights
   </h3>
   <div class="boxPlane">
   <asp:Repeater ID="rptHighLights" runat="server">
   <ItemTemplate>
   <ul class="vertical marginleft">
   <li class="width5Percent paddingTop"><img src="(/image.axd?Common=rightgray.png" alt="rightTick" /></li>
   <li class="width90Percent"><i class="aColor"><%# Eval("CollegeBranchCourseHighlight")%></i> </li>
   <span class="clearBoth dispBlock"></span>
   </ul>
    
   </ItemTemplate>
   </asp:Repeater>
  
   </div>
   
</div>

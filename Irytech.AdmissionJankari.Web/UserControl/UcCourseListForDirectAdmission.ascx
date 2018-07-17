<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcCourseListForDirectAdmission.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcCourseListForDirectAdmission" %>

<asp:DataList ID="ddlCourseList" runat="server" RepeatColumns="3" Width="100%">
        <ItemTemplate>
        <div class="boxPlane marginall">
        <ol class="marginleft">
        <li><strong><%# Eval("CourseName")%>
         </strong></li>
        <li><img  src='<%# String.Format("{0}{1}","/image.axd?Course=",Eval("CourseImage")==null ?"NoImage.jpg":Eval("CourseImage")) %>' alt='<%# Eval("CourseName") %>' height="80px" width="95%" style="border:1px solid #f1f1f1;" /></li>
        <li><a href="#" class="button">Apply Now </a></li>
        
        </ol>
     </div>
    </ItemTemplate>

</asp:DataList>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RightBanner.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.RightBanner" %>
<%@ Import Namespace="IryTech.AdmissionJankari.BL" %>

	
<asp:DataList ID="dtlRightBanner" runat="server">
        <ItemTemplate>
    
        <ul class="marginleft">
        <li class="border bannerbot">
        <a id="sndHeaderDirectAdmission" href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+(IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse((new Common().CourseName))+("/Get-Direct-Admission")).ToLower()%>'>
                    <img id="imgBanner<%# Eval("AjBannerId")%>" style="margin-left:0px;" width="100%" title='<%# Eval("AjCollegeBranchName")%>' alt='<%# Eval("AjCollegeBranchName")%>'  src='<%# String.Format("{0}{1}","/image.axd?Banner=",string.IsNullOrEmpty(Eval("AjBannerImage").ToString()) ?"NoImage.jpg":Eval("AjBannerImage")) %>' />
            </a>
        </li>
 

        </ul>
    
       </ItemTemplate>
    </asp:DataList>  
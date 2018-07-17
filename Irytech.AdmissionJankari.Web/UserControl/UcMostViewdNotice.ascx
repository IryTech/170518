<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcMostViewdNotice.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcMostViewdNotice" %>
<%@ Register src="CustomPaging.ascx" tagname="CustomPaging" tagprefix="AJ" %>
<asp:UpdatePanel ID="updateMostviewNewd" runat="server">
<ContentTemplate>
<div class="box1" runat="server" id="divPopularNotice">
    <h3 class="streamCompareH3" >Popular Notices</h3>
    <hr class="hrline" />
    <div class="boxPlane">
    <asp:Repeater ID="rptPapulorNotice" runat="server">
         <ItemTemplate>
            <div class="ucDiv">
        <ul class="vertical marginbottom">
        <li>
        <a  title='<%# Eval("NoticeSubject")%>' href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("Notice-Details/"+IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("NoticeSubject")))).ToLower()%>'>
                    <img src='<%# String.Format("{0}{1}","/image.axd?Notice=",Eval("NoticeImage") ?? "NoImage.jpg") %>' alt='<%# Eval("NoticeSubject") %>' height="40px" width="40px" /></a>
                </li>
                <li class="width70Percent">
                
                   <a  title='<%# Eval("NoticeSubject")%>' href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("Notice-Details/"+IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("NoticeSubject")))).ToLower()%>'  >
                       <%# Eval("NoticeSubject")%>
                      
                    </a>
                    </li>
                        <li class="width98Percent" style="margin-top: 2px;"><strong class="fleft publish"><strong
                                    style="color: gray;">
                                    AdmissionJankari</strong> |
                                    <%#Convert.ToDateTime(Eval("NoticeDate")).ToString("dd-MMM-yyyy")%></strong> </li>
                                <li class="width98Percent" style="border-top: 1px solid #e1e1e1; padding-top: 2px;
                                    margin-top: 2px;">
                                    <a class="rightImglink fright"  title='<%# Eval("NoticeSubject")%>'  href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("Notice-Details/"+IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("NoticeSubject")))).ToLower()%>' id="lnkPresident"  >Read More &raquo;</a>
                                </li>
              </ul>
              <div class="clearBoth"></div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
   
    </div>
     <AJ:CustomPaging ID="Pager" runat="server" />
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
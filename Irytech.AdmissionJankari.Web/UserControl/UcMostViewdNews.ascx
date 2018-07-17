<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcMostViewdNews.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcMostViewdNews" %>
<%@ Register Src="CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<asp:UpdatePanel ID="updateMostviewNewd" runat="server">
    <ContentTemplate>
        <div class="box1" id="divMostViewdNews" runat="server">
            <h3 class="streamCompareH3">
                Popular News</h3>
            <hr class="hrline" />
            <div class="boxPlane">
                <asp:Repeater ID="rptPapulorNews" runat="server">
                    <ItemTemplate>
                        <div class="ucDiv">
                            <ul class="vertical marginbottom">
                                <li><a href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("News-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("NewsSubject")))).ToLower()%>'
                                    rel="canonical" title='<%# Eval("NewsSubject")%>'>
                                    <img src='<%# String.Format("{0}{1}","/image.axd?News=",string.IsNullOrEmpty(Convert.ToString(Eval("NewsImage"))) ?"NoImage.jpg":Eval("NewsImage")) %>'
                                        alt='<%# Eval("NewsSubject") %>' height="40px" width="40px" /></a> </li>
                                <li class="width70Percent"><a href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("News-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("NewsSubject")))).ToLower()%>'
                                    rel="canonical" title=" <%# Eval("NewsSubject")%>">
                                    <%# Eval("NewsSubject")%>
                                </a></li>
                                <li class="width98Percent" style="margin-top: 2px;"><strong class="fleft publish"><strong
                                    style="color: gray;">
                                    <%# Eval("NewsBy")%></strong> |
                                    <%#Convert.ToDateTime(Eval("NewsDate")).ToString("dd-MMM-yyyy")%></strong> </li>
                                <li class="width98Percent" style="border-top: 1px solid #e1e1e1; padding-top: 2px;
                                    margin-top: 2px;"><a class="fright rightImglink" title='<%# Eval("NewsSubject")%>' href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("News-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("NewsSubject")))).ToLower()%>'>
                                        Read More &raquo;</a> </li>
                            </ul>
                            <span class="clearBoth dispBlock"></span>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <AJ:CustomPaging ID="Pager" runat="server" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

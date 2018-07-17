<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucLatestNews.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.ucLatestNews" %>
<div class="box1" id="divLatestNews" runat="server">
    <h3 class="streamCompareH3">
        Latest News <a target="_blank" href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot %>Syndication.axd?Path=News'
            title="Subscribe rss feeds for news and articles-admissionjankari.com " class="fright">
            <img src='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot%>image.axd?Common=rssButton.png'
                title="Subscribe rss feeds for new and articles-admissionjankari.com" alt="Rss Feeds" />
        </a>
    </h3>
    <hr class="hrline" />
    <div class="boxPlane">
        <asp:DataList ID="dtlLatestNews" runat="server" Width="100%" Visible="False">
            <ItemTemplate>
                <div class="ucDiv">
                    <ul class="vertical marginbottom">
                        <li><a href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("News-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("NewsSubject")))).ToLower()%>'
                            rel="canonical" title='<%# Eval("NewsSubject")%>'>
                            <img id="imgNewsArticle<%# Eval("NewsId")%>" title='<%# Eval("NewsSubject")%>' alt='<%# Eval("NewsSubject")%>'
                                height="40px;" width="40px;" src='<%# String.Format("{0}{1}","/image.axd?News=",string.IsNullOrEmpty(Eval("NewsImage").ToString()) ?"NoImage.jpg":Eval("NewsImage")) %>' />
                        </a></li>
                        <li class="width60Percent"><a href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("News-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("NewsSubject")))).ToLower()%>'
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
                        <span class="clearBoth dispBlock"></span>
                    </ul>
                    <div class="clearBoth">
                    </div>
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>

</div>

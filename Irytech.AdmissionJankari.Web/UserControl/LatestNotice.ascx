<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LatestNotice.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.LatestNotice" %>

<div class="box1">
    <h3 class="streamCompareH3">
        Latest Notices <a target="_blank" href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot %>Syndication.axd?Path=Notices'
            title="Subscribe rss feeds for latest notices-admissionjankari.com " class="fright">
            <img src='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot%>image.axd?Common=rssButton.png'
                title="Subscribe rss feeds for notices-admissionjankari.com" alt="Rss Feeds" />
        </a>
    </h3>
    <hr class="hrline" />
    <div class="boxPlane" style="overflow: hidden;">
        <asp:DataList ID="dtlLatestNotice" Width="100%" runat="server" Visible="False">
            <ItemTemplate>
                <div class="ucDiv">
                    <ul class="vertical">
                        <li><a title='<%# Eval("NoticeSubject")%>' href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+"notice-details/"+IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("NoticeSubject")))%>'>
                            <img src='<%# String.Format("{0}{1}","/image.axd?Notice=",string.IsNullOrEmpty(Eval("NoticeImage").ToString()) ?"NoImage.jpg":Eval("NoticeImage")) %>'
                                alt='<%# Eval("NoticeTitle") %>' height="40px" width="40px" /></a> </li>
                        <li class="width70Percent"><a title='<%# Eval("NoticeSubject")%>' href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+"notice-details/"+IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("NoticeSubject")))%>'>
                            <%# Eval("NoticeSubject")%>
                        </a></li>
                        <li class="width98Percent" style="margin-top: 2px;"><strong class="fleft publish"><strong
                            style="color: gray;">AdmissionJankari</strong> |
                            <%#Convert.ToDateTime(Eval("NoticeDate")).ToString("dd-MMM-yyyy")%></strong>
                        </li>
                        <li class="width98Percent" style="border-top: 1px solid #e1e1e1; padding-top: 2px;
                            margin-top: 2px;"><a class="rightImglink fright" title='<%# Eval("NoticeSubject")%>'
                                href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("Notice-Details/"+IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("NoticeSubject")))).ToLower()%>'
                                id="lnkPresident">Read More &raquo;</a> </li>
                    </ul>
                    <div class="clearBoth">
                    </div>
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>

</div>

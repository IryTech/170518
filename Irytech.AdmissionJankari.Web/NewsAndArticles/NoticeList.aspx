<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeList.aspx.cs" MasterPageFile="~/themes/Blue/Site.Master" Inherits="IryTech.AdmissionJankari20.Web.NewsAndArticles.NoticeList" %>
<%@ Register Src="~/UserControl/UcMostViewdNotice.ascx" TagPrefix="AJ" TagName="MostViewdNotice" %>
<%@ Register Src="~/UserControl/UcMostViewedCollege.ascx" TagPrefix="AJ" TagName="MostViewdCollege" %>
<asp:content id="BodyContent" runat="server" contentplaceholderid="cphBody">
    
     <asp:UpdatePanel runat="server">
        <ContentTemplate>
<div class="five_sixth fleft last">
     <asp:Label ID="lblInform" runat="server" Text="" Visible="false" CssClass="info">
    </asp:Label>
        <div id="fade"></div>
<asp:Label ID="lblText" runat="server"  Text=""></asp:Label> 
<div id="divImage" class="loading" >
<img src="/image.axd?Common=Loading.gif"  />
 
 </div>   
    <div class="four_fifth last fleft border mainBG" id="divNotice" >
    <h1 class="streamCompareH3">Notices
     <a  target="_blank" href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot %>Syndication.axd?Path=Notices' title="Subscribe rss feeds for latest notices-admissionjankari.com " class="fright">
                                               <img src='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot%>image.axd?Common=rssButton.png' title="Subscribe rss feeds for notices-admissionjankari.com" alt="Rss Feeds"
                                                             />
                                             </a>
    </h1>
    <hr class="hrline" />
    <asp:Repeater ID="rptNotice" runat="server">
            <ItemTemplate>
            

            <div class="boxPlane marginall">

            <ul class="vertical marginbottom">
                   
  <li class="width15Percent"><span class="Imgarrow">
           <a class="aColor" title='<%# Eval("NoticeSubject")%>' href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("Notice-Details/"+IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("NoticeSubject")))).ToLower()%>'>
                    <img src='<%# String.Format("{0}{1}","/image.axd?Notice=",string.IsNullOrEmpty(Convert.ToString(Eval("NoticeImage"))) ?"NoImage.jpg":Eval("NoticeImage")) %>'  width="80px" height="80" alt='<%# Eval("NoticeSubject")%>'  title='<%# Eval("NoticeSubject")%>' />
           </a>
                    </span>
                 </li>

            <li class="width80Percent">
              <ul class="horizontal">
              <li class="width98Percent">
              <h2><a class="aColor"title='<%# Eval("NoticeSubject")%>' href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("Notice-Details/"+IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("NoticeSubject")))).ToLower()%>'>
                        <%#Eval("NoticeSubject")%>
                      </a></h2>
                      </li>
                       <li class="width98Percent"><strong class="fleft publish"> Published :  <%#Convert.ToDateTime(Eval("NoticeDate")).ToString("dd  MMM, yyyy")%></strong><strong class="fright publish">By - AdmissionJankari</strong></li>
                      <li class="width98Percent"><span class="paragraph"><%# Eval("NoticeShortDesc")%></span></li>
                  <li class="width98Percent readMore" style="border-top:1px solid #e1e1e1; padding-top:5px;">
                 <a class="rightImglink marginRight"  title='Post your comment for <%# Eval("NoticeSubject")%>'  href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("Notice-Details/"+IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("NoticeSubject")))).ToLower()%>#comment' id="A1"  >Comment &raquo;</a>
                 <a class="rightImglink"  title='<%# Eval("NoticeSubject")%>'  href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("Notice-Details/"+IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("NoticeSubject")))).ToLower()%>' id="lnkPresident"  >Read More &raquo;</a></li>
             </ul>
        
                     
            </li>
          
            </ul>
            <div class="clearBoth"></div>
            </div>

          
          
          </ItemTemplate>
        </asp:Repeater> 
         <asp:Panel runat="server" id="pnlPager" CssClass="pagination">
                           </asp:Panel>
<%--        <ADMJ:Paging ID="Pager" runat="server" />--%>
                  
    </div>
    <div class="one_third fright last">
                <script type='text/javascript' src='http://www.hotelscombined.com/SearchBox/89975'></script>
            </div>
            <div class="one_third fright last">
                <AJ:MostViewdNotice ID="ucMostViewdNotice" runat="server"></AJ:MostViewdNotice>
            </div>
             <div class="one_third fright last">
                <AJ:MostViewdCollege ID="ucMostViewdCollege" runat="server"></AJ:MostViewdCollege>
            </div>
            </div>

<div class="one_sixth last fright border" >

</div>
          </ContentTemplate>
          
    </asp:UpdatePanel>
     <script type="text/javascript">         
         var prm = Sys.WebForms.PageRequestManager.getInstance();
         // Add initializeRequest and endRequest
         prm.add_initializeRequest(prm_InitializeRequest);
         prm.add_endRequest(prm_EndRequest);

         // Called when async postback begins
         function prm_InitializeRequest(sender, args) {
             // get the divImage and set it to visible
             $("#fade").show();
             $("#divImage").show();

         }

         // Called when async postback ends
         function prm_EndRequest(sender, args) {
             $("#fade").hide();
             $("#divImage").hide();

         }
    </script>
      
</asp:content>

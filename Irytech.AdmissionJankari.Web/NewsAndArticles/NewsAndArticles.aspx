<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsAndArticles.aspx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.NewsAndArticles.NewsAndArticles" %>

<%@ Register Src="~/UserControl/UcMostViewdNews.ascx" TagPrefix="AJ" TagName="MostViewdNews" %>
<%@ Register Src="~/UserControl/UcMostViewedCollege.ascx" TagPrefix="AJ" TagName="MostViewdCollege" %>
<asp:content id="BodyContent" runat="server" contentplaceholderid="cphBody">
<div class="five_sixth fleft last">
     <asp:UpdatePanel runat="server">
        <ContentTemplate>
      
    
    <asp:Label ID="lblInform" runat="server" Text="" Visible="false" CssClass="info">
    </asp:Label>

    <div class="four_fifth last fleft border mainBG" >
    <h1><asp:Label ID="lblNews"  runat="server" Visible="false"></asp:Label>
         <a  target="_blank" href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot %>Syndication.axd?Path=News' title="Subscribe rss feeds for news and articles-admissionjankari.com " class="fright">
                                               <img src='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot%>image.axd?Common=rssButton.png' title="Subscribe rss feeds for new and articles-admissionjankari.com" alt="Rss Feeds"
                                                             />
                                             </a>
    </h1>
    <hr class="hrline" />
    <asp:Repeater ID="rptNews" runat="server">
            <ItemTemplate>


            <div class="boxPlane marginall">
            <ul class="vertical marginbottom fleft width95Percent">
          <li class="width15Percent">
              <span class="Imgarrow">
                
                  <a title='<%# Eval("NewsSubject")%>' href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("News-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("NewsSubject")))).ToLower()%>'>

                        <img id="nsdId" title='<%# Eval("NewsSubject")%>' alt='<%# Eval("NewsSubject")%>'  height="80px;"  width="80px;"  src='<%# String.Format("{0}{1}","/image.axd?News=",string.IsNullOrEmpty(Convert.ToString(Eval("NewsImage"))) ?"NoImage.jpg":Eval("NewsImage")) %>' />
                      </a>
                                       </span>
          </li>
            <li class="width80Percent">
            <ul class="horizontal">
            <li class="width98Percent">
                <h2 class="streamCompareH3">
                              
                                <a class="aColor" href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("News-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("NewsSubject")))).ToLower()%>' title='<%# Eval("NewsSubject")%>'>
                       <%# Eval("NewsSubject")%></a>
                                       </h2>
            </li>
            <li class="width98Percent">
                <strong class="fleft publish"> Published : <%#Convert.ToDateTime(Eval("NewsDate")).ToString("dd-MMM,yyyy")%></strong>
            <strong class="fright publish">By - <%# Eval("NewsBy")%></strong> 
            </li>
            <li class="width98Percent">
                <span class="paragraph"><%# Eval("NewsShortDesc")%></span>
            </li>
            <li class="width98Percent readMore" style="border-top:1px solid #e1e1e1; padding-top:5px;">
                <a title='Post your comment for <%# Eval("NewsSubject")%>' href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("News-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("NewsSubject")))).ToLower()%>#comment' class="rightImglink marginRight">Comment &raquo;</a>
                <a title='<%# Eval("NewsSubject")%>' href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("News-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("NewsSubject")))).ToLower()%>' class="rightImglink" >Read More &raquo;</a>
            </li>
            
            </ul>
            </li>
            
            </ul>
            <div class="clearBoth"></div>
            </div>             
            </ItemTemplate>
        </asp:Repeater>
         <asp:Panel runat="server" id="pnlPager" CssClass="pagination">
                           </asp:Panel>
       <%-- <ADMJ:Paging ID="Pager" runat="server" />--%>
        </div>
        
             <div id="fade"></div>
<asp:Label ID="lblText" runat="server"  Text=""></asp:Label> 
<div id="divImage" class="loading" >
<img src="/image.axd?Common=Loading.gif"  />
 
 </div>   
                 
        </ContentTemplate>
       
    </asp:UpdatePanel>
     <div class="one_third fright last">
             <script type='text/javascript' src='http://www.hotelscombined.com/SearchBox/89975'></script>
            </div> 
     <div class="one_third fright last">
                <AJ:MostViewdNews ID="ucMostViewsNews" runat="server"></AJ:MostViewdNews>
            </div> 
      <div class="one_third fright last">
                <AJ:MostViewdCollege ID="ucMostViewdCollege" runat="server"></AJ:MostViewdCollege>
      </div> 
   

  </div>
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

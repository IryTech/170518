<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeNotice.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeNotice" %>
<div class="box1"  runat="server" id="divNotice" visible="false">
 <h3 class="streamCompareH3">Notices</h3>
 <hr class="hrline" />
 <div class="box"> 
  <asp:Repeater ID="rptNotice" runat="server" >
                            <ItemTemplate>
                                <div class="accordion fleft" id="divNoticeAccord" style="margin-bottom:5px;">

	                                  <h3 class="accord"><%#Eval("NoticeSubject")%>&nbsp;&nbsp; 
                                      <span class="fright" style="font-size:11px; color:Gray; font-weight:normal !important;"  id="spnNotice1">Published On: <%# Convert.ToDateTime(Eval("NoticeDate")).ToString("dd MMM,yyyy")%> <span class="fright" style=" display:none; color:Red !important; margin-left:10px;" id="spnNotice">Show</span></span> 
                                      
                                      </h3>

                                      <div class="fleft clearBoth" style=" width:665px;">
                                     <span style="float:left; margin-right:8px; font-size:10px !important; text-align:center; width:15%;"><img src='<%# String.Format("{0}{1}","/image.axd?Notice=",string.IsNullOrEmpty(Convert.ToString(Eval("NoticeImage").ToString())) ?"NoImage.jpg":Eval("NoticeImage")) %>'  width="80px" height="80" alt='<%# Eval("NoticeSubject")%>'  title='<%# Eval("NoticeSubject")%>' /></span>
                                     <span style="float:left; width:82%;"><span style="font-size:11px; color:#415983; margin-top:5px; font-weight:normal !important;" class="dspright">By - AdmissionJankari</span>
                                <span class="paragraph"><%# Eval("NoticeShortDesc")%></span>
                      <span class="dspright" style="font-size:11px !important; margin-bottom:5px;">
                 <a class="rightImglink marginRight"  title='Post your comment for <%# Eval("NoticeSubject")%>'  href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("Notice-Details/"+IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("NoticeSubject")))).ToLower()%>#comment' id="A1"  >Comment &raquo;</a>
                 <a class="rightImglink"  title='<%# Eval("NoticeSubject")%>'  href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("Notice-Details/"+IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("NoticeSubject")))).ToLower()%>' id="lnkPresident"  >Read More &raquo;</a>
                         </span>  </span>
                                                                             
                                      </div>
	                         

                                </div>
                            </ItemTemplate>
                            </asp:Repeater>
                            <div class="clearBoth"></div>
                            </div>
                            </div>
                            
<script type="text/javascript" defer="defer">
    $(document).ready(function () {
        $("#divNoticeAccord h3:first").addClass("active");
        $("#divNoticeAccord div:not(:first)").hide();
        $("#divNoticeAccord h3").click(function () {
            $(this).next("div").slideToggle("slow")
		.siblings("div:visible").slideUp("slow");
            $(this).toggleClass("active");
            $(this).siblings("h3").removeClass("active");
            $(this).find("#spnNotice").html("");
            $(this).find("#spnNotice").html("Hide");
        });

        $("#divNoticeAccord h3").hover(function () {
            $(this).find("#spnNotice").html("");
            $(this).find("#spnNotice").html("Show");
            $(this).find("#spnNotice").show();
        });
        $("#divNoticeAccord h3").mouseout(function () {

            $(this).find("#spnNotice").hide();
        });
    });

</script>
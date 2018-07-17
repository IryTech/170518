<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcReportDonationStory.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcReportDonationStory" %>
<%@ Register src="CustomPaging.ascx" tagname="CustomPaging" tagprefix="AJ" %>
<div class="box1" runat="server" id="divUserDonationStory" Visible="true">
   
<h3><asp:Label runat="server" ID="lblCount"></asp:Label>&nbsp;Donation Story</h3>
<span class="ucshare">
                    <a href="http://twitter.com/share" class="twitter-share-button" >
                     </a>
                     </span>
                     <span class="ucshare">
                            <fb:like layout="button_count"></fb:like>
                        </span>  
                    
                        <div class="g-plusone" data-size="medium">
                        </div>
                         
<hr class="hrline" />
<asp:Repeater ID="rptComment" runat="server">
        <ItemTemplate>
           <div class="boxPlane">
                <ul class="vertical">
                <li class="width15Percent"><img id="collegeImage"  title='<%# Eval("AjUserFullName")%>' alt='<%# Eval("AjUserFullName")%>' height="70px;" width="70px;"  src='<%# String.Format("{0}{1}","/image.axd?User=",string.IsNullOrEmpty(Convert.ToString(Eval("AjUserImage"))) ?"NoImage.jpg":Eval("AjUserImage")) %>' /></li>
                <li class="width80Percent">
                <ul class="vertical marginleft">
                    <li class="width95Percent">
                        <li class="width95Percent"><p style="font-size:12px;"><%# Eval("AjUserStory")%></p></li>
                        <li class="width95Percent "><strong class="fright" > 
                        <%# Eval("AjUserFullName")%> </strong></li>
                    </ul>
                    </li>
                    </ul>
               <span class="dispBlock clearBoth"></span>    
           </div>
        </ItemTemplate>
        
    </asp:Repeater><AJ:CustomPaging ID="ucCustomCommentPaging" runat="server" />
    <span class="dispBlock clearBoth"></span>
     
    </div>
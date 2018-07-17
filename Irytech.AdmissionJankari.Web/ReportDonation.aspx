<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportDonation.aspx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.ReportDonation" %>

<%@ Register Src="~/UserControl/UcReportDonation.ascx" TagPrefix="AJ" TagName="Report" %>
<%@ Register Src="~/UserControl/RightBanner.ascx" TagPrefix="AJ" TagName="RightBanner" %>
<%@ Register Src="~/UserControl/UcReportDonationCollegeList.ascx" TagPrefix="AJ" TagName="Donation" %>
<%@ Register Src="~/UserControl/UcReportDonationStory.ascx" TagPrefix="AJ" TagName="ReportDonation" %>
<asp:content id="BodyContent" runat="server" contentplaceholderid="cphBody">

<div class="five_sixth fleft last">

<div class="four_fifth last fleft" style="margin-top:5px;">
<div class="mainBG border fleft">
<h2 style="padding-left:5px;">Report Donation</h2>
<hr class="hrline" />

<div class="boxPlane marginall fleft">
<h3>Join Our Anti Donation Campaign to Help Fight Corruption in Education</h3><hr class="hrline" />
<p>Admission in top private and government MBBS, Engineering and MBA colleges through donation under the banner Management Quota Seats or NRI seats is completely illegal in India. Still it has been prevailing for quite long back and is still there. Under the procedure of donation admissions, college managements take huge money from candidates to provide them back door admissions. Such admissions are in the form of management quota seats reserved for those students who score comparatively lesser than others. </p>
<p>Admission through donation implies to corruption and thus, stands forth as a national problem. It is not only contaminating healthy competition among students but also installing a wrong mindset. In this way, donation based admissions not only lead to corrupt society but also reflect biased attitude towards merit-listed aspirants. The worse, this system leaves deserving students bereaved of what they deserve and to a certain point affect their carrier. </p>
<p>Institutions and student’s parents involved into such practices are equally responsible for the malpractice. This is not the time for blame game and lame excuses. Let us take action against this. Lets create India a knowledgeable nation.</p>
<p>We, at Admission Jankari, have taken the initiative in this regard by commencing an anti donation campaign. This is a national-level program designed to help students fight against corruption in education. The points we would be focusing on and taking along are <i>declaring donation completely illegal, help students get admissions on merit basis and eliminate corruption out of education system.</i> </p>
<h3>How to support</h3>
<hr class="hrline" />
<strong>If you are a student or parents, you can support the noble cause</strong>
<ol>
<li>Taking pledge that you will not involve yourself into such practices.</li>
<li>Like and share this campaign.</li>
</ol>
<strong>If you are a college, you can support us by participating with us:</strong>
<ol>
<li>You can make your Management Quota Seats more transparent</li>
<li>You can outsource you selection process to us.</li>
</ol>
<h3>How to report</h3>
<hr class="hrline" />
<p>This campaign is noble and for every student in India and we expect your cooperation and association. If you have encountered a situation where college/tout asked donation or cheated or misguided the admission process, you can report your experience showing or hiding your identity. We confirm the incident and raise alarm so as to save others. We are committed to raise your reported issues to concerned department. To report you just need to write your incidents here. </p>
<h3>How it has benefitted</h3>
<hr class="hrline" />
<p>We have been active on that scenario for past two years and helped many students get admission in reputed institutions without any donation. With your support – and we are sure about that – we’ll succeed in stopping this illegal money-laundering policy by which almost every institute in India has unreasonably earned till now.</p>
<p>All said and done about donation system in educational organizations in India, we need to quickly find a way out. Nothing but a strong anti-donation program backed by we-have-to-just-do-it mentality can lend a hand in this case.</p>
</div>
</div>
<div class="fleft marginTop" style="width:100%">
<AJ:ReportDonation ID="ucReportDonation" runat="server" />
</div>
</div>


<div class="one_third last fright">
<div class="fleft"><AJ:Report ID="ucReport" runat="server" /></div>
<div class="marginTop fleft">
<AJ:Donation ID="ucDonation" runat="server" /></div>

</div>

</div>



 <div class="one_sixth last fleft" style="margin-left:1%;">
    <AJ:RightBanner ID="ucRightBanner" runat="server" />
    </div>
    <div id="fb-root"></div>
</asp:content>

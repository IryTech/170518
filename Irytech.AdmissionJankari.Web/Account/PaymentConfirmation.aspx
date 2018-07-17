<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentConfirmation.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.Account.PaymentConfirmation" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    
     <div runat="server" id="divBanner" Visible="false">
            <h2>To insert banner details,please click here <a href="/account/college-profile?T=5" class="button">Insert Banner</a></h2>
        </div>
         <div class="clearboth"></div>
    <div class="boxPlane mainBG" style="width: 75%; margin: 10px auto;">
        <h2>
          Dear <asp:Label ID="lblName" runat="server"></asp:Label>,
        </h2>
        <hr class="hrline" />
        <div id="sucessMsg" visible="false" runat="server" class="box">
         
            <p>
              
                Thank you for registration. We have received registration amount of rupees <asp:Label ID="lblAmount" runat="server"></asp:Label> for your admission.<br />
                Our executive will be in touch with you in a while regarding your admission in your preferred college for the course.</p>
            <br />
            <strong>Please follow the instructions.</strong>
            <ol style="margin-top: 10px;">
                <li>Send photocopies of relevant document (Mark sheet, Certificates Xth onward) within 15 days
                    <br />
                    of registration to start processing of your application.</li>
                <li>You need to submit rest of the admission amount within 15 days of registration.</li>
                <li>If there are any discrepancies with your document we will cancel your registration and amount will be returned to your account.</li>
                <li>If you have any issue with admission, you can cancel your registration. We will refund your registration amount.</li>
            </ol>
            <br />
            <p>
                We at AdmissionJankari are constantly striving to help students to get admission in best college<br />
                of his choice of college in all India and abroad.
            </p>
            <br />
            <p>
                For any queries related to your registration send us mail at <a href="mailto:info@AdmissionJankari.com">info@AdmissionJankari.com</a> or contact us at<br />
                <strong>+91- 8800 5677 33</strong>
                <br />
                Thank you for registration with AdmissionJankari.</p>
            <br />
            <strong>Join our Anti-Donation Campaign to create a knowledgeable nation</strong><br />
            <br />
            <p>
                Sincerely,</p>
            <div class="regardAj">
                <strong>Team Admission </strong><span>Let’s create a knowledgeable nation...</span>
            </div>
        </div>
        <div id="failureMsg" visible="false" runat="server" class="box">
            <p>
                We are sorry to inform you that your registration with AdmissionJankari failed.<br />
                We regret for the inconvenience caused to you</p>
            <br />
            <p>
                We at AdmissionJankari are constantly striving to help students to get admission in best college<br />
                of his choice of college in all over India and abroad.
            </p>
            <br />
            <p>
                For any queries related to your registration send us mail at <a href="mailto:info@AdmissionJankari.com">info@AdmissionJankari.com </a>or contact us at<br />
                <strong>+91- 8800 5677 33</strong><br />
                Thank you for registration with AdmissionJankari.</p>
            <br />
            <strong>Join our Anit-Donation Campaign to create a knowledgeable nation</strong><br />
            <br />
            <p>
                Sincerely,</p>
            <div class="regardAj">
                <strong>Team Admission Jankari</strong> <span>Let’s create a knowledgeable nation...</span>
            </div>
        </div>
       
       
    </div>
</asp:Content>
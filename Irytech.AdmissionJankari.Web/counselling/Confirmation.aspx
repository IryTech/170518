<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Confirmation.aspx.cs" MasterPageFile="~/themes/Blue/Site.Master" Inherits="IryTech.AdmissionJankari20.Web.counselling.Confirmation" %>
<%@ Import Namespace="IryTech.AdmissionJankari.BL" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">
    <div style="width: 942px; margin: 5px auto; display: block; height: 32px;">
        <img src="/image.axd?Common=confirmation.jpg" /></div>
    <div class="boxPlane mainBG" style="width: 75%; margin: 10px auto;">
        <h2>
            Dear
            <asp:Label ID="lblUserName" runat="server"></asp:Label>
        </h2>
        <div id="sucess" runat="server" class="box">
            <p>
                Thank you for participating in online counseling with AdmissionJankari.<br />
                <br />
                Now you can manage your profile by using your credentials (Email and Mobile).<br />
                <br />
                To Choose your collges <a href="ChooseCollege.aspx" title="Choose your collges">click here </a>
                <br />
                <br />
                For any query related to admission feel free to call us on<strong> +91-8800 5677 33</strong>.<br />
                <br />
                We wish, you have great career ahead!<br />
            </p>
            Regards,
            <div class="regardAj">
                <strong>Team Admission </strong><span>Let’s create a knowledgeable nation...</span> </div>
            </div>
            <div id="failure" runat="server" class="box">
                <p>
                    Thank you for participating in online counseling with AdmissionJankari.<br />
                    <br />
                    Now you can manage your profile by using your credentials (Email and Mobile).<br />
                    <br />
                    You need to complete your payment process with AdmissionJankari so that we can help you in getting admission in top colleges so that you can participate in online counseling<br />
                    <br />
                    To make payment <strong><a href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+  (IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse((new Common().CourseName))+"/Counselling/OnlineTransaction").ToLower() %>' title="Make payment to book your seat now">Click here </a></strong>
                    <br />
                    <br />
                    For any query related to admission feel free to call us on <strong>+91-8800 5677 33</strong>.<br />
                    <br />
                    We wish, you have great career ahead!<br />
                    <br />
                    Regards,<br />
                </p>
                <div class="regardAj">
                    <strong>Team Admission Jankari</strong> <span>Let’s create a knowledgeable nation...</span>
                </div>
            </div>
        </div>
</asp:Content>

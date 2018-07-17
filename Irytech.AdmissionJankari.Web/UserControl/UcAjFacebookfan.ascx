<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcAjFacebookfan.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcAjFacebookfan" %>

<div class="box1" id="divFacebookfanpage">
    <h3 class="streamCompareH3" >Facebook Fan</h3>
    <hr class="hrline" />
    <div class="boxPlane">
  
    <div class="fb-like-box" data-href="http://www.facebook.com/admissionjankari" data-width="auto" data-show-faces="true" data-stream="false" data-header="false"></div>
    </div>
</div>
<div id="fb-root"></div>
<script>    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=228866610572164";
        fjs.parentNode.insertBefore(js, fjs);
    } (document, 'script', 'facebook-jssdk'));</script>
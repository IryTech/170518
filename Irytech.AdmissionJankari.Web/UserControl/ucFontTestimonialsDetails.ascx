<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucFontTestimonialsDetails.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.ucFontTestimonialsDetails" %>
<div class="box1">
    <h3 class="streamCompareH3">
        Testimonial</h3>
    <hr class="hrline" />
    <div id="testimonialSlider" class="boxPlane">
        <ul id="testimonialSliderContent" style="height: 100px;">
            <asp:Repeater ID="dlFontTestmonialsDetails" runat="server">
                <ItemTemplate>
                    <li class="testimonialSliderImage hide">
                        <img class="fleft border marginRight" src='<%# String.Format("{0}{1}","/image.axd?User=",Eval("UserImage")==null ?"NoImage.jpg":Eval("UserImage")) %>' id="Image" height="90px;" width="90px;" alt='<%# Eval("UserImage")%>' />
                        <%# Eval("Testimonials")%>
                        <label class="marginTop displayInline">
                          <span>  </label></span> </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
       
        <div class="clearBoth testimonialSliderImage">
        </div>
    </div>
</div>
<script src="/Js/S3Silder.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#testimonialSlider').s3Slider({

            timeOut: 6000
        });

    });
</script>
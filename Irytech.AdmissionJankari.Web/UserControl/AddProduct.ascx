<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddProduct.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.AddProduct" %>
<%@ Import Namespace="IryTech.AdmissionJankari.BL" %>
<asp:HiddenField ID="hdnCollegeId" runat="server" ClientIDMode="Static" />
<%@ Register Src="~/UserControl/LeftContentOnProductList.ascx" TagPrefix="AJ" TagName="ProductContent" %>
<%@ Register Src="~/UserControl/BannerProduct.ascx" TagPrefix="AJ" TagName="BannerProduct" %>
<%@ Register Src="~/UserControl/TextAdsProduct.ascx" TagPrefix="AJ" TagName="TextProduct" %>
<%@ Register Src="~/UserControl/SponseredColleges.ascx" TagPrefix="AJ" TagName="OurClients" %>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <div id="AdvetiseMentContent">
            <div>
                <AJ:ProductContent ID="ProContent" runat="server"></AJ:ProductContent>
            </div>
            <div id="pricing" class="fright pricing" style="border-radius: 2px; padding: 2px;
                border: 1px solid #005eb1; box-shadow: 0px 2px 4px 2px;">
                <p class="premiumDetails">
                    <span class="colorBlue" style="display: block;">Purchase product to be premium member.</span>
                </p>
                <hr class="hrLineAdvertise" />
                <div class="fright" style="margin-right: 25px;">
                    <a href="javascript::" onclick="CheckCartCount()" class="spanTotalCount">Wish List[
                        <span id="spanCartTotalCount">0</span> ] </a>
                </div>
                <div class="clearBoth">
                </div>
                <ul>
                    <asp:Repeater ID="dtlProduct" runat="server" OnItemDataBound="dtlProduct_ItemDataBound">
                        <ItemTemplate>
                            <li class="column-<%# Container.ItemIndex+1%> <%#Convert.ToBoolean(Eval("AjIsBestValue"))?"popular":""%>">
                                <%#Convert.ToBoolean(Eval("AjIsBestValue"))?"<div class='popular-tag'>Best Offer</div>":"" %>
                                <div class="plan">
                                    <h3>
                                        <%# Eval("AjProductName")%></h3>
                                    <span class="price">
                                        <%# !string.IsNullOrEmpty(Convert.ToString(Eval("AjProductDiscount"))) ? (Common.ConvertRupee(Convert.ToString(Convert.ToInt32(Eval("AjProductAmount")) - (Convert.ToInt32(Eval("AjProductAmount")) * Convert.ToInt32(Eval("AjProductDiscount")) / 100)))) : Common.ConvertRupee(Convert.ToString(Eval("AjProductAmount")))%>
                                        <span>/month</span> </span>
                                    <div class="sale-pricing">
                                        <div class="sale-pricing">
                                            <%# !string.IsNullOrEmpty(Convert.ToString(Eval("AjProductDiscount"))) ? "<span class='reg-price'><strike>" + Common.ConvertRupee(Convert.ToString(Eval("AjProductAmount"))) + " </strike><span>Save " + Eval("AjProductDiscount") + " %</span></span>" : ""%>
                                        </div>
                                    </div>
                                    <a href="javascript:void(0)" data-cart="<%# Eval("AjAdvertismentDiscountId")%>|1|1"
                                        class="flt-btn flt-btn-orng comboproduct">Add to Cart</a>
                                    <asp:Repeater ID="rptProductDiscount" runat="server">
                                        <ItemTemplate>
                                            <div class="plan-droplist-select">
                                                <%# Eval("Text").ToString() %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="details">
                                    <ul>
                                        <li><strong>Description-:</strong>
                                            <p class="g-toolTip">
                                                <%# Convert.ToString(Eval("AjProductDescription")).Substring(0,20)%>... <span>
                                                    <%# Convert.ToString(Eval("AjProductDescription"))%>.</span>
                                            </p>
                                        </li>
                                        <li><strong>Benefits-:</strong>
                                            <asp:HiddenField ID="hdnProductMasterId" Value='<%# Eval("AjProductMasterId")%>'
                                                runat="server"></asp:HiddenField>
                                            <asp:Repeater ID="rptFeatures" runat="server">
                                                <ItemTemplate>
                                                    <p class="g-toolTip">
                                                        <%# Convert.ToString(Eval("AjProductFeatures")).Substring(0, 20)%>... <span>
                                                            <%# Convert.ToString(Eval("AjProductFeatures"))%>.</span>
                                                    </p>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </li>
                                    </ul>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <div>
                    <div class="clearBoth">
                    </div>
                    <asp:LinkButton ID="lnkBannerMroeProduct" runat="server" ToolTip="show more product"
                        OnClick="lnkBannerMroeProduct_Click">Explore More Product...</asp:LinkButton>
                </div>
            </div>
            <div class="clearBoth">
            </div>
            <div style="display: none">
                <div id="courseContainer" style="width: 450px; height: auto!important">
                    <h2>
                        Choose courses to add Admissionjankari products</h2>
                    <span id="liCourseList">
                        
                    </span>
                </div>
                  <div id="divInsertCourseFirst" style="width: 250px; height: auto!important">
                    <h2 class="info">
                        Sorry you don't have any course,Please insert course first</h2>
                   <input type="button" id="btnCourseFirst" title="Insert Course" onclick="javascript:return GoToCourseTab()"/>
                </div>
            </div>
            <div style="display: none" id="divCartNone">
                <div id="divCart" style="width: 950px;">
                </div>
            </div>
            <div class="clearBoth">
            </div>
            <div class="clearBoth">
            </div>
            <div runat="server" id="divMoreProduct" visible="False" style="margin-top: 20px;">
                <asp:LinkButton ID="lnkHideProduct" runat="server" ToolTip="hide this product listing"
                    Visible="False" OnClick="lnkHideProduct_Click" CssClass=" fleft cart-remove-item fk-inline-block"></asp:LinkButton>
                <div style="margin-top: 15px;">
                    <AJ:BannerProduct ID="BannerProd" runat="server"></AJ:BannerProduct>
                </div>
                <AJ:TextProduct ID="UcTextProduct" runat="server"></AJ:TextProduct>
                <div class="clearBoth">
                </div>
                <%--<div id="divTextAds" class='boxPlane outerbox fleft' style="display: none; margin-left: 100px">
        </div>--%>
            </div>
            <div style="margin: 15px 15px 125px 125px">
                <AJ:OurClients ID="ucOurClients" runat="server"></AJ:OurClients>
            </div>
        </div>
        <input type="hidden" id="cartCount" />
        <input type="hidden" id="hdnProductId" />
        <input type="hidden" id="hdnAdvertismentType" />
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/Js/ProductAds.js" type="text/javascript"></script>
<script src="/Js/jquery.fancybox-1.3.1.js" type="text/javascript"></script>
<link href="/Styles/jquery.fancybox-1.3.1.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">

    GetProductCount();

    function SuccessCartProductCallBack(response) {
        BindCart(response.d, CustomFunctionForCart);
    }

    var solazy = {};
    (solazy.$);
    var planBox6Ui = {
        opt: {},
        init: function (t) { planBox6Ui.opt = t, planBox6Ui.bindUI(); },
        showTerm: function (t) {

            if (t) {
                var a = t.attr("data-view").split("|"), n = t.parents(".plan"), e = a[0], l = a[1], i = a[2];
                n && (n.children(".price").html(e + "<span>" + planBox6Ui.opt.duration_label + "</span>"), n.find(".reg-price").css("visibility", 0 < parseInt(i) ? "visible" : "hidden"), n.find(".reg-price").html("<strike>" + l + "</strike><span>" + planBox6Ui.opt.savings_label + " " + i + "%</span>"))
            }
        },
        bindUI: function () {
            $(planBox6Ui.opt.selector + " > ul > li").click(function () {

                var t = $(this), a = t.parents(".plan-droplist-select");
                t.siblings("li").removeClass("selected"), t.addClass("selected"), planBox6Ui.showTerm(t),
                    planBox6Ui.changeSelected(t, a)
            }),
                $(planBox6Ui.opt.selector).click(function () {

                    var t = $(this);
                    t.hasClass("droplist-open") ? t.removeClass("droplist-open") : t.addClass("droplist-open")
                }), $(".plan-droplist-select").mouseleave(function () { $(this).removeClass("droplist-open") })
        },
        changeSelected: function (t, a) {

            var n = t.find("div[data-main=true]").html();
            a.find(".plan-droplist-selected").html(n), t.parents(".plan").find("a.flt-btn-orng").attr("data-cart", t.data("main")), $("#frmAddToCart input#product").val(t.attr("data-main"))
        }
    };
    planBox6Ui.init({
        selector: '.plan-droplist-select',
        duration_label: '/month',
        savings_label: 'Save'
    });



    $("a.comboproduct").click(function () {
        var t = $(this).attr("data-cart");
        GetCollegeCourseForThisProduct(t.split('|')[0], 1);
    });

    function SetAdvertiseBanner() {
        $(".tab_content").hide();
        $("#tabAdvertise1").show();
        $("#ulBasics li").removeClass("active");
        $("#liTab").addClass("active");
        $("#<%=UcTextProduct.ClientID %>").focus();
    }

</script>
<style>
       .outerbox 
    {
           width: 100%;
       border: solid 1px #ccc;
margin: 0 0 10px 0;
padding: 10px 10px;
background: #fff;

}
    .innerbox 
    {
        width: 320px;
       border: solid 1px #ccc;
margin: 0 0 3px 0;
padding: 10px 10px;
background: #fff;
box-shadow: 1px 4px 5px #aaa;
}
.scrollit {
    overflow:scroll;
    min-height: 140px;
    max-height: 380px;
}

.cart-remove-item {
background: url('/image.axd?Common=delete-67bd9c5b.png') no-repeat 0 0 transparent;
width: 18px;
height: 18px;
}
.fk-inline-block {
display: inline-block;
zoom: 1;
}
.tblTitle{
   position:absolute;
    top:15px;
    margin-bottom:30px;
    background:lightblue;width: 100%!important;
}
.tblTitle{
   position:absolute;
    top:0px;
    margin-bottom:30px;
    background:lightblue;
}
.tblTitle td{
    height:20px;
    width:15%;
    font-size:14px;
 
}
   .pricing {
       margin:0 auto;width: 1000px;
   }
    .pricing>
ul{margin-left:10px}
    .pricing>ul>li{background:#ededed;float:left;margin:20px 0 0 19px;position:relative;text-align:center;width:314px;zoom:1}
    .pricing>ul>li.column-1{z-index:3}
    .pricing>ul>li.column-2{z-index:2}
    .pricing>ul>li.column-3{z-index:1}
    .pricing>ul>li:first-child{margin-left:0}
    .pricing ul li .plan{border:1px solid #dadada;border-bottom:0;color:#333;margin-right:16px;padding:20px 0 0;width:312px}
    .pricing ul li .plan h3{font-size:25px;font-weight:400;margin: 0 0 15px}
    .pricing ul li .plan .on-sale{color:#ff7800;display:block;font-size:14px;font-weight:bold;margin-bottom:5px}
    .pricing ul li .plan .price{display:block;font-size:26px;margin-bottom:5px}
    .pricing ul li .plan .price span{font-size: 14px;}
    .details>ul{*margin-bottom:-20px;}
    .pricing ul li .plan .sale-pricing{min-height:18px}
    .pricing ul li .plan .reg-price{display:block;font-size:16px;line-height:18px}
    .pricing ul li .plan .vat-price{display:block;font-size:14px;line-height:18px}
    .pricing ul li .plan .reg-price>span{color:#ff7800!important;font-weight:600;margin-left:5px}
    .save{color:#ff7800!important;font-weight:600;margin-left:5px}
    .pricing ul li .plan a.flt-btn-orng{font-weight: 600;font-size: 16px;margin-top:9px;-webkit-backface-visibility:hidden}
    .pricing .plan-droplist-select{background-color:#fff;border:1px solid #a4a4a4;font-size:14px;height:37px;line-height:37px;margin:15px 8px 0;overflow:hidden;padding:0;position:relative;z-index:3}
    .pricing .plan-droplist-select:hover{cursor:pointer}
    .pricing .plan-droplist-select>.plan-droplist-selected{padding:0 30px 0 11px}
    .pricing .plan-droplist-select>.plan-droplist-selectbtn{background-color:#fff;cursor:pointer;display:block;height:37px;line-height:0;margin:0;padding:0;position:absolute;right:0;top:0;width:25px}
    .pricing .plan-droplist-select>.plan-droplist-selectbtn span{background:url(/image.axd?Common=DrpMenuImage.png) no-repeat -49px -2px transparent;display:block;height:9px;top:17px;left:6px;position:absolute;width:12px}
    .pricing .plan-droplist-select>ul{border:1px solid #bcbcbc;background-color:#fff;cursor:pointer;font-size:12px;list-style:none;left:-999em;margin:-1px;padding:0;position:absolute;z-index:1000}
    .pricing .plan-droplist-select.droplist-open{overflow:visible}
    .pricing .plan-droplist-select.droplist-open>ul{left:0;right:0;top:100%;width:100%}
    .pricing .plan-droplist-select>ul>li{border-top:1px solid #bcbcbc;display:block;overflow:hidden;padding:5px 10px;position:relative;text-decoration:none;word-wrap:break-word;zoom:1;-ms-word-wrap:break-word}
    .pricing .plan-droplist-select>ul>li>div{overflow:hidden;width:100%}
    .pricing .plan-droplist-select>ul>li:first-child{border-top:0 none}
    .pricing .plan-droplist-select>ul>li:hover{background-color:#f5f5f5}
    .pricing .plan-droplist-select>ul>li.selected,.pricing .plan-droplist-select>ul>li.selected:hover{background-color:#e4efc7}
    .pricing .plan-droplist-select>.plan-droplist-selected>span,.pricing .plan-droplist-select>ul>li span{float:left;text-align:right;width:auto}
    .pricing span.sale{color:#d00000}
    .pricing .plan-droplist-select>.plan-droplist-selected>span:first-child,.pricing .plan-droplist-select>ul>li span:first-child{text-align:left;width: 90px;}
    .pricing .plan-droplist-select>.plan-droplist-selected>span.lastChild,.pricing .plan-droplist-select>ul>li span.lastChild{float:right}
    .pricing ul li .details ul li{border:1px solid #dadada;border-top:0;color:#333;font-size:18px;line-height:32px;padding:26px 10px;*height:37px}
    html[lang^=pt] .pricing ul li .details ul li, html[lang^=es] .pricing ul li .details ul li{border:1px solid #dadada;border-top:0;color:#333;font-size:18px;line-height:28px;padding:26px 26px;*height:37px;}
    .pricing ul li .details ul li.no-content{background:#fff;border:0}
    .show-more{display:none}
    .pricing ul li .details ul li:first-child{border-top:0;display:block}
    .pricing ul li .details ul li>span.g-toolTip{background:url(/image.axd?Common=DrpMenuImage.png) no-repeat 7px 7px;content:"";height:18px;position:absolute;padding:9px;width:18px;*margin-top:-8px;*z-index: 1}
    .pricing ul li .details ul li p{display:table;font-size:12px;line-height:12px;padding:10px;margin:-10px auto}
    .pricing ul li .details ul li p:hover{text-decoration: underline}
    .pricing ul li .details ul li p.no-underline{text-decoration:none}
    .pricing ul li .details ul li p.g-toolTip>span{top:28px}
    .pricing ul li .details ul li p.g-toolTip>span:after{border-left:0 solid transparent;border-right:8px solid transparent;border-bottom:5px solid #262626;top:-5px;left:0}
    .pricing ul li .popular-tag{background-color:#808080;color:#fff;font-size:18px;font-weight:600;line-height:31px;position:absolute;text-align:center;width:100%}
    .pricing ul .popular{margin-top:0}
    .pricing ul .popular .plan{padding-top:40px}
    .pricing .more-details{border-top:1px solid #878787;margin:50px 0 0 10px;text-align:center;width:980px}
    .pricing .more-details a{border:1px solid #878787;border-top:1px solid #fff;color:#333;display:inline-block;font-size:14px;margin-top:-1px;line-height:14px;padding:6px 20px 9px;text-decoration:none}

    .pricing ul li .plan a.flt-btn-orng {
        font-weight: 600;
        font-size: 16px;
        margin-top: 9px;
        -webkit-backface-visibility: hidden;
    }
    .flt-btn-orng {
        background: #ff8400;
        border-bottom: 3px solid #f06d00;
    }
    .flt-btn {
        background: #aaa;
        border: 0;
        border-bottom: 3px solid #888;
        color: #fff!important;
        cursor: pointer;
        display: inline-block;
        font-size: 14px;
        line-height: 14px;
        padding: 13px 35px 11px;
        text-align: center;
        text-decoration: none;
    }

    .hide {
        display: none;
    }
    .show {
        display: inline;
    }
   #AdvetiseMentContent .innerDiv 
   {
   
  background-color:#9DCFE4;
  padding:5px 14px;
}
 #AdvetiseMentContent .marginBottom {
  margin-bottom:14px;
}
.advertise_text {
font: Verdana, Geneva, sans-serif;
font-size: 1.24em;
color: #323232;
padding-bottom: 10px;
padding-top: 10px;
text-align: justify;
border-bottom: 1px dotted #B3B3B3;
margin-bottom: 10px;
}
p.premiumDetails {
color: #323232;
font-size: 1.2em;
padding: 0 7px;
word-spacing: 1px;
}
.hrLineAdvertise{
border-bottom: dotted #DEDEDE 1px;
margin: 14px 0;
}
.colorBlue {
color: #3D6295;
}
#ContentAdmission { background: url("/image.axd?Common=shadeBG.png") repeat-x scroll 0% 0% rgb(255, 255, 255);
    
}
</style>

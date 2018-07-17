<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentOption.aspx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.Account.PaymentOption" %>

<%@ Register TagName="Payment" TagPrefix="ADMJ" Src="/UserControl/ProductPayment.ascx" %>
<asp:content id="BodyContent" runat="server" contentplaceholderid="cphBody">
    
   
     <div class="co_rhs_cont fright" id="co_rhs_cont_id">
        <div class="co_rhs_heading">Order Summary</div>
        <div class="clearBoth" style="height:5px;"></div>
        <div id="co_cart_summary">
           <p>
               <strong style="font-size: 14px;width:100px;display: inline-block;color: #f06d00">Items	 :  </strong><strong id="spanCartTotalCount" style="font-size: 12px"></strong>
                    
           </p>
           <p>
               <strong style="font-size: 14px;width:100px;display: inline-block;color: #f06d00">Grand Total	 :  </strong><strong id="spnTotalAmount" style="font-size: 12px"></strong>
           </p>
        </div>
    </div>
    
     <div id="tabBasic" class="fleft" style="overflow: hidden; border: 1px solid #e1e1e1; border-radius: 5px;width:1050px!important" >
                <ul class="tabs" id="ulBasics" style="background-color: transparent;">
                 
                  
                    <li rel="PaymentOption"  class="active" id="liPaymentOption">
                        <a href="javascript:void(0)" class="cursor">Payment Options</a>
                    </li>
                     <li rel="OrderSummary"  id="liOrderSummary" >
                        <a href="javascript:void(0)" class="cursor" onclick="GetProductList()" >Order Summary</a>
                    </li>
                </ul>
                
              
                <div class="tab_container">
                    <div class="tab_content" id="PaymentOption">
                         <ADMJ:Payment ID="UcPayment" runat="server" />

                     </div>
              
                        <div class="tab_content" id="OrderSummary">
                            
                           <div id="divTotalProduct"  style="width: auto;height: 100%">
        
                            </div>
                     </div> 
              </div> 
              
      </div>
     <div  id="Container"> </div>
 <asp:HiddenField runat="server" Value="0" ClientIDMode="Static" ID="totalAmount"/>
    <div class="clearBoth"></div>
    <script type="text/javascript" src="/Js/ProductAds.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            GetProductList();

        });

        function GetProductList() {
            if (location.href.indexOf("id") > -1) {
                var id = location.href.split("?")[1].split("=")[1];
                $("[id*=spanCartTotalCount]").text(1);
                $("[id*=cartCount]").val(1);
                FillCartWithProductByPaymentId(id);

            } else {
                GetProductCount();
                FillCartWithProduct();

            }
        }

        $(".tab_content").hide();
        $(".tab_content:first").show();
        $("#ulBasics li").click(function () {
            $(".tab_content").hide();
            var activeTab = $(this).attr("rel");
            $("#" + activeTab).show();
            $("#ulBasics li").removeClass("active");
            $(this).addClass("active");
            return false;
        });


        function CustomFunctionForCartOnPaymentOption(rowIndex, finalData, totalAmount) {
            if (rowIndex > 0) {
                var totalAmount1 = "₹ " + totalAmount;
                $("#spnTotalAmount").html(totalAmount1);
                $("#divTotalProduct").html(finalData);
                $("#divTotalProduct").show();
                $("#totalAmount").val(totalAmount);
                $("#hdnTotalAmount").val(totalAmount);
                $("#hdnAmount").val(totalAmount);
            } else {
                var totalAmount11 = "₹ " + 0;
                $("#spnTotalAmount").html(totalAmount11);
                $("#totalAmount").val(0);
                $("#hdnTotalAmount").val(0);
                $("#hdnAmount").val(0);

            }
        }

        function SuccessCartProductCallBack(response) {

            if (response.d.length != 14) {
                
                BindCart(response.d, CustomFunctionForCartOnPaymentOption);
            } else {
                location.href = "/account/college-profile?T=4";

            }
        }        

    </script>
    <style>
        .co_rhs_cont
        {
            padding: 5px;
            width: 200px;
            border: 1px solid #ccc;
        }
        .co_rhs_heading
        {
            margin: 3px 5px;
            font-weight: bold;
            font-size: 13px;
            padding-bottom: 5px;
            border-bottom: 1px solid #a5a296;
        }
        .fclear, .cls
        {
            clear: both;
        }
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
        .scrollit
        {
            overflow: scroll;
            min-height: 140px;
            max-height: 380px;
        }
        
        .cart-remove-item
        {
            background: url('/image.axd?Common=delete-67bd9c5b.png') no-repeat 0 0 transparent;
            width: 18px;
            height: 18px;
        }
        .fk-inline-block
        {
            display: inline-block;
            zoom: 1;
        }
        .tblTitle
        {
            position: absolute;
            top: 15px;
            margin-bottom: 30px;
            background: lightblue;
            width: 100% !important;
        }
        .tblTitle
        {
            position: absolute;
            top: 0px;
            margin-bottom: 30px;
            background: lightblue;
        }
        .tblTitle td
        {
            height: 20px;
            width: 15%;
            font-size: 14px;
        }
        
        .flt-btn-orng
        {
            background: #ff8400;
            border-bottom: 3px solid #f06d00;
        }
        .flt-btn
        {
            background: #aaa;
            border: 0;
            border-bottom: 3px solid #888;
            color: #fff !important;
            cursor: pointer;
            display: inline-block;
            font-size: 14px;
            line-height: 14px;
            padding: 13px 35px 11px;
            text-align: center;
            text-decoration: none;
        }
        
        .hide
        {
            display: none;
        }
        .show
        {
            display: inline;
        }
        .g-toolTip
        {
            cursor: pointer;
            position: relative;
        }
        .g-toolTip span
        {
            display: none;
        }
        .g-toolTip:hover span
        {
            display: block;
        }
        .g-toolTip span
        {
            background: #262626;
            color: #fff;
            font-size: 14px !important;
            line-height: 16px;
            left: 35px;
            top: 11px;
            position: absolute;
            padding: 8px;
            text-align: left;
            width: 250px;
            z-index: 3;
        }
        .g-toolTip span:after
        {
            border-top: 0 solid transparent;
            border-bottom: 7px solid transparent;
            border-right: 12px solid #262626;
            content: "";
            top: 0;
            left: -10px;
            position: absolute;
            width: 0;
            height: 0;
            z-index: 99999;
        }
        .g-toolTip span.left
        {
            left: -225px;
        }
        .g-toolTip span.left:after
        {
            border-right: 0;
            border-left: 12px solid #262626;
            left: 220px;
        }
        .sl-img, .host-img
        {
            background-repeat: no-repeat;
            background-position: center;
        }
    </style>
</asp:content>

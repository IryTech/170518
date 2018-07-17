

   function GetCollegeCourseForThisProduct(advertismentDiscountId, advertismentType) {
     
       $("#hdnAdvertismentType").val(advertismentType);
       $("#hdnProductId").val(advertismentDiscountId);
       var url = "/WebServices/CommonWebServices.asmx/BindCourseForProduct";
       var dataQuery = '{advertismentDiscountId:"' + advertismentDiscountId + '",collegeId:"' + $("#hdnCollegeId").val() + '",advertismentType:"' + advertismentType + '"}';
       AjaxPostCallBack(url, dataQuery, ScuccessCourseCallBack);
   }

   function ScuccessCourseCallBack(response) {
       CreateCheckBoxList(response.d);
   }

   function CreateCheckBoxList(checkboxlistItems) {
       $('#liCourseList').html('');
       var xmlDoc = $.parseXML(checkboxlistItems);
       var xml = $(xmlDoc);
       var rowIndex = 0;
       var collegeList = xml.find("Table");
       var table = $('<ul><li><strong>Courses:</strong></li></ul>');
       var counter = 0;
       $.each(collegeList, function(i) {
           rowIndex = ++i;
           table.append($('<li></li>').append($('<input>').attr({
                   type: 'checkbox',
                   name: 'chklistitem',
                   value: $(this).find("AjCollegeBranchCourseId").text().trim(),
                   class: "chklistitem",
                   id: 'chklistitem' + counter
               })).append(
                   $('<label>').attr({
                       for: 'chklistitem' + counter++
                   }).text($(this).find("AjCourseName").text().trim())));

       });
       if (rowIndex > 0) {
           $('#liCourseList').append(table);
           $('#liCourseList').append("<div><a id='ProceedNext' class='button fright divCart'>Proceed To Next</a></div>");
           ShowCart('courseContainer');
       } else {
           ShowCart('divInsertCourseFirst');

       }
   }

   $('#ProceedNext').live("click", function(e) {
       if ($('#courseContainer').find('input[type=checkbox]:checked').length == 0) {
           if ($("[id*=cartCount]").val() <= 0) {
               alert('Please select atleast one course');
           } else {
               FillCartWithProduct();
           }
       } else {
           var value = "";
           $('.chklistitem:checked').each(function(i) {
               value = $(this).val() + "," + value;
           });

           InsertProduct($("#hdnProductId").val(), value.replace(/(\s+)?.$/, ''));
       }
   });
   function FillCartWithProduct() {
       AjaxPostCallBack("/WebServices/CommonWebServices.asmx/GetProductForCart", '{}', SuccessCartProductCallBack);

   }
     function FillCartWithProductByPaymentId(paymentId) {
       AjaxPostCallBack("/WebServices/CommonWebServices.asmx/GetProductByPaymentId", '{paymentId:"'+paymentId+'"}', SuccessCartProductCallBack);

   }
   

   function BindCart(data, customFunctionForCart) {
    var rowIndex = 0;

    var xmlDoc = $.parseXML(data);
    var xml = $(xmlDoc);

    var productList = xml.find("Table");
    var finalData = "";
    var totalAmount = 0;
    var discountSpan = "";
    var totalDiscountSave = 0;
      
    $.each(productList, function (i) {
    
        var discount = ($(this).find("AjProductDiscount").text() != "" || $(this).find("AjProductDiscount").text() != null) ? parseInt($(this).find("AjProductDiscount").text()) : 0;
        if (discount > 0) {
            totalDiscountSave = (parseInt($(this).find("AjMonthValue").text())*parseInt($(this).find("AjProductDiscount").text())) + totalDiscountSave;
            discountSpan = "<br/><br/><strong style='color:green'>(-) Savings: Rs." + discount + "<strong>";
        } else {
            discountSpan = "";
        }
        finalData += "<tr><td>" + (i + 1) + "</td><td style='width:250px;'>" + $(this).find("ProductName").text() + "</td><td>" + $(this).find("AjCourseName").text() + "</td><td><strong>Product Price: Rs. " + $(this).find("ProductAmount").text() + "/month</strong>" + discountSpan + "<br/><br/>Rs." + $(this).find("PayProductAmount").text() + "/month</td><td>" + $(this).find("AjMonthValue").text() + " month</td><td style='width:250px;'><p class='g-toolTip'>" + $(this).find("ProdShortDesc").text() + "<span>" + $(this).find("ProductDescription").text() + "</span></p></td><td><a class='cart-remove-item fk-inline-block' href='javascript:void(0)' onclick='DeleteProduct(" + $(this).find("AjProductPaymentId").text() + ","+$(this).find("CollegeCourseId").text()+");return false;'></a></tr>";
        rowIndex = ++i;
        totalAmount = (parseInt($(this).find("AjMonthValue").text())*parseInt($(this).find("PayProductAmount").text())) + totalAmount;
        discountSpan = "";
    });

    var columnData = "<span id='cartspanTotalCount'>CART( " + rowIndex + " )</span><div class='clearBoth'></div><div class='scrollit'><table class='grdView' style='width:100%'> <tr  style='background:#eff2f7'><td>S.No</td><td>Product</td><td>Course</td><td>Product Amount</td><td>Subscription Time</td><td>Product Description</td><td></td></tr>";
    finalData = columnData + finalData + "</table></div><div class='clearboth'></div><div><table  style='width:100%'><tr><td class='fright'><strong style='font-size: 14px;'>Your Total Savings: </strong><strong style='color:green;'> Rs.  " + totalDiscountSave + "</strong><div style='font-size: 22px;'>Amount Payable:<strong style='color:green;'> Rs. " + totalAmount + "</strong></div></td></tr></table><div>";
    customFunctionForCart(rowIndex, finalData,totalAmount);
}



function ShowCart(divId) {
    $.fancybox({
        'transitionIn': 'elastic',
        'transitionOut': 'elastic',
        'scrolling': 'no',
        'onStart': function (selectedArray, selectedIndex, selectedOptions) {
            $("#fancybox-close").show();
        },
        'onComplete': function () {
            $("#fancybox-close").show();
        },

        href: '#' + divId,
        modal: true
    });
}


  function InsertProduct(productId, collegecourseIds) {
      
        var dataQuery = '{advertisementDiscountId:"' + productId + '",collegeCourseIds:"' + collegecourseIds + '",advertismentType:"' + $("#hdnAdvertismentType").val() + '"}';
        var url = "/WebServices/CommonWebServices.asmx/InsertProduct";
        AjaxPostCallBack(url, dataQuery, InsertSuccessCallBack);

    }

    function InsertSuccessCallBack(response) {
        FillCartWithProduct();

    }

    
      function CheckCartCount() {

        FillCartWithProduct();
    }



    function CloseShoppingCart() {
        $.fancybox.close();
    }


    function HideProduct(control) {
        $("#sndHide").hide();
        $("#sndShow").show();
        $("#divBannerAds").hide();
        $("#divTextAds").hide();
    }
      function ShowMoreProduct() {
        GetBannerProduct();
      //  GetTextAdsProduct();
        $("#sndShow").hide();
        $("#sndHide").show();
    }
 
  function GetBannerProduct() {
        var dataQuery = '{collegeId:"' + $("#hdnCollegeId").val() + '"}';
        var url = "/WebServices/CommonWebServices.asmx/GetBannerAdsProduct";
        AjaxPostCallBack(url, dataQuery, SucessCallBackBannerAds);

    }
     
      function GetTextAdsProduct() {
        var dataQuery = '{collegeId:"' + $("#hdnCollegeId").val() + '"}';
        var url = "/WebServices/CommonWebServices.asmx/GetTextAdsProduct";
        AjaxPostCallBack(url, dataQuery, SucessCallBackTextAds);

    }
    function BindTextAdsProduct(data, advertismentType) {

        var rowIndex = 0;

        var xmlDoc = $.parseXML(data);
        var xml = $(xmlDoc);

        var productList = xml.find("Table");

        var finalData = "<h2 style='padding: 3px; color: #415983; margin-top: 0px; font-size: 12px; font-weight: bold;margin-bottom: 0px; height: 40px; background-color: #eff2f7; overflow: hidden;' >Micro Features Product</h2><div>";

        $.each(productList, function (i) {
            finalData += "<div class='fleft innerbox'><p style='font-size:18px;'>" + $(this).find("AjCollegeAssociationCategoryName").text() + "</p><p class='g-toolTip'><strong>Description:</strong>" + $(this).find("ProdShortDesc").text() + "<span>" + $(this).find("AjDescription").text() + "</span></p><p>₹ " + $(this).find("AjAmount").text() + " /month</p><p><a class='flt-btn flt-btn-orng'  href='javascript:void(0)' onclick='GetCollegeCourseForThisProduct(" + $(this).find("AjCollegeAssociationCategoryId").text() + "," + advertismentType + ");return false;'>Add to Cart</a></p></div>";
            rowIndex = ++i;
        });

        if (rowIndex > 0) {
            finalData = finalData + "</div>";
            $("#divTextAds").html(finalData);
            $("#divTextAds").show();

        }

    }

    function SucessCallBackBannerAds(response) {
        BindBannerProduct(response.d, 2);
    }
    function BindBannerProduct(data, advertismentType) {

        var rowIndex = 0;

        var xmlDoc = $.parseXML(data);
        var xml = $(xmlDoc);

        var productList = xml.find("Table");

        var finalData = "<h2 style='padding: 3px; color: #415983; margin-top: 0px; font-size: 12px; font-weight: bold;margin-bottom: 0px; height: 40px; background-color: #eff2f7; overflow: hidden;' >Banner Product</h2><div>";

        $.each(productList, function (i) {
            finalData += "<div class='fleft innerbox'><p style='font-size:18px;'>" + $(this).find("AjBannerPosition").text() + "-Banner</p><p class='g-toolTip'><strong>Description:</strong>" + $(this).find("ProdShortDesc").text() + "<span>" + $(this).find("AjBannerPostDescription").text() + "</span></p><p>₹ " + $(this).find("AjBannerPostAmount").text() + " /month</p><p><a class='flt-btn flt-btn-orng'  href='javascript:void(0)' onclick='GetCollegeCourseForThisProduct(" + $(this).find("AjBannerPositionId").text() + "," + advertismentType + ");return false;'>Add to Cart</a></p></div>";
            rowIndex = ++i;
        });

        if (rowIndex > 0) {
            finalData = finalData + "</div>";
            $("#divBannerAds").html(finalData);
            $("#divBannerAds").show();

        }

    }
   

    function SucessCallBackTextAds(response) {
        BindTextAdsProduct(response.d, 3);
    }
      
    function DeleteProduct(productPaymentId,collegeCourseId) {
        CustomDeleteProductFunction(productPaymentId,collegeCourseId);
    }

    function CustomDeleteProductFunction(productPaymentId, collegeCourseId) {

        AjaxPostCallBack("/WebServices/CommonWebServices.asmx/DeleteProduct", '{productPaymentId:"' + productPaymentId + '",collegeCourseId:"' + collegeCourseId + '"}', SuccessDeleteCallBack);

    }

    function SuccessDeleteCallBack() {
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

    function CustomFunctionForCart(rowIndex, finalData, totalAmount) {
        $("#divCart").html("");
        if (rowIndex > 0) {
            $("#divCart").append(finalData);
            $("#divCart").append("<div><a href='javascript:void()' id='sndContinueShopping' class='button'  onclick='CloseShoppingCart()'>< Continue Shopping</a><a href='javascript:void(0)' id='sndMakePayment' class='button fright'  onclick='GoForProductPayment()'>Place Order</a><div>");
            ShowCart('divCart');
        } else {
            $.fancybox.close();

        }
        GetProductCount();
    }

    function GetProductCount() {
        AjaxPostCallBack("/WebServices/CommonWebServices.asmx/GetProductCount",'{}', SuccessProductCountCallBack);
       
    }

    function SuccessProductCountCallBack(response) {
        $("[id*=spanCartTotalCount]").text(response.d);
        $("[id*=cartCount]").val(response.d);
    }
 function GoForProductPayment() {
        location.href = "/account/paymentoption.aspx";
    }

 function GoToCourseTab() {
     SetOuterTab('tabs1', 'litabs1', 'ulBasics');
     SetInnerTab('tab2', 'litab2', 'ulTopRanked');
 }
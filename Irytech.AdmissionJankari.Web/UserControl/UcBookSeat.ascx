<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcBookSeat.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcBookSeat" %>
    <script src="../Scripts/jquery-1.5.2.min.js" type="text/javascript"></script>
    <%@ Register Src="~/UserControl/UcDOB.ascx" TagName="DOB" TagPrefix="AJ"%>
     <h4 class="streamCompareH3" style="display:none;" id="hBkSeat" >
    </h4>
    <div style="display:none;" id="divCollegeBookesSeat">
    </div>
    
<div id="divRegister" class='register'>
    <h4 class="streamCompareH3">
        Book your seat
    </h4>
    <hr class="hrline" />
    <div  class="quickquery login" style="border: 1px solid #fff;">
        <div class="boxPlane"  style="width:60%;display:inline;float:left;margin-bottom:5px;">
            <ul class="horizontal" >
                <li class="width107Percent marginlftMinus20Px"><strong class="liststrong textalignRight rightmargin paddingTopBot width25Percent">
                    <%= Resources.label.Name %></strong>
                    <input type="text" id="txtUserNameSeat" title="Enter your name" class="width60Percent fleft" placeholder="Enter your name" tabindex="111" />
                    <span class="errormsgSpan">
                        <label id="lblNameSeatsError" class="error" title="Please Enter Name">
                    </label>
                    </span>
                </li>                
              <li class="width107Percent marginlftMinus20Px"><strong class="liststrong textalignRight rightmargin paddingTopBot width28Percent width25Percent">
                    <%= Resources.label.FName %></strong>
                    <input type="text" id="txtUserFNameSeat" title="Enter your father name" class="width60Percent fleft" placeholder="Enter your father name" tabindex="112" />
                    <span class="errormsgSpan">
                        <label id="lblFNameSeatsError" class="error" title="Please Enter Father Name">
                    </label>
                    </span>
                </li>
				<li class="width107Percent marginlftMinus20Px">
					<strong class="liststrong textalignRight rightmargin paddingTopBot width25Percent">
						<%=Resources.label.Mobile%></strong>
					<input type="text" tabindex="112" id="txtUserMobileSeat" title="Enter your 10 digit mobile number" class="width60Percent fleft" 
						placeholder="Enter your 10 digit mobile number" />
					<span class="errormsgSpan"><label id="lblNumberSeatError" class="error" title="Please Enter Mobile"></label></span>
				</li>
                  <li class="width107Percent marginlftMinus20Px"><strong class="liststrong textalignRight rightmargin paddingTopBot width25Percent">
                    <%=Resources.label.Email%></strong>
                    <input type="text" tabindex="113" class="width60Percent fleft" id="txtUserEmailIdSeat" placeholder="Enter your email id" 
						title="Enter your email id" />
                    <span class="errormsgSpan"><label id="lblEmailIdSeatError" class="error" title="Please Enter Email Id">
                    </label></span>
                </li>

                <li class="width107Percent marginlftMinus20Px"><strong class="liststrong textalignRight rightmargin paddingTopBot width25Percent">
                    Course</strong>                   
                       <select  id="ddlCourse" class="width60Percent" disabled="disabled" ></select>
                            
                      </li>

                <li class="width107Percent marginlftMinus20Px"><strong class="liststrong textalignRight rightmargin paddingTopBot width25Percent">
                    <%=Resources.label.DOB%></strong>
                    <AJ:DOB ID="dob" runat="server" />           
                      <%-- <asp:TextBox ID="txtDobBookSeat" class="width60Percent fleft" TabIndex="114" ToolTip="Enter date of birth" placeholder="Enter date of birth" runat="server"></asp:TextBox>                         
                            <Ajex:CalendarExtender ID="ajaxStartDate"  TargetControlID="txtDobBookSeat"  Format="dd/MM/yyyy" PopupPosition="Right" runat="server" CssClass="CalendarCSS" PopupButtonID="txtDobBookSeat"></Ajex:CalendarExtender>--%>                        
                             <%--<span class="errormsgSpan"><label id="lblDobBookSeat" class="error" title="Please Enter DOb"></label> </span>--%>
                      </li>
            </ul>

            </div>

         <div class="box  marginall" style="float:right;display:block;width:180px;">
        <h3 class="directh3">
            5 Simple Steps to Apply Online</h3>
        <ol class="direct">
            <li></li>
            <li><span class="linedotted">.....</span><span class="mainStepSpan"><span class="stepsSpan">Step
                1</span>Fill the Details</span>  </li>
            <li><span class="linedotted">.....</span><span class="mainStepSpan"><span class="stepsSpan">Step
                2</span>Eligiblity check</span></li>
            <li><span class="linedotted">.....</span><span class="mainStepSpan"><span class="stepsSpan">Step
                3</span>Payment</span></li>
           <li><span class="linedotted">.....</span><span class="mainStepSpan"><span class="stepsSpan">Step
                4</span> Documnet upload verification <span></li>
          <li><span class="linedotted">.....</span><span class="mainStepSpan"><span class="stepsSpan">Step
                5</span> Seat confirmation</span></li>
        </ol>
    </div>
    <div style="display:block;clear:both;" >
            <footer>
            <input type="checkbox" checked="checked" />
            I agree <a href="/Terms-and-Conditions" tabindex="115" target="_blank">T&amp;C</a> and <a href="/Privacy-Policy" tabindex="116" target="_blank">Privacy Policy</a> 
            <input type="button" title="Submit the for Registeration" class="button" onclick="BookSeat()" id="butSubmit" value="Book Your Seat" tabindex="117" />
           <input type="button" title="Clear Field " tabindex="118" id="butClear" onclick="ClearBookSeatControl()" value="Clear"  />
                   
        </footer>
        </div>
      
    </div>
      <span id="spnBookMsg" class="hide">
          <label id="lbllErMsg"  title="Message" class="msg" >
              You must fill in all of the mandatory fields
          </label>
             </span>
</div>

 <script type="text/javascript" defer="defer">

  $("#txtUserNameSeat").focus();
     var pageIndex = 1;
     var pageSize = 4;

     function BookSeat() {
         if (!ValidateBookForm()) {
             $("#spnBookMsg").addClass("hide");
             RegistationBooksSeat();
         }
         else {
             $("#spnBookMsg").removeClass("hide");
         } 
     }


     function RegistationBooksSeat() {
         var ddlDOB = $("#hfDay").val() + "/" + $("#hfMonth").val() + "/" + $("#hfYear").val();
         $("#lblerrMsg").html('');
         var dataQuery = '{"mobileNo":"' + $("#txtUserMobileSeat").val() + '","emailId":"' + $("#txtUserEmailIdSeat").val() + '","name":"' + $("#txtUserNameSeat").val() + '","dob":"' + ddlDOB + '","fName":"' + $("#txtUserFNameSeat").val() + '"}';
         $.ajax({
             type: "POST",
             url: "/WebServices/CommonWebServices.asmx/RegisterStudent",
             data: dataQuery,
             async: false,
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function (response) {
                 var Uid = response.d;
                 Uid = Uid.substr(Uid.indexOf("?") + 1);
                 //var url = "/Test.aspx?id="+Uid;
                 var url = "/counselling/BookYourSeat.aspx?" + Uid;
                 window.location = url;
                 
                 var splitResponse = response.d.split("-status-");
                 if (splitResponse.length > 0) {
                     if (splitResponse[1] == "False") {
                         ClearBookSeatControl();
                         $("#divBookSeat").hide();
                         $("#fade").hide();
                         alert("Sorry,you are register as a individual college,So you canot proceed here");
                         return false;
                     }

                     else {
                         InsertCollegePrefer($("#hdnCourse").val(), $("#hdnCollege").val());
                         InsertCityPrefer($("#hdnCity").val());
                         var msg = InsertInterestedCollegePrefer($("#hdnCollegeCourseId").val());                         
                         CheckCollegeBookSeatStatus();
                     }
                 }
             }, error: function (response) {

             }
         });
    }
    function SendMailToUser(collegeBranchCourseId) {
        var dataQuery = '{"collegeBranchCourseId":"' + collegeBranchCourseId + '"}';

        $.ajax({
            type: "POST",
            url: "/WebServices/CommonWebServices.asmx/SendMailToUserForBookSeat",
            data: dataQuery,
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

              
            }, error: function (response) {

            }
        });
    }
     
     function ValidateBookForm() {
         var emailIds = $("#txtUserEmailIdSeat");
         var monoss = $("#txtUserMobileSeat");
         var names = $("#txtUserNameSeat");
         var fName = $("#txtUserFNameSeat");
         var dob = $("#hfYear");
         var reEmails = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,6}$/;
         var mobileNos = /^[0-9]*$/;
     
         var isErrorRegiss = false;
         if (clientValidate()) {
             isErrorRegiss = true;
         }
         if (fName.val() == "") {
             $("#lblFNameSeatsError").html("Field Father Name cannot be blank");
             fName.focus();
             isErrorRegiss = true;
         } else { $("#lblFNameSeatsError").html(''); }
         
//                  if (dob.val() == "") {
//                      $("#lblDobBookSeat").html("Field date of birth can not be blank");

//                      dob.focus();
//                      isErrorRegiss = true;
//                  } 
//         else if (dob.val().match(/^\d\d?\/\d\d?\/\d\d\d\d$/) == null) {
//                      $("#lblDobBookSeat").html("Invalid Fields selection, please try again");

//                      dob.focus();
//                      isErrorRegiss = true;
//                  }


//         else {
//             $("#lblDobBookSeat").html('');
//         }
        

         if (emailIds.val() == "") {
             $("#lblEmailIdSeatError").html("Field Email cannot be blank");

             monoss.focus();
             isErrorRegiss = true;
         }
         else if (!reEmails.test(emailIds.val())) {
             emailIds.focus();
             $("#lblEmailIdSeatError").html("Incorrect Email format, please try again");
             isErrorRegiss = true;
         } else {
             $("#lblEmailIdSeatError").html('');
         }
         

         if (monoss.val() == "") {
             $("#lblNumberSeatError").html("Field Mobile Number cannot be blank");

             monoss.focus();
             isErrorRegiss = true;
         }
         else if (!mobileNos.test(monoss.val()) && monoss.val() > 0) {
          
             $("#lblNumberSeatError").html("Provide 10 digit mobile number");
             emailIds.focus();
             isErrorRegiss = true;
         }

         else if (monoss.val().length < 10 || monoss.val().length > 10) {
            
             $("#lblNumberSeatError").html("Provide 10 digit mobile number in numeric!");
             monoss.focus();
             isErrorRegiss = true;
         } else {
             $("#lblNumberSeatError").html('');
         }
         if (names.val().length < 2) {
             $("#lblNameSeatsError").html("Field Name cannot be blank");

             names.focus();

             isErrorRegiss = true;
         } else {
             $("#lblNameSeatsError").html('');
         }

         return isErrorRegiss;

     }
     function ClearBookSeatControl() {
         $("#spnBookMsg").addClass("hide");
         $("#txtUserNameSeat").val('');
         $("#txtUserFNameSeat").val('');
         $("#txtUserMobileSeat").val('');
         $("#txtUserEmailIdSeat").val('');
         $("#lblNameSeatsError").html('');
         $("#lblNameSeatsError").html('');
         $("#lblFNameSeatsError").html('');             
         $("#lblEmailIdSeatError").html('');
         $("#lblNumberSeatError").html('');
         $("#lblDobBookSeat").html('');
         $("#errMsg").text('');
     }
   
     function CheckCollegeBookSeatStatus() {
         var dataQuery = '{"branchCourseId":"' + $("#hdnCollegeCourseId").val() + '"}';

         $.ajax({
             type: "POST",
             url: "/WebServices/CommonWebServices.asmx/CheckCollegeBookSeatStatus",
             data: dataQuery,
             async: true,
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function (response) {

                 if (response.d[0] == '1') {
                     CheckUserBookSeatStatus($("#hdnCollegeCourseId").val());
                 } else if (response.d[0] == "0") {

                     $(".register").hide();
                     var url = location.href;

                     if (url.indexOf("college-details") > -1) {
                         alert(url.indexOf("college-details") > -1);
                         GetBookSeatCollege(pageIndex);

                         OpenPoup("divBookSeat", "550", "lnkBook");
                     } else {
                         GetBookSeatCollege(pageIndex);
                         OpenPoup("divBookSeat", "550", "lnkBookSeat");
                     }

                 }
             },
             error: function (xml, textStatus, errorThrown) {
                 alert(xml.status + "||" + xml.responseText);
             }
         });
     }
     function CheckUserBookSeatStatus(branchCourse) {
         $.ajax({
             type: "POST",
             url: "/WebServices/CommonWebServices.asmx/CheckUserBookSeatStatus",
             data: '{}',
             async: true,
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function (response) {
                 if (response.d[0] == "1") {
                     var branchCourseId = response.d[1];
                     var collegeName = response.d[2];

                     if (branchCourseId == branchCourse) {
                         Transaction(branchCourse);
                     }
                     else {
                         if (confirm("You have already booked seat for college-" + collegeName + ".Do you really update your book seat")) {
                             Transaction(branchCourse);
                         }
                     }
                 }
                 else {

                     Transaction(branchCourse);
                 }
             },
             error: function (xml, textStatus, errorThrown) {
                 //alert(xml.status + "||" + xml.responseText);
             }
         });
     }
     function GetBookSeatCollege(index) {
              
         var data = '{"pageNumber":"' + (index - 1) + '","pageSize":" ' + pageSize + '","courseId":"' + $("#hdnCourse").val() + '"}';

         $.ajax({
             type: "POST",
             url: "/WebServices/CommonWebServices.asmx/GetBookSeatCollege",
             data: data,
             async: true,
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function (response) {
                 $(".loading").remove();
                 var totalCount = response.d.TotalRecords;
                      
                 BindCollegeList(response, totalCount);
             },
             error: function (xml, textStatus, errorThrown) {
                 $(".loading").remove();
                 alert(xml.status + "||" + xml.responseText);
             }
         });
              }
              function BindCollegeList(response, totalCount) {
                  $('#divCollegeBookesSeat').html("");
                    $(".register").css("display", "none!important");
                     $(".register").hide();
                    var imageData;
                        var bookSeat;
                        $("#hBkSeat").html("Available College To Book Your Seat For Course-" + $("#hdnCourseValue").val());

                        $.each(response.d.MessageList, function (i, client) {
                           
                          imageData = "";
                          if (client.BookSeatStatus) {
                            if (client.CollegeBranchLogo == null || client.CollegeBranchLogo == "") {
                                  imageData = "<img  src='/image.axd?College=NoImage.jpg' alt='" + client.CollegeBranchName + "' height='70px' width='70px'/>";
                                }
                            else {
                              imageData = "<img  src='/image.axd?College=" + client.CollegeBranchLogo + "' alt='" + client.CollegeBranchName + "' height='70px' width='70px'/>";
                                }

               bookSeat = "<a rel='canonical' id='" + client.BookSeatPaymentEncrpted + "'  onclick='GetPayment(this," + client.CollegeBranchCourseId + ");return false;'  href='#' id='participate' class='rightImglink borderbtn' title='Book your Seat for college " + client.CollegeBranchName + "'>Book Your Seat</a>";
               $('#divCollegeBookesSeat').append("<div class='TabStyle' itemscope itemprop='http://schema.org/EducationalOrganization'><ul><li class='image Imgarrow marginRight'><a itemprop='url' target='_blank' href='" + client.CollegeUrl + "' rel='canonical' title='" + client.CollegeBranchName + "'>" + imageData + "</a></li><li class='width68Percent'><strong itemprop='college-Name'><a target='_blank' itemprop='url' href='" + client.CollegeUrl + "' rel='canonical' title='" + client.CollegeBranchName + "'>" + client.CollegeBranchName + "</a></strong><span itemprop='establishment'> Est:" + client.CollegeBranchEst + " | " + client.CollegeManagementType + "</span><label class='fright'>" + bookSeat + "</label><li></ul><div class='clearBoth'></div></div>");
           } 
           else { $('#divCollegeBookesSeat').append("<br/><br/><label class='info'>Sorry,No College to book seat regarding course-" + $("#hdnCourseValue").val() + "</label>"); }
       });
     
       if ($('#divCollegeBookesSeat').html().length == 0) { $('#divCollegeBookesSeat').append("<br/><br/><label class='info'>Sorry,No College to book seat regarding course-" + $("#hdnCourseValue").val() + "</label>"); }
         $('#divCollegeBookesSeat').append("<div class='Pager pagination' id='privatePaging'></div>");

         $("#hBkSeat").show();
         $('#divCollegeBookesSeat').show();
         $(".Pager").AdmissionJankari_Pager({
             ActiveCssClass: "current",
             PagerCssClass: "pager",
             PageIndex: pageIndex,
             PageSize: pageSize,
             RecordCount: totalCount
         });
     }

     $(".Pager .page").live("click", function () {        
         pageIndex = parseInt($(this).attr('page'));         
         $(this).after("<span class='loading error'>Please wait<img src='/image.axd?Common=LoadingImage.gif' alt='loading'/></span>");
         GetBookSeatCollege(pageIndex);

     });
 function GetPayment(control, branchCourseId) {

     CheckUserBookSeatStatus(branchCourseId)
 }

 function InsertBookSeatStatus(payment,branchCourseId) {
     var dataQuery = '{"collegeBranchCourseId":"' + branchCourseId + '"}';
     $.ajax({
         type: "POST",
         url: "/WebServices/CommonWebServices.asmx/InsertBookSeatStatus",
         data: dataQuery,
         async: true,
         contentType: "application/json; charset=utf-8",
         dataType: "json",
         success: function (response) {

             if (response.d == "INSERT") {
                 location.href = "/PaymentOptions.aspx?BookSeatPayment=" + payment;
             } else
                 CheckTransactionDetails(payment);
             SendMailToUser(branchCourseId);
         },
         error: function (xml, textStatus, errorThrown) {
             //alert(xml.status + "||" + xml.responseText);
         }
     });
 }
 function CheckTransactionDetails(payment) {

     $.ajax({
         type: "POST",
         url: "/WebServices/CommonWebServices.asmx/GetStudentTransationDetails",
         data: "{}",
         async: true,
         contentType: "application/json; charset=utf-8",
         dataType: "json",
         success: function (response) {

             GetCounsullingStatus(response, payment);

         },
         error: function (xml, textStatus, errorThrown) {
             //alert(xml.status + "||" + xml.responseText);
         }
     });

 }
 function GetCounsullingStatus(data, payment) {

     if (data.d.length > 0) {

         if (data.d[0].StudentPaymentStatus == true) {

             alert("Sorry you have already done  for book seat");

         } else if (data.d[0].StudentPaymentStatus == false) {
             location.href = "/PaymentOptions.aspx?BookSeatPayment=" + payment;
         }
     }
 }
 function Transaction(branchCourseId) {

     var dataQuery = '{"branchCourseId":"' + branchCourseId + '"}';

     $.ajax({
         type: "POST",
         url: "/WebServices/CommonWebServices.asmx/CheckCollegeBookSeatStatus",
         data: dataQuery,
         async: true,
         contentType: "application/json; charset=utf-8",
         dataType: "json",
         success: function (response) {

             if (response.d[0] == '1') {
                 InsertBookSeatStatus(response.d[1], branchCourseId);

             } else {
                 $("#txtUserNameSeat").focus();
                 $(".register").hide();
                 $(".register").hide();
                 var url = location.href;

                 if (url.contains("college-details")) {

                     OpenPoup("divBookSeat", "550", "lnkBook");
                 } else {
                     OpenPoup("divBookSeat", "550", "lnkBookSeat");
                 }
                 CheckCollegeBookSeatStatus();
             }
         },
         error: function (xml, textStatus, errorThrown) {
             alert(xml.status + "||" + xml.responseText);
         }
     });
 }
 </script>
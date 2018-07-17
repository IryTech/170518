<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForwardFriends.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.ForwardFriends" %>

  <div class="box">
  <h3>Forward to a friend</h3>
  <hr class="hrline" />
  <fieldset>
    <ul>
        <li>
<label class="strongD">Your name</label>

<input  type="text" id="txtYourName" style="width:150px;"  />
 <span id="lblYourName" class="error" title="Please Enter Name">
                </span>
</li>
<li>
<label class="strongD">Friend's name</label>

<input name="FriendNameTextBox" type="text" style="width:150px;" id="txtFrndName" />
 <span id="lblFrndName" class="error" title="Please Enter Name">
                </span>
</li>
<li>
<label class="strongD">Friend's email</label>

<input name="FriendEmailTextBox" type="text" style="width:150px;" id="txtFrndEmail" />
 <span id="lblFrndEmail" class="error" title="Please Enter Name">
                </span>
</li>
<li>
<label class="strongD">Message</label>
<textarea id="txtForward" title="Please enter message" style="width:31%;"></textarea>
<span id="lblMsgForward" class="error" title="Please Enter Name">
                </span>
</li>
<li><label class="strongD">&nbsp;</label>
<input type="button"   onclick="return ForwardToFriend()"  value="Send" id="btnForward" class="button"/>
<input type="button" onclick="ClearFrndForm()" value="Cancel" id="ShareCancelButton" class="button"/>
</li>
</ul>
</fieldset>

</div>
 <div id="loading" class="hide" >
                                                    
    <img src='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot%>image.axd?Common=LoadingImage.gif' alt="Loading"
                                                             />
                                                </div>
<script type="text/javascript" defer="defer">

    function ForwardToFriend() {

        var isErrorRegis = false;

        if (!CheckFrndForm()) {

            isErrorRegis = true;
        }
       
        if (isErrorRegis) {
         
            return false;

        } 
        else {
          
            MailForwardToFreind(MailBox);
        }
    }



    function CheckFrndForm() {
       var emailId = $("#txtFrndEmail");
     var yourName = $("#txtYourName");
        var frndName = $("#txtFrndName");
        var message = $("#txtForward");
        var reEmail = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,6}$/;
      var isErrorRegis = false;
      if (yourName.val() == "" ) {
            $("#lblYourName").html("Field Your Name cannot be blank");
            yourName.focus();

            isErrorRegis = true;
        } else {
            $("#lblYourName").html('');
        }

        if (frndName.val() == "" ) {
            $("#lblFrndName").html("Field Friend Name cannot be blank");
            frndName.focus();

            isErrorRegis = true;
        } else {
            $("#lblFrndName").html('');
        }

        if (emailId.val() == "") {
            $("#lblFrndEmail").html("Field Email cannot be blank");

            emailId.focus();
            isErrorRegis = true;
        }
        else if (!reEmail.test(emailId.val())) {
            $("#lblFrndEmail").html("Incorrect Email format, please try again");
            isErrorRegis = true;
        } else {
            $("#lblFrndEmail").html('');
        }
        if (message.val() == "" ) {
            $("#lblMsgForward").html("Field Message cannot be blank");
            message.focus();

            isErrorRegis = true;
        } else {
            $("#lblMsgForward").html('');
        }
      

        return !isErrorRegis;

    }
    function ClearFrndForm() {
       
        $("#txtYourName").val('');
        $("#txtFrndEmail").val('');
        $("#txtFrndName").val('');
        $("#txtForward").val('');
        $("#lblYourName").html('');
        $("#lblFrndEmail").html('');
        $("#lblFrndName").html('');
        $("#lblMsgForward").html('');
    }
    function MailForwardToFreind(messageShow) {
        $("#loading").removeClass("hide");
        
   var dataQuery = '{"yourName":"' + $("#txtYourName").val() + '","frndName":"' + $("#txtFrndName").val() + '","frndEmail":"' + $("#txtFrndEmail").val() + '","message":"' + $("#txtForward").val() + '","link":"' +location.href + '"}';
       
            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/SendMessageToFrind",
                data: dataQuery,
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: messageShow,
                error: function (response) {
                    
                   $("#loading").addClass("hide");
                },
                
            });
        }
        function   MailBox(reponse) {
            $("#divEmailPop").hide(); $("#fade").hide();
            $("#loading").addClass("hide");
            ClearFrndForm();
                alert("You have successfully shared this documnet to your friend");
         
        }
	</script>
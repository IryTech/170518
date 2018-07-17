<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcCallFromInstitute.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcCallFromInstitute" %>
<div class="iWantCall">
<asp:HiddenField ID="hndCollegeName" runat="server"/>
<asp:HiddenField ID="hndCourseid" runat="server"/>
<asp:HiddenField ID="hndCityName" runat="server"/>
<asp:HiddenField ID="hndCollegeBranchCourseId" runat="server"/>
<strong >I want a call from this Institute</strong>
<div  id="DivMsg" >
        <span style="font-size:11px; color:green;" id="spnCollegeFromInstitute"></span>
    </div>
<ul class="vertical">

<li><asp:TextBox ID="txtName"  Width="215px" runat="server" ToolTip="Enter Name"></asp:TextBox>
        <Ajex:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderName" runat="server" TargetControlID="txtName"
            WatermarkCssClass="roundedTxtbox" WatermarkText="Name">
        </Ajex:TextBoxWatermarkExtender><br />
<asp:TextBox ID="txtMobile" runat="server" Width="215px" ToolTip="Enter Mobile"></asp:TextBox>
        <Ajex:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderMobile" runat="server" TargetControlID="txtMobile"
            WatermarkCssClass="roundedTxtbox" WatermarkText="Mobile">
        </Ajex:TextBoxWatermarkExtender><br />
<asp:TextBox ID="txtEmail" runat="server" Width="215px" ToolTip="Enter Email Id"></asp:TextBox>
        <Ajex:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderEmail" runat="server" TargetControlID="txtEmail"
            WatermarkCssClass="roundedTxtbox" WatermarkText="Email Id">
        </Ajex:TextBoxWatermarkExtender><br />


<input type="button" id="btnSave" tabindex="3" value="Send to College" style="width:218px; font-weight:bold; margin-left:0px;"  onclick="SaveCallFromInstitute()" />

</li>

</ul>


</div>
<script language="javascript" type="text/javascript">
    function validate() {
        if (document.getElementById("<%=txtName.ClientID%>").value == "Name") {
            alert("Name Field Cannot  be blank");
            document.getElementById("<%=txtName.ClientID%>").focus();
            return false;
        }
        if (document.getElementById("<%=txtMobile.ClientID%>").value == "Mobile") {
            alert("Mobile No Cannot  be blank");
            document.getElementById("<%=txtMobile.ClientID%>").focus();
            return false;
        }
        var digits = "0123456789";
        var temp;
        for (var i = 0; i < document.getElementById("<%=txtMobile.ClientID %>").value.length; i++) {
            temp = document.getElementById("<%=txtMobile.ClientID%>").value.substring(i, i + 1);
            if (digits.indexOf(temp) == -1) {
                alert("Please enter correct digits");
                document.getElementById("<%=txtMobile.ClientID%>").focus();
                return false;
            }
        }
        var number = document.getElementById("<%=txtMobile.ClientID %>");
        var numb = number.value.split("/")[0];

        if (numb.length > 11 || numb.length < 10) {
            alert("enter 10 or 11 digits");
            return false;
        }
        if (document.getElementById("<%=txtEmail.ClientID %>").value == "Email Id") {
            alert("Email id Cannot  be blank");
            document.getElementById("<%=txtEmail.ClientID %>").focus();
            return false;
        }
        var emailPat = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
        var emailid = document.getElementById("<%=txtEmail.ClientID %>").value;
        var matchArray = emailid.match(emailPat);
        if (matchArray == null) {
            alert("Your email address seems incorrect. Please try again.");
            document.getElementById("<%=txtEmail.ClientID %>").focus();
            return false;
        }
       
        return true;
    }

    function SaveCallFromInstitute() {
        if (validate() == true) {

            SaveCollegeQuery($("#<%=txtName.ClientID%>").val(), $("#<%=txtEmail.ClientID %>").val(), $("#<%=txtMobile.ClientID %>").val(), 'User want call from the ' + '' + $("#<%=hndCollegeName.ClientID %>").val(), $("#<%=hndCityName.ClientID %>").val(), $("#<%=hndCollegeBranchCourseId.ClientID %>").val(), $("#<%=hndCourseid.ClientID %>").val(), $("#<%=hndCollegeName.ClientID %>").val())
            ClearControlCallfrom();
        }
    }
    $("#<%=txtEmail.ClientID %>").keyup(function (event) {
      
        if (event.keyCode == 13) {

            $('#btnSave').click();
        }
    });

    function SaveCollegeQuery(name, email, number, query, city, collegeCourseId, courseId, collegeName) {

        var dataQuery = '{"name":"' + name + '","email":"' + email + '","number":"' + number + '","query":"' + query + '","cityName":"' + city + '","collegeCourseId":"' + collegeCourseId + '","courseId":"' + courseId + '","collegeBranchName":"' + collegeName + '"}';
        var url = "/WebServices/CommonWebServices.asmx/InsertQueryCallFromInstitute";
        $.ajax({
            type: "POST",
            url: url,
            data: dataQuery,
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
               
                $("#spnCollegeFromInstitute").removeClass("hide"); ;
        //                $("#spnCollegeFromInstitute").addClass("success");
                $("#spnCollegeFromInstitute").html(response.d)
               
            },
            error: function (xml, textStatus, errorThrown) {

                //alert(xml.status + "||" + xml.responseText);
                ClearControlCallfrom();
            }
        });
    }

    function ClearControlCallfrom() {

        $("#<%=txtName.ClientID%>").val('Name');
        $("#<%=txtEmail.ClientID %>").val('Mobile')
        $("#<%=txtMobile.ClientID %>").val('Email Id')
       
       

    }
</script>

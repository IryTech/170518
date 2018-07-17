<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcExamSearch.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcExamSearch" %>
 <div class="clgSearch">
     <asp:HiddenField runat="server" ID="hdnExamName">
     </asp:HiddenField>
 <h1 class="clgSearchH2 paddingBottom">Search exams in India</h1>
<center>
<ul class="vertical">
<li class="width88Percent fleft"><input style="border-radius:5px" class="masterTooltip" type="text"  id="txtExamName" tabindex="1" placeholder="Please enter exam name"   title="Please enter keywords to search by exam " /></li>
<li class="width10Percent">
<input type="button" id="btExamSearch" onclick="ExamSearch()" class="masterTooltip" style="height:35px !important; font-weight:bold; font-size:13px; color:white"   value ="Search" tabindex="2" title="Please submit to exam serach" />
</li>

</ul></center>
</div>
<script src="/Js/CommonFrontScript.js" type="text/javascript"></script>
<script type="text/javascript">
    var numericNo = /^\d*[0-9](|.\d*[0-9]|,\d*[0-9])?$/;
    var url = "/WebServices/CommonWebServices.asmx/GetAllExamPopularList";
    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {
            BindDropDownCommon($("#txtExamName"), url);

        }
    }
    BindDropDownCommon($("#txtExamName"), url);
    function ExamSearch() {
        var examName = $("#txtExamName").val();
        $("#<%=hdnExamName.ClientID %>").val(examName);
        if ((!numericNo.test(examName.trim())) && $("#txtExamName").val().length != 0) {

            location.href = ("/ExamSearch?Exam=" + ChangeUrl(examName)).toLowerCase();
        }
        else {
            alert('Please check the exam name');
        }

    }

    $('#txtCollegeName').keyup(function (event) {
        if (event.keyCode == 13) {

            $('#btnCollegeSearch').click();
        }
    });
    function ChangeUrl(collegeName) {
        collegeName = $.trim(collegeName);
        collegeName = collegeName.replace(/ /g, "_");
        return collegeName.toLowerCase();

    }
</script>
		
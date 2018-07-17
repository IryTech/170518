<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeSearch.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeSearch" %>
<div class="clgSearch">
    <h1 class="clgSearchH2 paddingBottom">
        <%=Resources.label.CollegeSearchHeading%>
    </h1>
    <center>
    <ul class="vertical">
        <li class="width81Percent fleft">
            <input type="text" id="txtCollegeName" tabindex="1" class="image_tooltip" placeholder="Please enter keywords to search college" title="Please enter keywords to search college" /></li>
        <li class="width17Percent">
            <input type="button" style="height:35px;" class="image_tooltip" id="btnCollegeSearch" onclick="CollegeSearch()" value="<%=Resources.label.CollegeSearch%>" tabindex="2" title="Please submit to search college" />
        </li>
             
    </ul>
    </center>
</div>

<script type="text/javascript">


    
    var numericNo = /^\d*[0-9](|.\d*[0-9]|,\d*[0-9])?$/;
    var url = "/WebServices/CommonWebServices.asmx/GetCollegeForFrontEnd";
    BindDropDownCommon($("#txtCollegeName"), url);
  
    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {
            BindDropDownCommon($("#txtCollegeName"), url);
           
        }
    }

    function CollegeSearch() {
        var collegeName = $("#txtCollegeName").val();
       
        if (!numericNo.test(collegeName.trim())&&$("#txtCollegeName").val().length!=0) {

            location.href = ("/CollegeSearch?CollegeName="+ChangeCollege(collegeName)).toLocaleLowerCase();
        }
        else {
            alert('Please Enter the college name');
        }

    }

    $('#txtCollegeName').keyup(function (event) {
        if (event.keyCode == 13) {
            $('#btnCollegeSearch').click();
        }
    });

    function ChangeCollege(collegeName) {
        collegeName = $.trim(collegeName);
        collegeName = collegeName.replace(/ /g, "_");
        return collegeName.toLowerCase();

    }

</script>

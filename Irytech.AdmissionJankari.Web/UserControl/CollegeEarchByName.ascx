<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeEarchByName.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeEarchByName" %>

<div  class="border_strip" style="text-align:center; padding:15px 10px 15px 10px; margin-bottom:10px; background-color:#eef1f6;">
    <span>
        <input id="txtCollegeName" type="text"  tabindex="1" name="txtCollegeName" style="width:86%; height:30px;" class="roundedTxtbox" title="Enter the college name"/>
    </span>
    <span>
        <input type="button" id="btnSearchNew" tabindex="2" title="Click Search College" value="Search" onclick="TrigerEvent()" class="btnComman" style="height:32px;width:75px;"/>
    </span>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        KeyPressEvent();
        $("#btnSearchNew").click(function () {
            var CollegeName = $("#txtCollegeName").val();

            if (!isNaN($("#txtCollegeName")).val && $("#txtCollegeName").val.length != 0) {

                location.href = "../AdvanceSearch.aspx?CollegeName=" + CollegeName
            }
            else {
                alert('Please Enter The College Name');

            }
        });

    });



    function KeyPressEvent() {

        $('#txtCollegeName').keyup(function (event) {
            if (event.keyCode == 13) {

                $('#btnSearchNew').click();
            }
        });
    }

    function TrigerEvent() {
        $('#txtCollegeName').keyup(function (event) {
            if (event.keyCode == 13) {

                var CollegeName = $("#txtCollegeName").val();

                if (!isNaN($("#txtCollegeName")).val && $("#txtCollegeName").val.length != 0) {

                    location.href = "AdvanceSearch.aspx?CollegeName=" + CollegeName
                }
                else {
                    alert('Please Enter The College Name');

                }
            }
        });

    }
    
</script>


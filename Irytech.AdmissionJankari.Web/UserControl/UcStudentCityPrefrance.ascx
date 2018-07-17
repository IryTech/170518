<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcStudentCityPrefrance.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcStudentCityPrefrance" %>

<fieldset>
    <legend>Your City Preference </legend>
    <ul id="tblCityPrefrance">
        <li>
            <label>
              City Preference:</label>
            <asp:TextBox ID="txtCityPrefrance1" Width="400px"  runat="server"  ToolTip="Enter College Preference"></asp:TextBox>
             <a href="javascript:;" onclick="ShowControlExam()" >add more</a>
           </li>
       
      <li id="ExamAppeared2" class="hide">
            <label>
                City Preference:</label>
            <asp:TextBox ID="txtCityPrefrance2" Width="400px"  runat="server"  ToolTip="Enter College Preference"></asp:TextBox>
           
           <a href="javascript:;" onclick="ShowControlExam()" >add more</a>
           <a href="javascript:;" onclick="HideControlExam()">hide </a>
       </li>
       <li id="ExamAppeared3" class="hide">
            <label>
               City Preference:</label>
            <asp:TextBox ID="txtCityPrefrance3"   Width="400px" runat="server"  ToolTip="Enter College Preference"></asp:TextBox>
            <a href="javascript:;" onclick="HideControlExam()" >hide </a>
       </li>
              
        
        
    </ul>

    <script type="text/javascript" defer="defer">
        function ShowControlExam() {
            if ($('#ExamAppeared2').is(':visible')) {
                if ($('#ExamAppeared3').is(':visible')) {

                } else {
                    $("#ExamAppeared3").slideToggle();
                    $('#ExamAppeared3').removeClass("hide");
                    $('#ExamAppeared3').show();
                   
                }
            } else {
                $("#ExamAppeared2").slideToggle();
                $('#ExamAppeared2').removeClass("hide");
                $('#ExamAppeared2').show();
            }

        }
        function HideControlExam() {
            if ($('#ExamAppeared3').is(':visible')) {
                $("#ExamAppeared3").toggle();
                $('#ExamAppeared3').hide();
            } else {
                if ($('#ExamAppeared2').is(':visible')) {
                    $("#ExamAppeared2").toggle();
                    $('#ExamAppeared2').hide();

                }
            }

        }

        function GetAllCityList() {
            $.ajax
               ({
                   type: "POST",
                   url: "/WebServices/CommonWebServices.asmx/GetAllCity",
                   async: true,
                   data: '{}',
                   contentType: "application/json; charset=utf-8",
                   dataType: "json",
                   success: function (msg) {
                       data = msg.d.split(",");
                       $('#<%= txtCityPrefrance1.ClientID %>').autocomplete(data);
                       $('#<%= txtCityPrefrance2.ClientID %>').autocomplete(data);
                       $('#<%= txtCityPrefrance3.ClientID %>').autocomplete(data);
                   }
               });
        }
        $(document).ready(function () {
            GetAllCityList();
        });


        $('#tblCityPrefrance input[id^=ctl00_cphBody_wizardApplyForm_UcICityPrefrance_txtCityPrefrance2]').live('focusout', function () {
            var city1 = $('#<%= txtCityPrefrance1.ClientID %>').val();
            var city2 = $('#<%= txtCityPrefrance2.ClientID %>').val();
            if (city1 == city2) {
                var city2 = $('#<%= txtCityPrefrance2.ClientID %>').val('');
                alert("You have selected same city");
            }
        });
        $('#tblCityPrefrance input[id^=ctl00_cphBody_wizardApplyForm_UcICityPrefrance_txtCityPrefrance3]').live('focusout', function () {
            var city2 = $('#<%= txtCityPrefrance2.ClientID %>').val();
            var city1 = $('#<%= txtCityPrefrance1.ClientID %>').val();
            var city3 = $('#<%= txtCityPrefrance3.ClientID %>').val();
            if (city2 == city3) {
                var city3 = $('#<%= txtCityPrefrance3.ClientID %>').val('');
                alert("You have selected same city");
            }
            if (city1 == city3) {
                var city3 = $('#<%= txtCityPrefrance3.ClientID %>').val('');
                alert("You have selected same city");  
            }
        });
</script>

</fieldset>

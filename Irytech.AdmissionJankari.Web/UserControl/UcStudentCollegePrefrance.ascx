<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcStudentCollegePrefrance.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcStudentCollegePrefrance" %>
<asp:HiddenField ID="hndCourseId" runat="server" />
<fieldset >
    <legend>Your College Preference </legend>
    <ul id="tblCollegePrefrance" class="fleft">
        <li>
            <label>
               College Preference:</label>
            <asp:TextBox ID="txtCollegePrefrance1" Width="400px"  runat="server"  ToolTip="Enter College Prefrance"></asp:TextBox>
             <a href="javascript:;" onclick="ShowControl()" >add more</a>
           </li>
       
      <li id="CollegeAppeared2" class="hide">
            <label>
                College Preference:</label>
            <asp:TextBox ID="txtCollegePrefrance2" Width="400px"  runat="server"  ToolTip="Enter College Prefrance"></asp:TextBox>
           
           <a href="javascript:;" onclick="ShowControl()" >add more</a>
           <a href="javascript:;" onclick="HideControl()">hide </a>
       </li>
       <li id="CollegeAppeared3" class="hide">
            <label>
               College Preference:</label>
            <asp:TextBox ID="txtCollegePrefrance3"   Width="400px" runat="server"  ToolTip="Enter College Prefrance"></asp:TextBox>
            <a href="javascript:;" onclick="HideControl()" >hide </a>
       </li>
              
        
        
    </ul>
   
   


</fieldset>




    <script type="text/javascript" defer="defer">
        function ShowControl() {

            if ($('#CollegeAppeared2').is(':visible')) {
                if ($('#CollegeAppeared3').is(':visible')) {

                } else {
                    $("#CollegeAppeared3").slideToggle();
                    $('#CollegeAppeared3').removeClass("hide");
                    $('#CollegeAppeared3').show();
                }
            } else {
                $("#CollegeAppeared2").slideToggle();
                $('#CollegeAppeared2').removeClass("hide");
                $('#CollegeAppeared2').show();
            }

        }
        function HideControl() {
            if ($('#CollegeAppeared3').is(':visible')) {
                $("#CollegeAppeared3").toggle();
                $('#CollegeAppeared3').hide();
            } else {
                if ($('#CollegeAppeared2').is(':visible')) {
                    $("#CollegeAppeared2").toggle();
                    $('#CollegeAppeared2').hide();

                }
            }

        }

        function GetAllCollegeList() {
            $.ajax
               ({
                   type: "POST",
                   url: "/WebServices/CommonWebServices.asmx/GetCollegeByCourseSearch",
                   async: true,
                   data: '{courseId:"' + $("#<%=hndCourseId.ClientID%>").val() + '"}',
                   contentType: "application/json; charset=utf-8",
                   dataType: "json",
                   success: function (msg) {
                       data = msg.d.split(",");
                       $('#<%= txtCollegePrefrance1.ClientID %>').autocomplete(data);
                       $('#<%= txtCollegePrefrance2.ClientID %>').autocomplete(data);
                       $('#<%= txtCollegePrefrance3.ClientID %>').autocomplete(data);
                   }
               });
           }
         function pageLoad(sender, args) {
             if (args.get_isPartialLoad()) {
                 GetAllCollegeList();
          }
      }
        $(document).ready(function () {
            GetAllCollegeList();
        });
        $('#tblCollegePrefrance input[id^=ctl00_cphBody_wizardApplyForm_UcCollegePrefrance_txtCollegePrefrance2]').live('focusout', function () {
            var College1 = $('#<%= txtCollegePrefrance1.ClientID %>').val();
            var college2 = $('#<%= txtCollegePrefrance2.ClientID %>').val();
            if (College1 == college2) {
                var city2 = $('#<%= txtCollegePrefrance2.ClientID %>').val('');
                alert("You have selected same college");
            }
        });
        $('#tblCollegePrefrance input[id^=ctl00_cphBody_wizardApplyForm_UcCollegePrefrance_txtCollegePrefrance3]').live('focusout', function () {
            var College2 = $('#<%= txtCollegePrefrance2.ClientID %>').val();
            var college1 = $('#<%= txtCollegePrefrance1.ClientID %>').val();
            var college3 = $('#<%= txtCollegePrefrance3.ClientID %>').val();
            if (College2 == college3) {
               $('#<%= txtCollegePrefrance3.ClientID %>').val('');
                alert("You have selected same college");
            }
            if (college1 == college3) {
                 $('#<%= txtCollegePrefrance3.ClientID %>').val('');
                alert("You have selected same college");
            }
        });
</script>
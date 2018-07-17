<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcStudentExamAppeared.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcStudentExamAppeared" %>
<fieldset>
    <legend>Exam Appeared (If any) </legend>
    <ul style="width:95%;">
        <li>
            <label>
            Exam Appeared:</label>
            <asp:TextBox ID="txtExamAppeared1" Width="200px" runat="server"  ToolTip="Exam Appeared"></asp:TextBox><label>Rank/Score:</label>  <asp:TextBox Width="100px" ID="txtExamAppearedRank1" runat="server"  ToolTip="Exam Appeared"></asp:TextBox>
            <sup>Examination name in which you have appeared</sup>
            <img src="/image.axd?Common=QusetionMark.png" class="helpImage" title="Examination name in which you have appeared" alt="Examination name in which you have appeared" />
           
                       
           <a href="javascript:;" onclick="ShowControlExam1()" >add more</a>
           </li>
       
      <li id="1ExamAppeared2" class="hide">
            <label>
                Exam Appeared:</label>
            <asp:TextBox ID="txtExamAppeared2" Width="200px" runat="server"  ToolTip="Exam Appeared"></asp:TextBox>
           <label>Rank/Score:</label>  <asp:TextBox Width="100px" ID="txtExamAppearedRank2" runat="server"  ToolTip="Exam Appeared"></asp:TextBox>
           <a href="javascript:;" onclick="ShowControlExam1()" >add more</a>
           <a href="javascript:;" onclick="HideControlExam1()">hide </a>
       </li>
       <li id="1ExamAppeared3" class="hide clearBoth">
            <label>
                Exam Appeared:</label>
            <asp:TextBox ID="txtExamAppeared3"   Width="200px" runat="server"  ToolTip="Exam Appeared"></asp:TextBox>
           <label>Rank/Score:</label>  <asp:TextBox Width="100px" ID="txtExamAppearedRank3" runat="server"  ToolTip="Exam Appeared"></asp:TextBox>
            <a href="javascript:;" onclick="HideControlExam1()" >hide </a>
       </li>
              
        
        
    </ul>

    <script type="text/javascript" defer="defer">
        function ShowControlExam1() {
            if ($('#1ExamAppeared2').is(':visible')) {
                if ($('#1ExamAppeared3').is(':visible')) {

                } else {
                    $("#1ExamAppeared3").slideToggle();
                    $('#1ExamAppeared3').removeClass("hide");
                    $('#1ExamAppeared3').show();
                }
            } else {
                $("#1ExamAppeared2").slideToggle();
                $('#1ExamAppeared2').removeClass("hide");
                $('#1ExamAppeared2').show();
            }
            
        }
        function HideControlExam1() {
            if ($('#1ExamAppeared3').is(':visible')) {
                $("#1ExamAppeared3").toggle();
                
            } else {
                if ($('#1ExamAppeared2').is(':visible')) {
                    $("#1ExamAppeared2").toggle();
                  
                   
                } 
            }

        }

        function GetAllExamList() {
            $.ajax
               ({
                   type: "POST",
                   url: "/WebServices/CommonWebServices.asmx/GetAllExamListFront",
                   async: true,
                   data: '{}',
                   contentType: "application/json; charset=utf-8",
                   dataType: "json",
                   success: function (msg) {
                       data = msg.d.split(",");
                       $('#<%= txtExamAppeared1.ClientID %>').autocomplete(data);
                       $('#<%= txtExamAppeared2.ClientID %>').autocomplete(data);
                       $('#<%= txtExamAppeared3.ClientID %>').autocomplete(data);
                   }
               });
           }
           $(document).ready(function () {
               GetAllExamList();
           });
           function pageLoad(sender, args) {
               if (args.get_isPartialLoad()) {
                   GetAllExamList();
               }
           }
           $('#tblCityPrefrance input[id^=ctl00_cphBody_wizardApplyForm_StudentExamInfo_txtExamAppeared2]').live('focusout', function () {
               var exam1 = $('#<%= txtExamAppeared1.ClientID %>').val();
               var exam2 = $('#<%= txtExamAppeared2.ClientID %>').val();
               if (exam1 == exam2) {
                   var exam2 = $('#<%= txtExamAppeared2.ClientID %>').val('');
                   alert("You have selected same exam");
               }
           });
           $('#tblCityPrefrance input[id^=ctl00_cphBody_wizardApplyForm_StudentExamInfo_txtExamAppeared3]').live('focusout', function () {
               var exam2 = $('#<%= txtExamAppeared2.ClientID %>').val();
               var exam1 = $('#<%= txtExamAppeared1.ClientID %>').val();
               var exam3 = $('#<%= txtExamAppeared3.ClientID %>').val();
               if (exam2 == exam3) {
                   var city3 = $('#<%= txtExamAppeared3.ClientID %>').val('');
                   alert("You have selected same exam");
               }
               if (city1 == city3) {
                   var exam3 = $('#<%= txtExamAppeared3.ClientID %>').val('');
                   alert("You have selected same exam");
               }
           });

</script>

</fieldset>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcStudentUpdateStreamPrioty.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcStudentUpdateStreamPrioty" %>
<%@ Import Namespace="IryTech.AdmissionJankari.BL" %>

<asp:UpdatePanel ID="updateCollegeList" runat="server">
    <ContentTemplate>
    <asp:HiddenField ID="hndCollegeList" Value="0" runat="server" />
    <div class="four_fifth last fleft border mainBG">
             <div class="mainBG" style="height:530px; overflow-x:scroll;">
            <asp:Label ID="lblSucess" runat="server" Visible="false"></asp:Label>
                <asp:Repeater ID="rptCollegeDetails"  runat="server">
              
                    <ItemTemplate>
                        <div class="boxPlane marginall">
                            <ul class="vertical">
                                <asp:HiddenField ID="hndCollegeBranchCourseId" runat="server" Value=' <%# Eval("AjCollegeBranchCourseId")%>' />
                                
                                <li style="width:90px;"><span class="Imgarrow marginRight"><a target="_blank"  class="astrong" href='<%#IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("College-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("AjCourseName")))+"/"+ IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("AjCollegeBranchName")))).ToLower() %>'
                                    title=" <%# Eval("AjCollegeBranchName")%>">
                                    <img title='<%# Eval("AjCollegeBranchName")%>' alt='<%# Eval("AjCollegeBranchName")%>'
                                        height="70px;" width="70px;" src='<%# String.Format("{0}{1}","/image.axd?College=",string.IsNullOrEmpty(Eval("AjCollegeBranchLogo").ToString()) ?"NoImage.jpg":Eval("AjCollegeBranchLogo")) %>' />
                                </a></span></li>
                                
                                <li class="width75Percent">
                                    <h3>
                                        <a class="astrong" target="_blank" href='<%#IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("College-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("AjCourseName")))+"/"+ IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("AjCollegeBranchName")))).ToLower() %>'
                                            title=" <%# Eval("AjCollegeBranchName")%>">
                                            <%# Eval("AjCollegeBranchName")%>
                                        </a>
                                    </h3>
                                    <label class="displayBlock aColor">
                                        Course:
                                        <%# Eval("AjCourseName")%></label> 
                                          <label class="displayBlock aColor">
                                        Course:
                                        <%# Eval("AjCollegeBranchEst")%></label> 
                                        <span class="fright"><label> College Priorty : </label><label class="aColor">
                                     <%# String.Format("{0}", Eval("AjAdmissionPriority") != null ? Eval("AjAdmissionPriority") : "N/A")%>
                                      </span>

                                </li>

                            </ul>
                            <div class="clearBoth">
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
    
 <div class="clearBoth" style="height:30px; margin-top:10px;">
 <a href="/counselling/StudentConformationList.aspx"  class="button">Next</a>
 </div>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript" >
   var streamId = [0];
    function UpdateStreamPrioty(collegeBranchCourseStreamId) {
        var prioty = $("span#spnStreamPrioty" + collegeBranchCourseStreamId + ' ' + "input[type='text']").val()
        if (prioty != '') {
            $.ajax
            ({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/UpdateStudentStreamPrioty",
                async: true,
                data: '{collegeBranchCourseStreamId:"' + collegeBranchCourseStreamId + '",streamPriorty:"' + prioty + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    alert(msg.d);
                    $("span#spnStreamPrioty" + collegeBranchCourseStreamId + ' ' + "input[type='text']").css("display", "none");
                    $("span#spnStreamPriotyValue" + collegeBranchCourseStreamId).css("display", "block");
                    $("span#spnStreamPriotyValue" + collegeBranchCourseStreamId).html(prioty);
                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        } else {

            alert('Please Provide Stream Priorty');
        }
    }

    function CheckStreamPriority(control) {
   
        if ($(control).val() > 0) {
            if (!inArray($(control).val(), streamId)) {
                streamId.push($(control).val());
            } else {
                $(control).val('');
                alert("Please choose different stream prority");
            }

        } else {
            alert("please enter priorty greater than zero");
        }
    }
    function inArray(item, arr) {

        for (var i = 0; i < arr.length; i++) {
            var items = $.makeArray(arr[i]);

            for (var k = 0; k < items.length; k++) {
                if (items[k] == item[0]) {
                    return true;
                }
            }
        }

        return false;
    }
</script>
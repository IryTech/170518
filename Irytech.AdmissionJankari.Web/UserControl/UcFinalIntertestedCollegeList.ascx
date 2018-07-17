<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcFinalIntertestedCollegeList.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcFinalIntertestedCollegeList" %>
<%@ Import Namespace="IryTech.AdmissionJankari.BL" %>
<asp:UpdatePanel ID="updateCollegeList" runat="server">
    <ContentTemplate>
    <asp:HiddenField ID="hndCollegeList" Value="0" runat="server" />
    <asp:Label ID="lblSucess" CssClass="success" runat="server" Visible="false"></asp:Label>
    <div class="border mainBG">
             <div class="border mainBG">
            
                <asp:Repeater ID="rptCollegeDetails" OnItemCommand="CollegeDetailsItemCommand"  runat="server"
                    >
                    <ItemTemplate>
                        <div class="boxPlane marginall" id="divCollegeList">
                       <span class="closeBtn">
                           <asp:ImageButton ID="btnDelete" ToolTip="Remove College" CommandName="delete" CommandArgument='<%# Eval("AjStudentInterestedCollegeCounsellingId")%>'
                                            runat="server" ImageUrl="/image.axd?Common=whitecross.png" />
                                            </span>
                                             
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
                               
                                         <span  class="collegeBasics">
			     
                      <label class="displayBlock aColor">Course:
                                <%# Eval("AjCourseName")%></label>
                           
                    
                                <label>Eastablishment : 
                                <%# Eval("AjCollegeBranchEst")%></label> </span>
                              
                                      <span class="fright" id="CollegePriorty<%# Eval("AjCollegeBranchCourseId")%>" >
                                <label> Enter College priority</label>
                                    <input class="Prior"  type="text" id="txtPriority" style="width:25px" maxlength="1" onchange="DisableTextBox(this,<%# Eval("AjCollegeBranchCourseId")%>)"/>
                                        
                                  
                                  </span>  </li>
                              </ul>  <span class="clearBoth dispBlock">
                            </span>
                        </div>
                        
                    </ItemTemplate>
                </asp:Repeater>
                                          
                                      
                 
            </div>
 <span style="display:block;  height:20px; margin-top:10px; clear:both;"  >
 <a  onclick=" return AddIntertestedStream();" runat="server" id="spanNext" class="button" >Next</a>
  </span>
 </div>

    </ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript" defer="defer">
    var courseId = [0];
    var branchCourseId = [];
    var check = 0;
    function AddIntertestedStream() {

        if (branchCourseId.length > 0) {
            for (var i = 0; i < branchCourseId.length; i++) {
                if (branchCourseId[i].priority>0) {
                $.ajax({
                        type: "POST",
                        url: "/WebServices/CommonWebServices.asmx/InsertStudentStreamPrioty",
                        data: '{collegeBranchCourseStreamId:"' + 0 + '",collegePriorty:"' + branchCourseId[i].priority + '",collegeBranchCourseId:"' + branchCourseId[i + 1].collegeCourseId + '"}',
                        async: false,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function(response) {
                            check = check + 1;
                        },
                        error: function(xml, textStatus, errorThrown) {
                            //alert(xml.status + "||" + xml.responseText);
                        }
                    });
                }
            }
            if (check > 0) {

                location.href = "/counselling/StudentConformationList.aspx"; 
                return true;
                }
           
        }
        else {
            alert("Please set priority");
            return false;
        }
    }

    function DisableTextBox(control, value) {
    var number = /^[0-9]*$/;
        if ($(control).val()!=" ") {
            if (!number.test($(control).val())) {
                alert("Please input only numeric priorty");
                $(control).val('');
                return false;
            } else {
                if (inArray($(control).val(),courseId) == false) {
                    courseId.push($(control).val());
                    branchCourseId.push(
                        {
                            priority: $(control).val()
                        },
                        {
                            collegeCourseId: value
                        });

                    } else { alert("Priorty entred, already chosen by you "); return false; }
            }
        } 
        else {
            alert("Please enter priorty greater than zero");
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

    function GetTextValue() {
        var status =true;
        $("#divCollegeList .CollegePriority").each(function (i) {
            if ($(this).val() > 0) {
                status = true;
            }
            else {
                status = false;
            }
        });
        if (status == false) {
            alert("Please choose college priorty at least selecting one stream from every college");
            return false;
        }
        else {
            return true;
        }
    }

    
   
</script>
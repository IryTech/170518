<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucBookSeatavailability.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.ucBookSeatavailability" %>
<%@ Register TagPrefix="AJ" TagName="CustomPaging" Src="~/UserControl/CustomPaging.ascx" %>
<%@ Import Namespace="IryTech.AdmissionJankari.Components" %>
<div style="margin:auto;">
    <br/>
<div style="padding:5px;" >
   <asp:Label runat="server" ID="lblResult"  style="font-size:18px !important; font-weight:400; color:Red !important; border:0px solid !important; background-color:transparent !important;" Visible="false"></asp:Label> 

</div>
<div id="divCollegeList">
  <asp:DataList ID="rptCollegeDetails" runat="server"   Visible="False" RepeatColumns="2" RepeatDirection="Horizontal" >
        <ItemTemplate>
           <div class="itmTemplateInnerDiv border_blue">
                
                 <ul style="float: left; width:98%;" class="<%# Convert.ToBoolean(Eval("AjCollegeSponser").ToString())!=true?"":"" %>">
                    <li class="list_width">
                    <span class="Imgarrow marginRight" itemprop="image" style="margin-left:10px;"  >
                    <a  class="astrong masterTooltip" itemprop="url" href='<%#Utils.ApplicationRelativeWebRoot+("College-Details/" +Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("AjCourseName")))+"/"+ Utils.RemoveIllegalCharacters(Convert.ToString(Eval("AjCollegeBranchName")))).ToLower() %>' title=" <%# Eval("AjCollegeBranchName")%>"> 
                        <img id="collegeImage" title='<%# Eval("AjCollegeBranchName")%>' alt='<%# Eval("AjCollegeBranchName")%>'  style="width:40px;height:40px;"  src='<%# String.Format("{0}{1}","/image.axd?College=",string.IsNullOrEmpty(Convert.ToString(Eval("AjCollegeBranchLogo"))) ?"NoImage.jpg":Eval("AjCollegeBranchLogo")) %>' />
                 </a>  </span> 
                   
                   </li> 
                   <li style="text-align:left; width:81%;">
                   
                   <ul>
                      <li style="text-align: left;">   <h3 itemprop="College-name" style="line-height:18px;  font-size:13px;" >
               <a rel="canonical" itemprop="url" class="astrong" href='<%#Utils.ApplicationRelativeWebRoot+("College-Details/" +Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("AjCourseName")))+"/"+ Utils.RemoveIllegalCharacters(Convert.ToString(Eval("AjCollegeBranchName")))).ToLower() %>' title=" <%# Eval("AjCollegeBranchName")%>"> <%# Eval("AjCollegeBranchName")%></a>
                      
                       </h3>  </li>
                                <li class="child_list_first"><span itemprop="course">Course :</span>
                                 <i class="italic_text"><%# Eval("AjCourseName")%></i> |
                                <span itemprop="eastablishment">Eastablishment :</span>
                                 <i class="italic_text"> <%# String.Format("{0}", Convert.ToString(Eval("AjCollegeBranchEst")).Equals("null") ? "N/A" : Eval("AjCollegeBranchEst"))%></i><span class="comapreCheckbox fright">
                         <input type="checkbox" id="chk<%# Eval("AjCollegeBranchCourseId")%>" value='<%# Eval("AjCollegeBranchCourseId") %>'  onclick="GetSelectedCollege(this);" />
                   </span>  </li>
                                           
                </ul>
                   </li>
                    
                   
                    
                 </ul>




                  
            </div>
        </ItemTemplate> 
    </asp:DataList >
    <AJ:CustomPaging ID="ucCollegeList" runat="server" />
    <asp:HiddenField ID="hdnCollegeCourseId" runat="server" Value="0">
      </asp:HiddenField>
</div>
</div>
<script type="text/javascript">
    $('#chk'+$("#<%=hdnCollegeCourseId.ClientID %>").val()).attr('checked', 'checked');

    function GetSelectedCollege(control) {
        var checked = $(".itmTemplateInnerDiv span input:checkbox:checked").length;
         
        if (checked > 1) {
            $('input:checkbox[id="chk"' + control + ']').attr('checked', false);
            alert("You can not select more than one college");
            return false;

        }
        else if (checked > 0) {
         
            $("#<%=hdnCollegeCourseId.ClientID %>").val($(control).val());

        }
        else if (checked == 0) { $("#<%=hdnCollegeCourseId.ClientID %>").val(0); }
    }

</script>
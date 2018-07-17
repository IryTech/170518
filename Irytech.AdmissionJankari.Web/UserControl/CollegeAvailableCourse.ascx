<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeAvailableCourse.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeAvailableCourse" %>

<%@ Register src="CustomPaging.ascx" tagname="CustomPaging" tagprefix="AJ" %>
<%--<asp:UpdatePanel runat="server">
<ContentTemplate>--%>
<asp:Label runat="server" ID="lblStreamresult" Visible="false"></asp:Label>
<asp:HiddenField runat="server" ID="hdnCourseName"></asp:HiddenField>
 <div class="box1" runat="server" id="divStreamresult">
 <h3 class="streamCompareH3">Available Stream</h3>
 <hr class="hrline" />
 <div class="box">
 <asp:Repeater ID="rptCourse" runat="server" >
                                        <HeaderTemplate>
                                            <table class="grdView">
                                            <thead>
                                        <td align="right" colspan="6" >
                                           Select at least two streams to compare <span id="spnCompaire" runat="server"> <a class="abutton"  onclick="return compareSelected()"> Compare </a> </span>
                                          </td>
                                          </thead>
                                                <tr style="background-color:#eff2f7;">
                                                    <td >
                                                        Stream Name
                                                    </td>
                                                    <td >
                                                        Seats
                                                    </td>
                                                    <td >
                                                        Duration
                                                    </td>
                                                    <td >
                                                        Fee
                                                    </td>
                                                        <td >
                                                        Mode
                                                    </td>
                                                  
                                                    <td >
                                                        View
                                                    </td>
                                                </tr>
                                          
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           
                                                <tr >
                                                    <td >
                                                     <a  href='<%#IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("Stream-Detail/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName")))+"/"+ IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("StreamName")))).ToLower() %>' rel="canonical" target="_blank"  class="college_Name">
                                                        <%#Eval("StreamName")%>
                                                        </a>
                                                    </td>
                                                        <td >
                                                        <%#Eval("CollegeBranchCourseStreamSeat")%>
                                                    </td>
                                                    <td >
                                                        <%#Eval("CollegeBranchCourseStreamDuration")%>
                                                    </td>
                                                    <td >
                                                           <%#Eval("CollegeBranchCourseStreamFees")%>
                                                            
                                                    </td>
                                                    <td >
                                                        <%#Eval("CollegeBranchCourseStreamModeName")%>
                                                    </td>
                                                     <td class="spnCompare">
                                                         <input type="checkbox" id="chk_Compare" value='<%#IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("StreamName"))) %>'/>
                                                    </td>
                                                </tr>
                                          
                                        </ItemTemplate><FooterTemplate>
                         
                            </table></FooterTemplate>
                                    </asp:Repeater>
                                    </div>
<AJ:CustomPaging ID="ucCustomCoursePaging" runat="server" />
      

<asp:Label ID="lblText" runat="server" Text=""></asp:Label></div>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>

<script type="text/javascript">

  


    function compareSelected() {
        var qsa = ""
        var l = 0;

        $("table td").find("input:checkbox:checked").each(function (k) {

            if (qsa == "")
                qsa += "StreamId" + (k + 1) + "=" + $(this).val();

            else
                qsa += "&StreamId" + (k + 1) + "=" + $(this).val();

            l++;

        });


        if (qsa != "") {
           
            if (l >= 2) {
                document.location.href = ("/" + $("#<%=hdnCourseName.ClientID %>").val() + "/Compare-Streams?" + qsa).toLowerCase();

            }
            else
                alert("Please select minimum two Stream to compare");
        }
        else {
            alert("Pleases choose at least two Stream to compare!");
        }
    }



</script>


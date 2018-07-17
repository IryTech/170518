<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourseComparison.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.College.CourseComparison" %>
<%@ Register Src="~/UserControl/RightBanner.ascx" TagPrefix="AJ" TagName="RightBanner" %>
<asp:content id="BodyContent" runat="server" contentplaceholderid="cphBody">

    <asp:UpdatePanel ID="updateCourseStream" runat="server">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hdnCourse" />
            <asp:HiddenField runat="server" ID="hndFocus"></asp:HiddenField>
            <div class="five_sixth fleft last">
    
                <h1>Course Comparison</h1>
                <hr class="hrline" />
   
        
                <p>
                    Choose at least two Stream name to compare of your choice to see how they compare
                    on future scope, related industries, core companies and average package. You have
                    a choice of comparing new streams in courses.
                </p>
        
       
                <h3 class="streamCompareH3">
                    How to compare
                </h3>
                <p>
                    You can compare two course streams side by side. See differences from future scope,
                    related discipline, related industries, core companies, average package and various
                    other parameters at a glance. To compare streams, first select the desired stream
                    (e.g. Engineering and MBA ) placed at the top and then select two streams of your
                    choice from the list of available streams in the two text boxes (First stream and
                    Second stream). Once the selection of courses to be compared is done, click on Compare
                    button and start your comparison.
                </p><br />
                <i style="color: Navy;">Please feel free to write
                    to us at <a class="aColor" href="mailto:info@admissionjankari.com">info@admissionjankari.com</a>
                    if you have any query / feedback regarding the comparison tool.</i>
                             
      
    
       
                <div  class="box1 marginTop1">
                    
                    <h3 class="streamCompareH3">Search Course to compare <label id="lblShowCollege" style="font-size:15px; color:Maroon;"  ></label></h3>
                    <hr class="hrline" />
                    <fieldset class="boxBody">
                        <ul class="vertical width100Percent">
                            <li style="font-size: 12px; width:120px; padding-left:35px;"><strong style="font-size:15px; font-weight:600;" id="lblCourse">Change course </strong></li>
                            <li class="width75Percent">
                                <select id="ddlCourse" title="Select Course">
                                </select>
                                <label id="lblCollegeError" class=" hide error" title="Please Select College">
                                </label>
                            </li>
                        </ul>
                        <div class="campareDiv">
                        <span style=" float:left; border-right:1px dotted gray; padding-right:20px; width:30%; padding-bottom:10px;">
                            <strong>First Stream</strong>
                                <asp:TextBox ID="txtFirsteStream" Width="80%" runat="server"></asp:TextBox>  
                                </span>
                                <span style="float:left; width:32%; padding-left:50px; padding-bottom:10px;">                         
                            <strong>Second Stream</strong>
                                <asp:TextBox ID="txtSecondCollegeName" Width="80%" runat="server"></asp:TextBox>
                                </span>
                               
                        </div>
                        
                       <center class="clearBoth width70Percent" style="border-top:1px dotted gray; padding-top:10px; margin-left:10px;">
                            <asp:Button ID="btnCpmpareCollegeName" runat="server" OnClientClick="return CheckStreamField()" Text="Compare" CssClass="button" ToolTip="Compare"
                                       OnClick="btnCpmpareCollegeName_Click1" />
                            <input type="button" value="Clear" title="Clear" class="btnComman" onclick=" ClearControl() "
                                />
                        </center>
                    </fieldset>

                </div>
           
           
           
           
                <div id="DivStreamDetails" runat="server" class="box1">
                 <center><div class="width80Percent" style="margin:0px auto;">
                <ul class="vertical collegeCompare">
                    <li style="width:230px; display:block;" ><asp:Label ID="lblStream1" runat="server"></asp:Label></li>
                    <li style="width:100px; display:block;" ><img src="/image.axd?Common=vs1.png" alt="VS" /></li>
                    <li style="width:230px; display:block;" ><asp:Label ID="lblStream2" runat="server"></asp:Label></li>

                    </ul>
                 </div></center>
                 <hr class="hrline" />

                    <div id="focusdata"  class="box">
                        <ul class="clgCompare" id="ulStreamName">

                            <li id="liStreamBasicInfo" runat="server">
                                <span style="width:25%;">&nbsp;</span>
                                <span><asp:HyperLink ID="lblComFirstStreamName" runat="server" ForeColor="maroon" Font-Italic="true" Font-Bold="true"></asp:HyperLink></span>
                                <span><asp:HyperLink ID="lblComSecondStreamName" ForeColor="maroon" Font-Italic="true" Font-Bold="true" runat="server"></asp:HyperLink> </span>
                            </li>
   
                            <li  id="liStreamHistory" runat="server">
                                <strong><a href="javascript:;" title="Brief History">Brief History</a></strong>
                                
                                <asp:Label ID="lblBriefHistory"  runat="server"></asp:Label>
                                
                                <asp:Label ID="lblBriefHistory1" runat="server"></asp:Label>
                
                            </li>
       
                            <li id="liStreamDescription" runat="server">
                                <strong><a href="javascript:;" title="Short Description">Short Description</a></strong>
            
                                <asp:Label ID="lblShortDesc" runat="server"></asp:Label>
                
                                <asp:Label ID="lblShortDesc1" runat="server"></asp:Label>
               
                            </li>
                            <li id="liStreamFuruteScope" runat="server">
                                <strong><a href="javascript:;" title="Furute Scope">Future Scope</a></strong>
            
                                <asp:Label ID="lblFuruteScope" runat="server"></asp:Label>
                
                                <asp:Label ID="lblFuruteScope1" runat="server"></asp:Label>
               
                            </li>
                          
                            <li id="liStreamRelatedIndustry" runat="server">
                                <strong><a href="javascript:;" title="Related Industry">Related Industry</a></strong>
            
                                <asp:Label ID="lblReIndustry" runat="server"></asp:Label>
                
                                <asp:Label ID="lblReIndustry1" runat="server"></asp:Label>
                
                            </li>

                            <li id="liStreamCoreCompanies" runat="server">
                                <strong><a href="javascript:;" title="Core Companies">Core Companies</a></strong>
            
                                <asp:Label ID="lblCoreCompanies" runat="server"></asp:Label>
               
                                <asp:Label ID="lblCoreCompanies1" runat="server"></asp:Label>
              
        
                            </li>
       
                            <li><strong>Replace with</strong> 
                                <span style="height: 20px;">
                                    <asp:TextBox ID="txtFirstStreamReplace" width="50%" runat="server" />


                                    <asp:Button ID="btnFirstSearchReplace"  runat="server" Text="Replace" OnClientClick="return CheckReplaceStream()"
                                                ToolTip="Search"  CssClass="button" onclick="btnFirstSearchReplace_Click" />
                                  
                                </span><span  style="height: 20px;">
                                           <asp:TextBox ID="txtSecondStreamReplace" width="50%" runat="server" />
                                           <asp:Button ID="btnSecondSearchReplace" runat="server" Text="Replace"  OnClientClick="return CheckReplaceStream1()"
                                                       ToolTip="Search" CssClass="button"  onclick="btnSecondSearchReplace_Click" />
                              
                                


                                       </span>
                            </li>
     
                        </ul>
    
    
                    </div>


                </div>

            </div>
             <div class="one_sixth last fright" >
                                    <AJ:RightBanner ID="ucRightBanner" runat="server" />
                                    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
         

   
    <script type="text/javascript" defer="defer">

        $(document).ready(function () {

            BindCourseListHavingStream($("#ddlCourse"), $("#<%= hdnCourse.ClientID %>").val());

            $("#lblShowCollege").text($("#ddlCourse option:selected").text());
            showCourseWiseStreamDetails($("#<%= txtFirsteStream.ClientID %>"), $("#<%= txtSecondCollegeName.ClientID %>"), $("#<%= txtFirstStreamReplace.ClientID %>"), $("#<%= txtSecondStreamReplace.ClientID %>"), $("#<%= hdnCourse.ClientID %>").val());
            $("#ddlCourse").change(function () {
                ClearControl();
                if ($("#ddlCourse").val() > 0) {
                    $("#<%= hdnCourse.ClientID %>").val($("#ddlCourse").val());
                    ChangeCourseId($("#ddlCourse").val());
                    location.href = ("/" + RemoveIlegalCharecterFromCourse($("#ddlCourse option:selected").text()) + "/Compare-Streams/").toLocaleLowerCase();
                } else {
                    $("#ddlCourse").val($("#<%= hdnCourse.ClientID %>").val());
                    alert("Select course");
                    return false;
                }

            });


        });

        function pageLoad(sender, args) {

            if (args.get_isPartialLoad()) {

                BindCourseListHavingStream($("#ddlCourse"), $("#<%= hdnCourse.ClientID %>").val());

                $("#lblShowCollege").text($("#ddlCourse option:selected").text());
                showCourseWiseStreamDetails($("#<%= txtFirsteStream.ClientID %>"), $("#<%= txtSecondCollegeName.ClientID %>"), $("#<%= txtFirstStreamReplace.ClientID %>"), $("#<%= txtSecondStreamReplace.ClientID %>"), $("#<%= hdnCourse.ClientID %>").val());
                $("#ddlCourse").change(function () {
                    ClearControl();
                    if ($("#ddlCourse").val() > 0) {
                        $("#<%= hdnCourse.ClientID %>").val($("#ddlCourse").val());
                        ChangeCourseId($("#ddlCourse").val());
                        location.href = ("/" + RemoveIlegalCharecterFromCourse($("#ddlCourse option:selected").text()) + "/Compare-Streams/").toLocaleLowerCase();
                    } else {
                     
                        $("#ddlCourse").val($("#<%= hdnCourse.ClientID %>").val());
                        alert("Select course");
                        return false;
                    }


                });

            }
        }

        function ClearControl() {

            $('#ctl00_cphBody_txtFirsteStream').val('');
            $('#ctl00_cphBody_txtSecondCollegeName').val('');
        }

        function CheckStreamField() {
            if ($("#<%=txtFirsteStream.ClientID %>").val() == " " || $("#<%=txtSecondCollegeName.ClientID %>").val() == " ") {
                alert("Please enter stream");
                return false;
            }
            else if ($("#<%=txtFirsteStream.ClientID %>").val().length < 5 || $("#<%=txtSecondCollegeName.ClientID %>").val() < 5) {
                alert("Stream name are not valid");
                return false;
            }
            else if ($("#<%=txtFirsteStream.ClientID %>").val().trim() == $("#<%=txtSecondCollegeName.ClientID %>").val().trim()) {
                alert("Please enter different stream name");
                return false;
            }
            else {

                return true;
            }

        }

        function CheckReplaceStream() {

            if ($("#<%=txtFirstStreamReplace.ClientID %>").val() == "") {
                alert("Please enter stream ");
                return false;
            }
            else if ($("#<%=txtFirstStreamReplace.ClientID %>").val().trim() == $("#<%=txtFirsteStream.ClientID %>").val().trim()) {
                alert("Please enter different stream");
                return false;
            }
            else if ($("#<%=txtFirstStreamReplace.ClientID %>").val().trim() == $("#<%=txtSecondCollegeName.ClientID %>").val().trim()) {
                alert("Please enter different stream");
                return false;
            }
            else {
                return true;
            }

        }
        function CheckReplaceStream1() {
            if ($("#<%=txtSecondStreamReplace.ClientID %>").val() == "") {
                alert("Please enter stream ");
                return false;
            }
            else if ($("#<%=txtSecondStreamReplace.ClientID %>").val().trim() == $("#<%=txtFirsteStream.ClientID %>").val().trim()) {
                alert("Please enter different stream");
                return false;
            }
            else if ($("#<%=txtSecondStreamReplace.ClientID %>").val().trim() == $("#<%=txtSecondCollegeName.ClientID %>").val().trim()) {
                alert("Please enter different stream");
                return false;
            }
            else {
                return true;
            }

        }
    </script>
</asp:content>

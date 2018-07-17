<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollegeSearch.aspx.cs"
    EnableEventValidation="false"  Inherits="IryTech.AdmissionJankari20.Web.College.ColegeSearch" %>

<%@ Import Namespace="IryTech.AdmissionJankari.BL" %>
<%@ Import Namespace="IryTech.AdmissionJankari.Components" %>
<%@ Register Src="~/UserControl/CollegeSearch.ascx" TagPrefix="AJ" TagName="CollegeSearch" %>
<%@ Register Src="~/UserControl/CollegeQuickQuery.ascx" TagPrefix="AJ" TagName="CollegeQuickQuery" %>
<%@ Register Src="~/UserControl/UcMostViewedCollege.ascx" TagPrefix="AJ" TagName="MostviewdCollege" %>
<%@ Register Src="~/UserControl/ucAdmission.ascx" TagPrefix="AJ" TagName="UcDirectAdmission" %>
<%@ Register Src="~/UserControl/RightBanner.ascx" TagPrefix="AJ" TagName="RightBanner" %>
<asp:content id="BodyContent" runat="server" contentplaceholderid="cphBody">
<asp:UpdatePanel ID="updateCollegeList" runat="server">
 <ContentTemplate>
<asp:HiddenField ID="hdnCourseId" runat="server"></asp:HiddenField>
<asp:HiddenField ID="hdnCourseName" runat="server"></asp:HiddenField>
<asp:HiddenField ID="hdnCollegeBranchId" runat="server"></asp:HiddenField>
<asp:HiddenField ID="hdnSearchPriorityListCount" runat="server"></asp:HiddenField>
<div class="FL compare_nwCollegeBox MB20 bottom_add_banner" id="compareDiv" style="width: auto; display: none;">       
</div>

<div class="five_sixth fleft last">
<AJ:CollegeSearch ID="ucCollegeName" runat="server" />
<div id="searchPriorityListCollege" style="display:none">
</div>
<div id="div_scroll" style="display:none">
</div>
<div id="divCollegeList" class="four_fifth last fleft">
<div class="marginTop mainBG" style="padding:5px 10px;">
    <div id="fade"></div>
<asp:Label ID="lblText" runat="server"   Text=""></asp:Label> 
<div id="divImage" class="loading" >
<img src="/image.axd?Common=Loading.gif"  />
 </div>    
<span>
    <asp:Label runat="server" Visible="false" ID="lblResult" Font-Size="11px" ForeColor="Green" Font-Bold="true"  ></asp:Label>
</span> <span class="fright"></span>
<asp:Label ForeColor="Red" Font="16px" CssClass="fright marginTop1 rightmargin" runat="server" ID="lblSuccess" Visible="false" width="550px"></asp:Label>
</div>
    <asp:Repeater ID="rptCollegeDetails" runat="server">
        <ItemTemplate>
            <span class="hide"></span>
            <div class="<%# Convert.ToBoolean(Eval("CollegeBranchCourseSponserStatus").ToString())!=true?"itmTemplateInnerDiv":"itmTemplateInnerDiv" %>" itemscope itemprop="http://schema.org/EducationalOrganization" >
            
                <center style="height:16px; overflow:hidden;">
                <span  class="<%=Request.QueryString["CollegeName"]==null?"comapreCheckbox":"hide" %>">
                    <input name="compare"  type="checkbox" onclick="addToCompare('<%# Eval("CollegeBranchName")%>','<%# Eval("CollegeBranchCourseId")%>','<%# Eval("CollegeBranchLogo")%>')" title="Click to add in compare list"  id="chk_Compare<%# Eval("CollegeBranchCourseId")%>" accesskey='<%# Eval("CollegeBranchName")%>' value='<%#Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName"))) %>'  onclick="checkAllChecked(this)" />
                    <label title="Add to comapre" class="image_tooltip" for="chk_Compare<%# Eval("CollegeBranchCourseId")%>" style="color: #3b5998;font-size: 12px;">Add to compare 
                </label>
                  
                 <span itemprop="url">    <a href="#"  class="smsImglink image_tooltip" id="sendQuick" onclick='SearchPriorityObject.OpenQuickQueryPoup("divQuickQuery","550","sendQuick","<%# Eval("CollegeIdBranchId")%>","<%# Eval("CourseId")%>","<%# Eval("CollegeBranchCityName")%>","<%# Eval("CollegeBranchCourseId")%>","<%# Eval("CollegeBranchName")%>");ClearControl();return false;' title="Quick Query">Quick Query</a> </span>
                     <span itemprop="url">
                     <a href="#" class="rightImglink image_tooltip" id="sndShowHelpline" onclick='SearchPriorityObject.SaveHelpLineNo("<%# !string.IsNullOrEmpty(Convert.ToString(Eval("CollegeBranchCourseHelplineNo")))? Eval("CollegeBranchCourseHelplineNo") :Convert.ToString(Eval("HelpLineNumber"))%>");return false;' title='<%#  !string.IsNullOrEmpty(Convert.ToString(Eval("CollegeBranchCourseHelplineNo")))? Eval("CollegeBranchCourseHelplineNo") :Eval("HelpLineNumber")%>'>Helpline</a>

                     </span>
                      <span itemprop="url">
                       <%# String.Format("{0}", Convert.ToString(Eval("CollegeManagementType")) == "Government" ? "<a title='Apply for this and other top pvt colleges'  class='RedrightImglink image_tooltip' href=" + Utils.AbsoluteWebRoot + (Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName"))) + "/Get-Direct-Admission").ToLower() + ">Apply for this and other top pvt colleges </a>" : Convert.ToBoolean(Eval("CollegeBranchCourseOnlineStatus")) == false ? "<a title='Apply for this and other top pvt colleges' class='RedrightImglink image_tooltip' href=" + IryTech.AdmissionJankari.Components.Utils.AbsoluteWebRoot + (IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName"))) + "/Get-Direct-Admission").ToLower() + ">Apply for this and other top pvt colleges  </a>" : "<a title='Apply for this and other similar colleges' id='sndDirect' onclick='OpenDirectFormPopUp(" + Eval("CollegeIdBranchId") + "," + Eval("CollegeBranchCityId") + "," + Eval("CourseId") + "," + Eval("CollegeBranchCourseId") + ");ClearRegistationControl();return false;' class='RedrightImglink image_tooltip' href='#'" + ">Apply for this and other similar colleges </a>")%>
                       </span>
                       <span itemprop="url">
                            <a  rel="canonical" target="_blank" href='<%#Utils.ApplicationRelativeWebRoot+("reportdonation/" +Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName")))+"/"+ Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))).ToLower() %>' title=" Report Donation against  <%# Eval("CollegeBranchName")%> to help others." class="rightImglink image_tooltip">Report Donation</a>
                       </span>
                </center>
                 <ul class="<%# Convert.ToBoolean(Eval("CollegeBranchCourseSponserStatus").ToString())!=true?"":"searchBgColor" %>">
                    <span class="Imgarrow marginRight" itemprop="image" style="margin-left:10px;"  >
                 
                    <a  class="astrong image_tooltip" itemprop="url" href='<%#Utils.ApplicationRelativeWebRoot+("College-Details/" +Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName")))+"/"+ Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))).ToLower() %>' title=" <%# Eval("CollegeBranchName")%>"> 
                        <img class="image_tooltip" id="collegeImage" title='<%# Eval("CollegeBranchName")%>' alt='<%# Eval("CollegeBranchName")%>' height="40px;" width="40px;"  src='<%# String.Format("{0}{1}","/image.axd?College=",string.IsNullOrEmpty(Convert.ToString(Eval("CollegeBranchLogo"))) ?"NoImage.jpg":Eval("CollegeBranchLogo")) %>' />
                 </a>  </span>
                    <li   title="<%# new Common().IsDonationRepoted(Convert.ToInt32(Eval("CollegeBranchCourseId")))==true?"Donation reported against the " + Eval("CollegeBranchName") : "" %>" class='<%# new IryTech.AdmissionJankari.BL.Common().IsDonationRepoted(Convert.ToInt32(Eval("CollegeBranchCourseId")))==true?"blacklisted":"" %>'>
                        <ul>
                        <li>
                        <h3 itemprop="College-name" style="margin:0px;" >
                              <a rel="canonical" itemprop="url" class="astrong image_tooltip" href='<%#Utils.ApplicationRelativeWebRoot+("College-Details/" +Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName")))+"/"+ Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))).ToLower() %>' title=" <%# Eval("CollegeBranchName")%>"> <%# Eval("CollegeBranchName")%></a>
                        </h3>
                        </li>
                                <li><span itemprop="course">Course :</span>
                                <%# Eval("CourseName")%>
                                </li>
                                <li><span itemprop="location">Location : <%# Eval("CollegeBranchCityName")%></span>
                                <span>
                               <%#
                                   String.Format("{0}", ApplicationSettings.Instance.IsVissbibleBookYourSeat == true ? Convert.ToBoolean(Eval("CourseIsBookSeatVisible"))==true ?
                                                                                                                                             Convert.ToBoolean(Eval("CollegeBranchCourseSponserStatus")) == true ? Convert.ToBoolean(Eval("CollegeIsBookSeatVisible")) == true ? "<a title='Book Your Seat'  id='lnkBookSeat' href=" + Utils.ApplicationRelativeWebRoot + ("bookseat/" + Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName"))) + "/" + Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))).ToLower() + " class='fright margingreenblack' href=''>Book Your Seat </a>" : " " : " " : " " : "")
                              %>
                             </span>

                                </li>
                                <li><span itemprop="eastablishment">Eastablishment :</span>
                                 <%# String.Format("{0}", Convert.ToString(Eval("CollegeBranchEst")).Equals("null") ? "N/A" : Eval("CollegeBranchEst"))%> |  <%# Eval("CollegeManagementType")%>&nbsp;College</li>
                                <li><center class="bgNone" style="text-align:left;padding:0px;">
                    <span itemprop="details" >
                    <a class="aColor image_tooltip" itemprop="url" rel="canonical" href='<%#Utils.ApplicationRelativeWebRoot+("College-Details/" +Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName")))+"/"+ Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))).ToLower() %>'  title='<%# Eval("CollegeBranchName")%>: Contact Details'>
                      Contact Details</a></span> 
                    <span itemprop="availability"><a class="aColor image_tooltip" itemprop="url" rel="canonical" href='<%#  Utils.ApplicationRelativeWebRoot+("College-Details/" +Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName")))+"/"+ Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))+"#Course").ToLower()%>'    title='<%# Eval("CollegeBranchName")%>: Available Courses'>&raquo; Available Courses</a></span> 
                    <span itemprop="info" ><a  class="aColor image_tooltip" itemprop="url" rel="canonical" href='<%#  Utils.ApplicationRelativeWebRoot+("College-Details/" +Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName")))+"/"+ Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))+"#Hostel").ToLower()%>'    title='<%# Eval("CollegeBranchName")%>: Hostel Info'>&raquo;  Hostel Info</a></span>
                     <span itemprop="detail"><a  class="aColor image_tooltip" itemprop="url" rel="canonical"  href='<%# Utils.ApplicationRelativeWebRoot+("College-Details/" +Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName")))+"/"+ Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))+"#Overview").ToLower()%>' title='<%# Eval("CollegeBranchName")%>: View Details'>&raquo;   View Details</a></span>
                </center>
                </li>
                        
                        </ul>
                      
                    </li>
                   
                    
                </ul>
                
                 
                <center class="grdbottomImage" style="border-top:1px solid #d2d2dd;">
                    <span itemprop="fee">
                  <a  class="rightImglink image_tooltip" itemprop="url" rel="canonical" href='<%#  Utils.ApplicationRelativeWebRoot+("College-Details/" +Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName")))+"/"+ Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))+"#Course").ToLower()%>'  title='<%# Eval("CollegeBranchName")%>: Fees'>
                    Fee</a>
                    </span><span itemprop="exam">
                    <a  class="rightImglink image_tooltip" itemprop="url" rel="canonical" href='<%#  Utils.ApplicationRelativeWebRoot+("College-Details/" +Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName")))+"/"+ Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))+"#Exam").ToLower()%>'  title='<%# Eval("CollegeBranchName")%>: Exam' >
                   Exam</a>
                    </span><span itemprop="Facility">
                   <a class="rightImglink image_tooltip" itemprop="url" rel="canonical" href='<%# Utils.ApplicationRelativeWebRoot+("College-Details/" +Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName")))+"/"+ Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))+"#Facality").ToLower()%>'  title='<%# Eval("CollegeBranchName")%>: Facality'>
                  Facility</a>
                   </span>
                   <span itemprop="Criteria">
                    <a class="rightImglink image_tooltip" itemprop="url" rel="canonical" href='<%# Utils.ApplicationRelativeWebRoot+("College-Details/" +Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName")))+"/"+ Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))+"#Exam").ToLower()%>'  title='<%# Eval("CollegeBranchName")%>: Admission Criteria'>
                   Admission Criteria</a></span>
                     <span itemprop="Comment">
                    <a class="rightImglink image_tooltip" itemprop="url" rel="canonical" href='<%# Utils.ApplicationRelativeWebRoot+("College-Details/" +Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName")))+"/"+ Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))+"#comment").ToLower()%>'  title='<%# Eval("CollegeBranchName")%>: Comment'>
                  Comment</a></span>
                    <span itemprop="GDPI">
                    <a class="rightImglink image_tooltip" itemprop="url" rel="canonical" href='<%# Utils.ApplicationRelativeWebRoot+("College-Details/" +Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName")))+"/"+ Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))+"#event").ToLower()%>'  title='<%# Eval("CollegeBranchName")%> Event'>
                 Event</a></span>
                    </center>
                    

            </div> 
            
        </ItemTemplate>
    </asp:Repeater >
                           <asp:Panel runat="server" id="pnlPager" CssClass="pagination">
                           </asp:Panel>

</div>

<div class="one_third fright last marginTop">

<div class="box1">
                <h3>Why not refine your search here?</h3><hr class="hrline" />
                <div class="box">
                <ol>
                <li><h3>
                                    <label >Find</label>
                                    <label id="lblCourse" runat="server" class="label">Course</label>
                                    <label >colleges</label>
                              </h3>
                              <hr class="hrline" />
                              <asp:DropDownList id="ddlCourse" runat="server" Cssclass="masterTooltip" onchange="SearchPriorityObject.CourseChange(this)"
                                        Height="28px" title="Select Course" Width="95%" 
                                          ></asp:DropDownList></li>
                <li><h3>
                                    <label class="text11_blak">Colleges in</label>
                                    <label id="lblState" class="label" runat="server" >State</label>
                               </h3>
                               <hr class="hrline" />
                               <asp:DropDownList id="ddlState" Height="28px" AutoPostBack="true" Cssclass="masterTooltip"  runat="server" Width="95%"
                                        ToolTip="Select State" onselectedindexchanged="DdlStateSelectedIndexChanged"></asp:DropDownList></li>
                <li><h3>
                                    <label class="text11_blak">Colleges in</label>
                                    <label id="lblCity"  runat="server" class="label" >City</label>
                                </h3>
                                <hr class="hrline" />
                                <asp:DropDownList ID="ddlCity" runat="server" Cssclass="masterTooltip" AutoPostBack="true"  
                                        Height="28px" ToolTip="Select City" Width="95%" 
                                        onselectedindexchanged="DdlCitySelectedIndexChanged" ></asp:DropDownList></li>
                <li><h3>
                                    <label class="text11_blak">Colleges under</label>
                                  <label id="lblExam" runat="server"  class="label" >Exam</label> 
                                </h3>
                                <hr class="hrline" />
                                <asp:DropDownList ID="ddlExam" runat="server" Cssclass="masterTooltip" AutoPostBack="true" Height="28px" Width="95%"
                                           ToolTip="Select Exam" onselectedindexchanged="DdlExamSelectedIndexChanged"></asp:DropDownList></li>
                <li><h3>
                                  <label id="lblManagement" runat="server" class="label" >Management</label> 
                                  <label class="text11_blak">Colleges</label>
                                </h3>
                                <hr class="hrline" />
                                <asp:DropDownList ID="ddlManagement" Cssclass="masterTooltip" runat="server" AutoPostBack="true" Height="28px" Width="95%"
                                           ToolTip="Select Management" onselectedindexchanged="DdlManagementSelectedIndexChanged">
                                           
                                           </asp:DropDownList></li>
                
                </ol>
              </div>
               </div> 
            </div>
<div class="one_third fright last marginTop">
<div class="box1"><center>
                   <a href="http://www.flipkart.com/laptops/hp~brand/pr?sid=6bo,b5g&affid=krsandeeps" target="_blank">
                           <img   src = '/image.axd?Common=20120907-170350-hp-pav.jpg' height="275" width = "300" >
                           </a>
                             
                      
                  </center></div>

</div>

<div class="one_third fright last">
     <AJ:MostviewdCollege ID="mstViewdCollege" runat="server" />       
</div>
</div>

<div class="one_sixth last fright">
    <AJ:RightBanner ID="ucRightBanner" runat="server" />
</div>
                                    
   </ContentTemplate>
 </asp:UpdatePanel>
  <div class="popup_block" id="divQuickQuery">
 <AJ:CollegeQuickQuery ID="ucCollegeQuickQuery" runat="server" />
  </div>
  
  <div class="popup_block"  id="divHelpLine">
  
  </div>


  <div id='divDirectAdmssion' class="popup_block" >
    <AJ:UcDirectAdmission ID="UcDirectAdmission" runat="server" />    
  </div> 
  
  <input type="hidden" id="hdnCollegeCourseId" />
    <input id="hdnCourse" type="hidden" value="0" /><input id="hdnCourseValue" type="hidden" value="0" />
      <input id="hdnCollege" type="hidden" value="0" />
        <input id="hdnCity" type="hidden" value="0" />
        
    
    <input type="hidden" id="countdata" value="0"/>
   <input type="hidden" id="hdnContentCount" value="0"/>
<link rel="stylesheet" type="text/css" href="/Styles/tipTip.css" />
<script type="text/javascript" src="/Js/jquery.tipTip.js"></script>
    <script type="text/javascript">


        function OpenDirectFormPopUp(collegeBranchId, cityId, courseId, collegeCourseId) {

            $("#hdnCourse").val(courseId);
            $("#hdnCollege").val(collegeBranchId);
            $("#hdnCity").val(cityId);
            $("#hdnCollegeCourseId").val(collegeCourseId);
            var userLogin =<%=UserLogin %>;  //to check user login................

            if (userLogin==false) {

                OpenPoup("divDirectAdmssion", "550", "sndDirect");
            } else {

                InsertCollegePrefer();
                InsertCityPrefer();
                OpenPoup("divPaymentUser", "550", "sndDirect");
            }
        }

        $("#btnResponse").click(function() {
            var name = <%=LoginUserName%>;
            location.href = 'Account/' + name;
        });
        //to pass hiidden field value to external js file.......

        function RegisterSearchValue() {

            SearchPriorityObject.FieldValue.SearchPriorityCount = SearchPriorityObject.$("<%=hdnSearchPriorityListCount.ClientID %>");
            SearchPriorityObject.FieldValue.CourseName = SearchPriorityObject.$("<%=hdnCourseName.ClientID %>");
            SearchPriorityObject.FieldValue.CollegeId = SearchPriorityObject.$("<%=hdnCollegeBranchId.ClientID %>");
        }

        $(".image_tooltip").tipTip({ maxWidth: "auto", delay: 50 });

    </script>
<script type="text/javascript" src="/Js/SearchPriority.js"></script>
    <style type="text/css">
        #searchPriorityListCollege
        {
            height: 1%;
            overflow: hidden;
            padding: 0 0 0px;
            margin-top: 10px;
            border: 1px solid #fff;
        }
        #searchPriorityListCollege .viewport
        {
            float: left;
            width: 983px;
            height: 180px;
            overflow: hidden;
            position: relative;
        }
        #searchPriorityListCollege .buttons
        {
            background: url("/image.axd?Common=leftArrow.png") no-repeat;
            display: block;
            margin: 0px;
            background-position: center center;
            float: left;
            width: 40px;
            height: 18px;
            overflow: hidden;
            position: relative;
        }
        #searchPriorityListCollege .next
        {
            background: url("/image.axd?Common=rightArrow.png") no-repeat;
            margin: 0px;
            background-position: center center;
        }
        #searchPriorityListCollege .disable
        {
            visibility: hidden;
        }
        #searchPriorityListCollege .overview
        {
            list-style: none;
            position: absolute;
            padding: 0;
            margin: 0;
            width: 140px;
            left: 0 top: 0;
        }
        #searchPriorityListCollege .overview li
        {
            float: left;
            margin: 0 20px 0 0;
            padding: 1px;
            height: 175px;
            border: 1px solid #00bfff;
            box-shadow: 0 0 4px rgba(0, 0, 0, 0.5);
            -moz-box-shadow: 0 0 4px rgba(0, 0, 0, 0.5);
            -webkit-box-shadow: 0 0 4px rgba(0, 0, 0, 0.5);
            width: 176px;
            text-align: center;
            overflow: hidden;
        }
        .overview li a
        {
            clear: both;
            display: block;
            text-align: center;
            font-size: 12px !important;
            line-height: 15px;
            color: #1a3251;
        }
        
        
        
        #div_scroll ul li
        {
            float: left;
            margin: 0 5px 0 0;
            padding: 1px;
            width: 180px;
            overflow: hidden;
        }
        #div_scroll ul li img
        {
            width: 50px !important;
            height: 50px !important;
            border: 1px solid gray;
            border-radius: 3px;
            float: left;
            margin-right: 3px;
            overflow: hidden;
        }
        #div_scroll ul li a
        {
            font-size: 11px;
            color: #1a3251;
        }
        .clearfix:after
        {
            visibility: hidden;
            display: block;
            font-size: 0;
            content: " ";
            clear: both;
            height: 0;
        }
        ul.featured_list
        {
            list-style: none outside none;
            padding: 0;
            margin: 2px 5px;
            background-color: #fff;
        }
        .featured_top
        {
            position: relative;
            float: left;
            height: 70px;
            background-color: #FFF;
            border: 1px solid #9DCFE4;
            border-radius: 3px 3px 3px 3px;
            -moz-border-radius: 3px 3px 3px 3px;
            -webkit-border-radius: 3px 3px 3px 3px;
            border-radius: 3px 3px 3px 3px;
            box-shadow: 2px 2px 5px rgba(0, 0, 0, 0.15);
            -moz-box-shadow: 2px 2px 5px rgba(0, 0, 0, 0.15);
            -webkit-box-shadow: 2px 2px 5px rgba(0, 0, 0, 0.15);
        }
        .tbl_scroll
        {
            display: table;
            height: 62px;
            width: 66%;
        }
        #div_scroll
        {
            display: none;
            z-index: 500;
            background-color: #fff;
        }
        .tbl_scroll h3
        {
            display: table-cell;
            font-size: 12px !important;
            line-height: 1.4;
            vertical-align: middle;
            padding-bottom: 5px;
        }
        h3
        {
            display: block;
            font-size: 1.17em;
            -webkit-margin-before: 1em;
            -webkit-margin-after: 1em;
            -webkit-margin-start: 0px;
            -webkit-margin-end: 0px;
            font-weight: bold;
        }
        .featured_list li
        {
            padding: 0;
            float: left;
            display: inline;
            position: relative;
            margin-right: 10px;
        }
        
        #searchPriorityListCollege .overview li:hover
        {
            box-shadow: 0 0 8px rgba(110, 43, 43, 0.8);
            -moz-box-shadow: 0 0 8px rgba(110, 43, 43, 0.8);
            -webkit-box-shadow: 0 0 8px rgba(110, 43, 43, 0.8);
        }
        
        .bottom_add_banner
        {
            position: fixed;
            bottom: 0;
            z-index: 99;
            margin-left: 350px;
        }
        
        .compare_nwCollegeBox
        {
            padding: 10px;
            background: rgb(71,71,71); /* Old browsers */
            background: -moz-linear-gradient(top,  rgba(71,71,71,1) 0%, rgba(60,60,60,1) 16%, rgba(88,88,88,1) 100%); /* FF3.6+ */
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(71,71,71,1)), color-stop(16%,rgba(60,60,60,1)), color-stop(100%,rgba(88,88,88,1))); /* Chrome,Safari4+ */
            background: -webkit-linear-gradient(top,  rgba(71,71,71,1) 0%,rgba(60,60,60,1) 16%,rgba(88,88,88,1) 100%); /* Chrome10+,Safari5.1+ */
            background: -o-linear-gradient(top,  rgba(71,71,71,1) 0%,rgba(60,60,60,1) 16%,rgba(88,88,88,1) 100%); /* Opera 11.10+ */
            background: -ms-linear-gradient(top,  rgba(71,71,71,1) 0%,rgba(60,60,60,1) 16%,rgba(88,88,88,1) 100%); /* IE10+ */
            background: linear-gradient(to bottom,  rgba(71,71,71,1) 0%,rgba(60,60,60,1) 16%,rgba(88,88,88,1) 100%); /* W3C */
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#474747', endColorstr='#585858',GradientType=0 ); /* IE6-9 */
        }
        .cmbox
        {
            width: 110px;
            background: #fff;
            text-align: center;
            padding: 5px;
            height: 170px;
        }
        .cmboxlast
        {
            width: 133px;
            text-align: center;
            height: 160px;
            padding: 20px 15px 0px 5px;
        }
        .cmboxlast p
        {
            color: #FFF !important;
        }
        .cmboxlast span
        {
            margin-top: 20px;
        }
        .gry12
        {
            font-size: 12px !important;
            color: black !important;
            text-align: center;
        }
        .contl
        {
            width: 660px;
            _width: 640px;
            padding: 5px 0px;
            float: left;
            margin-top: 0;
        }
        .FL
        {
            float: left;
        }
        .FR
        {
            float: right;
        }
        .MT10
        {
            margin-top: 10px;
        }
        .MT12
        {
            margin-top: 12px;
        }
        .POSR
        {
            position: relative;
        }
        .PT5
        {
            padding-top: 5px;
        }
        .MR10
        {
            margin-right: 15px;
        }
        .MB20
        {
            margin-bottom: 20px;
        }
        
        .closeIco_College
        {
            width: 18px;
            height: 21px;
            background: url('/image.axd?Common=closeIco_newCollege.png');
            display: block;
            position: absolute;
            right: -5px;
            top: -8px;
            z-index: 99;
        }
        
        .comparebtn_newCollge
        {
            background: url('/image.axd?Common=comparebtn_newCollege.png') no-repeat !important;
            width: 141px;
            height: 41px;
            border: 0px;
            margin-top: 10px;
        }
    </style>
  </asp:content>

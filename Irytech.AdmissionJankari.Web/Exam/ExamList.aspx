<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamList.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.Exam.ExamList" %>

<%@ Import Namespace="IryTech.AdmissionJankari.BL" %>
<%@ Register Src="~/UserControl/UcExamSearch.ascx" TagPrefix="ADMJ" TagName="ExamSearch" %>
<%@ Register Src="~/UserControl/customPaging.ascx" TagPrefix="ADMJ" TagName="Paging" %>
<%@ Register Src="~/UserControl/ucExamQuickQuery.ascx" TagPrefix="ADMJ" TagName="ExamQuickQuery" %>
<%@ Register Src="~/UserControl/ucHelpLine.ascx" TagPrefix="ADMJ" TagName="ExamHelpLine" %>
<%@ Register Src="~/UserControl/UcMostViewdExam.ascx" TagPrefix="ADMJ" TagName="MostViewdExam" %>
<%@ Register Src="~/UserControl/Registeration.ascx" TagPrefix="ADMJ" TagName="Registeration" %>
<asp:content id="BodyContent" runat="server" contentplaceholderid="cphBody">
    <input type="hidden" id="hndExam" />
    <asp:UpdatePanel ID="updateExamList" runat="server">
        <ContentTemplate>     <div id="fade"></div>
<asp:Label ID="lblText" runat="server"  Text=""></asp:Label> 

<div id="divImage" class="loading" >
<img src="/image.axd?Common=Loading.gif"  />
 
 </div>   
            <div class="five_sixth fleft last">

            <ADMJ:ExamSearch ID="ucExamSearch" runat="server" />
                <div class="four_fifth last fleft">
                    <asp:Repeater ID="rptEntranceExam" runat="server">
                        <ItemTemplate>
                            <div class="itmTemplateInnerDiv" itemscope itemprop="http://schema.org/EducationalOrganization">
                                <center>
                                    <span itemprop="Recommend"><a href="#" itemprop="url" class="rightImglink masterTooltip" title="Recommend">Recommend </a></span>
                                    <span itemprop="exam-alert"><a href="#" itemprop="url" class="rightImglink masterTooltip" id="lnkAddTax" title="Set me exam alert">Set me exam
                                            alert</a> </span>
                                            <span itemprop="Query"><a href="#" itemprop="url" class="smsImglink masterTooltip" id="sendQuick" onclick='ExamQuery("<%# Eval("ExamName").ToString().ToUpper()%>",<%# Eval("ExamId")%>);ClearControl();return false;'
                                                title="Quick Query">Quick Query</a> </span>
                                              
                                              
                                                <span itemprop="Helpline"><a itemprop="url" href="#" class="rightImglink masterTooltip"
                                                    id="sndShowHelpline"  onclick='SaveHelpLineNo("<%# Eval("HelpLineNumber")%>");return false;'
                                                    title="<%# Eval("HelpLineNumber")%>">Helpline</a> </span>
                                </center>
                                <ul>
                                    <span class="Imgarrow marginRight" itemprop="url" style="margin-left:10px;">
                                    <a class="astrong masterTooltip" rel="canonical" href= '<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+ (IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse((new Common().CourseName))+"/Exam-Details/Year/"+ System.DateTime.Now.Year.ToString()+"-"+ (System.DateTime.Now.Year+1).ToString()+"/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("ExamName")))).ToLower()%>'>    <img id="Exam" title='<%# Eval("ExamFullName")%>' alt='<%# Eval("ExamName")%>'  height="50px;"
                                            width="50px;" src='<%# String.Format("{0}{1}","/image.axd?Exam=",string.IsNullOrEmpty(Convert.ToString(Eval("ExamLogo"))) ?"NoImage.jpg":Eval("ExamLogo")) %>' /></a>
                                    </span>
                                    <li>
                                        <ul>
                                            <li><h3 style="margin:0px; font-size:14px;"   itemprop="name">
                                            <a rel="canonical" class="astrong masterTooltip" title='<%# Eval("ExamFullName")%>' href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+ (IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse((new Common().CourseName))+"/Exam-Details/Year/"+ System.DateTime.Now.Year.ToString()+"-"+ (System.DateTime.Now.Year+1).ToString()+"/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("ExamName")))).ToLower() %>'> <%#Eval("ExamName").ToString().ToUpper() %> </a></h3> </li>
                                            <li><span itemprop="Exam-Name">Full Name :</span>
                                                <%# Eval("ExamFullName")%></li>
                                            <li><span itemprop="Course">Course :</span><span><%# Eval("Coursename")%></span></li>
                                            <li>
                                                <span itemprop="name"><a rel="canonical" class="astrong masterTooltip" itemprop="url"  href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+ (IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse(new Common().CourseName)+"/college/"+IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Eval("ExamName").ToString())).ToLower()%>'
                                                        title='<%# Eval("ExamFullName")%>'>
                                                        Find the list of colleges
                                                    </a></span><span itemprop="url"><a rel="canonical" href='#' class="fright margingreenblack masterTooltip" title=' Check Result'  style="margin-right:0px; padding:1px 12px;" onclick='OPenExamRegsiterPopUp("<%# Eval("ExamName").ToString().ToUpper()%>");return false;' id="sndExamRegister">
                                                        
                                                        Check Result
                                                    </a></span>
                                                     </li>
                                                     <li><center class="bgNone" itemscope style="text-align:left !important; padding:0px;">
                                    <span><a rel="canonical" itemprop="url" class="aColor masterTooltip" title="Form Sale Start" href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+ (IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters((new Common().CourseName))+"/Exam-Details/Year/"+ System.DateTime.Now.Year.ToString()+"-"+ (System.DateTime.Now.Year+1).ToString()+"/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("ExamName")))).ToLower() %>'>
                                        Form Sale Start</a></span> 
                                        <span><a rel="canonical" itemprop="url" class="aColor masterTooltip" title="Form Sale End"
                                            href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+ (IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse((new Common().CourseName))+"/Exam-Details/Year/"+ System.DateTime.Now.Year.ToString()+"-"+ (System.DateTime.Now.Year+1).ToString()+"/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("ExamName")))).ToLower() %>'>
                                            &raquo; Form Sale End</a></span> 
                                            <span itemprop="url"><a title="Date Sub" rel="canonical" class="aColor masterTooltip" href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+ (IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse((new Common().CourseName))+"/Exam-Details/Year/"+ System.DateTime.Now.Year.ToString()+"-"+ (System.DateTime.Now.Year+1).ToString()+"/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("ExamName")))).ToLower() %>'>
                                                &raquo; Last Date Sub</a></span> 
                                                <span itemprop="url"><a title="Exam Centre" rel="canonical" class="aColor masterTooltip" href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+ (IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse((new Common().CourseName))+"/Exam-Details/Year/"+ System.DateTime.Now.Year.ToString()+"-"+ (System.DateTime.Now.Year+1).ToString()+"/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("ExamName")))).ToLower() %>'>
                                                    &raquo; Exam Centre</a></span>
                                </center></li>
                                                     
                                         
                                        </ul>
                                    </li>
                                </ul>
                                
                                <center class="paddingBottom" style="border-top:1px solid #abafbc;">
                                    <span><a rel="canonical" class="rightImglink masterTooltip" title="Exam Date" href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+ (IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse((new Common().CourseName))+"/Exam-Details/Year/"+ System.DateTime.Now.Year.ToString()+"-"+ (System.DateTime.Now.Year+1).ToString()+"/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("ExamName")))).ToLower() %>'>
                                        Exam Date </a></span><span><a rel="canonical" class="rightImglink masterTooltip" title="Result Date"
                                            href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+ (IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse((new Common().CourseName))+"/Exam-Details/Year/"+ System.DateTime.Now.Year.ToString()+"-"+ (System.DateTime.Now.Year+1).ToString()+"/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("ExamName")))).ToLower() %>'>
                                            Result Date </a></span><span><a rel="canonical" class="rightImglink masterTooltip" title="Form Price"
                                                href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+ (IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse((new Common().CourseName))+"/Exam-Details/Year/"+ System.DateTime.Now.Year.ToString()+"-"+ (System.DateTime.Now.Year+1).ToString()+"/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("ExamName")))).ToLower() %>'>
                                                Form Price </a></span><span><a rel="canonical" class="rightImglink masterTooltip" title="Form Store"
                                                    href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+ (IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse((new Common().CourseName))+"/Exam-Details/Year/"+ System.DateTime.Now.Year.ToString()+"-"+ (System.DateTime.Now.Year+1).ToString()+"/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("ExamName")))).ToLower() %>'>
                                                    Form Store </a></span><span><a rel="canonical" class="rightImglink masterTooltip" title="View Details"
                                                        href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+ (IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse((new Common().CourseName))+"/Exam-Details/Year/"+ System.DateTime.Now.Year.ToString()+"-"+ (System.DateTime.Now.Year+1).ToString()+"/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("ExamName")))).ToLower() %>'>
                                                        View Details </a></span>
                                </center>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <ADMJ:Paging ID="Pager" runat="server" />
                </div>
                <div class="one_third fright last marginTop">
                    <div class="box1">
                    <h3>
                        Why not refine your search here?</h3>
                        <hr class="hrline" />
                        
                        <div class="box">
                            <ol>
                                <li>
                                    <h3>
                                        <label>
                                            Find
                                        </label>
                                        <label id="lblCourse" runat="server" class="label">
                                            Course</label>
                                        <label>
                                            Exam</label>
                                    </h3>
                                    <hr class="hrline" />
                                    <asp:DropDownList ID="ddlCourse" cssclass="masterTooltip" runat="server" AutoPostBack="true" Height="28px"
                                        title="Select Course" Width="95%" OnSelectedIndexChanged="ddlCourseSelectedIndexChanged">
                                    </asp:DropDownList>
                                </li>
                                
                            </ol>
                        </div>
                    </div>
                </div>
                <div class="one_third fright last marginTop">
                
         <div class="box1" id="divLatestNews" >
                <a href="http://www.hotelscombined.com/?a_aid=94784&label=Image300250" target="_blank" rel="nofollow">
                <img width="99%" src="http://media.datahc.com/banners/affiliate/en/inspirational_300x250.gif" alt="Compare hotel prices and find the best deal - HotelsCombined.com" title="Compare hotel prices and find the best deal - HotelsCombined.com" border="0" /></a>
            </div>
          
                <div class="box1">
                <center>
                <iframe src ='http://www.flipkart.com/affiliate/displayWidget?affrid=WRID-136092794822144390' frameborder = 0, height=250, width = 300 > </iframe>
                </center>
                
                <center>
                <iframe src ='http://www.flipkart.com/affiliate/displayWidget?affrid=WRID-136092801739068959' frameborder = 0, height=250, width = 300 > </iframe>
                </center>
                </div>
                </div>
                

                <div class="one_third fright last" id="Div1">
                    <ADMJ:MostViewdExam runat="server" ID="ucMostviewdExam" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<div class="one_sixth last fright border">
        <strong>Advertising Banner</strong>
    </div>--%>
    <div id="divExamQuery" class="popup_block">
        <ADMJ:ExamQuickQuery ID="ucExamQuery" runat="server" />
    </div>
    <div id="divHelpLine" class="popup_block">
        <h3>AdmissionJankari.com Helpline Number</h3>
<hr class="hrline" />
<div class="quickquery login">
<fieldset class="boxBody">
<ul>
<li id="Head"><img alt="HelpLineNo" src="/Image/CommonImages/HelpLine.jpg" /></li>
<li><h3 style="font-weight:bold; font-size:1.2em; color:Maroon;" id="helpLine"></h3></li>
<li><strong>Free, confidential, 24 hours</strong></li>
</ul>
</fieldset>
</div>
    </div>
      <div id="divRegisteration" class="popup_block">
        <ADMJ:Registeration ID="ucRegisteration" runat="server" />
    </div>

    <script type="text/javascript" defer="defer">
        function ExamQuery(examName, examId) {
            $("input[id*=hndExamId]").val(examId);
            $("span[id*=lblExamName]").text(examName);
            OpenPoup("divExamQuery", "550", "sendQuick");
        }

        function pageLoad(sender, args) {

            if (args.get_isPartialLoad()) {
                $("#<%=ddlCourse.ClientID%>").change(function () {

                    ChangeCourseId($("#<%=ddlCourse.ClientID%>").val());

                    location.href = ("/Exams/" + RemoveIlegalCharecterFromCourse($("#<%=ddlCourse.ClientID%> option:selected").text())).toLowerCase();

                });
            }
        }

        function ExamNameQuery(examFullName, examName) {

            $("#ExamName").val(examId);
            $('#lblExamName').text(examName);
            OpenPoup("divExamQuery", "445", "sendQuick");
        }
        $(document).ready(function () {
            $("#<%=ddlCourse.ClientID%>").change(function () {

                ChangeCourseId($("#<%=ddlCourse.ClientID%>").val());

                location.href = ("/Exams/" + RemoveIlegalCharecterFromCourse($("#<%=ddlCourse.ClientID%> option:selected").text())).toLowerCase();

            });
        });

        function SaveHelpLineNo(helpLineNumber) {

            $("#helpLine").html(helpLineNumber)
            OpenPoup("divHelpLine", "#?w=450", "sndShowHelpline");

        }

    </script>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        // Add initializeRequest and endRequest
        prm.add_initializeRequest(prm_InitializeRequest);
        prm.add_endRequest(prm_EndRequest);

        // Called when async postback begins
        function prm_InitializeRequest(sender, args) {
            // get the divImage and set it to visible
            $("#fade").show();
            $("#divImage").show();

        }

        // Called when async postback ends
        function prm_EndRequest(sender, args) {
            $("#fade").hide();
            $("#divImage").hide();

        }

        function OPenExamRegsiterPopUp(examName) {
            $("#lblExam").text("Check Exam Result For " + examName);
            $("#ExamName").val(examName);
            CheckSession(examName);
            window.ClearRegistationControl();
        }
         
    </script>
   
</asp:content>

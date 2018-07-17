<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamDetails.aspx.cs" MasterPageFile="~/themes/Blue/Site.Master"
    Inherits="IryTech.AdmissionJankari20.Web.Exam.ExamDetails" %>

<%@ Import Namespace="IryTech.AdmissionJankari.BL" %>
<%@ Register Src="~/UserControl/ucExamQuickQuery.ascx" TagPrefix="ADMJ" TagName="ExamQuickQuery" %>
<%@ Register Src="~/UserControl/UcMostViewdExam.ascx" TagPrefix="ADMJ" TagName="MostViewdExam" %>
<%@ Register Src="~/UserControl/UcExamRealtedCourse.ascx" TagPrefix="ADMJ" TagName="RealatedCourse" %>
<%@ Register Src="~/UserControl/UcCollegeRealtedToExam.ascx" TagPrefix="ADMJ" TagName="CollegeRealtedRealatedExam" %>
<%@ Register TagPrefix="ADMJ" TagName="Registeration" Src="~/UserControl/Registeration.ascx" %>
<%@ Register Src="~/UserControl/UcCommonComment.ascx" TagPrefix="AJ" TagName="CommonComment" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">
    <div class="five_sixth fleft last">
        <div class="boxPlane bgYellow marginbottom" id="examDetailsHeader">
            <ul class="vertical">
                <li class="Imgarrow marginRight">
                    <asp:Image runat="server" ID="imgExam" Width="100px" Height="100px"></asp:Image></li>
                <li>
                    <h1>
                        <asp:Label runat="server" ID="lblHeader"></asp:Label></h1>
                    <h2 style="font-size: 20px !important;">
                        Admission Helpline :<asp:Label ID="txtHelpLineNo" Style="color: Maroon;" runat="server"></asp:Label></h2>
                </li>
            </ul>
            <div class="clearBoth">
            </div>
        </div>
        <div class="pageTargetMenu">
            <ul class="vertical">
                <li><a href="#Overviews" title="Overview">Overview</a></li>
                <li><a href="#desc" title="Description">Description</a></li>
                <li><a href="#divEligibilty" title="Eligibility">Eligibility</a></li>
                <li><a href="#ExamForm" title="Exam Form Details">Exam Form Details</a></li>
                <li><a href="#examSyllabous" title="Exam Pattern">Exam Pattern</a> </li>
            </ul>
            <div class="clearBoth">
            </div>
        </div>
        <div class="four_fifth last fleft">
            <div id="divExamView">
                <asp:Repeater ID="rptViewExam" runat="server">
                    <ItemTemplate>
                        <div id="divExam" class="box1">
                            <h3 class="streamCompareH3" id="Overviews" title='<%#Eval("ExamFullName")%>'>
                                <%#Eval("ExamFullName")%></h3>
                            <hr class="hrline" />
                            <div class="box">
                                <ol>
                                    <li>
                                        <div runat="server" visible='<%#!string.IsNullOrEmpty(Eval("ExamName").ToString()).Equals(true) %>'>
                                            <strong class="strongDetails">Name</strong> : <span>
                                                <%#Eval("ExamName")%></span>
                                        </div>
                                    </li>
                                    <li>
                                        <div runat="server" visible='<%#!string.IsNullOrEmpty(Eval("ExamPopularName").ToString()).Equals(true) %>'>
                                            <strong class="strongDetails">Popular name</strong> : <span>
                                                <%# Eval("ExamPopularName")%></span>
                                        </div>
                                    </li>
                                    <li>
                                        <div runat="server" visible='<%#!string.IsNullOrEmpty(Eval("ExamWebSite").ToString()).Equals(true) %>'>
                                            <strong class="strongDetails">Website</strong> : <span><a href='<%# String.Format("{0}{1}","http://" , Eval("ExamWebSite")) %>'
                                                target="_blank">
                                                <%# Eval("ExamWebSite")%>
                                            </a></span><span><a href='#' class="fright rightImglink mainBG border " onclick='OPenExamRegsiterPopUp("<%# Eval("ExamName").ToString().ToUpper()%>");return false;'
                                                id="sndExamRegister">Check Result </a></span>
                                        </div>
                                    </li>
                                </ol>
                            </div>
                        </div>
                        <div id="divEligibilty" class="box1" runat="server" visible='<%# !string.IsNullOrEmpty(Eval("ExamEligiblityCriteria").ToString()).Equals(true) %>'>
                            <h3 class="streamCompareH3" id="h4eligibilty">
                                Eligibilty</h3>
                            <hr class="hrline" />
                            <div class="box">
                                <p>
                                    <%# Eval("ExamEligiblityCriteria")%>
                                </p>
                            </div>
                        </div>
                        <div id="desc" class="box1" runat="server" visible='<%# !string.IsNullOrEmpty(Eval("ExamDesc").ToString()).Equals(true) %>'>
                            <h3 id="Discrip">
                                Description</h3>
                            <hr class="hrline" />
                            <div class="box">
                                <p>
                                    <%# Eval("ExamDesc")%>
                                </p>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div id="ExamForm" runat="server" class="box1">
                <h3 class="streamCompareH3">
                    <asp:Label runat="server" Text="" ID="lblExamFormHeader"></asp:Label></h3>
                <div class="box1">
                    <asp:Repeater ID="rptExamForm" runat="server">
                        <ItemTemplate>
                     <div class="box">
                             
                                    <ol>
                                        <li>
                                            <h2 itemprop="Exam Form Name" style="margin: 0px;">
                                                
                                                   <%# GetMonth(Eval("ExamFormSaleStartDate"))%> - <%# Eval("ExamName")%> (<%# Eval("ExamFormYear")%>)</h2>
                                        </li>
                                        <li><div><strong class="strongDetails fleft " itemprop="course">Price:</strong>
                                            <span class="spanwidth"><%# Eval("ExamFormPrice")%></span></div>
                                        </li>
                                        <li><div><strong class="strongDetails fleft " itemprop="location">Sale Start Date :</strong>
                                           <span class="spanwidth"> <%# Eval("ExamFormSaleStartDate")%>
                                           
                                            <%# !string.IsNullOrEmpty(Eval("ExamFromSaleStartDateRemark").ToString()) ?
                                                Eval("ExamFromSaleStartDateRemark") : string.Empty%>
                                            </span>
                                            
                                    </div>
                                            </li>
                                        <li><div><strong class="strongDetails fleft " itemprop="eastablishment">Sale End Date :</strong>
                                            <span class="spanwidth"><%# Eval("ExamFormSaleEndDate")%>
                                            <%# !string.IsNullOrEmpty(Eval("ExamFormSaleEndDateRemark").ToString()) ? String.Format("<span>{0}</span>", Eval("ExamFormSaleEndDateRemark")) : string.Empty%></span>
                                            </div>
                                          </li>
                                          <li><div><strong class="strongDetails fleft" itemprop="eastablishment">Submit Last Date :</strong>
                                            <span class="spanwidth"><%# Eval("ExamFormSubmitDate")%>
                                              <span><%# !string.IsNullOrEmpty(Eval("ExamFormSubmitDateRemark").ToString()) ? String.Format("<span>{0}</span>", Eval("ExamFormSubmitDateRemark")) : string.Empty%></span></span>
                                            </div>

                                          </li>
                                         <li><div><strong class="strongDetails fleft " itemprop="eastablishment">Result Date :</strong>
                                            <span class="spanwidth"><%# Eval("ExamFormResultDate")%>
                                                <%#!string.IsNullOrEmpty(Eval("ExamFormResultDateReamrk").ToString()) ? String.Format("<span>{0}</span>", Eval("ExamFormResultDateReamrk"))  : string.Empty%>
                                         </span> </div>
                                          </li>
                                           <li><div><strong class="strongDetails fleft " itemprop="eastablishment">Result WebSite :</strong>
                                           <sapn class="spanwidth"><a href=" https://"+ <%# Eval("ExamFormResultWebsite")%>" target="_blank">
                                            <%# Eval("ExamFormResultWebsite")%>
                                          </a></sapn> </div>
                                          </li>
                                           <li><div><strong class="strongDetails fleft " itemprop="eastablishment">From Center :</strong>
                                            <span class="spanwidth"><%# Eval("ExamFormStore")%></span>
                                            </div>
                                          </li>
                                           <li><div><strong class="strongDetails fleft " itemprop="eastablishment">Exam Center :</strong>
                                            <span class="spanwidth"><%# Eval("ExamFormCenter")%></span>
                                            </div>
                                          </li>
                                           <li><div><strong class="strongDetails fleft" itemprop="eastablishment">Exam Syllabus :</strong>
                                          <span class="spanwidth"><%# Eval("ExamFormSyllabus")%></span>  
                                            </div>
                                          </li>
                                        
                                    </ol>
                                 
                           
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
          
            </div>
            <div class="box1">
                <asp:UpdatePanel runat="server" ID="updateExamCommentt">
                    <ContentTemplate>
                        <AJ:CommonComment runat="server" ID="UcComment" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="one_third fright last">
            <ADMJ:ExamQuickQuery ID="ucExamQuery" runat="server" />
        </div>
        <div class="one_third fright last">
            <div class="box1" id="divLatestNews">
                <a href="http://www.hotelscombined.com/?a_aid=94784&label=Image300250" target="_blank"
                    rel="nofollow">
                    <img width="99%" src="http://media.datahc.com/banners/affiliate/en/inspirational_300x250.gif"
                        alt="Compare hotel prices and find the best deal - HotelsCombined.com" title="Compare hotel prices and find the best deal - HotelsCombined.com"
                        border="0" /></a>
            </div>
        </div>
        <div class="one_third fright last" id="Div1">
            <ADMJ:MostViewdExam runat="server" ID="ucMostviewdExam" />
        </div>
        <div class="one_third fright last" id="Div2">
            <ADMJ:RealatedCourse runat="server" ID="ucRealatedCourse" />
        </div>
        <div class="one_third fright last" id="Div3">
            <ADMJ:CollegeRealtedRealatedExam runat="server" ID="ucCollegeRealtedRealatedExam" />
        </div>
        <div id="divRegisteration" class="popup_block">
            <ADMJ:Registeration ID="ucRegisteration" runat="server" />
        </div>
    </div>
    <script type="text/javascript" defer="defer">
        window.fbAsyncInit = function () {
            FB.init({
                appId: '145890325492400', // App ID
                channelUrl: 'http://www.admissionjankari.com/' + window.location.pathname, // Channel File
                status: true, // check login status
                cookie: true, // enable cookies to allow the server to access the session
                xfbml: true  // parse XFBML
            });

            // Additional initialization code here
        };

        // Load the SDK Asynchronously
        (function (d) {
            var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
            if (d.getElementById(id)) { return; }
            js = d.createElement('script'); js.id = id; js.async = true;
            js.src = "//connect.facebook.net/en_US/all.js";
            ref.parentNode.insertBefore(js, ref);
        } (document));
        function ExamNameQuery() {

            OpenPopus();
        }
        function OPenExamRegsiterPopUp(examName) {
            $("#lblExam").text("Check Exam Result For " + examName); $("#ExamName").val(examName);
            CheckSession(examName);
            window.ClearRegistationControl();
        }
    </script>
   <style>.spanwidth{width:75% !important; display:inline-block !important;}</style> 
</asp:Content>


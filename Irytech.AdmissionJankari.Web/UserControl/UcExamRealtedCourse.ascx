<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcExamRealtedCourse.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcExamRealtedCourse" %>
<%@ Import Namespace="IryTech.AdmissionJankari.BL" %>
<%@ Register Src="CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<asp:UpdatePanel runat="server" ID="updateCollegeCityList">
    <ContentTemplate>
        <div class="box1">
            <h3 id="lblMostViewdTitle" class="streamCompareH3">
                Exam Related to
                <asp:Label ID="lblCourseName" runat="server"></asp:Label>
            </h3>
            <hr class="hrline" />
            <div class="boxPlane">
                <asp:Repeater ID="rptExamRealtedCourse" runat="server">
                    <ItemTemplate>
                        <div class="ucDiv">
                            <ul class="vertical marginbottom">
                                <li class="width15Percent"><a href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+ (IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName")))+"/Exam-Details/Year/"+ System.DateTime.Now.Year.ToString()+"-"+ (System.DateTime.Now.Year+1).ToString()+"/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("ExamName")))).ToLower() %>'
                                    rel="canonical" title='<%# Eval("ExamFullName")%>'>
                                    <img id="Exam" title='<%# Eval("ExamFullName")%>' alt='<%# Eval("ExamName")%>' height="40px;"
                                        width="40px;" src='<%# String.Format("{0}{1}","/image.axd?Exam=",string.IsNullOrEmpty(Eval("ExamLogo").ToString()) ?"NoImage.jpg":Eval("ExamLogo")) %>' />
                                </a></li>
                                <li class="width70Percent"><a href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+ (IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName")))+"/Exam-Details/Year/"+ System.DateTime.Now.Year.ToString()+"-"+ (System.DateTime.Now.Year+1).ToString()+"/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("ExamName")))).ToLower() %>'
                                    rel="canonical" title='<%# Eval("ExamName")%>'>
                                    <%# Eval("ExamFullName")%>
                                </a></li>
                                <li><a href='<%# IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+  (IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName")))+"/college/"+IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Eval("ExamName").ToString())).ToLower()%>'
                                    title='<%# Eval("ExamFullName")%>'>Find the list of colleges </a></li>
                                <div class="clearBoth">
                                </div>
                            </ul>
                            <span class="clearBoth dispBlock">
                            </span>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <AJ:CustomPaging ID="Pager" runat="server" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

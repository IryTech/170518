<%@ Page Language="C#" AutoEventWireup="true" 
    CodeBehind="StudentCounselling.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.counselling.StudentCounselling" %>

<%@ Register Src="~/UserControl/UcStudentCounsellingPersonelInfo.ascx" TagPrefix="ADMJ"
    TagName="StudentPersonelInfo" %>
<%@ Register Src="~/UserControl/UcCourse.ascx" TagPrefix="ADMJ" TagName="StudentCourseInfos" %>
<%@ Register Src="~/UserControl/UcStudentAcademic.ascx" TagPrefix="ADMJ" TagName="StudentAcademicInfo" %>
<%@ Register Src="~/UserControl/UcStudentExamAppeared.ascx" TagPrefix="ADMJ" TagName="StudentExamInfo" %>
<%@ Register Src="~/UserControl/UcOnlineInstrucation.ascx" TagPrefix="ADMJ" TagName="Instrucation" %>
<%@ Register Src="~/UserControl/UcStudentCollegePrefrance.ascx" TagPrefix="ADMJ" TagName="IntertestedCollege" %>
<%@ Register Src="~/UserControl/UcStudentCityPrefrance.ascx" TagPrefix="ADMJ" TagName="IntertestedCity" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">
    <%--<asp:UpdatePanel ID="updateWizard" runat="server" >
<ContentTemplate>--%>
<div id="divFirstImage" runat="server" style="width:942px; margin:5px auto; display:block; height:32px;">
    <img src="/image.axd?Common=application_form.jpg" /></div>
    <div id="divSeccondImage" visible="false" runat="server" style="width:942px; margin:5px auto; display:block; height:32px;">
    <img src="/image.axd?Common=aplicationForm12.jpg" /></div>
    <div class="wizardOutDiv">
              <asp:Wizard ID="wizardApplyForm"  runat="server" ActiveStepIndex="0" Width="100%"   
                        DisplaySideBar="false" StartNextButtonText="Next »" StepNextButtonText="Next »"
                        StepPreviousButtonText="« Previous" OnActiveStepChanged="wizardApplyForm_ActiveStepChanged" FinishCompleteButtonText="Proceed  Next »"
                  onfinishbuttonclick="wizardApplyForm_FinishButtonClick">
                        <StepStyle />
                        <SideBarStyle VerticalAlign="Top" Wrap="False" />
                        <NavigationButtonStyle   CssClass="button" />
                        <NavigationStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <WizardSteps>
                            <asp:WizardStep ID="personalDetails" runat="server" Title="Personal Details">
                            <ADMJ:StudentCourseInfos ID="StudentCourseInfo" runat="server"></ADMJ:StudentCourseInfos>
                            <ADMJ:StudentPersonelInfo ID="studentPerInfo" runat="server"></ADMJ:StudentPersonelInfo>
                            </asp:WizardStep>
                            <asp:WizardStep ID="Instrucation" runat="server" Title="Instructions">
                                <ADMJ:Instrucation ID="ucInstrucation" runat="server"></ADMJ:Instrucation>
                            </asp:WizardStep>
                            <asp:WizardStep ID="AcedmicInfo" runat="server" Title="Your Scorecards">
                                <ADMJ:StudentAcademicInfo ID="StudentAcademicInfo" runat="server"></ADMJ:StudentAcademicInfo>
                            </asp:WizardStep>
                              <asp:WizardStep ID="Interested" runat="server" Title="Preferences">
                                <ADMJ:IntertestedCollege ID="UcCollegePrefrance" runat="server"></ADMJ:IntertestedCollege>
                                <ADMJ:IntertestedCity ID="UcICityPrefrance" runat="server"></ADMJ:IntertestedCity>
                                <ADMJ:StudentExamInfo ID="StudentExamInfo" runat="server"></ADMJ:StudentExamInfo>
                            </asp:WizardStep>
                                                      
                          </WizardSteps>
                        <HeaderTemplate>
                            <ul id="wizHeader">
                                <asp:Repeater ID="SideBarList" runat="server">
                                    <ItemTemplate>
                                        <li><a class="<%# GetClassForWizardStep(Container.DataItem) %>" title="<%#Eval("Name")%>">
                                            <%# Eval("Name")%></a> </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </HeaderTemplate>
                    </asp:Wizard>
              <%--<asp:UpdateProgress ID="progress" runat="server" AssociatedUpdatePanelID="updateWizard"  DynamicLayout="true" >
              <ProgressTemplate>
               <img src='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot%>image.axd?Common=LoadingImage.gif'
       alt="Please Wait Loading College..." />
              </ProgressTemplate>
              </asp:UpdateProgress>--%>
    </div>
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>
</asp:Content>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcParticipatedCollegeList.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcParticipatedCollegeList" %>
<%@ Register TagPrefix="AJ" TagName="CustomPaging" Src="~/UserControl/CustomPaging.ascx" %>
<%@ Register TagPrefix="AJ" TagName="CollegeSearch" Src="~/UserControl/BookSeatSearch.ascx" %>
<%@ Import Namespace="IryTech.AdmissionJankari.Components" %>
<asp:UpdatePanel ID="updateCollegeList" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="five_sixth fleft last">
            <AJ:CollegeSearch ID="ucCollegeName" runat="server" />
            <div id="divCollegeList" class="four_fifth last fleft">
                <asp:Repeater ID="rptCollegeDetails" runat="server">
                    <ItemTemplate>
                        <div itemscope itemprop="http://schema.org/EducationalOrganization">
                            <ul>
                                <span class="Imgarrow marginRight" itemprop="image" style="margin-left: 10px;"><a
                                    class="astrong masterTooltip" itemprop="url" href='<%#Utils.ApplicationRelativeWebRoot+("College-Details/" +Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseMaster.CourseName")))+"/"+ Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBasicInfo.CollegeBranchName")))).ToLower() %>'
                                    title=" <%# Eval("CollegeBasicInfo.CollegeBranchName")%>">
                                    <img class="masterTooltip" id="collegeImage" title='<%# Eval("CollegeBasicInfo.CollegeBranchName")%>'
                                        alt='<%# Eval("CollegeBasicInfo.CollegeBranchName")%>' height="40px;" width="40px;" src='<%# String.Format("{0}{1}","/image.axd?College=",string.IsNullOrEmpty(Convert.ToString(Eval("CollegeBasicInfo.CollegeBranchLogo"))) ?"NoImage.jpg":Eval("CollegeBasicInfo.CollegeBranchLogo")) %>' />
                                </a></span>
                                <li title="<%# new IryTech.AdmissionJankari.BL.Common().IsDonationRepoted(Convert.ToInt32(Eval("CollegeBranchCourse.CollegeBranchCourseId")))==true?"Donation reported against the " + Eval("CollegeBasicInfo.CollegeBranchName") : "" %>"
                                    class='<%# new IryTech.AdmissionJankari.BL.Common().IsDonationRepoted(Convert.ToInt32(Eval("CollegeBranchCourse.CollegeBranchCourseId")))==true?"blacklisted":"" %>'>
                                    <ul>
                                        <li>
                                            <h3 itemprop="College-name" style="margin: 0px;">
                                                <a rel="canonical" itemprop="url" class="astrong masterTooltip" target="_blank" href='<%#Utils.ApplicationRelativeWebRoot+("College-Details/" +Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseMaster.CourseName")))+"/"+ Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBasicInfo.CollegeBranchName")))).ToLower() %>'
                                                    title=" <%# Eval("CollegeBasicInfo.CollegeBranchName")%>">
                                                    <%# Eval("CollegeBasicInfo.CollegeBranchName")%></a>
                                            </h3>
                                        </li>
                                        <li><span itemprop="course">Course :</span>
                                            <%# Eval("CourseMaster.CourseName")%>
                                        </li>
                                        <li><span itemprop="location">Location :
                                            <%# Eval("CityMaster.CityName")%></span> <span>
                                                <%#String.Format("{0}", ApplicationSettings.Instance.IsVissbibleBookYourSeat == true ? Convert.ToBoolean(Eval("CollegeBranchCourse.IsBookSeatVisible")) == true ?
                                                                                                                                                                                                                    Convert.ToBoolean(Eval("CollegeBranchCourse.IsBookSeatVisible")) == true ? Convert.ToBoolean(Eval("CollegeBranchCourse.IsBookSeatVisible")) == true ? "<a title='Book Your Seat'  id='lnkBookSeat' href=" + Utils.ApplicationRelativeWebRoot + ("bookseat/" + Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseMaster.CourseName"))) + "/" + Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBasicInfo.CollegeBranchName")))).ToLower() + " class='fright margingreenblack' href=''>Book Your Seat </a>" : " " : " " : " " : "")
                                                %>
                                            </span></li>
                                        <li><span itemprop="eastablishment">Eastablishment :</span>
                                            <%# String.Format("{0}", Convert.ToString(Eval("CollegeBasicInfo.CollegeBranchEst")).Equals("null") ? "N/A" : Eval("CollegeBasicInfo.CollegeBranchEst"))%>
                                            |
                                           Private College</li>
                                        <li>
                                            <center class="bgNone" style="text-align: left; padding: 0px;">
                                                <span itemprop="details"><a class="aColor masterTooltip" itemprop="url" rel="canonical"
                                                    target="_blank" href='<%#Utils.ApplicationRelativeWebRoot+("College-Details/" +Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseMaster.CourseName")))+"/"+ Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBasicInfo.CollegeBranchName")))).ToLower() %>'
                                                    title='<%# Eval("CollegeBasicInfo.CollegeBranchName")%>: Contact Details'>Contact Details</a></span>
                                                <span itemprop="availability"><a class="aColor masterTooltip" itemprop="url" rel="canonical"
                                                    target="_blank" href=""
                                                    title='<%# Eval("CollegeBasicInfo.CollegeBranchName")%>: Available Courses'>&raquo; Available Courses</a></span>
                                                <span itemprop="info"><a class="aColor masterTooltip" itemprop="url" rel="canonical"
                                                    target="_blank" href=""
                                                    title='<%# Eval("CollegeBasicInfo.CollegeBranchName")%>: Hostel Info'>&raquo; Hostel Info</a></span>
                                                <span itemprop="detail"><a class="aColor masterTooltip" itemprop="url" rel="canonical"
                                                    target="_blank" href=""
                                                    title='<%# Eval("CollegeBasicInfo.CollegeBranchName")%>: View Details'>&raquo; View Details</a></span>
                                            </center>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <AJ:CustomPaging ID="ucCollegeList" runat="server" />
            </div>
            <div class="marginTop mainBG" style="padding: 5px 10px;">
                <div id="fade">
                </div>
                <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                <div id="divImage" class="loading">
                    <img src="/image.axd?Common=Loading.gif" />
                </div>
                <asp:Label runat="server" ID="lblResult" Visible="False" Font-Bold="True"></asp:Label>
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
                                        Find</label>
                                    <label id="lblCourse" runat="server" class="label">
                                        Course</label>
                                    <label>
                                        colleges</label>
                                </h3>
                                <hr class="hrline" />
                                <asp:DropDownList ID="ddlCourse" runat="server" Height="28px" title="Select Course"
                                    Width="95%">
                                </asp:DropDownList>
                            </li>
                            <li>
                                <h3>
                                    <label class="text11_blak">
                                        Colleges in</label>
                                    <label id="lblState" class="label" runat="server">
                                        State</label>
                                </h3>
                                <hr class="hrline" />
                                <asp:DropDownList ID="ddlState" Height="28px" AutoPostBack="true" runat="server"
                                    Width="95%" ToolTip="Select State" OnSelectedIndexChanged="DdlStateSelectedIndexChanged">
                                </asp:DropDownList>
                            </li>
                            <li>
                                <h3>
                                    <label class="text11_blak">
                                        Colleges in</label>
                                    <label id="lblCity" runat="server" class="label">
                                        City</label>
                                </h3>
                                <hr class="hrline" />
                                <asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlCitySelectedIndexChanged"
                                    Height="28px" ToolTip="Select City" Width="95%">
                                </asp:DropDownList>
                            </li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript">
    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {

            $("#<%=ddlCourse.ClientID%>").change(function () {
                ChangeCourseId();

            });

        }
    }


    $(document).ready(function () {

        $("#<%=ddlCourse.ClientID%>").change(function () {
            ChangeCourseId();
        });
    });
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

    function ChangeCourseId() {
        $.ajax({
            type: "POST",
            url: "/WebServices/CommonWebServices.asmx/UpdateCourseId",
            data: '{"courseId":"' + $("#<%=ddlCourse.ClientID%>").val() + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (response) {
                location.href = ("/bookseat/" + RemoveChahracterfromCorse($("#<%=ddlCourse.ClientID%> option:selected").text())).toLowerCase();
            },
            error: function (xml, textStatus, errorThrown) {
                alert(xml.status + "||" + xml.responseText);
            }
        });
    }

    
</script>

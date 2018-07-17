<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcCollegeCounsellingCollegeList.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcCollegeCounsellingCollegeList" %>
<%@ Register Src="~/UserControl/UcIntertestedCollegeList.ascx" TagPrefix="AJ" TagName="IntertestedCollege" %>
<%@ Register Src="~/UserControl/UcStudentPersonelInfo.ascx" TagPrefix="AJ" TagName="StudentPersonelInfo" %>
<div class="fleft">
    <div class="boxPlane mainBG">
        <AJ:StudentPersonelInfo ID="UcStudentPersonelInfo" runat="server"></AJ:StudentPersonelInfo>
    </div>
    <div id="divWishlistInformation" class="divCustomControlOuter mainBG " style="display: none">
        <img src="/image.axd?Common=closebox.png" class="close btn_close" onclick="showHideControls('divWishlistInformation', 'hide')" />
        <h3 class="streamCompareH3">
            Wish List[<span id="spanCartTotalCount">0</span>]</h3>
        <hr class="hrline" />
        <div class="boxPlane">
            <AJ:IntertestedCollege ID="ucIntertestedCollege" runat="server"></AJ:IntertestedCollege>
        </div>
    </div>
    <div class="boxPlane" style="height: 635px; overflow-x: scroll;">
        <asp:DataList ID="dtlCollegeList" RepeatDirection="Horizontal" CssClass="dtList"
            RepeatColumns="6" runat="server">
            <ItemTemplate>
                <center style="position: relative;">
                    <div class="divCollegeOuter" title="<%# Eval("CollegeBranchName")%>">
                        <div class="collegeDetails">
                            <h3 style="padding: 3px; color: #415983; margin-top: 0px; font-size: 12px; font-weight: bold;
                                margin-bottom: 0px; height: 40px; background-color: #eff2f7; overflow: hidden;">
                                <%# Eval("CollegeBranchName")%></h3>
                            <hr class="hrline" style="margin-top: 0px; width: 100%;" />
                            <a href='<%#IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("College-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse(Eval("CourseName").ToString())+"/"+ IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))).ToLower() %>'
                                title=" <%# Eval("CollegeBranchName")%>">
                                <img title='<%# Eval("CollegeBranchName")%>' alt='<%# Eval("CollegeBranchName")%>'
                                    height="170px;" width="170px;" src='<%# String.Format("{0}{1}","/image.axd?College=",Eval("CollegeBranchLogo")==null ?"NoImage.jpg":Eval("CollegeBranchLogo")) %>' />
                            </a>
                            <hr class="hrline" style="width: 100%;" />
                            <strong style="font-size: 12px; color: #134d62; background-color: #e1e1e1; display: block;
                                padding: 5px 0px; margin-top: -7px;">Course :
                                <%# Eval("CourseName")%></strong>
                            <%--   
                         <label class="displayBlock">Location :
                                <%# Eval("CollegeBranchCityName")%>
                                </label> 
                                <label>Eastablishment : 
                                <%# Eval("CollegeBranchEst")%></label>--%>
                        </div>
                        <div class="quickView">
                            <span><a class="button" href='<%#IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("College-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse(Eval("CourseName").ToString())+"/"+ IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))).ToLower() %>'
                                title=" <%# Eval("CollegeBranchName")%>">College Details</a> </span>
                            <br />
                            <br />
                            <span>
                                <%# String.Format("{0}", Convert.ToDateTime(Eval("CollegeBranchCourseAdmissionDate")) > DateTime.Now ? "<strong class='displayBlock NormalFont'>Last Date to Apply  </strong> " + "<label style='border-bottom:1px solid #76b5da; margin-bottom:15px;' class='displayBlock'>" + Convert.ToDateTime(Eval("CollegeBranchCourseAdmissionDate")).ToString("ddd,dd MMM, yyyy") + "</label>" : "<strong class='displayBlock NormalFont'> Admission already Closed on</strong>" + "<label style='border-bottom:1px solid #76b5da; margin-bottom:15px;' class='displayBlock'>" + Convert.ToDateTime(Eval("CollegeBranchCourseAdmissionDate")).ToString("ddd, dd MMM , yyyy") + "</label>")%>
                                <%# String.Format("{0}", Convert.ToBoolean(Eval("CollegeBranchCourseOnlineStatus")) == true ? "<span class='greenbutton'  onclick='AddIntertestedCollege(" + Eval("CollegeBranchCourseId") + ");showHideControls(\"divWishlistInformation\", \"show\");return false;'>Add to wish list</span> " : "<span class='redDisableButton' > Admission Closed</span>")%>
                            </span>
                        </div>
                    </div>
                </center>
            </ItemTemplate>
        </asp:DataList>
    </div>
    <a id="lnkChooseCollege" onclick="return CheckCollegeCount()" class="button fleft">Next</a>
</div>
<%--Mask--%>
<div id="popUpOuterDiv" class="hide">
</div>
<%--    End--%>
<script type="text/javascript">
    BindCollegeList();

    function AddIntertestedCollege(collegeBranchCourseId) {
        if ($("[id*=hdnTotalCount]").val() < 5) {
            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/InsertStudentInterestedCollege",
                data: '{collegeBranchCourseId:"' + collegeBranchCourseId + '"}',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    window.BindCollegeList();
                },
                error: function (xml, textStatus, errorThrown) {
                    // alert(xml.status + "||" + xml.responseText);
                }
            });

        } else {
            alert("Sorry you cann't choose more than 5 colleges");
            return false;
        }

    }

    function showHideControls(control, action) {


        if (action == "hide") {

            document.getElementById(control).style["display"] = 'none';
            document.getElementById("popUpOuterDiv").style["display"] = 'none';


        }
        else {

            document.getElementById(control).style["display"] = 'block';
            $(control).animate({ 'left': '30%' }, 500);
            // document.getElementById("popUpOuterDiv").style["display"] = 'block';

        }

    }


    function CheckCollegeCount() {

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "/WebServices/CommonWebServices.asmx/GetIntertestedForConsulling",
            data: "{}",
            async: true,
            success: function (response) {

                var xmlDoc = $.parseXML(response.d);

                var xml = $(xmlDoc);
                var collegeList = xml.find("Table");
                var rowIndex = 0;

                $.each(collegeList, function (i) {

                    rowIndex = ++i;


                });

                if (rowIndex > 0) {
                    location.href = "/counselling/FinalUserIntertestedList.aspx";
                    return true;
                }
                else {

                    alert('Please Choose at least one college to procedue further');
                    return false;
                }



            },
            failure: function (response) {
                // alert(response.d);
                alert('Please Choose at least one college to procedue further');
                return false;
            },
            error: function (xml, textStatus, errorThrown) {
                //alert(xml.status + "||" + xml.responseText);
                alert('Please Choose at least one college to procedue further');
                return false;
            }
        });

    }
   
</script>
<script type="text/javascript">

    hideShow();

    function hideShow() {
        $(".quickView").hide();

        $(".collegeDetails").show();
    }

    $('.divCollegeOuter').live('mouseenter', function () {

        $(this).find(".quickView").fadeIn();


        $(this).find(".collegeDetails").fadeTo(100, 0.5);

    })


    $('.divCollegeOuter').live('mouseleave', function () {
        $(this).find(".quickView").hide();

        $(this).find(".collegeDetails").fadeTo(100, 1.0);

    });

</script>

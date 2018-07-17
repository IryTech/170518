<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChooseCollege.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.counselling.ChooseCollege" %>

<%@ Register Src="~/UserControl/UcCollegeCounsellingCollegeList.ascx" TagPrefix="AJ" TagName="CollegeSearch" %>
<asp:content id="BodyContent" runat="server" contentplaceholderid="cphBody">
    
 <div class="fleft  marginbottom ">
<div class="fright" style="position:absolute; right:3%; margin-top:10px;">
<a href="#" onclick="CheckCartCount()" class="spanTotalCount">
Wish List[
<span id="spanCartTotalCount">0</span>
]
</a></div>
<div class="fleft">
    <AJ:CollegeSearch ID="collegeSearch" runat="server" />
    <div class="clearBoth"></div>
    </div>

    </div>
    <script type="text/javascript">
        function CheckCartCount() {

            if ($("[id*=hdnTotalCount]").val() > 0) {
                $("[id*=grdCollegeList]").css("display", "block");
                showHideControls("divWishlistInformation", "show");
            } else {
                $("[id*=grdCollegeList]").css("display", "none"); showHideControls("divWishlistInformation", "hide");
            }
        }

    </script>

    </asp:content>

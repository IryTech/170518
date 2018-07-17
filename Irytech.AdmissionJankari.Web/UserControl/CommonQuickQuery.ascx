<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommonQuickQuery.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CommonQuickQuery" %>

<%@ Register Src="~/UserControl/PackageList.ascx" TagPrefix="AJ" TagName="PackageList" %>
<%@ Register Src="~/UserControl/UcCommonPayment.ascx" TagPrefix="AJ" TagName="Payment" %>
<script src="/Js/JscriptHomeCommonQuery.js" type="text/javascript"></script>
<asp:HiddenField ID="hndCourseId" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hdnCourseInApp" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hndShowPackage" runat="server" ClientIDMode="Static" Value="0" />

<div class="box1 bgblue">
    <h3 class="streamCompareH3">Admission Query</h3>

    <hr class="hrline" />

    <span>
        <label id="lblMsg" title="Message"></label>
    </span>
    <div class="box">
        <ol class="marginleft style">

            <li>
                <strong><%=Resources.label.Course%> </strong>
                <select id="ddlCourse" title="Select course"></select>
                <label id="lblCourseError" class="hide error" title="Please Select course"></label>
            </li>
            <li>
                <strong><%=Resources.label.Name%></strong><input type="text" placeholder="Enter your name" id="txtName" title="Enter your name" />
                <label id="lblNameError" class="hide error" title="Please Enter Name"></label>
            </li>

            <li>
                <strong><%=Resources.label.Email%></strong>

                <input type="text" id="txtEmailId" placeholder="Enter your email id" title="Enter your email id" />
                <label id="lblEmailIdError" class="hide error" title="Please Enter Email Id"></label>
            </li>
            <li>
                <strong><%=Resources.label.Mobile%></strong><input type="text" id="txtNumber" placeholder="Enter your 10 digit mobile number"
                    title="Enter your 10 digit mobile number" />
                <label id="lblNumberError" class="hide error" title="Please Enter Mobile"></label>
            </li>
            <li>
                <strong>City:</strong><select id="ddlCity" title="Select city"></select>
                <label id="lblCityError" class="hide error" title="Please Enter City"></label>
            </li>
            <li id="otherCity" style="display: none;">
                <input type="text" title="Enter City Name" placeholder="Please Enter Name" id="txtOtherCity" />
                <label id="lblOtherCity" class="hide error" title="Please Enter other City"></label>
            </li>
            <li>
                <strong><%=Resources.label.Query%></strong><textarea id="txtQuery" title="Enter your query" placeholder="Enter your query"></textarea>
                <label id="lblQueryError" class="hide error" title="Please Enter Query"></label>
            </li>
            <li>
                <input type="checkbox" checked="checked" />
                I agree 
            <a href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("Terms-and-Conditions").ToLower()%>' target="_blank">T&amp;C</a> and  
            <a href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("Privacy-Policy").ToLower()%>' target="_blank">Privacy Policy</a></li>
        </ol>
        <footer>
            <input type="button" title="Click to finish post query" id="butSubmit" value="<%=Resources.label.SaveQuery%>" />
            <input type="button" title="Clear Field " id="butClear" onclick="ClearControl()" value="Clear" />
            <br />
            <span id="lbllErrMsg" class="hide">
                <label id="lbllErMsg" title="Message" class="msg">
                    You must fill in all of the mandatory fields
                </label>
            </span>

        </footer>

        <div id="divPackageList" class="popup_block" style="width: 750px!important">
            <AJ:PackageList ID="packageList" runat="server" />

        </div>
        <a href="#" style="display: none;" id='sndAddCollegePresedentSpeech'></a>

        <div id="divUniversityCategoryInsert" class="popup_block" style="width: 100%!important">
            <AJ:Payment ID="ucPayment" runat="server" />
           

        </div>
        <a href="#" id="sndAddCollegePresedentSpeechsdasd" style="display: none;"></a>
    </div>
    <style>
        .right {
            color: green !important;
            margin: 50px;
        }

        .wrong {
            color: red !important;
            margin: 50px;
        }
    </style>
</div>



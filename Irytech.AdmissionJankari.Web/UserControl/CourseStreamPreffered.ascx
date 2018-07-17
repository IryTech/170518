<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CourseStreamPreffered.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CourseStreamPreffered" %>
<div class="prohead"><strong class="RedrightImglink">Stream Preference</strong></div>
<div id="courseStreamPreference">
<label id="noCourseStreamPreference" class="hide info"></label>

</div>
<div id="divStreamInsert" class="hide" style="width:100%;">
<fieldset><legend>
Insert Stream Preference
</legend>
<ul>
    <li><strong style="text-transform:none!important">You have selected course:</strong>&nbsp;&nbsp;
<span class="text2" id="slctlabelStream" ></span>
</li>
<li><label>Stream:</label>
<select id="slctStreamPrefernceInsert" title="Select stream" disabled="disabled"></select>
</li>
<li>
<label> &nbsp;</label>
<input type="button" id="btnStreamInsert" value="Add" tabindex="3" onclick="StreamPrefernceInsert()" title="Click to submit stream preference" />
</li>
</ul>


</fieldset></div>

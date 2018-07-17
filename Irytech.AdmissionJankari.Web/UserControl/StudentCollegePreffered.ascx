<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentCollegePreffered.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.StudentCollegePreffered" %>

<div class="prohead"><strong class="RedrightImglink">College Preference</strong></div>
<div id="CollegePreFfered" >
    <asp:HiddenField runat="server" id="hdnCourse">
    </asp:HiddenField>
<label id="noCollegePrefer" class="hide info"></label>

</div>
<div id="divCollegeInsert" class="hide" style="width:100%;"><input type="hidden" id="hdnCollegeCount" />
<fieldset><legend>
Insert College Preference
</legend>
<ul>
<li><strong style="text-transform:none!important">You have selected course:</strong>&nbsp;&nbsp;
<span class="text2" id="slctlabel"></span>
</li>
<li>
<label>College:</label>
<input type="text" id="txtCollegeInsert" tabindex="2" title="Enter college" />
</li><li>
<label></label>
<input type="button" id="btnInsertCollege" value="Add" tabindex="3" onclick="InsertCollege()" title="Click to submit college preference" />
</li>
</ul>


</fieldset></div>



  
    

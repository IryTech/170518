<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentCoursePreffered.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.StudentCoursePreffered" %>
<div class="prohead"><strong class="RedrightImglink">Course Preference</strong></div>
<div id="coursePreference">
<label id="noCoursePreference" class="hide info"></label>

</div>

 <div class="popup_block" id="divCoursePrefereInsert">
       <fieldset><legend>Choose Course </legend>
      <ul>
    <li>
    <label><%=Resources.label.Course %></label>
    <select id="slctChooseCoursePrefer" title="Please select course"></select></li>
    <li>
    <label></label>
    <input type="button" title="Update course" value="Update"  class="button" onclick="UpdateCoursePrefer();return false" />
    </li>
    </ul></fieldset>
    </div>
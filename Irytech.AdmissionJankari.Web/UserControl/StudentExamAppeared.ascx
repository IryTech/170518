<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentExamAppeared.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.StudentExamAppeared" %>


<label id="noExamAppeared" class="hide info "></label>
<input type="hidden" id="hdnExamCount" value="1" />
<div class="prohead">
<strong><a id="sndExam" href="#"  onclick="showExam();return false;" class="RedrightImglink Profiledis">Add Exam</a></strong>
</div>
<div class="addMore popup_block" id="examPopup" >
<fieldset><legend>Add Your Exam Appeared</legend>
<ul>
<li id="liExam0"><label>ExamName</label>
<input type="text" id="txtExamName0" title="Please Enter Exam" tabindex="1" />
</li>
<li id="liRank0" class="liExam"><label>Exam Rank</label>
<input type="text" id="txtExamRank0" title="Please Enter Rank" tabindex="2" />
</li>
<li><input type="button" title="Please Enter Exam" value="submit"  class="submit" tabindex="3" onclick="InsertExamAppeared()" />  <a id="anchAdd" style="float:right" href="#" class="RedrightImglink" onclick="AddExamFields();return false;">Add More</a></li>
</ul>



</fieldset>

</div>

<div id="examAppeared" class="box marginall">
</div>


<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeCityPreference.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeCityPreference" %>
<div class="prohead"><strong class="RedrightImglink">City Preference</strong></div>
<div id="cityPreference">
<label id="noCityPreference" class="hide info"></label>

</div>

<div id="divCityInsert" class="hide" style="width:100%;" >
<fieldset><legend>
Insert City Preference
</legend>
<ul>
<li><label>City:</label>
<select id="slctCityPrefernceInsert" title="Select city" tabindex="1"></select>
</li>
<li>
<label>&nbsp;</label>
<input type="button" id="btnCityInsert" value="Submit" tabindex="2" onclick="CityPrefernceInsert()" title="Click to insert city preference" />
</li>
</ul>


</fieldset></div>

  <script type="text/javascript">
      var cityList = "/WebServices/CommonWebServices.asmx/GetAllCityWithoutId";
    BindDropDown($("#slctCityPrefernceInsert"), cityList);
    </script>
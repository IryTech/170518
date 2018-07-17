<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="AddUniversityMaster.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.AddUniversityMaster" %>

<%@ Register Src="../../UserControl/FckEditorCostomize.ascx" TagName="FckEditorCostomize"
    TagPrefix="AJ" %>
<%@ Register Src="~/UserControl/AjaxFileUpload.ascx" TagName="FileUpload" TagPrefix="Aj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
     
    <asp:HiddenField ID="hdnUniversityUrl" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdnUniversityTag" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdnUniversityTitle" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdnUniversityMetaDesc" runat="server"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="hdnCourseStreamMaster"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="hdnCountry" Value="0"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="hdnState" Value="0"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="hdnCity" Value="0"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="hdnUniversityCategory" Value="0"></asp:HiddenField>
    <ul class="addPage_utility">
        <li class="fright" style="width: 160px !important;">
            <div class="navbar-inner">
                <a href="UniversityMaster.aspx" class="viewIco">University Master </a>
                <div class="clear">
                </div>
            </div>
        </li>
        <li class="fright" style="width: 72px !important;">
          <a href="#" id='sndAddCollegeFactor' class="insertIco" onclick="OpenPoup('divRankSourceInsert','650px','sndAddCollegeFactor');return false;">
                                 <img src="/AdminPanel/Images/CommonImages/uploadExcel.png" /></a>
          
            <asp:ImageButton ID="btnSeeExcelFormat" runat="server" ImageUrl="~/AdminPanel/Images/CommonImages/downloadExcel2.png"
                ToolTip="Download Excel Format"  />
        </li>
    </ul>
    <div class="grdOuterDiv">
        <fieldset>
            <legend>
                <asp:Label ID="lblHeader" runat="server"></asp:Label><asp:Label ID="lblInsertUpdate"
                    runat="server" Text=""></asp:Label></legend>
            <%--class="multifield"     --%>
            <ul class="width48perc fleft">
                <li>
                    <label>
                        University Name</label>
                    <asp:TextBox ID="txtUniversityName" runat="server" CssClass="autocomplete" Width="63%"
                        Style="background: none !important; text-indent: 5px !important;" TabIndex="1"
                        ToolTip="Please Enter University Name" onkeyup="keyup(this)"></asp:TextBox>
                    <span id="msgName" class="error1"></span></li>
                <li>
                    <label>
                        University Popular Name</label>
                    <asp:TextBox ID="txtUniversityPopularName" runat="server" TabIndex="2" ToolTip="Please Enter University Popular Name"></asp:TextBox>
                </li>
                <li>
                    <label>
                        University short Name
                    </label>
                    <asp:TextBox ID="txtUniversityshortName" runat="server" TabIndex="3" ToolTip="Please Enter University short Name"></asp:TextBox>
                </li>
                <li>
                    <label>
                        University Category Name</label>
                    <select id="ddlUniversityCategoryName" tabindex="4" disabled="disabled">
                        <option value="0" selected="selected">Select University</option>
                    </select>
                    <span id="msgUniversity" class="error1"></span></li>
                <li>
                    <label>
                        University Established</label>
                    <asp:TextBox ID="txtUniversityEstablished" runat="server" TabIndex="5" ToolTip="Please Enter University Established"></asp:TextBox>
                    <span id="msgEst" class="error1"></span></li>
                <li>
                    <label>
                        University Website</label>
                    <asp:TextBox ID="txtUniversityWebsite" runat="server" TabIndex="6" ToolTip="Please Enter University Website"></asp:TextBox>
                </li>
                <li id="uploadImagePanel">
                    <label>
                        University Logo</label>
                    <asp:HiddenField runat="server" ID="hdnFileName" />
                    <Aj:FileUpload ID="FlUpload" runat="server" tabindex="7" uploadToDirectory="../../Image/Colleges/" />
                    <%--  <asp:FileUpload ID="Logoupload" runat="server" TabIndex="7" ToolTip="Please Enter University Logo"/>
                     <asp:RequiredFieldValidator ID="rfvImageUpload" runat="Server" ControlToValidate="Logoupload"
                                ValidationGroup="Upload" />--%>
                    <asp:Button ID="btnLogoUpload" runat="server" Visible="false" Text="Upload"  />
                    <asp:Image runat="server" ID="imgNews" Width="25px" Height="25px" Style="margin-top: 3px;
                        display: inline-block;" Visible="true"></asp:Image>
                </li>
            </ul>
            <ul class="width48perc fleft">
                <li>
                    <label>
                        University Email Id</label>
                    <asp:TextBox ID="txtUniversityEmailId" runat="server" TabIndex="8" ToolTip="Please Enter University Email Id"
                        onchange='CheckMailId()'></asp:TextBox>
                    <span id="msgEmail" class="error1"></span></li>
                <li>
                    <label>
                        PhoneNo</label>
                    <asp:TextBox ID="txtUniversityPhoneNo" runat="server" TabIndex="9" ToolTip="Please Enter Phone Number"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Mobile</label>
                    <asp:TextBox ID="txtUniversityMobile" runat="server" TabIndex="10" ToolTip="Please Enter University Mobile"
                        onchange='CheckMoNo()'></asp:TextBox>
                    <span id="msgMobile" class="error1"></span></li>
                <li>
                    <label>
                        Fax</label>
                    <asp:TextBox ID="txtUniversityFax" runat="server" TabIndex="11" ToolTip="Please Enter University Fax"></asp:TextBox>
                    <span class="error1" id="msgFax"></span></li>
                <li>
                    <label>
                        Country</label>
                    <select id="ddlUniversityCountryName" tabindex="12" style="width: 174px !important;">
                        <option value="0" selected="selected">Select Country</option>
                    </select>
                    <span id="msgCountry" class="error1"></span></li>
                <li>
                    <label>
                        State</label>
                    <select id="ddlUniversityStateName"  tabindex="13">
                        <option value="0" selected="selected">Select State</option>
                    </select>
                    <span id="msgState" class="error1"></span></li>
                <li>
                    <label>
                        City</label>
                    <select id="ddlUniversityCityName" tabindex="14" >
                        <option value="0" selected="selected">Select City</option>
                    </select>
                    <span id="msgCity" class="error1"></span></li>
            </ul>
            <ul>
                <li>
                    <label>
                        Address</label>
                    <asp:TextBox ID="txtUniversityAddrs" runat="server" TextMode="MultiLine" TabIndex="15"
                        ToolTip="Please Enter University Address"></asp:TextBox>
                    <span id="msgAddrss" class="error1"></span></li>
                <li style="width: 100% !important;">
                    <label>
                        University Desc</label>
                    <div class="fleft" style="margin: 3px 5px;">
                        <Aj:FckEditorCostomize ID="fckUniversityDesc" tabindex="16" runat="server" />
                    </div>
                </li>
            </ul>
            <h5 style="font-size: 17px !important; clear: both; font-weight: normal !important;
                border-bottom: 1px dashed #e1e1e1; padding: 3px 60px !important;">
                SEO Tool</h5>
            <ul>
                <li>
                    <label>
                        University Title</label>
                    <asp:TextBox ID="txtUniversityTitle" runat="server" TabIndex="18" CssClass="autocomplete"
                        Style="width: 60%; background: none !important; text-indent: 5px !important;"
                        ToolTip="Please Enter University Title" onkeyup="titlekeyup(this,'universityTitle')"></asp:TextBox>
                    <div id="universityTitle">
                    </div>
                </li>
                <li>
                    <label>
                        University Url</label>
                    <asp:TextBox ID="txtUniversityUrl" runat="server" TabIndex="17" CssClass="autocomplete"
                        Style="width: 60%; background: none !important; text-indent: 5px !important;"
                        ToolTip="Please Enter University Url" onkeyup="urlkeyup(this,'universityUrl')"></asp:TextBox>
                    <div id="universityUrl">
                    </div>
                </li>
                <li>
                    <label>
                        University Meta Desc</label>
                    <asp:TextBox ID="txtUniversityMetaDesc" runat="server" TabIndex="20" Style="width: 59.5%;
                        max-width: 100%;" ToolTip="Please Enter University Meta Desc" TextMode="MultiLine"
                        onkeyup="tagdesckeyup(this,'universityMetaDesc')"></asp:TextBox>
                    <div id="universityMetaDesc">
                    </div>
                </li>
                <li>
                    <label>
                        University Meta Tag</label>
                    <asp:TextBox ID="txtStreamMetaTag" runat="server" TabIndex="19" Style="width: 59.5%;
                        max-width: 100%;" TextMode="MultiLine" ToolTip="Please Enter University Meta Tag"
                        onkeyup="tagkeyup(this,'universityTag')"></asp:TextBox>
                    <div id="universityTag">
                    </div>
                </li>
                <li>
                    <label>
                        &nbsp;</label>
                    <asp:Button ID="btntUniversityMaster" runat="server" Text="Save" TabIndex="21" ToolTip="Please Submit"
                        OnClick="BtntUniversityMasterClick" />
                    <input id="btnReset" type="button" value="Clear" onclick="ClearFields()" tabindex="22"
                        title="Please Submit To Clear" />
                </li>
            </ul>
        </fieldset>
        <div id="divRankSourceInsert" class="popup_block width43perc">
            <ul>
                <li class="opt-barlist">
                    <label style="width: 80px !important;">
                        Upload File :</label>
                    <asp:FileUpload ID="fulUploadExcel" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvUpload" runat="server" Display="Dynamic" ControlToValidate="fulUploadExcel"
                        SetFocusOnError="true" ValidationGroup="GrUpload">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revExcelUpload" runat="server" Display="Dynamic"
                        ControlToValidate="fulUploadExcel" ValidationGroup="GrUpload" SetFocusOnError="true">
                    </asp:RegularExpressionValidator>

                </li>
                <li>
                  <asp:ImageButton ID="btnUpload" runat="server" ImageUrl="~/AdminPanel/Images/CommonImages/uploadExcel.png"
                ToolTip="Upload Excel Format" ValidationGroup="GrUpload" TabIndex="2"  OnClick="BtnUploadClick" />
                </li>
            </ul>
        </div>
    </div>
    <script src="../JS/CollegeBranch.js" type="text/javascript"></script>
    <script src="../JS/commonscripts.js" type="text/javascript"></script>
    <script type="text/javascript">

        $("#<%=btntUniversityMaster.ClientID %>").click(function () {
            var iserror = false;
            if (!validateDetails())
                iserror = true;
            if (iserror) return false;
        });
               
    </script>
    <style type="text/css">
        .error1
        {
            color: red;
            font-style: italic;
            font-size: 11px;
        }
    </style>
    <script type="text/javascript">
        var countryUrl = "../../WebServices/CommonWebServices.asmx/GetCountryList";
        var universityUrl = "../../WebServices/CommonWebServices.asmx/BindUniversityCategory";
        var stateUrl = "../../WebServices/CommonWebServices.asmx/GetAllState";
        var cityUrl = "../../WebServices/CommonWebServices.asmx/GetAllCityWithoutId";

        $(document).ready(function () {

            var universUrl = location.href;
            if (universUrl.indexOf("UniversityId=") > -1) {
                BindDropDown($("#ddlUniversityCategoryName"), universityUrl, CategoryValueSelected, $("#<%=hdnUniversityCategory.ClientID %>").val());
                BindDropDown($("#ddlUniversityCountryName"), countryUrl, CountrySelectedValue, $("#<%=hdnCountry.ClientID %>").val());
                BindDropDown($("#ddlUniversityStateName"), stateUrl, StateValueSelected, $("#ctl00_ContentPlaceHolderMain_hdnState").val());
                BindDropDown($("#ddlUniversityCityName"), cityUrl, CitySelectedValue, $("#ctl00_ContentPlaceHolderMain_hdnCity").val());

            } else {
                BindDropDownForData($("#ddlUniversityCategoryName"), universityUrl);
                BindDropDownForData($("#ddlUniversityCountryName"), countryUrl);

            }
        });

        function CategoryValueSelected(data, objControl, selectedValue) {
            PopulateControl(data, objControl);
            $("#ddlUniversityCategoryName").val(selectedValue);

        }
        function CountrySelectedValue(data, objControl, selectedValue) {
            PopulateControl(data, objControl);
            $("#ddlUniversityCountryName").val(selectedValue);

        }
        function StateValueSelected(data, objControl, selectedValue) {
            PopulateControl(data, objControl);
            $("#ddlUniversityStateName").val(selectedValue);

        }
        function CitySelectedValue(data, objControl, selectedValue) {
            PopulateControl(data, objControl);
            $("#ddlUniversityCityName").val(selectedValue);

        }


        $("#ddlUniversityCategoryName").change(function () {

            $("#<%=hdnUniversityCategory.ClientID %>").val($("#ddlUniversityCategoryName").val());

        });
        $("#ddlUniversityCountryName").change(function () {
            if ($("#ddlUniversityCountryName").val() > 0) {
                State($("#ddlUniversityStateName"), $("#ddlUniversityCountryName"));
                $("#<%=hdnCountry.ClientID %>").val($("#ddlUniversityCountryName").val());
            } else {
                alert('disable');
                $("#<%=hdnCountry.ClientID %>").val(0);
                $("#<%=hdnState.ClientID %>").val(0);
                $("#<%=hdnCity.ClientID %>").val(0);
                $("#ddlUniversityStateName").val(0); $("#ddlUniversityCityName").val(0);
                $("#ddlUniversityStateName").attr("disabled", "disabled");
                $("#ddlUniversityCityName").attr("disabled", "disabled");
            }
        });
        $("#ddlUniversityStateName").change(function () {
            if ($("#ddlUniversityStateName").val() > 0) {
                City($("#ddlUniversityStateName"), $("#ddlUniversityCityName"));
                $("#<%=hdnState.ClientID %>").val($("#ddlUniversityStateName").val());
            } else {
                $("#<%=hdnState.ClientID %>").val(0);
                $("#<%=hdnCity.ClientID %>").val(0);
                $("#ddlUniversityCityName").val(0);
                $("#ddlUniversityCityName").attr("disabled", "disabled");
            }
        });
        $("#ddlUniversityCityName").change(function () {
            if ($("#ddlUniversityCityName").val() > 0) {
                $("#<%=hdnCity.ClientID %>").val($("#ddlUniversityCityName").val());
            } else {
                $("#<%=hdnCity.ClientID %>").val(0);
                $("#ddlUniversityCityName").val(0);
                $("#ddlUniversityCityName").attr("disabled", "disabled");
            }
        });

        function validateDetails() {

            var number = /^[0-9]*$/;
            var isError = false;

            var name = $("#<%=txtUniversityName.ClientID %>");
            var fax = $("#<%=txtUniversityFax.ClientID %>");
            var est = $("#<%=txtUniversityEstablished.ClientID %>");
            var emailId = $("#<%=txtUniversityEmailId.ClientID %>");
            var mobile = $("#<%=txtUniversityMobile.ClientID %>");

            if (name.val() == "") {
                $("#msgName").html("Name Required!");
                name.focus();
                isError = true;
            }
            else {
                $("#msgName").html(" ");
            }
            if (est.val().trim().length != 0) {
                if (!number.test(est.val())) {
                    $("#msgEst").text("Only Numeric Year");
                    isError = true;
                } else {
                    $("#msgEst").html(" ");
                }
            } else {
                $("#msgEst").html(" ");
            }
            if (emailId.val().trim().length != 0) {
                if (!CheckEmail(emailId)) {

                    isError = true;
                }
            }
            if (mobile.val().length != 0) {
                if (!CheckMoNumber(mobile)) {

                    isError = true;
                }
            }
            if (fax.val().trim().length != 0) {
                if (!number.test(fax.val())) {
                    $("#msgFax").text("Only Numeric");
                    isError = true;
                } else {

                    $("#msgFax").html(" ");
                }
            } else {

                $("#msgFax").html(" ");
            }


            return !isError;
        }

        function CheckEmail(emailId) {
            var reEmail = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,6}$/;

            var isError = false;
            if (emailId.val().trim().length != 0) {
                if (!reEmail.test(emailId.val())) {
                    $("#msgEmail").text("Check Email Format");
                    isError = true;
                } else {
                    $("#msgEmail").html(" ");

                }
            } else {
                $("#msgEmail").html(" ");

            }

            return !isError;
        }
        function CheckMailId() {
            CheckEmail($("#<%=txtUniversityEmailId.ClientID %>"));
        }
        function CheckMoNo() {
            CheckMoNumber($("#<%=txtUniversityMobile.ClientID %>"));
        }

        function CheckMoNumber(mobile) {
            var mobileNo = /^[1-9][0-9]{9}$/;
            var isError = false;
            if (mobile.val().trim().length != 0) {
                if (!mobileNo.test(mobile.val()) && mobile.val() > 0) {
                    $("#msgMobile").text("Numeric starting with 7 or 8 or 9");
                    isError = true;
                } else if (mobile.val().length < 10 || mobile.val().length > 10) {
                    $("#msgMobile").text("Only 10 digit Numeric");
                    isError = true;
                } else {
                    $("#msgMobile").html(" ");
                }
            } else {
                $("#msgMobile").html(" ");

            }
            return !isError;
        }
        function ClearFields() {
            $("#<%=txtUniversityName.ClientID %>").val('');
            $("#<%=txtUniversityUrl.ClientID %>").val('');
            $("#<%=txtUniversityTitle.ClientID %>").val('');
            $("#<%=txtStreamMetaTag.ClientID %>").val('');
            $("#<%=txtUniversityMetaDesc.ClientID %>").val('');
            $("#<%=txtUniversityPopularName.ClientID %>").val('');
            $("#<%=txtUniversityshortName.ClientID %>").val('');
            $("#<%=txtUniversityEstablished.ClientID %>").val('');
            $("#<%=txtUniversityEmailId.ClientID %>").val('');
            $("#<%=txtUniversityPhoneNo.ClientID %>").val('');
            $("#<%=txtUniversityMobile.ClientID %>").val('');
            $("#<%=txtUniversityFax.ClientID %>").val('');
            $("#<%=txtUniversityAddrs.ClientID %>").val('');
            $("#<%=txtUniversityFax.ClientID %>").val('');
            $("#<%=txtUniversityWebsite.ClientID %>").val('');
            window.scrollTo(0, 0);
        }
        function keyup(control) {
            CopyContent(control, $("#<%=txtUniversityUrl.ClientID %>"), $("#<%=txtStreamMetaTag.ClientID %>"), $("#<%=txtUniversityTitle.ClientID %>"), $("#<%=txtUniversityMetaDesc.ClientID %>"));
        }
        function urlkeyup(control, cssName) {
            GetUrlLength(control, cssName, $("#<%=hdnUniversityUrl.ClientID %>"));
        }
        function tagkeyup(control, cssName) {
            GetUrlLength(control, cssName, $("#<%=hdnUniversityTag.ClientID %>"));
        }
        function tagdesckeyup(control, cssName) {
            GetUrlLength(control, cssName, $("#<%=hdnUniversityMetaDesc.ClientID %>"));
        }
        function titlekeyup(control, cssName) {
            GetUrlLength(control, cssName, $("#<%=hdnUniversityTitle.ClientID %>"));
        };
    </script>
</asp:Content>

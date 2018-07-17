<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeBranchBasicInfo.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeBranchBasicInfo" %>
<script src="/AdminPanel/JS/CollegeBranch.js" type="text/javascript"></script>
<fieldset id="basicInfo">
    <legend>College Branch Basic Info</legend>
    <ul>
        <li>
            <label>
                Institute Type</label>
            <asp:DropDownList runat="server" ID="ddlInstituteType" TabIndex="1" ToolTip="Please Select Institute" >
            </asp:DropDownList>
                         
        </li>
       <li>
            <label>
               Institute Associate</label>
            <asp:DropDownList runat="server" ID="ddlCollegeAssociate" TabIndex="3" ToolTip="Please Select College Associate" >
            </asp:DropDownList>
                         
        </li>
        <li>
            <label></label>
             <span>(Enter 3 words to check college group name)</span> 
               
          
                
        </li>
        <li>
            <label>
                Institute Group</label>    
                <asp:TextBox ID="txtCollegeGroup" runat="server" TabIndex="4" ToolTip="Please Enter Institute Group" ></asp:TextBox>      
      
                
        </li>
        <li>
            <label>
            Institute Name</label>
            <asp:TextBox ID="txtCollegeBranch" runat="server" TabIndex="5" ToolTip="Please Enter CollegeBranch"></asp:TextBox>
           
               
        </li>
          <li >
            
            <label>Management</label>
              <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="rbtManagement" Width="500px" class="tbl">
              </asp:RadioButtonList>        
        </li> <li >
            
            <label>Establishment</label>
                  <asp:TextBox ID="txtCollegeEst" runat="server" TabIndex="6" ToolTip="Please Enter College Establishment"></asp:TextBox>
                 
        </li>
        
        <li >
            
            <label>Popular Name</label>
                  <asp:TextBox ID="txtCollegePopularName" runat="server" TabIndex="6" ToolTip="Please Enter College Popular Name"></asp:TextBox>
                
        </li>
          <li>
            <label>
               City</label>
            <asp:DropDownList runat="server" ID="ddlCollegeCity" TabIndex="12" ToolTip="Please Select City" >
            </asp:DropDownList>
                         
        </li>
                <li>
            <label>WebSite</label>
                  <asp:TextBox ID="txtCollegeWebsite" runat="server" TabIndex="7" ToolTip="Please Enter College WebSite"></asp:TextBox>
                
        </li>
         <li>
            <label>Status</label>
             <asp:CheckBox runat="server" ID="chkCollegeStatus" 
                 ToolTip="Please Check Status" TabIndex="8" CssClass="chkCollege">
             </asp:CheckBox>         
        </li><li >
            
            <label>Description</label>
                  <asp:TextBox ID="txtCollegeDesc" runat="server" TabIndex="6" TextMode="MultiLine" ToolTip="Please Enter College Establishment"></asp:TextBox>
                 
        </li><li >
            
            <label></label>
                  
      <input type="button" value="Next" style="float:right" id="btnNextContact" style="float:right" onclick="showContactForm()" />   </li>
          </ul>
</fieldset>
<fieldset id="basicInfoContact" style="display:none">
    <legend>Contact Details</legend>
    <ul>  <li>
            <label>
                Institute Group</label>
        <asp:DropDownList runat="server" ID="ddlCollegeGroup" TabIndex="4" ToolTip="Please Select College Group" >
            </asp:DropDownList>
                             
        </li>
        <li>
            <label>
                EmailId</label>
          <asp:TextBox ID="txtEmailId" runat="server" TabIndex="9" ToolTip="Please Enter EmailId"></asp:TextBox>
                             
        </li>
          <li>
            <label>
                Phone</label>
          <asp:TextBox ID="txtCollegeMobile" runat="server" TabIndex="9" ToolTip="Please Enter MobileNo"></asp:TextBox>
                             
        </li>  
        <li>
            <label>PinCode
           </label>
           <asp:TextBox ID="txtPinCode" runat="server" TabIndex="10" ToolTip="Please Enter PinCode"></asp:TextBox>
                                 
        </li>
        <li>
            <label>
              Fax</label>
             <asp:TextBox ID="txtCollegeFax" runat="server" TabIndex="11" ToolTip="Please Enter Fax"></asp:TextBox>
        </li>
        <li>
            <label>
           Address</label>
            <asp:TextBox ID="txtAddress" runat="server" TabIndex="12" ToolTip="Please Enter Adress" TextMode="MultiLine"></asp:TextBox>
           
               
        </li><li>  <input type="button" value="Save" id="btnSubmit" style="float:right"  onclick="ChekForm()"/>
                 
                                    <input type="button" value="Next" style="float:right" id="btnNext" style="float:right" /> 
                           </li>
                    <li><span id="progress" style="display: none">
                          <img src="../Images/CommonImages/loading.gif" />
                          Please Wait
                      </span>  </li>     
       </ul>
</fieldset> 
     
                    
<input type="hidden" ID="collegeBranchIdGenerated">
<input type="hidden" ID="hdnCollegeAssociation"><input type="hidden" ID="hdnCollegeGroupName">
 <link href="../../Styles/autoCompliteCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Autocomplete.js" type="text/javascript"></script>
    <script src="../../Scripts/CommonScript.js" type="text/javascript"></script>
<script src="../AdminPanel/JS/CollegeBranch.js" type="text/javascript"></script>
 <script type="text/javascript">
     function showContactForm() {
         var collegeGroup1 = "../../WebServices/CommonWebServices.asmx/GetCollegeGroup";
         if (validateFieldsInstitute()) {
             BindCollegeGroup($("#<%=ddlCollegeGroup.ClientID %>"), collegeGroup1);
            InsertCollegeGroup($("#<%=txtCollegeGroup.ClientID %>")); 
             $("#hdnCollegeGroupName").val($("#<%=txtCollegeGroup.ClientID %>").val());
            // InstituteName($("#<%=txtCollegeBranch.ClientID %>"));
             $("#basicInfo").hide(); $("#basicInfoContact").show(); 
         }
     }
     function validateFieldsInstitute() {
         if (collegeInstitute.val() <= 0) {
             collegeInstitute.focus();
             alert("Please Select Institute");

             return false;
         }
         else if (collegeAssociate.val() <= 0) {
             collegeAssociate.focus();
             alert("Please Select College Associate");

             return false;
         }  else if (collegeBranchName.val() == "") {
             alert("Please Enter College Branch");
             collegeBranchName.focus();
             return false;
         }
         else if (!numericNo.test(collegeEst.val())) {
             collegeEst.focus();
             alert('Please Enter College Establishment In Number'); return false;
         }
         else if (collegeDsec.val() == "") {
             alert("Please Enter College Description");
             collegeDsec.focus();
             return false;
         } else if (collegeCity.val() <= 0) {
             alert("Please Select City");
             collegeCity.focus();
             return false;
         } else if (collegePopularName.val() == "") {
             alert("Please Enter Popular Name");
             collegePopularName.focus();
             return false;
         } else if (collegeWebSite.val() == "") {
             alert("Please Enter Website");
             collegeWebSite.focus();
             return false;
         } else {return true;}
     }
     CollegeGroup($("#<%=txtCollegeGroup.ClientID %>"));
     $("#btnNext").hide();
     var instituteUrl = "../../WebServices/CommonWebServices.asmx/GetInstituteType";
  var collegeGroup = "../../WebServices/CommonWebServices.asmx/GetCollegeGroup";
      var cityUrl = "../../WebServices/CommonWebServices.asmx/GetAllCityWithoutId";
    BindDropDown($("#<%=ddlInstituteType.ClientID %>"),instituteUrl);
    BindDropDown($("#<%=ddlCollegeAssociate.ClientID %>"), collegeAssociate);
   
    BindDropDown($("#<%=ddlCollegeCity.ClientID %>"), cityUrl);
        var collegeInstitute = $("#<%=ddlInstituteType.ClientID %>");
        var collegeGroup = $("#<%=ddlCollegeGroup.ClientID %>");
        var collegeBranchName = $("#<%=txtCollegeBranch.ClientID %>");
        var collegePopularName = $("#<%=txtCollegePopularName.ClientID %>");
        var collegeWebSite = $("#<%=txtCollegeWebsite.ClientID %>");
        var collegeEmailId = $("#<%=txtEmailId.ClientID %>");
        var collegePincode = $("#<%=txtPinCode.ClientID %>");
        var collegeAddress = $("#<%=txtAddress.ClientID %>");
        var collegeEst = $("#<%=txtCollegeEst.ClientID %>");
        var collegeStatus = $("#<%=chkCollegeStatus.ClientID %>");
        var collegeDsec = $("#<%=txtCollegeDesc.ClientID %>");
        var collegeFax = $("#<%=txtCollegeFax.ClientID %>");
        var collegeCity = $("#<%=ddlCollegeCity.ClientID %>");
        var collegeMobileNo = $("#<%=txtCollegeMobile.ClientID %>");
        var hdnCollegeBranchIdGenerated = $("#collegeBranchIdGenerated");
        var collegeMgt = $("table.tbl input:radio");
        
        var chkCollegestatus = false;
       
        var reEmail = /^[a-z]+(([a-z_0-9]*)|([a-z_0-9]*\.[a-z_0-9]+))*@([a-z_0-9\-]+)((\.[a-z]{3})|((\.[a-z]{2})+)|(\.[a-z]{3}(\.[a-z]{2})+))$/;
        var mobileNo = /^[7-9][0-9]{9}$/;
        var numericNo =  /^\d*[0-9](|.\d*[0-9]|,\d*[0-9])?$/;
        var $radChecked;
       
        collegeMgt.click(function() {
            radChecked = $(':radio:checked');
            $("#hdnMgt").val(radChecked.val()); 
        });
       
      

        function ChekForm() {
            if (collegeStatus.is(":checked")) {
                chkCollegestatus = true;

            }
          
            if (validateFields()) {  
                insertCollegeDetails(hdnCollegeBranchIdGenerated, collegeInstitute, collegeGroup, collegeAssociate, collegeBranchName, $("#hdnMgt"), collegeEst, collegeDsec, collegePopularName, collegeCity, collegeWebSite, chkCollegestatus, collegeEmailId, collegeMobileNo, collegePincode, collegeFax, collegeAddress);
            
            }
        }

        function validateFields() {

          if (collegeEmailId.val() == "") {
                alert("Please Enter EmailId");
                collegeEmailId.focus();
                return false;
                
            }    else if (!reEmail.test(collegeEmailId.val())) {
                collegeEmailId.focus();
                alert('Please Enter Correct EmailId'); return false;
            }
            else if (collegeMobileNo.val() == "") {
                alert("Please Enter MobileNO");
                collegeMobileNo.focus();
                return false;
            } 
            else if ( collegeMobileNo.val().length < 10 || collegeMobileNo.val().length > 10 || !mobileNo.test(collegeMobileNo.val())) {
                collegeMobileNo.focus();
                alert('Please enter 10 digit MoNo strating with 7,8 or 9');
                return false;
            }
            else if (collegePincode.val() == "") {
                alert("Please Enter PinCode");
                collegePincode.focus();
                return false;
            } 
            else if (!numericNo.test(collegePincode.val().trim())) {
                collegePincode.focus();
                alert('Please Enter Numeric Number'); return false;
            }
            else if (collegeAddress.val() == "") {
                alert("Please Enter Address");
                collegeAddress.focus();
                return false;
            }
            else if (!numericNo.test(collegeFax.val().trim())) {
                collegeFax.focus();
                alert('Please Enter Fax in number '); return false;
            }
            else 
            {
                return true;
            } 
        }

       
    </script>


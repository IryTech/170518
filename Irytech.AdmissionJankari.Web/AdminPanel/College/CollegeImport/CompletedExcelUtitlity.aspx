<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="CompletedExcelUtitlity.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.CollegeImport.CompletedExcelUtitlity" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
 <script type="text/javascript" src="../../JS/jquery-1.5.2.min.js"></script>
 <script src="../../JS/general.js" type="text/javascript"></script>
<div class="centercontent">
 
    <div class="contentblock">
    <h3>Import Utility <span id="spanTableName"></span></h3>

      <a href="#" onclick="Back()" class="back">Go Back To Upload</a>
  <div >
      
       

          
 
        <h4>
            Step 3: Completed !</h4>
             
     <p><strong>Total Records : </strong><span id="spanTotalRecords"></span></p>
     <p><strong>Records Completed : </strong><span id="spanRecordsCompleted"></span></p>
     <p><strong>Failed Records : </strong><span id="spanFailed"></span></p>
        
    </div>

     <script type="text/javascript">
         var TotalRecords = getQueryStringValue('t');
         var Recordsfailed = getQueryStringValue('f');
         var RecordsCompleted = getQueryStringValue('s');
         document.getElementById("spanTotalRecords").innerHTML = TotalRecords;
         document.getElementById("spanRecordsCompleted").innerHTML = RecordsCompleted;
         document.getElementById("spanFailed").innerHTML = Recordsfailed;

         function Back() {
             if (tblName.length > 0) {
                 location.href = "/AdminPanel/College/CollegeImport/CollegeExcelImport.aspx";
             }
             else
             { location.href = "/AdminPanel/College/CollegeImport/CollegeExcelImport.aspx"; }
             return false;
         }
         </script>
         </div>
         </div>
</asp:Content>

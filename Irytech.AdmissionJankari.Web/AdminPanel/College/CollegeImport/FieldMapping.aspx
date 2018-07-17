<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="FieldMapping.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.CollegeImport.FieldMapping" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">

    <script src="../../JS/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script src="../../JS/general.js" type="text/javascript"></script>

 <div class="centercontent">

    <div class="contentblock">
    <h3>Import Utility <span id="spanTableName"></span></h3>
      <a href="#" onclick="Back()" class="back">Go Back To Upload</a>

  <div>
      
        <h4>
            Step 2: Confirm Field Mappings</h4>

          <fieldset>
             <p>
             
    <label>Select Table</label>  
    <select id="drpTable" onchange="tableSelectedIndexChanged()"></select>
        <script type="text/javascript">
            bindAllTableNamesNPrimaryColumnToDRP("#drpTable", "../../../WebServices/CommonWebServices.asmx/GetTableNamesNPrimaryField", getQueryStringValue('tblName'));
        </script>
         </p>
        </fieldset>
   
        
        <p style="font-size:11px; color:Gray;">
            The table below contains all of the fields in the module that can be mapped to the
            data in the import file.</p>
            <br />
            

            <p id="MappedMsg">
          
            </p>




        <script type="text/javascript">
            function tableSelectedIndexChanged() {
                 createGrid($("#drpTable option:selected").text());
            }

            function createGrid(tableName) {
                var value = getQueryStringValue('file');
                createMappingGrid(tableName, value);
            }
        </script>


        <input id="hfTotalCSVFileColumns" type="hidden" value="0" />
        <div id="MappingGrid">
        </div>
      
        <asp:HiddenField ID="hfDynamicQuery" runat="server" />
        
        <input type="button" value="Import" class="button" onclick="ImportMappedTable()" /> 
        <br />
         <div  id="loadingdiv" style="display:none; margin-left:5.3em">
          <img src="/image.axd?Common=LoadingImage.gif" alt="Loading" />Uploading data Please wait...
         </div>
    </div>
    </div>
    </div>
  
       <script type="text/javascript">

           var tblName = getQueryStringValue('tblName');
           document.getElementById("spanTableName").innerHTML = "for " + tblName;
           if (tblName.length == 0) {

               $("#drpTable option:contains(" + tblName + ")").attr('selected', 'selected');

           } else {


           }

           function Back() {
               if (tblName.length > 0) {
                   location.href = "index.aspx?tblN=" + tblName;
               }
               else
               { location.href = "index.aspx"; }
               return false;
           }
          
          

         </script>
      
</asp:Content>

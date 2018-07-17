<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcCourse.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcCourse" %>
<fieldset>
    <legend> Choose Course
 </legend>
<asp:UpdatePanel ID="updateCourse" runat="server" >
<ContentTemplate>
    <ul class="width90Percent">
        <li>
            <label>
               Course:</label>
            <asp:DropDownList ID="ddlCourse" runat="server"  ToolTip="Choose Course" AutoPostBack="true"
                onselectedindexchanged="DdlCourseSelectedIndexChanged"></asp:DropDownList><sup> &nbsp; &nbsp; &nbsp;i.e BE/BTECH, Select course for admission</sup>
             
            <asp:RequiredFieldValidator ID="rfvCourse" runat="server" InitialValue="Select" CssClass="error" ControlToValidate="ddlCourse"
                Display="Dynamic">
                   Select the course you are interested in

                </asp:RequiredFieldValidator>

          
        </li>
        <li  id="lblOtherCourse" visible="false" runat="server">
            <label>
        Please specify
            :</label>
                 <asp:TextBox ID="txtOtherCourse" runat="server" ></asp:TextBox><sup> &nbsp; &nbsp; &nbsp;If course is not in list then enter</sup>
            
            <asp:RequiredFieldValidator ID="rfvOtherCourse" Enabled="false" runat="server"  CssClass="error" ControlToValidate="txtOtherCourse"
                Display="Dynamic">
                  Enter the course you are interested in
                </asp:RequiredFieldValidator>
              <div class="clearBoth"></div>
                   <label>
       Your highest qualification:</label>
          <span class="forRadio">
                <asp:CheckBoxList ID="chkelgibilty"  runat="server" TextAlign="Right" Width="350px" RepeatDirection="Vertical" RepeatLayout="UnorderedList">
                <asp:ListItem Value="15" Text="Graduate"></asp:ListItem> 
                 <asp:ListItem Value="12"  Text="Senior Secondary(12th/Intermediate or equivalent )"></asp:ListItem>
                   <asp:ListItem Value="10" Text="Higher Secondary(10th or equivalent)"></asp:ListItem>
                </asp:CheckBoxList>
                
              </span>
                <sup> &nbsp; &nbsp; &nbsp;Select your highest qualification</sup>
                <asp:CustomValidator runat="server" ID="CheckBoxRequired" CssClass="error" Enabled="false" ClientValidationFunction="validateQalification"> Please select maximum qualification.</asp:CustomValidator>
                </li>
                
            
                </ul>
          
      </ContentTemplate></asp:UpdatePanel>
    
</fieldset>

  <script type="text/javascript" >
    
      $("#<%=ddlCourse.ClientID%>").change(function () {
          if ($("#<%=ddlCourse.ClientID%> option:selected").text() != "Other") {
              if ($("#<%=ddlCourse.ClientID%>").val() > 0) {
                  ChangeCourseId($("#<%=ddlCourse.ClientID%>").val());

                  location.href = ("/" + RemoveChahracterfromCorse($("#<%=ddlCourse.ClientID%> option:selected").text())).toLowerCase() + "/counselling/onlineapplicationform/";
              }
          }
      });
    
      function pageLoad(sender, args) {
          if (args.get_isPartialLoad()) {
              $("#<%=ddlCourse.ClientID%>").change(function () {
                  if ($("#<%=ddlCourse.ClientID%> option:selected").text() != "Other") {
                      if ($("#<%=ddlCourse.ClientID%>").val() > 0) {
                          ChangeCourseId($("#<%=ddlCourse.ClientID%>").val());

                          location.href = ("/" + RemoveChahracterfromCorse($("#<%=ddlCourse.ClientID%> option:selected").text())).toLowerCase() + "/counselling/onlineapplicationform/";
                      }
                  }
              });
    
              function validateQalification(source, arguments) {
                  arguments.IsValid = false;

                  var checklist = document.getElementById("<%=chkelgibilty.ClientID%>");
                  alert(checklist);
                  if (checklist == null) return;

                  var elements = checklist.getElementsByTagName("INPUT");
                  if (elements == null) return;

                  var checkBoxCount = 0;
                  for (i = 0; i < elements.length; i++) {
                      if (elements[i].checked) checkBoxCount++;
                  }
                  arguments.IsValid = (checkBoxCount > 0);
              }
          }
      }
      function validateQalification(source, arguments) {
          arguments.IsValid = false;

          var checklist = document.getElementById("<%=chkelgibilty.ClientID%>");
              if (checklist == null) return;

          var elements = checklist.getElementsByTagName("INPUT");
          if (elements == null) return;

          var checkBoxCount = 0;
          for (i = 0; i < elements.length; i++) {
              if (elements[i].checked) checkBoxCount++;
          }
          arguments.IsValid = (checkBoxCount > 0);
      }
    </script>
    
    
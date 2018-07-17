<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcIntermediateInfo.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcIntermediateInfo" %>
<fieldset>
    <legend>Senior Secondary (12th/Intermediate or equivalent) </legend>
    <div id="liIntermediateType" visible="false" runat="server"  class="clearBoth" style="padding-left:15px;">
   <strong> You are:</strong>
    <asp:RadioButtonList ID="rbtItermediateType" Width="400px" TextAlign="left"  RepeatDirection="Horizontal" 
                  AutoPostBack="true" runat="server" 
                  onselectedindexchanged="rbtItermediateType_SelectedIndexChanged">
                   <asp:ListItem Text="12th Appeared" Selected="True" Value="1"></asp:ListItem>
                  <asp:ListItem Text="12th Appearing"  Value="0"></asp:ListItem>
                 
              </asp:RadioButtonList>
              
    </div>
    <hr class="hrline" />
    <ul>
         <li>
            <label>
                School Name:</label>
            <asp:TextBox ID="txt12SchoolName" runat="server" ToolTip="12 School Name"></asp:TextBox><sup>i.e Ramjas school, Max 99 chars</sup>
             
            <asp:RequiredFieldValidator ID="requiredFieldValidator12SchoolBoard" runat="server"
                ControlToValidate="txt12SchoolName" CssClass="error" Display="Dynamic">
                Field School Name cannot be blank
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label>
                Board:</label>
            <asp:DropDownList ID="ddl12Board" runat="server" title="Select Board" Width="260px" AutoPostBack="False">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv12Board" runat="server" InitialValue="Select"
                ControlToValidate="ddl12Board" CssClass="error" Display="Dynamic">
             Select Board
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label>
                Year of Passing:</label>
            <asp:TextBox ID="txt12YrPass" runat="server" MaxLength="4"></asp:TextBox><sup>i.e 2010, Numeric</sup>
             
            <asp:RequiredFieldValidator ID="rfv12YrPass" CssClass="error" runat="server" ControlToValidate="txt12YrPass"
                Display="Dynamic">  Field Year of passing cannot be blank
              
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="rev12YerPass" CssClass="error" runat="server" ControlToValidate="txt12YrPass">
                Incorrect Field selection,please try again
            </asp:RegularExpressionValidator>
        </li>
        <li ID="liHide1" runat="server" >
            <label>
              Aggregate % or CGPA:
            </label>
            <asp:TextBox ID="txt12PreMarks"  runat="server" MaxLength="4"></asp:TextBox><sup>i.e 65.3, Numeric</sup>
              
           <asp:RequiredFieldValidator ID="rfvCGPA" Enabled="false" CssClass="error" runat="server" ControlToValidate="txt12PreMarks"
                Display="Dynamic">Field Aggregate % or CGPA cannot be blank
           </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="rev12PreMarks" runat="server" CssClass="error" ControlToValidate="txt12PreMarks">
                Incorrect Field selection eg(98.3),please try agian
           
            </asp:RegularExpressionValidator>
        </li>
        
        <li ID="liHide2" runat="server">
            <label>
             Specialization:
            </label>
            <asp:TextBox ID="txt12CourseCombination"  onmouseover="tooltip.pop(this, '#Coursescombination_tip')" runat="server" MaxLength="3"></asp:TextBox>
            <sup>i.e PCM (P-Physics,C-Chemistry,M-Maths),PCB(P-Physics,C-Chemistry,B-Biology) etc, Max 99 chars</sup>
             
        </li>
         <li ID="liHide3" runat="server">
            <label>
               Specialization %:
            </label>
            <asp:TextBox ID="txt12CourseCombinationPercentage" onmouseover="tooltip.pop(this, '#CoursescombinationPer_tip')" runat="server" MaxLength="3"></asp:TextBox>
            <sup>i.e enter percentage of PCM if you are passed out</sup>
           
            <asp:RegularExpressionValidator ID="rev12CourseCombinationPercentage" runat="server" CssClass="error" ControlToValidate="txt12CourseCombinationPercentage">
            Incorrect Field selection, please try again 
            </asp:RegularExpressionValidator>
        </li>
    </ul>
</fieldset>
  <div style="display:none;">
            <ul id="Coursescombination_tip">
                <li>Specialization :</li>
                <li>The Specalization Courses in your <strong> Higher Secondary(10+2)</strong> that could be:</li>
                <li><strong>PCM(Phyics,Chemistry,Math) write PCM </strong></li>
                <li><strong>PCB(Phyics,Chemistry,BIO)  write PCB </strong></li>
             </ul>
        </div>
      <div style="display:none;">
            <ul id="CoursescombinationPer_tip">
                <li> Specialization %:</li>
                <li>The Percenatge Specalization Courses in your <strong> Higher Secondary(10+2)</strong> that could be:</li>
                <li><strong>PCM(Phyics,Chemistry,Math)</strong>
                For example, if You scores 60 out of 75 in Phyics ,70 out of 75 in Chemistry and 70 out of 75 in Mathematics then you can
                 find the percentage of Your PCM marks by adding PCM Marks  60+70+70=200 and then add out of marks 75+75+75=225  divide the 200 by 225   which is .88 and then multiplying this figure by 100. This gives us the percentage of Your  marks in PCM, which is 88%. 
                (200/225) x 100 = 88%
                </li>
               
             </ul>
        </div>
       <%-- <script type="text/javascript">        
            var tempData = $("#<%=rbtItermediateType.ClientID %> input[type=radio]:checked'").val();
            if (tempData == 0) {
                $(".liHide").addClass("hide");
            } else {
                $(".liHide").removeClass("hide");
            }
            $("#<%=rbtItermediateType.ClientID %>").change(function () {
                var tempData3 = $("#<%=rbtItermediateType.ClientID %> input[type=radio]:checked'").val();
                alert(tempData3);
                if (tempData3 == 0) {
                    $(".liHide").addClass("hide");
                } else {
                    $(".liHide").removeClass("hide");
                }
            });

            function pageLoad(sender, args) {
                if (args.get_isPartialLoad()) {
                    var tempData2 = $("#<%=rbtItermediateType.ClientID %> input[type=radio]:checked'").val();
                    alert(tempData2);
                    if (tempData2== 0) {
                        $(".liHide").addClass("hide");
                    } else {
                        $(".liHide").removeClass("hide");
                    }
                    $("#<%=rbtItermediateType.ClientID %>").change(function () {
                        var tempData1 = $("#<%=rbtItermediateType.ClientID %> input[type=radio]:checked'").val();
                        if (tempData1 == 0) {
                            $(".liHide").addClass("hide");
                        } else {
                            $(".liHide").removeClass("hide");
                        }
                    });

                }
            }

        </script>--%>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcDOB.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcDOB" %>
<script src="../Scripts/jquery-1.5.2.min.js" type="text/javascript"></script>
<input id="hfYear" type="hidden" value="0" />
<input id="hfMonth" type="hidden" value="0" />
<input id="hfDay" type="hidden" value="0" />

<asp:DropDownList ID="ddlYear" runat="server" CssClass="width16Percent" onchange  = "PopulateDays()" />
<asp:DropDownList ID="ddlMonth" runat="server" CssClass="width16Percent" onchange  = "PopulateDays()" />
<asp:DropDownList ID="ddlDay" runat="server" CssClass="width16Percent" onchange="setHddnField()" />
<%--<sup>Candidate date of birth in YYYY/MM/DD format e.g. (1985/July/15)</sup>--%>
 <%--<img src="/image.axd?Common=QusetionMark.png" class="helpImage" title="Candidate Date of Birth in YYYY/MM/DD format e.g. (1985/July/15)" alt="Candidate date of birth in YYYY/MM/DD format e.g. (1985/July/15)" />--%>
 <span id="errMsg">

<asp:CustomValidator ID="Validator" runat="server" CssClass="error" ClientValidationFunction = "Validate" ValidationGroup="DropDownList"  >
  Invalid Fields selection, please try again

 </asp:CustomValidator>
 
 </span>
<script type="text/javascript">
    function setHddnField() {
        $("#hfDay").val($("#<%=ddlDay.ClientID %>").val());                
    }

    function PopulateDays() {        
        var ddlMonth = document.getElementById("<%=ddlMonth.ClientID%>");        
        var ddlYear = document.getElementById("<%=ddlYear.ClientID%>");
        var ddlDay = document.getElementById("<%=ddlDay.ClientID%>");
        $("#hfYear").val($("#<%=ddlYear.ClientID %>").val());
        $("#hfMonth").val($("#<%=ddlMonth.ClientID %>").val());
        
        var y = ddlYear.options[ddlYear.selectedIndex].value;
        var m = ddlMonth.options[ddlMonth.selectedIndex].value != 0;
        if (ddlMonth.options[ddlMonth.selectedIndex].value != 0 && ddlYear.options[ddlYear.selectedIndex].value != 0) {
            var dayCount = 32 - new Date(ddlYear.options[ddlYear.selectedIndex].value, ddlMonth.options[ddlMonth.selectedIndex].value - 1, 32).getDate();
            ddlDay.options.length = 0;
            AddOption(ddlDay, "DD", "0");
            for (var i = 1; i <= dayCount; i++) {
                AddOption(ddlDay, i, i);
            }
        }
    }

    function AddOption(ddl, text, value) {
        var opt = document.createElement("OPTION");
        opt.text = text;
        opt.value = value;
        ddl.options.add(opt);
    }

    function Validate(sender, args) {
        var ddlMonth = document.getElementById("<%=ddlMonth.ClientID%>");
        var ddlYear = document.getElementById("<%=ddlYear.ClientID%>");
        var ddlDay = document.getElementById("<%=ddlDay.ClientID%>");
        args.IsValid = (ddlDay.selectedIndex != 0 && ddlMonth.selectedIndex != 0 && ddlYear.selectedIndex != 0)
        
        if (args.IsValid == true) {
            ValidateDate(sender, args);
        }
    }
    function ValidateDate(sender, args) {
      
        var ddlMonth = $('#<%= ddlMonth.ClientID %>').val();
        var ddlYear = parseInt($('#<%= ddlYear.ClientID %>').val())+10;
        var ddlDay = $('#<%= ddlDay.ClientID %>').val();

        var selDate = ddlMonth + "/" + ddlDay + "/" + ddlYear;
       
        var d = new Date,
     dformat = [d.getMonth()+1 +"/"+ d.getDate()+"/"+
                 d.getFullYear()]

      
        args.IsValid = Date.parse(selDate) < Date.parse(dformat);

    }
    
    function clientValidate() {
        var isErrRegiss = false;
        $("#errMsg").text('');
        $("#hfYear").val($("#<%=ddlYear.ClientID %>").val());
        $("#hfMonth").val($("#<%=ddlMonth.ClientID %>").val());
        $("#hfDay").val($("#<%=ddlDay.ClientID %>").val());

        var Year = $("#hfYear").val();
        var Month = $("#hfMonth").val();
        var Day = $("#hfDay").val();
        
        if (Year == 0 || Month == 0 || Day == 0) {
            $("#errMsg").addClass("error").append("Invalid Fields selection, please try again");
            isErrRegiss = true;
        } else { $("#errMsg").text(''); isErrRegiss = false; }
        return isErrRegiss;
    
    }
</script>
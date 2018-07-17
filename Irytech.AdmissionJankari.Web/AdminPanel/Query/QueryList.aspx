<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="QueryList.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Query.QueryList" %>

<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
   
    <asp:UpdatePanel ID="UdtpnlCity" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <ul class="addPage_utility">
        <li class="fright">
           <a href="#" id="lnkDownload" onclick="OpenPoup('divRankSourceInsert','650px','lnkDownload');return false;" title="Click to download query" ><img alt="Downlaod" src="/AdminPanel/Images/CommonImages/downloadExcel2.png" /> </a>
        </li>
        </ul>   
           
           
            <fieldset>
                <legend>Query Reply & Search</legend>
                <ul class="options-bar">
                    <li style="height: 27px !important;">
                        <label>
                            Query Type:</label>
                        <asp:RadioButtonList ID="rbtSearchQuery" CssClass="RadioButtonList" AutoPostBack="true"
                            RepeatDirection="Horizontal" runat="server" OnSelectedIndexChanged="rbtSearchQuery_SelectedIndexChanged">
                            <asp:ListItem Text="College Query" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Exam Query" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Loan Query" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Common Query" Value="4"></asp:ListItem>
                        </asp:RadioButtonList>
                    </li>
                    <li>
                        <label>
                            &nbsp;
                        </label>
                        <asp:TextBox ID="txtFromdateSearch" Style="min-width: 100px !important;" placeholder="From Date"
                            runat="server"></asp:TextBox>
                        <Ajex:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromdateSearch"
                            PopupPosition="Right" Format="dd/MM/yyyy" PopupButtonID="txtFrpmdateSearch">
                        </Ajex:CalendarExtender>
                        <asp:TextBox ID="txtTodateSearch" Style="min-width: 100px !important;" placeholder="To Date"
                            runat="server"></asp:TextBox>
                        <Ajex:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTodateSearch"
                            PopupPosition="Right" Format="dd/MM/yyyy" PopupButtonID="txtTodateSearch">
                        </Ajex:CalendarExtender>
                       
                        <asp:DropDownList ID="ddlCourseListSearch" runat="server" ToolTip="Please Select Course"
                            Height="25px" AutoPostBack="True" OnSelectedIndexChanged="ddlCourseListSearch_SelectedIndexChanged">
                        </asp:DropDownList>
                         
                    </li>
                    <li>
                        <label>
                        </label>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="fleft" TabIndex="5"
                            OnClick="btnSearch_Click" />

                        
                      
                    </li>
                </ul>
            
                <asp:Repeater ID="rptQuery" runat="server" >
                  
                    <HeaderTemplate>
                        <table class="grdView">
                            <tr>
                                <th>
                                    For
                                </th>
                                <th>
                                    Course Name
                                </th>
                                <th>
                                    Query
                                </th>
                                <th>
                                    Query On
                                </th>
                                <th>
                                   Publish Query
                                </th>
                                <th>
                                    Replies
                                </th>
                                <th>
                                    Reply
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# Eval("SourceName")%>
                            </td>
                            <td>
                                <%# Eval("StudentCourseName")%>
                            </td>
                            <td>
                                <%# Eval("StudentQuery")%>
                            </td>
                             <td>
                                <%# Convert.ToDateTime(Eval("CreatedOn")).ToString("dd/MM/yyyy")%>
                            </td>
                            <td>
                                <a href="#" id="lnkModerateQuery<%# Eval("StudentQueryId")%>" title="Click to moderate the query"  onclick="OpenModerateQuery(<%# Eval("StudentQueryId")%>);return false;"> <%#GetModerateQueryClass(Eval("StudentQueryId"))%></a>
                           
                            </td>
                           
                            <td>
                            
                                <%#string.Format("{0}", Convert.ToBoolean(Eval("ReplyStatus")) == true ? "<a href='#'onclick='GetQueryReply(" + Eval("StudentQueryId") + ")' >"+  GetReplyCount(Convert.ToInt32(Eval("StudentQueryId")))+ "</a>" :"0")%>
                            </td>
                            <td>
                                <a href="#" id="lnkQueryReply" onclick='OpenQueryReply("<%# Eval("StudentQueryId")%>", "<%# Eval("StudentQuery")%>","<%# Eval("UserEmailId")%>"); return false;'>
                                    Reply Query</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table></FooterTemplate>
                </asp:Repeater>
                <AJ:CustomPaging ID="ucCustomPaging" runat="server" />
                </fieldset>
                <div id="fade">
                </div>
                <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                <div id="divImage" class="loading">
                    <img src="/image.axd?Common=Loading.gif" />
                </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
            <asp:PostBackTrigger ControlID="ddlCourseList" />
        </Triggers>
    </asp:UpdatePanel>
    <span id="lblReplyMsg"></span>
    <input type="hidden" id="queryId" />
    <input type="hidden" id="studentEmailId" />
    <div id="divCourseCategoryInsert" class="popup_block width43perc">
        <fieldset>
            <legend>Reply Query </legend>
            <div>
                <table id="userDetails" class="grdView" style="border: 0px solid !important;">
                    <tr>
                        <td style="width: 45px; vertical-align: text-top; border-bottom: 0px solid !important;
                            padding-top: 0px !important;">
                            Query :
                        </td>
                        <td style="border-bottom: 0px solid !important; padding-top: 0px !important;">
                            <textarea id="txtQuestion" readonly="readonly" style="max-width: 394px !important;
                                width: 394px !important;" rows="3" cols="9"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 45px; vertical-align: text-top; padding-top: 0px !important; border-bottom: 0px solid !important;">
                            Reply :
                        </td>
                        <td style="border-bottom: 0px solid !important; padding-top: 0px !important;">
                            <textarea id="txtReply" style="max-width: 394px !important; width: 394px !important;"
                                rows="3" cols="9"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 0px solid !important; padding-top: 0px !important;">
                        </td>
                        <td style="border-bottom: 0px solid !important; padding-top: 0px !important;">
                            <input type="button" value="Reply" onclick="ReplyUserQuery()" id="btnReply" />
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
    <div id="divModerateQuery" class="popup_block width43perc">
        <fieldset>
            <legend>Moderate Query</legend>
             <ul class="options-bar">
                <li>
                <input type="hidden" id="moderateQueryId" />
                <input type="radio" name="group1" value="true" /> Publish<br/>
                <input type="radio" name="group1" value="false"/> Restrict<br/>
                </li>
                <li>
                <input type="button" value="Moderate" onclick="ModerateQuery()" />
                 <input type="button" class="close" value="Cancel"   />
                
                </li>
            </ul>
        </fieldset>
    </div>
    <div id="divUserReply" class="popup_block width43perc">
    </div>
     <div id="divRankSourceInsert" class="popup_block width43perc">
         <fieldset>
        <legend>Query Download</legend>
        <!--PopUp -->
        <ul class="options-bar">
            <li>
                <label>
                    Select Query type:
                </label>
                <asp:RadioButtonList ID="rbtQueryDownloadType" CssClass="RadioButtonList" RepeatDirection="Horizontal"
                    RepeatLayout="Table" Width="650px" runat="server">
                    <asp:ListItem Text="College Query" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Exam Query" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Loan Query" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Common Query" Value="4"></asp:ListItem>
                </asp:RadioButtonList>
            </li>
            <li class="width40perc fleft">
                <label>
                    Course:</label>
                <asp:DropDownList ID="ddlCourseList" runat="server" ToolTip="Please Select Course"
                    Height="25px" AutoPostBack="True" OnSelectedIndexChanged="ddlCourseList_SelectedIndexChanged">
                </asp:DropDownList>
            </li>
            <li class="width22perc fleft">
                <label style="width: 66px !important;">
                    Form Date:
                </label>
                <asp:TextBox ID="txtFromdate" Style="min-width: 100px !important;" runat="server"></asp:TextBox>
                <Ajex:CalendarExtender ID="calfrmdate" runat="server" TargetControlID="txtFromdate"
                    PopupPosition="Right" Format="dd/MM/yyyy" PopupButtonID="txtFrpmdate">
                </Ajex:CalendarExtender>
            </li>
            <li class="width22perc fleft">
                <label style="width: 52px !important;">
                    To Date:
                </label>
                <asp:TextBox ID="txtTodate" Style="min-width: 100px !important;" runat="server"></asp:TextBox>
                <Ajex:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtTodate"
                    PopupPosition="Right" Format="dd/MM/yyyy" PopupButtonID="txtTodate">
                </Ajex:CalendarExtender>
            </li>
            <li>
                <label>
                </label>
                <asp:Button ID="btnUpload" runat="server" Text="Download" TabIndex="5" OnClick="btnUpload_Click" />
            </li>
        </ul>
    </fieldset>
    </div>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        // Add initializeRequest and endRequest
        prm.add_initializeRequest(prm_InitializeRequest);
        prm.add_endRequest(prm_EndRequest);

        // Called when async postback begins
        function prm_InitializeRequest(sender, args) {
            // get the divImage and set it to visible
            $("#fade").show();
            $("#divImage").show();
        }
        // Called when async postback ends
        function prm_EndRequest(sender, args) {
            $("#fade").hide();
            $("#divImage").hide();
        }
        function ReplyUserQuery() {
            if ($("#txtReply").val() != '') {
                var json = "{'queryId':" + $("#queryId").val() + ",'queryReply':'" + $("[id$='txtReply']").val() + "','queryUserEmail':'" + $("#studentEmailId").val() + "','userFullName':'" + $("#studentEmailId").val() + "','query':'" + $("#txtQuestion").val() + "'}";
                var msg = "";
                alert(json);
                $.ajax({
                    type: "POST",
                    url: "../../WebServices/CommonWebServices.asmx/ReplyUserQuery",
                    data: json,
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        msg = response.d;
                        $("#lblReplyMsg").text(msg);
                        $("#lblReplyMsg").addClass("success");
                        $("#lblReplyMsg").fadeOut(50000);
                        $("#txtQuestion").val('');
                        $("#txtReply").val('');
                        $("#queryId").val('')
                        $("#studentEmailId").val('')


                    },
                    error: function (xml, textStatus, errorThrown) {

                        alert(xml.status + "||" + xml.responseText);
                    }
                });
            } else {
                alert('Field reply can not be blank');
                $("#txtReply").focus();
            }

        }

        function OpenQueryReply(queryId, userQuery, studentEmail) {

            $("#txtQuestion").val(userQuery);
            $("#queryId").val(queryId);
            $("#studentEmailId").val(studentEmail);
            OpenPoup('divCourseCategoryInsert', '800px', 'lnkQueryReply')

        }
        function GetQueryReply(queryId) {


            var json = "{'queryId':" + queryId + "}";
            var msg = "";

            $.ajax({
                type: "POST",
                url: "../../WebServices/CommonWebServices.asmx/GetReply",
                data: json,
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    BindQueryReplyss(response);
                },
                error: function (xml, textStatus, errorThrown) {

                    alert(xml.status + "||" + xml.responseText);
                }
            });

        }

        function BindQueryReplyss(data) {
            $('#divUserReply').html('');

            var datas = "";
            $.each(data.d, function (i, client) {
              
                var replyMsg = client.ReplyStatus == true ? 'Published' : 'Restricted'
                datas = datas + "<fieldset><ul  class='" + GetModerateReplyClass(client.QueryReplyId) + "'  style='font-size:12px; margin:5px 0px; '><li><label style='font-weight:bold; color:#3089a3; width:50px !important;'>Reply: </label>" + client.QueryReply + "</li> <span style='display:block;  color:#333; text-align:right; '>- By " + client.ReplyUserName + "</span><span style='display:block;  color:#333; text-align:right; '>- Publish Reply : <a href='#'  onclick='UpdateReplyStatus(" + client.QueryReplyId + "," + client.ReplyStatus + " )' > " + replyMsg + "</a></span> </ul></fiedlset>"
            });

            $('#divUserReply').append(datas);
            OpenPoup('divUserReply', '800', 'lnkQueryReply')
        }
        function OpenModerateQuery(queryId) {
                   
            $("#moderateQueryId").val(queryId);
            OpenPoup('divModerateQuery', '800', 'lnkQueryReply')
        }
        function GetModerateReplyClass(replyId) {
            var json = "{'replyId':" + replyId + "}";
            var cssClass = "";
            $.ajax({
                type: "POST",
                url: "../../WebServices/CommonWebServices.asmx/CheckReplyModerate",
                data: json,
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d) {
                        cssClass = "err_msg";
                    }
                },
                error: function (xml, textStatus, errorThrown) {

                    alert(xml.status + "||" + xml.responseText);
                }
            });
            return cssClass;
        }

        function UpdateReplyStatus(replyId, replystatus) {
            var sttaus = false;
            if (replystatus) {
                sttaus = false;
            } else {
                sttaus = true
            }
            var json = "{'replyId':'" + replyId + "','replystatus':'" + sttaus + "'}";
            var msg = "";

            $.ajax({
                type: "POST",
                url: "../../WebServices/CommonWebServices.asmx/ModerateReply",
                data: json,
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
               
                   
                    $("#lblReplyMsg").text(response.d);
                    $("#lblReplyMsg").addClass("success");
                    $("#lblReplyMsg").fadeOut(20000);
                },
                error: function (xml, textStatus, errorThrown) {

                    alert(xml.status + "||" + xml.responseText);
                }
            });

        }


        function ModerateQuery() {
           queryId= $("#moderateQueryId").val();
           var id = $("#lnkModerateQuery" + queryId).text().trim();
           var value = $("input:radio[name='group1']:checked").val();

           var msg = value == 'true' ? "Publish" : "Restrict";
           var retVal = confirm("Being moderated... \n Are you sure, you want to " + msg + " this query?");
          if (retVal == true) {
                         
                var json = "{'queryId':" + queryId + ",'status':" + value + "}";
             
                $.ajax({
                    type: "POST",
                    url: "../../WebServices/CommonWebServices.asmx/ModerateStudentQuery",
                    data: json,
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        $("#lblReplyMsg").text(response.d);
                        $("#lblReplyMsg").addClass("success");
                        $("#lnkModerateQuery" + queryId).text(msg)
                        $('#fade , .popup_block').fadeOut(function () {

                            $('#fade, a.close').remove();
                        }); //fade them both out



                    },
                    error: function (xml, textStatus, errorThrown) {

                        alert(xml.status + "||" + xml.responseText);
                    }
                });
            }
            
        }
    </script>
</asp:Content>

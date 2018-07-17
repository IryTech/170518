<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcCollegeQuery.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcCollegeQuery" %>
<%@ Import Namespace="IryTech.AdmissionJankari.Components" %>
<script src="/Js/JQueryReply.js" type="text/javascript"></script>
<asp:HiddenField ID="hndCourseId" runat="server" />
<div class="box1" id="CollegeHeader" runat="server">
    <h3 class="streamCompareH3">
        Query Related to College
    </h3>
    <hr class="hrline" />
    <div class="boxPlane">
        <asp:Repeater ID="rptCollegeQuery" runat="server" OnItemDataBound="rptCollegeQuery_ItemDataBound">
            <ItemTemplate>
                <ol class="vertical marginbottom" style="border: 1px solid #e1e1e1; border-radius: 3px;
                    padding-bottom: 3px;">
                    <li class="width95Percent"><strong>Query: </strong><span><b style="font-size: 11px !important;">
                        <%# Eval("AjStudentQueryText")%></b></span> <span style="display: block; width: 100%;
                            text-align: right;"><i style="color: #647899 !important;">- By :
                                <%# Eval("AjUserFullName")%></i></span>
                        <asp:Literal Visible="false" ID="ltrQueryId" Text='<%# Eval("AjStudentQueryId")%>'
                            runat="server"></asp:Literal>
                        <asp:Repeater ID="rptCollegeQueryReply" runat="server">
                            <ItemTemplate>
                                <span>
                                    <%# !string.IsNullOrEmpty(Convert.ToString(Eval("AjQueryReply"))) ? "<strong>Answer: </strong>" + "<span style='font-size:12px;'>"+ Convert.ToString(Eval("AjQueryReply")) + "</span>" : ""%></span>
                                <span style="display: block; width: 100%; text-align: right;"><i style="color: gray !important;">
                                    - By :
                                    <%# Eval("AjUserFullName")%></i></span>
                            </ItemTemplate>
                        </asp:Repeater>
                        <a href="/College/QueryReply.aspx?QueryId=<%# Eval("AjStudentQueryId")%>&SourceId=<%#Eval("AjQuerySourceId") %>">
                            Reply Query </a></li>
                </ol>
            </ItemTemplate>
        </asp:Repeater>
        <span style="float: right;"><a id="lnkViewQuery" runat="server">View All </a></span>
    </div>
</div>
<div class="popup_block" id="divQueryReply">
    <div class="quickquery login" style="padding: 5px;">
        <span>
            <label id="lblReplyerrMsg" class="hide">
            </label>
        </span>
        <h2 style="font-size: 1.5em;" id="lblExam">
        </h2>
        <hr class="hrline" />
        <fieldset class="boxBody " id="fldRegister">
            <ul id="registertabReply">
                <li><strong class="liststrong">You are</strong>
                    <select id="ddlUserType" title="Select User Type">
                    </select>
                    <span id='userTypeError' class="error hide"></span></li>
                <li><strong class="liststrong">
                    <%=Resources.label.Name%></strong>
                    <input type="text" id="txtReplyUserName" title="Enter your name" onfocus="clear(this)"
                        placeholder="Enter your name" />
                    <span id='nameReplyError' class="error hide"></span></li>
                <li><strong class="liststrong">
                    <%=Resources.label.Email%></strong><input type="text" id="txtReplyEmailId" onfocus="clear(this)"
                        placeholder="Enter your email id" title="Enter your email id" />
                    <span id='EmailReplyError' class="error hide"></span></li>
                <li><strong class="liststrong">
                    <%=Resources.label.Mobile%></strong>
                    <input type="text" id="txtReplyMobileNumber" title="Enter your 10 digit mobile number"
                        onfocus="clear(this)" placeholder="Enter your 10 digit mobile number" />
                    <span id='MobileReplyNumberError' class="error hide"></span></li>
            </ul>
            <ul>
                <li><strong class="liststrong">Query</strong>
                    <textarea rows="3" cols="7" id="txtQuery" title="Student Query" readonly="readonly"
                        onfocus="clear(this)"></textarea>
                </li>
                <li><strong class="liststrong">Reply</strong>
                    <textarea rows="4" cols="5" id="txtReply" title="Enter Your Reply" onfocus="clear(this)"></textarea>
                    <span id='ReplyError' class="error hide"></span></li>
                <li>
                    <input type="checkbox" checked="checked" />
                    I agree <a href="/Terms-and-Conditions" target="_blank">T&amp;C</a> and <a href="/Privacy-Policy"
                        target="_blank">Privacy Policy</a> </li>
                <li>
                    <input type="button" title="Click to Reply" class="button" onclick="ReplyQuery()"
                        id="butreplySubmit" value="Reply" />
                    <input type="button" title="Clear Field " id="butClear" value="Clear" />
                </li>
            </ul>
        </fieldset>
        <span id="spnReplyMsg" class="hide">
            <label id="lbllReplyErMsg" title="Message" class="msg" style="text-align: center !important;">
                You must fill in all of the mandatory fields
            </label>
        </span>
    </div>
</div>

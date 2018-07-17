<%@ Page Title="" Language="C#" MasterPageFile="~/themes/Blue/Site.Master" AutoEventWireup="true"
    CodeBehind="QueryReply.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.College.QueryReply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div runat="server" id="divResultModerate" visible="False">
    </div>
    <div class="boxPlane" style="margin-top: 10px;">
        <div class="clearBoth">
        </div>
        <ul>
            <li class="width95Percent"><strong style="font-size: 14px;">Query: </strong><span><b
                style="font-size: 11px !important;">
                <asp:Label ID="lblQuery" runat="server"></asp:Label>
            </b></span><span style="display: block; width: 100%; text-align: right;"><i style="color: #647899 !important;">
                <asp:Label ID="lblQueryBy" runat="server" Style="color:darkorange"></asp:Label>
            </i></span></li>
            <li>
                <asp:Repeater ID="rptCollegeQueryReply" runat="server">
                    <ItemTemplate>
                        <span>
                            <%# !string.IsNullOrEmpty(Convert.ToString(Eval("AjQueryReply"))) ? "<strong>Answer: </strong>" + "<span style='font-size:12px;'>"+ Convert.ToString(Eval("AjQueryReply")) + "</span>" : ""%></span>
                        <span style="display: block; width: 100%; text-align: right;"><i style="color: gray !important;">
                            - By :
                            <%# Eval("AjUserFullName")%></i> </span>
                    </ItemTemplate>
                </asp:Repeater>
            </li>
            <li>
                <label>
                </label>
                <asp:TextBox ID="txtQueryReply" CssClass="QueryReply" TextMode="MultiLine" Rows="5"
                    Columns="2" runat="server" placeholder="Enter your answer related to this query"
                    Width="720px"></asp:TextBox>
                <span class="charLeft" id="countCharacter">450 characters left</span> </li>
            <li>
                <label>
                </label>
                <asp:Button ID="btnReply" runat="server" CssClass="button" Text="Reply" OnClick="btnReply_Click" />
            </li>
        </ul>
    </div>
    <div style="margin-top: 15px; display: block;">
        <asp:LinkButton ID="lnkViewMore" CssClass="ViewMore" runat="server" OnClick="lnkViewMore_Click">Query related To this college &raquo;</asp:LinkButton>
    </div>
    
      <div runat="server" id="divNoResult" visible="False"  style="margin-top: 10px;">
    <div class="box1" id="CollegeHeader" runat="server" visible="False" style="margin-top: 15px;">
        <h3 class="streamCompareH3">
            Query Related to College
        </h3>
        <hr class="hrline" />
      
        </div>
        <div class="boxPlane">
            <asp:Repeater ID="rptCollegeQuery" runat="server" OnItemDataBound="rptCollegeQuery_ItemDataBound">
                <ItemTemplate>
                    <ol class="vertical marginbottom" style="border: 1px solid #e1e1e1; border-radius: 3px;
                        padding-bottom: 3px;">
                        <li class="width95Percent"><strong>Query: </strong><span><b style="font-size: 11px !important;">
                            <%# Eval("AjStudentQueryText")%></b></span> <span style="display: block; width: 100%;
                                text-align: right;"><i style="color: #647899 !important;">  <%# Eval("AjUserFullName")%> | <%#Convert.ToDateTime(Eval("CreatedOn")).ToString("MMM-dd-yyyy")%>
                                   </i></span>
                            <asp:Literal Visible="false" ID="ltrQueryId" Text='<%# Eval("AjStudentQueryId")%>'
                                runat="server"></asp:Literal>
                            <asp:Repeater ID="rptCollegeQueryReply" runat="server">
                                <ItemTemplate>
                                    <span>
                                        <%# !string.IsNullOrEmpty(Convert.ToString(Eval("AjQueryReply"))) ? "<strong>Answer: </strong>" + "<span style='font-size:12px;'>"+ Convert.ToString(Eval("AjQueryReply")) + "</span>" : ""%></span>
                                    <span style="display: block; width: 100%; text-align: right;">
                                        <i style="color: gray !important;">
                                        - By :
                                        <%# Eval("AjUserFullName")%></i>
                                    </span>
                                   
                                </ItemTemplate>
                            </asp:Repeater>
                            <a href="/College/QueryReply.aspx?QueryId=<%# Eval("AjStudentQueryId")%>&SourceId=<%#Eval("AjQuerySourceId") %>">
                                Reply Query </a>
                          </li>
                    </ol>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <script type="text/javascript">
        function CountLeft(field, max) {
            if (field.val().length > max)
                field.val(field.val().substring(0, max));
            else
                jQuery(".charLeft").html((max - field.val().length) + " characters left");
        }

        jQuery("#<%=txtQueryReply.ClientID %>").keyup(
        function (event) {
            CountLeft(jQuery(this), 450); // you can increase or derease the number depend on your need.
        }
    );

        function HideMessageStatus() {
            $("[id$='divNoResult']").fadeOut('slow', function () {
            });
            $("[id$='divResultModerate']").fadeOut('slow', function () {
            });
        }
    </script>
    <style>
        .resultSuccess
        {
            padding: 15px;
            margin-bottom: 20px;
            border: 1px solid transparent;
            border-radius: 7px;
            color: #005e00 !important;
            background-color: #dbf7d8 !important;
            border-color: rgb(235, 204, 209);
            margin-top: 15px;
        }
        
        .resultSuccess a
        {
            color: rgb(174, 19, 16);
        }
        .resultError
        {
            padding: 15px;
            margin-bottom: 20px;
            border: 1px solid transparent;
            border-radius: 7px;
            color: #bf0000 !important;
            background-color: #fef9c5 !important;
            border: 1px solid #a89d18;
            margin-top: 15px;
        }
        
        .resultError a
        {
            color: rgb(174, 19, 16);
        }
        a.ViewMore
        {
            display: inline-block;
            color: #FFFFFF;
            background-color: #186f8f;
            font-weight: bold;
            font-size: 12px;
            text-align: center;
            padding-left: 10px;
            padding-right: 10px;
            padding-top: 3px;
            padding-bottom: 4px;
            text-decoration: none;
            margin-left: 5px;
            margin-top: 0px;
            margin-bottom: 5px;
            border: 1px solid #aaaaaa;
            border-radius: 5px;
            white-space: nowrap;
        }
        a.ViewMore:hover
        {
            background-color: #ffffff;
            color: #647899;
        }
    </style>
</asp:Content>

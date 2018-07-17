<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Home.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div id="divHome" >
        <asp:DataList ID="ddlUserMenu" runat="server" RepeatColumns="3"   DataKeyField="AjMenuId" 
            onitemdatabound="ddlUserMenu_ItemDataBound">
            <ItemTemplate>
                <div class=" accordion">
                    <h3 class="accord">
                        <center>
                            <i></i>
                        </center>
                        <%# Eval("AjMenuName")%></h3>
                    <asp:Repeater ID="rptuserSubMenu" runat="server">
                    <HeaderTemplate> <ul>
                    </HeaderTemplate>
                        <ItemTemplate>
                           
                                <li><a href="<%# Convert.ToString(Eval("AjMenuUrl")).Replace("~","").Trim() %>"><%# Eval("AjMenuName")%></a>
                                </li>
                                                                
                         
                        </ItemTemplate>
                        <FooterTemplate>
                         </ul >
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
   
    <script type="text/javascript">

        $(".accordion ul").hide();
        $(".accordion h3").click(function () {
            $(".accordion ul").hide();
             $(this).next("ul").removeClass("hide");
                $(this).next("ul").slideToggle("fast")
		.siblings("ul:visible").slideUp("slow");
                $(this).toggleClass("active");
                $(this).siblings("h3").removeClass("active");
            });

      
    </script>
</asp:Content>

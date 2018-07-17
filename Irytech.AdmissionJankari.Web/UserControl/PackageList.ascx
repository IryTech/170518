<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PackageList.ascx.cs" Inherits="Irytech.AdmissionJankari.Web.UserControl.PackageList" %>



<div id="divPackageListss">
    <asp:Repeater ID="rptFactor" runat="server" OnItemDataBound="rptFactor_ItemDataBound" OnItemCommand="rptFactor_ItemCommand">
        <HeaderTemplate>

            <table class="grdView">
                <tr>
                    <th></th>
                    <th>
                        <asp:Repeater ID="rptPackageName" runat="server">
                            <ItemTemplate>
                                <span style="margin: 50px;">
                                    <%# Eval("PackageName")%>
                                  
                                </span>

                            </ItemTemplate>


                        </asp:Repeater>
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# Eval("FactorName")%>
                    <asp:HiddenField ID="hndFactorId" runat="server" Value='<%# Eval("FactorId")%>' />

                </td>
                <td id="tdExists" runat="server"></td>

            </tr>


        </ItemTemplate>
        <FooterTemplate>
            <tr>
                <td></td>
                <td>
                    <asp:Repeater ID="rptPackagePrice" runat="server" OnItemCommand="rptFactor_ItemCommand">
                       
                        <ItemTemplate>
                           
                               
                                    <span style="margin: 50px;">
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Convert.ToString(Eval("PackageAmount")).Equals("0")?"Free":Convert.ToString(Eval("PackageAmount"))%>'></asp:Label>


                                    </span>
                            <br />
                            <br />
                              
                            <span>
                                   <asp:LinkButton ID="lnkSelect" CommandName="Select" Text="Select" CommandArgument='<%#Eval("PackageId")%>' runat="server"></asp:LinkButton>

                            </span>


                        </ItemTemplate>
                       

                    </asp:Repeater>

                </td>

            </tr>



            </table>
        </FooterTemplate>
    </asp:Repeater>
    
   
</div>

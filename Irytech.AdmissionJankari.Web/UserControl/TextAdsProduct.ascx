<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TextAdsProduct.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.TextAdsProduct" %>
<%@ Import Namespace="IryTech.AdmissionJankari.BL" %>

<div id="diVtextAds" class="pricing fright" Visible="False" runat="server" style="margin-top: 25px!important;border-radius:2px; padding:2px; border:1px solid #005eb1; box-shadow:0px 2px 4px 2px">
           <h2>Text Ads Products</h2>
             <ul>
               <asp:Repeater ID="rptTextProduct" runat="server" OnItemDataBound="rptTextProduct_ItemDataBound">
                <ItemTemplate>
                    <li>
                        <div class="plan">
                            <h3>
                                <%# Eval("AjCollegeAssociationCategoryName")%></h3>
                            <span class="price">
                                <%# !string.IsNullOrEmpty(Convert.ToString(Eval("AjProductDiscount"))) ? (Common.ConvertRupee(Convert.ToString(Convert.ToInt32(Eval("AjAmount")) - (Convert.ToInt32(Eval("AjAmount")) * Convert.ToInt32(Eval("AjProductDiscount")) / 100)))) : Common.ConvertRupee(Convert.ToString(Eval("AjAmount")))%>
                                <span>/month</span> </span>
                            <div class="sale-pricing">
                                <div class="sale-pricing">
                                    <%# !string.IsNullOrEmpty(Convert.ToString(Eval("AjProductDiscount"))) ? "<span class='reg-price'><strike>" + Common.ConvertRupee(Convert.ToString(Eval("AjAmount"))) + " </strike><span>Save " + Eval("AjProductDiscount") + " %</span></span>" : ""%>
                                </div>
                            </div>
                            <a  href="javascript:void(0)" data-cart="<%# Eval("AjAdvertismentDiscountId")%>|1|1"
                                class="flt-btn flt-btn-orng textadsanchor">Add to Cart</a>
                                 <asp:HiddenField ID="hdnAdvertisementTYpeId" Value='<%# Eval("AjCollegeAssociationCategoryId")%>'
                                        runat="server"></asp:HiddenField>
                            <asp:Repeater ID="rptProductDiscount" runat="server">
                                <ItemTemplate>
                                    <div class="plan-droplist-select">
                                        <%# Eval("Text").ToString() %>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="details">
                            <ul>
                                <li><strong>Description-:</strong>
                                    <p class="g-toolTip">
                                        <%# Convert.ToString(Eval("AjDescription")).Substring(0, 12)%>... <span>
                                            <%# Convert.ToString(Eval("AjDescription"))%>.</span>
                                    </p>
                                </li>
                            </ul>
                        </div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
            </ul>
        </div>
        <script type="text/javascript">
            $("a.textadsanchor").click(function() {
                var t = $(this).attr("data-cart");
                GetCollegeCourseForThisProduct(t.split('|')[0], 3);
            });
        </script>
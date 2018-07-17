<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannerProduct.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.BannerProduct" %>

<%@ Import Namespace="IryTech.AdmissionJankari.BL" %>

<div id="divBannerAds" class="pricing fright" Visible="False" runat="server" style="border-radius:2px; padding:2px; border:1px solid #005eb1; box-shadow:0px 2px 2px 2px">
           <h2>Banner Products</h2>
             <ul>
               <asp:Repeater ID="rptBannerProduct" runat="server" OnItemDataBound="rptBannerProduct_ItemDataBound">
                <ItemTemplate>
                    <li>
                        <div class="plan">
                            <h3>
                                <%# Eval("AjBannerPosition")%></h3>
                            <span class="price">
                                <%# !string.IsNullOrEmpty(Convert.ToString(Eval("AjProductDiscount"))) ? (Common.ConvertRupee(Convert.ToString(Convert.ToInt32(Eval("AjBannerPostAmount")) - (Convert.ToInt32(Eval("AjBannerPostAmount")) * Convert.ToInt32(Eval("AjProductDiscount")) / 100)))) : Common.ConvertRupee(Convert.ToString(Eval("AjBannerPostAmount")))%>
                                <span>/month</span> </span>
                            <div class="sale-pricing">
                                <div class="sale-pricing">
                                    <%# !string.IsNullOrEmpty(Convert.ToString(Eval("AjProductDiscount"))) ? "<span class='reg-price'><strike>" + Common.ConvertRupee(Convert.ToString(Eval("AjBannerPostAmount"))) + " </strike><span>Save " + Eval("AjProductDiscount") + " %</span></span>" : ""%>
                                </div>
                            </div>
                            <a  href="javascript:void(0)" data-cart="<%# Eval("AjAdvertismentDiscountId")%>|1|1"
                                class="flt-btn flt-btn-orng bannerAnchor">Add to Cart</a>
                                 <asp:HiddenField ID="hdnAdvertisementTYpeId" Value='<%# Eval("AjBannerPositionId")%>'
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
                                        <%# Convert.ToString(Eval("AjBannerPostDescription")).Substring(0, 20)%>... <span>
                                            <%# Convert.ToString(Eval("AjBannerPostDescription"))%>.</span>
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
            $("a.bannerAnchor").click(function () {
                var t = $(this).attr("data-cart");
                GetCollegeCourseForThisProduct(t.split('|')[0], 2);
            });
        </script>
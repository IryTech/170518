using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class BannerProduct : System.Web.UI.UserControl
    {
        private readonly Common _objCommon = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public void BindBannerProduct(int collegeId)
        {

            try
            {
                var objBannerData = _objCommon.GetBannerAdsProduct(collegeId);
                if (objBannerData.Tables[0].Rows.Count > 0)
                {
                    divBannerAds.Visible = true;
                    rptBannerProduct.Visible = true;
                    rptBannerProduct.DataSource = objBannerData.Tables[0];
                    rptBannerProduct.DataBind();
                }
                else
                {
                    divBannerAds.Visible = false;
                    rptBannerProduct.Visible = false;

                }
            }
            catch
                (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo =
                    "Error in Executing  BindProduct in UserControl/BannerProduct.ascx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        protected void rptBannerProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    var objCommon = new Common();

                    var hdnAdvertisementTYpeId = (HiddenField)(e.Item.FindControl("hdnAdvertisementTYpeId"));
                    var rptProductDiscount = (Repeater)e.Item.FindControl("rptProductDiscount");
                   
                    if (rptProductDiscount != null)
                    {

                        var dt =
                            objCommon.GetBannerProductDiscount(
                                Convert.ToInt32(hdnAdvertisementTYpeId.Value));
                        if (dt.Tables.Count > 0)
                        {
                            if (dt.Tables[0].Rows.Count > 0)
                            {
                                var liItem = new StringBuilder();
                                var defaultSelected =
                                     dt.Tables[0].AsEnumerable().Where(result => result.Field<bool>("AjDefaultSelection")).CopyToDataTable();
                                liItem.Append("<div class='plan-droplist-selected'><span>" +
                                              defaultSelected.Rows[0]["AjMonthValue"] + "</span> <span class='sale'>" +
                                              Common.ConvertRupee(Convert.ToString(defaultSelected.Rows[0]["AjProductPayAmount"])) +
                                              "</span> <span class='sale lastChild'>On Sale</span></div> <div class='plan-droplist-selectbtn'> <span></span></div><ul> ");
                                var rowCount = 0;
                                for (rowCount = 0; rowCount <= dt.Tables[0].Rows.Count - 1; rowCount++)
                                {
                                    var productAmount =
                                        Common.ConvertRupee(
                                            Convert.ToString(dt.Tables[0].Rows[rowCount]["AjBannerPostAmount"]));
                                    var productPayAmount =
                                        Common.ConvertRupee(
                                            Convert.ToString(dt.Tables[0].Rows[rowCount]["AjProductPayAmount"]));

                                    if (string.IsNullOrEmpty(
                                            Convert.ToString(dt.Tables[0].Rows[rowCount]["AjProductDiscount"])))
                                    {

                                        liItem.Append("<li class='" +
                                                      (Convert.ToBoolean(
                                                          dt.Tables[0].Rows[rowCount]["AjDefaultSelection"]) == true
                                                           ? "selected"
                                                           : "") + "' data-main='" +
                                                      dt.Tables[0].Rows[rowCount]["AjAdvertismentDiscountId"] +
                                                      "|1|1' data-view='" + productAmount +
                                                      "||0'><div data-main='true'><span>" +
                                                      dt.Tables[0].Rows[rowCount]["AjMonthValue"] + "</span><span>Rs " +
                                                      productAmount +
                                                      "/mo</span></div></li>");
                                    }
                                    else
                                    {
                                        liItem.Append("<li data-main='" +
                                                      dt.Tables[0].Rows[rowCount]["AjAdvertismentDiscountId"] +
                                                      "|1|1' class='" +
                                                      (Convert.ToBoolean(
                                                          dt.Tables[0].Rows[rowCount]["AjDefaultSelection"]) == true
                                                           ? "selected"
                                                           : "") + "' data-view='" +
                                                      productPayAmount +
                                                      "|&lt;strike&gt;" +
                                                      productAmount +
                                                      "&lt;/strike&gt;|" + dt.Tables[0].Rows[rowCount]["AjProductDiscount"] + "'><div data-main='true'><span> " +
                                                      dt.Tables[0].Rows[rowCount]["AjMonthValue"] +
                                                      "</span><span class='sale'>" +
                                                      productPayAmount +
                                                      "/mo</span><span class='sale lastChild'>On Sale</span></div><div><span>&nbsp;</span><span><strike>Rs. " +
                                                      productAmount +
                                                      "</strike>/mo</span><span class='sale lastChild'>Save " +
                                                      dt.Tables[0].Rows[rowCount]["AjProductDiscount"] +
                                                      "%</span></div></li>");

                                    }

                                }
                                if (rowCount == dt.Tables[0].Rows.Count)
                                {
                                    liItem.Append("</ul>");

                                }
                                var lst = new List<AddProduct.TestObject> { new AddProduct.TestObject() { Text = liItem.ToString() } };
                                rptProductDiscount.Visible = true;
                                rptProductDiscount.DataSource = lst;
                                rptProductDiscount.DataBind();

                            }
                            else
                            {
                                rptProductDiscount.Visible = false;

                            }
                        }
                        else
                        {
                            rptProductDiscount.Visible = false;

                        }
                    }
                }
            }
            catch
                (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo =
                    "Error in Executing  rptBannerProduct_ItemDataBound in UserControl/BannerProduct.ascx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
    }
}
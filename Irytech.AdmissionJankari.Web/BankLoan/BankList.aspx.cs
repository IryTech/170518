using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.Components.Web.Controls;
using IryTech.AdmissionJankari.BL;
using System.Data;
using System.Web.UI.HtmlControls;
namespace IryTech.AdmissionJankari20.Web.BankLoan
{
    public partial class BankList : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Pager.PageSize = ApplicationSettings.Instance.NewsArticlePageSize;
            Pager.ButtonsCount = ApplicationSettings.Instance.NewsArticlePageCount;
     
            Pager.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindBankList();
            BindPageTitleAndKeyWords();
            hdnSearchBankName.Value = "";
        }
        private void BindPageTitleAndKeyWords()
        {
            var objPage = new Common().GetPageTitleKeyWordAndDecsription("BankList");

            try
            {
                if (objPage.Rows.Count > 0)
                {

                    Page.Title = "";
                    Page.Title = Convert.ToString(objPage.Rows[0]["AjPageTitle"].ToString());

                    var metadesc = new HtmlMeta();
                    metadesc.Attributes.Clear();
                    metadesc.Name = "description";

                    metadesc.Content = Convert.ToString(objPage.Rows[0]["AjPageDescription"].ToString());

                    Page.Header.Controls.Add(metadesc);

                    var metaKeywords = new HtmlMeta
                                           {
                                               Name = "keywords",
                                               Content =
                                                   Convert.ToString(objPage.Rows[0]["AjPageKeyword"].ToString())
                                           };

                    Page.Header.Controls.Add(metaKeywords);
                }

            }
            catch (Exception Ex)
            {
                string err = Ex.Message;
                if (Ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + Ex.InnerException.Message;
                }
                string addInfo = "Error While fetching BindPageTitleAndKeyWords in CollegeSearch.aspx :: -> ";
                ClsExceptionPublisher objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        //---Edited by Saurabh-----
        private void BindBankList(bool parameter=true)
        {
            Common _objCommon = new Common();
            //var bankData = BankProvider.Instance.GetAllBankList();
            var bankData = BankProvider.Instance.GetBankListByName(hdnSearchBankName.Value.Trim().ToString());
            if (bankData.Count > 0)
            {
                try
                {
                    rptBankList.Visible = true;

                    Pager.BindDataWithPaging(rptBankList, Common.ConvertToDataTable(bankData), parameter);

                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.ToString();
                    }
                    const string addInfo = "Error while executing CourseCategory.aspx in College.cs  :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
           
        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            //var objCommon = new Common();
            //var bankData = BankProvider.Instance.GetAllBankList();
            //if (bankData.Count > 0)
            //{
            //    try
            //    {
            //        rptBankList.Visible = true;

            //        Pager.BindDataWithPaging(rptBankList, Common.ConvertToDataTable(bankData));
            //        BindPageTitleAndKeyWords();

            //    }
            //    catch (Exception ex)
            //    {
            //        var err = ex.Message;
            //        if (ex.InnerException != null)
            //        {
            //            err = err + " :: Inner Exception :- " + ex.ToString();
            //        }
            //        const string addInfo = "Error while executing CourseCategory.aspx in College.cs  :: -> ";
            //        var objPub = new ClsExceptionPublisher();
            //        objPub.Publish(err, addInfo);
            //    }
            //}
            BindBankList(false);

        }
       protected void btnSearch_Click(object sender, EventArgs e)
        {
            hdnSearchBankName.Value = txtBankName.Text.Trim().ToString();
            //var bankData = BankProvider.Instance.GetBankListByName(txtBankName.Text.Trim());
            //if (bankData.Count > 0)
            //{
            //    //Edited By Saurabh from here----
            //    //Pager.Visible=false;
            //    //rptBankList.DataSource = bankData;
            //    //rptBankList.DataBind();
            //    Pager.Visible = true;
            //    Pager.BindDataWithPaging(rptBankList, Common.ConvertToDataTable(bankData));
            //    //Editing end here---
            //}
            BindBankList();

        }
        protected void rptBankList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            bool indiaCorrect = false;
            bool abroadCorrect = false;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var imgIndiaCorrecrt = (Image)(e.Item.FindControl("imgIndiaCorrect"));
                var imgIndiaWrong = (Image)(e.Item.FindControl("imgIndiaWrong"));
                var imgAbroadCorrecrt = (Image)(e.Item.FindControl("imgAbroadCorrect"));
                var imgAbroadWrong = (Image)(e.Item.FindControl("imgAbroadWrong"));
                var hdnBankId = (HiddenField)e.Item.FindControl("hdnBankId");
                var bankData = BankProvider.Instance.GetLoanListByBankId(Convert.ToInt16(hdnBankId.Value));
                if (bankData.Count > 0)
                {
                    DataTable dt = Common.ConvertToDataTable(bankData);
                    for (var i = 0; i < dt.Rows.Count; i++) {
                        if (dt.Rows[i]["StudyPlaceId"].ToString() == "1")
                        {
                            indiaCorrect = true;
                           
                        }
                        if (dt.Rows[i]["StudyPlaceId"].ToString() == "2")
                        {
                            abroadCorrect = true;
                            
                        }
                        
                    }
                    if (indiaCorrect) { imgIndiaCorrecrt.Visible = true; } else { imgIndiaWrong.Visible = true; }
                    if (abroadCorrect) { imgAbroadCorrecrt.Visible = true; } else { imgAbroadWrong.Visible = true; }
                   
                }
            }
        }
    }
}
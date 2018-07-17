using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.Components.Web.Controls;
using IryTech.AdmissionJankari.BL;
using System.Data;
using System.Web.UI.HtmlControls;
namespace IryTech.AdmissionJankari20.Web.BankLoan
{
    public partial class BankLoanDetails : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["BankId"] != null)
            {
                UcComment.CommentType = Convert.ToString(CommentType.Loan);
                UcComment.CommentTypeId = Request.QueryString["BankId"];
                GetUserComment();
            }
            if (Request.QueryString["BankId"] != null) {
                BindBankDetails(Request.QueryString["BankId"]); 
            }
        }
        private void GetUserComment()
        {
            
            var dataset = new Common().GetUserComment(UcComment.CommentType, UcComment.CommentTypeId);
          
            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    var rowResults = from result in dataset.Tables[0].AsEnumerable()
                                     where result.Field<bool>("AjCommentStatus") == true
                                     select new
                                     {
                                         AjUserFullName = result.Field<string>("AjUserFullName"),
                                         AjUserImage = result.Field<string>("AjUserImage"),
                                         Comment = result.Field<string>("Comment"),
                                         CreatedOn = result.Field<DateTime>("CreatedOn"),

                                     };

                    if (rowResults.Count() > 0)
                    {
                        var control = UcComment.FindControl("rptComment");
                        var divUserComment = UcComment.FindControl("divUserComment") as HtmlGenericControl;

                        var repeater = control as Repeater;
                        if (repeater != null)
                        {
                            var lblCount = UcComment.FindControl("lblCount") as Label;
                            if (divUserComment != null) divUserComment.Visible = true;
                            repeater.DataSource = rowResults.ToList();
                            repeater.DataBind();
                            if (lblCount != null)
                                lblCount.Text = !string.IsNullOrEmpty(rowResults.Count().ToString()) ? Convert.ToString(rowResults.Count()) : "0";
                            
                        }
                    }
                }

            }

        }
        private void BindPageTitleAndKeyWords(ICollection<BankDetailsProperty> objBankList)
        {
           

            try
            {
                if (objBankList.Count > 0)
                {

                    Page.Title = "";
                    Page.Title = objBankList.First().BankShortName + " Education Loan " + objBankList.First().BankName + "Students Loan-Admission Jankari";

                    var metadesc = new HtmlMeta();
                    metadesc.Attributes.Clear();
                    metadesc.Name = "description";

                    metadesc.Content = objBankList.First().BankName +
                                       " offers education loan to students. Apply online for" +
                                       objBankList.First().BankShortName +
                                       " education loan to study in India and abroad. Get 100% loan imbursed, at lowest interest rate.";

                    Page.Header.Controls.Add(metadesc);

                    var metaKeywords = new HtmlMeta
                                           {
                                               Name = "keywords",
                                               Content = objBankList.First().BankName +" "+objBankList.First().BankShortName+
                                                  " Education Loan," + objBankList.First().BankShortName + " Loans for Education in India," + objBankList.First().BankShortName + " Student Loan, student Loans, " + objBankList.First().BankShortName + " Educational Loan, Education Loans, " + objBankList.First().BankShortName + " Study Loan, Apply Educational Loan"
                                           };

                    Page.Header.Controls.Add(metaKeywords);
                }

            }
            catch (Exception Ex)
            {
                var err = Ex.Message;
                if (Ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + Ex.InnerException.Message;
                }
                const string addInfo = "Error While fetching BindPageTitleAndKeyWords in BankLoanDetails.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        private void BindBankDetails(string bankId)
        {
            var bankData = BankProvider.Instance.GetBankListById(Convert.ToInt16(bankId));
            BindPageTitleAndKeyWords(bankData);
            if (bankData.Count > 0)
            {
                rptLoanInfo.DataSource = bankData; 
                rptLoanInfo.DataBind();

                var query = bankData.Select(result => new
                    {
                        BankName = result.BankName,
                        BankUrl = result.BankUrl,
                        BankShortName = result.BankShortName,
                        BankAddress = result.BankAddress,
                        BankShortDescription = result.BankShortDescription,
                        BankContactPerson = result.BankContactPerson,
                        BankContactPersonDesignation = result.BankContactPersonDesignation,
                        BankContactPersonMobile = result.BankContactPersonMobile,
                        BankPhoneNo = result.BankPhoneNo,
                        BankContactPersonEmailId = result.BankContactPersonEmailId,
                        BankLogo = result.BankLogo
                    }).First();
                lblHeaderCollegeName.Text = query.BankName;
                if (string.IsNullOrEmpty(query.BankLogo))
                {
                    CollegeImageHeader.ImageUrl = String.Format("{0}{1}", "/image.axd?Bank=", (query.BankLogo==null ||string.IsNullOrEmpty(query.BankLogo.ToString()) ? "NoImage.jpg" : query.BankLogo)); 
                    CollegeImageHeader.AlternateText=query.BankName;
                }
                
        }

        }
        protected void rptEntranceExam_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                var hdnBankId = (HiddenField) e.Item.FindControl("hdnBankId");
                var rptLoan = (Repeater) e.Item.FindControl("rptLoan");
                var LRange = (Panel) e.Item.FindControl("LRange");
                var bankData = BankProvider.Instance.GetLoanListByBankId(Convert.ToInt16(hdnBankId.Value));
                if (bankData.Count > 0)
                {
                    LRange.Visible = true;
                    rptLoan.DataSource = bankData;
                    rptLoan.DataBind();
                }
                else
                {
                    LRange.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing rptEntranceExam_ItemDataBound in BankLoanDetails.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
    }
}
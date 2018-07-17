using System;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.NewsArticle
{
    public partial class NoticeMaster : SecurePage 
    {
         Common _objCommon;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindRelatedColleges();
        
            if ( Request.QueryString["NoticeId"] != null)
            {
                
                BindParticularNoticeDetails(Convert.ToInt16(Request.QueryString["NoticeId"]));
            }
            else
            {
                BindNoticeCategory();
              
          
                BindNoticeDetails();
            }
        }
        private void BindRelatedColleges()
        {
            var collegeData = CollegeProvider.Instance.GetCollegeList();
            if (collegeData.Count > 0)
            {
                ddlRelatedCollege.DataSource = collegeData;
                ddlRelatedCollege.DataTextField = "CollegeBranchName";
                ddlRelatedCollege.DataValueField = "CollegeIdBranchId";
                ddlRelatedCollege.DataBind();
                ddlRelatedCollege.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                ddlRelatedCollege.Items.Insert(0, new ListItem("--Select--", "0"));
            }

        }
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            _objCommon=new Common();
            var data = NewsArticleNoticeProvider.Instance.GetAllNoticeList();
             if (data.Count > 0)
            {
                 try
                 {
                     ucCustomPaging.Visible = true;
                             ucCustomPaging.BindDataWithPaging(rptNoticeDetails, Common.ConvertToDataTable(data));
                        }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  Pager_PageIndexChanged in NoticeMaster.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            }
             else
             {
                 ucCustomPaging.Visible = false;
                 rptNoticeDetails.Visible = false;
                 lblInform.Visible = true;
                 lblInform.Text = _objCommon.GetErrorMessage("noRecords") ?? "N/A";
             }

        }
       
        #region Method
        private void BindNoticeDetails()
        {
            _objCommon = new Common();
            var data = NewsArticleNoticeProvider.Instance.GetAllNoticeList();
            if (data.Count > 0)
            {
                try
                {
                    ucCustomPaging.Visible = true;
                    rptNoticeDetails.Visible = true;
                    lblInform.Visible = false;
                    ucCustomPaging.BindDataWithPaging(rptNoticeDetails, Common.ConvertToDataTable(data));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  BindNoticeDetails in NoticeMaster.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                ucCustomPaging.Visible = false;
                rptNoticeDetails.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords") ?? "N/A";
            }

        }
        private void BindParticularNoticeDetails(int noticeId)
        {
            _objCommon = new Common();
            var data = NewsArticleNoticeProvider.Instance.GetNoticeListById(noticeId);
            if (data.Count > 0)
            {
                try
                {
                    ucCustomPaging.Visible = true;
                    rptNoticeDetails.Visible = true;
                    lblInform.Visible = false;
                    ucCustomPaging.BindDataWithPaging(rptNoticeDetails, Common.ConvertToDataTable(data));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  BindParticularNoticeDetails in NoticeMaster.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                ucCustomPaging.Visible = false;
                rptNoticeDetails.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords") ?? "N/A";
            }
        }
        private  void  BindNoticeCategory()
        {
            var noticeData = NewsArticleNoticeProvider.Instance.GetAllNoticeCategoryList();
            if(noticeData.Count>0)
            {
                lblInform.Visible = false;
                ddlNoticeCategory.DataSource =noticeData;
                ddlNoticeCategory.DataTextField = "NoticeCategoryName";
                ddlNoticeCategory.DataValueField = "NoticecategoryId";
                ddlNoticeCategory.DataBind();
                ddlNoticeCategory.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                ddlNoticeCategory.Items.Insert(0, new ListItem("--Select--", "0"));
                
            }
        }

        private  void FilterNoticeByText(string noticeName)
        {
            _objCommon = new Common();
            var data = NewsArticleNoticeProvider.Instance.GetNoticeListByName(noticeName);
            if(data.Count>0)
            {
                ucCustomPaging.Visible = true;
                lblInform.Visible = false;
                rptNoticeDetails.Visible = true;
                ucCustomPaging.BindDataWithPaging(rptNoticeDetails, Common.ConvertToDataTable(data));
                
            }
            else
            { 
                rptNoticeDetails.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords");
            }
        }
        private void FilterNoticeBycategoryId(int noticeCategoryId)
        {
            _objCommon = new Common();
            var data = NewsArticleNoticeProvider.Instance.GetNoticeListByNoticeCategory(noticeCategoryId);
            if (data.Count > 0)
            {
                ucCustomPaging.Visible = true;
                lblInform.Visible = false;
                rptNoticeDetails.Visible = true;
                ucCustomPaging.BindDataWithPaging(rptNoticeDetails, Common.ConvertToDataTable(data));
                
            }
            else
            {
                ucCustomPaging.Visible = false;
                rptNoticeDetails.Visible = false;
                lblInform.Visible = true;
                lblInform.Text = _objCommon.GetErrorMessage("noRecords");
            }
        }
        private void FilternoticedetailsByCollege(int collegeId)
        {
            try
            {
                _objCommon = new Common();
                var data = NewsArticleNoticeProvider.Instance.GetNoticeListOfParticulerCollege(collegeId);
                if (data.Count > 0)
                {
                    ucCustomPaging.Visible = true;
                    lblInform.Visible = false;
                    rptNoticeDetails.Visible = true;
                    ucCustomPaging.BindDataWithPaging(rptNoticeDetails, Common.ConvertToDataTable(data));
                    
                }
                else
                {
                    ucCustomPaging.Visible = false;
                    rptNoticeDetails.Visible = false;
                    lblInform.Visible = true;
                    lblInform.Text = _objCommon.GetErrorMessage("noRecords");
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error while executing FilternoticedetailsByCollege in MoticeMaster.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
      
        #endregion
        #region BtnSearchClick
        protected void BtnSearchClick(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(txtNoticeName.Text))
            {
                FilterNoticeByText(txtNoticeName.Text.Trim());
            }
            else if (ddlNoticeCategory.SelectedIndex > 0 )
            {
                FilterNoticeBycategoryId(Convert.ToInt16(ddlNoticeCategory.SelectedValue));
            }
            else if (ddlRelatedCollege.SelectedIndex > 0 )
            {
                FilternoticedetailsByCollege(Convert.ToInt16(ddlRelatedCollege.SelectedValue));
            }
            
            else
            {
                BindNoticeDetails();
            }
        }
        #endregion
        
    }
}
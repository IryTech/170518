using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.PageTitle
{
    public partial class PageTitle :SecurePage
    {
        #region "DataMember"
        Common _objCommon;
        DataSet _ds;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindPageTitleDetails();
            ValidationErrorMessages();
        }


        #region "MemberFunction"


        private DataSet BindPageTitleDetails()
        {
             _ds = new DataSet();
            try
            {
                
                _objCommon = new Common();
                _ds = _objCommon.GetPageTitle();
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    ucCustomPaging.BindDataWithPaging(rptPageTitle, _ds.Tables[0]);
                }
               
            }

            catch (Exception ex)
            { 
            
            }

            return _ds;
        }



        // Method to Insert update The PageTitle Details
        protected void InsertUpdatePageTitle( string PageName, string PageTitle, string PageKeyword,string PageDesc,int PageId=0)
        {
            _objCommon = new Common();
            SecurePage _objSecurePage = new SecurePage();
            string ErrMsg;
            try
            {
                int i = _objCommon.InsertUpdatePageTitle(PageName, PageTitle, PageKeyword, PageDesc, out ErrMsg, _objSecurePage.LoggedInUserId,PageId);
                if (i > 0)
                {
                    lblSuccess.Visible = true;
                    lblSuccess.Text = ErrMsg;
                    BindPageTitleDetails();
                    ClearControls();
                    hndPageId.Value = null;
                    btnSubmit.Text = "Submit";
           
                }
                else
                {
                    lblError.Visible = true;
                    lblError.CssClass = "warning";
                    lblError.Text = ErrMsg;
                
                }
            }
            catch (Exception Ex)
            {
                string err = Ex.Message;
                if (Ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + Ex.InnerException.Message;
                }
                string addInfo = "Error in GETTING InsertUpdatePageTitle in ManageState.aspx :: -> ";
                ClsExceptionPublisher objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        // Method to Bind The Fcator Details
        protected DataSet GetPageTitleDetailsById(int PageId = 0)
        {
           _ds= new DataSet();
            Common ObjCommon = new Common();
            try
            {
                _ds = ObjCommon.GetPageTitleById(PageId);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindPageTitleDetails in PageTitle.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _ds;
        }

        private void ClearControls()
        {
            txtPageName.Text = "";
            txtPageTitle.Text = "";
            txtPagekeyWord.Text = "";
            txtDesc.Text = "";
        
        }

        private void ValidationErrorMessages()
        {
            _objCommon = new Common();
            rfvPageName.ErrorMessage = _objCommon.GetValidationMessage("rfvPageName");
            rfvPageTitle.ErrorMessage = _objCommon.GetValidationMessage("rfvPageTitle");
            rfvPageKeyWord.ErrorMessage = _objCommon.GetValidationMessage("rfvPageKeyWord");

        }
        
        #endregion

        #region "Events"

        protected void rptPageTitle_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int pageId = Convert.ToInt16(e.CommandArgument);
            _objCommon = new Common();
            switch (e.CommandName)
            {
                case "Edit":
                    {
                        lblError.Visible = false;
                        lblSuccess.Visible = false;
                        _ds = new DataSet();
                        _ds = _objCommon.GetPageTitleById(pageId);
                        var dtb = _ds.Tables[0];
                        if (_ds.Tables[0].Rows.Count > 0)
                        {
                            btnSubmit.Text = "Update";
                            txtPageName.Text = Convert.ToString(dtb.Rows[0]["AjPageName"].ToString());
                            txtPageTitle.Text = Convert.ToString(dtb.Rows[0]["AjPageTitle"].ToString());
                            txtPagekeyWord.Text = Convert.ToString(dtb.Rows[0]["AjPageKeyword"].ToString());
                            txtDesc.Text  = Convert.ToString(dtb.Rows[0]["AjPageDescription"].ToString());
                            hndPageId.Value = pageId.ToString(CultureInfo.InvariantCulture);
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "anyIdweqeewqe", "OpenPoup('divCourseCategoryInsert','650px','sndAddPageTitle');", true);

                        }
                        break;
                    }
             
            }

        }
    

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (hndPageId.Value != string.Empty)
            {

                InsertUpdatePageTitle(txtPageName.Text.Trim(), txtPageTitle.Text.Trim(), txtPagekeyWord.Text.Trim(), txtDesc.Text, Convert.ToInt32(hndPageId.Value));
            }

            else
            {

                InsertUpdatePageTitle(txtPageName.Text.Trim(), txtPageTitle.Text.Trim(), txtPagekeyWord.Text.Trim(), txtDesc.Text); ClearControls();
            }
            
        }


        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            lblSuccess.Visible = false;
            _ds = BindPageTitleDetails();
            if (_ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    rptPageTitle.Visible = true;

                    ucCustomPaging.BindDataWithPaging(rptPageTitle, _ds.Tables[0]);
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in PageTitle.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptPageTitle.Visible = false;

            }
        }
        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
    }
}
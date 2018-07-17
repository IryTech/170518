using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using System.Data;
using IryTech.AdmissionJankari.Components;
using System.Web.Services;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.College
{
    public partial class FactorMaster : SecurePage 
    {

        DataTable _datatable;

        protected void Page_Load(object sender, EventArgs e)
        {

            ucCustomPaging.PageSize = ClsSingelton.PageSize;
            ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindFactorMaster();
        }

        protected void btnFactorInsertUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnFactorId.Value))
                InsertUpdateFactor(txtFactorName.Text.Trim(), Convert.ToInt32(hdnFactorId.Value));
            else
               InsertUpdateFactor(txtFactorName.Text.Trim());
           
           
        }

        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            lblSuccess.Visible = false;
            try
            {
                _datatable = GetFactorMaster();
                if (_datatable.Rows.Count > 0)
                {

                    rptFactor.Visible = true;

                    ucCustomPaging.BindDataWithPaging(rptFactor, _datatable);
                }
                else
                {
                    rptFactor.Visible = false;

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  Pager_PageIndexChanged in FactorMaster.aspx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }



        // Method to insert Update The Factor
        protected void InsertUpdateFactor(string factorName, int factorId = 0)
        {
            var objCommon = new Common();
            var objSecurePage = new SecurePage();
            try
            {
                var errMsg="";
                var i = objCommon.InsertUpdateFactor(factorName, out errMsg,objSecurePage.LoggedInUserId, factorId);

                lblSuccess.CssClass = "success show";
                BindFactorMaster();
                txtFactorName.Text = "";
                hdnFactorId.Value = null;
                btnFactorInsertUpdate.Text = "Save";
                
                
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUpdateFactor in FactorMaster.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }

        // Method to Bind The Fcator Details
        protected DataTable GetFactorMaster(int factorId=0)
        {
            _datatable = new DataTable();
            var objCommon = new Common();
            try
            {
                _datatable = objCommon.GetFactor(factorId);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindFactorMaster in FactorMaster.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _datatable;
        }

        protected void BindFactorMaster()
        {
            _datatable = new DataTable();
            _datatable = GetFactorMaster();
            if (_datatable.Rows.Count > 0)
            {

                ucCustomPaging.BindDataWithPaging(rptFactor, _datatable);
               
            }
        }

        [WebMethod]
        public static string UpdateCollegeRankingFactor(int factorId, string factorName)
        {
            var objCommon = new Common();
            var objSecurePage = new SecurePage();
            var errMsg = "";
            try
            {

                var i = objCommon.InsertUpdateFactor(factorName.Trim(), out errMsg, objSecurePage.LoggedInUserId, factorId);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateCollegeRankingFactor in FactorMaster.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return errMsg;
        }

       
    }
}
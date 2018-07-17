using System;
using System.Web;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using System.Data;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.Setting
{
    public partial class AppsError : SecurePage 
    {
        Common _objCommon;
        DataTable _dt;

        protected void Page_Load(object sender, EventArgs e)
         {
             ucCustomPaging.PageSize = ClsSingelton.PageSize;
            ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
           ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindErrorList();

        }

        #region Method
        protected void BindErrorList()
        {
            _objCommon = new Common();
            _dt = new DataTable();
            try
            {
                _dt = _objCommon.GetApplicationErrorList();
                if (_dt.Rows.Count <= 0) return;


                rptAppsError.Visible = true;
               ucCustomPaging.BindDataWithPaging(rptAppsError, _dt);
               
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindErrorList in AppsError.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }


        #endregion

        #region Event
        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            _objCommon = new Common();
            var data = _objCommon.GetApplicationErrorList();
            if (data.Rows.Count > 0)
            {
                try
                {
                    rptAppsError.Visible = true;
                    lblMsg.Visible = false;
                    ucCustomPaging.BindDataWithPaging(rptAppsError, data);
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in AppError.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptAppsError.Visible = false;
                lblMsg.Visible = true;
                lblMsg.Text = _objCommon.GetErrorMessage("noRecords");
            }

        }
        #endregion

        protected void rptAppsError_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            _objCommon = new Common ();
            if (e.CommandName == "Delete")
            { 
            
                int ErrorId= Convert.ToInt32(e.CommandArgument.ToString());
                if (Convert.ToInt32(ErrorId) >= 0)
                {
                    int data = _objCommon.DeleteApplicationError(Convert.ToInt32(ErrorId));
                    if (data > 0)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Selected Application Error has been Deleted";
                        BindErrorList();
                    
                    }

                }
            }
        }

    }
}
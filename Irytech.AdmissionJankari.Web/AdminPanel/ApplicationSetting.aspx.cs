using System;
using System.Web;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using System.Data;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel
{
    public partial class ApplicationSetting : SecurePage 
    {
        Common _objCommon;
        DataTable _dt;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;

            BindApplicationSetting();
            ValidateControl();
        }

    


        // Method to Bind The Setting value of the Application 

        protected void BindApplicationSetting()
        {
            _objCommon = new Common();
            _dt = new DataTable();

            try
            {
                _dt = _objCommon.GetApplicationSettingValue();


                rptApplicationSetting.Visible = true;
                lblMsg.Visible = false;
                ucCustomPaging.BindDataWithPaging(rptApplicationSetting, _dt);

                //rptApplicationSetting.DataSource = _dt;
                //rptApplicationSetting.DataBind();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindApplicationSetting in ApplicationSetting.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
          

        }


        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            _objCommon = new Common();
            var data = _objCommon.GetApplicationSettingValue();
            if (data.Rows.Count > 0)
            {
                try
                {
                    rptApplicationSetting.Visible = true;
                    lblMsg.Visible = false;
                    ucCustomPaging.BindDataWithPaging(rptApplicationSetting, data);
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in AppException.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptApplicationSetting.Visible = false;
                lblMsg.Visible = true;
                lblMsg.Text = _objCommon.GetErrorMessage("noRecords");
            }

        }


        protected void rptApplicationSetting_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            _objCommon = new Common();
            _dt = new DataTable();
            switch (e.CommandName)
            {
              

                case "Edit":
                    {

                        updateApplicationSetting.Visible = true;
                        hdnApplicationSettingId.Value = e.CommandArgument.ToString();
                        _dt=_objCommon.GetApplicationSettingValue(Convert.ToInt32(hdnApplicationSettingId.Value));
                        if (_dt.Rows.Count <= 0) return;
                            txtSettingName.Text=Convert.ToString(_dt.Rows[0]["ApplicationSettingName"]);
                            txtSettingvalue.Text=Convert.ToString(_dt.Rows[0]["ApplicationSettingValue"]);
                            updateApplicationSetting.Focus();
                        break;
                    }
            }
        }



        protected void InsertUpdateApplicationSetting(int applicationSettingId, string applicationSettingName, string applicationSettingValue)
        {
            _objCommon = new Common();
            var ErrMsg = "";
            try
            {
                var i = _objCommon.InsertUpdateApplicationSettingValue(applicationSettingName, applicationSettingValue, LoggedInUserId, out ErrMsg, applicationSettingId);
                if (i > 0)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = ErrMsg;
                    BindApplicationSetting();
                    txtSettingName.Text = "";
                    txtSettingvalue.Text = "";
                    hdnApplicationSettingId = null;
                }
                else
                {
                    lblMsg.Visible = false;
                    lblwarningMsg.Visible = true;
                    lblwarningMsg.Text = ErrMsg;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUpdateApplicationSetting in ApplicationSetting.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void btnUpdateApplicationSettings(object sender, EventArgs e)
        {
            if(hdnApplicationSettingId.Value!=null)
                 InsertUpdateApplicationSetting(Convert.ToInt32(hdnApplicationSettingId.Value), txtSettingName.Text, txtSettingvalue.Text);
        }

        protected void ValidateControl()
        {
            _objCommon=new Common();
            rfvSettingName.ErrorMessage = _objCommon.GetValidationMessage("rfvSettingName");
            rfvSettingValue.ErrorMessage = _objCommon.GetValidationMessage("rfvSettingValue");
        }
    }
}
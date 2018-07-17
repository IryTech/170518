using System;
using System.Web;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using System.Data;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.Message
{
    public partial class ValidationMessage : SecurePage
    {
        Common _objCommon;
        DataTable _dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
            ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;

          
            BindValidationMessage();
            ValidateControl();
        }


        #region Method
        // Method to Bind The Application validation Message
        protected void BindValidationMessage()
        {
            _objCommon = new Common();
            _dt = new DataTable();
            try
            {
                _dt = _objCommon.GetXmlMessage("Validation");
               if (_dt.Rows.Count > 0)
                {
                    DataTable dtb = new DataTable();
                    dtb.Columns.Add("SrNo");
                    dtb.Columns.Add("MessageID");
                    dtb.Columns.Add("description");
                    dtb.Columns.Add("message_Text");
                    dtb.Merge(_dt);
                    int i = 1;
                    foreach (DataRow drow in dtb.Rows)
                    {
                        drow["SrNo"] = i;
                        i++;
                    }

                    ucCustomPaging.BindDataWithPaging(rptValidationMessage, dtb);
                   
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindValidationMessage in ValidationMessage.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }


       

        protected void ValidateControl()
        {
            _objCommon = new Common();
           rfvValidationId.ErrorMessage = _objCommon.GetValidationMessage("rfvCommon");
           rfvValidationMessage.ErrorMessage = _objCommon.GetValidationMessage("rfvCommon");
        }

        #endregion

       

        protected void UpdateValidationMessage(object sender, EventArgs e)
        {
            _objCommon = new Common();
            int i = _objCommon.UpdateMessage(txtValidationId.Text, txtValidationMessage.Text, "Validation");
            if (i > 0)
            {
                lblMsg.Text = "You successfully Updated the details";
                lblMsg.Visible = true;
                updateValidationMessage.Visible = false;
                BindValidationMessage();
                txtValidationId.Text = "";
                txtValidationMessage.Text = "";
            }

        }


        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            _objCommon = new Common();
            _dt = _objCommon.GetXmlMessage("Validation");
            if (_dt.Rows.Count > 0)
            {
                try
                {
                    rptValidationMessage.Visible = true;
                    lblMsg.Visible = false;
                    DataTable dtb = new DataTable();
                    dtb.Columns.Add("SrNo");
                    dtb.Columns.Add("MessageID");
                    dtb.Columns.Add("description");
                    dtb.Columns.Add("message_Text");
                    dtb.Merge(_dt);
                    int i = 1;
                    foreach (DataRow drow in dtb.Rows)
                    {
                        drow["SrNo"] = i;
                        i++;
                    }
                    ucCustomPaging.BindDataWithPaging(rptValidationMessage, dtb);
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    if (ex.InnerException != null)
                    {
                        err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                    }
                    const string addInfo = "Error in Executing  Pager_PageIndexChanged in ValidationMessage.aspx :: -> ";
                    var objPub = new ClsExceptionPublisher();
                    objPub.Publish(err, addInfo);
                }
            }
            else
            {
                rptValidationMessage.Visible = false;
                lblMsg.Visible = true;
                lblMsg.Text = _objCommon.GetErrorMessage("noRecords");
            }

        }


        protected void rptValidationMessageItemCommand(object source, RepeaterCommandEventArgs e)
        {
            _objCommon = new Common();
            switch (e.CommandName)
            {
                case "Edit":
                    {
                        updateValidationMessage.Visible = true;
                        updateValidationMessage.Focus();
                        lblMsg.Visible = false;
                        txtValidationId.Text = e.CommandArgument.ToString();
                        txtValidationMessage.Text = _objCommon.GetValidationMessage(txtValidationId.Text);
                        break;
                    }
            }
        }

    }
}
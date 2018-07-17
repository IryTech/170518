using IryTech.AdmissionJankari.BL;
using System;
using System.Data;
using System.Linq;
using System.Web.UI;

namespace Irytech.AdmissionJankari.Web.AdminPanel.Form_Package
{
    public partial class FactorMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack) return;
            PackageFactor();
            lblCollegePlacement.Text = "Add Form Package";
        }


        // Method to get the list of the factor 

        private void PackageFactor()
        {
            var data = FormFackageFactor.Instance.GetAllFactor();
            if (data != null)
            {
                rptPresidentDetails.DataSource = data;
                rptPresidentDetails.DataBind();
                lblErrorMessage.Visible = false;
                rptPresidentDetails.Visible = true; 

            }
            else
            {
                rptPresidentDetails.Visible = false;
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "No records found";
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (hndFactorId.Value == null || string.IsNullOrEmpty(hndFactorId.Value))
            {
                InsertUpdate(0, txtFactorName.Text, txtFactorRemark.Text, chkChargeable.Checked == true ? true : false, chkVisible.Checked == true ? true : false);
            }
            else
            {
                InsertUpdate(Convert.ToInt16(hndFactorId.Value), txtFactorName.Text, txtFactorRemark.Text, chkChargeable.Checked == true ? true : false, chkVisible.Checked == true ? true : false);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtFactorName.Text = "";
            txtFactorRemark.Text = "";
            chkChargeable.Checked = false;
            chkVisible.Checked = false;
        }

        private void InsertUpdate(int factorId, string factorName, string factorRemark, bool isPaid, bool isVisible)
        {
            string errMsg = "";
            if ( factorId == 0)
            {
                errMsg = FormFackageFactor.Instance.InsertFactor(factorName, factorRemark, isPaid, isVisible);
            }
            else
            {
                errMsg = FormFackageFactor.Instance.UpdateFactor(factorId,factorName, factorRemark, isPaid, isVisible);
            }
            if (!string.IsNullOrEmpty(errMsg))
            {
                lblSeccessMsg.Visible = true;
                lblErrorMessage.Visible = false;
                lblSeccessMsg.Text = errMsg;
                PackageFactor();
                btnSubmit.Text = "Insert";
                lblCollegePlacement.Text = "Add Form Package";
                hndFactorId.Value = null;
                txtFactorName.Text = "";
                txtFactorRemark.Text = "";
                chkChargeable.Checked = false;
                chkVisible.Checked = false;
            }
            else
            {
                lblSeccessMsg.Visible = false;
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Some problem occurred";
            }
        }

        protected void rptPresidentDetails_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            if(e.CommandName== "Edit")
            {
                hndFactorId.Value = e.CommandArgument.ToString();
                var data = FormFackageFactor.Instance.GetFactorById(Convert.ToInt16(hndFactorId.Value));
                if(data!=null && data.Count>0)
                {
                    var query = data.Select(result => new
                    {
                        result.FactorName,
                        result.FactorRemark,
                        result.IsChargeable,
                        result.IsVisible
                    });


                    txtFactorName.Text = query.FirstOrDefault().FactorName;
                    txtFactorRemark.Text = query.FirstOrDefault().FactorRemark;
                    chkChargeable.Checked = query.FirstOrDefault().IsChargeable;
                    chkVisible.Checked = query.FirstOrDefault().IsVisible;

                    Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScriptg",
                                                                   "OpenPoup('divUniversityCategoryInsert','650px','sndAddCollegePresedentSpeech')", true);
                    

                    btnSubmit.Text = "Update";
                    lblCollegePlacement.Text = "Update the factor";
                }
                     

            }
        }
    }
}
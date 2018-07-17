using IryTech.AdmissionJankari.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Irytech.AdmissionJankari.Web.AdminPanel.Form_Package
{
    public partial class PackageCreation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindPackage();
            BindFactor();
            lblCollegePlacement.Text = "Create Package";
            BindCourse();
        }

        // Method to Bind The Package Details 

        protected void BindPackage()
        {
            try
            {
                var data = FactorProvider.Instance.GetAllPackage();
                if (data != null && data.Count>0)
                {
                    rptPresidentDetails.Visible = true;
                    lblErrorMessage.Visible = false;
                    rptPresidentDetails.DataSource = data;
                    rptPresidentDetails.DataBind();

                }
                else
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "No records found";
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindPackage in PackageCreation.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        protected void rptPresidentDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            RepeaterItem item = e.Item;
            if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
            {
                Repeater rptfactor = (Repeater)item.FindControl("rptFactorDetails");
                if (rptfactor != null)
                {
                    HiddenField hndPackageId = (HiddenField)item.FindControl("hndPackageId");

                    if (hndPackageId != null)
                    {
                        var factorData = FactorProvider.Instance.GetFactorByPackageId(Convert.ToInt16(hndPackageId.Value));
                        if ((factorData != null))
                        {
                            rptfactor.DataSource = factorData[0].ObjPackageFactor;
                            rptfactor.DataBind();
                            rptfactor.Visible = true;

                        }
                        else
                        {
                            rptfactor.Visible = false;
                        }
                    }

                }
            }
        }

        protected void InsertUpdate(int packageId, string packageName, int courseId, string factorId, bool isChargeable, bool isVisible, int packageAmount)
        {
            try
            {

                var msg = packageId == 0 ? FactorProvider.Instance.InsertPackage(packageName, courseId, factorId, isChargeable, isVisible, packageAmount) :
                                         FactorProvider.Instance.UpdatePackage(packageId, courseId, packageName, factorId, isChargeable, isVisible, packageAmount);
                if (!string.IsNullOrEmpty(msg))
                {
                    txtPackageName.Text = "";
                    txtAmount.Text = "";
                    chkChargeable.Checked = false;
                    chkVisible.Checked = false;
                    lblSeccessMsg.Visible = true;
                    lblErrorMessage.Visible = false;
                    lblSeccessMsg.Text = msg;
                    btnSubmit.Text = "Insert";
                    lblCollegePlacement.Text = "Create Package";
                    hndPackageId.Value = null;
                }
                else
                {

                }
                    BindPackage();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUpdate in PackageCreation.aspx  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var factorId= string.Join(", ", chkFactorDetails.Items.Cast<ListItem>().Where(li => li.Selected).Select(x => x.Value).ToArray());

            if (string.IsNullOrEmpty(hndPackageId.Value))
            {
                InsertUpdate(0, txtPackageName.Text,Convert.ToInt16(ddlCourse.SelectedValue), factorId, chkChargeable.Checked == true ? true : false, chkVisible.Checked == true ? true : false,   !string.IsNullOrEmpty(Convert.ToString(txtAmount.Text))?Convert.ToInt32(txtAmount.Text):0);
            }
            else
            {
                InsertUpdate(Convert.ToInt32(hndPackageId.Value), txtPackageName.Text, Convert.ToInt16(ddlCourse.SelectedValue),factorId, chkChargeable.Checked == true ? true : false, chkVisible.Checked == true ? true : false, Convert.ToInt32(txtAmount.Text));
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtPackageName.Text = "";
            txtAmount.Text = "";
            chkChargeable.Checked = false;
            chkVisible.Checked = false;
        }

        protected void rptPresidentDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName== "Edit")
            {

            }
        }

        // Method to Bind The factor 

        protected void BindFactor()
        {
            var factordata = FactorProvider.Instance.GetAllFactor().Where(a => a.IsVisible == true);
            if (factordata != null && factordata.Count() > 0)
            {
                chkFactorDetails.DataSource = factordata;
                chkFactorDetails.DataTextField = "FactorName";
                chkFactorDetails.DataValueField = "FactorId";
                chkFactorDetails.DataBind();
            }
        }

        // Method to Bind The Course
        protected void BindCourse()
        {
            var courseDate = Course.Instance.GetAllCourseList();
            if(courseDate!=null && courseDate.Count>0)
            {
                ddlCourse.DataSource = courseDate;
                ddlCourse.DataTextField = "CourseName";
                ddlCourse.DataValueField = "CourseId";
                ddlCourse.DataBind();
                ListItem objLits = new ListItem();
                objLits.Value = "0";
                objLits.Text = "Select Course";
                ddlCourse.Items.Insert(0, objLits);
            }
            else
            {
                ListItem objLits = new ListItem();
                objLits.Value = "0";
                objLits.Text = "Select Course";
                ddlCourse.Items.Insert(0, objLits);
            }
        }
    }
}

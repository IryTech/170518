using IryTech.AdmissionJankari.BL;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.Components;
using System.Web.UI.HtmlControls;
using System.Data;

namespace Irytech.AdmissionJankari.Web.UserControl
{
    public partial class PackageList : System.Web.UI.UserControl
    {
        Common _obCommon= new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ApplicationSettings.Instance.ShowPackage)
            {
                BindPackageByCourse();
            }
        }
        protected void BindPackageByCourse()
        {
            try
            {
                var data = FactorProvider.Instance.GetAllFactor().Where(a => a.IsVisible == true).ToList();
                if (data != null && data.Count > 0)
                {

                    rptFactor.DataSource = data;
                    rptFactor.DataBind();
                    Control HeaderTemplate = rptFactor.Controls[0].Controls[0];
                    Repeater lblHeader = HeaderTemplate.FindControl("rptPackageName") as Repeater;
                    Repeater rptPackagePrice = rptFactor.Controls[rptFactor.Controls.Count - 1].FindControl("rptPackagePrice") as Repeater;
                    if (lblHeader == null && rptPackagePrice == null) return;
                    var dataPackage = FactorProvider.Instance.GetAllPackage().
                            Where(a => a.courseId == _obCommon.CourseId && a.IsVisible == true).
                               OrderBy(a => a.PackageName).ToList();
                    lblHeader.DataSource = dataPackage;
                    lblHeader.DataBind();
                    rptPackagePrice.DataSource = dataPackage;
                    rptPackagePrice.DataBind();
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

        protected void rptFactor_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;
            if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
            {
                HiddenField hndfactorId = (HiddenField)item.FindControl("hndFactorId");
                var dataPackage = FactorProvider.Instance.GetAllPackage().
                        Where(a => a.courseId == _obCommon.CourseId && a.IsVisible == true).
                            OrderBy(a => a.PackageName).ToList();

                foreach (var packagedetails in dataPackage)
                {
                    if (hndfactorId != null)
                    {
                        var packageFactor = FactorProvider.Instance.GetFactorByPackageId(packagedetails.PackageId).Where(a => a.objPackage.courseId == _obCommon.CourseId && a.objPackage.IsVisible == true).ToList();

                        HtmlControl tr = (HtmlControl)item.FindControl("tdExists");

                        var yourspan = new HtmlGenericControl("span");
                        var factorDetails = packageFactor.FirstOrDefault().ObjPackageFactor.Where(a => a.FactorID == Convert.ToInt32(hndfactorId.Value));

                        if (factorDetails != null && factorDetails.Count() > 0)
                        {
                            yourspan.Attributes["class"] = "right";
                            yourspan.InnerHtml = "&#10004;";
                        }
                        else
                        {
                            yourspan.Attributes["class"] = "wrong";
                            yourspan.InnerHtml = "&#10006;";
                        }

                        tr.Controls.Add(yourspan);


                    }
                }
            }

        }

        protected void rptFactor_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName== "Select")
            {
                var packageId = Convert.ToInt32(e.CommandArgument);
                var packageDetails = FactorProvider.Instance.GetPackageById(packageId);
                if (packageDetails != null && packageDetails.Count > 0)
                {
                    var data = packageDetails.FirstOrDefault();
                    if (data != null)
                    {
                        SecurePage objSecurePage = new SecurePage();
                        var userid = objSecurePage.LoggedInUserId;
                        Consulling objConsulling = new Consulling();
                        int i = objConsulling.InsertUpdateUserTransctionalDetails(objSecurePage.LoggedInUserId, "", false, "", "", " ", Convert.ToString(data.PackageAmount), packageId);
                        if(data.PackageAmount>0)
                        {

                       
                           
                            Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScriptdg",
                                                                  "OpenPoup('divUniversityCategoryInsert','650px','sndAddCollegePresedentSpeechsdasd')", true);
                           
                        }
                    }
                }

            }
        }
    }
}
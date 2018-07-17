using System;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.Components.Web.Controls;

namespace IryTech.AdmissionJankari20.Web.counselling
{
    public partial class Payment : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            UcPayment.TotalAmountInserted = "25000";
            UcPayment.RedirectUrl = Utils.AbsoluteWebRoot + "ConformationPage.aspx";
            // UpdateUserTranscationalDetails();
        }

        private void UpdateUserTranscationalDetails()
        {
            var objSecurePage = new SecurePage();
            var objCommon = new Common();

            var objConsulling = new Consulling();
            var formNum = "ADMJ" + System.DateTime.Now.Year + objCommon.CourseId.ToString() + objSecurePage.LoggedInUserId.ToString();
            try
            {
                var i = objConsulling.InsertUpdateUserTransctionalDetails(objSecurePage.LoggedInUserId, formNum, true,
                                                                          "", "", "","26100");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateUserTranscationalDetails in Payment.aspx for user :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using System.Data;
using System.Globalization;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcStudentAcademic : System.Web.UI.UserControl
    {
        Common _ObjCommon;
        Consulling _ObjConsulling;
        DataTable _dt;
        static string objElgigibilty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            _ObjCommon = new Common();
           

        }
        // Method To validate The Admission Eeligibilty
        public void ValidateAcademicInfo(int courseId)
        {
           

            var datas = CourseProvider.Instance.GetCourseById(courseId);
            if (datas.Count > 0)
            {
                var query = from result in datas
                            select new
                            {
                                CourseEligibityName = result.CourseEligibityName
                            };
                
                    var records = query.First();
                    objElgigibilty = records.CourseEligibityName;
                    HideShowAcademicInfo(records.CourseEligibityName);
                
            }           
            
        }
        // Method to Bind The Laterentry Info
        public void BindLateralEntryInfo(int courseId)
        {
            _ObjConsulling = new Consulling();
            _dt = new DataTable();
            try
            {
                _dt = _ObjConsulling.GetCourseAdmissionEligibilty(courseId);
                if (_dt.Rows.Count > 0)
                {
                    ltrEntry.Visible = true;
                    rbtCourseAdmissionEligibilty.DataSource = _dt;
                    rbtCourseAdmissionEligibilty.DataTextField = "AjCourseCounsellingEligibilityName";
                    rbtCourseAdmissionEligibilty.DataValueField = "AjCourseCounsellingEligibilityId";
                    rbtCourseAdmissionEligibilty.DataBind();

                }
                else
                {
                    ltrEntry.Visible = false;

                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindLateralEntryInfo in UcStudentAcademic.axcs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
        
        //  Method To Hide Show
        public void HideShowAcademicInfo(string eligibiltyName )
        {
            switch (eligibiltyName)
            {
                case "10":
                    {
                        Student12Info.Visible = false;
                        grdInfo.Visible = false;
                        dicInfo.Visible = false;
                        Student10info.Visible = true;
                        Student12Info.IntermediateType = false;


                        break;
                    }
                case "12":
                    {
                        grdInfo.Visible = false;
                        dicInfo.Visible = false;
                        Student10info.Visible = true;
                        Student12Info.Visible = true;
                        Student12Info.IntermediateType = true;
                        Student12Info.ValidationPer = true;
                        
                        break;
                    }
                case "13":
                    {
                        Student10info.Visible = true;
                        Student12Info.Visible = true;
                        grdInfo.Visible = false;
                        Student12Info.IntermediateType = false;
                        dicInfo.Visible = true;
                        break;
                    }
                case "15":
                    {
                        Student10info.Visible = true;
                        Student12Info.Visible = true;
                        grdInfo.Visible = true;
                        Student12Info.IntermediateType = false;
                        dicInfo.Visible = false;
                        break;
                    }
                case "11":
                    {
                        Student10info.Visible = true;
                        Student12Info.Visible = false;
                        dicInfo.Visible = true;
                        grdInfo.Visible = false;
                        Student12Info.IntermediateType = false;

                        break;
                    }
            }
        }

        protected void rbtCourseAdmissionEligibilty_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ObjConsulling = new Consulling();
            _dt = new DataTable();
            _dt = _ObjConsulling.GetCourseAdmissionEligibiltyById(Convert.ToInt32(rbtCourseAdmissionEligibilty.SelectedValue));
            if (_dt.Rows.Count > 0)
            {
                HideShowAcademicInfo(Convert.ToString(_dt.Rows[0]["AjCollegeCourseEligibiltyName"]));
                objElgigibilty = Convert.ToString(_dt.Rows[0]["AjCollegeCourseEligibiltyName"]);
            }
        }

        #region Property
        public UcHighSchoolInfo HighSchoolInfo
        {
            get { return Student10info; }
        }
        public UcDiplomaInfo DiplomaInfo
        {
            get { return dicInfo; }
        }

        public UcGraduateInfo GraduateInfo
        {
            get { return grdInfo; }
        }
        public UcIntermediateInfo IntermediateInfo
        {
            get { return Student12Info; }
        }

        public  string Eligibilty
        {
            get { return objElgigibilty; }
        }

        #endregion
        
    }
}
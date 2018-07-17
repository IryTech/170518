using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcMostViewdExam : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Pager.PageSize = ApplicationSettings.Instance.MostViewExamPageSize;
            Pager.ButtonsCount = ApplicationSettings.Instance.MostViewExamPageCount;
            Pager.PagerPageIndexChanged += PagerPageIndexChanged;
            if (IsPostBack) return;
            BindMostViewdExam();
        }


        protected void PagerPageIndexChanged(object sender, EventArgs e)
        {
            Common objCommon = new Common();
            var data = ExamProvider.Instance.GetMostViewdExamByCourse(objCommon.CourseId);
            data = data.Where(exam => exam.ExamStatus == true).ToList();
            if (data.Count <= 0) return;
            try
            {
                rptMostviewdExam.Visible = true;
                Pager.BindDataWithPaging(rptMostviewdExam, Common.ConvertToDataTable(data));
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  Pager_PageIndexChanged in UcMostViewdExam.ascx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }


        public void BindMostViewdExam()
        {
            try
            {
                Common objCommon = new Common();
                var data = ExamProvider.Instance.GetMostViewdExamByCourse(objCommon.CourseId);
                data = data.Where(exam => exam.ExamStatus == true).ToList();
                if (data.Count > 0)
                {
                    Pager.BindDataWithPaging(rptMostviewdExam, Common.ConvertToDataTable(data));
                    lblMostViewdTitle.Visible = true;
                    Pager.Visible = true;
                    rptMostviewdExam.Visible = true;
                }
                else
                {
                    lblMostViewdTitle.Visible=false;
                    rptMostviewdExam.Visible = false;
                    Pager.Visible = false;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.InnerException.Message;
                }
                const string addInfo = "Error in Executing  BindMostViewdExam in UcMostViewdExam.ascx :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

        }
    }
}
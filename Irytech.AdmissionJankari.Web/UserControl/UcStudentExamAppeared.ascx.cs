using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcStudentExamAppeared : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #region Properties
        public string ExamApp1
        {
            get { return txtExamAppeared1.Text; }
        }
        public string ExamApp2
        {
            get { return txtExamAppeared2.Text ; }
        }
        public string ExamApp3
        {
            get { return txtExamAppeared3.Text; }
        }
        public string ExamApp1Rank1
        {
            get { return txtExamAppearedRank1.Text; }
        }
        public string ExamApp2Rank2
        {
            get { return txtExamAppearedRank2.Text; }
        }
        public string ExamApp3Rank3
        {
            get { return txtExamAppearedRank3.Text; }
        }

        #endregion
    }
}
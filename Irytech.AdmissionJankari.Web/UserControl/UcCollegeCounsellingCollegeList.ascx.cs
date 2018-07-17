using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class UcCollegeCounsellingCollegeList : System.Web.UI.UserControl
    {
        public string SPonserValue;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            UcStudentPersonelInfo.BindUserPersonelInfo();
            BindCollegeParticipentList();
        }


        // Method to Get The Participanet  College List
        protected void BindCollegeParticipentList()
        {
            var objConsulling = new Consulling();
            var data = objConsulling.GetParticipentCollegeListByCourse(UcStudentPersonelInfo.ConsullingCourseId );
            if (data.Count > 0)
            {
                dtlCollegeList.DataSource = data;
                dtlCollegeList.DataBind();
            }
        }

        public string Sponser
        {
            get
            {
                return SPonserValue;

            }
            set { SPonserValue = value; }
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;

namespace IryTech.AdmissionJankari20.Web.UserControl
{
    public partial class StudentCollegePreffered : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {if(IsPostBack)return;
            var obj = new Common();
           hdnCourse.Value=Convert.ToString(obj.CourseId);

        }
    }
}
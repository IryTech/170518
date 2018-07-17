using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;

namespace IryTech.AdmissionJankari20.Web.AdminPanel.Course
{

	public partial class CoursePaymentMaster : SecurePage
	{
		#region " Events "
		Common _objCommon;
		protected void Page_Load( object sender, EventArgs e )
		{
            ucCustomPaging.PageSize = ClsSingelton.PageSize;
           ucCustomPaging.ButtonsCount = ClsSingelton.PageButtonCount;
			ucCustomPaging.PagerPageIndexChanged += PagerPageIndexChanged;
			ValidationErrorMessages();
			if (IsPostBack) return;
			BindCourseCategory();
			BindCoursePaymentMaster();


		}




		protected void PagerPageIndexChanged( object sender, EventArgs e )
		{
			_objCommon = new Common();
			var data = CourseProvider.Instance.GetCoursePaymentMasterList();
			if (data.Count > 0)
			{
				try
				{
					rptCoursePaymentAmount.Visible = true;
					ucCustomPaging.BindDataWithPaging(rptCoursePaymentAmount, Common.ConvertToDataTable(data));
					lblMsg.Visible=false;
                    lblMsg.Attributes.Add("class", "success show");

				}
				catch (Exception ex)
				{
					var err = ex.Message;
					if (ex.InnerException != null)
					{
						err = err + " :: Inner Exception :- " + ex.ToString();
					}
					const string addInfo = "Error while executing PagerPageIndexChanged in CoursePaymentMaster.aspx:: -> ";
					var objPub = new ClsExceptionPublisher();
					objPub.Publish(err, addInfo);
				}
			}
			else
			{
				rptCoursePaymentAmount.Visible = false;
				lblMsg.Visible = true;
				lblMsg.Attributes.Add("class", "info");
				lblMsg.InnerText = _objCommon.GetErrorMessage("noRecords") ?? "N/A";

			}

		}

		private void ValidationErrorMessages()
		{
			_objCommon = new Common();
			rfvCoursePaymentCategory.ErrorMessage = _objCommon.GetValidationMessage("rfvCoursePaymentCategory") ?? "N/A";
			rfvddlCourse.ErrorMessage = _objCommon.GetValidationMessage("rfvddlCourse") ?? "N/A";
			rfvOnlinePaymentAmount.ErrorMessage = _objCommon.GetValidationMessage("rfvOnlinePaymentAmount") ?? "N/A";
			rfvOfflinePaymentAmount.ErrorMessage = _objCommon.GetValidationMessage("rfvOfflinePaymentAmount") ?? "N/A";

		}


	
		protected void ddlCourseCategory_SelectedIndexChanged( object sender, EventArgs e )
		{
			if (ddlCourseCategory.SelectedIndex > 0)
			{
				BindCourseListByCategory(Convert.ToInt32(ddlCourseCategory.SelectedValue));
				lblMsg.Visible=false;

			}

		}
		public void BindCourseListByCategory( int courseCategoryId )
		{
			var data = CourseProvider.Instance.GetCourseByCategory(courseCategoryId);

			if (data.Count > 0)
			{
				try
				{
					ddlCourse.DataSource = data;
					ddlCourse.DataTextField = "CourseName";
					ddlCourse.DataValueField = "CourseId";
					ddlCourse.DataBind();
					ddlCourse.Items.Insert(0, new ListItem("Please Select", "0"));
					ddlCourse.Enabled = true;
				}

				catch (Exception ex)
				{
					var err = ex.Message;
					if (ex.InnerException != null)
					{
						err = err + " :: Inner Exception :- " + ex.InnerException.Message;
					}
					const string addInfo = "Error in Executing  BindCourseListByCategory in CoursePaymentMaster.aspx :: -> ";
					var objPub = new ClsExceptionPublisher();
					objPub.Publish(err, addInfo);
				}
			}
		}


   

		#endregion " Events "




		#region "Bind Methords"

		public void BindCoursePaymentMaster()
		{
			_objCommon = new Common();
			var data = CourseProvider.Instance.GetCoursePaymentMasterList();
			if (data.Count > 0)
			{
				try
				{
					rptCoursePaymentAmount.Visible = true;
					ucCustomPaging.BindDataWithPaging(rptCoursePaymentAmount, Common.ConvertToDataTable(data));

				}
				catch (Exception ex)
				{
					var err = ex.Message;
					if (ex.InnerException != null)
					{
						err = err + " :: Inner Exception :- " + ex.InnerException.Message;
					}
					const string addInfo = "Error in Executing  BindCoursePaymentMaster in CoursePaymentMaster.aspx :: -> ";
					var objPub = new ClsExceptionPublisher();
					objPub.Publish(err, addInfo);
				}
			}
			else
			{
				rptCoursePaymentAmount.Visible = false;
				lblMsg.Visible = true;
				lblMsg.Attributes.Add("class", "info");
				lblMsg.InnerText = _objCommon.GetErrorMessage("noRecords") ?? "N/A";

			}
		}





		private void BindCourseCategory()
		{

			var courseCategory = CourseProvider.Instance.GetAllCourseCategoryList();
			if (courseCategory.Count > 0)
			{
				ddlCourseCategory.DataSource = courseCategory;
				ddlCourseCategory.DataTextField = "CourseCategoryName";
				ddlCourseCategory.DataValueField = "CourseCategoryId";
				ddlCourseCategory.DataBind();
				ddlCourseCategory.Items.Insert(0, new ListItem("Please Select", "0"));
				ddlCourse.Items.Insert(0, new ListItem("Please Select", "0"));
			}
			else
			{
				ddlCourseCategory.Items.Insert(0, new ListItem("Please Select", "0"));
				ddlCourse.Items.Insert(0, new ListItem("Please Select", "0"));
			}

		}


		public void GetAllCourseList()
		{
			var data = CourseProvider.Instance.GetAllCourseList();
			try
			{
				ddlCourse.DataSource = data;
				ddlCourse.DataTextField = "CourseName";
				ddlCourse.DataValueField = "CourseId";
				ddlCourse.DataBind();
				ddlCourse.Items.Insert(0, new ListItem("Please Select"));
			}

			catch (Exception ex)
			{
				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.InnerException.Message;
				}
				const string addInfo = "Error in Executing GetAllCourseList  in CoursePaymentMaster.aspx :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);
			}
		}

		#endregion "Bind Methords"


		#region "InsertUpdate"


		private void InsertUpdateCoursePaymentMasterDetails()
		{
			SecurePage _objSecurePage = new SecurePage();

			string errMsg = "";
			int result;
			if (btntCoursePaymentAmount.Text == "Save")
			{
				result = CourseProvider.Instance.InsertCoursePaymentMasterDetails(Convert.ToInt32(ddlCourse.SelectedValue.ToString()), txtOnlinePaymentAmount.Text.Trim(), txtOfflinePaymentAmount.Text.Trim(), out errMsg, 1);
				if (result > 0)
				{
					lblMsg.Visible = true;
                    lblMsg.Attributes.Add("class", "success hide");
					lblMsg.InnerText = errMsg;
					ClearFields();
				
				}
				else
				{
					lblMsg.Visible = true;
					lblMsg.Attributes.Add("class", "error");
					lblMsg.InnerText = errMsg;

				}
			}
			else
			{
				result = CourseProvider.Instance.UpdateCoursePaymentMasterDetails(Convert.ToInt16(0), Convert.ToInt32(ddlCourseCategory.SelectedValue.ToString()), txtOnlinePaymentAmount.Text.Trim(), txtOfflinePaymentAmount.Text.Trim(), out errMsg, 1);
				if (result > 0)
				{
					 btntCoursePaymentAmount.Text = "Save";
					lblMsg.Visible = true;
					lblMsg.Attributes.Add("class", "success");
					lblMsg.InnerText = errMsg;
					BindCoursePaymentMaster();
					ClearFields();
					
				}
				else
				{
					lblMsg.Visible = true;
					lblMsg.Attributes.Add("class", "error");
					lblMsg.InnerText = _objCommon.GetErrorMessage("noRecords") ?? "N/A";

				}
			}
		}


		private void ClearFields()
		{

			txtOnlinePaymentAmount.Text = string.Empty;
			txtOfflinePaymentAmount.Text = string.Empty;
		}


		protected void btntCoursePaymentAmount_Click( object sender, EventArgs e )
		{

			InsertUpdateCoursePaymentMasterDetails();

			BindCoursePaymentMaster();
			ddlCourseCategory.SelectedIndex = 0;
			ddlCourse.SelectedIndex = 0;




		}
		#endregion "InsertUpdate"










	}
}
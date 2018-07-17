using System;
using System.Collections.Generic;
using IryTech.AdmissionJankari.DAL;
using System.Data.SqlClient;
using System.Data;

namespace IryTech.AdmissionJankari.BL
{
	public class Course : CourseProvider
	{
		private DbWrapper _objDataWrapper;
		private DataSet _dataSet;
		private int _i;


		#region Method defined  for the Course category
		public override int InsertCourseCategory( string courseCategoryName, out string errmsg, int createdBy, bool courseCategoryStatus = false )
		{
			_objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
			errmsg = string.Empty;
			try
			{
				_objDataWrapper.AddParameter("@CourseCategoryName", courseCategoryName);
				_objDataWrapper.AddParameter("@CourseCategoryStatus", courseCategoryStatus);
				_objDataWrapper.AddParameter("@CreatedBy", createdBy);
				var objErrmsg =
					(SqlParameter)
					(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
				_i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCourseCategoryDetails");
				if (objErrmsg != null && objErrmsg.Value != null)
					errmsg = Convert.ToString(objErrmsg.Value);
			}
			catch (Exception ex)
			{
				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing InsertCourseCategory in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);

			}





			return _i;
		}

		public override int UpdateCourseCategory( int courseCategoryId, string courseCategoryName, out string errmsg, int modifiedBy, bool courseCategoryStatus = false )
		{
			_objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
			errmsg = string.Empty;
			try
			{
				_objDataWrapper.AddParameter("@CourseCategoryId", courseCategoryId);
				_objDataWrapper.AddParameter("@CourseCategoryName", courseCategoryName);
				_objDataWrapper.AddParameter("@CourseCategoryStatus", courseCategoryStatus);
				_objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
				var objErrmsg =
					(SqlParameter)
					(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
				_i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCourseCategoryDetails");
				if (objErrmsg != null && objErrmsg.Value != null)
					errmsg = Convert.ToString(objErrmsg.Value);
			}
			catch (Exception ex)
			{
				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing UpdateCourseCategory in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);

			}
			return _i;
		}

		public override List<CourseCategoryProperty> GetAllCourseCategoryList()
		{
			var courseCategoryList = new List<CourseCategoryProperty>();
			_objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
			_dataSet = new DataSet();
			try
			{
				_dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCourseCategoryList");
				courseCategoryList = BindCourseCategoryObject(_dataSet.Tables[0]);
			}
			catch (Exception ex)
			{
				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing GetAllCourseCategoryList in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);

			}
			return courseCategoryList;

		}

		public override List<CourseCategoryProperty> GetCourseCategoryById( int courseCategoryId )
		{
			var courseCategoryList = new List<CourseCategoryProperty>();
			_objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
			_dataSet = new DataSet();
			try
			{
				_objDataWrapper.AddParameter("@CourseCategoryId", courseCategoryId);
				_dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCourseCategoryList");
				courseCategoryList = BindCourseCategoryObject(_dataSet.Tables[0]);
			}
			catch (Exception ex)
			{

				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing GetCourseCategoryById in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);
			}
			return courseCategoryList;

		}
		#endregion end Region for the Course category


		#region Method s defined for the Course Eligibilty

		public override int InsertCourseEligibilty( string courseEligibiltyName, int createdBy, out string errmsg, bool courseEligibiltyStatus = false )
		{
			_objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
			errmsg = string.Empty;
			try
			{
				_objDataWrapper.AddParameter("@CourseEligibilityName", courseEligibiltyName);
				_objDataWrapper.AddParameter("@CourseEligibilityStatus", courseEligibiltyStatus);
				_objDataWrapper.AddParameter("@CreatedBy", createdBy);
				var objErrMsg =
					(SqlParameter)
					(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
				_i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCourseEligibilityDetails");
				if (objErrMsg != null && objErrMsg.Value != null)
					errmsg = Convert.ToString(objErrMsg.Value);


			}
			catch (Exception ex)
			{
				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing InsertCourseCategory in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);

			}
			return _i;
		}

		public override int UpdateCourseEligibilty( int courseEligibiltyId, string courseEligibiltyName, int modifiedBy, out string errmsg, bool courseEligibiltyStatus = false )
		{
			_objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
			errmsg = string.Empty;
			try
			{
				_objDataWrapper.AddParameter("@CourseEligibilityId", courseEligibiltyId);
				_objDataWrapper.AddParameter("@CourseEligibilityName", courseEligibiltyName);
				_objDataWrapper.AddParameter("@CourseEligibilityStatus", courseEligibiltyStatus);
				_objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
				var objErrMsg =
					(SqlParameter)
					(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
				_i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCourseEligibilityDetails");
				if (objErrMsg != null && objErrMsg.Value != null)
					errmsg = Convert.ToString(objErrMsg.Value);


			}
			catch (Exception ex)
			{
				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing UpdateCourseEligibilty in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);

			}
			return _i;
		}

		public override List<CourseEligibiltyProperty> GetAllCourseEligibiltyList()
		{
			var courseEligibiltyList = new List<CourseEligibiltyProperty>();
			_objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
			_dataSet = new DataSet();
			try
			{
				_dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCourseEligibiltyList");
				courseEligibiltyList = BindCourseEligibiltyObject(_dataSet.Tables[0]);
			}
			catch (Exception ex)
			{
				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing GetAllCourseEligibiltyList in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);

			}
			return courseEligibiltyList;
		}

		public override List<CourseEligibiltyProperty> GetCourseEligibiltyById( int courseeEligibiltyId )
		{
			var courseEligibiltyList = new List<CourseEligibiltyProperty>();
			_objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
			_dataSet = new DataSet();
			try
			{
				_objDataWrapper.AddParameter("@CourseEligibiltyId", courseeEligibiltyId);
				_dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCourseEligibiltyList");
				courseEligibiltyList = BindCourseEligibiltyObject(_dataSet.Tables[0]);
			}
			catch (Exception ex)
			{
				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing GetCourseEligibiltyById in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);

			}
			return courseEligibiltyList;
		}


		#endregion


		#region Method Defined for the Course Master
        public override int InsertCourseMasterDetails(string courseName, string courseUrl, string courseTitle, string courseMetaTag, string courseMetaDesc, string courseDesc, string courseshortName, string coursePopularName, int courseCategoryId, int courseEligibiltyId, int createdBy, out string errMsg, string CourseImage, string HelpLineNo, bool courseStatus = false, bool IsBookSeatVisible = true)
		{
			_objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
			errMsg = string.Empty;
			try
			{

				_objDataWrapper.AddParameter("@CourseName", courseName);
				_objDataWrapper.AddParameter("@CourseUrl", courseUrl);
				_objDataWrapper.AddParameter("@CourseTitle", courseTitle);
				_objDataWrapper.AddParameter("@CourseMetaTag", courseMetaTag);
				_objDataWrapper.AddParameter("@CourseMetaDesc", courseMetaDesc);
				_objDataWrapper.AddParameter("@CourseDesc", courseDesc);
				_objDataWrapper.AddParameter("@CourseShortName", courseshortName);
				_objDataWrapper.AddParameter("@CoursePopularName", coursePopularName);
				_objDataWrapper.AddParameter("@CoursecategoryId", courseCategoryId);
				_objDataWrapper.AddParameter("@CourseEligibiltyId", courseEligibiltyId);
				_objDataWrapper.AddParameter("@CourseStatus", courseStatus);
				_objDataWrapper.AddParameter("@CourseImage", CourseImage);
				_objDataWrapper.AddParameter("createdBy", createdBy);
                _objDataWrapper.AddParameter("@AjHelpLineNo", HelpLineNo);
                _objDataWrapper.AddParameter("@AjIsBookSeatVisible", IsBookSeatVisible);
                
				var objErrmsg =
					(SqlParameter)
					(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
				_i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCourseMasterDetails");
				if (objErrmsg != null && objErrmsg.Value != null)
					errMsg = Convert.ToString(objErrmsg.Value);
			}
			catch (Exception ex)
			{
				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing InsertCourseMasterDetails in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);

			}
			return _i;
		}

        public override int UpdateCourseMasterDetails(int courseId, string courseName, string courseUrl, string courseTitle, string courseMetaTag, string courseMetaDesc, string courseDesc, string courseshortName, string coursePopularName, int courseCategoryId, int courseEligibiltyId, int createdBy, out string errMsg, string CourseImage, string HelpLineNo, bool courseStatus = false, bool IsBookSeatVisible = true)
		{
			_objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
			errMsg = string.Empty;
			try
			{
				_objDataWrapper.AddParameter("@CourseId", courseId);
				_objDataWrapper.AddParameter("@CourseName", courseName);
				_objDataWrapper.AddParameter("@CourseUrl", courseUrl);
				_objDataWrapper.AddParameter("@CourseTitle", courseTitle);
				_objDataWrapper.AddParameter("@CourseMetaTag", courseMetaTag);
				_objDataWrapper.AddParameter("@CourseMetaDesc", courseMetaDesc);
				_objDataWrapper.AddParameter("@CourseDesc", courseDesc);
				_objDataWrapper.AddParameter("@CourseShortName", courseshortName);
				_objDataWrapper.AddParameter("@CoursePopularName", coursePopularName);
				_objDataWrapper.AddParameter("@CoursecategoryId", courseCategoryId);
				_objDataWrapper.AddParameter("@CourseEligibiltyId", courseEligibiltyId);
				_objDataWrapper.AddParameter("@CourseStatus", courseStatus);
				_objDataWrapper.AddParameter("@CourseImage", CourseImage);
				_objDataWrapper.AddParameter("createdBy", createdBy);
                _objDataWrapper.AddParameter("@AjHelpLineNo", HelpLineNo);
                _objDataWrapper.AddParameter("@AjIsBookSeatVisible", IsBookSeatVisible);
				var objErrmsg =
					(SqlParameter)
					(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
				_i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCourseMasterDetails");
				if (objErrmsg != null && objErrmsg.Value != null)
					errMsg = Convert.ToString(objErrmsg.Value);
			}
			catch (Exception ex)
			{
				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing UpdateCourseMasterDetails in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);

			}
			return _i;
		}

		public override List<CourseMasterProperty> GetAllCourseList()
		{
			_objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
			_dataSet = new DataSet();
			var courseMasterObjectList = new List<CourseMasterProperty>();

			try
			{

				_dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCourseList");
				courseMasterObjectList = BindCourseMasterObject(_dataSet.Tables[0]);
			}
			catch (Exception ex)
			{
				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing GetAllCourseList in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);

			}
			return courseMasterObjectList;
		}
		public override List<CourseMasterProperty> GetCourseSourceHome()
		{
			_objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
			_dataSet = new DataSet();
			var courseMasterObjectList = new List<CourseMasterProperty>();

			try
			{

				_dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCourseSourceHome");
				courseMasterObjectList = BindCourseHome(_dataSet.Tables[0]);
			}
			catch (Exception ex)
			{
				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing GetCourseSourceHome in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);

			}
			return courseMasterObjectList;
		}
		public override List<CourseMasterProperty> GetCourseById( int courseId )
		{
			_objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
			_dataSet = new DataSet();
			var courseMasterObjectList = new List<CourseMasterProperty>();

			try
			{

				_objDataWrapper.AddParameter("@CourseId", courseId);
				_dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCourseList");
				courseMasterObjectList = BindCourseMasterObject(_dataSet.Tables[0]);
			}
			catch (Exception ex)
			{
				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing GetCourseById in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);

			}
			return courseMasterObjectList;
		}

		public override List<CourseMasterProperty> GetCourseByCategory( int courseCategoryId )
		{
			_objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
			_dataSet = new DataSet();
			var courseMasterObjectList = new List<CourseMasterProperty>();

			try
			{

				_objDataWrapper.AddParameter("@CourseCategoryId", courseCategoryId);
				_dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCourseList");
				courseMasterObjectList = BindCourseMasterObject(_dataSet.Tables[0]);
			}
			catch (Exception ex)
			{
				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing GetCourseByCategory in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);

			}
			return courseMasterObjectList;
		}

		public override List<CourseMasterProperty> GetCourseByEligibity( int courseEligibityId )
		{
			_objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
			_dataSet = new DataSet();
			var courseMasterObjectList = new List<CourseMasterProperty>();

			try
			{
				_objDataWrapper.AddParameter("@CourseEligibiltyId", courseEligibityId);
				_dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCourseList");
				courseMasterObjectList = BindCourseMasterObject(_dataSet.Tables[0]);
			}
			catch (Exception ex)
			{
				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing GetCourseByEligibity in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);

			}
			return courseMasterObjectList;
		}

		#endregion


		#region Private Member of the Class
		private List<CourseCategoryProperty> BindCourseCategoryObject( DataTable dataTable )
		{
			var courseCategoryList = new List<CourseCategoryProperty>();

			try
			{
				if (dataTable.Rows.Count > 0)
				{
					for (var j = 0; j < dataTable.Rows.Count; j++)
					{
						var objcoursecategory = new CourseCategoryProperty
													{
														CourseCategoryId =
																		(dataTable.Rows[j]["AjCourseCategoryId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjCourseCategoryId"]),
														CourseCategoryName =
																	   (dataTable.Rows[j]["AjCourseCategoryName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjCourseCategoryName"]),
														CourseCategoryStatus =
																	   (dataTable.Rows[j]["AjCourseCategoryStatus"] is DBNull) ? false : Convert.ToBoolean(dataTable.Rows[j]["AjCourseCategoryStatus"])

													};
						courseCategoryList.Add(objcoursecategory);
					}
				}

			}
			catch (Exception ex)
			{

				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing BindCourseCategoryObject in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);
			}
			return courseCategoryList;
		}
		private List<CourseEligibiltyProperty> BindCourseEligibiltyObject( DataTable dataTable )
		{
			var courseEligibiltyList = new List<CourseEligibiltyProperty>();
			try
			{
				if (dataTable.Rows.Count > 0)
				{
					for (var j = 0; j < dataTable.Rows.Count; j++)
					{
						var objCourseEligibilty = new CourseEligibiltyProperty
													  {
														  CourseEligibilityId =
															  (dataTable.Rows[j]["AjCollegeCourseEligibiltyId"] is DBNull) ? 0 : Convert.ToInt32(
																	dataTable.Rows[j]["AjCollegeCourseEligibiltyId"]),

														  CourseEligibiltyName =
																(dataTable.Rows[j]["AjCollegeCourseEligibiltyName"] is DBNull) ? null : Convert.ToString(
																	  dataTable.Rows[j]["AjCollegeCourseEligibiltyName"]),

														  CourseEligibilityStatus =
																(dataTable.Rows[j]["AjCollegeCourseEligibiltyStatus"] is DBNull) ? false : Convert.ToBoolean(
																  dataTable.Rows[j]["AjCollegeCourseEligibiltyStatus"])


													  };
						courseEligibiltyList.Add(objCourseEligibilty);
					}

				}
			}
			catch (Exception ex)
			{
				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing BindCourseEligibiltyObject in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);

			}
			return courseEligibiltyList;
		}

		private List<CourseMasterProperty> BindCourseMasterObject( DataTable dataTable )
		{
			var courseMasterObjectList = new List<CourseMasterProperty>();
			try
			{
				if (dataTable.Rows.Count > 0)
				{
					for (var j = 0; j < dataTable.Rows.Count; j++)
					{

						var objCoursemaster = new CourseMasterProperty
												  {
													  CourseCategoryId =
														  (dataTable.Rows[j]["AjCourseCategoryId"] is DBNull) ? 0 :
																	   Convert.ToInt32(dataTable.Rows[j]["AjCourseCategoryId"]),
													  CourseCategoryName =
														  (dataTable.Rows[j]["AjCourseCategoryName"] is DBNull) ? null :
																	   Convert.ToString(dataTable.Rows[j]["AjCourseCategoryName"]),

													  CourseEligibiltyId =
														   (dataTable.Rows[j]["AjCollegeCourseEligibiltyId"] is DBNull) ? 0 :
																		  Convert.ToInt32(dataTable.Rows[j]["AjCollegeCourseEligibiltyId"]),

													  CourseEligibityName =
															 (dataTable.Rows[j]["AjCollegeCourseEligibiltyName"] is DBNull) ? null :
																		  Convert.ToString(dataTable.Rows[j]["AjCollegeCourseEligibiltyName"]),

													  CourseId =
															(dataTable.Rows[j]["AjCourseId"] is DBNull) ? 0 :
																		   Convert.ToInt32(dataTable.Rows[j]["AjCourseId"]),

													  CourseName =
															(dataTable.Rows[j]["AjCourseName"] is DBNull) ? null :
																		   Convert.ToString(dataTable.Rows[j]["AjCourseName"]),

													  CoursePopularName =
															(dataTable.Rows[j]["AjCoursepopularName"] is DBNull) ? null :
																			Convert.ToString(dataTable.Rows[j]["AjCoursepopularName"]),

													  CourseShortName =
														  (dataTable.Rows[j]["AjCourseShortName"] is DBNull) ? null :
																			Convert.ToString(dataTable.Rows[j]["AjCourseShortName"]),

													  CourseMetaDesc =
														   (dataTable.Rows[j]["AjCourseMetaDesc"] is DBNull) ? null :
																		   Convert.ToString(dataTable.Rows[j]["AjCourseMetaDesc"]),

													  CourseMetaTag =
														  (dataTable.Rows[j]["AjCourseMetaTag"] is DBNull) ? null :
																		 Convert.ToString(dataTable.Rows[j]["AjCourseMetaTag"]),

													  CourseStatus =
															(dataTable.Rows[j]["AjCourseStatus"] is DBNull) ? false :
																		Convert.ToBoolean(dataTable.Rows[j]["AjCourseStatus"]),

													  CourseTitle =
															(dataTable.Rows[j]["AjCourseTitle"] is DBNull) ? null :
																		  Convert.ToString(dataTable.Rows[j]["AjCourseTitle"]),

													  CourseUrl =
															(dataTable.Rows[j]["AjCourseUrl"] is DBNull) ? null :
																		   Convert.ToString(dataTable.Rows[j]["AjCourseUrl"]),

													  CourseDesc =
													  (dataTable.Rows[j]["AjCourseDesc"] is DBNull) ? null :
																			Convert.ToString(dataTable.Rows[j]["AjCourseDesc"]),

													  CourseImage =
																(dataTable.Rows[j]["AjCourseImage"] is DBNull) ? null :
																			Convert.ToString(dataTable.Rows[j]["AjCourseImage"]),
                                                      HelpLineNo =
                                                          (dataTable.Rows[j]["AjHelpLineNo"] is DBNull) ? null :
                                                                         Convert.ToString(dataTable.Rows[j]["AjHelpLineNo"]) ,
                                                                          IsBookSeatVisible =
															(dataTable.Rows[j]["AjIsBookSeatVisible"] is DBNull) ? false :
																		Convert.ToBoolean(dataTable.Rows[j]["AjIsBookSeatVisible"])
                                                                        
												  };
						courseMasterObjectList.Add(objCoursemaster);
					}
				}
			}
			catch (Exception ex)
			{
				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing BindCourseMasterObject in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);

			}
			return courseMasterObjectList;
		}

		private List<CourseMasterProperty> BindCourseHome( DataTable dataTable )
		{
			var courseMasterObjectList = new List<CourseMasterProperty>();
			try
			{
				if (dataTable.Rows.Count > 0)
				{
					for (var j = 0; j < dataTable.Rows.Count; j++)
					{

						var objCoursemaster = new CourseMasterProperty
						{

							CourseId = (dataTable.Rows[j]["AjCourseId"] is DBNull) ? 0 :
																	   Convert.ToInt32(dataTable.Rows[j]["AjCourseId"]),
							CourseName =
								(dataTable.Rows[j]["AjCourseName"] is DBNull) ? null :
																	  Convert.ToString(dataTable.Rows[j]["AjCourseName"])

						};
						courseMasterObjectList.Add(objCoursemaster);
					}
				}
			}
			catch (Exception ex)
			{
				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing BindCourseMasterObject in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);

			}
			return courseMasterObjectList;
		}


		public List<CoursePaymentMasterProperty> BindCoursePaymentMasterObject( DataTable dataTable )
		{
			var coursePaymentMasterList = new List<CoursePaymentMasterProperty>();

			try
			{
				if (dataTable.Rows.Count > 0)
				{
					for (var j = 0; j < dataTable.Rows.Count; j++)
					{
						var objcoursecategory = new CoursePaymentMasterProperty
						{

							PaymentCourseId = (dataTable.Rows[j]["AjPaymentCourseId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjPaymentCourseId"]),
							CourseId = (dataTable.Rows[j]["AjCourseId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjCourseId"]),

							OnlinePaymentAmount =
													   (dataTable.Rows[j]["AjPaymentAmount"] is DBNull) ? null :
																							 Convert.ToString(dataTable.Rows[j]["AjPaymentAmount"]),
							OfflinePaymentAmount =
													   (dataTable.Rows[j]["AjOfflinePaymentAmount"] is DBNull) ? null :
																							 Convert.ToString(dataTable.Rows[j]["AjOfflinePaymentAmount"]),
							CourseName = (dataTable.Rows[j]["AjCourseName"] is DBNull) ? null :
																							 Convert.ToString(dataTable.Rows[j]["AjCourseName"]),
							courseCategoryId = (dataTable.Rows[j]["AjCourseCategoryId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjCourseCategoryId"]),

						};
						coursePaymentMasterList.Add(objcoursecategory);
					}
				}

			}
			catch (Exception ex)
			{

				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing BindCoursePaymentMasterObject in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);
			}
			return coursePaymentMasterList;
		}


		#endregion




		#region Method defined  for the Course Payment Master

		public override int InsertCoursePaymentMasterDetails( int CourseId, string OnlinePaymentAmount, string OfflinePaymentAmount, out string errMsg, int createdBy )
		{

			_objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
			errMsg = string.Empty;
			try
			{
				_objDataWrapper.AddParameter("@CourseId", CourseId);
				_objDataWrapper.AddParameter("@OnlinePaymentAmount", OnlinePaymentAmount);
				_objDataWrapper.AddParameter("@OfflinePaymentAmount", OfflinePaymentAmount);
				_objDataWrapper.AddParameter("@createdBy", createdBy);
				var objErrmsg =
					(SqlParameter)
					(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
				_i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCoursePaymentMaster");
				if (objErrmsg != null && objErrmsg.Value != null)
					errMsg = Convert.ToString(objErrmsg.Value);
			}
			catch (Exception ex)
			{
				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing InsertCoursePayment in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);

			}





			return _i;
		}

		public override int UpdateCoursePaymentMasterDetails( int PaymentCourseId, int CourseId, string OnlinePaymentAmount, string OfflinePaymentAmount, out string errMsg, int createdBy )
		{
			_objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
			errMsg = string.Empty;
			try
			{
				_objDataWrapper.AddParameter("@PaymentCourseId", PaymentCourseId);
				_objDataWrapper.AddParameter("@CourseId", CourseId);
				_objDataWrapper.AddParameter("@OnlinePaymentAmount", OnlinePaymentAmount);
				_objDataWrapper.AddParameter("@OfflinePaymentAmount", OfflinePaymentAmount);
				_objDataWrapper.AddParameter("@createdBy", createdBy);

				var objErrmsg =
					(SqlParameter)
					(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
				_i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateCoursePaymentMaster");
				if (objErrmsg != null && objErrmsg.Value != null)
					errMsg = Convert.ToString(objErrmsg.Value);
			}
			catch (Exception ex)
			{
				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing UpdateCoursePayment in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);

			}


			return _i;
		}

		public override List<CoursePaymentMasterProperty> GetCoursePaymentMasterList()
		{
			var CoursePaymentMasterList = new List<CoursePaymentMasterProperty>();
			_objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
			_dataSet = new DataSet();
			try
			{
				_dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCoursePaymentMasterList");
				CoursePaymentMasterList = BindCoursePaymentMasterObject(_dataSet.Tables[0]);
			}
			catch (Exception ex)
			{
				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing GetAllCoursePaymentList in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);

			}
			return CoursePaymentMasterList;

		}

		public override List<CoursePaymentMasterProperty> GetCoursePaymentById( int PaymentCourseId )
		{
			var coursePaymentMasterList = new List<CoursePaymentMasterProperty>();
			_objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
			_dataSet = new DataSet();
			try
			{
				_objDataWrapper.AddParameter("@PaymentCourseId", PaymentCourseId);
				_dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetCoursePaymentMasterList");
				coursePaymentMasterList = BindCoursePaymentMasterObject(_dataSet.Tables[0]);
			}
			catch (Exception ex)
			{

				var err = ex.Message;
				if (ex.InnerException != null)
				{
					err = err + " :: Inner Exception :- " + ex.ToString();
				}
				const string addInfo = "Error while executing GetCoursePaymentById in Course.cs  :: -> ";
				var objPub = new ClsExceptionPublisher();
				objPub.Publish(err, addInfo);
			}
			return coursePaymentMasterList;

		}


		#endregion Method defined  for the Course Payment Master

	}
}

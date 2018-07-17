using System;
using System.Collections.Generic;
using System.Data;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.DAL;
using System.Data.SqlClient;

namespace IryTech.AdmissionJankari.BL
{
   public  class Exam:ExamProvider 
    {
        private DbWrapper _objDataWrapper;
        private int _i;
        private DataSet _dataSet;

        public override int InsertExamDetails(ExamProperty objExamProperty, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = string.Empty;
           try
           {
               _objDataWrapper.AddParameter("@ExamName", objExamProperty.ExamName );
               _objDataWrapper.AddParameter("@CourseId", objExamProperty.CourseId);
               _objDataWrapper.AddParameter("@ExamFullName", objExamProperty.ExamFullName);
               _objDataWrapper.AddParameter("@ExamPopularName", objExamProperty.ExamPopularName);
               _objDataWrapper.AddParameter("@ExamLogo", objExamProperty.ExamLogo);
               _objDataWrapper.AddParameter("@ExamDesc", objExamProperty.ExamDesc);
               _objDataWrapper.AddParameter("@ExamWebsite", objExamProperty.ExamWebSite);
               _objDataWrapper.AddParameter("@ExamEligiblityCriteria", objExamProperty.ExamEligiblityCriteria);
               _objDataWrapper.AddParameter("@ExamStatus", objExamProperty.ExamStatus);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);
               var  objErrMsg =
                   (SqlParameter)
                   (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateExam");
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
               const string addInfo = "Error while executing InsertExamDetails in Exam.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
              
           }
            return _i;
       }

        public override int UpdateExamDetails(ExamProperty objExamProperty, int modifiedBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = string.Empty;
           try
           {
               _objDataWrapper.AddParameter("@ExamId", objExamProperty.ExamId);
               _objDataWrapper.AddParameter("@ExamName", objExamProperty.ExamName );
               _objDataWrapper.AddParameter("@CourseId", objExamProperty.CourseId);
               _objDataWrapper.AddParameter("@ExamFullName", objExamProperty.ExamFullName);
               _objDataWrapper.AddParameter("@ExamPopularName", objExamProperty.ExamPopularName);
               _objDataWrapper.AddParameter("@ExamLogo", objExamProperty.ExamLogo);
               _objDataWrapper.AddParameter("@ExamDesc", objExamProperty.ExamDesc);
               _objDataWrapper.AddParameter("@ExamWebsite", objExamProperty.ExamWebSite);
               _objDataWrapper.AddParameter("@ExamEligiblityCriteria", objExamProperty.ExamEligiblityCriteria);
               _objDataWrapper.AddParameter("@ExamStatus", objExamProperty.ExamStatus);
               _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
               var objErrMsg =
                   (SqlParameter)
                   (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateExam");
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
               const string addInfo = "Error while executing UpdateExamDetails in Exam.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);

           }
           return _i;
       }

       public override List<ExamProperty> GetAllExamList()
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var examObjectList = new List<ExamProperty>();
          
           try
           {
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetExamList");
               examObjectList = BindExamListObject(_dataSet.Tables[0]);

           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing UpdateCountry in Exam.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);

           }
           return examObjectList;
       }

       public override List<ExamProperty> GetExamListById(int examId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var examObjectList = new List<ExamProperty>();

           try
           {
               _objDataWrapper.AddParameter("@ExamId", examId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetExamList");
               examObjectList = BindExamListObject(_dataSet.Tables[0]);

           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing UpdateCountry in Exam.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);

           }
           return examObjectList;
       }

       public override List<ExamProperty> GetExamListByCourseId(int courseId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var examObjectList = new List<ExamProperty>();

           try
           {
               _objDataWrapper.AddParameter("@CourseId", courseId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetExamList");
               examObjectList = BindExamListObject(_dataSet.Tables[0]);

           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing UpdateCountry in Exam.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);

           }
           return examObjectList;

       }

       public override List<ExamProperty> GetExamListByName(string examName)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var examObjectList = new List<ExamProperty>();

           try
           {
               _objDataWrapper.AddParameter("@ExamName", examName);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetExamList");
               examObjectList = BindExamListObject(_dataSet.Tables[0]);

           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing UpdateCountry in Exam.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);

           }
           return  examObjectList;

       }
       public override List<ExamProperty> GetMostViewdExamByCourse(int courseId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet = new DataSet();
           var examObjectList = new List<ExamProperty>();

           try
           {
               _objDataWrapper.AddParameter("@CourseId", courseId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetMostViewdExam");
               examObjectList = BindExamListObject(_dataSet.Tables[0]);

           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetMostViewdExamByCourse in Exam.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);

           }
           return examObjectList;
       }
       public override List<ExamProperty> GetUpComingExamList(int courseId, DateTime upComingDate)
       {
           _dataSet = new DataSet();
           var examObjectList = new List<ExamProperty>();

           try
           {
               _objDataWrapper.AddParameter("@CourseId", courseId);
               _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetMostViewdExam");
               examObjectList = BindExamListObject(_dataSet.Tables[0]);

           }
           catch (Exception ex)
           {
               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetMostViewdExamByCourse in Exam.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);

           }
           return examObjectList;
       }
       public override int InsertExamFormDetails(ExamFormProperty objExamFormProperty, int createdBy, out string errmsg)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = string.Empty;
           try
           {
               _objDataWrapper.AddParameter("@ExamId", objExamFormProperty.ExamId);
               _objDataWrapper.AddParameter("@ExamFormUrl", objExamFormProperty.ExamFormUrl);
               _objDataWrapper.AddParameter("@ExamFormTitle", objExamFormProperty.ExamFormTitle);
               _objDataWrapper.AddParameter("@ExamFormMetaDesc", objExamFormProperty.ExamFormMetaDesc);
               _objDataWrapper.AddParameter("@ExamFormMetaTag", objExamFormProperty.ExamFormKeywords);
               _objDataWrapper.AddParameter("@ExamFormSubject", objExamFormProperty.ExamFormSubject);
               _objDataWrapper.AddParameter("@ExamFormYear", objExamFormProperty.ExamFormYear);
               _objDataWrapper.AddParameter("@ExamFormWebsite", objExamFormProperty.ExamFormWebsite);
               _objDataWrapper.AddParameter("@ExamFormSalesStartDate", objExamFormProperty.ExamFormSaleStartDate);
               _objDataWrapper.AddParameter("@ExamFormSalesStartDateReamrk", objExamFormProperty.ExamFromSaleStartDateRemark);
               _objDataWrapper.AddParameter("@ExamFormSalesEndDate", objExamFormProperty.ExamFormSaleEndDate);
               _objDataWrapper.AddParameter("@ExamFormSalesEndDateReamrk", objExamFormProperty.ExamFormSaleEndDateRemark);
               _objDataWrapper.AddParameter("@ExamFormSubmitDate", objExamFormProperty.ExamFormSubmitDate);
               _objDataWrapper.AddParameter("@ExamFormSubmitDateReamrk", objExamFormProperty.ExamFormSubmitDateRemark);
               _objDataWrapper.AddParameter("@ExamFormResultDate", objExamFormProperty.ExamFormResultDate);
               _objDataWrapper.AddParameter("@ExamFormResultDateRemark", objExamFormProperty.ExamFormResultDateReamrk);
               _objDataWrapper.AddParameter("@ExamFormResultWebsite", objExamFormProperty.ExamFormResultWebsite);
               _objDataWrapper.AddParameter("@ExamFormPrice", objExamFormProperty.ExamFormPrice);
               _objDataWrapper.AddParameter("@ExamFormStore", objExamFormProperty.ExamFormStore);
               _objDataWrapper.AddParameter("@ExamFromCenter", objExamFormProperty.ExamFormCenter);
               _objDataWrapper.AddParameter("@ExamFromDD", objExamFormProperty.ExamFormDd);
               _objDataWrapper.AddParameter("@ExamFromSyllabus", objExamFormProperty.ExamFormSyllabus);
               _objDataWrapper.AddParameter("@CreatedBy", createdBy);
               _objDataWrapper.AddParameter("@AjExamFormStatus", objExamFormProperty.ExamFormStatus);

               var  objErrmsg =
                   (SqlParameter)
                   (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateExamFormDetails");
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
               const string addInfo = "Error while executing UpdateCountry in Exam.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;

       }

       public override int UpdateExamFormDetails(ExamFormProperty objExamFormProperty, int modifiedBy, out string errmsg)
       {

           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           errmsg = string.Empty;
           try
           {
               _objDataWrapper.AddParameter("@EaxmFormId", objExamFormProperty.ExamFormId);
               _objDataWrapper.AddParameter("@ExamId", objExamFormProperty.ExamId);
               _objDataWrapper.AddParameter("@ExamFormUrl", objExamFormProperty.ExamFormUrl);
               _objDataWrapper.AddParameter("@ExamFormTitle", objExamFormProperty.ExamFormTitle);
               _objDataWrapper.AddParameter("@ExamFormMetaDesc", objExamFormProperty.ExamFormMetaDesc);
               _objDataWrapper.AddParameter("@ExamFormMetaTag", objExamFormProperty.ExamFormKeywords);
               _objDataWrapper.AddParameter("@ExamFormSubject", objExamFormProperty.ExamFormSubject);
               _objDataWrapper.AddParameter("@ExamFormYear", objExamFormProperty.ExamFormYear);
               _objDataWrapper.AddParameter("@ExamFormWebsite", objExamFormProperty.ExamFormWebsite);
               _objDataWrapper.AddParameter("@ExamFormSalesStartDate", objExamFormProperty.ExamFormSaleStartDate);
               _objDataWrapper.AddParameter("@ExamFormSalesStartDateReamrk", objExamFormProperty.ExamFromSaleStartDateRemark);
               _objDataWrapper.AddParameter("@ExamFormSalesEndDate", objExamFormProperty.ExamFormSaleEndDate);
               _objDataWrapper.AddParameter("@ExamFormSalesEndDateReamrk", objExamFormProperty.ExamFormSaleEndDateRemark);
               _objDataWrapper.AddParameter("@ExamFormSubmitDate", objExamFormProperty.ExamFormSubmitDate);
               _objDataWrapper.AddParameter("@ExamFormSubmitDateReamrk", objExamFormProperty.ExamFormSubmitDateRemark);
               _objDataWrapper.AddParameter("@ExamFormResultDate", objExamFormProperty.ExamFormResultDate);
               _objDataWrapper.AddParameter("@ExamFormResultDateRemark", objExamFormProperty.ExamFormResultDateReamrk);
               _objDataWrapper.AddParameter("@ExamFormResultWebsite", objExamFormProperty.ExamFormResultWebsite);
               _objDataWrapper.AddParameter("@ExamFormPrice", objExamFormProperty.ExamFormPrice);
               _objDataWrapper.AddParameter("@ExamFormStore", objExamFormProperty.ExamFormStore);
               _objDataWrapper.AddParameter("@ExamFromCenter", objExamFormProperty.ExamFormCenter);
               _objDataWrapper.AddParameter("@ExamFromDD", objExamFormProperty.ExamFormDd);
               _objDataWrapper.AddParameter("@ExamFromSyllabus", objExamFormProperty.ExamFormSyllabus);
               _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
               _objDataWrapper.AddParameter("@AjExamFormStatus", objExamFormProperty.ExamFormStatus);

               var objErrmsg =
                   (SqlParameter)
                   (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateExamFormDetails");
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
               const string addInfo = "Error while executing UpdateExamFormDetails in Exam.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return _i;

       }

       public override List<ExamFormProperty> GetAllExamFormDetails()
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet=new DataSet();
            var examFormPropertyList = new List<ExamFormProperty>();

           try
           {
               
               _dataSet=_objDataWrapper.ExecuteDataSet("Aj_Proc_GetExamFormList");
               examFormPropertyList=BindExamFormListObject(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {

               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing UpdateCountry in Exam.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return examFormPropertyList;
       }

       public override List<ExamFormProperty> GetExamFormDeatilsById(int examFormId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet=new DataSet();
            var examFormPropertyList = new List<ExamFormProperty>();

           try
           {
               
               _objDataWrapper.AddParameter("@ExamFormId",examFormId);
               _dataSet=_objDataWrapper.ExecuteDataSet("Aj_Proc_GetExamFormList");
               examFormPropertyList=BindExamFormListObject(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {

               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetExamFormDeatilsById in Exam.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return examFormPropertyList;
       }

       public override List<ExamFormProperty> GetExamFormDetailsByExamId(int examId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet=new DataSet();
            var examFormPropertyList = new List<ExamFormProperty>();

           try
           {
               _objDataWrapper.AddParameter("@ExamId",examId);
               _dataSet=_objDataWrapper.ExecuteDataSet("Aj_Proc_GetExamFormList");
               examFormPropertyList=BindExamFormListObject(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {

               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetExamFormDetailsByExamId in Exam.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return examFormPropertyList;
       }

       public override List<ExamFormProperty> GetExamFormDetailByCourseId(int courseId)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet=new DataSet();
            var examFormPropertyList = new List<ExamFormProperty>();

           try
           {
                 _objDataWrapper.AddParameter("@CourseId",courseId);
               _dataSet=_objDataWrapper.ExecuteDataSet("Aj_Proc_GetExamFormList");
               examFormPropertyList=BindExamFormListObject(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {

               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetExamFormDetailByCourseId in Exam.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return examFormPropertyList;
       }

       public override List<ExamFormProperty> GetExamFormDetailsByExamSubject(string subjectName)
       {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet=new DataSet();
            var examFormPropertyList = new List<ExamFormProperty>();

           try
           {
                _objDataWrapper.AddParameter("@SubjectName",subjectName);
               _dataSet=_objDataWrapper.ExecuteDataSet("Aj_Proc_GetExamFormList");
               examFormPropertyList=BindExamFormListObject(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {

               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetExamFormDetailsByExamSubject in Exam.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return examFormPropertyList;
       }

       public override List<ExamFormProperty> GetExamFormDetailsByExamSubjectCourseId(int courseId, string subjectName)
       {

           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           _dataSet=new DataSet();
            var examFormPropertyList = new List<ExamFormProperty>();

           try
           {
               _objDataWrapper.AddParameter("@CourseId",courseId);
               _objDataWrapper.AddParameter("@SubjectName",subjectName);
               _dataSet=_objDataWrapper.ExecuteDataSet("Aj_Proc_GetExamFormList");
               examFormPropertyList=BindExamFormListObject(_dataSet.Tables[0]);
           }
           catch (Exception ex)
           {

               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing GetExamFormDetailsByExamSubjectCourseId in Exam.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return examFormPropertyList;
       }
      

       #region Local Method Definition to bind the object list
       private List<ExamProperty> BindExamListObject(DataTable dataTable)
       {
           var examObjectList = new List<ExamProperty>();

           try
           {
               if(dataTable.Rows.Count>0)
               {
                   for(var j=0;j<dataTable.Rows.Count;j++)
                   {
                       var objExamProperty = new ExamProperty
                                                 {
                                                     ExamId = Convert.ToInt32(dataTable.Rows[j]["AjExamId"]),
                                                     CourseName = Convert.ToString(dataTable.Rows[j]["AjCourseName"]),
                                                     ExamDesc = Convert.ToString(dataTable.Rows[j]["AjExamDesc"]),
                                                     ExamEligiblityCriteria =
                                                         Convert.ToString(dataTable.Rows[j]["AjExamEligiblityCriteria"]),
                                                     ExamFullName = Convert.ToString(dataTable.Rows[j]["AjExamFullName"]),
                                                     ExamLogo = Convert.ToString(dataTable.Rows[j]["AjExamLogo"]),
                                                     ExamName = Convert.ToString(dataTable.Rows[j]["AjExamName"]),
                                                     ExamPopularName =
                                                         Convert.ToString(dataTable.Rows[j]["AjExamPopularName"]),
                                                     ExamWebSite = Convert.ToString(dataTable.Rows[j]["AjExamWebsite"]),
                                                     CourseId = Convert.ToInt32(dataTable.Rows[j]["AjCourseId"]),
                                                     ExamStatus = Convert.ToBoolean(dataTable.Rows[j]["AjExamStatus"]),
                                                     HelpLineNumber = Convert.ToString(dataTable.Columns.Contains("AjHelpLineNo") ? (dataTable.Rows[j]["AjHelpLineNo"].ToString() != "" ? dataTable.Rows[j]["AjHelpLineNo"] : null) : null),
                                                 };
                       examObjectList.Add(objExamProperty);
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
               const string addInfo = "Error while executing BindExamListObject in Exam.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return examObjectList;
       }


       private List<ExamFormProperty> BindExamFormListObject(DataTable dataTable)
       {
           var examFormPropertyList = new List<ExamFormProperty>();
           try
           {

               if(dataTable.Rows.Count>0)
               {
                   for(var j=0;j<dataTable.Rows.Count;j++)
                   {
                           var objExamFormProperty = new ExamFormProperty{
                           CourseId = Convert.ToInt32(dataTable.Rows[j]["AjCourseId"]),
                           CourseName = Convert.ToString(dataTable.Rows[j]["AjCourseName"]),
                           ExamFormCenter = Convert.ToString(dataTable.Rows[j]["AjExamFormCenter"]),
                           ExamFormDd = Convert.ToString(dataTable.Rows[j]["AjExamFormDD"]),
                           ExamFormId = Convert.ToInt32(dataTable.Rows[j]["AjExamFormId"]),
                           ExamFormKeywords = Convert.ToString(dataTable.Rows[j]["AjExamFormMetaTag"]),
                           ExamFormMetaDesc = Convert.ToString(dataTable.Rows[j]["AjExamFormMetaDesc"]),
                           ExamFormPrice = Convert.ToString(dataTable.Rows[j]["AjExamFormPrice"]),
                           ExamFormResultDate = Convert.ToString(dataTable.Rows[j]["AjExamFormResultDate"]),
                           ExamFormResultDateReamrk = Convert.ToString(dataTable.Rows[j]["AjExamFormResultDateRemark"]),
                           ExamFormResultWebsite = Convert.ToString(dataTable.Rows[j]["AjExamFormResultWebsite"]),
                           ExamFormSaleEndDate = Convert.ToString(dataTable.Rows[j]["AjExamFormSaleEndDate"]),
                           ExamFormSaleEndDateRemark = Convert.ToString(dataTable.Rows[j]["AjExamFormSaleEndDateRemark"]),
                           ExamFormSaleStartDate = Convert.ToString(dataTable.Rows[j]["AjExamFormSaleStartDate"]),
                           ExamFormStatus = Convert.ToBoolean(dataTable.Rows[j]["AjExamFormStatus"]),
                           ExamFormStore = Convert.ToString(dataTable.Rows[j]["AjExamFormStore"]),
                           ExamFormSubject = Convert.ToString(dataTable.Rows[j]["AjExamFormSubject"]),
                           ExamFormSubmitDate = Convert.ToString(dataTable.Rows[j]["AjExamFormSubmitDate"]),
                           ExamFormSubmitDateRemark = Convert.ToString(dataTable.Rows[j]["AjExamFormSubmitDateRemark"]),
                           ExamFormSyllabus = Convert.ToString(dataTable.Rows[j]["AjExamFormSyllabus"]),
                           ExamFormTitle = Convert.ToString(dataTable.Rows[j]["AjExamFormTitle"]),
                           ExamFormUrl = Convert.ToString(dataTable.Rows[j]["AjExamFormUrl"]),
                           ExamFormWebsite = Convert.ToString(dataTable.Rows[j]["AjExamFormWebsite"]),
                           ExamFormYear = Convert.ToString(dataTable.Rows[j]["AjExamFormYear"]),
                           ExamFromSaleStartDateRemark = Convert.ToString(dataTable.Rows[j]["AjExamFormSaleStartDateRemark"]),
                           ExamId = Convert.ToInt32(dataTable.Rows[j]["AjExamId"]),
                           ExamName = Convert.ToString(dataTable.Rows[j]["AjExamName"])
                       };
                       examFormPropertyList.Add(objExamFormProperty);

                   }
               }
           }
           catch (Exception ex )
           {

               var err = ex.Message;
               if (ex.InnerException != null)
               {
                   err = err + " :: Inner Exception :- " + ex.ToString();
               }
               const string addInfo = "Error while executing BindExamFormListObject in Exam.cs  :: -> ";
               var objPub = new ClsExceptionPublisher();
               objPub.Publish(err, addInfo);
           }
           return examFormPropertyList;
       }
       #endregion



      
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.DAL;


namespace IryTech.AdmissionJankari.BL
{
    public class Profile : ProfileProvider
    {
        private DbWrapper _objDataWrapper;
        private DataSet _dataSet;
        private int _i;


        public override int InsertStudentGraduationDetails(StudentGraduationproperty objobjStudentGraduationproperty, int userId,
                                            out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            try
            {
                _objDataWrapper.AddParameter("@StudentGrdCollegeName", objobjStudentGraduationproperty.StudentGrdCollegeName);
                _objDataWrapper.AddParameter("@StudentGrdSpecialization", objobjStudentGraduationproperty.StudentGrdSpecialization);
                _objDataWrapper.AddParameter("@StudentGrdYOP", objobjStudentGraduationproperty.StudentGrdYOP);
                _objDataWrapper.AddParameter("@StudentGrdPer", objobjStudentGraduationproperty.StudentGrdPer);
                _objDataWrapper.AddParameter("@StudentGrdCGPA", objobjStudentGraduationproperty.StudentGrdCGPA);


                _objDataWrapper.AddParameter("@CreatedBy", userId);
                var errMsg =
                     (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateGraduationDetails");
                if (errMsg != null && errMsg.Value != null)
                    errmsg = Convert.ToString(errMsg.Value);


            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertStudentGraduationDetails in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override int UpdateStudentGraduationDetails(StudentGraduationproperty objStudentGraduationproperty, int userId,
                                           out string errmsg){
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            try
            {
                _objDataWrapper.AddParameter("@StudentGrdScorecardId", objStudentGraduationproperty.StudentGrdScorecardId);

                _objDataWrapper.AddParameter("@StudentGrdCollegeName", objStudentGraduationproperty.StudentGrdCollegeName);
                _objDataWrapper.AddParameter("@StudentGrdSpecialization", objStudentGraduationproperty.StudentGrdSpecialization);
                _objDataWrapper.AddParameter("@StudentGrdYOP", objStudentGraduationproperty.StudentGrdYOP);
                _objDataWrapper.AddParameter("@StudentGrdPer", objStudentGraduationproperty.StudentGrdPer);
                _objDataWrapper.AddParameter("@StudentGrdCGPA", objStudentGraduationproperty.StudentGrdCGPA);


                _objDataWrapper.AddParameter("@CreatedBy", userId);
                var errMsg =
                     (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateGraduationDetails");
                if (errMsg != null && errMsg.Value != null)
                    errmsg = Convert.ToString(errMsg.Value);


            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateStudentGraduationDetails in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override int InsertStudentDiplomaDetails(StudentDiplomaProperty objStudentDiplomaProperty, int userId,
                                              out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            try
            {
                _objDataWrapper.AddParameter("@StudentDicCollegeName", objStudentDiplomaProperty.StudentDicCollegeName);
                _objDataWrapper.AddParameter("@StudentDicYOP", objStudentDiplomaProperty.StudentDicYOP);
                _objDataWrapper.AddParameter("@StudentDicCGPA", objStudentDiplomaProperty.StudentDicCGPA);
                _objDataWrapper.AddParameter("@StudentDicPercentage", objStudentDiplomaProperty.StudentDicPercentage);
                _objDataWrapper.AddParameter("@StudentDicCourse", objStudentDiplomaProperty.StudentDicCourse);
                

                _objDataWrapper.AddParameter("@CreatedBy", userId);
                var errMsg =
                     (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateDiplomaDetails");
                if (errMsg != null && errMsg.Value != null)
                    errmsg = Convert.ToString(errMsg.Value);


            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertStudentDiplomaDetails in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override int UpdateStudentDiplomaDetails(StudentDiplomaProperty objStudentDiplomaProperty, int userId,
                                           out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            try
            {
                _objDataWrapper.AddParameter("@StudentDicScoreCardId", objStudentDiplomaProperty.StudentDicScoreCardId);
             
                _objDataWrapper.AddParameter("@StudentDicCollegeName", objStudentDiplomaProperty.StudentDicCollegeName);
                _objDataWrapper.AddParameter("@StudentDicYOP", objStudentDiplomaProperty.StudentDicYOP);
                _objDataWrapper.AddParameter("@StudentDicCGPA", objStudentDiplomaProperty.StudentDicCGPA);
                _objDataWrapper.AddParameter("@StudentDicPercentage", objStudentDiplomaProperty.StudentDicPercentage);
                _objDataWrapper.AddParameter("@StudentDicCourse", objStudentDiplomaProperty.StudentDicCourse);


                _objDataWrapper.AddParameter("@CreatedBy", userId);
                var errMsg =
                     (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateDiplomaDetails");
                if (errMsg != null && errMsg.Value != null)
                    errmsg = Convert.ToString(errMsg.Value);


            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateStudentDiplomaDetails in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override int InsertStudentInterSchoolDetails(StudentInterSchoolProperty objStudentInterSchoolProperty, int userId,
                                              out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            try
            {
                _objDataWrapper.AddParameter("@SeniorSecondarySchoolName", objStudentInterSchoolProperty.SeniorSecondarySchoolName);
                _objDataWrapper.AddParameter("@SeniorSecondarySchoolPassingYear", objStudentInterSchoolProperty.SeniorSecondarySchoolPassingYear);
                _objDataWrapper.AddParameter("@SeniorSecondarySchoolScoreCGPA", objStudentInterSchoolProperty.SeniorSecondarySchoolScoreCgpa);
                _objDataWrapper.AddParameter("@SeniorSecondarySchoolSpecialization", objStudentInterSchoolProperty.SeniorSecondarySchoolSpecialization);
                _objDataWrapper.AddParameter("@SeniorSecondarySchoolSubjectCombination", objStudentInterSchoolProperty.SeniorSecondarySchoolSubjectCombination);
                _objDataWrapper.AddParameter("@SeniorSecondarySchoolSubjectMarks", objStudentInterSchoolProperty.SeniorSecondarySchoolSubjectMarks);
                _objDataWrapper.AddParameter("@SeniorSecondarySchoolSubjectPercent", objStudentInterSchoolProperty.SeniorSecondarySchoolSubjectPercent);
                _objDataWrapper.AddParameter("@BoardId", objStudentInterSchoolProperty.BoardId);
                _objDataWrapper.AddParameter("@StudyModeId", objStudentInterSchoolProperty.StudyModeId);

                _objDataWrapper.AddParameter("@CreatedBy", userId);
                var errMsg =
                     (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateStudentInterMediateDetails");
                if (errMsg != null && errMsg.Value != null)
                    errmsg = Convert.ToString(errMsg.Value);


            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertStudentInterSchoolDetails in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override int UpdateStudentInterSchoolDetails(StudentInterSchoolProperty objStudentInterSchoolProperty, int userId,
                                           out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            try
            {
                _objDataWrapper.AddParameter("@SeniorSecondaryScoreCardId", objStudentInterSchoolProperty.SeniorSecondaryScoreCardId);
                _objDataWrapper.AddParameter("@SeniorSecondarySchoolName", objStudentInterSchoolProperty.SeniorSecondarySchoolName);
                _objDataWrapper.AddParameter("@SeniorSecondarySchoolPassingYear", objStudentInterSchoolProperty.SeniorSecondarySchoolPassingYear);
                _objDataWrapper.AddParameter("@SeniorSecondarySchoolScoreCGPA", objStudentInterSchoolProperty.SeniorSecondarySchoolScoreCgpa);
                _objDataWrapper.AddParameter("@SeniorSecondarySchoolSpecialization", objStudentInterSchoolProperty.SeniorSecondarySchoolSpecialization);
                _objDataWrapper.AddParameter("@SeniorSecondarySchoolSubjectCombination", objStudentInterSchoolProperty.SeniorSecondarySchoolSubjectCombination);
                _objDataWrapper.AddParameter("@SeniorSecondarySchoolSubjectMarks", objStudentInterSchoolProperty.SeniorSecondarySchoolSubjectMarks);
                _objDataWrapper.AddParameter("@SeniorSecondarySchoolSubjectPercent", objStudentInterSchoolProperty.SeniorSecondarySchoolSubjectPercent);
                _objDataWrapper.AddParameter("@BoardId", objStudentInterSchoolProperty.BoardId);
                _objDataWrapper.AddParameter("@StudyModeId", objStudentInterSchoolProperty.StudyModeId);

                _objDataWrapper.AddParameter("@CreatedBy", userId);
                var errMsg =
                     (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateStudentInterMediateDetails");
                if (errMsg != null && errMsg.Value != null)
                    errmsg = Convert.ToString(errMsg.Value);



            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateStudentInterSchoolDetails in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override int InsertStudentHighSchoolDetails(StudentHighSchoolProperty objStudentHighSchoolProperty, int userId, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            try
            {
                _objDataWrapper.AddParameter("@HigherSecondarySchoolName", objStudentHighSchoolProperty.HigherSecondarySchoolName);
                _objDataWrapper.AddParameter("@HigherSecondarySchoolPassingYear", objStudentHighSchoolProperty.HigherSecondarySchoolPassingYear);
                _objDataWrapper.AddParameter("@HigherSecondarySchoolScoreCGPA", objStudentHighSchoolProperty.HigherSecondarySchoolScoreCGPA);
                _objDataWrapper.AddParameter("@BoardId", objStudentHighSchoolProperty.BoardId);
                _objDataWrapper.AddParameter("@StudyModeId", objStudentHighSchoolProperty.StudyModeId);

                _objDataWrapper.AddParameter("@CreatedBy", userId);
                var errMsg =
                     (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateStudentHighSchoolDetails");
                if (errMsg != null && errMsg.Value != null)
                    errmsg = Convert.ToString(errMsg.Value);


            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUpdateStudentHighSchoolDetails in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

         public override int UpdateStudentHighSchoolDetails(StudentHighSchoolProperty objStudentHighSchoolProperty, int userId, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            try
            {
                _objDataWrapper.AddParameter("@HigherSecondarySchoolName", objStudentHighSchoolProperty.HigherSecondarySchoolName);
                _objDataWrapper.AddParameter("@HigherSecondaryScoreCardId", objStudentHighSchoolProperty.HigherSecondaryScoreCardId);
                _objDataWrapper.AddParameter("@HigherSecondarySchoolPassingYear", objStudentHighSchoolProperty.HigherSecondarySchoolPassingYear);
                _objDataWrapper.AddParameter("@HigherSecondarySchoolScoreCGPA", objStudentHighSchoolProperty.HigherSecondarySchoolScoreCGPA);
                _objDataWrapper.AddParameter("@BoardId", objStudentHighSchoolProperty.BoardId);
                _objDataWrapper.AddParameter("@StudyModeId", objStudentHighSchoolProperty.StudyModeId);

                _objDataWrapper.AddParameter("@CreatedBy", userId);
                var errMsg =
                     (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateStudentHighSchoolDetails");
                if (errMsg != null && errMsg.Value != null)
                    errmsg = Convert.ToString(errMsg.Value);


            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUpdateStudentHighSchoolDetails in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }
        public override List<SchoolBoardproperty> GetBoardDetails()
        {
            _dataSet = new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objStudentQueryProperty = new List<SchoolBoardproperty>();
            try
            {
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetBoardDetails");
                objStudentQueryProperty = BindBoardDetails(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing SchoolBoardproperty in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objStudentQueryProperty;
        }
        public List<StudentGraduationproperty> GetGraduationDetails(int userId)
        {

            _dataSet = new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objStudentQueryProperty = new List<StudentGraduationproperty>();

            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);

                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetGraduationDetails");



                objStudentQueryProperty = BindGraduation(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetGraduationDetails in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objStudentQueryProperty;
        }
        public List<StudentDiplomaProperty> GetDiplomaDetails(int userId)
        {

            _dataSet = new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objStudentQueryProperty = new List<StudentDiplomaProperty>();

            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);

                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetDiplomaDetails");



                objStudentQueryProperty = BindDiploma(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetDiplomaDetails in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objStudentQueryProperty;
        }
        public List<StudentInterSchoolProperty> GetInterMediateDetails(int userId)
        {

            _dataSet = new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objStudentQueryProperty = new List<StudentInterSchoolProperty>();

            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);

                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetInterMediateDetails");



                objStudentQueryProperty = BindInterMediateDetails(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetInterMediateDetails in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objStudentQueryProperty;
        }

        public override List<StudentQueryProperty> GetStudentQuery(int userId)
        {
          
            _dataSet = new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objStudentQueryProperty = new List<StudentQueryProperty>();

            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);
               
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetStudentQuery");
               

               
                objStudentQueryProperty = BindStudentQuery(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetStudentQuery in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objStudentQueryProperty;
        }
        public List<StudentHighSchoolProperty> GetStudentHighSchoolDetails(int userId)
        {

            _dataSet = new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objStudentQueryProperty = new List<StudentHighSchoolProperty>();

            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);

                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetStudentHighSchoolDetails");



                objStudentQueryProperty = BindHscDetails(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetStudentHighSchoolDetails in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objStudentQueryProperty;
        }
        public override int StudentInsertExamAppeared(string examName, string rank, int userId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
           
            try
            {
                _objDataWrapper.AddParameter("@ExamName", examName);

                _objDataWrapper.AddParameter("@Rank", rank);

                _objDataWrapper.AddParameter("@CreatedBy", userId);

                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_StudentInsertExamAppeared");
               

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing StudentInsertExamAppeared in College.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }
       public override List<ExamAppearedProperty> GetAllExamAppearedList(int userId)
        {
            _dataSet = new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objExamAppearedProperty = new List<ExamAppearedProperty>();

            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);

                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetExamAppearedList");
                objExamAppearedProperty = BindExamAppearedObject(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllExamAppearedList in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objExamAppearedProperty;
        }
        public override List<CollegePrefered> GetStudentCollegePreference(int userId)
        {
            _dataSet = new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objCollegePreferedList = new List<CollegePrefered>();

            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);

                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetStudentCollegeAppearedList");
                objCollegePreferedList = BindCollegePreference(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetStudentCollegePreference in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objCollegePreferedList;
        }
        public override List<CoursePreffered> GetAllCoursePreferList(int userId)
        {
            _dataSet = new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objCoursePrefferedList = new List<CoursePreffered>();

            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);

                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetStudentCoursePreference");
                objCoursePrefferedList = BindCoursePrefer(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllCoursePreferList in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objCoursePrefferedList;
        }
        public override List<CourseStreamPreffered> GetCourseStreamListPreferedByStudent(int userId)
        {
            _dataSet = new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objCourseStreamPrefferedList = new List<CourseStreamPreffered>();

            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);

                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetCourseStreamListPreferedByStudent");
                objCourseStreamPrefferedList = BindCourseStreamPrefer(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCourseStreamListPreferedByStudent in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objCourseStreamPrefferedList;
        }
        public override List<CityPrefferedProperty> GetCityPreferedByStudent(int userId)
        {
            _dataSet = new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objCityPrefferedPropertyList = new List<CityPrefferedProperty>();

            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);

                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetCityPreferedByStudent");
                objCityPrefferedPropertyList = BindCityPrefer(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {

                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetCityPreferedByStudent in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objCityPrefferedPropertyList;
        }
        private List<ExamAppearedProperty> BindExamAppearedObject(DataTable dataTable)
        {
            var objExamAppearedPropertyList = new List<ExamAppearedProperty>();

            try
            {
                if (dataTable.Rows.Count > 0)
                {

                    for (var j = 0; j < dataTable.Rows.Count; j++)
                    {

                        var objExamAppearedProperty = new ExamAppearedProperty
                                          {
                                              UserId = (dataTable.Rows[j]["AjUserId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjUserId"]),
                                             // ExamId = (dataTable.Rows[j]["AjExamId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjExamId"]),
                                              ExamName = (dataTable.Rows[j]["AjExamName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjExamName"]),

                                              AjStudentExamAppId = (dataTable.Rows[j]["AjStudentExamAppId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjStudentExamAppId"]),
                                              AjExamAppRank = (dataTable.Rows[j]["AjExamAppRank"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjExamAppRank"]),

                                          };
                        objExamAppearedPropertyList.Add(objExamAppearedProperty);

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
                const string addInfo = "Error while executing BindExamAppearedObject in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objExamAppearedPropertyList;
        }
        private List<CollegePrefered> BindCollegePreference(DataTable dataTable)
        {
            var objExamAppearedPropertyList = new List<CollegePrefered>();

            try
            {
                if (dataTable.Rows.Count > 0)
                {

                    for (var j = 0; j < dataTable.Rows.Count; j++)
                    {

                        var objExamAppearedProperty = new CollegePrefered
                        {
                            UserId = (dataTable.Rows[j]["AjUserId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjUserId"]),
                            CollegeBranchCourseId = (dataTable.Rows[j]["AjCollegeBranchCourseId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjCollegeBranchCourseId"]),
                            CollegeBranchId = (dataTable.Rows[j]["AjCollegeBranchId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjCollegeBranchId"]),
                            CollegeName = (dataTable.Rows[j]["AjCollegeBranchName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjCollegeBranchName"]),

                            AjStudentCollegePreferenceId = (dataTable.Rows[j]["AjStudentCollegePreferenceId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjStudentCollegePreferenceId"]),
                            CollegePopularName = (dataTable.Rows[j]["AjCollegeBranchPopularName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjCollegeBranchPopularName"]),
                            AjCollegeBranchAddress = (dataTable.Rows[j]["AjCollegeBranchAddress"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjCollegeBranchAddress"]),
                            AjCollegeBranchWebSite = (dataTable.Rows[j]["AjCollegeBranchWebSite"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjCollegeBranchWebSite"]),

                        };
                        objExamAppearedPropertyList.Add(objExamAppearedProperty);

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
                const string addInfo = "Error while executing BindCollegePreference in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objExamAppearedPropertyList;
        }

        private List<CoursePreffered> BindCoursePrefer(DataTable dataTable)
        {
            var objCoursePrefferedList = new List<CoursePreffered>();

            try
            {
                if (dataTable.Rows.Count > 0)
                {

                    for (var j = 0; j < dataTable.Rows.Count; j++)
                    {

                        var objCoursePreffered = new CoursePreffered
                        {
                            UserId = (dataTable.Rows[j]["AjUserId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjUserId"]),

                            CourseId = (dataTable.Rows[j]["AjCourseId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjCourseId"]),
                            CourseName = (dataTable.Rows[j]["AjCourseName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjCourseName"]),

                            CoursePopularName = (dataTable.Rows[j]["AjCoursepopularName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjCoursepopularName"]),
                            CourseShortName = (dataTable.Rows[j]["AjCourseShortName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjCourseShortName"]),
                           
                        };
                        objCoursePrefferedList.Add(objCoursePreffered);

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
                const string addInfo = "Error while executing BindCoursePrefer in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objCoursePrefferedList;
        }
        private List<CourseStreamPreffered> BindCourseStreamPrefer(DataTable dataTable)
        {
            var objCourseStreamPrefferedList = new List<CourseStreamPreffered>();

            try
            {
                if (dataTable.Rows.Count > 0)
                {

                    for (var j = 0; j < dataTable.Rows.Count; j++)
                    {

                        var courseStreamPreffered = new CourseStreamPreffered
                        {
                            UserId = (dataTable.Rows[j]["AjUserId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjUserId"]),
                            StudentStreamPreferenceId = (dataTable.Rows[j]["AjStudentStreamPreferenceId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjStudentStreamPreferenceId"]),
                            CourseStreamId = (dataTable.Rows[j]["AjCourseStreamId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjCourseStreamId"]),
                            CourseStreamName = (dataTable.Rows[j]["AjCourseStreamName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjCourseStreamName"]),

                            CourseStreamCoreCompanies = (dataTable.Rows[j]["AjCourseStreamCoreCompanies"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjCourseStreamCoreCompanies"]),
                            CourseStreamRelatedIndustry = (dataTable.Rows[j]["AjCourseStreamRelatedIndustry"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjCourseStreamRelatedIndustry"]),
                            
                        };
                        objCourseStreamPrefferedList.Add(courseStreamPreffered);

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
                const string addInfo = "Error while executing BindCourseStreamPrefer in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objCourseStreamPrefferedList;
        }
        private List<CityPrefferedProperty> BindCityPrefer(DataTable dataTable)
        {
            var objCourseStreamPrefferedList = new List<CityPrefferedProperty>();

            try
            {
                if (dataTable.Rows.Count > 0)
                {

                    for (var j = 0; j < dataTable.Rows.Count; j++)
                    {

                        var courseStreamPreffered = new CityPrefferedProperty
                        {
                            UserId = (dataTable.Rows[j]["AjUserId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjUserId"]),
                            CityPreferId = (dataTable.Rows[j]["AjStudentCityPreferenceId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjStudentCityPreferenceId"]),
                            CountryId = (dataTable.Rows[j]["AjCountryId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjCountryId"]),
                            StateId = (dataTable.Rows[j]["AjStateId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjStateId"]),

                            CityId = (dataTable.Rows[j]["AjCityId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjCityId"]),
                            CountryName = (dataTable.Rows[j]["AjCountryName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjCountryName"]),
                            StateName = (dataTable.Rows[j]["AjStateName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjStateName"]),
                            CityName = (dataTable.Rows[j]["AjCityName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjCityName"]),


                        };
                        objCourseStreamPrefferedList.Add(courseStreamPreffered);

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
                const string addInfo = "Error while executing BindCityPrefer in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objCourseStreamPrefferedList;
        }
        private List<StudentQueryProperty> BindStudentQuery(DataTable dataTable)
        {
            var objStudentQueryPropertyList = new List<StudentQueryProperty>();

            try
            {
                if (dataTable.Rows.Count > 0)
                {

                    for (var j = 0; j < dataTable.Rows.Count; j++)
                    {

                        var objStudentQueryProperty = new StudentQueryProperty
                        {
                            UserId = (dataTable.Rows[j]["AjUserId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjUserId"]),
                            CourseId = (dataTable.Rows[j]["AjCourseId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjCourseId"]),
                            CourseName = (dataTable.Rows[j]["AjCourseName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjCourseName"]),
                            StudentQueryId = (dataTable.Rows[j]["AjStudentQueryId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjStudentQueryId"]),
                            Query = (dataTable.Rows[j]["AjStudentQueryText"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjStudentQueryText"]),
                            ReplyStatatus = (dataTable.Rows[j]["AjStudentQueryReplyStatus"] is DBNull) ? false : Convert.ToBoolean(dataTable.Rows[j]["AjStudentQueryReplyStatus"]),
                            QueryAnswer = (dataTable.Rows[j]["AjQueryReply"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjQueryReply"]),
                        };
                        objStudentQueryPropertyList.Add(objStudentQueryProperty);

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
                const string addInfo = "Error while executing BindExamAppearedObject in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objStudentQueryPropertyList;
        }
        private List<StudentHighSchoolProperty> BindHscDetails(DataTable dataTable)
        {
            var objStudentQueryPropertyList = new List<StudentHighSchoolProperty>();

            try
            {
                if (dataTable.Rows.Count > 0)
                {

                    for (var j = 0; j < dataTable.Rows.Count; j++)
                    {

                        var objStudentQueryProperty = new StudentHighSchoolProperty
                        {
                            UserId = (dataTable.Rows[j]["AjUserId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjUserId"]),
                            BoardId = (dataTable.Rows[j]["AjBoardId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjBoardId"]),
                            BoardName = (dataTable.Rows[j]["AjBoardName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjBoardName"]),
                            HigherSecondarySchoolName = (dataTable.Rows[j]["AjHigherSecondarySchoolName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjHigherSecondarySchoolName"]),
                            HigherSecondarySchoolScoreCGPA = (dataTable.Rows[j]["AjHigherSecondarySchoolScoreCGPA"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjHigherSecondarySchoolScoreCGPA"]),
                            HigherSecondaryScoreCardId = (dataTable.Rows[j]["AjHigherSecondaryScoreCardId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjHigherSecondaryScoreCardId"]),
                            HigherSecondarySchoolPassingYear = (dataTable.Rows[j]["AjHigherSecondarySchoolPassingYear"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjHigherSecondarySchoolPassingYear"]),
                            StudyModeId = (dataTable.Rows[j]["AjMasterValueId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjMasterValueId"]),
                            StudyMode = (dataTable.Rows[j]["AjMasterValues"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjMasterValues"]),
                           
                        };
                        objStudentQueryPropertyList.Add(objStudentQueryProperty);

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
                const string addInfo = "Error while executing BindExamAppearedObject in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objStudentQueryPropertyList;
        }
        private List<StudentInterSchoolProperty> BindInterMediateDetails(DataTable dataTable)
        {
            var objStudentQueryPropertyList = new List<StudentInterSchoolProperty>();

            try
            {
                if (dataTable.Rows.Count > 0)
                {

                    for (var j = 0; j < dataTable.Rows.Count; j++)
                    {

                        var objStudentQueryProperty = new StudentInterSchoolProperty
                        {
                            UserId = (dataTable.Rows[j]["AjUserId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjUserId"]),
                            BoardId = (dataTable.Rows[j]["AjBoardId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjBoardId"]),
                            BoardName = (dataTable.Rows[j]["AjBoardName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjBoardName"]),
                            SeniorSecondarySchoolName = (dataTable.Rows[j]["AjSeniorSecondarySchoolName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjSeniorSecondarySchoolName"]),
                            SeniorSecondarySchoolScoreCgpa = (dataTable.Rows[j]["AjSeniorSecondarySchoolScoreCGPA"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjSeniorSecondarySchoolScoreCGPA"]),
                            SeniorSecondaryScoreCardId = (dataTable.Rows[j]["AjSeniorSecondaryScoreCardId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjSeniorSecondaryScoreCardId"]),
                            SeniorSecondarySchoolPassingYear = (dataTable.Rows[j]["AjSeniorSecondarySchoolPassingYear"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjSeniorSecondarySchoolPassingYear"]),
                            SeniorSecondarySchoolSpecialization = (dataTable.Rows[j]["AjSeniorSecondarySchoolSpecialization"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjSeniorSecondarySchoolSpecialization"]),
                            SeniorSecondarySchoolSubjectCombination = (dataTable.Rows[j]["AjSeniorSecondarySchoolSubjectCombination"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjSeniorSecondarySchoolSubjectCombination"]),
                            SeniorSecondarySchoolSubjectMarks = (dataTable.Rows[j]["AjSeniorSecondarySchoolSubjectMarks"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjSeniorSecondarySchoolSubjectMarks"]),
                            SeniorSecondarySchoolSubjectPercent = (dataTable.Rows[j]["AjSeniorSecondarySchoolSubjectPercent"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjSeniorSecondarySchoolSubjectPercent"]),
                            StudyMode = (dataTable.Rows[j]["AjMasterValues"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjMasterValues"]),
                            StudyModeId = (dataTable.Rows[j]["AjMasterValueId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjMasterValueId"]),
                            
                        };
                        objStudentQueryPropertyList.Add(objStudentQueryProperty);

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
                const string addInfo = "Error while executing BindExamAppearedObject in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objStudentQueryPropertyList;
        }

        private List<StudentDiplomaProperty> BindDiploma(DataTable dataTable)
        {
            var objStudentQueryPropertyList = new List<StudentDiplomaProperty>();

            try
            {
                if (dataTable.Rows.Count > 0)
                {

                    for (var j = 0; j < dataTable.Rows.Count; j++)
                    {

                        var objStudentQueryProperty = new StudentDiplomaProperty
                        {
                            UserId = (dataTable.Rows[j]["AjUserId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjUserId"]),

                            StudentDicCollegeName = (dataTable.Rows[j]["AjStudentDicCollegeName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjStudentDicCollegeName"]),
                            StudentDicCGPA = (dataTable.Rows[j]["AjStudentDicCGPA"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjStudentDicCGPA"]),
                            StudentDicScoreCardId = (dataTable.Rows[j]["AjStudentDicScoreCardId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjStudentDicScoreCardId"]),
                            StudentDicCourse = (dataTable.Rows[j]["AjStudentDicCourse"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjStudentDicCourse"]),
                            StudentDicPercentage = (dataTable.Rows[j]["AjStudentDicPercentage"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjStudentDicPercentage"]),
                            StudentDicYOP = (dataTable.Rows[j]["AjStudentDicYOP"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjStudentDicYOP"]),
                           
                        };
                        objStudentQueryPropertyList.Add(objStudentQueryProperty);

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
                const string addInfo = "Error while executing BindExamAppearedObject in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objStudentQueryPropertyList;
        }
        private List<StudentGraduationproperty> BindGraduation(DataTable dataTable)
        {
            var objStudentQueryPropertyList = new List<StudentGraduationproperty>();

            try
            {
                if (dataTable.Rows.Count > 0)
                {

                    for (var j = 0; j < dataTable.Rows.Count; j++)
                    {

                        var objStudentQueryProperty = new StudentGraduationproperty
                        {
                            UserId = (dataTable.Rows[j]["AjUserId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjUserId"]),

                            StudentGrdCollegeName = (dataTable.Rows[j]["AjStudentGrdCollegeName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjStudentGrdCollegeName"]),
                            StudentGrdCGPA = (dataTable.Rows[j]["AjStudentGrdCGPA"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjStudentGrdCGPA"]),
                            StudentGrdSpecialization = (dataTable.Rows[j]["AjStudentGrdSpecialization"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjStudentGrdSpecialization"]),
                            StudentGrdPer = (dataTable.Rows[j]["AjStudentGrdPer"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjStudentGrdPer"]),
                            StudentGrdScorecardId = (dataTable.Rows[j]["AjStudentGrdScorecardId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjStudentGrdScorecardId"]),
                            StudentGrdYOP = (dataTable.Rows[j]["AjStudentGrdYOP"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjStudentGrdYOP"]),

                        };
                        objStudentQueryPropertyList.Add(objStudentQueryProperty);

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
                const string addInfo = "Error while executing BindGraduation in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objStudentQueryPropertyList;
        }
        private List<SchoolBoardproperty> BindBoardDetails(DataTable dataTable)
        {
            var objSchoolBoardpropertyList = new List<SchoolBoardproperty>();

            try
            {
                if (dataTable.Rows.Count > 0)
                {

                    for (var j = 0; j < dataTable.Rows.Count; j++)
                    {

                        var objSchoolBoardproperty = new SchoolBoardproperty
                        {
                           
                            BoardId = (dataTable.Rows[j]["AjBoardId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjBoardId"]),
                            CityId = (dataTable.Rows[j]["AjCityId"] is DBNull) ? 0 : Convert.ToInt32(dataTable.Rows[j]["AjCityId"]),
                            CityName = (dataTable.Rows[j]["AjCityName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjCityName"]),
                            BoardFullName = (dataTable.Rows[j]["AjBoardFullName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjBoardFullName"]),
                            BoardShortName = (dataTable.Rows[j]["AjBoardShortName"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjBoardShortName"]),
                            HeadOffAddrs = (dataTable.Rows[j]["AjHeadOffAddrs"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjHeadOffAddrs"]),
                            Website = (dataTable.Rows[j]["AjWebsite"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjWebsite"]),
                            BoardLogo = (dataTable.Rows[j]["AjBoardLogo"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjBoardLogo"]),
                            ContactNumber = (dataTable.Rows[j]["AjContactNumber"] is DBNull) ? null : Convert.ToString(dataTable.Rows[j]["AjContactNumber"]),

                        };
                        objSchoolBoardpropertyList.Add(objSchoolBoardproperty);

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
                const string addInfo = "Error while executing BindBoardDetails in Profile.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return objSchoolBoardpropertyList;
        }
    }
}

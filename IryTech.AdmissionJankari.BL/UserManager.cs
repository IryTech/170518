using System.Collections.Generic;
using IryTech.AdmissionJankari.DAL;
using System.Data;
using System.Data.SqlClient;
using System;


namespace IryTech.AdmissionJankari.BL
{
    public class UserManager : UserManagerProvider
    {
        private DbWrapper _objDataWrapper;
        private DataSet _dataSet;
        private int _i;
        public DataTable GetadmissionJankriTestimonial(int testimonialId=0)
        {
            _dataSet = new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {
                _objDataWrapper.AddParameter("@TestimonialId", testimonialId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetAdmissionJankariTestimonial");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetadmissionJankriTestimonial in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];

        }
        public override int InsertUpdateTestimonialAdmssionjankari(AdmissionJankariTestimonial objAdmissionJankariTestimonial, int createdBy, out string errMsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errMsg = "";
            try
            {
                _objDataWrapper.AddParameter("@TestimonialId", objAdmissionJankariTestimonial.TestimonialId);
                _objDataWrapper.AddParameter("@TestimonialPersonName", objAdmissionJankariTestimonial.TestimonialPersonName);
                _objDataWrapper.AddParameter("@TestimonialPersonDesgination", objAdmissionJankariTestimonial.TestimonialPeronDesignation);
                _objDataWrapper.AddParameter("@TestimonialPriority", objAdmissionJankariTestimonial.TestimonialPriority);
                _objDataWrapper.AddParameter("@TestimonialText", objAdmissionJankariTestimonial.TestimonialText);
                _objDataWrapper.AddParameter("@TestimonialStatus", objAdmissionJankariTestimonial.TestimonialStatus);
                _objDataWrapper.AddParameter("@TestimonialPersonImage", objAdmissionJankariTestimonial.TestimonialImage);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);

                var ObjerrMsg =
                    (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateAdmissionjankariTestimonial");
                if (ObjerrMsg != null && ObjerrMsg.Value != null)
                    errMsg = Convert.ToString(ObjerrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUpdateTestimonialAdmssionjankari in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }
        public override int UpdateCourseByUser(UserRegistrationProperty objUserCategoryProperty, int modifiedBy)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);

            try
            {

                _objDataWrapper.AddParameter("@CourseId", objUserCategoryProperty.CourseId);
                _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
           
               _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_UpdateCourseByUser");
              

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateCourseByUser in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }
        public DataTable GetStudentAccademicInfoStatus(int UserId)
        {
            _dataSet = new DataSet();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {
                _objDataWrapper.AddParameter("@UserId", UserId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetStudentAccademicInfo");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetStudentAccademicInfoStatus in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _dataSet.Tables[0];

        }
        public override int UpdateUserProfile(string value, string field, int userId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            
            try
            {
                if (field != "AjUserDOB")
                {
                    _objDataWrapper.AddParameter("@Value", value);
                }else
                {
                    _objDataWrapper.AddParameter("@Value", Common.GetDateFromString(value));
                }
                _objDataWrapper.AddParameter("@Field", field);
                _objDataWrapper.AddParameter("@CreatedBy", userId);
                var errmsg = "";
                var errMsg =
                  (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_UpdateUserProfile");
            
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
                const string addInfo = "Error while executing UpdateUserCategory in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }
        public override int UpdateUserCityDetails(int country, int state, int city, int userId)
        {
            var query = "";
            query = " AjCountryId=" + country + ",AjStateId=" + state + ",AjCityId=" + city + " where AjUserId=" + userId;
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);

            try
            {
                
                _objDataWrapper.AddParameter("@Query", query);
                
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_UpdateCityUserProfile");


            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateUserCategory in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }
        public override int InsertUserCategory(UserCategoryProperty objUserCategoryProperty, int createdBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            try
            {
                _objDataWrapper.AddParameter("@UserCategoryName", objUserCategoryProperty.UserCategoryName);
                _objDataWrapper.AddParameter("@UserCategoryDashBoardUrl", objUserCategoryProperty.UserCategoryDashboard);
                _objDataWrapper.AddParameter("@UserCategoryCanCreateUser", objUserCategoryProperty.CanCreateUser);
                _objDataWrapper.AddParameter("@UserCategoryStatus", objUserCategoryProperty.UserCategoryStatus);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                var objerrMsg
                    = (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateUserCategoryDetails");
                if (objerrMsg != null && objerrMsg.Value != null)
                    errmsg = Convert.ToString(objerrMsg.Value);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUserCategory in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override int  UpdateUserCategory(UserCategoryProperty objUserCategoryProperty, int modifiedBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            try
            {
                _objDataWrapper.AddParameter("@UserCategoryId", objUserCategoryProperty.UserCategoryId);
                _objDataWrapper.AddParameter("@UserCategoryName", objUserCategoryProperty.UserCategoryName);
                _objDataWrapper.AddParameter("@UserCategoryDashBoardUrl", objUserCategoryProperty.UserCategoryDashboard);
                _objDataWrapper.AddParameter("@UserCategoryCanCreateUser", objUserCategoryProperty.CanCreateUser);
                _objDataWrapper.AddParameter("@UserCategoryStatus", objUserCategoryProperty.UserCategoryStatus);
                _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
                var objerrMsg
                    = (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateUserCategoryDetails");
                if (objerrMsg != null && objerrMsg.Value != null)
                    errmsg = Convert.ToString(objerrMsg.Value);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateUserCategory in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override List<UserCategoryProperty> GetAllUserCategoryList()
        {
            var userCategoryListObject = new List<UserCategoryProperty>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();

            try
            {
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetUserCategoryList");
                userCategoryListObject = BindUserCategoryListObject(_dataSet.Tables[0]);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllUserCategoryList in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return userCategoryListObject;
        }

        public override List<UserCategoryProperty> GetUserCategoryById(int userCategoryId)
        {
            var userCategoryListObject = new List<UserCategoryProperty>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            try
            {
                _objDataWrapper.AddParameter("@UserCategoryId", userCategoryId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetUserCategoryList");
                userCategoryListObject = BindUserCategoryListObject(_dataSet.Tables[0]);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetUserCategoryById in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return userCategoryListObject;
        }
        public override int InsertUserInfo(UserRegistrationProperty objUserRegistrationProperty, int createdBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            try
            {
                _objDataWrapper.AddParameter("@UserCityId", objUserRegistrationProperty.CityId);
                _objDataWrapper.AddParameter("@UserCourseId", objUserRegistrationProperty.CourseId);
                _objDataWrapper.AddParameter("@UserCountryId", objUserRegistrationProperty.CountryCode);
                _objDataWrapper.AddParameter("@UserPhone", objUserRegistrationProperty.PhoneNo);
                _objDataWrapper.AddParameter("@UserStateId", objUserRegistrationProperty.StateId);
                _objDataWrapper.AddParameter("@UserCatId", objUserRegistrationProperty.UserCategoryId);
                _objDataWrapper.AddParameter("@UserCorssAddrs", objUserRegistrationProperty.UserCorrespondenceAddress);
                _objDataWrapper.AddParameter("@UserEmailId", objUserRegistrationProperty.UserEmailid);
                _objDataWrapper.AddParameter("@UserMobileNo", objUserRegistrationProperty.MobileNo);
                _objDataWrapper.AddParameter("@UserFullName", objUserRegistrationProperty.UserFullName);
                _objDataWrapper.AddParameter("@UserPassword",objCrypto.Encrypt(objUserRegistrationProperty.UserPassword));
                _objDataWrapper.AddParameter("@UserPerAddrs", objUserRegistrationProperty.UserPermanentAddress);
                _objDataWrapper.AddParameter("@UserPincode", objUserRegistrationProperty.UserPincode);
                _objDataWrapper.AddParameter("@UserStatus", objUserRegistrationProperty.UserStatus);
                _objDataWrapper.AddParameter("@SubUserId", objUserRegistrationProperty.UserSubId);
                _objDataWrapper.AddParameter("@UserGender", objUserRegistrationProperty.UserGender);
                _objDataWrapper.AddParameter("@UserDOB", DateTime.Now);
                _objDataWrapper.AddParameter("@UserFatherName",objUserRegistrationProperty.UserFatherName);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                var objerrMsg
                    = (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                var objUserId
                   = (SqlParameter)(_objDataWrapper.AddParameter("@UserId", "", SqlDbType.Int, ParameterDirection.Output));
                          
                 _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateUserDetails");
                if (objerrMsg != null && objerrMsg.Value != null)
                    errmsg = Convert.ToString(objerrMsg.Value);
                if (objUserId != null && objUserId.Value != null)
                    objUserRegistrationProperty.UserId = Convert.ToInt32(objUserId.Value);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertUserInfo in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override int UpdateUserInfo(UserRegistrationProperty objUserRegistrationProperty, int modifiedBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            try
            {
                _objDataWrapper.AddParameter("@UserCityId", objUserRegistrationProperty.CityId);
                _objDataWrapper.AddParameter("@UserCountryId", objUserRegistrationProperty.CountryCode);
                _objDataWrapper.AddParameter("@UserPhone", objUserRegistrationProperty.PhoneNo);
                _objDataWrapper.AddParameter("@UserStateId", objUserRegistrationProperty.StateId);
                _objDataWrapper.AddParameter("@UserCatId", objUserRegistrationProperty.UserCategoryId);
                _objDataWrapper.AddParameter("@UserCorssAddrs", objUserRegistrationProperty.UserCorrespondenceAddress);
                _objDataWrapper.AddParameter("@UserEmailId", objUserRegistrationProperty.UserEmailid);
                _objDataWrapper.AddParameter("@UserFullName", objUserRegistrationProperty.UserFullName);
                _objDataWrapper.AddParameter("@UserPassword", objCrypto.Encrypt(objUserRegistrationProperty.UserPassword));
                _objDataWrapper.AddParameter("@UserMobileNo", objUserRegistrationProperty.MobileNo);
                _objDataWrapper.AddParameter("@UserPerAddrs", objUserRegistrationProperty.UserPermanentAddress);
                _objDataWrapper.AddParameter("@UserPincode", objUserRegistrationProperty.UserPincode);
                _objDataWrapper.AddParameter("@UserStatus", objUserRegistrationProperty.UserStatus);
                _objDataWrapper.AddParameter("@SubUserId", objUserRegistrationProperty.UserSubId);
                _objDataWrapper.AddParameter("@UserCourseId", objUserRegistrationProperty.CourseId);
                _objDataWrapper.AddParameter("@UserGender", objUserRegistrationProperty.UserGender);
                _objDataWrapper.AddParameter("@UserDOB", objUserRegistrationProperty.UserDOB);
                _objDataWrapper.AddParameter("@UserFatherName", objUserRegistrationProperty.UserFatherName);
                _objDataWrapper.AddParameter("@CreatedBy", modifiedBy);
                var objerrMsg
                    = (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                var objUserId
                   = (SqlParameter)(_objDataWrapper.AddParameter("@UserId", objUserRegistrationProperty.UserId, SqlDbType.Int, ParameterDirection.Input));

                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateUserDetails");
                if (objerrMsg != null && objerrMsg.Value != null)
                    errmsg = Convert.ToString(objerrMsg.Value);
                if (objUserId != null && objUserId.Value != null)
                    objUserRegistrationProperty.UserId = Convert.ToInt32(objUserId.Value);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateUserInfo in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override List<UserRegistrationProperty> GetAllUserList()
        {
            var userListObject = new List<UserRegistrationProperty>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();

            try
            {

                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetUserList");
                userListObject = BindUserListObject(_dataSet.Tables[0]);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllUserList in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return userListObject;
        }
        public override List<UserRegistrationProperty> GetUserListByPaymenStatus(bool paymentStatus)
        {
            var userListObject = new List<UserRegistrationProperty>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();

            try
            {
                _objDataWrapper.AddParameter("@PaymentStatus", paymentStatus);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetPaidUserDetails");
                userListObject = BindUserListObject(_dataSet.Tables[0]);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllUserList in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return userListObject;
        }

        public override List<UserRegistrationProperty> GetUserListById(int userId)
        {
            var userListObject = new List<UserRegistrationProperty>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();

            try
            {
                _objDataWrapper.AddParameter("@UserId", userId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetUserList");
                userListObject = BindUserListObject(_dataSet.Tables[0]);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetUserListById in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return userListObject;
        }


        public override List<UserRegistrationProperty> GetUserListByName(string userName)
        {
            var userListObject = new List<UserRegistrationProperty>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();

            try
            {
                _objDataWrapper.AddParameter("@UserName", userName);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetUserList");
                userListObject = BindUserListObject(_dataSet.Tables[0]);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetUserListByName in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return userListObject;
        }

        public override List<UserRegistrationProperty> GetUserListByEmailId(string emailId)
        {
            var userListObject = new List<UserRegistrationProperty>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();

            try
            {
                _objDataWrapper.AddParameter("@UserEmailId", emailId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetUserList");
                userListObject = BindUserListObject(_dataSet.Tables[0]);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetUserListByEmailId in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return userListObject;
        }

        public override List<UserRegistrationProperty> GetUserListByMobile(string mobileNo)
        {
            var userListObject = new List<UserRegistrationProperty>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();

            try
            {
                _objDataWrapper.AddParameter("@UserMobileNo", mobileNo);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetUserList");
                userListObject = BindUserListObject(_dataSet.Tables[0]);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetUserListByMobile in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return userListObject;
        }

        public override List<UserRegistrationProperty> GetUserListByCity(int cityId)
        {
            var userListObject = new List<UserRegistrationProperty>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();

            try
            {
                _objDataWrapper.AddParameter("@UserCityId", cityId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetUserList");
                userListObject = BindUserListObject(_dataSet.Tables[0]);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetUserListByCity in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return userListObject;
        }

        public override List<UserRegistrationProperty> GetUserListByState(int stateId)
        {
            var userListObject = new List<UserRegistrationProperty>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();

            try
            {
                _objDataWrapper.AddParameter("@UserStateId", stateId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetUserList");
                userListObject = BindUserListObject(_dataSet.Tables[0]);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetUserListByCity in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return userListObject;
        }

        public override List<UserRegistrationProperty> GetUserListByCategory(int categoryId)
        {
            var userListObject = new List<UserRegistrationProperty>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();

            try
            {
                _objDataWrapper.AddParameter("@UsercategoryId", categoryId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetUserList");
                userListObject = BindUserListObject(_dataSet.Tables[0]);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetUserListByCategory in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return userListObject;
        }


        public override List<UserRegistrationProperty> GetUserListByCourseId(int courseId)
        {
            var userListObject = new List<UserRegistrationProperty>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();

            try
            {
                _objDataWrapper.AddParameter("@UserCourseId", courseId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetUserList");
                userListObject = BindUserListObject(_dataSet.Tables[0]);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetUserListByCourseId in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return userListObject;
        }

        public override string GetUserPassword(string emailId, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            string Password="";
              var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            try
            {
                _objDataWrapper.AddParameter("@UserEmailId", emailId);
                var ObjErrMsg =
                        (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                var objPassword =
                        (SqlParameter)(_objDataWrapper.AddParameter("@Password", "", SqlDbType.VarChar, ParameterDirection.Output, 64));
                _i=_objDataWrapper.ExecuteNonQuery("Aj_Proc_GetUserPassword");
                if(ObjErrMsg!=null && ObjErrMsg.Value!=null)
                        errmsg=Convert.ToString(ObjErrMsg.Value);
                if(objPassword!=null && objPassword.Value!=null)
                        Password=Convert.ToString(objPassword.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetUserPassword in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return !string.IsNullOrEmpty(Password)? objCrypto.Decrypt(Password):"";
           
        }

        public override int ChangePassword(int userId, string oldPassword, string newPassword, out string errmsg)
        {
           _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = "";
            string Password="";
              var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            try
            {
               
                _objDataWrapper.AddParameter("@UserId", userId);
                _objDataWrapper.AddParameter("@UserOldPassword",objCrypto.Encrypt(oldPassword));
                _objDataWrapper.AddParameter("@UserNewPassword",objCrypto.Encrypt(newPassword));
                var ObjErrMsg =
                        (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
               
                _i=_objDataWrapper.ExecuteNonQuery("Aj_UserChangePassword");
                if(ObjErrMsg!=null && ObjErrMsg.Value!=null)
                        errmsg=Convert.ToString(ObjErrMsg.Value);
                
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetUserPassword in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return _i;
        }

        public override bool DoLogin(string emailId, string password, out int userTypeId, out int userId, out string userName, out string landingPage, out string mobileNo, out bool canCreateUser,out string errmsg,out bool userStatus)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            errmsg = "";
            userTypeId = 0;
            userName = "";
            landingPage = "";
            mobileNo = "";
            userId = 0;
            userStatus = false;
            canCreateUser=false;
            string pwd = "";
            try
            {
              
                _objDataWrapper.AddParameter("@UserEmailId", emailId);
                var objUserStatus =
                   (SqlParameter)(_objDataWrapper.AddParameter("@UserStatus", "", SqlDbType.Bit, ParameterDirection.Output));
                var ObjErrMsg =
                       (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                var ObjPassword =
                      (SqlParameter)(_objDataWrapper.AddParameter("@UserPassword", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                var ObjUserName =
                      (SqlParameter)(_objDataWrapper.AddParameter("@UserName", "", SqlDbType.VarChar, ParameterDirection.Output, 64));
                var ObjUseId =
                     (SqlParameter)(_objDataWrapper.AddParameter("@UserId", "", SqlDbType.Int, ParameterDirection.Output));
                var ObjUseTypeId =
                     (SqlParameter)(_objDataWrapper.AddParameter("@UserTypeId", "", SqlDbType.Int, ParameterDirection.Output));
                 var ObjCanCreateUser =
                     (SqlParameter)(_objDataWrapper.AddParameter("@CanCreateUser", "", SqlDbType.Bit, ParameterDirection.Output));
                var ObjUserMobile =
                      (SqlParameter)(_objDataWrapper.AddParameter("@UserMobileNo", "", SqlDbType.VarChar, ParameterDirection.Output, 32));
                var ObjUserLandingPage =
                      (SqlParameter)(_objDataWrapper.AddParameter("@LandingPage", "", SqlDbType.VarChar, ParameterDirection.Output, 256));
              

                _i=_objDataWrapper.ExecuteNonQuery("Aj_Proc_DoLogin");

                    if(ObjErrMsg!=null && ObjErrMsg.Value!=null)
                            errmsg=Convert.ToString(ObjErrMsg.Value);
                    if (ObjPassword != null && ObjPassword.Value != null)
                        pwd = Convert.ToString(ObjPassword.Value);

                    if(ObjUserName!=null && ObjUserName.Value!=null)
                            userName=Convert.ToString(ObjUserName.Value);
                    if(ObjUseId!=null && ObjUseId.Value!=null)
                            userId=Convert.ToInt32(ObjUseId.Value);
                    if(ObjUseTypeId!=null && ObjUseTypeId.Value!=null)
                            userTypeId=Convert.ToInt32(ObjUseTypeId.Value);
                    if(ObjCanCreateUser!=null && ObjCanCreateUser.Value!=null)
                            canCreateUser=Convert.ToBoolean(ObjCanCreateUser.Value);
                    if(ObjUserMobile!=null && ObjUserMobile.Value!=null)
                            mobileNo=Convert.ToString(ObjUserMobile.Value);
                    if(ObjUserLandingPage!=null && ObjUserLandingPage.Value!=null)
                            landingPage=Convert.ToString(ObjUserLandingPage.Value);
                    if (objUserStatus != null && objUserStatus.Value != null)
                        userStatus = Convert.ToBoolean(objUserStatus.Value);
                    
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing DoLogin in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            if (userId > 0)
            {
                pwd = objCrypto.Decrypt(pwd);
                if(pwd.Equals(password))
                        return true;
                else
                {
                    errmsg = "Re-check your password.";
                        return false;
                }
            }
            else
            {
                return false;
            }
        }
        
        // Private Method to Bind The user Category object List
        private List<UserCategoryProperty> BindUserCategoryListObject(DataTable datatable)
        {
            var userCategoryListObject = new List<UserCategoryProperty>();

            try
            {
                if (datatable.Rows.Count > 0)
                {
                    for (var j = 0; j < datatable.Rows.Count; j++)
                    {
                        var objUserCategoryProperty = new UserCategoryProperty
                                {
                                    CanCreateUser = Convert.ToBoolean(datatable.Rows[j]["AjUserCategoryCanCreateUser"]),
                                    UserCategoryDashboard = Convert.ToString(datatable.Rows[j]["AjUserCategoryDashBoardUrl"]),
                                    UserCategoryId = Convert.ToInt32(datatable.Rows[j]["AjUserCategoryId"]),
                                    UserCategoryName = Convert.ToString(datatable.Rows[j]["AjUserCategoryName"]),
                                    UserCategoryStatus = Convert.ToBoolean(datatable.Rows[j]["AjUserCategoryStatus"])
                                };

                        userCategoryListObject.Add(objUserCategoryProperty);
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
                const string addInfo = "Error while executing BindUserCategoryListObject in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return userCategoryListObject;
        }
        private List<UserRegistrationProperty> BindUserListObject(DataTable datatable)
        {
            var userListObject = new List<UserRegistrationProperty>();
            var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
         
            try
            {
               
                if (datatable.Rows.Count > 0)
                {
                   
                    for (var j = 0; j < datatable.Rows.Count; j++)
                    {
                      
                        var objUserProperty = new UserRegistrationProperty
                        {
                           
                    
                            CityId = (datatable.Rows[j]["AjCityId"] is DBNull)?0:Convert.ToInt32(datatable.Rows[j]["AjCityId"]),
                            CityName = (datatable.Rows[j]["AjCityName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCityName"]),
                            CountryCode = (datatable.Rows[j]["AjCountryId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCountryId"]),
                            CountryName = (datatable.Rows[j]["AjCountryName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCountryName"]),
                            PhoneNo = (datatable.Rows[j]["AjUserPhoneNo"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUserPhoneNo"]),
                            MobileNo = (datatable.Rows[j]["AjUserMobile"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUserMobile"]),
                            StateId = (datatable.Rows[j]["AjStateId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjStateId"]),
                            StateName = (datatable.Rows[j]["AjStateName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjStateName"]),
                            UserCategoryId = (datatable.Rows[j]["AjUserCategoryId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjUserCategoryId"]),
                            UserCategoryName = (datatable.Rows[j]["AjUserCategoryName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUserCategoryName"]),
                            UserCorrespondenceAddress = (datatable.Rows[j]["AjUserMailingAddress"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUserMailingAddress"]),
                            UserEmailid = (datatable.Rows[j]["AjUserEmail"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUserEmail"]),
                            UserFullName = (datatable.Rows[j]["AjUserFullName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUserFullName"]),
                            UserId = (datatable.Rows[j]["AjUserId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjUserId"]),
                            UserPermanentAddress = (datatable.Rows[j]["AjUserPermanentAddress"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUserPermanentAddress"]),
                            UserPincode = (datatable.Rows[j]["AjUserPincode"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUserPincode"]),
                            CourseId = (datatable.Rows[j]["AjCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCourseId"]),
                            CourseName = (datatable.Rows[j]["AjCourseName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCourseName"]),
                            UserGender = (datatable.Rows[j]["AjUserGender"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUserGender"]),
                            UserDOB = (datatable.Rows[j]["AjUserDOB"] is DBNull) ? System.DateTime.Now : Convert.ToDateTime(datatable.Rows[j]["AjUserDOB"]),
                            UserPassword =objCrypto.Decrypt((datatable.Rows[j]["AjUserPassword"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUserPassword"])),
                         
                            UserImage = (datatable.Rows[j]["AjUserImage"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjUserImage"]),
                            ApplicationFormNumber = datatable.Columns.Contains("AjStudentFormNumber")?((datatable.Rows[j]["AjStudentFormNumber"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjStudentFormNumber"])):null,

                            StudentPaymentStatus = datatable.Columns.Contains("AjStudentPaymentStatus") ? ((datatable.Rows[j]["AjStudentPaymentStatus"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjStudentPaymentStatus"])) : null,
                            UserStatus = datatable.Columns.Contains("AjUserStatus") ? ((datatable.Rows[j]["AjUserStatus"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjUserStatus"])) : false,
                        };
                        userListObject.Add(objUserProperty);
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
                const string addInfo = "Error while executing BindUserCategoryListObject in UserManager.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return userListObject;
        }

      



        
    }
}

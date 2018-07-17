using IryTech.BaseClassLibraray;
using System.Data;
using System.Collections.Generic;
namespace IryTech.AdmissionJankari.BL
{
    public abstract  class UserManagerProvider : IUserManager
    {
        public static UserManagerProvider Instance
        {
            get
            {
                return new UserManager();
            }
        }

        public abstract  int InsertUserCategory(UserCategoryProperty objUserCategoryProperty, int createdBy, out string errmsg);
        public abstract int  UpdateUserCategory(UserCategoryProperty objUserCategoryProperty, int modifiedBy, out string errmsg);
        public abstract  List<UserCategoryProperty> GetAllUserCategoryList();
        public abstract List<UserCategoryProperty> GetUserCategoryById(int userCategoryId);
        


        #region IUserManager Members for User Registation
        public abstract int InsertUserInfo(UserRegistrationProperty objUserRegistrationProperty, int createdBy, out string errmsg);
        public abstract int UpdateUserInfo(UserRegistrationProperty objUserRegistrationProperty, int modifiedBy, out string errmsg);
        public abstract List<UserRegistrationProperty> GetAllUserList();
        public abstract List<UserRegistrationProperty> GetUserListById(int userId);
        public abstract List<UserRegistrationProperty> GetUserListByName(string userName);
        public abstract List<UserRegistrationProperty> GetUserListByEmailId(string emailId);
        public abstract List<UserRegistrationProperty> GetUserListByMobile(string mobileNo);
        public abstract List<UserRegistrationProperty> GetUserListByCity(int cityId);
        public abstract List<UserRegistrationProperty> GetUserListByState(int stateId);
        public abstract List<UserRegistrationProperty> GetUserListByCategory(int categoryId);
        public abstract List<UserRegistrationProperty> GetUserListByCourseId(int courseId);
        public abstract string GetUserPassword(string emailId, out string errmsg);
        public abstract int ChangePassword(int userId, string oldPassword, string newPassword, out string errmsg);
        public abstract bool DoLogin(string emailId, string password, out int userTypeId, out int userId, out string userName,
                                      out string landingPage, out string mobileNo, out bool canCreateUser, out string errmsg, out bool userStatus);

        public abstract int UpdateUserProfile(string value, string field, int userId);

        public abstract int UpdateUserCityDetails(int country, int state, int city,int userId);


        public abstract int UpdateCourseByUser(UserRegistrationProperty objUserCategoryProperty, int modifiedBy);



        public abstract List<UserRegistrationProperty> GetUserListByPaymenStatus(bool paymentstatus);

        public abstract int InsertUpdateTestimonialAdmssionjankari(AdmissionJankariTestimonial objUserCategoryProperty, int createdOn, out string errMsg);

        #endregion









    }
}

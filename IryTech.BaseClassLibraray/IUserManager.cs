
using System.Collections.Generic;

namespace IryTech.BaseClassLibraray
{
    public interface IUserManager
    {
        int InsertUserCategory(UserCategoryProperty objUserCategoryProperty, int createdBy, out string errmsg);
        int UpdateUserCategory(UserCategoryProperty objUserCategoryProperty, int modifiedBy, out string errmsg);
        List<UserCategoryProperty> GetAllUserCategoryList();
        List<UserCategoryProperty> GetUserCategoryById(int userCategoryId);
        int InsertUserInfo(UserRegistrationProperty objUserRegistrationProperty, int createdBy, out string errmsg);
        int UpdateUserInfo(UserRegistrationProperty objUserRegistrationProperty, int modifiedBy, out string errmsg);
        List<UserRegistrationProperty> GetAllUserList();
        List<UserRegistrationProperty> GetUserListById(int userId);
        List<UserRegistrationProperty> GetUserListByName(string userName);
        List<UserRegistrationProperty> GetUserListByEmailId(string emailId);
        List<UserRegistrationProperty> GetUserListByMobile(string mobileNo);
        List<UserRegistrationProperty> GetUserListByCity(int cityId);
        List<UserRegistrationProperty> GetUserListByState(int stateId);
        List<UserRegistrationProperty> GetUserListByCategory(int categoryId);
        List<UserRegistrationProperty> GetUserListByCourseId(int courseId);
        string GetUserPassword(string emailId, out string errmsg);
        int ChangePassword(int userId, string oldPassword, string newPassword, out string errmsg);
        bool DoLogin(string emailId, string password, out int userTypeId, out int userId, out string userName, out string landingPage,
                            out string mobileNo, out bool canCreateUser,out string errmsg,out bool userStatus);
        int UpdateCourseByUser(UserRegistrationProperty objUserCategoryProperty, int modifiedBy);


        List<UserRegistrationProperty> GetUserListByPaymenStatus(bool paymentstatus);


        int InsertUpdateTestimonialAdmssionjankari(AdmissionJankariTestimonial objUserCategoryProperty, int createdOn,out string errMsg);
        
    }
}

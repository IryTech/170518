using System;
[Serializable()] 
public class UserCategoryProperty
{
    public int UserCategoryId { get; set; }
    public string UserCategoryName { get; set; }
    public string UserCategoryDashboard { get; set; }
    public bool CanCreateUser { get; set; }
    public bool UserCategoryStatus { get; set; }
}
[Serializable()]
public class UserRegistrationProperty
{
    public int UserId { get; set; }
    public int UserSubId { get; set; }
    public int UserCategoryId { get; set; }
    public string UserCategoryName { get; set; }
    public string UserFullName { get; set; }
    public string UserEmailid { get; set; }
    public string UserPassword { get; set; }
    public string UserCorrespondenceAddress { get; set; }
    public string UserPermanentAddress { get; set; }
    public string UserPincode { get; set; }
    public string PhoneNo { get; set; }
    public string MobileNo { get; set; }
    public int CountryCode { get; set; }
    public string CountryName { get; set; }
    public int StateId { get; set; }
    public string StateName { get; set; }
    public int CityId { get; set; }
    public string CityName { get; set; }
    public bool UserStatus { get; set; }
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public string UserGender { get; set; }
    public DateTime UserDOB { get; set; }
    public string UserFatherName { get; set; }
    public string UserImage { get; set; }
    public string ApplicationFormNumber { get; set; }public string StudentPaymentStatus { get; set; }

}
    [Serializable()]
    public class AdmissionJankariTestimonial
    {
        public int TestimonialId { get; set; }
        public string TestimonialText { get; set; }
        public bool TestimonialStatus { get; set; }
        public string TestimonialPersonName { get; set; }
        public string TestimonialPeronDesignation { get; set; }
        public string TestimonialPriority { get; set; }
        public string TestimonialImage { get; set; }
         }



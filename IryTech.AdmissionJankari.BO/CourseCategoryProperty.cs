using System;
[Serializable()] 
  public  class CourseCategoryProperty
  {
      public int CourseCategoryId { get; set;}
      public string CourseCategoryName { get; set; }
      public bool CourseCategoryStatus { get; set; }

  }
[Serializable()] 
public class CourseEligibiltyProperty
{
    public int CourseEligibilityId { get; set; }
    public string CourseEligibiltyName { get; set;}
    public bool CourseEligibilityStatus { get; set; }
}
[Serializable()] 
public class CourseMasterProperty
{
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public string CourseUrl { get; set; }
    public string CourseTitle { get; set; }
    public string CourseMetaTag { get; set; }
    public string CourseMetaDesc { get; set; }
    public string CourseShortName { get; set; }
    public string CoursePopularName { get; set; }
    public int CourseCategoryId { get; set;}
    public string CourseCategoryName { get; set;}
    public string CourseEligibityName { get; set; }
    public int CourseEligibiltyId { get; set;}
    public bool CourseStatus { get; set; }
    public string CourseDesc { get; set; }
    public string CourseShowHome { get; set; }
    public string CourseImage { get; set; }
    public string HelpLineNo { get;set;}
    public bool IsBookSeatVisible { get; set; }
}
[Serializable()] 
public class CourseStreamProperty
{
    public int StreamId { get; set; }
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public string CourseStreamUrl { get; set; }
    public string CourseStreamTitle { get; set; }
    public string CourseStreamMetaTag { get; set; }
    public string CourseStreamMetaDesc { get; set; }
    public string CourseStreamName { get; set; }
    public string CourseStreamDesc { get; set; }
    public string CourseStreamHistory { get; set; }
    public string CourseSteamFuture { get; set; }
    public string CourseStreamRelatedIndustry { get; set; }
    public string CourseStreamCoreCompanies { get; set; }
    public bool CourseStreamStatus { get; set; }
}

[Serializable()]
public class CoursePaymentMasterProperty
{
	public int PaymentCourseId { get; set; }
	public int CourseId { get; set; }
	public string OnlinePaymentAmount { get; set; }
	public string OfflinePaymentAmount { get; set; }
	public string CourseName {get; set;}
	public int courseCategoryId  {get;set;}

	
}

namespace IryTech.AdmissionJankari.BO
{
   public class QueryProperty
    {
       public string StudentQueryId { get; set; }
       public string StudentName { get; set; }
       public int StudentQueryParentId { get; set; }
       public int StudentSourceTypeId { get; set; }
       public int StudentSourceId { get; set; }
       public string StudentCityName { get; set; }
       public int StudentCourseId { get; set; }
       public string StudentCourseName { get; set; }
       public int StudentCourseStreamId { get; set; }
       public string StudentQuery { get; set; }
       public int UserId { get; set; }
       public string UserEmailId { get; set; }
       public string UserMobileNo { get; set; }
       public int ExamId { get; set; }
       public string SourceTypeName { get; set; }
       public string SourceName { get; set; }
       public bool ReplyStatus { get; set; }
       public string QueryReply { get; set; }
       public int ReplyBy { get; set; }
       public System.DateTime CreatedOn { get; set; }
       public bool QueryStatus{ get; set; }

    }


   public class ReplyProperty
   {
       public int QueryId { get; set; }
       public string QueryReply { get; set; }
       public string ReplyUserMobile { get; set; }
       public string ReplyUserName { get; set; }
       public string ReplyUserEmailId { get; set; }
       public string QueryReplyId { get; set; }
       public int ReplyBy { get; set; }
       public bool ReplyStatus { get; set; }
   }
}

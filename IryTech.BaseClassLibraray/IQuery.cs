
using IryTech.AdmissionJankari.BO;
using System.Collections.Generic;
namespace IryTech.BaseClassLibraray
{
  public interface IQuery
    {
        int InsertCommonQuickQuery(QueryProperty objQuickQuery, out string errMsg,out int userId);
        int InsertCollegeQuickQuery(QueryProperty objQuickQuery, out string errMsg);
        int InsertExamQuickQuery(QueryProperty objQuickQuery, out string errMsg);
        int InsertLoanQuickQuery(QueryProperty objQuickQuery, out string errMsg);
        int InsertQueryThread(QueryProperty objQuickQuery, out string errMsg);
        List<QueryProperty> GetAllQueryListByCourse();
        List<QueryProperty> GetAllQueryListByExam();
        List<QueryProperty> GetAllQueryListByCollege();
        List<QueryProperty> GetAllQueryListByLoan();
       int InsertQueryReply(ReplyProperty objReplyProperty, out string errMsg);

       int UpdateQueryReplyStatus(QueryProperty objReplyProperty, out string errMsg);
       List<QueryProperty> GetCollegeQuery(int collegeId, int queryId,int course);
       int ModerateStudentQuery(int queryId,int moderateBy,bool status, out string errMsg);
       string  CheckQueryModerate(int queryId);
    }
}

using IryTech.AdmissionJankari.BO;
using System.Collections.Generic;
using IryTech.BaseClassLibraray;
namespace IryTech.AdmissionJankari.BL
{
    public abstract class QueryProvider :IQuery
    {
        public static QueryProvider Instance
        {
            get { return new Query(); }
        }

        #region IQuery Members
        public abstract int InsertCommonQuickQuery(QueryProperty objQuickQuery, out string errMsg, out int UserId);
        public abstract int InsertCollegeQuickQuery(QueryProperty objQuickQuery, out string errMsg);
        public abstract int InsertExamQuickQuery(QueryProperty objQuickQuery, out string errMsg);
        public abstract int InsertLoanQuickQuery(QueryProperty objQuickQuery, out string errMsg);
        public abstract int InsertQueryThread(QueryProperty objQuickQuery, out string errMsg);
        public abstract List<QueryProperty> GetAllQueryListByCourse();
        public abstract List<QueryProperty> GetAllQueryListByExam();
        public abstract List<QueryProperty> GetAllQueryListByCollege();
        public abstract List<QueryProperty> GetAllQueryListByLoan();
        
        #endregion


        #region IQuery Members for Query Reply
            public abstract int InsertQueryReply(ReplyProperty objReplyProperty, out string errMsg);


            public abstract int UpdateQueryReplyStatus(QueryProperty objReplyProperty, out string errMsg);

            public abstract List<QueryProperty> GetCollegeQuery(int collegeId, int queryId, int course);
        #endregion

            #region IQuery Members


            public abstract int ModerateStudentQuery(int queryId, int moderateBy,bool status, out string errMsg);

            public abstract string  CheckQueryModerate(int queryId);
            #endregion
    }
}

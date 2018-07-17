using System.Collections.Generic;
using IryTech.BaseClassLibraray;
using IryTech.AdmissionJankari.BO;
namespace IryTech.AdmissionJankari.BL
{
    public abstract class BankProvider:IBank
    {
        public static BankProvider Instance
        {
            get { return new Bank(); }
        }
        public abstract int InsertBankInfo(BankDetailsProperty objBankProperty, int createdBy, out string errmsg,
             out int bankId);
        public abstract int UpdateBankInfo(BankDetailsProperty objBankProperty, int createdBy, out string errmsg);
           
        public abstract List<BankDetailsProperty> GetAllBankList();
        public abstract List<BankDetailsProperty> GetBankListById(int bankId);
        public abstract List<BankDetailsProperty> GetBankListByShortName(string shortName);
        public abstract List<BankDetailsProperty> GetBankListByName(string BankName);

        public abstract int InsertLoanInfo(LoanDetailsProperty objLoanProperty, int createdBy, out string errmsg);

        public abstract int UpdateLoanInfo(LoanDetailsProperty objLoanProperty, int createdBy, out string errmsg);

        public abstract List<LoanDetailsProperty> GetLoanListByBankId(int bankId);
        public abstract List<StudyPlace> GetStudyPlace();
        public abstract List<LoanRange> GetLoanRange();
        public abstract List<LoanDetailsProperty> GetLoanListByLoan(int LoanId);
        
    }
}

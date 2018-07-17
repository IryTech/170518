using IryTech.AdmissionJankari.BO;
using System.Collections.Generic;
namespace IryTech.BaseClassLibraray
{

    public interface IBank
    {
        int InsertBankInfo(BankDetailsProperty objBankProperty, int createdBy, out string errmsg,
              out int bankId);
        int UpdateBankInfo(BankDetailsProperty objBankProperty, int createdBy, out string errmsg);
           
        int InsertLoanInfo(LoanDetailsProperty objLoanProperty, int createdBy, out string errmsg);

        int UpdateLoanInfo(LoanDetailsProperty objLoanProperty, int createdBy, out string errmsg);
           
        List<BankDetailsProperty> GetAllBankList();
        List<BankDetailsProperty> GetBankListById(int bankId);
        List<LoanDetailsProperty> GetLoanListByBankId(int bankId);
        List<BankDetailsProperty> GetBankListByShortName(string shortName);
        List<BankDetailsProperty> GetBankListByName(string BankName);
        List<StudyPlace> GetStudyPlace();
        List<LoanRange> GetLoanRange();
        List<LoanDetailsProperty> GetLoanListByLoan(int LoanId);
    }
}

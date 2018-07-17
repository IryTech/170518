

namespace IryTech.AdmissionJankari.BO
{
    public class BankDetailsProperty
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string BankUrl { get; set; }
        public string BankShortName { get; set; }
        public string BankAddress { get; set; }
        public string BankShortDescription { get; set; }
        public string BankContactPerson { get; set; }
        public string BankContactPersonDesignation { get; set; }
        public string BankContactPersonMobile { get; set; }
        public string BankPhoneNo { get; set; }
        public string BankContactPersonEmailId { get; set; }
        public string BankLogo { get; set; }

    }
    public class LoanDetailsProperty
    {
        public int LoanId { get; set; }
        public int BankId { get; set; }
        public int LoanRangeId { get; set; }
        public int StudyPlaceId { get; set; }
        public string Eligibilty { get; set; }
        public string Security { get; set; }
        public string RepaymentDuration { get; set; }
        public string RateOfInterest { get; set; }
        public string Margin { get; set; }
        public string Amount { get; set; }
        public string StudyPlaceName { get; set; }
        public string ProcessingFees { get; set; }
        public string ProcessingTime { get; set; }
        public string Remark { get; set; }

    }
    public class StudyPlace
    {
        public int StudyPlaceId { get; set; }
        public string StudyPlaceName { get; set; }
    }
    public class LoanRange
    {
        public int LoanRangeId { get; set; }
        public int StudyPlaceId { get; set; }
        public string Amount { get; set; }
    }
}
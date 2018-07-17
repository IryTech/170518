using System.Collections.Generic;
using IryTech.BaseClassLibraray;
using IryTech.AdmissionJankari.BO;
namespace IryTech.AdmissionJankari.BL
{
   public abstract class FAQProvider :IFAQ
    {

       public static FAQProvider Instance
       {
           get { return new FAQ(); }
       }


        public abstract int InsertFAQCategory(FAQCategoryProperty objFAQCategoryProperty, int createdBy, out string errmsg);
        public abstract int UpdtaeFAQCategory(FAQCategoryProperty objFAQCategoryProperty, int modifiedBy, out string errmsg);
        public abstract List<FAQCategoryProperty> GetAllFAQCategoryList();
        public abstract List<FAQCategoryProperty> GetAllFAQCategoryById(int faqCategotryId);
        public abstract int InsertFAQDetails(FAQDetailsProperty objFAQDetailsProperty, int createdBy, out string errmsg);
        public abstract int UpdateFAQDetails(FAQDetailsProperty objFAQDetailsProperty, int modifiedBy, out string errmsg);
        public abstract List<FAQDetailsProperty> GetAllFAQDetailsList();
        public abstract List<FAQDetailsProperty> GetFAQDetailsById(int faqDetailsId);
        public abstract List<FAQDetailsProperty> GetFAQDetailsByName(string faqDetailsName);
        public abstract List<FAQDetailsProperty> GetFAQDetailsByFAQCategory(int faqCategoryId);
       
    }
}

using System.Collections.Generic;
using IryTech.AdmissionJankari.BO;


namespace IryTech.BaseClassLibraray
{
  public interface IFAQ
    {
      int InsertFAQCategory(FAQCategoryProperty objFAQCategoryProperty,int createdBy,out string errmsg);
      int UpdtaeFAQCategory(FAQCategoryProperty objFAQCategoryProperty, int modifiedBy, out string errmsg);
      List<FAQCategoryProperty> GetAllFAQCategoryList();
      List<FAQCategoryProperty> GetAllFAQCategoryById(int faqCategotryId);
      int InsertFAQDetails(FAQDetailsProperty objFAQDetailsProperty, int createdBy, out string errmsg);
      int UpdateFAQDetails(FAQDetailsProperty objFAQDetailsProperty, int modifiedBy, out string errmsg);
      List<FAQDetailsProperty> GetAllFAQDetailsList();
      List<FAQDetailsProperty> GetFAQDetailsById(int faqDetailsId);
      List<FAQDetailsProperty> GetFAQDetailsByName(string faqDetailsName);
      List<FAQDetailsProperty> GetFAQDetailsByFAQCategory(int faqCategoryId);
    }
}

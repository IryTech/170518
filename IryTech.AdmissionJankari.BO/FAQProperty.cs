
namespace IryTech.AdmissionJankari.BO
{
    public class FAQCategoryProperty
    {
       public int FAQCategoryId { get; set; }
       public string FAQCategoryName { get; set; }
       public bool FAQCategoryStatus { get; set; }
    }

   public class FAQDetailsProperty
   {
       public int FAQCategoryId { get; set; }
       public string FAQCategoryName { get; set; }
       public int FAQDetailsId { get; set; }
       public string FAQDetailsQuestion { get; set; }
       public string FAQDetailsAnswer { get; set; }
       public bool FAQDetailsStatus { get; set; }

   }
}

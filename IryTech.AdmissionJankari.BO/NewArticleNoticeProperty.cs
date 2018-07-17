
using System;
namespace IryTech.AdmissionJankari.BO
{
    [Serializable()] 
    public class NewArticleNoticeProperty
    {
        public int NoticecategoryId { get; set; }
        public string NoticeCategoryName { get; set; }
        public bool NoticeCategoryStatus { get; set; }

    }

    [Serializable()] 
    public class NoticeDetails
    {
        public int NoticeId { get; set; }
        public string NoticeSubject { get; set; }
        public string NoticeUrl { get; set; }
        public string NoticeTitle { get; set; }
        public string NoticeMetaDesc { get; set; }
        public string NoticeShortDesc { get; set; }
        public string NoticeMetaTag { get; set; }
        public string NoticeImage { get; set; }
        public string NoticeDesc { get; set; }
        public int NoticeTypeId { get; set; }
        public bool NoticeStatus { get; set; }
        public int RealtedCollegeId { get; set; }
        public string RelatedCollegeName { get; set; }
        public string NoticeTypeName { get; set; }
        public DateTime NoticeDate { get; set; }

    }
    [Serializable()] 
    public class NewsArticleProperty
    {
        public int NewsId { get; set; }
        public string NewsUrl { get; set; }
        public string NewsTitle { get; set; }
        public string NewsMetaDesc { get; set; }
        public string NewsDesc { get; set; }
        public string NewsTag { get; set; }
        public string NewsSubject { get; set; }
        public string NewsImage { get; set; }
        public string NewsBy { get; set; }
        public DateTime NewsDate { get; set; }
        public string NewsShortDesc { get; set; }
        public string NewsType { get; set; }
        public bool NewsStatus { get; set; }


    }

    [Serializable()]
    public class TestimonialProperty
    {
        public int TestimonialID { get; set; }
        public int UserID { get; set; }
        public string UserImage { get; set; }
        public string UserName { get; set; }
        public string Testimonials { get; set; }
        public bool TestimonilaStatus { get; set; }
        public string MobileNo { get; set; }
        

    }


}

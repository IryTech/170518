
using System.Collections.Generic;
using System;


namespace IryTech.AdmissionJankari.BO
{
    [Serializable()] 
  public  class UniversityProperty
    {
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
        public string UniversityUrl {get;set;}
        public string UniversityTitle { get; set; }
        public string UniversityMetaDesc { get; set; }
        public string UniversityMetaTag { get; set; }
        public int UniversityCategoryId { get; set; }
        public string UniversityCategoryName { get; set; }
        public string UniversityDesc { get; set; }
        public string UniversityLogo { get; set; }
        public string UniversityWebsite { get; set; }
        public string UniversityPopularName { get; set; }
        public string UniversityshortName { get; set; }
        public int UniversityEst { get; set; }
        public string UniversityPhoneNo { get; set; }
        public string UniversityMobile { get; set; }
        public string UniversityFax { get; set; }
        public string UniversityAddrs { get; set; }
        public int UniversityCountryId { get; set; }
        public string UniversityCountryName { get; set; }
        public int UniversityStateId { get; set; }
        public string UniversityStateName { get; set; }
        public int UniversityCityId { get; set; }
        public string UniversityCityName { get; set; }
        public string UniversityEmailId { get; set; }


    }
    public class UniversityCategoryProperty
    {
        public int UniversityCategoryId { get; set;}
        public string UniversityCategoryName { get; set; }
    }

}

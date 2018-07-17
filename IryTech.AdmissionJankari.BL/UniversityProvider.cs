
using System.Collections.Generic;
using IryTech.BaseClassLibraray;


namespace IryTech.AdmissionJankari.BL
{
    public  abstract class UniversityProvider : IUniversity
    {

        public static UniversityProvider Instance
        {
            get
            {
                return new University();
            }
        }


        public abstract int InsertUniversityCategoryDetails(BO.UniversityCategoryProperty objUniversityCategory, int createdBy, out string errmsg);
        public abstract int UpdateUnivesityCategoryDetails(BO.UniversityCategoryProperty objUniversityCategory, int modifiedBy, out string errmsg);
        public abstract List<BO.UniversityCategoryProperty> GetAllUniversityCategoryList();
        public abstract List<BO.UniversityCategoryProperty> GetUniversityCategoryById(int universityCategoryId);
        public abstract int InsertUniversityDetails(BO.UniversityProperty objUniversityProerty, int createdBy, out string errmsg);
        public abstract int InsertUniversityDetailsUpload(BO.UniversityProperty objUniversityProerty, int createdBy, out string errmsg);
        public abstract int UpdateUniversityDetails(BO.UniversityProperty objUniversityProerty, int modifiedBy, out string errmsg);
        public abstract List<BO.UniversityProperty> GetAllUniversityList();
        public abstract List<BO.UniversityProperty> GetUniversityListById(int universityId);
        public abstract List<BO.UniversityProperty> GetUniversityListByName(string universityName);
        public abstract List<BO.UniversityProperty> GetUniversityListByCategory(int universityCategoryId);
        public abstract List<BO.UniversityProperty> GetUniversityListByCategoryUniversityName(int universityCategoryId, string universityName);
        

      
    }
}

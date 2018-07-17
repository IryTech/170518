
using System.Collections.Generic;
using IryTech.AdmissionJankari.BO;

namespace IryTech.BaseClassLibraray
{
  public interface IUniversity
    {

      int InsertUniversityCategoryDetails(UniversityCategoryProperty  objUniversityCategory, int createdBy, out string errmsg);
      int UpdateUnivesityCategoryDetails(UniversityCategoryProperty objUniversityCategory, int modifiedBy, out string errmsg);
      List<UniversityCategoryProperty> GetAllUniversityCategoryList();
      List<UniversityCategoryProperty> GetUniversityCategoryById(int universityCategoryId);
      int InsertUniversityDetailsUpload(UniversityProperty objUniversityProerty, int createdBy, out string errmsg);
      int InsertUniversityDetails(UniversityProperty objUniversityProerty,int createdBy,out string errmsg);
      int UpdateUniversityDetails(UniversityProperty objUniversityProerty, int modifiedBy, out string errmsg);
      List<UniversityProperty> GetAllUniversityList();
      List<UniversityProperty> GetUniversityListById(int universityId);
      List<UniversityProperty> GetUniversityListByName(string universityName);
      List<UniversityProperty> GetUniversityListByCategory(int universityCategoryId);
      List<UniversityProperty> GetUniversityListByCategoryUniversityName(int universityCategoryId,string universityName);



        
    }
}

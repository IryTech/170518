
using System.Collections.Generic;
using IryTech.BaseClassLibraray;

namespace IryTech.AdmissionJankari.BL
{
  public abstract  class CityProvider:ICity 
    {
      

      public static CityProvider Instacnce
      {
        get
          {
              return new City();
          }
          
      }

      public abstract int InsertCityDetails(string cityName, int stateId, int createdby, out string errmsg);
      public abstract  int UpdateCityDetails(int cityId, string cityName, int stateId, int modifiedBy, out string errmsg);
      public abstract List<CityProperty> GetAllCityList();
      public abstract List<CityProperty> GetCityById(int cityId);
      public abstract List<CityProperty> GetCityListByState(int stateId);
      public abstract List<CityProperty> GetCityListByCountry(int countryId);
      public abstract List<CityProperty> GetCityListByZone(int zoneId);
      public abstract List<CityProperty> GetCityDetailsByName(string cityName);
    }
}

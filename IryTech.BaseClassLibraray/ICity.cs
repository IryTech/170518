using System.Collections.Generic;
namespace IryTech.BaseClassLibraray
{
    public interface ICity
    {
        int InsertCityDetails(string cityName, int stateId, int createdby, out string errmsg);
        int UpdateCityDetails(int cityId, string cityName, int stateId, int modifiedBy, out string errmsg);
        List<CityProperty> GetAllCityList();
        List<CityProperty> GetCityById(int cityId);
        List<CityProperty> GetCityListByState(int stateId);
        List<CityProperty> GetCityListByCountry(int countryId);
        List<CityProperty> GetCityListByZone(int zoneId);

        List<CityProperty> GetCityDetailsByName(string cityName);
    }
}


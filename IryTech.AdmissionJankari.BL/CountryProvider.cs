using System.Collections.Generic;
using IryTech.BaseClassLibraray;

namespace IryTech.AdmissionJankari.BL
{
  public abstract   class CountryProvider:ICountry
  {

      public static CountryProvider Instance
      {
          get
          {
              return new Country();
          }
      }

      public abstract int InsertCountry(string countryname, int createdby, out string errmsg, string countrycode = null);


      public abstract int UpdateCountry(int countryid, string countryname, int createdby, out string errmsg,
                                        string countrycode = null);

      public abstract List<CountryProperty> GetAllCountry();


      public abstract List<CountryProperty> GetCountryById(int countryid);

    }
}

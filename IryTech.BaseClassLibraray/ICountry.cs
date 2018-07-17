using System;
using System.Collections.Generic;
using System.Globalization;
namespace IryTech.BaseClassLibraray
{
  public  interface ICountry
    {
        int InsertCountry(string countryname,int createdby, out  string errmsg,string countrycode=null);
        int UpdateCountry(int countryid, string countryname,int createdby, out  string errmsg, string countrycode = null);
        List<CountryProperty> GetAllCountry();
        List< CountryProperty> GetCountryById(int countryid);
       


    }
}

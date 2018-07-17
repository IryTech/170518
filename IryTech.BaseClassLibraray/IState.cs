
using System.Collections.Generic;

namespace IryTech.BaseClassLibraray
{
   public  interface IState
   {
       int InsertStateDetails(string stateName, int zoneId, int countryId, int createdBy, out string errmsg);
       int UpdateStateDetails(int stateId, string stateName, int zoneId, int countryId, int modofiedBy, out string errmsg);
       List<StateProperty> GetAllState();
       List<StateProperty> GetStateByStateId(int stateId);
       List<StateProperty> GetStateByCountry(int countryId);
       List<StateProperty> GetStateByZone(int zoneId);

   }
}

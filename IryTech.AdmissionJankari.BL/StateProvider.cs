
using System.Collections.Generic;
using IryTech.BaseClassLibraray;

namespace IryTech.AdmissionJankari.BL
{
    public abstract  class StateProvider:IState
    {

        public static StateProvider Instance 
        {
            get 
            {
                return new State();
            }

        }


        public abstract int InsertStateDetails(string stateName, int zoneId, int countryId, int createdBy,
                                               out string errmsg);


        public abstract int UpdateStateDetails(int stateId, string stateName, int zoneId, int countryId, int modofiedBy,
                                               out string errmsg);


        public abstract List<StateProperty> GetAllState();


        public abstract List<StateProperty> GetStateByStateId(int stateId);


        public abstract List<StateProperty> GetStateByCountry(int countryId);


        public abstract List<StateProperty> GetStateByZone(int zoneId);

    }
}


using System.Collections.Generic;
using IryTech.AdmissionJankari.BO;

namespace IryTech.BaseClassLibraray
{
    public interface IZone
    {
        int InsertZoneDetails(string zoneName, int createdBy,out string errMsg);
        int UpdateZoneDetails(int zoneId, string zoneName, int modifiedBy, out string errMsg);
        List<ZoneProperty> GetAllZoneList();
        List<ZoneProperty> GetZoneListById(int zoneId);

    }
}


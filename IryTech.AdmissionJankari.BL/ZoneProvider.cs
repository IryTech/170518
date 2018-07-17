
using System.Collections.Generic;
using IryTech.AdmissionJankari.BO;
using IryTech.BaseClassLibraray;

namespace IryTech.AdmissionJankari.BL
{
    public abstract class ZoneProvider : IZone
    {
        public static ZoneProvider Instance
        {
            get { return new Zone(); }
        }

        public abstract int InsertZoneDetails(string zoneName, int createdBy, out string errMsg);
        public abstract int UpdateZoneDetails(int zoneId, string zoneName, int modifiedBy, out string errMsg);
        public abstract List<ZoneProperty> GetAllZoneList();
        public abstract List<ZoneProperty> GetZoneListById(int zoneId);

    }
}

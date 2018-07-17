
using System;
using System.Collections.Generic;

namespace IryTech.AdmissionJankari.BO
{
    [Serializable()]
    public class PackageFactor
    {
        public int FactorID { get; set; }
        public string FactorName { get; set; }
        public string FactorRemark { get; set; }
        public bool IsChargeable { get; set; }
        public bool IsVisible { get; set; }
    }
    [Serializable()]
    public class PackageMaster
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public bool IsChargeable { get; set; }
        public bool IsVisible { get; set; }
        public int PackageAmount { get; set; }
        public int courseId { get; set; }
        public string CourseName { get; set; }
        

    }
    [Serializable()]
    public class PackageFactorMaster
    {
        public List<int> PackageFactorId { get; set; }
        public List<PackageFactor> ObjPackageFactor { get; set; }
        public PackageMaster objPackage { get; set; }

    }
}

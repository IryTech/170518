using IryTech.BaseClassLibraray;
using System.Collections.Generic;
using IryTech.AdmissionJankari.BO;

namespace IryTech.AdmissionJankari.BL
{
    public abstract class FactorProvider : IFactor
    {
        public static FactorProvider Instance
        {
            get
            {
                return new FormFackageFactor();
            }
        }

        public abstract List<PackageFactor> GetAllFactor();
        public abstract List<PackageFactor> GetFactorById(int factorId);
        public abstract string InsertFactor(string factorName, string factorRemar, bool isChargable, bool isVisible);
        public abstract string UpdateFactor(int factorId, string factorName, string factorRemar, bool isChargable, bool isVisible);

        public abstract List<PackageMaster> GetAllPackage();
        public abstract List<PackageMaster> GetPackageById(int packageId);
        public abstract List<PackageFactorMaster> GetFactorByPackageId(int pacakgeId);
        public abstract string InsertPackage(string packageName, int courseId, string factorId, bool isChargable, bool isVisible, int amount);
        public abstract string UpdatePackage(int packageId, int courseId, string packageName, string factorId, bool isChargable, bool isVisible, int amount);
        public abstract string DeletePackageFactor(int packageId, int factorId);
        public abstract string InsertPackageFactor(int packageId, int factorId);
    }
}



using IryTech.AdmissionJankari.BO;
using System.Collections.Generic;

namespace IryTech.BaseClassLibraray
{
   public interface IFactor
    {
        string  InsertFactor(string factorName, string factorRemar, bool isChargable, bool isVisible);
        string UpdateFactor( int factorId, string factorName, string factorRemar, bool isChargable, bool isVisible);
        List<PackageFactor> GetAllFactor();
        List<PackageFactor> GetFactorById(int factorId);
          List<PackageMaster> GetAllPackage();
          List<PackageMaster> GetPackageById(int packageId);
          List<PackageFactorMaster> GetFactorByPackageId(int pacakgeId);
          string InsertPackage(string packageName,int courseId, string factorId, bool isChargable, bool isVisible, int amount);
          string UpdatePackage(int packageId, int courseId, string packageName, string factorId, bool isChargable, bool isVisible, int amount);
          string DeletePackageFactor(int packageId, int factorId);
          string InsertPackageFactor(int packageId, int factorId);
    }
}

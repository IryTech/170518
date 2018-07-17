using System;
using System.Collections.Generic;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.DAL;
using System.Data;
using System.Data.SqlClient;

namespace IryTech.AdmissionJankari.BL
{
    public class FormFackageFactor : FactorProvider
    {
        private DbWrapper _objDataWrapper;
        private DataSet _dataSet;
        private int _i;
      
        public override List<PackageFactor> GetAllFactor()
        {
            _dataSet = new DataSet();
            var objPackageFactorList = new List<PackageFactor>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetFactorList");
                objPackageFactorList = BindPackageFactor(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllFactor in FormFackageFactor.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objPackageFactorList;
        }

        public override List<PackageFactor> GetFactorById(int factorId)
        {
            _dataSet = new DataSet();
            var objPackageFactorList = new List<PackageFactor>();
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            try
            {
                _objDataWrapper.AddParameter("@AjFactorId", factorId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetFactorList");
                objPackageFactorList = BindPackageFactor(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetFactorById in FormFackageFactor.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objPackageFactorList;
        }

        public override string InsertFactor(string factorName, string factorRemar, bool isChargable, bool isVisible)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            string errMsg = "";

            try
            {
                _objDataWrapper.AddParameter("@AjFactorName", factorName);
                _objDataWrapper.AddParameter("@AjFactorRemark", factorRemar);
                _objDataWrapper.AddParameter("@AjIsChargeable", isChargable);
                _objDataWrapper.AddParameter("@AjIsVisible", isVisible);
                var ObjErrMsg =
                      (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Insert_Update_Package_Factor");
                if (ObjErrMsg != null && ObjErrMsg.Value != null)
                    errMsg = Convert.ToString(ObjErrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertFactor in FormFackageFactor.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return errMsg;
        }

        public override string UpdateFactor(int factorId, string factorName, string factorRemar, bool isChargable, bool isVisible)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            string errMsg = "";

            try
            {
                _objDataWrapper.AddParameter("@AjFactorId", factorId);
                _objDataWrapper.AddParameter("@AjFactorName", factorName);
                _objDataWrapper.AddParameter("@AjFactorRemark", factorRemar);
                _objDataWrapper.AddParameter("@AjIsChargeable", isChargable);
                _objDataWrapper.AddParameter("@AjIsVisible", isVisible);
                var ObjErrMsg =
                      (SqlParameter)(_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Insert_Update_Package_Factor");
                if (ObjErrMsg != null && ObjErrMsg.Value != null)
                    errMsg = Convert.ToString(ObjErrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateFactor in FormFackageFactor.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return errMsg;
        }

        private List<PackageFactor> BindPackageFactor(DataTable datatable)
        {
            var objPackageFactorList = new List<PackageFactor>();

            if (datatable != null && datatable.Rows.Count > 0)
            {
                for (var j = 0; j < datatable.Rows.Count; j++)
                {
                    var objPackageFactor = new PackageFactor
                    {
                        FactorID = (datatable.Rows[j]["AjFactorId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjFactorId"]),
                        FactorName = (datatable.Rows[j]["AjFactorName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjFactorName"]),
                        FactorRemark = (datatable.Rows[j]["AjFactorRemark"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjFactorRemark"]),
                        IsChargeable = (datatable.Rows[j]["AjIsChargeable"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjIsChargeable"]),
                        IsVisible = (datatable.Rows[j]["AjIsVisible"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["AjIsVisible"])
                    };
                    objPackageFactorList.Add(objPackageFactor);
                }
            }

            return objPackageFactorList;

        }

        public override List<PackageMaster> GetAllPackage()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objPackageFactorList = new List<PackageMaster>();
            try
            {
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_PackageList");
                objPackageFactorList = BindPackageDetails(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllPackage in FormFackageFactor.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objPackageFactorList;
        }

        public override List<PackageMaster> GetPackageById(int packageId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objPackageFactorList = new List<PackageMaster>();
            try
            {
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_PackageList");
                _objDataWrapper.AddParameter("@packageId", packageId);
                objPackageFactorList = BindPackageDetails(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetPackageById in FormFackageFactor.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objPackageFactorList;
        }

        public override List<PackageFactorMaster> GetFactorByPackageId(int pacakgeId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objPackageFactorList = new List<PackageFactorMaster>();
            try
            {
                _objDataWrapper.AddParameter("@PackageId", pacakgeId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_GetFactorByPackage");
               
                objPackageFactorList = BindFactorByPackage(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetFactorByPackageId in FormFackageFactor.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objPackageFactorList;
        }

        public override string InsertPackage(string packageName,int courseId, string factorId, bool isChargable, bool isVisible, int amount)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            string errMsg = "";
            try
            {
                _objDataWrapper.AddParameter("@PackageName", packageName);
                _objDataWrapper.AddParameter("@FactorId", factorId);
                _objDataWrapper.AddParameter("@IsVisible", isVisible);
                _objDataWrapper.AddParameter("@IsPackageChargeable", isChargable);
                _objDataWrapper.AddParameter("@PackageAmount", amount);
                _objDataWrapper.AddParameter("@CourseId", courseId);
                var ObjErrMsg =
                      (SqlParameter)(_objDataWrapper.AddParameter("@errMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_InsertPackageDetails");
                if (ObjErrMsg != null && ObjErrMsg.Value != null)
                    errMsg = Convert.ToString(ObjErrMsg.Value);
            }
            catch(Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertPackage in FormFackageFactor.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

            return errMsg;
        }

        public override string UpdatePackage(int packageId, int courseId, string packageName, string factorId, bool isChargable, bool isVisible, int amount)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            string errMsg = "";
            try
            {
                _objDataWrapper.AddParameter("@AjPackageId", packageId);
                _objDataWrapper.AddParameter("@PackageName", packageName);
                _objDataWrapper.AddParameter("@FactorId", factorId);
                _objDataWrapper.AddParameter("@IsVisible", isVisible);
                _objDataWrapper.AddParameter("@IsPackageChargeable", isChargable);
                _objDataWrapper.AddParameter("@PackageAmount", amount);
                _objDataWrapper.AddParameter("@CourseId", courseId);
                var ObjErrMsg =
                      (SqlParameter)(_objDataWrapper.AddParameter("@errMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_UpdatePackageDetails");
                if (ObjErrMsg != null && ObjErrMsg.Value != null)
                    errMsg = Convert.ToString(ObjErrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdatePackage in FormFackageFactor.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

            return errMsg;
        }

        public override string DeletePackageFactor(int packageId, int factorId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            string errMsg = "";
            try
            {
                _objDataWrapper.AddParameter("@AjPackageId", packageId);
                _objDataWrapper.AddParameter("@FactorId", factorId);
                var ObjErrMsg =
                      (SqlParameter)(_objDataWrapper.AddParameter("@errMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_DeletePackageFactor");
                if (ObjErrMsg != null && ObjErrMsg.Value != null)
                    errMsg = Convert.ToString(ObjErrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing DeletePackageFactor in FormFackageFactor.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

            return errMsg;
        }

        public override string InsertPackageFactor(int packageId, int factorId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            string errMsg = "";
            try
            {
                _objDataWrapper.AddParameter("@PackageId", packageId);
                _objDataWrapper.AddParameter("@FactorId", factorId);
                var ObjErrMsg =
                      (SqlParameter)(_objDataWrapper.AddParameter("@errMsg", "", SqlDbType.VarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_InsertPackageFactor");
                if (ObjErrMsg != null && ObjErrMsg.Value != null)
                    errMsg = Convert.ToString(ObjErrMsg.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertPackageFactor in FormFackageFactor.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }

            return errMsg;
        }

        private List<PackageMaster> BindPackageDetails(DataTable datatable)
        {
            var objPackageFactorList = new List<PackageMaster>();
            try
            {
                if (datatable != null && datatable.Rows.Count > 0)
                {
                    for (var j = 0; j < datatable.Rows.Count; j++)
                    {
                        var objPackageFactor = new PackageMaster
                        {
                            PackageId = (datatable.Rows[j]["AjPackageId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjPackageId"]),
                            PackageName = (datatable.Rows[j]["PackageName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["PackageName"]),
                            PackageAmount = (datatable.Rows[j]["PackageAmount"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["PackageAmount"]),
                            IsChargeable = (datatable.Rows[j]["IsPackageChargeable"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["IsPackageChargeable"]),
                            IsVisible = (datatable.Rows[j]["IsVisible"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[j]["IsVisible"]),
                            courseId= (datatable.Rows[j]["AjCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjCourseId"]),
                            CourseName= (datatable.Rows[j]["AjCourseName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjCourseName"])
                        };
                        objPackageFactorList.Add(objPackageFactor);
                    }
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindPackageDetails in FormFackageFactor.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objPackageFactorList;
        }

        private List<PackageFactorMaster> BindFactorByPackage(DataTable datatable)
        {
            var objPackageFactorList = new List<PackageFactorMaster>();
            try
            {
                if (datatable != null && datatable.Rows.Count > 0)
                {
                               
                        var objPackageFactor = new PackageFactorMaster();
                        var objPackage = new PackageMaster();
                        var objFactorMasterList = new List<PackageFactor>();
                        var objPackageFactorID = new List<int>();
                        objPackage.PackageId = (datatable.Rows[0]["AjPackageId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[0]["AjPackageId"]);
                        objPackage.PackageName = (datatable.Rows[0]["PackageName"] is DBNull) ? "" : Convert.ToString(datatable.Rows[0]["PackageName"]);
                        objPackage.PackageAmount = (datatable.Rows[0]["PackageAmount"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[0]["PackageAmount"]);
                        objPackage.IsChargeable = (datatable.Rows[0]["IsPackageChargeable"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[0]["IsPackageChargeable"]);
                        objPackage.IsVisible = (datatable.Rows[0]["IsVisible"] is DBNull) ? false : Convert.ToBoolean(datatable.Rows[0]["IsVisible"]);
                        objPackage.courseId = (datatable.Rows[0]["AjCourseId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[0]["AjCourseId"]);
                        objPackage.CourseName = (datatable.Rows[0]["AjCourseName"] is DBNull) ? null : Convert.ToString(datatable.Rows[0]["AjCourseName"]);
                        objPackageFactor.objPackage = objPackage;
                    for (var j = 0; j < datatable.Rows.Count; j++)
                    {
                        var packageFactorId= (datatable.Rows[j]["AjPackageFactorId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjPackageFactorId"]);
                        var objPackageMaster = new PackageFactor();
                        objPackageMaster.FactorID = (datatable.Rows[j]["FactorId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["FactorId"]);
                        objPackageMaster.FactorName = (datatable.Rows[j]["AjFactorName"] is DBNull) ? "" : Convert.ToString(datatable.Rows[j]["AjFactorName"]);
                        objPackageFactorID.Add(packageFactorId);
                        objFactorMasterList.Add(objPackageMaster);
                    }
                    objPackageFactor.ObjPackageFactor = objFactorMasterList;
                    objPackageFactor.PackageFactorId = objPackageFactorID;
                    objPackageFactorList.Add(objPackageFactor);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing BindFactorByPackage in FormFackageFactor.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objPackageFactorList;
        }
    }
}

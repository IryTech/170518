using System.Collections.Generic;
using IryTech.AdmissionJankari.BO;
using IryTech.AdmissionJankari.DAL;
using System.Data;
using System.Data.SqlClient;
using System;


namespace IryTech.AdmissionJankari.BL
{
    public class Bank : BankProvider
    {
        private DbWrapper _objDataWrapper;
        private DataSet _dataSet;
        private int _i;

        public override int InsertBankInfo(BankDetailsProperty objBankProperty, int createdBy, out string errmsg,
                out int bankId)
        {

            bankId = 0;
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = string.Empty;
            try
            {
                _objDataWrapper.AddParameter("@BankName", objBankProperty.BankName);
                _objDataWrapper.AddParameter("@BankShortName", objBankProperty.BankShortName);
                _objDataWrapper.AddParameter("@BankUrl", objBankProperty.BankUrl);
                _objDataWrapper.AddParameter("@BankAddress", objBankProperty.BankAddress);
                _objDataWrapper.AddParameter("@BankPhoneNo", objBankProperty.BankPhoneNo);
                _objDataWrapper.AddParameter("@BankLogo", objBankProperty.BankLogo);
                _objDataWrapper.AddParameter("@BankShortDescription", objBankProperty.BankShortDescription);
                _objDataWrapper.AddParameter("@BankContactPerson", objBankProperty.BankContactPerson);
                _objDataWrapper.AddParameter("@BankContactPersonDesignation", objBankProperty.BankContactPersonDesignation);
                _objDataWrapper.AddParameter("@BankContactPersonEmailId", objBankProperty.BankContactPersonEmailId);
                _objDataWrapper.AddParameter("@BankContactPersonMobile", objBankProperty.BankContactPersonMobile);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
                var objBankId =
                       (SqlParameter)(_objDataWrapper.AddParameter("@BankId", objBankProperty.BankId, SqlDbType.Int, ParameterDirection.InputOutput));

                              var objErrmsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateBankDetails");
                if (objErrmsg != null && objErrmsg.Value != null)
                    errmsg = Convert.ToString(objErrmsg.Value);
                if (objBankId != null && objBankId.Value != null)
                    bankId = Convert.ToInt32(objBankId.Value);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertBankInfo in Bank.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

            return _i;
        }
        public override int UpdateBankInfo(BankDetailsProperty objBankProperty, int createdBy, out string errmsg)
               
        {

           
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = string.Empty;
            try
            {
                _objDataWrapper.AddParameter("@BankId", objBankProperty.BankId);
                _objDataWrapper.AddParameter("@BankName", objBankProperty.BankName);
                _objDataWrapper.AddParameter("@BankShortName", objBankProperty.BankShortName);
                _objDataWrapper.AddParameter("@BankUrl", objBankProperty.BankUrl);
                _objDataWrapper.AddParameter("@BankAddress", objBankProperty.BankAddress);
                _objDataWrapper.AddParameter("@BankPhoneNo", objBankProperty.BankPhoneNo);
                _objDataWrapper.AddParameter("@BankLogo", objBankProperty.BankLogo);
                _objDataWrapper.AddParameter("@BankShortDescription", objBankProperty.BankShortDescription);
                _objDataWrapper.AddParameter("@BankContactPerson", objBankProperty.BankContactPerson);
                _objDataWrapper.AddParameter("@BankContactPersonDesignation", objBankProperty.BankContactPersonDesignation);
                _objDataWrapper.AddParameter("@BankContactPersonEmailId", objBankProperty.BankContactPersonEmailId);
                _objDataWrapper.AddParameter("@BankContactPersonMobile", objBankProperty.BankContactPersonMobile);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
               
                var objErrmsg =
      (SqlParameter)
      (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateBankDetails");
                if (objErrmsg != null && objErrmsg.Value != null)
                    errmsg = Convert.ToString(objErrmsg.Value);
               
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateBankInfo in Bank.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

            return _i;
        }
        public override List<BankDetailsProperty> GetAllBankList()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objBankList = new List<BankDetailsProperty>();
            try
            {
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetBankList");
                objBankList = BindBankList(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetAllBankList in Bank.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objBankList;
        }
        public override List<BankDetailsProperty> GetBankListById(int bankId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objBankList = new List<BankDetailsProperty>();
            try
            {
                _objDataWrapper.AddParameter("@BankId", bankId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetBankList");
                objBankList = BindBankList(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetBankListById in Bank.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objBankList;
        }
        public override List<BankDetailsProperty> GetBankListByShortName(string bankShortName)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objBankList = new List<BankDetailsProperty>();
            try
            {
                _objDataWrapper.AddParameter("@BankShortName", bankShortName);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetBankList");
                objBankList = BindBankList(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetBankListByShortName in Bank.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objBankList;
        }
        public override List<BankDetailsProperty> GetBankListByName(string bankName)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objBankList = new List<BankDetailsProperty>();
            try
            {
                _objDataWrapper.AddParameter("@BankName", bankName);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetBankList");
                objBankList = BindBankList(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetBankListByName in Bank.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objBankList;
        }
        public override List<StudyPlace> GetStudyPlace()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objBankList = new List<StudyPlace>();
            try
            {
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetStudyPlace");
                objBankList = BindStudyPlaceList(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetStudyPlace in Bank.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objBankList;
        }
        public override List<LoanRange> GetLoanRange()
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objBankList = new List<LoanRange>();
            try
            {
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetLoanRange");
                objBankList = BindLoanRange(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetLoanRange in Bank.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objBankList;
        }
        private List<BankDetailsProperty> BindBankList(DataTable datatable)
        {
            var objBankList = new List<BankDetailsProperty>();
            try
            {
                if (datatable.Rows.Count > 0)
                {
                    for (var j = 0; j < datatable.Rows.Count; j++)
                    {
                        var objBankListProperty = new BankDetailsProperty
                             {
                                 BankId = Convert.ToInt32(datatable.Rows[j]["AjBankId"]),
                                 BankName = Convert.ToString(datatable.Rows[j]["AJBankName"]),
                                 BankShortName = Convert.ToString(datatable.Rows[j]["AjBankShortName"]),
                                 BankShortDescription = Convert.ToString(datatable.Rows[j]["AjBankShortDescription"]),
                                 BankPhoneNo = Convert.ToString(datatable.Rows[j]["AjBankPhoneNo"]),
                                 BankUrl = Convert.ToString(datatable.Rows[j]["AjBankUrl"]),
                                 BankContactPersonMobile = Convert.ToString(datatable.Rows[j]["AjBankContactPersonMobile"]),
                                 BankContactPerson = Convert.ToString(datatable.Rows[j]["AjBankContactPerson"]),
                                 BankContactPersonDesignation = Convert.ToString(datatable.Rows[j]["AjBankContactPersonDesignation"]),
                                 BankContactPersonEmailId = Convert.ToString(datatable.Rows[j]["AjBankContactPersonEmailId"]),
                                 BankAddress = Convert.ToString(datatable.Rows[j]["AjBankAddress"]),
                                 BankLogo = Convert.ToString(datatable.Rows[j]["AjBankLogo"])
                             };
                        objBankList.Add(objBankListProperty);
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
                const string addInfo = "Error while executing BindBankList in Bank.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objBankList;
        }
        private List<StudyPlace> BindStudyPlaceList(DataTable datatable)
        {
            var objStudyPlace = new List<StudyPlace>();
            try
            {
                if (datatable.Rows.Count > 0)
                {
                    for (var j = 0; j < datatable.Rows.Count; j++)
                    {
                        var objStudyPlaceProperty = new StudyPlace
                             {
                                 StudyPlaceId = Convert.ToInt32(datatable.Rows[j]["AjStudyPlaceId"]),
                                 StudyPlaceName = Convert.ToString(datatable.Rows[j]["AjStudyPlaceName"]),

                             };
                        objStudyPlace.Add(objStudyPlaceProperty);
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
                const string addInfo = "Error while executing BindStudyPlaceList in Bank.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objStudyPlace;
        }
        private List<LoanRange> BindLoanRange(DataTable datatable)
        {
            var objLoanRange = new List<LoanRange>();
            try
            {
                if (datatable.Rows.Count > 0)
                {
                    for (var j = 0; j < datatable.Rows.Count; j++)
                    {
                        var objLoanRangeProperty = new LoanRange
                             {
                                 LoanRangeId = Convert.ToInt32(datatable.Rows[j]["AjLoanRangeId"]),
                                 StudyPlaceId = Convert.ToInt32(datatable.Rows[j]["AjStudyPlaceId"]),
                                 Amount = Convert.ToString(datatable.Rows[j]["AjAmount"]),

                             };
                        objLoanRange.Add(objLoanRangeProperty);
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
                const string addInfo = "Error while executing BindStudyPlaceList in Bank.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objLoanRange;
        }

        public override int InsertLoanInfo(LoanDetailsProperty objLoanDetailsProperty, int createdBy, out string errmsg)
               
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = string.Empty;
            try
            {
                _objDataWrapper.AddParameter("@BankId", objLoanDetailsProperty.BankId);
                   _objDataWrapper.AddParameter("@LoanRangeId", objLoanDetailsProperty.LoanRangeId);
                   _objDataWrapper.AddParameter("@StudyPlaceId", objLoanDetailsProperty.StudyPlaceId);
                _objDataWrapper.AddParameter("@Eligibilty", objLoanDetailsProperty.Eligibilty);
                _objDataWrapper.AddParameter("@Security", objLoanDetailsProperty.Security);
                _objDataWrapper.AddParameter("@Margin", objLoanDetailsProperty.Margin);
                _objDataWrapper.AddParameter("@ProcessingFees", objLoanDetailsProperty.ProcessingFees);
                _objDataWrapper.AddParameter("@ProcessingTime", objLoanDetailsProperty.ProcessingTime);
                _objDataWrapper.AddParameter("@RateOfInterest", objLoanDetailsProperty.RateOfInterest);
                _objDataWrapper.AddParameter("@Remark", objLoanDetailsProperty.Remark);
                _objDataWrapper.AddParameter("@RepaymentDuration", objLoanDetailsProperty.RepaymentDuration);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);
               
                var objErrmsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateLoanInfoDetails");
                if (objErrmsg != null && objErrmsg.Value != null)
                    errmsg = Convert.ToString(objErrmsg.Value);
                
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing InsertLoanInfo in Bank.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

            return _i;
        }
        public override int UpdateLoanInfo(LoanDetailsProperty objLoanDetailsProperty, int createdBy, out string errmsg)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            errmsg = string.Empty;
            try
            {
                _objDataWrapper.AddParameter("@LoanId", objLoanDetailsProperty.LoanId);
                _objDataWrapper.AddParameter("@BankId", objLoanDetailsProperty.BankId);
                _objDataWrapper.AddParameter("@LoanRangeId", objLoanDetailsProperty.LoanRangeId);
                _objDataWrapper.AddParameter("@StudyPlaceId", objLoanDetailsProperty.StudyPlaceId);
                _objDataWrapper.AddParameter("@Eligibilty", objLoanDetailsProperty.Eligibilty);
                _objDataWrapper.AddParameter("@Security", objLoanDetailsProperty.Security);
                _objDataWrapper.AddParameter("@Margin", objLoanDetailsProperty.Margin);
                _objDataWrapper.AddParameter("@ProcessingFees", objLoanDetailsProperty.ProcessingFees);
                _objDataWrapper.AddParameter("@ProcessingTime", objLoanDetailsProperty.ProcessingTime);
                _objDataWrapper.AddParameter("@RateOfInterest", objLoanDetailsProperty.RateOfInterest);
                _objDataWrapper.AddParameter("@Remark", objLoanDetailsProperty.Remark);
                _objDataWrapper.AddParameter("@RepaymentDuration", objLoanDetailsProperty.RepaymentDuration);
                _objDataWrapper.AddParameter("@CreatedBy", createdBy);

                var objErrmsg =
                    (SqlParameter)
                    (_objDataWrapper.AddParameter("@ErrMsg", "", SqlDbType.NVarChar, ParameterDirection.Output, 128));
                _i = _objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertUpdateLoanInfoDetails");
                if (objErrmsg != null && objErrmsg.Value != null)
                    errmsg = Convert.ToString(objErrmsg.Value);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing UpdateLoanInfo in Bank.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }

            return _i;
        }

        public override List<LoanDetailsProperty> GetLoanListByBankId(int bankId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objLoanList = new List<LoanDetailsProperty>();
            try
            {
                _objDataWrapper.AddParameter("@BankId", bankId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetLoanList");
                objLoanList = BindLoanList(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetLoanListByBankId in Bank.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objLoanList;
        }
        public override List<LoanDetailsProperty> GetLoanListByLoan(int LoanId)
        {
            _objDataWrapper = new DbWrapper(Common.CnnString, CommandType.StoredProcedure);
            _dataSet = new DataSet();
            var objLoanList = new List<LoanDetailsProperty>();
            try
            {
                _objDataWrapper.AddParameter("@LoanId", LoanId);
                _dataSet = _objDataWrapper.ExecuteDataSet("Aj_Proc_GetLoanList");
                objLoanList = BindLoanList(_dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo = "Error while executing GetLoanListByLoan in Bank.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objLoanList;
        }
        private List<LoanDetailsProperty> BindLoanList(DataTable datatable)
        {
            var objLoanList = new List<LoanDetailsProperty>();
            try
            {
                if (datatable.Rows.Count > 0)
                {
                    for (var j = 0; j < datatable.Rows.Count; j++)
                    {
                        var objLoanListProperty = new LoanDetailsProperty
                        {
                            BankId = (datatable.Rows[j]["AjBankId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjBankId"]),
                            LoanId = (datatable.Rows[j]["AjLoanId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjLoanId"]),
                            LoanRangeId =(datatable.Rows[j]["AjLoanRangeId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjLoanRangeId"]),
                            StudyPlaceId = (datatable.Rows[j]["AjStudyPlaceId"] is DBNull) ? 0 : Convert.ToInt32(datatable.Rows[j]["AjStudyPlaceId"]),
                            Eligibilty = (datatable.Rows[j]["AjEligibilty"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjEligibilty"]),
                            Security =(datatable.Rows[j]["AjSecurity"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjSecurity"]),
                            RepaymentDuration = (datatable.Rows[j]["AjRepaymentDuration"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjRepaymentDuration"]),
                            Remark = (datatable.Rows[j]["AjRemark"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjRemark"]),
                            RateOfInterest = (datatable.Rows[j]["AjRateOfInterest"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjRateOfInterest"]),
                            ProcessingFees = (datatable.Rows[j]["AjProcessingFees"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjProcessingFees"]),
                            ProcessingTime = (datatable.Rows[j]["AjProcessingTime"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjProcessingTime"]),
                            Margin =(datatable.Rows[j]["AjMargin"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjMargin"]),
                            Amount =(datatable.Rows[j]["AjAmount"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjAmount"]),
                            StudyPlaceName =(datatable.Rows[j]["AjStudyPlaceName"] is DBNull) ? null : Convert.ToString(datatable.Rows[j]["AjStudyPlaceName"]) 
                        };
                        objLoanList.Add(objLoanListProperty);
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
                const string addInfo = "Error while executing BindBankList in Bank.cs  :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
            return objLoanList;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IryTech.AdmissionJankari.BL;
using IryTech.AdmissionJankari.Components;
using IryTech.AdmissionJankari.Components.Web.Controls;

namespace IryTech.AdmissionJankari20.Web.Account
{
    public partial class PaymentConfirmation : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            if (Request["mode"] != null)
            {

                SucessMsg();
            }
            else
            {
                if (Request.QueryString.Count > 2)
                {

                    populate(sender, e);
                }
                else
                {
                    FailureMsg();
                }

            }
            CheckBannerCount();
        }

        private void SucessMsg()
        {
            var totalAmountPayed = 0;
            var objSecurePage = new SecurePage();
            var orderNumber = "ADMJPROD" + System.DateTime.Now.Year + "UID" + objSecurePage.LoggedInUserId.ToString();
            var objCarProduct = new Common().GetProductForCart(0, orderNumber,0);
            for (var i = 0; i <= objCarProduct.Tables[0].Rows.Count - 1; i++)
            {
                totalAmountPayed = Convert.ToInt32(objCarProduct.Tables[0].Rows[i]["PayProductAmount"]) +
                                   totalAmountPayed;

            }
            lblAmount.Text = string.Format("Rs. {0}", totalAmountPayed);
            lblName.Text = objSecurePage.LoggedInUserName;
            sucessMsg.Visible = true;
        }

        private void UpdateUserTransctionalDetails(string euserEmailId, string orderNumb, string euserId,string id=null)
        {
            var objSecurePage = new SecurePage();
            var objCrypto = new ClsCrypto(ClsSecurity.GetPasswordPhrase(Common.PassPhraseOne, Common.PassPhraseTwo));
            var objMailTemplates = new MailTemplates();
            var objSecure = new SecurePage();
            euserEmailId = euserEmailId.Replace(" ", "+");
            string emailId = Convert.ToString(objCrypto.Decrypt(euserEmailId));
            try
            {
                euserId = euserId.Replace(" ", "+");
                string userId = Convert.ToString(objCrypto.Decrypt(euserId));
                string orderNumber = orderNumb.Replace(" ", "+");
                orderNumber = Convert.ToString(objCrypto.Decrypt(orderNumber));
                var i = new Common().UpdateOrderIdForProduct(objSecurePage.LoggedInUserId, false, Request["id"] != null ? null : orderNumber,
                                                                 "Online",
                                                                 Request["id"] != null ? Convert.ToInt32(Request["id"]) : 0);
                if (i > 0)
                {
                    var userDetails = UserManagerProvider.Instance.GetUserListById(Convert.ToInt32(userId));
                    var sp = userDetails.First();
                    objSecure.LoggedInUserEmailId = emailId;
                    objSecure.LoggedInUserId = sp.UserId;
                    objSecure.LoggedInUserName = Common.GetStringProperCase(sp.UserFullName);
                    objSecure.LoggedInUserType = sp.UserCategoryId;
                    objSecure.LoggedInUserMobile = sp.MobileNo;
                    lblName.Text = Common.GetStringProperCase(sp.UserFullName);
                    var mail = new MailMessage
                        {
                            From = new MailAddress(ApplicationSettings.Instance.Email),
                            Subject = "AdmissionJankari:Product Payment confirmation for order number:" + orderNumber
                        };
                    var objCarProduct = new Common().GetProductForCart(0, id == null?orderNumber:null,
                                                                       id == null ? 0 : Convert.ToInt32(id));
                    var body = objMailTemplates.SendProductConfirmationMailAfterSuccess(objSecurePage.LoggedInUserName,
                                                                                        orderNumber,
                                                                                        objCarProduct.Tables[0],
                                                                                        "Online");
                    mail.Body = body;
                    mail.To.Add(emailId);
                    mail.Bcc.Add(ClsSingelton.bccDirectAdmission);
                    Utils.SendMailMessageAsync(mail);
                    sucessMsg.Visible = true;


                }
                else
                {
                    failureMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                failureMsg.Visible = true;
                if (ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + ex.ToString();
                }
                const string addInfo =
                    "Error while executing UpdateUserTransctionalDetails in PaymentConformation.aspx for user :: -> ";
                var objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);
            }
        }

        private void FailureMsg()
        {
            var objSecurePage = new SecurePage();
            lblName.Text = objSecurePage.LoggedInUserName;
            failureMsg.Visible = true;
        }

        private void populate(Object sender, EventArgs e)
        {
            string WorkingKey, Order_Id, Merchant_Id, Amount, AuthDesc, Checksum, newChecksum, status;

            //Assign following values to send it to verifychecksum function.
            //put in the 32 bit working key in the quotes provided here
            WorkingKey = "xodv4qh9rxw8kscaqcqbmpzx43xxxn43";
            Merchant_Id = "M_shi18022_18022";
            Order_Id = Request.Form["Order_Id"];

            Amount = Request.Form["Amount"];
            lblAmount.Text = Amount;
            AuthDesc = Request.Form["AuthDesc"];

            ////////////////////////  ERROR...This variable(status) is not declared anywhere 

            status = Request.Form["Status"];

            ////////////////////// This comment is given by Majestic People, Coimbatore
            ////////////////////// The following variable "checksum" is declared as "Checksum" at the top

            Checksum = Request.Form["Checksum"];



            //Checksum = verifychecksum(Merchant_Id , Order_Id, Amount , AuthDesc ,WorkingKey, Checksum);
            Checksum = verifychecksum(Merchant_Id, Order_Id, Amount, AuthDesc, WorkingKey, Checksum);


            if ((Checksum == "true") && (AuthDesc == "Y"))
            {

                /* 
                    Here you need to put in the routines for a successful 
                     transaction such as sending an email to customer,
                     setting database status, informing logistics etc etc
                */
                if (Request.QueryString.Count > 2)
                {
                    UpdateUserTransctionalDetails(Convert.ToString(Request.QueryString[0]),
                                                  Convert.ToString(Request.QueryString[1]),
                                                  Convert.ToString(Request.QueryString[2]));
                }
                else
                {
                    UpdateUserTransctionalDetails(Convert.ToString(Request.QueryString[0]),
                                                  Convert.ToString(Request.QueryString[1]),
                                                  Convert.ToString(Request.QueryString[2]), Convert.ToString(Request.QueryString[3]));
                }
            }
            else if ((Checksum == "true") && (AuthDesc == "N"))
            {
                FailureMsg();
                /*
                    Here you need to put in the routines for a failed
                    transaction such as sending an email to customer
                    setting database status etc etc
                */
            }
            else if ((Checksum == "true") && (AuthDesc == "B"))
            {

                /*
                    Here you need to put in the routines/e-mail for a  "Batch Processing" order
                    This is only if payment for this transaction has been made by an American Express Card
                    since American Express authorisation status is available only after 5-6 hours by mail from ccavenue and at the "View Pending Orders"
             */
                if (Request.QueryString.Count > 2)
                {
                    UpdateUserTransctionalDetails(Convert.ToString(Request.QueryString[0]),
                                                  Convert.ToString(Request.QueryString[1]),
                                                  Convert.ToString(Request.QueryString[2]));
                }
                else
                {
                    UpdateUserTransctionalDetails(Convert.ToString(Request.QueryString[0]),
                                                    Convert.ToString(Request.QueryString[1]),
                                                    Convert.ToString(Request.QueryString[2]), Convert.ToString(Request.QueryString[3]));
                    
                }
            }
            else
            {
                FailureMsg();
                /*
                    Here you need to simply ignore this and dont need
                    to perform any operation in this condition
                */
            }
        }

        public string verifychecksum(string MerchantId, string OrderId, string Amount, string AuthDesc,
                                     string WorkingKey, string checksum)
        {
            string str, retval, adlerResult;
            long adler;
            str = MerchantId + "|" + OrderId + "|" + Amount + "|" + AuthDesc + "|" + WorkingKey;
            adler = 1;
            adlerResult = adler32(adler, str);

            if (string.Compare(adlerResult, checksum, true) == 0)
            {
                retval = "true";
            }
            else
            {
                retval = "false";
            }
            return retval;
        }

        private string adler32(long adler, string strPattern)
        {
            long BASE;
            long s1, s2;
            char[] testchar;
            long intTest = 0;

            BASE = 65521;
            s1 = andop(adler, 65535);
            s2 = andop(cdec(rightshift(cbin(adler), 16)), 65535);

            for (int n = 0; n < strPattern.Length; n++)
            {

                testchar = (strPattern.Substring(n, 1)).ToCharArray();
                intTest = (long) testchar[0];
                s1 = (s1 + intTest)%BASE;
                s2 = (s2 + s1)%BASE;
            }
            return (cdec(leftshift(cbin(s2), 16)) + s1).ToString();
        }

        private long power(long num)
        {
            long result = 1;
            for (int i = 1; i <= num; i++)
            {
                result = result*2;
            }
            return result;
        }

        private long andop(long op1, long op2)
        {
            string op, op3, op4;
            op = "";

            op3 = cbin(op1);
            op4 = cbin(op2);

            for (int i = 0; i < 32; i++)
            {
                op = op + "" + ((long.Parse(op3.Substring(i, 1))) & (long.Parse(op4.Substring(i, 1))));
            }
            return cdec(op);
        }

        private string cbin(long num)
        {
            string bin = "";
            double num2 = num;
            do
            {
                bin = (((num2%2)) + bin).ToString();
                num2 = (long) Math.Floor(num2/2);
            } while (!(num2 == 0));

            long tempCount = 32 - bin.Length;

            for (int i = 1; i <= tempCount; i++)
            {
                bin = "0" + bin;
            }
            return bin;
        }

        private string leftshift(string str, long num)
        {
            long tempCount = 32 - str.Length;

            for (int i = 1; i <= tempCount; i++)
            {

                str = "0" + str;
            }

            for (int i = 1; i <= num; i++)
            {
                str = str + "0";
                str = str.Substring(1, str.Length - 1);
            }
            return str;
        }

        private string rightshift(string str, long num)
        {

            for (int i = 1; i <= num; i++)
            {
                str = "0" + str;
                str = str.Substring(0, str.Length - 1);
            }
            return str;
        }

        private long cdec(string strNum)
        {
            long dec = 0;
            for (int n = 0; n < strNum.Length; n++)
            {
                dec = dec + (long) (long.Parse(strNum.Substring(n, 1))*power(strNum.Length - (n + 1)));
            }
            return dec;
        }

        private void CheckBannerCount()
        {
            var collegeBasicData = CollegeProvider.Instance.GetCollegeListByUserId(new SecurePage().LoggedInUserId);
            if (collegeBasicData.Count > 0)
            {
                var query = collegeBasicData.Select(result => new
                    {
                        result.CollegeIdBranchId,

                    }).First();
                var objDataSet = new Common().GetBannerListByCollegeId(Convert.ToInt32(query.CollegeIdBranchId), "COUNT");
                divBanner.Visible = objDataSet.Rows.Count > 0;
            }

        }
    }
}
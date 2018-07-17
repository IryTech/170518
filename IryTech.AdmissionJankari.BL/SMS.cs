using System;

using System.Text;
using System.Net;
using System.IO;

namespace IryTech.AdmissionJankari.BL
{
   public class SMS
    {
        // Method to send the SMS
        public bool SendSMS(string MobileNo, string Message)
        {
            string responsedata = "";
            string url = string.Empty;
            bool Sucess = false;
            try
            {
                url = "http://www.myvaluefirst.com/smpp/sendsms?username=quorconsultancy&password=quorom2consultancy1&to=" + MobileNo + " + &from=ajankari&text=" + Message + "";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader responseReader = new StreamReader(response.GetResponseStream());
                    responsedata = responseReader.ReadToEnd();
                    responseReader.Close();
                    Sucess = true;
                }
                else
                {
                    Sucess = false;
                }
            }
            catch (Exception Ex)
            {
                string err = Ex.Message;
                if (Ex.InnerException != null)
                {
                    err = err + " :: Inner Exception :- " + Ex.InnerException.Message;
                }
                string addInfo = "Error While sending the SMS in the CLSSMS:: -> ";
                ClsExceptionPublisher objPub = new ClsExceptionPublisher();
                objPub.Publish(err, addInfo);

            }
            return Sucess;
        }
    }
}

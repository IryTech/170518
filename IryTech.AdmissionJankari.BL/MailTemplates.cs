using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;



namespace IryTech.AdmissionJankari.BL
{
   public class MailTemplates
    {
       Common _objClsCommon;

       public string SendValidationMailForTheDirectAdmission(string Url, string Name, string FormNumber, string TranctionDetails)
       {
           const string validateKey1 = "info@AdmissionJankari.com";
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; width:100%; color: #666666;  border: solid 2px #7f9db9;  font-family: Arial, Verdana'>");
           sbLetter.Append("<table width='100%' align='left' border='0' cellspacing='0' style='font-family:Verdana; height:15px; font-size:10pt;'><tr><td align='left' valign='top' style='background-color:#3b5998;' > ");
           sbLetter.Append("</td></tr><tr><td valign='middle' style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none'><span style='vertical-align:middle;font-size: 2em !important;color:white;font-family:Agency FB;font-weight:bold;'>Admissionjankari.com</span></a></td></tr><tr><td>&nbsp;</td></tr></table><div style='padding:4px;'>");
           sbLetter.Append(" <br />Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(Name));
           sbLetter.Append("</strong>");
           sbLetter.Append(", <br /><br />Congratulations on successfully filling up Combined Application Form on <a href='http://www.admissionjankari.com/'style='text-decoration: none'><strong><i> AdmissionJankari.com</i></strong></a>");
           sbLetter.Append("  . We have received your request for submission of your application form.");
           sbLetter.Append("<br /><br />");
           sbLetter.Append("Your Application Form Number is: ");
           sbLetter.Append("<strong>");
           sbLetter.Append(FormNumber);
           sbLetter.Append("</strong>");
           sbLetter.Append("<br /><br />");
           sbLetter.Append(TranctionDetails);
           sbLetter.Append("You can email us a soft copy of the photograph and document at <a href='mailto:info@admissionjankari.com'>info@admissionjankari.com </a> ");
           sbLetter.Append("<br/>");
           sbLetter.Append("<br/>");
           sbLetter.Append("  Please do mention your Application Form Number in the email as a subject and also for further references");
           //sbLetter.Append("</strong><br /><br />");
           sbLetter.Append("</strong>");
           sbLetter.Append("<br /> <br /> If you have any questions or doubts, please do get in touch with us at <a href='mailto:info@admissionjankari.com'><strong>");
           sbLetter.Append(validateKey1);
           sbLetter.Append(" </a>,alternatively you can call us and speak to our counselor on +91-8800 56 77 33");
           sbLetter.Append("</strong><br />");
           sbLetter.Append("<br /> <br /><strong>Regards,<br /><br />Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></body></html>");
           return sbLetter.ToString();
       }

       public string MailBodyForRegistation(string userName,string emailId,string password)
       {
           StringBuilder sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append("<br /><div style='padding-left:15px; padding-bottom:10px;'>Dear ");
           sbLetter.Append(userName);
            sbLetter.Append(",<br /><br />Thank you for registering and Welcome to <a href='http://www.admissionjankari.com/'style='text-decoration: none'><strong><i>AdmissionJankari.com</i></strong></a>.");
           sbLetter.Append("<br /><br />Your account membership information is:<br /> ");
           sbLetter.Append("<br /> LoginId : <strong>");
           sbLetter.Append(emailId);
           sbLetter.Append("</strong><br />");
           sbLetter.Append(" Password : <strong>");
           sbLetter.Append(password);
           sbLetter.Append("</strong><br /><br />Your account is ready to use and you can login anytime by using the following link: <strong>");
           sbLetter.Append("<a href='");
           sbLetter.Append(Common.WebUrl);
           sbLetter.Append("http://www.admissionjankari.com/");
           sbLetter.Append("'>  click here");
           sbLetter.Append("</a></strong>");
           sbLetter.Append(ClsSingelton.DirectAdmissionNote);
           sbLetter.Append("<br /> <br /><strong>Regards,<br /><br />Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></div></body></html>");
           return sbLetter.ToString();
       }

       public string MailBodyForGetPassword(string emailId, string password,string userName)
       {
           StringBuilder sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px; padding-bottom:10px;'>Dear <strong> ");
           sbLetter.Append(Common.GetStringProperCase(userName));
           sbLetter.Append("</strong>,<br /><br /> You or someone else has used your email account <strong>(");
           sbLetter.Append(emailId);
           sbLetter.Append(")</strong> either to retrieve or change your password on <a href='http://www.admissionjankari.com/'style='text-decoration: none'><strong>AdmissionJankari.com</strong></a>.");
           sbLetter.Append("<br /> <br />Your password is:<strong> ");
           sbLetter.Append(password);
           sbLetter.Append("<br/><br/>click <a href='");
           sbLetter.Append(Common.WebUrl);
           sbLetter.Append("http://www.admissionjankari.com/");
           sbLetter.Append("'>here");
           sbLetter.Append("</a></strong> to visit your account.");
           sbLetter.Append(ClsSingelton.DirectAdmissionNote);
           sbLetter.Append("<br /><br /><strong>Regards<br />Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:12px;'>Let’s create a knowledgeable nation...</span></div></div></body></html>");

           return sbLetter.ToString();
       }

       public string MailBodyForCommonQuery(string userName,string cousreName,string studentQuery,string mobile)
       {
           StringBuilder sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(userName));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br /> Thank you for posting your query on <strong>");
           sbLetter.Append("<a href='");
           sbLetter.Append(Common.WebUrl);
           sbLetter.Append("http://www.admissionjankari.com/");
           sbLetter.Append("'> AdmissionJankari.com");
           sbLetter.Append("</a></strong>");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Course : </strong>");
           sbLetter.Append(cousreName);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Mobile : </strong>");
           sbLetter.Append(mobile);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Query : </strong>");
           sbLetter.Append(studentQuery);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("Our expert panel will look into your query and reply you as soon as possible. In case of urgency please call us on 08800 56 77 33.");
           sbLetter.Append("<br />");
           sbLetter.Append(ClsSingelton.DirectAdmissionNote);
           sbLetter.Append("<br /> <br /><strong>Regards,<br/>Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></div></body></html>");
           return sbLetter.ToString();
       }

       public string MailBodyForCollegeQuery(string userName, string cousreName, string studentQuery, string mobile,string collegeName)
       {
           StringBuilder sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(userName));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br /> Thank you for posting your query on <strong>");
           sbLetter.Append("<a href='");
           sbLetter.Append(Common.WebUrl);
           sbLetter.Append("http://www.admissionjankari.com/");
           sbLetter.Append("'> AdmissionJankari.com");
           sbLetter.Append("</a></strong>");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Course : </strong>");
           sbLetter.Append(cousreName);
           sbLetter.Append("<br /> <br /><strong>College Name : </strong>");
           sbLetter.Append(collegeName);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Mobile : </strong>");
           sbLetter.Append(mobile);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Query : </strong>");
           sbLetter.Append(studentQuery);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("Our expert panel will look into your query and reply you as soon as possible. In case of urgency please call us on 08800 56 77 33.");
           sbLetter.Append("<br />");
           sbLetter.Append(ClsSingelton.DirectAdmissionNote);
           sbLetter.Append("<br /> <br /><strong>Regards,<br/>Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></div></body></html>");
           return sbLetter.ToString();
       }
       public string MailBodyForUserSponserCollegeQuery(string userName, string cousreName,string streamName, string studentQuery, string mobile, string collegeName)
       {
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(userName));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br /> Thank you for posting your query on <strong>");
           sbLetter.Append("<a href='");
           sbLetter.Append(Common.WebUrl);
           sbLetter.Append("http://www.admissionjankari.com/");
           sbLetter.Append("'> AdmissionJankari.com");
           sbLetter.Append("</a></strong>");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Course : </strong>");
           sbLetter.Append(cousreName);
          
           sbLetter.Append("<br /> <br /><strong>College Name : </strong>");
           sbLetter.Append(collegeName);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Stream : </strong>");
           sbLetter.Append(streamName);
         
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Mobile : </strong>");
           sbLetter.Append(mobile);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Query : </strong>");
           sbLetter.Append(studentQuery);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("Our expert panel will look into your query and reply you as soon as possible. In case of urgency please call us on 08800 56 77 33.");
           sbLetter.Append("<br />");
           sbLetter.Append(ClsSingelton.DirectAdmissionNote);
           sbLetter.Append("<br /> <br /><strong>Regards,<br/>Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></div></body></html>");
           return sbLetter.ToString();
       }
       public string MailBodyForSponserCollegeQuery(string userName, string cousreName, string streamName, string studentQuery, string mobile, string collegeName)
       {
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(userName));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br /> Has posted query on <strong>");
           sbLetter.Append("<a href='");
           sbLetter.Append(Common.WebUrl);
           sbLetter.Append("http://www.admissionjankari.com/");
           sbLetter.Append("'> AdmissionJankari.com");
           sbLetter.Append("</a></strong>");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Course : </strong>");
           sbLetter.Append(cousreName);
           
           sbLetter.Append("<br /> <br /><strong>College Name : </strong>");
           sbLetter.Append(collegeName);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Stream : </strong>");
           sbLetter.Append(streamName);
         
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Mobile : </strong>");
           sbLetter.Append(mobile);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Query : </strong>");
           sbLetter.Append(studentQuery);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("In case of urgency please call us on 08800 56 77 33.");
           sbLetter.Append("<br />");
           sbLetter.Append(ClsSingelton.DirectAdmissionNote);
           sbLetter.Append("<br /> <br /><strong>Regards,<br/>Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></div></body></html>");
           return sbLetter.ToString();
       }
       public string MailBodyForExamQuery(string userName, string cousreName, string studentQuery, string mobile, string examName)
       {
           StringBuilder sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(userName));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br /> Thank you for posting your query on <strong>");
           sbLetter.Append("<a href='");
           sbLetter.Append(Common.WebUrl);
           sbLetter.Append("http://www.admissionjankari.com/");
           sbLetter.Append("'> AdmissionJankari.com");
           sbLetter.Append("</a></strong>");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Course : </strong>");
           sbLetter.Append(cousreName);
           sbLetter.Append("<br /> <br /><strong>Exam Name : </strong>");
           sbLetter.Append(examName);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Mobile : </strong>");
           sbLetter.Append(mobile);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Query : </strong>");
           sbLetter.Append(studentQuery);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("Our expert panel will look into your query and reply you as soon as possible. In case of urgency please call us on 08800 56 77 33.");
           sbLetter.Append("<br />");
           sbLetter.Append(ClsSingelton.DirectAdmissionNote);
           sbLetter.Append("<br /> <br /><strong>Regards,<br/>Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></div></body></html>");
           return sbLetter.ToString();
       }

       public string MailBodyForBankQuery(string userName, string cousreName, string studentQuery, string mobile, string bankName,string amount)
       {
           StringBuilder sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(userName));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br /> Thank you for posting your query on <strong>");
           sbLetter.Append("<a href='");
           sbLetter.Append(Common.WebUrl);
           sbLetter.Append("http://www.admissionjankari.com/");
           sbLetter.Append("'> AdmissionJankari.com");
           sbLetter.Append("</a></strong>");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Course : </strong>");
           sbLetter.Append(cousreName);
           sbLetter.Append("<br /> <br /><strong>Bank Name : </strong>");
           sbLetter.Append(bankName);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Amount : </strong>");
           sbLetter.Append("Rs. "+ amount);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Mobile : </strong>");
           sbLetter.Append(mobile);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Query : </strong>");
           sbLetter.Append(studentQuery);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("Our expert panel will look into your query and reply you as soon as possible. In case of urgency please call us on 08800 56 77 33.");
           sbLetter.Append("<br />");
           sbLetter.Append(ClsSingelton.DirectAdmissionNote);
           sbLetter.Append("<br /> <br /><strong>Regards,<br/>Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></div></body></html>");
           return sbLetter.ToString();
       }
       public string MailBodyForPaymentConformation(string Url, string Name, string FormNumber, string TransctionMode,string transctionId)
       {
           const string validateKey1 = "info@AdmissionJankari.com";
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; width:100%; color: #666666; background-color:#f0ffef; border: solid 2px #7f9db9;  font-family: Arial, Verdana'>");
           sbLetter.Append("<table width='100%' align='left' border='0' cellspacing='0' style='font-family:Verdana; height:15px; font-size:10pt;'><tr><td align='left' valign='top' style='background-color:#3b5998;' > ");
           sbLetter.Append("</td></tr><tr><td valign='middle' style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;'><span style='vertical-align:middle;font-size: 2em !important;color:white;font-family:Agency FB; font-weight:bold;'>Admissionjankari.com</span></a></td></tr><tr><td>&nbsp;</td></tr></table><div style='padding:4px;'>");
           sbLetter.Append(" <br />Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(Name));
           sbLetter.Append("</strong>");
           sbLetter.Append(", <br /><br />Congratulations on successfully filling up Combined Application Form on <a href='http://www.admissionjankari.com/'style='text-decoration: none'><strong><i> AdmissionJankari.com</i></strong></a>");
           sbLetter.Append("  . We have received your payment against application form: ");
           sbLetter.Append("<strong>");
           sbLetter.Append(FormNumber);
           sbLetter.Append("</strong>");
           sbLetter.Append("<br /><br />");
           sbLetter.Append("TransctionMode: ");
           sbLetter.Append("<strong>");
           sbLetter.Append(TransctionMode);
           sbLetter.Append("</strong>");
           sbLetter.Append("<br /><br />");
           sbLetter.Append("You can email us a soft copy of the photograph and document at <a href='mailto:info@admissionjankari.com'>info@admissionjankari.com </a> ");
           sbLetter.Append("<br/>");
           sbLetter.Append("<br/>");
           sbLetter.Append("  Please do mention your Application form Number in the email as a subject and also for further references");
           sbLetter.Append("</strong><br /><br /> Your account is ready to use and you can login anytime by using the following link: <strong>");
           sbLetter.Append("<a href='");
           sbLetter.Append(Common.WebUrl);
           sbLetter.Append(Url);
           sbLetter.Append("'>  click here");
           sbLetter.Append("</a></strong><br /> <br /> If you have any questions or doubts, please do get in touch with us at <a href='mailto:info@admissionjankari.com'><strong>");
           sbLetter.Append(validateKey1);
           sbLetter.Append(" </a>,alternatively you can call us and speak to our counselor on +91-08800 56 77 33");
           sbLetter.Append("</strong><br />");
           sbLetter.Append("<br /> <br /><strong>Regards,<br /><br />Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></body></html>");
           return sbLetter.ToString();
       }
       public string MailBodyForCOunsellingQuery(string userName, string collegeName )
       {
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(userName));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br /> Thank you for participating in online counselling.You have chosen following college:");
           sbLetter.Append("<br /> <br />");
            sbLetter.Append(collegeName);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append(" We will revert you back with admission conformation in the colleges: <strong>");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append(" </a>alternatively you can call us and speak to our counselor on +91-08800 56 77 33");
           sbLetter.Append("<br />");
           sbLetter.Append("<br /> <br /><strong>Regards,<br /><br />Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></body></html>");
           return sbLetter.ToString();
       }
       public string MailBodyForDonationUser(string userName)
       {
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(userName));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br /> Thank you very much for your brave move to fight against donation.");
           sbLetter.Append("<br /><br />We assure to act on your report with the best of our abilities and would keep you updated about the same. We would also ensure about your");
           sbLetter.Append(" anonymity while investigating the matter. ");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("Once again please accept our sincere thanks. You can also call us on our student helpline number <strong> +91 - 8800 56 77 33 </strong>.");
           sbLetter.Append(ClsSingelton.DirectAdmissionNote);
           sbLetter.Append("<br /> <br /><strong>Regards,<br /><br />Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></body></html>");
           return sbLetter.ToString();
       }
      
       public string MailBodyForDonationAdmin(string userName,string emailId,string userMobile,string userCourseName,
                    string collegeName,string accusedName,string accusedMobile,string accusedEmailId,string userStory)
       {
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase("Admin"));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br />" + userName + " reort a Donation complain on ");
           sbLetter.Append("<a href='");
           sbLetter.Append(Common.WebUrl);
           sbLetter.Append("http://www.admissionjankari.com/");
           sbLetter.Append("'> AdmissionJankari.com");
           sbLetter.Append("</a></strong> The  details are as follows:");
           sbLetter.Append("<br /> <br /><strong> User Name:</strong> ");
           sbLetter.Append(userName);
           sbLetter.Append("<br /> <br /> <strong>User EmailId:</strong> ");
           sbLetter.Append(emailId);
           sbLetter.Append("<br /> <br /><strong> User Mobile:</strong> ");
           sbLetter.Append(userMobile);
           sbLetter.Append("<br /> <br /> <strong>Course:</strong> ");
           sbLetter.Append(userCourseName);
           sbLetter.Append("<br /> <br /> <strong>College Name: </strong>");
           sbLetter.Append(collegeName);
           sbLetter.Append("<br /> <br /> <strong>User Story:</strong> ");
           sbLetter.Append(userStory);
           sbLetter.Append("<br /> <br /><strong> Accused Name:</strong> ");
           sbLetter.Append(!string.IsNullOrEmpty(accusedName)?accusedName :"N/A");
           sbLetter.Append("<br /> <br /><strong> Accused Mobile:</strong> ");
           sbLetter.Append(!string.IsNullOrEmpty(accusedMobile) ? accusedMobile : "N/A");
           sbLetter.Append("<br /> <br /><strong> Accused Email Id:</strong> ");
           sbLetter.Append(!string.IsNullOrEmpty(accusedEmailId) ? accusedEmailId : "N/A");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("Please him to resolve this issue as this is illegal and unethical issue which student are getting admission to respective college. ");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<br /> <br /><strong>Regards,<br /><br />Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></body></html>");
           return sbLetter.ToString();
       }
       public string MailBodyForDirectCounsulling(string userName, string collegeName,string courseName,string formNumber)
       {
           string ValidateKey1 = "info@AdmissionJankari.com";
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(userName));
           sbLetter.Append("</strong>, <br /><br />Congratulations on successfully filling up Combined Application Form on <a href='http://www.admissionjankari.com/'style='text-decoration: none'><strong><i> AdmissionJankari.com</i></strong></a>");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Application Form Number: ");
           sbLetter.Append(formNumber);
           sbLetter.Append("</strong><br /> <br />");
           sbLetter.Append("<strong>Course: ");
           sbLetter.Append(courseName);
           sbLetter.Append("</strong><br /> <br />");
           sbLetter.Append("<strong> You have chosen college: ");
           sbLetter.Append(collegeName);
           sbLetter.Append("</strong><br /> <br />");
           sbLetter.Append("If you have any questions or doubts, please do get in touch with us at <a href='mailto:info@admissionjankari.com'><strong>");
           sbLetter.Append(ValidateKey1);
           sbLetter.Append(" </a>,alternatively you can call us and speak to our counselor on +91-08800 56 77 33");
           sbLetter.Append("</strong><br />");
           sbLetter.Append("<br /> <br /><strong>Regards,<br /><br />Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></body></html>");
           return sbLetter.ToString();
       }

       public string MailBodyForCallFromInstitute(string userName, string collegeName)
       {
           StringBuilder sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(userName));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br /> Congratulations you successfully register your request  <strong>Call from Institute </strong> for the  <strong>" + collegeName +"</strong> on <strong>" );
           sbLetter.Append("<a href='");
           sbLetter.Append(Common.WebUrl);
           sbLetter.Append("http://www.admissionjankari.com/");
           sbLetter.Append("'> AdmissionJankari.com");
           sbLetter.Append("</a></strong>");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("Our expert panel will look into your request and reply you as soon as possible. In case of urgency please call us on 08800 56 77 33.");
           sbLetter.Append("<br />");
           sbLetter.Append(ClsSingelton.DirectAdmissionNote);
           sbLetter.Append("<br /> <br /><strong>Regards,<br/>Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></div></body></html>");
           return sbLetter.ToString();
       }

       public string MailBodyForCallFromInstituteForAdmin(string userName, string emailId,string mobileNo,string courseName, string collegeName)
       {
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase("Admin"));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br /> Congratulations user successfully register  <strong>Call from Institute </strong> for the  <strong>" + collegeName +"</strong> on <strong>" );
           sbLetter.Append("<a href='");
           sbLetter.Append(Common.WebUrl);
           sbLetter.Append("http://www.admissionjankari.com/");
           sbLetter.Append("'> AdmissionJankari.com");
           sbLetter.Append("</a></strong> for the course " + courseName);
           sbLetter.Append("<br /> <br /><strong>The user details are as follows: </strong>");
           sbLetter.Append("<br /> <br />User Name: " + userName);
           sbLetter.Append("<br /> <br />User Email Id: " + emailId);
           sbLetter.Append("<br /> <br />User Mobile No: " + mobileNo);
           sbLetter.Append("<br /> <br />Please look into request and reply you as soon as possible.");
           sbLetter.Append("<br />");
           sbLetter.Append("<br /> <br /><strong>Regards,<br/>Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></div></body></html>");
           return sbLetter.ToString();
       }
       public string MailForPayment(string userName,string url, string formNumber)
       {
       var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(userName));
           sbLetter.Append("</strong>, <br /><br />We have not received the payment against direct admission form.");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Application Form Number: ");
           sbLetter.Append(formNumber);
           sbLetter.Append("</strong><br /> <br />");
         
           sbLetter.Append("Please make payment to start your counselling with us.");
       
           sbLetter.Append(url+" Click here to make payment.</a>");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append(" Alternatively you can call us and speak to our counselor on +91-08800 56 77 33");
           sbLetter.Append("<br />");
           sbLetter.Append("<br /> <br /><strong>Regards,<br /><br />Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></body></html>");
           return sbLetter.ToString();
       }

       public string MailBodyForDownloadApplicationfrom(string userName, string cousreName)
       {
           StringBuilder sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(userName));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br /> Thank you for downloading the application form from  <strong>");
           sbLetter.Append("<a href='");
           sbLetter.Append(Common.WebUrl);
           sbLetter.Append("http://www.admissionjankari.com/");
           sbLetter.Append("'> AdmissionJankari.com");
           sbLetter.Append("</a></strong>");
           sbLetter.Append("");
           sbLetter.Append(" for the Course  <strong>");
           sbLetter.Append(cousreName + "</strong>");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("if you have any questions regarding the application form  do not hesitate to  call us on 08800 56 77 33.");
           sbLetter.Append("<br />");
           sbLetter.Append("<br /> <br /><strong>Regards,<br/>Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></div></body></html>");
           return sbLetter.ToString();
       }

       public string MailBodyForAdminToDownloadApplicationfrom(string userName, string cousreName,string mobileNo)
       {
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase("Admin"));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br />One of the user download the Application form from  <strong>");
           sbLetter.Append("<a href='");
           sbLetter.Append(Common.WebUrl);
           sbLetter.Append("http://www.admissionjankari.com/");
           sbLetter.Append("'> AdmissionJankari.com");
           sbLetter.Append("</a></strong>");
           sbLetter.Append(" having the follwoing details: ");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Name : ");
           sbLetter.Append(userName);
           sbLetter.Append("</strong> <br /> <br />");
           sbLetter.Append("<strong> Course :  ");
           sbLetter.Append(cousreName + "</strong>");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong> Mobile No :  ");
           sbLetter.Append(mobileNo + "</strong>");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<br /> <br /><strong>Regards,<br/>Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></div></body></html>");
           return sbLetter.ToString();
       }

       public string SendValidationMailForTheBookSeat(string Url, string Name, string FormNumber, string Transcationmade,string courseName)
       {
           string ValidateKey1 = "info@AdmissionJankari.com";
           StringBuilder sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; width:100%; color: #666666; background-color:#f0ffef; border: solid 2px #7f9db9;  font-family: Arial, Verdana'>");
           sbLetter.Append("<table width='100%' align='left' border='0' cellspacing='0' style='font-family:Verdana; height:15px; font-size:10pt;'><tr><td align='left' valign='top' style='background-color:#3b5998;' > ");
           sbLetter.Append("</td></tr><tr><td valign='middle' style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a></td></tr><tr><td>&nbsp;</td></tr></table><div style='padding:4px;'>");
           sbLetter.Append(" <br />Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(Name));
           sbLetter.Append("</strong>");
           sbLetter.Append(", <br /><br />Thank you for registration. You have selected payment mode of " + Transcationmade);
           sbLetter.Append(".<br /><br />");
           sbLetter.Append("We are yet to receive confirmation from bank.");
           sbLetter.Append("<br /><br />");
           sbLetter.Append("Once we receive confirmation from bank we will proceed for registration for your admission in your preferred college for the course " + courseName);
           sbLetter.Append("Your Application Form Number is: ");
           sbLetter.Append(FormNumber);
           sbLetter.Append("<br /><br />");
           sbLetter.Append("<strong>Please be ready and follow the instructions:</strong>");
           sbLetter.Append("<br /><br />");
           sbLetter.Append("<strong>a. </strong> Send photocopies of relevant document (Mark sheet, Certificates Xth onward) within 15 ");
           sbLetter.Append("<br/>");
           sbLetter.Append("days of registration to start processing of your application.");
           sbLetter.Append("<br /><br />"); 
           sbLetter.Append("<strong>b.</strong> You need to submit rest of the admission amount within 15 days of registration.");
           sbLetter.Append("<br/><br />");
           sbLetter.Append("<strong>c. </strong>If there are any discrepancies with your document we will cancel your registration and ");
           sbLetter.Append("<br/>");
           sbLetter.Append("amount will be returned to your account.");
           sbLetter.Append("<br /><br />");
           sbLetter.Append("<strong>d. </strong>If you have any issue with admission, you can cancel your registration. We will refund your");
           sbLetter.Append("<br/>");
           sbLetter.Append("registration amount.");
           sbLetter.Append("<br /><br />");
           sbLetter.Append("You can email us a soft copy of the photograph and document at <a href='mailto:info@admissionjankari.com'>info@admissionjankari.com </a> ");
           sbLetter.Append("<br/>");
           sbLetter.Append("<br/>");
           sbLetter.Append("  Please do mention your Application form Number in the email as a subject  for further references");
           sbLetter.Append("</strong><br /><br />");
           sbLetter.Append("We at AdmissionJankari are constantly striving to help students to get admission in best college of his");
           sbLetter.Append("<br/>");
           sbLetter.Append("choice of college in all india and abroad.");
           sbLetter.Append("<br /> <br /> If you have any questions or doubts, please do get in touch with us at <a href='mailto:info@admissionjankari.com'><strong>");
           sbLetter.Append(ValidateKey1);
           sbLetter.Append(" </a>,alternatively you can call us and speak to our counselor on +91-08800 56 77 33");
           sbLetter.Append("</strong><br />");
           sbLetter.Append("<br /> <br /><strong>Regards,<br /><br />Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></body></html>");
           return sbLetter.ToString();
       }
       public string MailBodyForUserCommentToAdmin(string userName, string mobileNo,string emailId,string section,string comment)
       {
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase("Admin"));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br />You have received following comments regarding "+section+"  <strong>, from");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Name : ");
           sbLetter.Append(userName);
           sbLetter.Append("</strong> <br /> <br />");
           sbLetter.Append("<strong>Email : ");
           sbLetter.Append(emailId);
           sbLetter.Append("</strong> <br /> <br />");
           sbLetter.Append("<strong>Mobile : ");
           sbLetter.Append(mobileNo);
           sbLetter.Append("</strong> <br /> <br />");
           sbLetter.Append("<strong> Comment :  ");
           sbLetter.Append(comment + "</strong>");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Please revert back and resolve/clarify his/her concerns</strong>");
           sbLetter.Append("<br /> <br /><strong>Regards,<br/>Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></div></body></html>");
           return sbLetter.ToString();
       }
       public string MailBodyForUserCommentByAdmin(string userName,  string section,string commentFor)
       {
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(userName));
           sbLetter.Append("</strong>, <br /><br />Thank you for submitting your comments to us.");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("We have reviewed your comments for regarding " + section + " " + commentFor + ", and found it appropriate. ");
           sbLetter.Append("Now your comment is visible on AdmissionJankari.com. Click <a href='http://www.admissionjankari.com/'>here</a>  to view your comments.");
           sbLetter.Append("<br /> <br />");
          sbLetter.Append("If you have any questions or doubts, please do get in touch with us at info@AdmissionJankari.com ,"+ " alternatively you can call us and speak to our counselor on +91-08800 56 77 33");
           sbLetter.Append("<br /> <br /><strong>Regards,<br /><br />Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>"+
               "Let’s create a knowledgeable nation...</span></div></body></html>");
           return sbLetter.ToString();
       }
       public string MailBodyForUserTellAfrndToAdmin(string userName, string frndName, string emailId, string message, string link)
       {
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase("Admin"));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br />Mr./Mrs. " + userName + " has refered an article to");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Name : ");
           sbLetter.Append(frndName);
           sbLetter.Append("</strong> <br /> <br />");
           sbLetter.Append("<strong>Email : ");
           sbLetter.Append(emailId);
           sbLetter.Append("</strong> <br /> <br />");
           sbLetter.Append("<strong>Message : ");
           sbLetter.Append(message + "</strong>");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Link : ");
           sbLetter.Append(link + "</strong>");
           sbLetter.Append("<br /> <br />");
         
           return sbLetter.ToString();
       }
       public string MailBodyForreferAfrnd(string userName, string link,string message,string frnName )
       {
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(frnName));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br /> " + userName + " has recommended this article for your viewing.");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append(link);
             sbLetter.Append(",<br /><br /> " + userName + " has said.");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append(message);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("if you have any questions regarding the application form  do not hesitate to  call us on 08800 56 77 33.");
           sbLetter.Append("<br />");
           sbLetter.Append("<br /> <br /><strong>Regards,<br/>Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></div></body></html>");
           return sbLetter.ToString();
       }
       public string MailToUserForAbuse(string userName, string abuseType)
       {
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(userName));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br />Thank you for reporting abuse to AdmissionJankari.");
           sbLetter.Append("<br /> <br />We have received your complain regarding " + abuseType + ",We would get back to you shortly.");
          sbLetter.Append("<br /> <br />");
         sbLetter.Append("if you have any questions regarding the application form  do not hesitate to  call us on 08800 56 77 33.");
           sbLetter.Append("<br />");
           sbLetter.Append("<br /> <br /><strong>Regards,<br/>Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></div></body></html>");
           return sbLetter.ToString();
       }
       public string MailToAdminForAbuse(string userName,  string emailId,string mobileNo, string abusecontent, string abusetype)
       {
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase("Admin"));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br />You have received Report Abuse from "+userName);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Name : ");
           sbLetter.Append(userName);
           sbLetter.Append("</strong> <br /> <br />");
           sbLetter.Append("<strong>Email : ");
           sbLetter.Append(emailId);
           sbLetter.Append("</strong> <br /> <br />");
           sbLetter.Append("<strong>Mobile : ");
           sbLetter.Append(mobileNo);
           sbLetter.Append("</strong> <br /> <br />");
           sbLetter.Append("<strong>AbuseType : ");
           sbLetter.Append(abusetype + "</strong>");
           sbLetter.Append("<br /> <br />");
           return sbLetter.ToString();
       }
       public string MailToCollegeUser(string userName, string collegeName,  string state,string city,string mobile,string email,string contactmobile)
       {
         
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(userName));
           sbLetter.Append("</strong>, <br /><br />Thank you for college registration.");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>We are happy to inform you that we have received a registration request from you with the following details.");
            sbLetter.Append("</strong><br /> <br />");
            sbLetter.Append("<strong>College Name:</strong> ");
           sbLetter.Append(collegeName);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>College Contact Mobile:</strong> ");
           sbLetter.Append(mobile);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>College State:</strong> ");
           sbLetter.Append(state);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>College City:</strong> ");
           sbLetter.Append(city);
           sbLetter.Append("<br /><br />");
           sbLetter.Append("<strong>Contact Person Email:</strong> ");
           sbLetter.Append(email);
           sbLetter.Append("<br /><br />");
           sbLetter.Append("<strong>We are in process of verification, once the details are verified and confirmed we will create a profile"+
               " with the name of your college where you can manage your college detail section from your end.");
           sbLetter.Append("</strong><br /> <br />");
           sbLetter.Append("<strong>Once the verification is done at our end we will inform you along with your login credentials");
                sbLetter.Append("</strong><br /> <br />");
           sbLetter.Append("Feel free reach us at +91-08800 56 77 33 or mail <a href='mailto:info@admissionjankari.com'> for any help or question. <strong>");
            sbLetter.Append("<br /> <br /><strong>Regards,<br /><br />Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></body></html>");
           return sbLetter.ToString();
       }
       public string MailBodyForCollegeRegisterToAdmin(string userName, string collegeName, string state, string city, string mobile, string email)
       {
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase("Admin"));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br />One of college has approached us for College Registration with following details,"+
               " Please go through details and moderate after verification of details.");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>College Name : ");
           sbLetter.Append(collegeName);
           sbLetter.Append("</strong> <br /> <br />");
           sbLetter.Append("<strong>College Mobile : ");
           sbLetter.Append(mobile);
           sbLetter.Append("</strong> <br /> <br />");
           sbLetter.Append("<strong>College State : ");
           sbLetter.Append(state);
           sbLetter.Append("</strong> <br /> <br />");
           sbLetter.Append("<strong>College City :  ");
           sbLetter.Append(city + "</strong>");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>College Contact Person Details : ");
           sbLetter.Append(userName);
           sbLetter.Append("</strong> <br /> <br />");
           sbLetter.Append("<strong>Contact Person Email : ");
           sbLetter.Append(email);
           sbLetter.Append("</strong> <br /> <br />");
           sbLetter.Append("<strong>Once verification is done, don’t forget to inform college authority to access"+
               " their college details by providing college credentials.</strong>");
           sbLetter.Append("<br /> <br /><strong>Regards,<br/>Team Admission Jankari</strong><br/>"+
               "<span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></div></body></html>");
           return sbLetter.ToString();
       }
       public string MailToCollegeForRegisterationVerification(string userName, string collegeName,  string email, string contactPersonMobile,string url)
       {

           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(userName));
           sbLetter.Append("</strong>, <br /><br />We are happy to inform you that your verification is complete against the " + Common.GetStringProperCase(collegeName));
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Click here to manage college profile. "+url);
           sbLetter.Append("</strong><br /> <br />");
           sbLetter.Append("<strong>UserId:</strong> ");
           sbLetter.Append(email);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Password:</strong> ");
           sbLetter.Append(contactPersonMobile);
            sbLetter.Append("</strong><br /> <br />");
           sbLetter.Append("Feel free reach us at +91-08800 56 77 33 or mail at  <a href='mailto:info@admissionjankari.com'>info@admissionjankari.com</a> for any help or question. <strong>");
           sbLetter.Append("<br /> <br /><strong>Regards,<br /><br />Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></body></html>");
           return sbLetter.ToString();
       }
       public string MailReplyByCollegeToserForQueryPosted(string userName, string query, string reply, string collegeName)
       {

           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(userName));
           sbLetter.Append("</strong>");
           sbLetter.Append("<br /> <br /><strong>We have received your query.To know more about our college. Please find following answer for your concern.");
           sbLetter.Append("</strong><br /> <br />");
           sbLetter.Append("<strong>Query:</strong> ");
           sbLetter.Append(query);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Solution:</strong> ");
           sbLetter.Append(reply);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("Feel free reach us at +91-08800 56 77 33 or mail <a href='mailto:info@admissionjankari.com'> for any help or question. <strong>");
           sbLetter.Append("<br /> <br /><strong>Regards,<br /><br />Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></body></html>");
           return sbLetter.ToString();
       }
       public string MailToAdminRegardingBookSeatByStudent(string userName, string emailId, string mobileNo, string collegeName)
       {
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase("Admin"));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br />You have received a request for BookMySeat from " + userName);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Name : ");
           sbLetter.Append(userName);
           sbLetter.Append("</strong> <br /> <br />");
           sbLetter.Append("<strong>Email : ");
           sbLetter.Append(emailId);
           sbLetter.Append("</strong> <br /> <br />");
           sbLetter.Append("<strong>Mobile : ");
           sbLetter.Append(mobileNo);
           sbLetter.Append("</strong> <br /> <br />");
           sbLetter.Append("<strong>College Name : ");
           sbLetter.Append(collegeName + "</strong>");
           sbLetter.Append("<br /> <br />");
           return sbLetter.ToString();
       }
       public string MailBodyForBookseatToUser(string userName, string emailId, string collegeName)
       {
           StringBuilder sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append("<br /><div style='padding-left:15px; padding-bottom:10px;'>Dear ");
           sbLetter.Append(userName);
           sbLetter.Append(",<br /><br />Thank you for booking you seat to " + collegeName);
           sbLetter.Append("<br /><br />We have received your request; we would get back to you shortly.<br /><br /> ");
           sbLetter.Append("Feel free reach us at +91-08800 56 77 33 or mail <a href='mailto:info@admissionjankari.com'> for any help or question. <strong>");
           sbLetter.Append("<br /> <br /><strong>Regards,<br /><br />Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></body></html>");
           return sbLetter.ToString();
       }
       public string MailToAdminRegardingEventAttendingByStudent(string userName, string emailId, string mobileNo, string collegeName,string eventName,string eventDate)
       {
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase("Admin"));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br />" + userName + " wants to attend event-" + eventName + " for college-<h3>" + collegeName + " </h3>");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Name : ");
           sbLetter.Append(userName);
           sbLetter.Append("</strong> <br /> <br />");
           sbLetter.Append("<strong>Email : ");
           sbLetter.Append(emailId);
           sbLetter.Append("</strong> <br /> <br />");
           sbLetter.Append("<strong>Mobile : ");
           sbLetter.Append(mobileNo);
           sbLetter.Append("</strong> <br /> <br />");
           sbLetter.Append("<strong>College Name : ");
           sbLetter.Append(collegeName + "</strong>");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Event Name : ");
           sbLetter.Append(eventName + "</strong>");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Event Date: ");
           sbLetter.Append(eventDate + "</strong>");
           sbLetter.Append("<br /> <br />");
           return sbLetter.ToString();
       }
       public string MailToAdminforRefundSent(string userName, string userEmail, string userFormNo, string dateOfRequest, string courseName)
       {
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase("Admin"));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br />" + userName + " wants to refund the fees for form number- " + userFormNo  + " on " + dateOfRequest + " </h3>");
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Name : ");
           sbLetter.Append(userName);
           sbLetter.Append("</strong> <br /> <br />");
           sbLetter.Append("<strong>Email : ");
           sbLetter.Append(userEmail);
           sbLetter.Append("</strong> <br /> <br />");
           sbLetter.Append("<strong>Form Number : ");
           sbLetter.Append(userFormNo);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Date of Request : ");
           sbLetter.Append(dateOfRequest);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<strong>Course Name : ");
           sbLetter.Append(courseName);
           sbLetter.Append("<br /> <br />");
           sbLetter.Append("<br /> <br /><strong>Regards,<br /><br />Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></div></body></html>");
           return sbLetter.ToString();
       }
       public string MailToUserforRefundSent(string userName, string userFormNo, string dateOfTransaction, string courseName)
       {
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append("<br /><div style='padding-left:15px; padding-bottom:10px;'>Dear ");
           sbLetter.Append(userName);
           sbLetter.Append(",<br /><br />We have received the refund request for form number " + userFormNo + " on " + dateOfTransaction + " for " + courseName);
           sbLetter.Append("<br /><br />The request has been sent to the admin team to review and the same shall be updated to you.<br /><br /> ");
           sbLetter.Append("Feel free reach us at +91-08800 56 77 33 or mail <a href='mailto:info@admissionjankari.com'> for any help or question. <strong>");
           sbLetter.Append("<br /> <br /><strong>Regards,<br /><br />Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></body></html>");
           return sbLetter.ToString();
       }
       public string QueryResponseMailToUser(string userName, string query)
       {
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(userName));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br /><strong>Your Query:-</strong>");
           sbLetter.Append(query);
           sbLetter.Append("<br /> has been replied. To see query, please login at given link<br />");
           sbLetter.Append("<a href='");
           sbLetter.Append(Common.WebUrl);
           sbLetter.Append("http://www.admissionjankari.com/account/login");
           sbLetter.Append("'> AdmissionJankari.com");
           sbLetter.Append("</a></strong>");
           sbLetter.Append("<br/>");
           sbLetter.Append("Feel free reach us at +91-08800 56 77 33 or mail <a href='mailto:info@admissionjankari.com'> for any help or question. <strong>");
           sbLetter.Append("<br /> <br /><strong>Regards,<br /><br />Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></body></html>");
           return sbLetter.ToString();
       }
       public string QueryResponseMailToAdmin(string userName, string query,string replyAnswer)
       {
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; border: solid 1px #7f9db9;border-bottom: solid 5px #7f9db9; color: #666666; font-family: Arial, Verdana'>");
           sbLetter.Append("<div style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none;font-size: 2em !important;color:white;font-family:Agency FB;'>Admissionjankari.com</a>");
           sbLetter.Append("</div>");
           sbLetter.Append(" <br /><div style='padding-left:15px;padding-bottom:5px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase("Admin"));
           sbLetter.Append("</strong>");
           sbLetter.Append(",<br /><br /><strong>Query posted by user " + userName + ":</strong>");
           sbLetter.Append(query);
           sbLetter.Append(",<br /><br /><strong>Answer replied by " + new SecurePage().LoggedInUserName + ":</strong>");
           sbLetter.Append(replyAnswer);
           return sbLetter.ToString();
       }

       public string SendProductConfirmationMail(string name, string orderNumber, string tranctionDetails,DataTable objProduct)
       {
           var totalAmountPayed = 0;
           const string validateKey1 = "info@AdmissionJankari.com";
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; width:100%; color: #666666;  border: solid 2px #7f9db9;  font-family: Arial, Verdana'>");
           sbLetter.Append("<table width='100%' align='left' border='0' cellspacing='0' style='font-family:Verdana; height:15px; font-size:10pt;'><tr><td align='left' valign='top' style='background-color:#3b5998;' > ");
           sbLetter.Append("</td></tr><tr><td valign='middle' style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none'><span style='vertical-align:middle;font-size: 2em !important;color:white;font-family:Agency FB;font-weight:bold;'>Admissionjankari.com</span></a></td></tr><tr><td>&nbsp;</td></tr></table><div style='padding:4px;'>");
           sbLetter.Append("<div style='clear: both'></div><div style='padding:4px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(name));
           sbLetter.Append("</strong>");
           sbLetter.Append(", <br /><br />Congratulations,you have successfully added features using AdmissionJankari.<br/>");
           sbLetter.Append("  . We have received your request for following features.");
           sbLetter.Append("<br />");
           sbLetter.Append("<table style='width:100%;font-family:Lucida Sans Unicode', 'Lucida Grande', Sans-Serif;font-size: 12px;	width:100%;border-collapse: collapse;text-align: left;'><tr  style='background:#eff2f7'><td>S.No</td><td style='height: 45px;'>Product</td><td style='height: 45px;'>Course</td><td style='height: 45px;'>Product Amount</td></tr>");
           for (var i = 0; i <=objProduct.Rows.Count-1;i++ )
           {
               totalAmountPayed = Convert.ToInt32(objProduct.Rows[i]["AjMonthValue"]) * Convert.ToInt32(objProduct.Rows[i]["PayProductAmount"]) + totalAmountPayed;
               sbLetter.Append("<tr><td style='color: #669;padding: 9px 8px 5px 8px;border-bottom:1px solid #e1e1e1;'>" + (i + 1) + "</td><td style='color: #669;padding: 9px 8px 5px 8px;border-bottom:1px solid #e1e1e1;'>" + objProduct.Rows[i]["ProductName"] + "</td><td style='color: #669;padding: 9px 8px 5px 8px;border-bottom:1px solid #e1e1e1;'>" + objProduct.Rows[i]["AjCourseName"] + "</td><td style='color: #669;padding: 9px 8px 5px 8px;border-bottom:1px solid #e1e1e1;'>Rs." + objProduct.Rows[i]["PayProductAmount"] + "</td></tr>");
           }
           sbLetter.Append("<tr><td style='color: #669;padding: 9px 8px 5px 8px;border-bottom:1px solid #e1e1e1;'></td><td style='color: #669;padding: 9px 8px 5px 8px;border-bottom:1px solid #e1e1e1;'></td><td style='color: #669;padding: 9px 8px 5px 8px;border-bottom:1px solid #e1e1e1;'></td><td style='color: #669;padding: 9px 8px 5px 8px;border-bottom:1px solid #e1e1e1;'>Amount Payable:Rs. " + totalAmountPayed + "</td> </tr></table>");
           sbLetter.Append("<br />");
           sbLetter.Append("Your Order Number is: ");
           sbLetter.Append("<strong>");
           sbLetter.Append(orderNumber);
           sbLetter.Append("</strong>");
           sbLetter.Append("<br /><br />");
           sbLetter.Append(tranctionDetails);
           sbLetter.Append("</strong><br /><br />");
           sbLetter.Append("<br /> <br /> If you have any questions or doubts, please do get in touch with us at <a href='mailto:info@admissionjankari.com'><strong>");
           sbLetter.Append(validateKey1);
           sbLetter.Append(" </a>,alternatively you can call us and speak to our counselor on +91-8800 56 77 33");
           sbLetter.Append("</strong><br />");
           sbLetter.Append("<br /> <br /><strong>Regards,<br /><br />Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></body></html>");
           return sbLetter.ToString();
       }
       public string SendProductConfirmationMailAfterSuccess(string name, string orderNumber, DataTable objProduct,string transactionMode)
       {
           var totalAmountPayed = 0;
           const string validateKey1 = "info@AdmissionJankari.com";
           var sbLetter = new StringBuilder();
           sbLetter.Append("<html><body><div style='font-size: 13px; width:100%; color: #666666;  border: solid 2px #7f9db9;  font-family: Arial, Verdana'>");
           sbLetter.Append("<table width='100%' align='left' border='0' cellspacing='0' style='font-family:Verdana; height:15px; font-size:10pt;'><tr><td align='left' valign='top' style='background-color:#3b5998;' > ");
           sbLetter.Append("</td></tr><tr><td valign='middle' style='padding-left:15px; padding-bottom:10px; text-align:left; background-color:#3b5998; padding-top:9px;'><a href='http://www.admissionjankari.com/'style='text-decoration: none'><span style='vertical-align:middle;font-size: 2em !important;color:white;font-family:Agency FB;font-weight:bold;'>Admissionjankari.com</span></a></td></tr><tr><td>&nbsp;</td></tr></table><div style='padding:4px;'>");
           sbLetter.Append("<div style='clear: both'></div><div style='padding:4px;'>Dear ");
           sbLetter.Append("<strong>");
           sbLetter.Append(Common.GetStringProperCase(name));
           sbLetter.Append("</strong>");
           sbLetter.Append(", <br /><br />Congratulations,you have successfully added features using AdmissionJankari.<br/>");
           sbLetter.Append("  . We have received your payment using " + transactionMode);
           sbLetter.Append("<br />");
           sbLetter.Append("<table style='width:100%;font-family:Lucida Sans Unicode', 'Lucida Grande', Sans-Serif;font-size: 12px;	width:100%;border-collapse: collapse;text-align: left;'><tr  style='background:#eff2f7'><td>S.No</td><td style='height: 45px;'>Product</td><td style='height: 45px;'>Course</td><td style='height: 45px;'>Product Amount</td></tr>");
           for (var i = 0; i <= objProduct.Rows.Count - 1; i++)
           {
               totalAmountPayed = Convert.ToInt32(objProduct.Rows[i]["AjMonthValue"]) * Convert.ToInt32(objProduct.Rows[i]["PayProductAmount"]) + totalAmountPayed;
               sbLetter.Append("<tr><td style='color: #669;padding: 9px 8px 5px 8px;border-bottom:1px solid #e1e1e1;'>" + (i + 1) + "</td><td style='color: #669;padding: 9px 8px 5px 8px;border-bottom:1px solid #e1e1e1;'>" + objProduct.Rows[i]["AjProductName"] + "</td><td style='color: #669;padding: 9px 8px 5px 8px;border-bottom:1px solid #e1e1e1;'>" + objProduct.Rows[i]["AjCourseName"] + "</td><td style='color: #669;padding: 9px 8px 5px 8px;border-bottom:1px solid #e1e1e1;'>Rs." + objProduct.Rows[i]["PayProductAmount"] + "</td></tr>");
           }
           sbLetter.Append("<tr><td style='color: #669;padding: 9px 8px 5px 8px;border-bottom:1px solid #e1e1e1;'></td><td style='color: #669;padding: 9px 8px 5px 8px;border-bottom:1px solid #e1e1e1;'></td><td style='color: #669;padding: 9px 8px 5px 8px;border-bottom:1px solid #e1e1e1;'></td><td style='color: #669;padding: 9px 8px 5px 8px;border-bottom:1px solid #e1e1e1;'>Amount Payable:Rs. " + totalAmountPayed + "</td> </tr></table>");
           sbLetter.Append("<br />");
           sbLetter.Append("Your Order Number is: ");
           sbLetter.Append("<strong>");
           sbLetter.Append(orderNumber);
           sbLetter.Append("</strong>");
          sbLetter.Append("</strong><br /><br />");
           sbLetter.Append("<br /> <br /> If you have any questions or doubts, please do get in touch with us at <a href='mailto:info@admissionjankari.com'><strong>");
           sbLetter.Append(validateKey1);
           sbLetter.Append(" </a>,alternatively you can call us and speak to our counselor on +91-8800 56 77 33");
           sbLetter.Append("</strong><br />");
           sbLetter.Append("<br /> <br /><strong>Regards,<br /><br />Team Admission Jankari</strong><br/><span style='font-family:Lucida Calligraphy;font-size:10px;'>Let’s create a knowledgeable nation...</span></div></body></html>");
           return sbLetter.ToString();
       }
    }
}

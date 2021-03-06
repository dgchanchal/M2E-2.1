﻿using System;
using System.Web;
using System.Reflection;
using System.Text;
using M2E.Common.Logger;
using M2E.CommonMethods;
using M2E.Models.DataWrapper;

namespace M2E.CommonMethods
{
    public class SendAccountCreationValidationEmail
    {
        private ILogger _logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        public delegate void exceptionEmailSend_Delegate(String toEmailAddrList, String senderName, String subject, String body, String attachmentsFilePathList, String logoPath, String companyDescription);

        public SendAccountCreationValidationEmail(ILogger logger)
        {
            _logger = logger;
        }

        public static void SendAccountCreationValidationEmailMessage(String toMail,String guid, HttpRequestBase request)
        {
            var sendEmail = new SendEmail();
            if (request.Url != null)
            {
                sendEmail.SendEmailMessage(toMail,
                    "donotreply",
                    "Validate your Account",
                    CreateAccountEmailBodyContent(request.Url.Authority, toMail, guid),
                    null,
                    null,
                    "Zestork - Place to boost your Carrer"
                    );
            }
        }

        public static void SendContactUsEmailMessage(String toMail, ContactUsRequest req)
        {
            var sendEmail = new SendEmail();
            
                sendEmail.SendEmailMessage(toMail,
                   "ContactUs",
                    "User Contact Us",
                    ContactUsEmailBodyContent(req),
                    null,
                    null,
                    "Cautom - Where Human Intelligence works"
                    );
            
        }

        public static void SendExceptionEmailMessage(String toMail, String exceptionMessage)
        {
            var sendEmail = new SendEmail();
            exceptionEmailSend_Delegate exceptionEmail_delegate = null;
            exceptionEmail_delegate = new exceptionEmailSend_Delegate(sendEmail.SendEmailMessage);
            IAsyncResult CallAsynchMethod = null;
            CallAsynchMethod = exceptionEmail_delegate.BeginInvoke(toMail,"Exception","Exception",ExceptionEmailBodyContent(exceptionMessage),null,null,"Cautom - Where Human Intelligence works",null,null); //invoking the method

        }

        private static string ContactUsEmailBodyContent(ContactUsRequest req)
        {
            var htmlBody = new StringBuilder();

            htmlBody.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#368ee0\">");
            htmlBody.Append("<tr>");
            htmlBody.Append("<td align=\"center\">");
            htmlBody.Append("<center>");
            htmlBody.Append("<table border=\"0\" width=\"600\" cellpadding=\"0\" cellspacing=\"0\">");
            htmlBody.Append("<tr>");
            htmlBody.Append("<td style=\"color:#ffffff !important; font-size:24px; font-family: Arial, Verdana, sans-serif; padding-left:10px;\" height=\"40\"></td>");
            htmlBody.Append("<td align=\"right\" width=\"50\" height=\"45\"></td>");
            htmlBody.Append("</tr>");
            htmlBody.Append("</table>");
            htmlBody.Append("</center>");
            htmlBody.Append("</td>");
            htmlBody.Append("</tr>");
            htmlBody.Append("</table>");

            htmlBody.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#ffffff\">");
            htmlBody.Append("<tr>");
            htmlBody.Append("<td align=\"center\">");
            htmlBody.Append("<center>");
            htmlBody.Append("<table border=\"0\" width=\"600\" cellpadding=\"0\" cellspacing=\"0\">");
            htmlBody.Append("<tr>");
            htmlBody.Append("<td style=\"color:#333333 !important; font-size:20px; font-family: Arial, Verdana, sans-serif; padding-left:10px;\" height=\"40\">");
            htmlBody.Append("<h3 style=\"font-weight:normal; margin: 20px 0;\">Contact Us</h3>");
            htmlBody.Append("<p style=\"font-size:12px; line-height:18px;\">");
            htmlBody.Append("User Message. <br /><br />");
            htmlBody.Append("Name : " + req.Name + "<br /><br />");
            htmlBody.Append("Email : " + req.Email + "<br /><br />");
            htmlBody.Append("Phone : " + req.Phone + "<br /><br />");
            htmlBody.Append("Type : " + req.Type + "<br /><br />");
            htmlBody.Append("Message : " + req.Message + "<br /><br />");
            htmlBody.Append("SendMeACopy : " + req.SendMeACopy + "<br /><br />");
            htmlBody.Append("</p>");
            
            htmlBody.Append("</td>");
            htmlBody.Append("</tr>");
            htmlBody.Append("</table>");
            htmlBody.Append("</center>");
            htmlBody.Append("</td>");
            htmlBody.Append("</tr>");
            htmlBody.Append("</table>");
            htmlBody.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#ffffff\">");
            htmlBody.Append("<tr>");
            htmlBody.Append("<td align=\"center\">");
            htmlBody.Append("<center>");
            htmlBody.Append("<table border=\"0\" width=\"600\" cellpadding=\"0\" cellspacing=\"0\">");
            htmlBody.Append("<tr>");
            htmlBody.Append("<td style=\"color:#333333 !important; font-size:20px; font-family: Arial, Verdana, sans-serif; padding-left:10px;\" height=\"40\">");
            htmlBody.Append("<h3 style=\"font-weight:normal; margin: 20px 0;\">Security</h3>");
            htmlBody.Append("<p style=\"font-size:12px; line-height:18px;\">");
            htmlBody.Append("Some details for user<br />");
            htmlBody.Append("<br />");
            htmlBody.Append("<br />More details for user.");
            htmlBody.Append("</p>");
            htmlBody.Append("<p style=\"font-size:12px; line-height:18px;\">");
            htmlBody.Append("<a href=\"#\">Check security settings</a>");
            htmlBody.Append("</p>");
            htmlBody.Append(" </td>");
            htmlBody.Append("</tr>");
            htmlBody.Append("</table>");
            htmlBody.Append("</center>");
            htmlBody.Append("</td>");
            htmlBody.Append("</tr>");
            htmlBody.Append("</table>");

            htmlBody.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#368ee0\">");
            htmlBody.Append("<tr>");
            htmlBody.Append("<td align=\"center\">");
            htmlBody.Append("<center>");
            htmlBody.Append("<table border=\"0\" width=\"600\" cellpadding=\"0\" cellspacing=\"0\">");
            htmlBody.Append("<tr>");
            htmlBody.Append("<td style=\"color:#ffffff !important; font-size:20px; font-family: Arial, Verdana, sans-serif; padding-left:10px;\" height=\"40\">");
            htmlBody.Append("<center>");
            htmlBody.Append("<p style=\"font-size:12px; line-height:18px;\">");
            htmlBody.Append("If you don't want to get system emails from FLAT please change your email settings.");
            htmlBody.Append("<br />");
            htmlBody.Append("<a href=\"#\" style=\"color:#ffffff !important;\">Click here to change email settings</a>");
            htmlBody.Append("</p>");
            htmlBody.Append("</center>");
            htmlBody.Append("</td>");
            htmlBody.Append("</tr>");
            htmlBody.Append("</table>");
            htmlBody.Append("</center>");
            htmlBody.Append("</td>");
            htmlBody.Append("</tr>");
            htmlBody.Append("</table>");


            return htmlBody.ToString();
        }

        private static string CreateAccountEmailBodyContent(String requestUrlAuthority,String toMail,String guid)
        {
            var htmlBody = new StringBuilder();

            htmlBody.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#368ee0\">");
		        htmlBody.Append("<tr>");
			        htmlBody.Append("<td align=\"center\">");
				        htmlBody.Append("<center>");
					        htmlBody.Append("<table border=\"0\" width=\"600\" cellpadding=\"0\" cellspacing=\"0\">");
						        htmlBody.Append("<tr>");
							        htmlBody.Append("<td style=\"color:#ffffff !important; font-size:24px; font-family: Arial, Verdana, sans-serif; padding-left:10px;\" height=\"40\"></td>");
							        htmlBody.Append("<td align=\"right\" width=\"50\" height=\"45\"></td>");
						        htmlBody.Append("</tr>");
					        htmlBody.Append("</table>");
				        htmlBody.Append("</center>");
			        htmlBody.Append("</td>");
		        htmlBody.Append("</tr>");
	        htmlBody.Append("</table>");

	        htmlBody.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#ffffff\">");
		        htmlBody.Append("<tr>");
			        htmlBody.Append("<td align=\"center\">");
				        htmlBody.Append("<center>");
					        htmlBody.Append("<table border=\"0\" width=\"600\" cellpadding=\"0\" cellspacing=\"0\">");
						        htmlBody.Append("<tr>");
							        htmlBody.Append("<td style=\"color:#333333 !important; font-size:20px; font-family: Arial, Verdana, sans-serif; padding-left:10px;\" height=\"40\">");
								        htmlBody.Append("<h3 style=\"font-weight:normal; margin: 20px 0;\">Account verification</h3>");
								        htmlBody.Append("<p style=\"font-size:12px; line-height:18px;\">");
									        htmlBody.Append("Message for User. <br /><br />");
									        htmlBody.Append("Email: "+toMail+"");
								        htmlBody.Append("</p>");
								        htmlBody.Append("<p style=\"font-size:12px; line-height:18px;\">");
                                        htmlBody.Append("<a href=\"http://" + requestUrlAuthority + "/#/" + "validate/" + toMail + "/" + guid + "\"> Click here to validate your account </a>");
								        htmlBody.Append("</p>");
                                        htmlBody.Append("<br> OR <br><p style=\"font-size:12px; line-height:18px;\">");
                                        htmlBody.Append("copy given URL in your browser <br>http://" + requestUrlAuthority + "/#/" + "validate/" + toMail + "/" + guid + " <br>");
                                        htmlBody.Append("</p>");
							        htmlBody.Append("</td>");
						        htmlBody.Append("</tr>");
					        htmlBody.Append("</table>");
				        htmlBody.Append("</center>");
			        htmlBody.Append("</td>");
		        htmlBody.Append("</tr>");
	        htmlBody.Append("</table>");
	        htmlBody.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#ffffff\">");
		        htmlBody.Append("<tr>");
			        htmlBody.Append("<td align=\"center\">");
				        htmlBody.Append("<center>");
					        htmlBody.Append("<table border=\"0\" width=\"600\" cellpadding=\"0\" cellspacing=\"0\">");
						        htmlBody.Append("<tr>");
							        htmlBody.Append("<td style=\"color:#333333 !important; font-size:20px; font-family: Arial, Verdana, sans-serif; padding-left:10px;\" height=\"40\">");
								        htmlBody.Append("<h3 style=\"font-weight:normal; margin: 20px 0;\">Security</h3>");
								        htmlBody.Append("<p style=\"font-size:12px; line-height:18px;\">");
									        htmlBody.Append("Some details for user<br />");
									        htmlBody.Append("<br />");
									        htmlBody.Append("<br />More details for user.");
								        htmlBody.Append("</p>");
								        htmlBody.Append("<p style=\"font-size:12px; line-height:18px;\">");
									        htmlBody.Append("<a href=\"#\">Check security settings</a>");
								        htmlBody.Append("</p>");
							       htmlBody.Append(" </td>");
						        htmlBody.Append("</tr>");
					        htmlBody.Append("</table>");
				        htmlBody.Append("</center>");
			        htmlBody.Append("</td>");
		        htmlBody.Append("</tr>");
	        htmlBody.Append("</table>");

	        htmlBody.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#368ee0\">");
		        htmlBody.Append("<tr>");
			        htmlBody.Append("<td align=\"center\">");
				        htmlBody.Append("<center>");
					        htmlBody.Append("<table border=\"0\" width=\"600\" cellpadding=\"0\" cellspacing=\"0\">");
						        htmlBody.Append("<tr>");
							        htmlBody.Append("<td style=\"color:#ffffff !important; font-size:20px; font-family: Arial, Verdana, sans-serif; padding-left:10px;\" height=\"40\">");
								        htmlBody.Append("<center>");
									        htmlBody.Append("<p style=\"font-size:12px; line-height:18px;\">");
									        htmlBody.Append("If you don't want to get system emails from FLAT please change your email settings.");
									        htmlBody.Append("<br />");
									        htmlBody.Append("<a href=\"#\" style=\"color:#ffffff !important;\">Click here to change email settings</a>");
								        htmlBody.Append("</p>");
								        htmlBody.Append("</center>");
							        htmlBody.Append("</td>");
						        htmlBody.Append("</tr>");
					        htmlBody.Append("</table>");
				        htmlBody.Append("</center>");
			        htmlBody.Append("</td>");
		        htmlBody.Append("</tr>");
	        htmlBody.Append("</table>");


            return htmlBody.ToString();
        }

        private static string ExceptionEmailBodyContent(String exceptionMessage)
        {
            var htmlBody = new StringBuilder();

            htmlBody.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#368ee0\">");
            htmlBody.Append("<tr>");
            htmlBody.Append("<td align=\"center\">");
            htmlBody.Append("<center>");
            htmlBody.Append("<table border=\"0\" width=\"600\" cellpadding=\"0\" cellspacing=\"0\">");
            htmlBody.Append("<tr>");
            htmlBody.Append("<td style=\"color:#ffffff !important; font-size:24px; font-family: Arial, Verdana, sans-serif; padding-left:10px;\" height=\"40\"></td>");
            htmlBody.Append("<td align=\"right\" width=\"50\" height=\"45\"></td>");
            htmlBody.Append("</tr>");
            htmlBody.Append("</table>");
            htmlBody.Append("</center>");
            htmlBody.Append("</td>");
            htmlBody.Append("</tr>");
            htmlBody.Append("</table>");

            htmlBody.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#ffffff\">");
            htmlBody.Append("<tr>");
            htmlBody.Append("<td align=\"center\">");
            htmlBody.Append("<center>");
            htmlBody.Append("<table border=\"0\" width=\"600\" cellpadding=\"0\" cellspacing=\"0\">");
            htmlBody.Append("<tr>");
            htmlBody.Append("<td style=\"color:#333333 !important; font-size:20px; font-family: Arial, Verdana, sans-serif; padding-left:10px;\" height=\"40\">");
            htmlBody.Append("<h3 style=\"font-weight:normal; margin: 20px 0;\">Contact Us</h3>");
            htmlBody.Append("<p style=\"font-size:12px; line-height:18px;\">");
            htmlBody.Append("Exception occured. <br /><br />");
            htmlBody.Append("Date :" + DateTime.Now.ToLongDateString() + "<br /><br />");
            htmlBody.Append("time : " + DateTime.Now.ToLongTimeString() + "<br /><br />");
            htmlBody.Append("Exception : " + exceptionMessage + "<br /><br />");            
            htmlBody.Append("</p>");

            htmlBody.Append("</td>");
            htmlBody.Append("</tr>");
            htmlBody.Append("</table>");
            htmlBody.Append("</center>");
            htmlBody.Append("</td>");
            htmlBody.Append("</tr>");
            htmlBody.Append("</table>");
            htmlBody.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#ffffff\">");
            htmlBody.Append("<tr>");
            htmlBody.Append("<td align=\"center\">");
            htmlBody.Append("<center>");
            htmlBody.Append("<table border=\"0\" width=\"600\" cellpadding=\"0\" cellspacing=\"0\">");
            htmlBody.Append("<tr>");
            htmlBody.Append("<td style=\"color:#333333 !important; font-size:20px; font-family: Arial, Verdana, sans-serif; padding-left:10px;\" height=\"40\">");
            htmlBody.Append("<h3 style=\"font-weight:normal; margin: 20px 0;\">Security</h3>");
            htmlBody.Append("<p style=\"font-size:12px; line-height:18px;\">");
            htmlBody.Append("Some details for user<br />");
            htmlBody.Append("<br />");
            htmlBody.Append("<br />More details for user.");
            htmlBody.Append("</p>");
            htmlBody.Append("<p style=\"font-size:12px; line-height:18px;\">");
            htmlBody.Append("<a href=\"#\">Check security settings</a>");
            htmlBody.Append("</p>");
            htmlBody.Append(" </td>");
            htmlBody.Append("</tr>");
            htmlBody.Append("</table>");
            htmlBody.Append("</center>");
            htmlBody.Append("</td>");
            htmlBody.Append("</tr>");
            htmlBody.Append("</table>");

            htmlBody.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#368ee0\">");
            htmlBody.Append("<tr>");
            htmlBody.Append("<td align=\"center\">");
            htmlBody.Append("<center>");
            htmlBody.Append("<table border=\"0\" width=\"600\" cellpadding=\"0\" cellspacing=\"0\">");
            htmlBody.Append("<tr>");
            htmlBody.Append("<td style=\"color:#ffffff !important; font-size:20px; font-family: Arial, Verdana, sans-serif; padding-left:10px;\" height=\"40\">");
            htmlBody.Append("<center>");
            htmlBody.Append("<p style=\"font-size:12px; line-height:18px;\">");
            htmlBody.Append("If you don't want to get system emails from FLAT please change your email settings.");
            htmlBody.Append("<br />");
            htmlBody.Append("<a href=\"#\" style=\"color:#ffffff !important;\">Click here to change email settings</a>");
            htmlBody.Append("</p>");
            htmlBody.Append("</center>");
            htmlBody.Append("</td>");
            htmlBody.Append("</tr>");
            htmlBody.Append("</table>");
            htmlBody.Append("</center>");
            htmlBody.Append("</td>");
            htmlBody.Append("</tr>");
            htmlBody.Append("</table>");


            return htmlBody.ToString();
        }
    }
}
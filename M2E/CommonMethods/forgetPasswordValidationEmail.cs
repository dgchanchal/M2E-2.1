using System;
using System.Web;
using System.Text;
using M2E.CommonMethods;

namespace M2E.CommonMethods
{
    public class ForgetPasswordValidationEmail
    {
        public void SendForgetPasswordValidationEmailMessage(String toMail, String guid, HttpRequestBase request, string id)
        {
            var sendEmail = new SendEmail();
            if (request.Url != null)
            {
                sendEmail.SendEmailMessage(toMail,
                    "donotreply",
                    "Validate your Account",
                    ForgetPasswordEmailBodyContent(request.Url.Authority, id, guid),
                    null,
                    null,
                    "Zestork - Place to boost your Carrer"
                    );
            }
        }

        private string ForgetPasswordEmailBodyContent(String requestUrlAuthority, String toMail, String guid)
        {
            return toMail.Contains("facebook.com") ? ForgetPasswordEmailBodyContentFacebook(requestUrlAuthority, toMail, guid) : ForgetPasswordEmailBodyContentEmail(requestUrlAuthority, toMail, guid);
        }

        private string ForgetPasswordEmailBodyContentFacebook(String requestUrlAuthority, String toMail, String guid)
        {
            var htmlBody = new StringBuilder();
            htmlBody.Append("Change password for your account <a href=\"http://" + requestUrlAuthority + "/#" + "resetpassword/" + toMail + "/" + guid + "\"> Click here </a>");
            return htmlBody.ToString();
        }

        private string ForgetPasswordEmailBodyContentEmail(String requestUrlAuthority, String toMail, String guid)
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
            htmlBody.Append("<h3 style=\"font-weight:normal; margin: 20px 0;\">Change your Password</h3>");
            htmlBody.Append("<p style=\"font-size:12px; line-height:18px;\">");
            htmlBody.Append("Message for User. <br /><br />");
            //htmlBody.Append("Email: " + toMail + "");
            htmlBody.Append("</p>");
            htmlBody.Append("<p style=\"font-size:12px; line-height:18px;\">");
            htmlBody.Append("<a href=\"http://" + requestUrlAuthority + "/#" + "resetpassword/" + toMail + "/" + guid + "\"> Click here to change your Password </a>");
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
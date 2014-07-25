using System;
using System.Reflection;
using System.Net.Mail;
using System.Configuration;
using System.Net.Mime;
using M2E.Common.Logger;

namespace M2E.CommonMethods
{
    public class SendEmail
    {
        private readonly ILogger _logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        
        String _path;
        MailMessage _mail = new MailMessage();

        public string SendEmailMessage(String toEmailAddrList,String senderName,String subject,String body,String attachmentsFilePathList,String logoPath, String companyDescription)
        {
            var smtpServer = new SmtpClient
            {
                Credentials =
                    new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SmtpEmail"],
                        ConfigurationManager.AppSettings["SmtpPassword"]),
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true
            };
            _mail = new MailMessage();
            var addr = toEmailAddrList.Split(',');
            try
            {
                _mail.From = new MailAddress(ConfigurationManager.AppSettings["SmtpEmail"], senderName, System.Text.Encoding.UTF8);
                Byte i;
                for (i = 0; i < addr.Length; i++)
                    _mail.To.Add(addr[i]);
                _mail.Subject = subject;
                _mail.Body = body;
                if (attachmentsFilePathList != null)
                {
                    var attachments = attachmentsFilePathList.Split(',');
                    for (i = 0; i < attachments.Length; i++)
                        _mail.Attachments.Add(new Attachment(attachments[i]));
                }                
                _path = logoPath;
                if (_path != null)
                {
                    var logo = new LinkedResource(_path) {ContentId = "Logo"};
                    string htmlview = "<html><body><table border=2><tr width=100%><td><img src=cid:Logo alt=companyname /></td><td>" + companyDescription + "</td></tr></table><hr/></body></html>";
                    var alternateView1 = AlternateView.CreateAlternateViewFromString(htmlview + body, null, MediaTypeNames.Text.Html);
                    alternateView1.LinkedResources.Add(logo);
                    _mail.AlternateViews.Add(alternateView1);
                }
                else
                {
                    var alternateView1 = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);                    
                    _mail.AlternateViews.Add(alternateView1);
                }
                _mail.IsBodyHtml = true;
                _mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //mail.ReplyToList = new MailAddress(ConfigurationManager.AppSettings["SmtpEmail"].ToString());
                smtpServer.Send(_mail);
                return "200";
            }
            catch (Exception ex)
            {
                _logger.Error("Exception occured while sending email",ex);
                return "500";
            }
            
        }
    }
}
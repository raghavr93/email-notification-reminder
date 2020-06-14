using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace NotificationApp
{
    class MailSender : IMailSender
    {
        public readonly IDbRespondent _dbRespondent;
        public MailSender(IDbRespondent dbRespondent)
        {
            _dbRespondent = dbRespondent;
        }
        void IMailSender.SendEmail(string emailSubject)
        {
            var respondents = _dbRespondent.GetRespondentsFromDb("dbo.getRespondents");

            foreach (Respondents resp in respondents)
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                message.From = new MailAddress(ConfigurationManager.AppSettings["From"]);
                message.Subject = emailSubject;
                message.IsBodyHtml = false;
                message.To.Add(new MailAddress(resp.EmailId));
                message.Body = "Hello! Is there anybody in there ? "+"http://localhost:51096/api/Survey/"+resp.RespondentId;

                smtp.Port =Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
                smtp.Host = ConfigurationManager.AppSettings["SmtpHost"];
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["From"],ConfigurationManager.AppSettings["Password"]);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                _dbRespondent.Update("dbo.updateSent", resp.RespondentId);
            }
        }
    }
}

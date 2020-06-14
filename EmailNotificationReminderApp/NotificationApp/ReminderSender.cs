using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;


namespace NotificationApp
{
    class ReminderSender : IReminderSender
    {
        private readonly IDbRespondent _dbRespondent;
        public ReminderSender(IDbRespondent dbRespondent)
        {
            _dbRespondent = dbRespondent;
        }

        void IReminderSender.SendEmail( string emailSubject)
        {
            var respondents = _dbRespondent.GetRespondentsFromDb("dbo.getReminderRespondents");
            
            foreach (Respondents resp in respondents)
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                message.From = new MailAddress(ConfigurationManager.AppSettings["From"]);
                message.Subject = emailSubject;
                message.IsBodyHtml = false;
                message.To.Add(new MailAddress(resp.EmailId));
                message.Body = "Hello! Is there anybody in there for a Reminder?";

                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
                smtp.Host = ConfigurationManager.AppSettings["SmtpHost"];
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["From"], ConfigurationManager.AppSettings["Password"]);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                _dbRespondent.Update("dbo.updateReminderCount",resp.RespondentId);

            }


        }

       
    }
}

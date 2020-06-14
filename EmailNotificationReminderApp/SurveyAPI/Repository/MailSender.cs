using SurveyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace SurveyAPI.Repository
{
    public class MailSender : IMailSender
    {
        public void SendMail(string email)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            message.From = new MailAddress(ConfigurationManager.AppSettings["From"]);
            message.Subject = ConfigurationManager.AppSettings["EmailSubject"];
            message.IsBodyHtml = false;
            message.To.Add(new MailAddress(email));
            message.Body = "Thank You! Activation done.";

            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
            smtp.Host = ConfigurationManager.AppSettings["SmtpHost"];
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["From"], ConfigurationManager.AppSettings["Password"]);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);

        }
    }
}
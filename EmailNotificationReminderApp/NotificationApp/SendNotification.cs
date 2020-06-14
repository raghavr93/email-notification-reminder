
using System.Configuration;

namespace NotificationApp
{
    class SendNotification
    {
        private readonly IMailSender _mailSender;
       
        public SendNotification(IMailSender mailSender)
        {
            _mailSender = mailSender;
        }

        
        public void Run()
        {
            Process();
        }

        public void Process()
        {

            _mailSender.SendEmail(ConfigurationManager.AppSettings["surveySubject"]);

        }

    }
}

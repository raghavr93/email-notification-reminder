
using System.Configuration;

namespace NotificationApp
{
    class SendReminder
    {
        private readonly IReminderSender _reminderSender;
       

        public SendReminder(IReminderSender reminderSender)
        {
            _reminderSender = reminderSender;
        }


        public void Run()
        {
            Process();
        }

        public void Process()
        {

          
            _reminderSender.SendEmail( ConfigurationManager.AppSettings["reminderSubject"]);

        }
    }
}

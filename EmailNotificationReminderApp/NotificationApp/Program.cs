using SimpleInjector;
using System;


namespace NotificationApp
{
    class Program
    {
        public static void Main(string []args)
        {
            var container = new Container();

            container.Register<IDbRespondent, DbRespondents>();
            container.Register<IMailSender, MailSender>();
            container.Register<IReminderSender, ReminderSender>();

            switch ("reminder")
            {
                case "survey":
                    container.Register<SendNotification>();
                    var send = container.GetInstance<SendNotification>();
                    send.Run();
                    break;
                case "reminder":
                    container.Register<SendReminder>();
                    var remind = container.GetInstance<SendReminder>();
                    remind.Run();
                    break;
                default:
                    Console.WriteLine("Enter parameter \"survey\" or \"reminder\" ");
                    break;
            }
        }

    }
}

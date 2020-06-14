using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationApp
{
    interface IMailSender
    {
        void SendEmail(string emailSubject);
    }
}

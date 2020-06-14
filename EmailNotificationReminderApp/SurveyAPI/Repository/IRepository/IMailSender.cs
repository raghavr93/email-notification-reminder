using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyAPI.Repository.IRepository
{
    public interface IMailSender
    {
        void SendMail(string email);
    }
}

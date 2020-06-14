using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationApp
{
    interface IDbRespondent
    {
        IEnumerable<Respondents> GetRespondentsFromDb(string spName);

        void Update(string spName, int Id);
        
    }
}

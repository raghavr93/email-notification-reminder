using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace NotificationApp
{
    public class DbRespondents : IDbRespondent
    {
        private readonly string _connString;
        public DbRespondents()
        {
            _connString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }

        public void Update(string spName, int Id)
        {
            {
                using (var connection = new SqlConnection(_connString))
                {
                    connection.Open();

                    connection.Execute(spName,
                                    new { RespondentId = Id },
                                    commandTimeout: 300,
                                    commandType: CommandType.StoredProcedure);
                }
            }
        }

        IEnumerable<Respondents> IDbRespondent.GetRespondentsFromDb(string spName)
        {
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();

                return connection.Query<Respondents>(spName,
                                commandTimeout: 300,
                                commandType: CommandType.StoredProcedure);
            }
        }
    }
}

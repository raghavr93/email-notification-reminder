using Dapper;
using SurveyAPI.Models;
using SurveyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SurveyAPI.Repository
{
    public class ResponseUpdate : IResponseUpdate
    {
        private readonly string _connString;
        public ResponseUpdate()
        {
            _connString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }
        public Respondent UpdateResponse(int Id)
        {
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();

                return connection.QueryFirst<Respondent>("dbo.updateResponse",
                                new { RespondentId = Id },
                                commandTimeout: 300,
                                commandType: CommandType.StoredProcedure);
            }
        }
    }
}
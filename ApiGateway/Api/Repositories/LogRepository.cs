using Api.Configurations.AppSettings;
using Api.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Api.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly ConnectionStrings _connectionStringsConfig;

        public LogRepository(ConnectionStrings connectionStringsConfig)
        {
            _connectionStringsConfig = connectionStringsConfig;
        }
        public List<Logs> GetLogs()
        {
            var listaLogs = new List<Logs>();
            using (IDbConnection db = new SqlConnection(_connectionStringsConfig.SqlConnection))
            {
                listaLogs = db.Query<Logs>("SELECT Id,Message,Level,TimeStamp,Exception FROM LogTable").ToList();
            }

            return listaLogs;
        }
    }
    //create interface
    public interface ILogRepository
    {
        List<Logs> GetLogs();
    }
}

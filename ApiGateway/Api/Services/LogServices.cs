using Api.Models;
using Api.Repositories;

namespace Api.Services
{
    public class LogServices : ILogServices
    {
        private readonly ILogRepository _logRepository;
        public LogServices(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }
        public List<Logs> ObtenerLogs()
        {
            var logs = _logRepository.GetLogs();
            return logs;
        }
    }
    //create interface
    public interface ILogServices
    {
        List<Logs> ObtenerLogs();
    }
}

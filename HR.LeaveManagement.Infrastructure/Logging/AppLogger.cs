using HR.LeaveManagement.Application.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Infrastructure.Logging
{
    public class AppLogger<T> : IAppLogger<T>
    {
        private readonly ILogger<T> logger;
        public AppLogger(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<T>(); 
        }
        public void LogInformaion(string message, params object[] args)
        {
            logger.LogInformation(message,args);
        }

        public void LogWarning(string message, params object[] args)
        {
            logger.LogWarning(message, args);
        }
    }
}

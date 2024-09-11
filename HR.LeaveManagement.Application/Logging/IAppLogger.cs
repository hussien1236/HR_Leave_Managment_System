using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Logging
{
    public interface IAppLogger<T>
    {
        void LogInformaion(string message,params object[] args);
        void LogWarning(string message, params object[] args);
    }
}

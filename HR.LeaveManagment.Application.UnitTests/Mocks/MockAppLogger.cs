using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Application.UnitTests.Mocks
{
    public class MockAppLogger
    {
        public static Mock<IAppLogger<UpdateLeaveTypeCommandHandler>> GetMockAppLogger()
        {
            Mock<IAppLogger<UpdateLeaveTypeCommandHandler>> mockAppLogger = new Mock<IAppLogger<UpdateLeaveTypeCommandHandler>>();
            mockAppLogger.Setup(l => l.LogInformaion(It.IsAny<string>()));
            mockAppLogger.Setup(l => l.LogWarning(It.IsAny<string>()));
            return mockAppLogger;
        }
    }
}

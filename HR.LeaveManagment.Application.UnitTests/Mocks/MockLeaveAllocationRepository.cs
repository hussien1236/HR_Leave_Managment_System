using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Application.UnitTests.Mocks
{
    public class MockLeaveAllocationRepository
    {
        public static Mock<ILeaveAllocationRepository> GetMockLeaveAllocationRepo()
        {
            var leaveAllocations = new List<LeaveAllocation>
            {
                new LeaveAllocation
                {
                    Id=1,
                    LeaveTypeId=1,
                    EmployeeId="employee_1",
                    Period=10,
                    NbOfDays=10
                },
                new LeaveAllocation
                {
                    Id=2,
                    LeaveTypeId=2,
                    EmployeeId="employee_2",
                    Period=10,
                    NbOfDays=10
                },
                new LeaveAllocation
                {
                    Id=3,
                    LeaveTypeId=3,
                    EmployeeId="employee_3",
                    Period=10,
                    NbOfDays=10
                },
            };
            var MockLeaveAllocationRepo = new Mock<ILeaveAllocationRepository>();
            MockLeaveAllocationRepo.Setup(q => q.GetAsync()).ReturnsAsync(leaveAllocations);
            MockLeaveAllocationRepo.Setup(q => q.CreateAsync(It.IsAny<LeaveAllocation>())).Returns((LeaveAllocation leaveAllocaion) =>
            {
                leaveAllocations.Add(leaveAllocaion);
                return Task.CompletedTask;
            });
            return MockLeaveAllocationRepo;
        }
    }
}

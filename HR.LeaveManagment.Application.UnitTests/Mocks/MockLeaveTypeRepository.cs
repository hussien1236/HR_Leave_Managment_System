using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Domain;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Application.UnitTests.Mocks
{
    public class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetMockLeaveTypeRepository()
        {
            var leaveTypes = new List<LeaveType>
            {
                new LeaveType
                {
                    Id = 1,
                    DefaultDays = 10,
                    Name = "Test Vacation"
                },
                new LeaveType
                {
                    Id = 2,
                    DefaultDays = 15,
                    Name = "Test Case"
                },
                new LeaveType
                {
                    Id = 3,
                    DefaultDays = 15,
                    Name = "Test Maternity"
                }
            };

            var mockRepo = new Mock<ILeaveTypeRepository>();

            mockRepo.Setup(q => q.GetAsync()).ReturnsAsync(leaveTypes);

            mockRepo.Setup(q => q.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) =>
               leaveTypes.FirstOrDefault(l => l.Id == id)
            );

            mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<LeaveType>()))
            .ReturnsAsync((LeaveType newLeaveType) =>
            {
                leaveTypes.Add(newLeaveType); // Simulate adding the new entity to the list
                return Unit.Value;
            });
            mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<LeaveType>()))
            .ReturnsAsync((LeaveType newLeaveType) =>
            {
                var index = leaveTypes.FindIndex(l => l.Id == newLeaveType.Id); // Simulate adding the new entity to the list
                leaveTypes[index] = newLeaveType;
                return Unit.Value;
            });
            mockRepo.Setup(repo => repo.IsLeaveTypeUnique(It.IsAny<string>())).ReturnsAsync((string Name) => !leaveTypes.Any(l => l.Name == Name)
            );
            mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<LeaveType>())).ReturnsAsync((LeaveType leaveType) =>
            {
                leaveTypes.Remove(leaveType);
                return Unit.Value;
            });

            return mockRepo;
        }
    }
}

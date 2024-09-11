using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
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
    public class MockLeaveRequestRepository
    {
        public static Mock<ILeaveRequestRepository> GetMockLeaveRequestRepo() 
        {
            var leaveRequests = new List<LeaveRequest>
            {
                new LeaveRequest
    {
        Id = 1,
        StartDate = new DateTime(2024, 9, 10),
        EndDate = new DateTime(2024, 9, 15),
        LeaveTypeId = 1,
        DateRequested = new DateTime(2024, 9, 1),
        RequestComments = "Vacation leave for a family trip.",
        Approved = true,
        Cancelled = false,
        RequestingEmployeeId = "EMP001"
    },
    new LeaveRequest
    {
        Id = 2,
        StartDate = new DateTime(2024, 10, 1),
        EndDate = new DateTime(2024, 10, 5),
        LeaveTypeId = 2,
        DateRequested = new DateTime(2024, 9, 20),
        RequestComments = "Medical leave for a minor surgery.",
        Approved = false,
        Cancelled = false,
        RequestingEmployeeId = "EMP002"
    },
    new LeaveRequest
    {
        Id = 3,
        StartDate = new DateTime(2024, 11, 12),
        EndDate = new DateTime(2024, 11, 14),
        LeaveTypeId = 3,
        DateRequested = new DateTime(2024, 11, 5),
        RequestComments = "Attending a conference.",
        Approved = true,
        Cancelled = false,
        RequestingEmployeeId = "EMP003"
    }
            };
            var mockRepo = new Mock<ILeaveRequestRepository>();
            mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(leaveRequests);
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync( (int id) => leaveRequests.FirstOrDefault(l=>l.Id==id));
            mockRepo.Setup(r=> r.CreateAsync(It.IsAny<LeaveRequest>())).ReturnsAsync((LeaveRequest leaveRequest) =>
            {
                leaveRequests.Add(leaveRequest);
                return Unit.Value;
            });
            mockRepo.Setup(r=> r.UpdateAsync(It.IsAny<LeaveRequest>())).ReturnsAsync((LeaveRequest leaveRequest) =>
            {
                var index = leaveRequests.FindIndex(l => l.Id == leaveRequest.Id);
                leaveRequests[index] = leaveRequest;
                return Unit.Value;
            });
            mockRepo.Setup(r=> r.CancelLeaveRequest(It.IsAny<int>())).ReturnsAsync((LeaveRequest leaveRequest) =>
            {
                var index = leaveRequests.FindIndex(l => l.Id == leaveRequest.Id);
                leaveRequests[index].Cancelled = true;
                return Unit.Value;
            });
            mockRepo.Setup(r=> r.ChangeLeaveRequestApproval(It.IsAny<ChangeLeaveRequestApprovalCommand>())).ReturnsAsync((ChangeLeaveRequestApprovalCommand leaveRequest) =>
            {
                var index = leaveRequests.FindIndex(l => l.Id == leaveRequest.Id);
                leaveRequests[index].Approved = leaveRequest.Approved;
                return Unit.Value;
            });
            return mockRepo;
        }
    }
}
using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Domain;
using HR.LeaveManagment.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Application.UnitTests.Features.LeaveRequests.Queries
{
    public class GetLeaveRequestDetailsQueryHandlerTests
    {
        private Mock<ILeaveRequestRepository> mockLeaveRequestRepo;
        private IMapper mapper;

        public GetLeaveRequestDetailsQueryHandlerTests()
        {
            mockLeaveRequestRepo = MockLeaveRequestRepository.GetMockLeaveRequestRepo();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveRequestProfile>();
            });
            mapper = mapperConfig.CreateMapper();
        }
        [Fact]
        public async Task GetLeaveRequestDetailsTest()
        {
            var leaveRequest = new LeaveRequestDetailsDto
            {
                StartDate = new DateTime(2024, 10, 1),
                EndDate = new DateTime(2024, 10, 5),
                LeaveTypeId = 2,
                DateRequested = new DateTime(2024, 9, 20),
                RequestComments = "Medical leave for a minor surgery.",
                Approved = false,
                Cancelled = false,
                RequestingEmployeeId = "EMP002"
            };
            var handler = new GetLeaveRequestDetailsQueryHandler(mapper, mockLeaveRequestRepo.Object);
            var result = await handler.Handle(new GetLeaveRequestDetailsQuery { Id = 2}, CancellationToken.None);
            Assert.NotNull(result);
            result.ShouldBeOfType<LeaveRequestDetailsDto> ();
            Assert.Equal(leaveRequest.StartDate, result.StartDate);
            Assert.Equal(leaveRequest.EndDate, result.EndDate);
            Assert.Equal(leaveRequest.LeaveTypeId, result.LeaveTypeId);
            Assert.Equal(leaveRequest.DateRequested, result.DateRequested);
            Assert.Equal(leaveRequest.RequestComments, result.RequestComments);
            Assert.Equal(leaveRequest.Approved, result.Approved);
            Assert.Equal(leaveRequest.Cancelled, result.Cancelled);
            Assert.Equal(leaveRequest.RequestingEmployeeId, result.RequestingEmployeeId);

        }
    }
}

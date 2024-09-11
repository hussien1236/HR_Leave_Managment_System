using AutoMapper;
using Castle.Core.Smtp;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagment.Application.UnitTests.Mocks;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Application.UnitTests.Features.LeaveRequests.Commands
{
    public class CreateLeaveRequestCommandHandlerTests
    {
        private IMapper mapper;
        private Mock<LeaveManagement.Application.Contracts.Email.IEmailSender> mockEmailSender;
        private Mock<ILeaveTypeRepository> mockLeaveTypeRepository;
        private Mock<ILeaveRequestRepository> mockLeaveRequestRepository;

        public CreateLeaveRequestCommandHandlerTests()
        {
            mockLeaveTypeRepository = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
            mockLeaveRequestRepository = MockLeaveRequestRepository.GetMockLeaveRequestRepo();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new LeaveRequestProfile());
            });
            mapper = mapperConfig.CreateMapper();
            mockEmailSender = MockEmailSender.GetEmailSender();
        }
        [Fact]
        public async Task CreateLeaveRequestTests()
        {
            var handler =new CreateLeaveRequestCommandHandler(mockLeaveTypeRepository.Object,mockLeaveRequestRepository.Object,mapper,mockEmailSender.Object);
            var result = await handler.Handle(new CreateLeaveRequestCommand
            {
                StartDate = new DateTime(2024, 11, 12),
                EndDate = new DateTime(2024, 11, 14),
                LeaveTypeId = 3,
                RequestComments = "hello world"
            },CancellationToken.None);
            var leaveRequests = await mockLeaveRequestRepository.Object.GetAsync();
            leaveRequests.Count.ShouldBe(4);
        }
    }
}

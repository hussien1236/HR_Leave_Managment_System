using AutoMapper;
using Castle.Core.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Logging;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagment.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Application.UnitTests.Features.LeaveTypes.Queries
{
    public class GetLeaveTypeListQueryHandlerTests
    {
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private  IMapper _mapper;
        private Mock<IAppLogger<GetLeaveTypeListQueryHandler>> _MockAppLogger;
        public GetLeaveTypeListQueryHandlerTests()
        {
            _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveTypeProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _MockAppLogger = new Mock<IAppLogger<GetLeaveTypeListQueryHandler>>();
        }
        [Fact]
        public async Task GetLeaveTypeListTest()
        {
            var handler = new GetLeaveTypeListQueryHandler(_mockRepo.Object, _mapper, _MockAppLogger.Object);
            var result = await handler.Handle(new GetLeaveTypeListQuery(),CancellationToken.None);
            result.ShouldBeOfType<List<LeaveTypeDto>>();
            result.Count.ShouldBe(3);
        }
    }
}

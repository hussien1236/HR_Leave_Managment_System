using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using HR.LeaveManagement.Application.MappingProfiles;
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
    public class GetLeaveRequestListQueryHandlerTests
    {
        private Mock<ILeaveRequestRepository> mockLeaveRequestRepo;
        private IMapper mapper;

        public GetLeaveRequestListQueryHandlerTests()
        {
            mockLeaveRequestRepo = MockLeaveRequestRepository.GetMockLeaveRequestRepo();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveRequestProfile>();
            });
            mapper = mapperConfig.CreateMapper();
        }
        [Fact]
        public async Task GetLeaveRequestListTest()
        {
            var handler = new GetLeaveRequestListQueryHandler(mockLeaveRequestRepo.Object, mapper);
            var result = await handler.Handle(new GetLeaveRequestListQuery(), CancellationToken.None);
            Assert.NotNull(result);
            result.ShouldBeOfType<List<LeaveRequestListDto>>();
            result.Count.ShouldBe(3);
        }
    }
}

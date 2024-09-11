using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
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
    public class GetLeaveTypeDetailsQueryHandlerTests
    {
        private Mock<ILeaveTypeRepository> _MockLeaveTypeRepository;
        private IMapper _mapper;
        public GetLeaveTypeDetailsQueryHandlerTests()
        {
            _MockLeaveTypeRepository = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
            var mapperConfig = new MapperConfiguration(c => {
                c.AddProfile<LeaveTypeProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }
        [Fact]
        public async Task GetLeaveTypeDetailsTest()
        {
            var handler = new GetLeaveTypeDetailsQueryHandler(_MockLeaveTypeRepository.Object, _mapper);

            var result = await handler.Handle(new GetLeaveTypeDetailsQuery(2),CancellationToken.None);

            Assert.NotNull(result);
            result.ShouldBeOfType<LeaveTypeDetailsDto>();
            Assert.Equal(2, result.Id);
            Assert.Equal("Test Case", result.Name);
            Assert.Equal(15, result.DefaultDays);
        }
    }
}

using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagment.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Application.UnitTests.Features.LeaveTypes.Commands
{
    public class CreateLeaveTypeDetailsCommandHandlerTests
    {
        private readonly Mock<ILeaveTypeRepository> leaveTypeRepository;
        private readonly IMapper mapper;
        public CreateLeaveTypeDetailsCommandHandlerTests()
        {
            leaveTypeRepository = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
            var mapperConfiguration = new MapperConfiguration(c=>
            {
                c.AddProfile<LeaveTypeProfile>();
            });
            mapper = mapperConfiguration.CreateMapper();
        }
        [Fact]
        public async void CreateLeaveTypeTest()
        {
            var handler = new CreateLeaveTypeCommandHandler(leaveTypeRepository.Object, mapper);
            await handler.Handle(new CreateLeaveTypeCommand 
            { Name = "sick vacation",
                DefaultDays = 12 }, 
             CancellationToken.None);
            var leaveList = await leaveTypeRepository.Object.GetAsync();
            Assert.Equal(leaveList.Count, 4);

        }
    }
}

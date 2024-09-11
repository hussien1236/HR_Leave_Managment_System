using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Logging;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Domain;
using HR.LeaveManagment.Application.UnitTests.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Application.UnitTests.Features.LeaveTypes.Commands
{
    public class UpdateLeaveTypeCommandHandlerTests
    {
        private readonly Mock<ILeaveTypeRepository> _mockLeaveTypeRepository;
        private readonly IMapper mapper;
        private readonly Mock<IAppLogger<UpdateLeaveTypeCommandHandler>> _logger;
        public UpdateLeaveTypeCommandHandlerTests()
        {
            _mockLeaveTypeRepository = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveTypeProfile>();
            });
            mapper = mapperConfig.CreateMapper();
            _logger = MockAppLogger.GetMockAppLogger();
        }
        [Fact]
        public async Task UpdateLeaveTypeTest()
        {
            var handler = new UpdateLeaveTypeCommandHandler(_mockLeaveTypeRepository.Object, mapper, _logger.Object);
            var updateLeaveCommand = new UpdateLeaveTypeCommand
            {
                Id = 1,
                Name = "weekend off",
                DefaultDays = 2
            };
            await handler.Handle(updateLeaveCommand, CancellationToken.None);

            var UpdatedLeaveType = await _mockLeaveTypeRepository.Object.GetByIdAsync(1);

            //Assert
            Assert.Equal(UpdatedLeaveType.Name,updateLeaveCommand.Name);
            Assert.Equal(UpdatedLeaveType.DefaultDays, updateLeaveCommand.DefaultDays);
        }
    }
}

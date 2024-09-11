using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveAllocation.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
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
    public class DeleteLeaveTypeCommandHandlerTests
    {
        private Mock<ILeaveTypeRepository> _mockLeaveTypeRepo;

        public DeleteLeaveTypeCommandHandlerTests()
        {
            _mockLeaveTypeRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
        }
        [Fact]
        public async Task DeleteLeaveTypeCommandTest()
        {
            var handler = new DeleteLeaveTypeCommandHandler(_mockLeaveTypeRepo.Object);
            await handler.Handle(new DeleteLeaveTypeCommand { Id = 1}, CancellationToken.None);
            var leaveTypeList = await _mockLeaveTypeRepo.Object.GetAsync();
            leaveTypeList.Count.ShouldBe(2);
            leaveTypeList.FirstOrDefault(l => l.Id == 1).ShouldBeNull();
        }
    }
}

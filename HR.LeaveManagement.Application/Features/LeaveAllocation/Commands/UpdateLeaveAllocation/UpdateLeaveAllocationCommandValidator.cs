using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;

        public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository )
        {
            this.leaveTypeRepository = leaveTypeRepository;
            this.leaveAllocationRepository = leaveAllocationRepository;
            RuleFor(q => q.NumberOfDays)
                .GreaterThan(0)
                .WithMessage("{ProperyName} must be greater that {ComparisonValue}");
            RuleFor(q => q.Period)
                .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be after {ComparisonValue}");
            RuleFor(q => q.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(LeaveTypeMustExist)
                .WithMessage("{PropertyName} does not exist.");
            RuleFor(q => q.Id)
                .NotNull()
                .MustAsync(LeaveAllcoationMustExist)
                .WithMessage("{PropertyName} must be present");
        }

        private async Task<bool> LeaveAllcoationMustExist(int id, CancellationToken token)
        {
            var leaveAllocation = await leaveAllocationRepository.GetByIdAsync(id);
            return leaveAllocation != null ;
        }

        private async Task<bool> LeaveTypeMustExist(int LeaveTypeId, CancellationToken token)
        {
            var leaveType = await leaveTypeRepository.GetByIdAsync(LeaveTypeId);
            return leaveType != null;
        }
    }
}

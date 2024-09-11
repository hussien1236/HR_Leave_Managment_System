using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Shared
{
    public class BaseLeaveRequestValidator : AbstractValidator<BaseLeaveRequest>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public BaseLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            try
            {
                _leaveTypeRepository = leaveTypeRepository;
                RuleFor(q => q.StartDate)
                    .LessThan(q => q.EndDate).WithMessage("{PropertyName} must be before {ComparisonValue}");
                RuleFor(q => q.EndDate)
                    .GreaterThan(q => q.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue}");
                RuleFor(q => q.LeaveTypeId)
                    .GreaterThan(0);
                RuleFor(q => q)
                    .MustAsync(LeaveTypeMustExist)
                    .WithMessage("{PropertyName} does not exist.");
            }
            catch(Exception e)
            {
                throw new BadRequestException("---------------------error occured"+e.StackTrace);
            }
        }

        private async Task<bool> LeaveTypeMustExist(BaseLeaveRequest request, CancellationToken token)
        {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);
            return leaveType != null;
        }
    }
}

using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandValidator : AbstractValidator<UpdateLeaveRequestCommand>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveRequestCommandValidator(ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            Include(new BaseLeaveRequestValidator(_leaveTypeRepository));
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .MustAsync(LeaveRequestMustExist)
                .WithMessage("{PropertyName} must exist");
        }

        private async Task<bool> LeaveRequestMustExist(int id, CancellationToken token)
        {
            var leaveRequest = await leaveRequestRepository.GetByIdAsync(id);
            return leaveRequest != null;
        }
    }
}

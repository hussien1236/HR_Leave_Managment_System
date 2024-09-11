using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(q => q.Id)
                .NotNull()
                .MustAsync(LeaveTypeMustExist);
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} must not be empty")
                .NotNull()
                .MaximumLength(70).WithMessage("{PropertyName} Length must not exceed 70");
            RuleFor(p => p.DefaultDays)
               .LessThan(100).WithMessage("{PropertyName} cannot exceed 100")
               .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");
            RuleFor(p => p)
                .MustAsync(LeaveTypeNameUnique)
                .WithMessage("LeaveType already exist");
            this.leaveTypeRepository = leaveTypeRepository;
        }

        private async Task<bool> LeaveTypeMustExist(int Id, CancellationToken token)
        {
            var result = await leaveTypeRepository.GetByIdAsync(Id);
            return result != null;
        }
        private async Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand command, CancellationToken token)
        {
            return await leaveTypeRepository.IsLeaveTypeUnique(command.Name);
        }
    }
}

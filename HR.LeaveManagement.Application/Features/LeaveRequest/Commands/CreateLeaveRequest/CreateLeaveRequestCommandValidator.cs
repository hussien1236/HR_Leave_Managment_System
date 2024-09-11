using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandValidator : AbstractValidator<CreateLeaveRequestCommand>
    {
        public CreateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            try
            {
                Include(new BaseLeaveRequestValidator(leaveTypeRepository));
            }
            catch (Exception e)
            {
                throw new BadRequestException("---------------------------------Error from create validator :"+e.StackTrace);
            }
        }
    }
}

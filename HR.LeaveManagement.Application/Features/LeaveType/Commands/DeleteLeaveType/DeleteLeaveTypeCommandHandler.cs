using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {      
            //Retrieve LeaveType object
            var leaveType = await leaveTypeRepository.GetByIdAsync(request.Id);
            //verify record exists
            if (leaveType is null)
                throw new NotFoundException(nameof(leaveType), request.Id);
            //delete LeaveType Object from db
            await leaveTypeRepository.DeleteAsync(leaveType);
            //return Unit value
            return Unit.Value;
        }
    }
}

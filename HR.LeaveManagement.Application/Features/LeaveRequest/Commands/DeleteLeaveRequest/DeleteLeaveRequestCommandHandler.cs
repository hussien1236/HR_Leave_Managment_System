using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest
{
    public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand>
    {
        private readonly ILeaveTypeRepository leaveRequestRepository;

        public DeleteLeaveRequestCommandHandler(ILeaveTypeRepository leaveRequestRepository)
        {
            this.leaveRequestRepository = leaveRequestRepository;
        }
        public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);
            if(leaveRequest is null)
                throw new NotFoundException(nameof(leaveRequest),request.Id);
            await leaveRequestRepository.DeleteAsync(leaveRequest);
            return Unit.Value;
        }
    }
}

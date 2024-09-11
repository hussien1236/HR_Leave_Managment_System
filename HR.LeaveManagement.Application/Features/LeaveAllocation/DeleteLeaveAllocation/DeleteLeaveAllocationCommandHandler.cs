using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.DeleteLeaveAllocation
{
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand>
    {
        private readonly ILeaveAllocationRepository leaveAllocationRepository;

        public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository)
        {
            this.leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocaiton = await leaveAllocationRepository.GetByIdAsync(request.Id);
            if (leaveAllocaiton is null)
                throw new NotFoundException(nameof(leaveAllocaiton),request.Id);
            await leaveAllocationRepository.DeleteAsync(leaveAllocaiton);
            return Unit.Value;
        }
    }
}

using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper,
            ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var Validator = new CreateLeaveAllocationCommandValidator(leaveTypeRepository);
            var validationResult = await Validator.ValidateAsync(request);
            if (validationResult.Errors.Any())
            {
                throw new BadRequestException("Invalid leave allocation request", validationResult);
            }
            var leaveType = await leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);
            var leaveAllocation = mapper.Map<Domain.LeaveAllocation>(request);
            await leaveAllocationRepository.CreateAsync(leaveAllocation);
            return Unit.Value;
        }
    }
}

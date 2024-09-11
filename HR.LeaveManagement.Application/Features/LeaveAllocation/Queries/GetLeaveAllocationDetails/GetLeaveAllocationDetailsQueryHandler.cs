using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails
{
    public class GetLeaveAllocationDetailsQueryHandler : IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDetailsDto>
    {
        private readonly IMapper mapper;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;

        public GetLeaveAllocationDetailsQueryHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
        {
            this.mapper = mapper;
            this.leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailsQuery request, CancellationToken cancellationToken)
        {
            var leaveAllocationDetails = await leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);
            var leaveAllocationDetailsDto = mapper.Map<LeaveAllocationDetailsDto>(leaveAllocationDetails);
            return leaveAllocationDetailsDto;
        }
    }
}

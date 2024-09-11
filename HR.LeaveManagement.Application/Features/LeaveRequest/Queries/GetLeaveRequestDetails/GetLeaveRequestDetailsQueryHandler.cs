using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails
{
    public class GetLeaveRequestDetailsQueryHandler : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDto>
    {
        private readonly IMapper mapper;
        private readonly ILeaveRequestRepository leaveRequestRepository;

        public GetLeaveRequestDetailsQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository)
        {
            this.mapper = mapper;
            this.leaveRequestRepository = leaveRequestRepository;
        }
        public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
        {
            var LeaveRequestDetails = await leaveRequestRepository.GetByIdAsync(request.Id);
            var LeaveRequestDetailsDto = mapper.Map<LeaveRequestDetailsDto>(LeaveRequestDetails);
            return LeaveRequestDetailsDto;
        }
    }
}

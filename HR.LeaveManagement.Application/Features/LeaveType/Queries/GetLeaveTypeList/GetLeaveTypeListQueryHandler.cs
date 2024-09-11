using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Logging;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public class GetLeaveTypeListQueryHandler : IRequestHandler<GetLeaveTypeListQuery, List<LeaveTypeDto>>
    {
        private readonly ILeaveTypeRepository _LeaveTypeRepository;
        private readonly IMapper mapper;
        private readonly IAppLogger<GetLeaveTypeListQueryHandler> logger;

        public GetLeaveTypeListQueryHandler(ILeaveTypeRepository LeaveTypeRepository, IMapper mapper, IAppLogger<GetLeaveTypeListQueryHandler> logger)
        {
            _LeaveTypeRepository = LeaveTypeRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypeListQuery request, CancellationToken cancellationToken)
        {
            //Query Leave Types
            var LeaveTypes = await _LeaveTypeRepository.GetAsync();
            //auto map from leavetype to leavetypedto
            var data = mapper.Map<List<LeaveTypeDto>>(LeaveTypes);
            //return the list of leavetypedto
            logger.LogInformaion("Leave types are retrieved successfully");
            return data;
         }
    }
}

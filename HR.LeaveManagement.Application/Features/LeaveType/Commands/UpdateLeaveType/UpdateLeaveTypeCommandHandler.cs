using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Logging;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IMapper mapper;
        private readonly IAppLogger<UpdateLeaveTypeCommandHandler> logger;

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper ,IAppLogger<UpdateLeaveTypeCommandHandler> logger)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            //Validate incomming data 
            var Validator = new UpdateLeaveTypeCommandValidator(leaveTypeRepository);
            var ValidationResults = await Validator.ValidateAsync(request, cancellationToken);
            if (ValidationResults.Errors.Any())
            {
                logger.LogWarning("Validation errors in updates request for {0} - {1}",
                    nameof(LeaveType), request.Id);
                throw new BadRequestException("Invalid Leavetype", ValidationResults);
            }
            //convert domain entity object
            var leaveType = mapper.Map<Domain.LeaveType>(request);
            //Update in db
            await leaveTypeRepository.UpdateAsync(leaveType);
            //return unit value
            return Unit.Value;
        }
    }
}

using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Logging;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly IMapper mapper;
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IEmailSender emailSender;
        private readonly IAppLogger<UpdateLeaveRequestCommandHandler> appLogger;

        public UpdateLeaveRequestCommandHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository, 
            ILeaveTypeRepository leaveTypeRepository, IEmailSender emailSender, IAppLogger<UpdateLeaveRequestCommandHandler> appLogger)
        {
            this.mapper = mapper;
            this.leaveRequestRepository = leaveRequestRepository;
            this.leaveTypeRepository = leaveTypeRepository;
            this.emailSender = emailSender;
            this.appLogger = appLogger;
        }
        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);
            if (leaveRequest is null)
                throw new NotFoundException(nameof(leaveRequest), request.Id);
            var validator = new UpdateLeaveRequestCommandValidator(leaveRequestRepository,leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Leave Request", validationResult);
            mapper.Map(request, leaveRequest);
            await leaveRequestRepository.UpdateAsync(leaveRequest);
            try
            {
                var email = new EmailMessage
                {
                    To = string.Empty,
                    Body = $"Your leave request for {request.StartDate} to {request.EndDate}" +
                    $" has been updated successfully.",
                    Subject = "Leave Request Updated"
                };
                await emailSender.SendEmail(email);
            }
            catch(Exception ex)
            {
                appLogger.LogWarning(ex.Message);
            }
            return Unit.Value;
        }
    }
}

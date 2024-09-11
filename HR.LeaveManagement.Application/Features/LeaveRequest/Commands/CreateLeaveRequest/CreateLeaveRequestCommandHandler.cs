using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Logging;
using HR.LeaveManagement.Application.Models.Email;
using HR.LeaveManagement.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IMapper mapper;
        private readonly IEmailSender emailSender;

        public CreateLeaveRequestCommandHandler(ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository,
            IMapper mapper, IEmailSender emailSender)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            this.leaveRequestRepository = leaveRequestRepository;
            this.mapper = mapper;
            this.emailSender = emailSender;
        }
        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestCommandValidator(leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count() > 0)
                throw new BadRequestException("Invalid Leave Request", validationResult);
            
            var leaveRequest = mapper.Map<Domain.LeaveRequest>(request);
            await leaveRequestRepository.CreateAsync(leaveRequest);

            var emailMessage = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your Leave Request for {request.StartDate:D} to {request.EndDate:D} " +
                "has been submitted successfully.",
                Subject = "Leave Request Submitted"
            };
            await emailSender.SendEmail(emailMessage);
            return Unit.Value;
        }
    }
}

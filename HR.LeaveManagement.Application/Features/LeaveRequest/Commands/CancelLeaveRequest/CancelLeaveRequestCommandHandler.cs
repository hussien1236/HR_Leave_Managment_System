﻿using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest
{
    public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IEmailSender emailSender;

        public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IEmailSender emailSender)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.emailSender = emailSender;
        }
        public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);
            if (leaveRequest is null)
                throw new NotFoundException(nameof(leaveRequest), request.Id);
            leaveRequest.Cancelled = true;
            await leaveRequestRepository.UpdateAsync(leaveRequest);
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your Leave Request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} " +
                "has been cancelled Successfully.",
                Subject = "Leave Request Cancelled"
            };
            await emailSender.SendEmail(email);
            return Unit.Value;
        }
    }
}

using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval
{
    public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IEmailSender emailSender;

        public ChangeLeaveRequestApprovalCommandHandler(ILeaveRequestRepository leaveRequestRepository,
            IEmailSender emailSender)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.emailSender = emailSender;
        }
        public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);
            if (leaveRequest is null)
                throw new NotFoundException(nameof(leaveRequest), request.Id);
            leaveRequest.Approved = request.Approved;
            await leaveRequestRepository.UpdateAsync(leaveRequest);

            //send email
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"The approval status for your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} " +
                "has been updated.",
                Subject = "Leave Request Approval Status Updated"
            };
            await emailSender.SendEmail(email);
            return Unit.Value;
        }
    }
}

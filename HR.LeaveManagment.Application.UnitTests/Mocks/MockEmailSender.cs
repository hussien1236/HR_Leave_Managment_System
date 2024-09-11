using Castle.Core.Smtp;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Models.Email;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IEmailSender = HR.LeaveManagement.Application.Contracts.Email.IEmailSender;

namespace HR.LeaveManagment.Application.UnitTests.Mocks
{
    public class MockEmailSender
    {
        public static Mock<IEmailSender> GetEmailSender()
        {
            var mockRepo = new Mock<IEmailSender>();
            mockRepo.Setup(r => r.SendEmail(It.IsAny<EmailMessage>()));
            return mockRepo;
        } 
    }
}

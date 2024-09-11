using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly HRDbContext context;

        public LeaveRequestRepository(HRDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Unit> CancelLeaveRequest(int id)
        {
            var leaveRequest = await context.LeaveRequests.FindAsync(id);
            leaveRequest.Cancelled = true;
            return Unit.Value;
        }

        public async Task<Unit> ChangeLeaveRequestApproval(ChangeLeaveRequestApprovalCommand command)
        {
            var leaveRequest = await context.LeaveRequests.FindAsync(command.Id);
            leaveRequest.Approved = command.Approved;
            return Unit.Value;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            return await context.LeaveRequests.Include(q=> q.LeaveType).ToListAsync();
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
        {
            return await context.LeaveRequests.Where(q=> q.RequestingEmployeeId == userId).Include(q=>q.LeaveType).ToListAsync();
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
        {
            return await context.LeaveRequests.Include(q => q.LeaveType).FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}

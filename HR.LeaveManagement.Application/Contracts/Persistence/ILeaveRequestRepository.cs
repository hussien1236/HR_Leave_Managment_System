using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<LeaveRequest> GetLeaveRequestWithDetails(int id);
        Task<List<LeaveRequest>> GetLeaveRequestsWithDetails();
        Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId);
        Task<Unit> CancelLeaveRequest(int id);

        Task<Unit> ChangeLeaveRequestApproval(ChangeLeaveRequestApprovalCommand changeLeaveRequestApprovalCommand);
    }
}
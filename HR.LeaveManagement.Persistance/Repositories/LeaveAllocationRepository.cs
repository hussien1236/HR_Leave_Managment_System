using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly HRDbContext context;

        public LeaveAllocationRepository(HRDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
            await context.AddRangeAsync(allocations);
            await context.SaveChangesAsync();
        }

        public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
        {
            return await context.LeaveAllocations.AnyAsync(q => q.EmployeeId == userId && q.LeaveTypeId == leaveTypeId && q.Period == period);
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            return await context.LeaveAllocations.Include(q=> q.LeaveType).ToListAsync();
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
        {
            return await context.LeaveAllocations.Where(q=> q.EmployeeId == userId).Include(q => q.LeaveType).ToListAsync();
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            return await context.LeaveAllocations.Include(q => q.LeaveType).FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<LeaveAllocation> GetUserAllocations(string userId, int LeaveTypeId)
        {
            return await context.LeaveAllocations.FirstOrDefaultAsync(q => q.EmployeeId == userId && q.LeaveTypeId == LeaveTypeId);                
        }
    }    }

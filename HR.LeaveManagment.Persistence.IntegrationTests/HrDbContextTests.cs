using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.LeaveManagment.Persistence.IntegrationTests
{
    public class HrDbContextTests
    {
        private HRDbContext _HrDbContext;

        public HrDbContextTests()
        {
            var HrDbContextOptions = new DbContextOptionsBuilder<HRDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _HrDbContext = new HRDbContext(HrDbContextOptions);
        }
        [Fact]
        public async void Save_SetDateCreatedValue()
        {
            var leaveType = new LeaveType
            {
                Id=1,
                Name="Vacation",
                DefaultDays=10
            };
            await _HrDbContext.LeaveTypes.AddAsync(leaveType);
            await _HrDbContext.SaveChangesAsync();

            //assert
            leaveType.DateCreated.ShouldNotBeNull();
        }
        [Fact]
        public async void Save_SetDateModifiedValue()
        {
            var leaveType = new LeaveType
            {
                Id = 1,
                Name = "Vacation",
                DefaultDays = 10
            };
            await _HrDbContext.LeaveTypes.AddAsync(leaveType);
            await _HrDbContext.SaveChangesAsync();

            //assert
            leaveType.DateModified.ShouldNotBeNull();
        }
    }
}
﻿using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        private readonly HRDbContext context;

        public LeaveTypeRepository(HRDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<bool> IsLeaveTypeUnique(string name)
        {
            return await context.LeaveTypes.AnyAsync(q => q.Name==name) == false;
        }
    }
}

using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly HRDbContext context;

        public GenericRepository(HRDbContext context)
        {
            this.context = context;
        }
        public async Task<Unit> CreateAsync(T entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<Unit> DeleteAsync(T entity)
        {
            context.Remove(entity);
            await context.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<IReadOnlyList<T>> GetAsync()
        {
            return await context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Unit> UpdateAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}

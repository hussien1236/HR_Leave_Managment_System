using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<Unit> CreateAsync(T entity);
        Task<Unit> UpdateAsync(T entity);
        Task<Unit> DeleteAsync(T entity);
        Task<IReadOnlyList<T>> GetAsync();
        Task<T> GetByIdAsync(int id);

    }    
}
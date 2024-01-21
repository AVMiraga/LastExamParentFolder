using ExamTask.Core.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ExamTask.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        public DbSet<T> Table { get; }

        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task CreateAsync(T entity);
        void Delete(T entity);
        void Update(T entity);

        Task SaveChangesAsnyc();
    }
}

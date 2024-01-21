using ExamTask.Core.Entities.Common;
using ExamTask.DAL.Context;
using ExamTask.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExamTask.DAL.Repositories.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task CreateAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;

            Table.Update(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IQueryable<T> entites = Table.Where(x => !x.IsDeleted);

            return await entites.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T entity = await Table.AsNoTracking().Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();

            return entity;
        }

        public async Task SaveChangesAsnyc()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            Table.Update(entity);
        }
    }
}

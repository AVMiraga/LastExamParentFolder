using ExamTask.Core.Entities;
using ExamTask.DAL.Context;
using ExamTask.DAL.Repositories.Interfaces;

namespace ExamTask.DAL.Repositories.Implementation
{
    public class PortfolioRepository : Repository<Portfolio>, IPortfolioRepository
    {
        public PortfolioRepository(AppDbContext context) : base(context)
        {
        }
    }
}

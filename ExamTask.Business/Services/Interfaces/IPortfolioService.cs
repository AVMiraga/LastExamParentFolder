using ExamTask.Business.ViewModels.EntityVms;
using ExamTask.Core.Entities;

namespace ExamTask.Business.Services.Interfaces
{
    public interface IPortfolioService
    {
        Task<Portfolio> GetByIdAsync(int id);
        Task<IEnumerable<Portfolio>> GetAllAsync();
        Task CreateAsync(CreatePortfolioVm vm);
        Task UpdateAsync(UpdatePortfolioVm vm);
        Task DeleteAsync(int id);
    }
}

using AutoMapper;
using ExamTask.Business.Helpers;
using ExamTask.Business.Services.Interfaces;
using ExamTask.Business.ViewModels.EntityVms;
using ExamTask.Core.Entities;
using ExamTask.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace ExamTask.Business.Services.Implementation
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _repo;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public PortfolioService(IPortfolioRepository repo, IMapper mapper, IWebHostEnvironment env)
        {
            _repo = repo;
            _mapper = mapper;
            _env = env;
        }

        public async Task CreateAsync(CreatePortfolioVm vm)
        {
            Portfolio portfolio = _mapper.Map<Portfolio>(vm);

            portfolio.ImageUrl = await vm.ImageFile.UploadFile(_env.WebRootPath, "Upload");

            await _repo.CreateAsync(portfolio);
            await _repo.SaveChangesAsnyc();
        }

        public async Task DeleteAsync(int id)
        {
            Portfolio portfolio = await _repo.GetByIdAsync(id);

            _repo.Delete(portfolio);
            await _repo.SaveChangesAsnyc();
        }

        public async Task<IEnumerable<Portfolio>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public Task<Portfolio> GetByIdAsync(int id)
        {
            return _repo.GetByIdAsync(id);
        }

        public async Task UpdateAsync(UpdatePortfolioVm vm)
        {
            Portfolio portfolio = await _repo.GetByIdAsync(vm.Id);

            _mapper.Map<UpdatePortfolioVm, Portfolio>(vm, portfolio);

            if(vm.ImageFile is not null)
            {
                portfolio.ImageUrl = await vm.ImageFile.UploadFile(_env.WebRootPath, "Upload");
            }

            _repo.Update(portfolio);
            await _repo.SaveChangesAsnyc();
        }
    }
}

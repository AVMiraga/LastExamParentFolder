using AutoMapper;
using ExamTask.Business.ViewModels.EntityVms;
using ExamTask.Core.Entities;

namespace ExamTask.Business.MapperProfiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreatePortfolioVm, Portfolio>();

            CreateMap<UpdatePortfolioVm, Portfolio>();
            CreateMap<UpdatePortfolioVm, Portfolio>().ReverseMap();
        }
    }
}

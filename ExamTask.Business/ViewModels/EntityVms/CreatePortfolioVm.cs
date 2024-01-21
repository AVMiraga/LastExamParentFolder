using ExamTask.Business.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace ExamTask.Business.ViewModels.EntityVms
{
    public class CreatePortfolioVm
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public IFormFile ImageFile { get; set; }
    }

    public class CreatePortfolioVmValidator : AbstractValidator<CreatePortfolioVm>
    {
        public CreatePortfolioVmValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Category)
                .NotEmpty();

            RuleFor(x => x.ImageFile)
                .NotNull()
                .WithMessage("Image Required when Creating a portfolio!");

            When(x => x.ImageFile is not null, () =>
            {
                RuleFor(x => x.ImageFile)
                    .NotEmpty()
                    .Must(c => c.CheckFile(3))
                    .WithMessage("Only Image files allowed which has size of less than 3!");
            });
        }
    }
}

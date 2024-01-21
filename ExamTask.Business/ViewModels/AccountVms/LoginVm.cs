using FluentValidation;

namespace ExamTask.Business.ViewModels.AccountVms
{
    public class LoginVm
    {
        public string LoginID { get; set; }
        public string Password { get; set; }
    }
    public class LoginVmValidator : AbstractValidator<LoginVm>
    {
        public LoginVmValidator()
        {
            RuleFor(x => x.LoginID)
                .NotEmpty()
                .WithMessage("Please enter a Username or Email");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Please enter password!");
        }
    }
}

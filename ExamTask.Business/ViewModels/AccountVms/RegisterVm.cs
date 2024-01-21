using FluentValidation;

namespace ExamTask.Business.ViewModels.AccountVms
{
    public class RegisterVm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegisterVmValidator : AbstractValidator<RegisterVm>
    {
        public RegisterVmValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First Name Is Required!");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last Name Is Required!");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is Required and must be unique")
                .EmailAddress()
                .WithMessage("This is not a valid email address!");

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required and must be unique");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password Is required with this rule: At least 6 character");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm Password Is required")
                .Equal(c => c.Password)
                .WithMessage("Confirm Password doesn't match password!");
        }
    }
}

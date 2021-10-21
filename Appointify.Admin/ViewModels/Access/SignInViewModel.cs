using FluentValidation;

namespace Appointify.Admin.ViewModels.Access
{
    public class SignInViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class SignInValidator : AbstractValidator<SignInViewModel>
    {
        public SignInValidator()
        {
            RuleFor(x => x.Username)
                .NotNull().WithMessage("Please specify a username")
                .NotEmpty().WithMessage("Please specify a username")
                .Length(5, 30).WithMessage("Username has invalid value");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Please specify a password")
                .NotEmpty().WithMessage("Please specify a password");
        }
    }
}

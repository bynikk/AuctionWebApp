using BLL.Entities;
using FluentValidation;

namespace AuctionWebApp.Validators
{
    /// <summary>Provide rules for UserViewModel validation</summary>
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(c => c.Id > 0);
            RuleFor(c => c.Name).Length(4, 20).Matches(@"^[a-zA-Z-']*$").WithMessage("Only latters");
            RuleFor(c => c.UserName).NotEmpty().Length(2, 20).Matches(@"^[a-zA-Z-']*$").WithMessage("Only latters");
            RuleFor(c => c.Password).NotEmpty().Length(4, 20).Matches(@"^\w+$").WithMessage("Can contain only: letters, numbers and underscore");
        }
    }
}

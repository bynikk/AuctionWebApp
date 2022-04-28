using BLL.Entities;
using FluentValidation;

namespace AuctionWebApp.Validators
{
    /// <summary>Provide rules for UserViewModel validation</summary>
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        private static readonly int minId = 0;
        private static readonly int minNameCharCount = 4;
        private static readonly int maxNameCharCount = 20;
        private static readonly int minUserNameCharCount = 2;
        private static readonly int maxUserNameCharCount = 20;
        private static readonly int minPasswordCharCount = 4;
        private static readonly int maxPasswordCharCount = 20;

        /// <summary>The regx for name. Only latters.</summary>
        private static readonly string regxName = @"^[a-zA-Z-']*$";
        /// <summary>The regx for user name. Only latters.</summary>
        private static readonly string regxUserName = @"^[a-zA-Z-']*$";
        /// <summary>
        /// The regx for password. Only letters, number, underscore.</para>
        /// </summary>
        private static readonly string regxPassword = @"^\w+$";

        public UserViewModelValidator()
        {
            RuleFor(c => c.Id > minId);
            RuleFor(c => c.Name)
                .Length(minNameCharCount, maxNameCharCount)
                .Matches(regxName)
                .WithMessage("Only latters");

            RuleFor(c => c.UserName)
                .NotEmpty()
                .Length(minUserNameCharCount, maxUserNameCharCount)
                .Matches(regxUserName)
                .WithMessage("Only latters");

            RuleFor(c => c.Password)
                .NotEmpty()
                .Length(minPasswordCharCount, maxPasswordCharCount)
                .Matches(regxPassword)
                .WithMessage("Can contain only: letters, numbers and underscore");
        }
    }
}

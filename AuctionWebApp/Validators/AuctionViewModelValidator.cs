using AuctionWebApp.Models;
using FluentValidation;
using System;

namespace AuctionWebApp.Validators
{
    /// <summary>Provide rules for AuctionItemViewModel validation</summary>
    public class AuctionItemViewModelValidator : AbstractValidator<AuctionItemViewModel>
    {
        private static readonly int minId = 0;
        private static readonly int minNameCharCount = 2;
        private static readonly int maxNameCharCount = 20;
        private static readonly int minStartPrice = 10;
        /// <summary>The interval than auction start in minutes.</summary>
        private static readonly int intervalThanAuctionStart = 60;
        private static readonly int maxOwnerCharCount = 20;

        public AuctionItemViewModelValidator()
        {
            RuleFor(c => c.Id > minId).NotEmpty();
            RuleFor(c => c.Name).NotEmpty().Length(minNameCharCount, maxNameCharCount);
            RuleFor(c => c.StartPrice).NotEmpty().GreaterThan(minStartPrice);
            RuleFor(c => c.StartTime).NotEmpty();
            //.GreaterThanOrEqualTo(d => DateTime.Now.AddMinutes(intervalThanAuctionStart)).WithMessage("Start time error"); ;
            RuleFor(c => c.Owner).MaximumLength(maxOwnerCharCount);
            RuleFor(c => c.LastBitTime);
            RuleFor(c => c.CurrentPrice);
            RuleFor(c => c.OnLive);
            RuleFor(c => c.OnWait);
        }
    }
}
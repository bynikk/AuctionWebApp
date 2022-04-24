using AuctionWebApp.Models;
using FluentValidation;
using System;

namespace AuctionWebApp.Validators
{
    public class AuctionItemViewModelValidator : AbstractValidator<AuctionItemViewModel>
    {
        public AuctionItemViewModelValidator()
        {
            RuleFor(c => c.Id > 0).NotEmpty();
            RuleFor(c => c.Name).NotEmpty().MaximumLength(20);
            RuleFor(c => c.StartPrice).NotEmpty().GreaterThan(10);
            RuleFor(c => c.StartTime).NotEmpty().GreaterThanOrEqualTo(DateTime.UtcNow.AddMinutes(60));
            RuleFor(c => c.Owner).MaximumLength(20);
            RuleFor(c => c.LastBitTime);
            RuleFor(c => c.CurrentPrice);
            RuleFor(c => c.OnLive);
            RuleFor(c => c.OnWait);
        }
    }
}
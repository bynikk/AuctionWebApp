using AuctionWebApp.Models;
using FluentValidation;
using System;

namespace AuctionWebApp.Validators
{
    /// <summary>Provide rules for AuctionItemViewModel validation</summary>
    public class AuctionItemViewModelValidator : AbstractValidator<AuctionItemViewModel>
    {
        public AuctionItemViewModelValidator()
        {
            RuleFor(c => c.Id > 0).NotEmpty();
            RuleFor(c => c.Name).NotEmpty().Length(2, 20);
            RuleFor(c => c.StartPrice).NotEmpty().GreaterThan(10);
            RuleFor(c => c.StartTime).NotEmpty();
                //.GreaterThanOrEqualTo(d => DateTime.Now.AddMinutes(60)).WithMessage("Start time error"); ;
            RuleFor(c => c.Owner).MaximumLength(20);
            RuleFor(c => c.LastBitTime);
            RuleFor(c => c.CurrentPrice);
            RuleFor(c => c.OnLive);
            RuleFor(c => c.OnWait);
        }
    }
}
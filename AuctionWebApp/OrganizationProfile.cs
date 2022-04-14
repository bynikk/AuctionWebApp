using AuctionWebApp.Models;
using AutoMapper;
using BLL.Entities;

namespace CatsCRUDApp
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<AuctionItemViewModel, AuctionItem>();
            CreateMap<AuctionItem, AuctionItemViewModel>();
        }
    }
}

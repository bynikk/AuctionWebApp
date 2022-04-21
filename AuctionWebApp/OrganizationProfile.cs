using AuctionWebApp.Models;
using AutoMapper;
using BLL;
using BLL.Entities;
using DAL.Live;

namespace CatsCRUDApp
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<AuctionItemViewModel, AuctionItem>();
            CreateMap<AuctionItem, AuctionItemViewModel>();

            CreateMap<User, UserViewModel>().ForMember(src => src.Password, act => act.Ignore());
            CreateMap<UserViewModel, User>().ForMember("Password", opt => opt.MapFrom(src => StringExtensions.GetHash(src.Password)));
        }
    }
}

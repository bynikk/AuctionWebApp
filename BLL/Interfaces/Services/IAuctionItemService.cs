﻿using BLL.Entities;

namespace BLL.Interfaces.Services
{
    public interface IAuctionItemService
    {
        Task Create(AuctionItem auctionItem);
        Task Update(AuctionItem auctionItem);
        Task Delete(int id);
        Task<List<AuctionItem>> Get();
    }
}
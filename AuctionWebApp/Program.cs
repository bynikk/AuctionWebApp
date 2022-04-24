using AuctionWebApp.BackgroundServices;
using AuctionWebApp.Hubs;
using BLL.Entities;
using BLL.Interfaces;
using BLL.Interfaces.Cache;
using BLL.Services;
using CatsCRUDApp;
using DAL.CacheAllocation;
using DAL.CacheAllocation.Cosumers;
using DAL.CacheAllocation.Producers;
using DAL.Config;
using DAL.Finders;
using DAL.Findres;
using DAL.MongoDb;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Cache distribution
// Fix UI bug with inputs (owner, id, bit status)
// Implement validation

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHostedService<EndItemsListener>();
builder.Services.AddHostedService<LiveItemsListener>();

builder.Services.AddSignalR();
// DI
builder.Services.Configure<MongoConfig>(builder.Configuration.GetSection(nameof(MongoConfig)));
builder.Services.AddSingleton<MongoConfig>(sp => sp.GetRequiredService<IOptions<MongoConfig>>().Value);

builder.Services.Configure<RedisConfig>(builder.Configuration.GetSection(nameof(RedisConfig)));
builder.Services.AddSingleton<RedisConfig>(sp => sp.GetRequiredService<IOptions<RedisConfig>>().Value);

builder.Services.AddScoped<IAuctionItemService, AuctionItemService>();
builder.Services.AddScoped<IRepository<AuctionItem>, AuctionItemRepository>();
builder.Services.AddScoped<IAuctionItemFinder, AuctionItemFinder>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IUserFinder, UserFinder>();

builder.Services.AddAutoMapper(typeof(OrganizationProfile));

//cache 
builder.Services.AddSingleton<IChannelProducer<AuctionStreamModel>, ChannelProducer>();
builder.Services.AddSingleton<IChannelConsumer<AuctionStreamModel>, ChannelConsumer>();
builder.Services.AddSingleton<IRedisProducer<AuctionItem>, RedisProducer>();
builder.Services.AddSingleton<IRedisConsumer, RedisConsumer>();

builder.Services.AddSingleton<ICache<AuctionItem>, Cache>();
builder.Services.AddSingleton<IChannelContext<AuctionStreamModel>, ChannelContext>();

builder.Services.AddScoped<IDbContext, DbContext>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                // CookieAuthenticationOptions
                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/LoginIn");
            });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auction}/{action=Index}/{id?}");


app.MapHub<AuctionHub>("/auction");

var mongoConfig = app.Services.GetService(typeof(MongoConfig)) as MongoConfig;

Console.WriteLine("mongo - " + mongoConfig.Ip + ":" + mongoConfig.Port);

var redisConfig = app.Services.GetService(typeof(RedisConfig)) as RedisConfig;

Console.WriteLine("redis - " + redisConfig.Ip + ":" + redisConfig.Port);

app.Run();

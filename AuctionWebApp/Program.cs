using AuctionWebApp.Hubs;
using AuctionWebApp.Validators;
using AuctionWebApp.Models;
using BLL.Entities;
using BLL.Services;
using CatsCRUDApp;
using DAL.Config;
using DAL.Finders;
using DAL.Findres;
using DAL.MongoDb;
using DAL.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using BLL.Interfaces.Finders;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using BLL.Interfaces.Database;
using MongoDB.Bson.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<MongoConfig>(builder.Configuration.GetSection(nameof(MongoConfig)));
builder.Services.AddSingleton<MongoConfig>(sp => sp.GetRequiredService<IOptions<MongoConfig>>().Value);

builder.Services.AddScoped<IDbContext, DbContext>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAutoMapper(typeof(OrganizationProfile));

builder.Services.AddMvc().AddFluentValidation(fv => fv.ImplicitlyValidateRootCollectionElements = true);

builder.Services.AddTransient<IValidator<AuctionItemViewModel>, AuctionItemViewModelValidator>();
builder.Services.AddTransient<IValidator<UserViewModel>, UserViewModelValidator>();

builder.Services.AddScoped<IAuctionItemService, AuctionItemService>();
builder.Services.AddScoped<IRepository<AuctionItem>, AuctionItemRepository>();
builder.Services.AddScoped<IAuctionItemFinder, AuctionItemFinder>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IUserFinder, UserFinder>();

builder.Services.AddSignalR();


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

// SignalR routs.
app.MapHub<AuctionHub>("/auction");
app.MapHub<MainHub>("/main");

var mongoConfig = app.Services.GetService(typeof(MongoConfig)) as MongoConfig;
Console.WriteLine("mongo - " + mongoConfig.Ip + ":" + mongoConfig.Port);

app.Run();

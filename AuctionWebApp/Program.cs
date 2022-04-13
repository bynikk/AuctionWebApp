using BLL.Interfaces;
using DAL.Config;
using DAL.MongoDb;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DI
builder.Services.Configure<MongoConfig>(builder.Configuration.GetSection(nameof(MongoConfig)));
builder.Services.AddSingleton<MongoConfig>(sp => sp.GetRequiredService<IOptions<MongoConfig>>().Value);

builder.Services.AddScoped<IDbContext, DbContext>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


var mongoConfig = app.Services.GetService(typeof(MongoConfig)) as MongoConfig;

Console.WriteLine("mongo - " + mongoConfig.Ip + ":" + mongoConfig.Port);

app.Run();

using InfrontAssesment.Core.Interfaces;
using InfrontAssesment.Infrastructure.Data;
using InfrontAssesment.Infrastructure.Repositories;
using InfrontAssesment.WebUI.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using InfrontAssesment.WebUI;
using InfrontAssesment.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddTransient<IStockRepository, StockRepository>();
builder.Services.AddTransient<WeatherForecastService>();
builder.Services.AddTransient<IPriceDataRepository, PriceDataRepository>();
builder.Services.AddTransient<IStockOperationService, StockOperationService>();


builder.Services.AddDbContext<StockContext>(x =>
               x.UseSqlite("Data source = stockdata.db"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

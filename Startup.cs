using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShopifyBilling.Data;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopifyBilling.Models;
using ShopifyBilling;

 public class Startup{
  public IConfiguration configRoot {
        get;
    }
    public Startup(IConfiguration configuration) {
        configRoot = configuration;
    }
 public void ConfigureServices(IServiceCollection services) {
        services.AddControllersWithViews();
 // Add cookie authentication
 var authScheme = CookieAuthenticationDefaults.AuthenticationScheme;
 services
 .AddAuthentication(authScheme)
 .AddCookie(ConfigureCookieAuthentication);
 // Add Entity Framework and the DataContext
 services.AddDbContext<DataContext>(options =>
options.UseSqlServer(GetSqlConnectionString()));
 // Add ISecrets to Dependency Injection
 services.AddSingleton<ISecrets, Secrets>();
    }

//configure cookie authentication
private void ConfigureCookieAuthentication(CookieAuthenticationOptions options)
 {
 options.Cookie.HttpOnly = true;
 options.SlidingExpiration = true;
 options.ExpireTimeSpan = TimeSpan.FromDays(1);
 options.LogoutPath = "/Auth/Logout";
 options.LoginPath = "/Auth/Login";
 options.AccessDeniedPath = "/Auth/Login";
 }

//configure connection string
private string GetSqlConnectionString()
 {
 var partialConnectionString =
configRoot.GetConnectionString("DefaultConnection");
 var password = configRoot.GetValue<string>("sqlPassword");
 var connStr = new SqlConnectionStringBuilder(partialConnectionString)
 {
 Password = password,
 Authentication = SqlAuthenticationMethod.SqlPassword
 };
 return connStr.ToString();
 }



 public void Configure(IWebHostEnvironment env){
  var builder = WebApplication.CreateBuilder();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>( options =>
            options.UseNpgsql("Server=localhost;Database=postgres;User Id=postgres;Password=Alpha2;"));
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
app.UseStatusCodePages();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
 }
 }



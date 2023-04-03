using dotNFT.Data;
using dotNFT.Services;
using dotNFT.Services.PasswordServices;
using dotNFT.Services.Repositories.Artists;
using dotNFT.Services.Repositories.Collections;
using dotNFT.Services.Repositories.NFTs;
using dotNFT.Services.Repositories.Transactions;
using dotNFT.Services.Repositories.Users;
using dotNFT.Services.Repositories.Wallets;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Add DbContext
builder.Services
    .AddDbContext<AppDbContext>()
    /*.AddSqlServer<AppDbContext>(builder.Configuration.GetConnectionString("Data Source=DESKTOP-NFKHDAA\\SQLEXPRESS01;Initial Catalog=dotNFT;Integrated Security=True;TrustServerCertificate=True"))*/
    .AddScoped<ITransactionRepository, TransactionRepository>()
    .AddScoped<IArtistRepository, ArtistRepository>()
    .AddScoped<ICollectionRepository, CollectionRepository>()
    .AddScoped<INFTRepository, NFTRepository>()
    .AddScoped<IWalletRepository, WalletRepository>()
    .AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddDistributedMemoryCache();
 
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});

// Add repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add password hashing service
builder.Services.AddScoped<ICryptographyService, CryptographyService>();

// Add authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "AuthNFTsCookie";
        options.LoginPath = "/User/Login";
        options.LogoutPath = "/User/Logout";
        options.AccessDeniedPath = "/User/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = context =>
            {
                context.Response.Redirect("/User/Login");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddMvcCore(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
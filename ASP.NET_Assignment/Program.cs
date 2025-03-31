using ASP.NET_Assignment.Pages.Shared.Partials.Sections;
using Business.Models;
using Data.Contexts;
using Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("LocalDB")));

builder.Services.AddTransient<_SignOutModel>();

builder.Services.AddIdentity<AppUser, IdentityRole>(x =>
{
    x.Password.RequireDigit = false;
    x.Password.RequiredLength = 6;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(x =>
{
    x.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    x.SlidingExpiration = true;
});


builder.Services.AddScoped<UserService>();

builder.Services.AddRazorPages();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();
app.Run();

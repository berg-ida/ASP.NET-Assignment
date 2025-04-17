using ASP.NET_Assignment.Pages.Shared.Partials.Sections;
using Data.Contexts;
using Data.Entities;
using Data.Repositories;
using Business.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("LocalDB")));
builder.Services.AddTransient<_SignOutModel>();
builder.Services.AddTransient<_EditAndDeleteModel>();
builder.Services.AddIdentity<UserEntity, IdentityRole>(x =>
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
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddRazorPages();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();
app.Run();



using Microsoft.EntityFrameworkCore;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyApp.DataAccessLayer.Infrastructure.Repository;
using MywebApplication.Data;
using MyApp.Models;
using Microsoft.AspNetCore.Identity;
using MyApp.common;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 1. DbContext Register karein (Ye line missing thi)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("constr")));

// 2. Identity Register karein (Sirf EK baar aayegi)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();

// 3. Razor Pages
builder.Services.AddRazorPages();

// 4. UnitOfWork Register karein
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IEmailSender,EmailSender>();

builder.Services.ConfigureApplicationCookie(x => {

    x.AccessDeniedPath = $"/Identity/Account/AccessDenied";
    x.LoginPath = $"/Identity/Account/Login";
    x.LogoutPath = $"/Identity/Account/LogOut";

}); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// 5. Authentication pehle, Authorization baad mein
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
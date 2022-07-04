using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using E_medicine.Data;
using E_medicine.Models;
using Microsoft.AspNetCore.Identity;
using E_medicine.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddRazorPages();
builder.Services.AddIdentity<E_medicineUser, IdentityRole>().AddEntityFrameworkStores<E_medicineContext>().AddDefaultTokenProviders().AddDefaultUI();




builder.Services.AddDbContext<E_medicineContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("E_medicineContext") ?? throw new InvalidOperationException("Connection string 'E_medicineContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews()
.AddRazorPagesOptions(options =>
 {
     options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
     options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
 });

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 6;
    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;
    // User settings
    options.User.RequireUniqueEmail = true;
});
//Setting the Account Login page
builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    seeddata.Initialize(services);
}
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
    pattern: "{controller=Companies}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

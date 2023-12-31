using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Stripe;
using TheBookshelf.DataAccess.DbInitializer;
using TheBookshelf.DataAccess.Repository;
using TheBookshelf.DataAccess.Repository.IRepository;
using TheBookshelf.Utility;
using TheBookshelfWeb.DataAccess.Data;

var builder = WebApplication.CreateBuilder(args);

/////////////////////////////////
// Add services to the container.
/////////////////////////////////
builder.Services.AddControllersWithViews();
// Add DB context to container and using SqlServer
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Configure stripe settings
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
// Configure cookies must be added after identity is added
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});
builder.Services.AddAuthentication().AddGoogle(option =>
{
    option.ClientId = "1098554166825-kvrqf7b365f0u5j73cmr1enk3uib9pm7.apps.googleusercontent.com";
    option.ClientSecret = "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";
});
builder.Services.AddAuthentication().AddMicrosoftAccount(option =>
{
    option.ClientId = "167c0d70-69db-4ced-8cbd-8ffea229c7ca";
    option.ClientSecret = "fec67173-dc2f-4886-a7de-8929b39ce2a1";
});

//Configure sessions
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//Seeding DB
builder.Services.AddScoped<IDbInitializer, DbInitializer>();

// Added UnitOfWork Repository to Service
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();


//////////////////////////////////////
// Configure the HTTP request pipeline.
//////////////////////////////////////
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
SeedDatabase();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();


void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}
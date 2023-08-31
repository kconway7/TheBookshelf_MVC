using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheBookshelf.DataAccess.Repository;
using TheBookshelf.DataAccess.Repository.IRepository;
using TheBookshelfWeb.DataAccess.Data;

var builder = WebApplication.CreateBuilder(args);

/////////////////////////////////
// Add services to the container.
/////////////////////////////////
builder.Services.AddControllersWithViews();
// Add DB context to container and using SqlServer
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
// Added UnitOfWork Repository to Service
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();

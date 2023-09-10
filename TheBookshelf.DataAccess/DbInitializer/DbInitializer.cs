using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheBookshelf.Models;
using TheBookshelf.Utility;
using TheBookshelfWeb.DataAccess.Data;

namespace TheBookshelf.DataAccess.DbInitializer
{
	public class DbInitializer : IDbInitializer
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ApplicationDbContext _db;

		public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_db = db;
		}

		public void Initialize()
		{
			// migrations if they are not applied
			try
			{
				if (_db.Database.GetPendingMigrations().Count() > 0)
				{
					_db.Database.Migrate();
				}
			}
			catch (Exception ex)
			{

			}

			//create roles if they are not created
			if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
			{
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();

				//if roles are not created, then we will create admin user as well
				_userManager.CreateAsync(new ApplicationUser
				{
					UserName = "admin@bookshelf.com",
					Email = "admin@bookshelf.com",
					Name = "Admin",
					PhoneNumber = "1234567890",
					StreetAddress = "Test 123 Ave",
					State = "OH",
					PostalCode = "12345",
					City = "Cleveland"
				}, "Pass1234$").GetAwaiter().GetResult();

				ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@bookshelf.com");
				_userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
			}

			return;
		}
	}


}

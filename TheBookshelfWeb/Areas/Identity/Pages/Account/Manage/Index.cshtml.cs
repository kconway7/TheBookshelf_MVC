// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TheBookshelf.DataAccess.Repository.IRepository;
using TheBookshelf.Models;

namespace TheBookshelfWeb.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(
            IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /// 
        [BindProperties]
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            public ApplicationUser ApplicationUser { get; set; }
            public string userId { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;
            ApplicationUser appUser = (ApplicationUser)await _userManager.FindByIdAsync(user.Id);

            Input = new InputModel
            {
                userId = user.Id,
                ApplicationUser = appUser,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        /* public async Task<IActionResult> OnPostAsync()
         {
             var user = await _userManager.GetUserAsync(User);
             if (user == null)
             {
                 return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
             }

             if (!ModelState.IsValid)
             {
                 await LoadAsync(user);
                 return Page();
             }

             var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
             if (Input.PhoneNumber != phoneNumber)
             {
                 var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                 if (!setPhoneResult.Succeeded)
                 {
                     StatusMessage = "Unexpected error when trying to set phone number.";
                     return RedirectToPage();
                 }
             }

             await _signInManager.RefreshSignInAsync(user);

             return RedirectToPage();
         }*/

        async public Task<IActionResult> OnPost()
        {

            ApplicationUser appUser = (ApplicationUser)await _userManager.FindByIdAsync(Input.userId);
            if (appUser != null)
            {
                appUser.Name = Input.ApplicationUser.Name;
                appUser.PhoneNumber = Input.ApplicationUser.PhoneNumber;
                appUser.StreetAddress = Input.ApplicationUser.StreetAddress;
                appUser.City = Input.ApplicationUser.City;
                appUser.PostalCode = Input.ApplicationUser.PostalCode;
                appUser.State = Input.ApplicationUser.State;
                StatusMessage = "Your profile has been updated";
                await _userManager.UpdateAsync(appUser);
            }
            else
            {
                StatusMessage = "Error updating profile";
            }

            return RedirectToPage();

        }




    }
}

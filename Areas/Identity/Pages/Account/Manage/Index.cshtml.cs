using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GoThere.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoThere.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<GoThereUser> _userManager;
        private readonly SignInManager<GoThereUser> _signInManager;

        public IndexModel(
            UserManager<GoThereUser> userManager,
            SignInManager<GoThereUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
            [StringLength(100, MinimumLength = 2)]
            public string FirstName { get; set; }

            [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
            [StringLength(100, MinimumLength = 2)]
            public string LastName { get; set; }

            public string FullName = "{firstName} {lastName}";

            [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
            [StringLength(100, MinimumLength = 2)]
            public string Occupation { get; set; }

            [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
            [StringLength(100, MinimumLength = 2)]
            public string Industry { get; set; }

            [Display(Name = "Postal Code")]
            [StringLength(100, MinimumLength = 2)]
            public string PostalCode { get; set; }
        }

        private async Task LoadAsync(GoThereUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
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

        public async Task<IActionResult> OnPostAsync()
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
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            if (!string.IsNullOrEmpty(Input.FirstName) && Input.FirstName != user.FirstName)
            {
                user.FirstName = Input.FirstName;
            }

            if (!string.IsNullOrEmpty(Input.LastName) && Input.LastName != user.LastName)
            {
                user.LastName = Input.LastName;
            }

            if (!string.IsNullOrEmpty(Input.Occupation) && Input.Occupation != user.Occupation)
            {
                user.Occupation = Input.Occupation;
            }

            if (!string.IsNullOrEmpty(Input.Industry) && Input.Industry != user.Industry)
            {
                user.Industry = Input.Industry;
            }

            if (!string.IsNullOrEmpty(Input.PostalCode) && Input.PostalCode != user.PostalCode)
            {
                user.FirstName = Input.PostalCode;
            }

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}

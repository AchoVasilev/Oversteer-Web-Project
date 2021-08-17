namespace Oversteer.Web.Areas.Identity.Pages.Account.Manage
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using Oversteer.Data.Models.Users;
    using Oversteer.Services.Companies;
    using Oversteer.Services.Companies.Account;
    using Oversteer.Services.Users;

    using static Oversteer.Data.Common.Constants.DataConstants.Users;

    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ICompaniesService companiesService;
        private readonly ICompanyAccountService companyAccountService;
        private readonly IUserService userService;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ICompaniesService companiesService,
            ICompanyAccountService companyAccountService,
            IUserService userService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.companiesService = companiesService;
            this.companyAccountService = companyAccountService;
            this.userService = userService;
        }

        public string Username { get; set; }

        public bool IsCompany { get; set; } = false;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [MaxLength(NameMaxLength)]
            [Display(Name = "First name")]
            public string FirstName { get; set; }

            [MaxLength(NameMaxLength)]
            public string Surname { get; set; }

            [Required]
            [MaxLength(NameMaxLength)]
            [Display(Name = "Last name")]
            public string LastName { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [MaxLength(NameMaxLength)]
            [Display(Name = "Company name")]
            public string CompanyName { get; set; }

            [Required]
            public string Description { get; set; }

            [EmailAddress]
            public string Email { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await this.userManager.GetUserNameAsync(user);
            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);
            var firstName = await this.userService.GetUserFirstNameAsync(user.Id);
            var surname = await this.userService.GetUserMiddleNameAsync(user.Id);
            var lastName = await this.userService.GetUserLastNameAsync(user.Id);

            var companyId = this.companiesService.GetCurrentCompanyId(user.Id);

            if (companyId != 0)
            {
                var companyDetails = await this.companiesService.DetailsAsync(companyId);

                IsCompany = true;
                Username = companyDetails.Name;

                Input = new InputModel
                {
                    Email = userName,
                    Description = companyDetails.Description,
                    PhoneNumber = companyDetails.PhoneNumber
                };
            }
            else
            {
                Username = userName;

                Input = new InputModel
                {
                    FirstName = firstName,
                    Surname = surname,
                    LastName = lastName,
                    PhoneNumber = phoneNumber
                };
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{this.userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{this.userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);

            var firstName = await this.userService.GetUserFirstNameAsync(user.Id);
            var surname = await this.userService.GetUserMiddleNameAsync(user.Id);
            var lastName = await this.userService.GetUserLastNameAsync(user.Id);

            var companyId = this.companiesService.GetCurrentCompanyId(user.Id);

            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await this.userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);

                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if (companyId != 0)
            {
                var companyDetails = await this.companiesService.DetailsAsync(companyId);

                if (Input.CompanyName != companyDetails.Name)
                {
                    var setCompanyName = await this.companyAccountService.SetCompanyName(user.Id, Input.CompanyName);

                    if (!setCompanyName)
                    {
                        StatusMessage = "Unexpected error when trying to set first name.";
                        return RedirectToPage();
                    }
                }

                if (Input.Description != companyDetails.Description)
                {
                    var setCompanyDescription = await this.companyAccountService.SetCompanyDescription(user.Id, Input.Description);

                    if (!setCompanyDescription)
                    {
                        StatusMessage = "Unexpected error when trying to set first name.";
                        return RedirectToPage();
                    }
                }

                if (Input.PhoneNumber != companyDetails.PhoneNumber)
                {
                    var setCompanyPhoneNumber = await this.companyAccountService.SetPhoneNumberAsync(companyId, Input.PhoneNumber);

                    if (!setCompanyPhoneNumber)
                    {
                        StatusMessage = "Unexpected error when trying to set first name.";
                        return RedirectToPage();
                    }
                }
            }
            else
            {
                if (Input.FirstName != firstName)
                {
                    var setFirstNameResult = await this.userService.SetUserFirstNameAsync(user.Id, Input.FirstName);

                    if (!setFirstNameResult)
                    {
                        StatusMessage = "Unexpected error when trying to set first name.";
                        return RedirectToPage();
                    }
                }

                if (Input.Surname != surname)
                {
                    var setSurnameResult = await this.userService.SetUserMiddleNameAsync(user.Id, Input.Surname);

                    if (!setSurnameResult)
                    {
                        StatusMessage = "Unexpected error when trying to set surname.";
                        return RedirectToPage();
                    }
                }

                if (Input.LastName != lastName)
                {
                    var setLastNameResult = await this.userService.SetUserLastNameAsync(user.Id, Input.LastName);

                    if (!setLastNameResult)
                    {
                        StatusMessage = "Unexpected error when trying to set last name.";
                        return RedirectToPage();
                    }
                }
            }

            await signInManager.RefreshSignInAsync(user);

            StatusMessage = "Your profile has been updated";

            return RedirectToPage();
        }
    }
}

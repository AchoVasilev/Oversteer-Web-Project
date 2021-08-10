namespace Oversteer.Web.ViewModels.Companies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Oversteer.Data.Common.Constants.DataConstants.Users;
    using static Oversteer.Data.Common.Constants.DataConstants.Companies;
    using static Oversteer.Data.Common.Constants.ErrorMessages.UserErrors;
    using static Oversteer.Data.Common.Constants.ModelsDisplayNames;

    public class CreateCompanyFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = InvalidNameLength)]
        [Display(Name = CompanyDisplayName)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength, ErrorMessage = InvalidPhoneNumberLength)]
        [Display(Name = PhoneNumberDisplay)]
        [RegularExpression(PhoneNumberRegularExpression, ErrorMessage = InvalidPhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = DisplayCompanyServices)]
        public IEnumerable<CreateCompanyServiceFormModel> CompanyServices { get; set; }
    }
}

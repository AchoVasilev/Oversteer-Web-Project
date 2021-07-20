namespace Oversteer.Web.Models.Companies
{
    using System.ComponentModel.DataAnnotations;

    using static Oversteer.Models.Constants.DataConstants.Users;
    using static Oversteer.Web.Data.Constants.ModelsDisplayNames;
    using static Oversteer.Web.Data.Constants.ErrorMessages.CompanyErrors;

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
    }
}

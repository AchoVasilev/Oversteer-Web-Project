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
        [Display(Name = CompanyDisplayName)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = InvalidNameLength)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength, ErrorMessage = InvalidPhoneNumberLength)]
        [Display(Name = PhoneNumberDisplay)]
        [RegularExpression(PhoneNumberRegularExpression, ErrorMessage = InvalidPhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }

        public IEnumerable<CreateCompanyServiceFormModel> CompanyServices { get; set; }
    }
}

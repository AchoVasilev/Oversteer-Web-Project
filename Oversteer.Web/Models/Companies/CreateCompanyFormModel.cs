namespace Oversteer.Web.Models.Companies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Oversteer.Models.Constants.DataConstants.Users;
    using static Oversteer.Models.Constants.DataConstants.Companies;
    using static Oversteer.Web.Data.Constants.ErrorMessages.CompanyErrors;
    using static Oversteer.Web.Data.Constants.ModelsDisplayNames;

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

        [Display(Name = "Add your offered services")]
        public IEnumerable<CreateCompanyServiceFormModel> CompanyServices { get; set; }
    }
}

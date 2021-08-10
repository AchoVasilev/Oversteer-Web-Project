namespace Oversteer.Web.ViewModels.Locations
{
    using System.ComponentModel.DataAnnotations;

    using static Oversteer.Data.Common.Constants.ModelsDisplayNames;
    using static Oversteer.Data.Common.Constants.DataConstants.Cities;

    public class CreateLocationFormModel
    {
        [Display(Name = CountryDisplayName)]
        public int CountryId { get; set; }

        [Required]
        [StringLength(CityNameMaxLength, MinimumLength = CityNameMinLength)]
        public string CityName { get; set; }

        [Required]
        [StringLength(AddressNameMaxLength, MinimumLength = AddressNameMinLength)]
        public string Address { get; set; }

        [Range(ZipCodeMinLength, ZipCodeMaxLength)]
        public int ZipCode { get; set; }
    }
}

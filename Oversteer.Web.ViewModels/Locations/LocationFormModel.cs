namespace Oversteer.Web.ViewModels.Locations
{
    using System.ComponentModel.DataAnnotations;

    using static Oversteer.Data.Common.Constants.DataConstants.Cities;

    public class LocationFormModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        [Required]
        [StringLength(CityNameMaxLength, MinimumLength = CityNameMinLength)]
        public string CityName { get; init; }

        [Required]
        [StringLength(AddressNameMaxLength, MinimumLength = AddressNameMinLength)]
        public string Address { get; init; }

        [Range(ZipCodeMinLength, ZipCodeMaxLength)]
        public int ZipCode { get; init; }
    }
}

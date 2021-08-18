namespace Oversteer.Web.ViewModels.Locations
{
    using System.ComponentModel.DataAnnotations;

    using Oversteer.Data.Models.Users;

    using static Oversteer.Data.Common.Constants.DataConstants.Cities;

    public class LocationFormModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        [Display(Name = (nameof(Country)))]
        public int CountryId { get; init; }

        [Display(Name = (nameof(Country)))]
        public string CountryName { get; init; }

        public int CityId { get; init; }

        [Required]
        [StringLength(CityNameMaxLength, MinimumLength = CityNameMinLength)]
        [Display(Name = (nameof(City)))]
        public string CityName { get; init; }

        public int AddressId { get; init; }

        [Required]
        [StringLength(AddressNameMaxLength, MinimumLength = AddressNameMinLength)]
        [Display(Name = (nameof(Address)))]
        public string AddressName { get; init; }

        public int CompanyId { get; init; }

        public string CompanyName { get; init; }
    }
}

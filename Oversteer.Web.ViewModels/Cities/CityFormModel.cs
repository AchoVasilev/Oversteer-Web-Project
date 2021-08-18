namespace Oversteer.Web.ViewModels.Cities
{
    using System.ComponentModel.DataAnnotations;

    using static Oversteer.Data.Common.Constants.DataConstants.Cities;

    public class CityFormModel
    {
        public int Id { get; init; }

        [Required]
        [StringLength(CityNameMaxLength, MinimumLength = CityNameMinLength)]
        public string Name { get; init; }

        public int CountryId { get; init; }

        public string CountryName { get; init; }
    }
}

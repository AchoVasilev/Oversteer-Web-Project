namespace Oversteer.Web.ViewModels.Cities
{
    using System.ComponentModel.DataAnnotations;

    using static Oversteer.Data.Common.Constants.DataConstants.Cities;

    public class CityFormModel
    {
        [Required]
        [StringLength(CityNameMaxLength, MinimumLength = CityNameMinLength)]
        public string Name { get; init; }

        public ZipCodeFormModel ZipCode { get; init; }
    }
}

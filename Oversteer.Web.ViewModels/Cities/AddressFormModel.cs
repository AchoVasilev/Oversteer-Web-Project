namespace Oversteer.Web.ViewModels.Cities
{
    using System.ComponentModel.DataAnnotations;

    using static Oversteer.Data.Common.Constants.DataConstants.Cities;

    public class AddressFormModel
    {
        [Required]
        [StringLength(AddressNameMaxLength, MinimumLength = AddressNameMinLength)]
        public string Name { get; init; }

        public int CityId { get; init; }
    }
}

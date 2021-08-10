namespace Oversteer.Web.ViewModels.Cars.CarItems
{
    using System.ComponentModel.DataAnnotations;

    using static Oversteer.Data.Common.Constants.DataConstants.Cars;
    public class CarFeatureFormModel
    {
        [Required]
        [StringLength(NameMaxValue, MinimumLength = NameMinValue)]
        public string Name { get; init; }
    }
}

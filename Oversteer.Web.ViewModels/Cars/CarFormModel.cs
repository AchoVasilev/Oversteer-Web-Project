namespace Oversteer.Web.ViewModels.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using Oversteer.Web.Infrastructure.CustomAttributes;
    using Oversteer.Web.ViewModels.Cars.CarItems;
    using Oversteer.Web.ViewModels.Locations;

    using static Oversteer.Data.Common.Constants.DataConstants.Cars;
    using static Oversteer.Data.Common.Constants.ErrorMessages;
    using static Oversteer.Data.Common.Constants.ModelsDisplayNames;

    public class CarFormModel
    {
        [Required]
        [Display(Name = CarDailyPrice)]
        public decimal? DailyPrice { get; init; }

        [Required]
        [Display(Name = CarSeatsCount)]
        [Range(CarSeatMinCount, CarSeatMaxCount)]
        public int? SeatsCount { get; init; }

        [Display(Name = CarBrand)]
        public int BrandId { get; init; }

        [Required]
        [CurrentYearMaxValue(CarYearMinValue)]
        public int? Year { get; init; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = DescriptionMinLength, ErrorMessage = DescriptionLengthError)]
        public string Description { get; init; }

        [Display(Name = CarImageUrl)]
        [Url(ErrorMessage = IvalidURL)]
        public string ImageUrl { get; init; }

        [Display(Name = CarColor)]
        public int ColorId { get; init; }

        [Display(Name = CarFuel)]
        public int FuelId { get; init; }

        [Display(Name = CarTransmission)]
        public int TransmissionId { get; init; }

        [Display(Name = CarType)]
        public int CarTypeId { get; init; }

        [Display(Name = CarModel)]
        public int ModelId { get; init; }

        [Display(Name = CarLocation)]
        public int LocationId { get; init; }

        public IEnumerable<LocationFormModel> Locations { get; set; }

        public IEnumerable<CarBrandFormModel> Brands { get; set; }

        public IEnumerable<ColorFormModel> Colors { get; set; }

        public IEnumerable<FuelTypeFormModel> FuelTypes { get; set; }

        public IEnumerable<TransmissionTypeFormModel> Transmissions { get; set; }

        public IEnumerable<CarTypeFormModel> CarTypes { get; set; }

        public IEnumerable<CarModelFormModel> CarModels { get; set; }

        [Display(Name = CarImageUrl)]
        public IEnumerable<IFormFile> Images { get; set; }

        [Display(Name = Features)]
        public IEnumerable<CarFeatureFormModel> CarFeatures { get; set; }
    }
}

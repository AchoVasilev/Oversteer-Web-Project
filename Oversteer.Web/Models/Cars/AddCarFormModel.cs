namespace Oversteer.Web.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Oversteer.Models.Constants.DataConstants.Cars;
    using static Oversteer.Web.Data.Constants.ModelsDisplayNames;
    using static Oversteer.Web.Data.Constants.ErrorMessages;
    using Oversteer.Web.Infrastructure.CustomAttributes;

    public class AddCarFormModel
    {
        [Required]
        [Display(Name = CarDailyPrice)]
        public decimal? DailyPrice { get; set; }

        [Required]
        [Display(Name = CarSeatsCount)]
        [Range(CarSeatMinCount, CarSeatMaxCount)]
        public int? SeatsCount { get; set; }

        [Display(Name = CarBrand)]
        public int BrandId { get; init; }

        [Required]
        [CurrentYearMaxValue(CarYearMinValue)]
        [MaxLength(CarYearLength)]
        [MinLength(CarYearLength)]
        public int? Year { get; init; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = DescriptionMinLength, ErrorMessage = DescriptionLengthError)]
        public string Description { get; init; }

        //[Required]
        //[Display(Name = CarImageUrl)]
        //public int ImageId { get; set; }

        [Display(Name = CarImageUrl)]
        [Required]
        [Url(ErrorMessage = IvalidURL)]
        public string ImageUrl { get; set; }

        [Display(Name = CarColor)]
        public int ColorId { get; set; }

        [Display(Name = CarFuel)]
        public int FuelId { get; set; }

        [Display(Name = CarTransmission)]
        public int TransmissionId { get; set; }

        [Display(Name = CarType)]
        public int CarTypeId { get; set; }

        [Display(Name = CarModel)]
        public int ModelId { get; set; }

        public IEnumerable<CarBrandFormModel> Brands { get; set; }

        public IEnumerable<ColorFormModel> Colors { get; set; }

        public IEnumerable<FuelTypeFormModel> FuelTypes { get; set; }

        public IEnumerable<TransmissionTypeFormModel> Transmissions { get; set; }

        public IEnumerable<CarTypeFormModel> CarTypes { get; set; }

        public IEnumerable<CarModelFormModel> CarModels { get; set; }

        // public IEnumerable<CarImageFormModel> CarImages { get; init; }
    }
}

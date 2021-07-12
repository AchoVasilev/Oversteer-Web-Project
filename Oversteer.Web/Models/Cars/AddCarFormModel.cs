namespace Oversteer.Web.Models.Cars
{
    using System.Collections.Generic;

    public class AddCarFormModel
    {

        public decimal DailyPrice { get; set; }

        public int SeatsCount { get; set; }

        public int BrandId { get; init; }

        public string Model { get; init; }

        public int Year { get; init; }

        public string Description { get; init; }

        public int ImageId { get; set; }

        public int ColorId { get; set; }

        public int FuelId { get; set; }

        public int FeatureId { get; set; }

        public IEnumerable<CarBrandFormModel> Brands { get; init; }

        public IEnumerable<ColorFormModel> Colors { get; init; }

        public IEnumerable<FuelTypeFormModel> FuelTypes { get; init; }

    }
}

namespace Oversteer.Web.Models.Cars
{
    using System.Collections.Generic;

    public class AddCarFormModel
    {
        public decimal DailyPrice { get; set; }

        public int SeatsCount { get; set; }

        public int BrandId { get; init; }

        public int ModelId { get; init; }

        public int Year { get; init; }

        public int ImageId { get; set; }

        public int ColorId { get; set; }

        public int FuelId { get; set; }

        public IEnumerable<string> InteriourFeatures { get; set; }

        public IEnumerable<string> ImageUrls { get; set; }
    }
}

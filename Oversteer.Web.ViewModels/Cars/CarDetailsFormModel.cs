namespace Oversteer.Web.ViewModels.Cars
{
    using System.Collections.Generic;

    using Oversteer.Web.ViewModels.Cars.CarItems;

    public class CarDetailsFormModel : ICarModel
    {
        public int Id { get; init; }

        public string BrandName { get; init; }

        public string ModelName { get; init; }

        public int ModelYear { get; init; }

        public string CarTypeName { get; init; }

        public string ColorName { get; init; }

        public string FuelName { get; init; }

        public string Url { get; init; }

        public string TransmissionName { get; init; }

        public int CompanyId { get; init; }

        public string CompanyName { get; init; }

        public string CompanyDescription { get; init; }

        public string Description { get; init; }

        public decimal DailyPrice { get; init; }

        public int SeatsCount { get; init; }

        public string CompanyUserId { get; init; }

        public List<string> Urls { get; init; } = new List<string>();

        public IEnumerable<CarFeatureFormModel> CarFeatures { get; set; }
    }
}

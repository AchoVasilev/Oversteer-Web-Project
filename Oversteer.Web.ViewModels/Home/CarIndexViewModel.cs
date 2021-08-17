namespace Oversteer.Web.ViewModels.Home
{
    using Oversteer.Web.ViewModels.Cars;

    public class CarIndexViewModel : ICarModel
    {
        public int Id { get; init; }

        public string BrandName { get; init; }

        public string ModelName { get; init; }

        public int ModelYear { get; init; }

        public int CompanyId { get; init; }

        public string CompanyName { get; init; }

        public decimal DailyPrice { get; init; }

        public string Url { get; init; }
    }
}

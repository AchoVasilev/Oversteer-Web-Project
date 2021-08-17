namespace Oversteer.Web.ViewModels.Home
{
    public class CarIndexViewModel
    {
        public int Id { get; init; }

        public string BrandName { get; init; }

        public string ModelName { get; init; }

        public int CompanyId { get; init; }

        public string CompanyName { get; init; }

        public decimal DailyPrice { get; init; }

        public string Url { get; init; }
    }
}

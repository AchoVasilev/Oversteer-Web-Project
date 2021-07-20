namespace Oversteer.Web.Models.Cars
{
    public class ListCarFormModel
    {
        public int Id { get; init; }

        public string Brand { get; init; }

        public string Model { get; init; }

        public int Year { get; init; }

        public string CarType { get; init; }

        public string Color { get; init; }

        public string FuelType { get; init; }

        public string Url { get; init; }

        public string TransmissionType { get; init; }

        public int CompanyId { get; init; }
    }
}

namespace Oversteer.Web.ViewModels.Rents
{
    using Oversteer.Data.Models.Enumerations;

    public class RentDetailsModel
    {
        public string Id { get; init; }

        public string CarModelName { get; init; }

        public string CarTransmissionName { get; init; }

        public int CarModelYear { get; init; }

        public string Url { get; init; }

        public string UserId { get; init; }

        public string UserEmail { get; init; }

        public string CarDescription { get; init; }

        public int CompanyId { get; init; }

        public string CompanyName { get; init; }

        public decimal Price { get; init; }

        public string StartDate { get; init; }

        public string ReturnDate { get; init; }

        public OrderStatus OrderStatus { get; init; }

        public string PickUpLocationName { get; init; }

        public string DropOffLocationName { get; init; }
    }
}

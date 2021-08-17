namespace Oversteer.Web.ViewModels.Rents
{
    using Oversteer.Data.Models.Enumerations;
    using Oversteer.Web.ViewModels.Cars;
    using Oversteer.Web.ViewModels.Clients;
    using Oversteer.Web.ViewModels.Feedbacks;
    using Oversteer.Web.ViewModels.Locations;

    public class RentsDto
    {
        public string Id { get; init; }

        public CarDetailsFormModel Car { get; init; }

        public string UserId { get; init; }

        public string StartDate { get; init; }

        public string ReturnDate { get; init; }

        public decimal Price { get; init; }

        public int CompanyId { get; init; }

        public string CompanyName { get; init; }

        public ClientFormModel User { get; init; }

        public OrderStatus OrderStatus { get; init; }

        public int PickUpLocationId { get; init; }

        public LocationDto PickUpLocation { get; init; }

        public int ReturnLocationId { get; init; }

        public LocationDto DropOffLocation { get; init; }

        public string CreatedOn { get; init; }

        public int? FeedbackId { get; set; }

        public FeedbackDto Feedback { get; set; }
    }
}

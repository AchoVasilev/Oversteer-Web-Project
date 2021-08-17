namespace Oversteer.Web.ViewModels.Rents
{
    using Oversteer.Data.Models.Enumerations;

    public class MyRentsViewModel
    {
        public string Id { get; set; }

        public string CarModelName { get; set; }

        public string PickUpLocationName { get; set; }

        public string DropOffLocationName { get; set; }

        public string CompanyName { get; init; }

        public decimal Price { get; set; }

        public string StartDate { get; set; }

        public string ReturnDate { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public int FeedbackId { get; set; }
    }
}

namespace Oversteer.Web.ViewModels.Rents
{
    using System.ComponentModel.DataAnnotations;

    public class RentPreviewModel
    {
        public int CarId { get; init; }

        public string Model { get; init; }

        public decimal PricePerDay { get; init; }

        public int CompanyId { get; init; }

        public string CompanyName { get; init; }

        public string RentStart { get; init; }

        public string RentEnd { get; init; }

        public int Days { get; init; }

        [Required]
        public string Image { get; init; }

        [Required]
        public string PickUpPlace { get; init; }

        [Required]
        public string ReturnPlace { get; init; }

        public decimal TotalPrice => this.PricePerDay * this.Days;
    }
}

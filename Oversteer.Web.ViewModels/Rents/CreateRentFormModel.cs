namespace Oversteer.Web.ViewModels.Rents
{
    using System.ComponentModel.DataAnnotations;

    public class CreateRentFormModel
    {
        public int CarId { get; init; }

        [Required]
        public string StartDate { get; init; }

        [Required]
        public string EndDate { get; init; }

        public int CompanyId { get; init; }

        public string CompanyName { get; init; }

        [Required]
        public decimal Price { get; init; }

        [Required]
        public string StartLocation { get; init; }

        [Required]
        public string ReturnLocation { get; init; }
    }
}

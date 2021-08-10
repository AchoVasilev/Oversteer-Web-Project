namespace Oversteer.Web.ViewModels.Rents
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Oversteer.Web.ViewModels.Locations;

    using static Oversteer.Data.Common.Constants.ModelsDisplayNames;

    public class CreateRentFormModel
    {
        public int CarId { get; init; }

        [Required]
        public string PickUpDate { get; init; }

        [Required]
        public string ReturnDate { get; init; }

        public decimal Price { get; init; }

        [Required]
        public string StartLocation { get; init; }

        public IEnumerable<LocationFormModel> ReturnLocation { get; init; }

        public int CompanyId { get; init; }

        public string CompanyName { get; init; }

        [Display(Name = CarLocation)]
        public int LocationId { get; init; }
    }
}

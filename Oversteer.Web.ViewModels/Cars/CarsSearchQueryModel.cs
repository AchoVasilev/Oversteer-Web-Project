namespace Oversteer.Web.ViewModels.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Oversteer.Web.ViewModels.Cars.Enumerations;

    using static Oversteer.Data.Common.Constants.ModelsDisplayNames;

    public class CarsSearchQueryModel
    {
        public const int CarsPerPage = 3;

        public string Brand { get; init; }

        public IEnumerable<string> Brands { get; init; }

        [Display(Name = Search)]
        public string SearchTerm { get; init; }

        [Display(Name = Sorting)]
        public CarSorting CarSorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalCars { get; init; } = 0;

        public IEnumerable<ListCarFormModel> Cars { get; init; }

        public int CompanyId { get; init; }
    }
}

namespace Oversteer.Web.Models.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int TotalCars { get; init; }

        public int TotalRents { get; init; }

        public int TotalUsers { get; init; }

        public int TotalCompanies { get; init; }

        public List<CarIndexViewModel> Cars { get; init; }
    }
}

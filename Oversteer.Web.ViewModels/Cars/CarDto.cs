namespace Oversteer.Web.ViewModels.Cars
{
    using System.Collections.Generic;

    using Oversteer.Data.Models.Cars;

    public class CarDto
    {
        public int Id { get; init; }

        public int CompanyId { get; init; }

        public string CompanyName { get; init; }

        public string BrandName { get; init; }

        public string ModelName { get; init; }

        public string Description { get; init; }

        public int ModelYear { get; init; }

        public string Image { get; init; }

        public string TransmissionName { get; init; }

        public decimal DailyPrice { get; init; }

        public string LocationName { get; init; }

        public string StartRent { get; init; }

        public string EndRent { get; init; }

        public int Days { get; init; }

        public virtual IEnumerable<CarRentDays> RentDays { get; set; }
    }
}

namespace Oversteer.Web.ViewModels.Cars
{
    using System.Collections.Generic;

    using Oversteer.Data.Models.Cars;

    public class CarDto
    {
        public int Id { get; set; }

        public string BrandName { get; set; }

        public string ModelName { get; set; }

        public string Description { get; set; }

        public int ModelYear { get; set; }

        public string Image { get; set; }

        public string TransmissionName { get; set; }

        public decimal DailyPrice { get; set; }

        public string LocationName { get; set; }

        public string StartRent { get; set; }

        public string EndRent { get; set; }

        public int Days { get; set; }

        public virtual IEnumerable<CarRentDays> RentDays { get; set; }
    }
}

namespace Oversteer.Web.ViewModels.Cars
{
    using System.Collections.Generic;

    public class AvailableCarModel
    {
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public double Days { get; set; }

        public IEnumerable<CarDto> Cars { get; set; }

        public string PickUpPlace { get; set; }

        public string ReturnPlace { get; set; }
    }
}

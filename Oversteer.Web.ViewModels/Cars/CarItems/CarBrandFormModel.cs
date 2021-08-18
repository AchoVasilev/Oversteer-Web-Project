namespace Oversteer.Web.ViewModels.Cars.CarItems
{
    using System.Collections.Generic;

    public class CarBrandFormModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public IEnumerable<CarModelFormModel> Models { get; set; }
    }
}

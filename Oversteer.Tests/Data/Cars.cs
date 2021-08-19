namespace Oversteer.Tests.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Oversteer.Data.Models.Cars;

    public class Cars
    {
        public static IEnumerable<Car> TenPublicCars
            => Enumerable.Range(0, 10).Select(i => new Car());
    }
}

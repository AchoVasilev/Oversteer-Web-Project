namespace Oversteer.Data.Seeding.Dto
{
    using System.Collections.Generic;

    public class CarBrandDto
    {
        public string Name { get; set; }

        public ICollection<CarModelDto> Models { get; set; }
    }
}

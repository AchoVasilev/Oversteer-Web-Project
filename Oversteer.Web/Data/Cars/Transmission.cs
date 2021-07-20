namespace Oversteer.Web.Data.Cars
{
    using Oversteer.Models.Cars;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Transmission
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}

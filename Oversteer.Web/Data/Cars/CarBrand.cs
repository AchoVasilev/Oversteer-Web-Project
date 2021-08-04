namespace Oversteer.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Oversteer.Web.Data.Cars;

    using static Oversteer.Models.Constants.DataConstants.Cars;

    public class CarBrand
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxValue)]
        public string Name { get; set; }

        public virtual ICollection<CarModel> CarModels { get; set; } = new HashSet<CarModel>();

        public virtual ICollection<Car> Cars { get; set; } = new HashSet<Car>();
    }
}

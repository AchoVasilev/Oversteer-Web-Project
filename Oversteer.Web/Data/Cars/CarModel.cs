
namespace Oversteer.Web.Data.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Oversteer.Models.Cars;
    using static Oversteer.Models.Constants.DataConstants.Cars;

    public class CarModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CarModelMaxValue)]
        public string Name { get; set; }

        [ForeignKey(nameof(CarBrand))]
        public int CarBrandId { get; set; }

        public virtual CarBrand CarBrand { get; set; }

        public virtual ICollection<Car> Cars { get; set; } = new HashSet<Car>();
    }
}

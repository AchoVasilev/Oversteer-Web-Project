namespace Oversteer.Data.Models.Cars
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Oversteer.Data.Common.Constants.DataConstants.Cars;

    public class CarBrand
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxValue)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime DeletedOn { get; set; }

        public virtual ICollection<CarModel> CarModels { get; set; } = new HashSet<CarModel>();

        public virtual ICollection<Car> Cars { get; set; } = new HashSet<Car>();
    }
}

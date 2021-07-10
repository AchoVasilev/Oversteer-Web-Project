﻿namespace Oversteer.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Oversteer.Web.Data.Cars;
    using static Oversteer.Models.Constants.DataConstants;

    public class CarBrand
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(CarBrandNameMaxValue)]
        public string Name { get; set; }

        public IEnumerable<CarModel> CarModels { get; set; } = new HashSet<CarModel>();
    }
}

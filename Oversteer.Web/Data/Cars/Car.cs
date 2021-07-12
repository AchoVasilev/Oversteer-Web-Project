﻿namespace Oversteer.Models.Cars
{
    using Oversteer.Web.Data.Cars;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Oversteer.Models.Constants.DataConstants;

    public class Car
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }

        public virtual CarBrand Brand { get; set; }

        [Required]
        [MaxLength(CarModelMaxValue)]
        public string Model { get; set; }

        public int ModelYear { get; set; }

        public decimal DailyPrice { get; set; }

        public int SeatsCount { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string Description { get; set; }

        [ForeignKey(nameof(Color))]

        public int ColorId { get; set; }

        public virtual Color Color { get; set; }

        [ForeignKey(nameof(Fuel))]
        public int FuelId { get; set; }

        public virtual Fuel Fuel { get; set; }

        [ForeignKey(nameof(CarType))]
        public int CarTypeId { get; set; }

        public virtual CarType CarType { get; set; }

        [ForeignKey(nameof(Transmission))]
        public int TransmissionId { get; set; }

        public virtual Transmission Transmission { get; set; }

       // public virtual ICollection<CarImage> CarImages { get; set; } = new HashSet<CarImage>();
    }
}
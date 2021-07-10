namespace Oversteer.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Car
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }

        public virtual CarBrand Brand { get; set; }

        public int ModelYear { get; set; }

        public decimal DailyPrice { get; set; }

        public int SeatsCount { get; set; }

        [ForeignKey(nameof(Color))]

        public int ColorId { get; set; }

        public virtual Color Color { get; set; }

        [ForeignKey(nameof(Fuel))]
        public int FuelId { get; set; }

        public virtual Fuel Fuel { get; set; }

        [ForeignKey(nameof(CarType))]
        public int CarTypeId { get; set; }

        public virtual CarType CarType { get; set; }

        public virtual ICollection<InteriourFeature> InteriourFeatures { get; set; } = new HashSet<InteriourFeature>();

        public virtual ICollection<CarImage> CarImages { get; set; } = new HashSet<CarImage>();
    }
}

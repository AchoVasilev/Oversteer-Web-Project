namespace Oversteer.Data.Models.Cars
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Oversteer.Data.Models.Rentals;
    using Oversteer.Data.Models.Users;

    public class Car
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [ForeignKey(nameof(CarBrand))]
        public int CarBrandId { get; set; }

        public virtual CarBrand Brand { get; set; }

        [ForeignKey(nameof(CarModel))]
        public int CarModelId { get; set; }

        public virtual CarModel Model { get; set; }

        public int ModelYear { get; set; }

        public decimal DailyPrice { get; set; }

        public int SeatsCount { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsAvailable { get; set; } = true;

        public DateTime? DeleteDate { get; set; }

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

        [ForeignKey(nameof(Company))]
        public int CompanyId { get; init; }

        public virtual Company Company { get; init; }

        [ForeignKey(nameof(Location))]
        public int LocationId { get; init; }

        public virtual Location Location { get; init; }

        public virtual ICollection<CarImage> CarImages { get; set; } = new HashSet<CarImage>();

        public virtual ICollection<CarFeature> CarFeatures { get; set; } = new HashSet<CarFeature>();

        public virtual ICollection<CarRentDays> RentDays { get; set; } = new HashSet<CarRentDays>();
    }
}

namespace Oversteer.Models.Cars
{
    using Oversteer.Models.Add;
    using Oversteer.Models.Users;
    using Oversteer.Web.Data.Cars;

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey(nameof(Company))]
        public int CompanyId { get; init; }

        public virtual Company Company { get; init; }

        [ForeignKey(nameof(CarAdd))]
        public int CarAddId { get; init; }

        public virtual CarAdd CarAdd { get; init; }

        public bool IsDeleted { get; set; } = false;

        public DateTime? DeleteDate { get; set; }

        // public virtual ICollection<CarImage> CarImages { get; set; } = new HashSet<CarImage>();
    }
}

namespace Oversteer.Data.Models.Rentals
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Oversteer.Data.Models.Cars;
    using Oversteer.Data.Models.Enumerations;
    using Oversteer.Data.Models.Users;

    using static Oversteer.Data.Common.Constants.DataConstants;

    public class Rental
    {
        [Key]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime StartDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public decimal Price { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime DeletedOn { get; set; }

        public OrderStatus OrderStatus { get; set; }

        [ForeignKey(nameof(Location))]
        public int PickUpLocationId { get; set; }

        public virtual Location PickUpLocation { get; set; }

        [ForeignKey(nameof(Location))]
        public int DropOffLocationId { get; set; }

        public virtual Location DropOffLocation { get; set; }

        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        [ForeignKey(nameof(Car))]
        public int CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}

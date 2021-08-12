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

        public DateTime CreatedOn { get; init; } = DateTime.UtcNow;

        public DateTime StartDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public decimal Price { get; set; }

        public OrderStatus OrderStatus { get; set; }

        [ForeignKey(nameof(Location))]
        public int PickUpLocationId { get; init; }

        public virtual Location PickUpLocation { get; init; }

        [ForeignKey(nameof(Location))]
        public int DropOffLocationId { get; init; }

        public virtual Location DropOffLocation { get; init; }

        [ForeignKey(nameof(Client))]
        public int ClientId { get; init; }

        public virtual Client Client { get; init; }

        [ForeignKey(nameof(Company))]
        public int CompanyId { get; init; }

        public virtual Company Company { get; init; }

        [ForeignKey(nameof(Car))]
        public int CarId { get; init; }

        public virtual Car Car { get; init; }
    }
}

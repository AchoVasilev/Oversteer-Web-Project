namespace Oversteer.Data.Models.Rentals
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Oversteer.Data.Models.Users;

    public class Location
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(Country))]
        public int? CountryId { get; init; }

        public virtual Country Country { get; init; }

        [ForeignKey(nameof(City))]
        public int? CityId { get; init; }

        public virtual City City { get; init; }

        [ForeignKey(nameof(Address))]
        public int? AddressId { get; init; }

        public virtual Address Address { get; init; }

        [ForeignKey(nameof(Company))]
        public int CompanyId { get; init; }

        public virtual Company Company { get; init; }

        public bool IsDeleted { get; set; } = false;

        public DateTime DeletedOn { get; set; }
    }
}

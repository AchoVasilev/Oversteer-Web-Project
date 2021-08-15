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
        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }

        [ForeignKey(nameof(City))]
        public int? CityId { get; set; }

        public virtual City City { get; set; }

        [ForeignKey(nameof(Address))]
        public int? AddressId { get; set; }

        public virtual Address Address { get; set; }

        [ForeignKey(nameof(Company))]
        public int CompanyId { get; init; }

        public virtual Company Company { get; init; }

        public bool IsDeleted { get; set; } = false;

        public DateTime DeletedOn { get; set; }
    }
}

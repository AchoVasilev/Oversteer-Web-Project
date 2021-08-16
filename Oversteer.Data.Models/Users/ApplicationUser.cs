namespace Oversteer.Data.Models.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Identity;

    using Oversteer.Data.Models.Rentals;

    using static Oversteer.Data.Common.Constants.DataConstants.Users;

    public class ApplicationUser : IdentityUser
    {
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        [ForeignKey(nameof(Country))]
        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }

        [ForeignKey(nameof(City))]
        public int? CityId { get; set; }

        public virtual City City { get; set; }

        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [MaxLength(NameMaxLength)]
        public string MiddleName { get; set; }

        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }

        public virtual ICollection<Rental> Rental { get; set; } = new HashSet<Rental>();
    }
}

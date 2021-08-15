namespace Oversteer.Data.Models.Users
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Identity;

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

        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}

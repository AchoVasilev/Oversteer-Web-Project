namespace Oversteer.Data.Models.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime DeletedOn { get; set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; } = new HashSet<ApplicationUser>();

        public virtual ICollection<Address> Addresses { get; set; } = new HashSet<Address>();
    }
}
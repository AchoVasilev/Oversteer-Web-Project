namespace Oversteer.Data.Models.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime DeletedOn { get; set; }

        public virtual ICollection<City> Cities { get; set; } = new HashSet<City>();

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; } = new HashSet<ApplicationUser>();
    }
}

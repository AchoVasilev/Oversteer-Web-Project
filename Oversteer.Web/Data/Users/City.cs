namespace Oversteer.Models.Users
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Oversteer.Models.Add;
    using Oversteer.Web.Data.Users;

    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<ZipCode> ZipCodes { get; set; } = new HashSet<ZipCode>();

        public virtual ICollection<ApplicationUser> Users { get; set; } = new HashSet<ApplicationUser>();

        public virtual ICollection<Destination> Destinations { get; set; } = new HashSet<Destination>();

        public virtual ICollection<Address> Addresses { get; set; } = new HashSet<Address>();
    }
}
namespace Oversteer.Models.Users
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Oversteer.Models.Add;

    public class Country
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(CountryCode))]
        public int CountryCodeId { get; set; }

        public CountryCode CountryCode { get; set; }

        public ICollection<City> Cities { get; set; } = new HashSet<City>();

        public ICollection<Destination> Destinations { get; set; } = new HashSet<Destination>();

        public ICollection<ApplicationUser> ApplicationUsers { get; set; } = new HashSet<ApplicationUser>();
    }
}

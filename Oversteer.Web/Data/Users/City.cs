namespace Oversteer.Models.Users
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Oversteer.Models.Add;

    public class City
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string CityName { get; set; }

        [Required]
        public string Address { get; set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public Country Country { get; set; }

        public ICollection<ZipCode> ZipCodes { get; set; } = new HashSet<ZipCode>();

        public ICollection<ApplicationUser> Users { get; set; } = new HashSet<ApplicationUser>();

        public ICollection<Destination> Destinations { get; set; } = new HashSet<Destination>();
    }
}
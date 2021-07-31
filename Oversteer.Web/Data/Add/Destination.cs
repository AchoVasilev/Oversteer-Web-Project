namespace Oversteer.Models.Add
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Oversteer.Models.Users;

    public class Destination
    {
        [Required]
        public int Id { get; set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        [ForeignKey(nameof(City))]
        public int CityId { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<Rental> Rental { get; set; } = new HashSet<Rental>();
    }
}

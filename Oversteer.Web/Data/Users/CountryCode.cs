namespace Oversteer.Models.Users
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Oversteer.Models.Constants.DataConstants;

    public class CountryCode
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CountryCodeMaxLength)]
        public string Code { get; set; }

        public string IsoCode { get; set; }

        public virtual ICollection<Country> Countries { get; set; } = new HashSet<Country>();

        public virtual ICollection<ApplicationUser> Users { get; set; } = new HashSet<ApplicationUser>();
    }
}

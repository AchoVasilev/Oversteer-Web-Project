namespace Oversteer.Data.Models.Users
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Oversteer.Data.Models.Cars;
    using Oversteer.Data.Models.Rentals;

    using static Oversteer.Data.Common.Constants.DataConstants;
    using static Oversteer.Data.Common.Constants.DataConstants.Users;

    public class Company
    {
        [Key]
        [MaxLength(IdMaxLength)]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Description { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        [Required]
        public string UserId { get; init; }

        public virtual ApplicationUser User { get; init; }

        public virtual ICollection<Car> Cars { get; set; } = new HashSet<Car>();

        public virtual ICollection<CompanyService> CompanyServices { get; set; } = new HashSet<CompanyService>();

        public virtual ICollection<Location> Locations { get; set; } = new HashSet<Location>();
    }
}

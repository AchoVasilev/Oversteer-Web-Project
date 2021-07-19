namespace Oversteer.Models.Users
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Oversteer.Models.Add;
    using Oversteer.Models.Cars;

    using static Oversteer.Models.Constants.DataConstants;
    using static Oversteer.Models.Constants.DataConstants.Users;

    public class Company
    {
        [Key]
        [MaxLength(IdMaxLength)]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        public string Description { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Car> Cars { get; set; } = new HashSet<Car>();

        public virtual ICollection<CompanyService> CompanyServices { get; set; } = new HashSet<CompanyService>();

        public virtual ICollection<CarAdd> CarAdds { get; set; } = new HashSet<CarAdd>();
    }
}

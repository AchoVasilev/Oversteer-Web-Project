namespace Oversteer.Models.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Oversteer.Models.Cars;
    using static Oversteer.Models.Constants.DataConstants;

    public class Company
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string CompanyName { get; set; }

        public string Description { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Car> Cars { get; set; } = new HashSet<Car>();

        public virtual ICollection<CompanyService> CompanyServices { get; set; } = new HashSet<CompanyService>();
    }
}

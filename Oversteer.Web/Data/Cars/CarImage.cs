namespace Oversteer.Models.Cars
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Oversteer.Models.Users;

    using static Oversteer.Models.Constants.DataConstants;

    public class CarImage
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string RemoteImageUrl { get; set; }

        public string Extension { get; set; }

        [Required]
        [ForeignKey(nameof(Car))]
        public int CarId { get; set; }

        public virtual Car Car { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public int CompanyId { get; set; }

        public Company AddedByCompany { get; set; }
    }
}

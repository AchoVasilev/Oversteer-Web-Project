namespace Oversteer.Models.Add
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Oversteer.Models.Cars;
    using Oversteer.Models.Users;

    using static Oversteer.Models.Constants.DataConstants;

    public class CarAdd
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Title { get; set; }

        public DateTime PostedOn { get; set; } = DateTime.UtcNow;

        public bool IsAvailable { get; set; }

        [Required]
        [ForeignKey(nameof(Car))]
        public int CarId { get; set; }

        public virtual Car Car { get; set; }

        [Required]
        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime? DeleteDate { get; set; }
    }
}

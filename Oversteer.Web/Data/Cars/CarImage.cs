namespace Oversteer.Models.Cars
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Oversteer.Models.Constants.DataConstants;

    public class CarImage
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Path { get; set; }

        public DateTime ImageDate { get; set; } = DateTime.UtcNow;

        [Required]
        [ForeignKey(nameof(Car))]
        public int CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}

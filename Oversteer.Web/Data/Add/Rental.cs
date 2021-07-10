namespace Oversteer.Models.Add
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Oversteer.Models.Constants.DataConstants;

    public class Rental
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime StartDate { get; set; }

        public DateTime ReturnDate { get; set; }

        [ForeignKey(nameof(Destination))]
        public int PlaceId { get; set; }

        public Destination Place { get; set; }
    }
}

namespace Oversteer.Models.Add
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Oversteer.Models.Users;

    using static Oversteer.Models.Constants.DataConstants;

    public class Rental
    {
        [Key]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime StartDate { get; set; }

        public DateTime ReturnDate { get; set; }

        [ForeignKey(nameof(Add.Destination))]
        public int DestinationId { get; set; }

        public virtual Destination Destination { get; set; }

        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}

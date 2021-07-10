namespace Oversteer.Models.Add
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Oversteer.Models.Constants.DataConstants;

    public class Payment
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string NameSurname { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string ExpirationDate { get; set; }

        [Required]
        public string Cvc { get; set; }

        [Required]
        [ForeignKey(nameof(Rental))]
        public string RentalId { get; set; }

        public virtual Rental Rental { get; set; }
    }
}

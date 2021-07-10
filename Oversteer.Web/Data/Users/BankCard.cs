namespace Oversteer.Models.Users
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Oversteer.Models.Constants.DataConstants;

    public class BankCard
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
        public byte ExpirationMonth { get; set; }

        [Required]
        public byte ExpirationYear { get; set; }

        [Required]
        public string Cvc { get; set; }

        [Required]
        [ForeignKey(nameof(Client))]
        public string ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}

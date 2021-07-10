namespace Oversteer.Models.Users
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Oversteer.Models.Constants.DataConstants;

    public class Client
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}

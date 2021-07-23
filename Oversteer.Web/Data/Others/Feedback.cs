namespace Oversteer.Models.Others
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Oversteer.Models.Users;

    using static Oversteer.Models.Constants.DataConstants;

    public class Feedback
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public int Raiting { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}

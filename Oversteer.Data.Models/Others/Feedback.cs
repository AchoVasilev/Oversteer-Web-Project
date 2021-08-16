namespace Oversteer.Data.Models.Others
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Oversteer.Data.Models.Users;

    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        [Range(1, 5)]
        public int Raiting { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}

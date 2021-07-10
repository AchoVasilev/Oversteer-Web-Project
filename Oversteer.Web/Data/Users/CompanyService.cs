namespace Oversteer.Models.Users
{
    using System.ComponentModel.DataAnnotations;

    public class CompanyService
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}

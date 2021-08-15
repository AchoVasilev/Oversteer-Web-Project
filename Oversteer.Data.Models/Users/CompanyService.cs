namespace Oversteer.Data.Models.Users
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Data.Common.Constants.DataConstants.Companies;
    public class CompanyService
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ServiceNameMaxLength)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}

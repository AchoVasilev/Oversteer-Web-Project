﻿namespace Oversteer.Data.Models.Users
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CompanyService
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}

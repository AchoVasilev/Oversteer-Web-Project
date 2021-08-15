namespace Oversteer.Data.Models.Others
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Oversteer.Data.Models.Users;

    public class Feedback
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Raiting { get; set; }

        [Required]
        public string Comment { get; set; }

        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}

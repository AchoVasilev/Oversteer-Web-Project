namespace Oversteer.Models.Cars
{
    using System.ComponentModel.DataAnnotations;

    public class InteriourFeature
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}

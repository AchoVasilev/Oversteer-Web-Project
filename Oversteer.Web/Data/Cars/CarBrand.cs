namespace Oversteer.Models.Cars
{
    using System.ComponentModel.DataAnnotations;
    using static Oversteer.Models.Constants.DataConstants;

    public class CarBrand
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(CarBrandNameMaxValue)]
        public string Name { get; set; }
    }
}

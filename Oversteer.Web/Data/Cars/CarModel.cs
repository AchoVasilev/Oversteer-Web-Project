namespace Oversteer.Web.Data.Cars
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Oversteer.Models.Cars;

    public class CarModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey(nameof(CarBrand))]
        public int CarBrandId { get; set; }

        public virtual CarBrand CarBrand { get; set; }
    }
}

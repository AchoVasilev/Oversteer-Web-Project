namespace Oversteer.Data.Models.Cars
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CarRentDays
    {
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        public virtual Car Car { get; set; }

        [Required]
        public DateTime RentDate { get; set; }
    }
}

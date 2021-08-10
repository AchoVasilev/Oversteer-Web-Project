namespace Oversteer.Web.ViewModels.Rents
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RentPreviewModel
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public decimal PricePerDay { get; set; }

        public DateTime RentStart { get; set; }

        public DateTime RentEnd { get; set; }

        public int Days { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string PickUpPlace { get; set; }

        [Required]
        public string ReturnPlace { get; set; }

        public decimal TotalPrice => this.PricePerDay * this.Days;
    }
}

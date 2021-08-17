namespace Oversteer.Web.ViewModels.Rents
{
    using System.ComponentModel.DataAnnotations;

    using static Oversteer.Data.Common.Constants.ModelsDisplayNames;

    public class EditRentFormModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = ClientEmail)]
        public string ClientUserEmail { get; set; }

        [Required]
        [Display(Name = FirstName)]
        public string ClientFirstName { get; set; }

        [Required]
        [Display(Name = LastName)]
        public string ClientLastName { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string StartDate { get; set; }

        public string ReturnDate { get; set; }

        public string PickUpLocationName { get; set; }

        public string DropOffLocationName { get; set; }
    }
}

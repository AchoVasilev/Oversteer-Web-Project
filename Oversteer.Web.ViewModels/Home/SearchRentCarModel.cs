namespace Oversteer.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    public class SearchRentCarModel : IValidatableObject
    {
        private const string PickupError = "Pick Up date is invalid!";
        private const string ReturnError = "Return date is invalid!";

        public string PickUpDate { get; init; }

        public string ReturnDate { get; init; }

        public string PickUpLocation { get; init; }

        public string DropOffLocation { get; init; }

        public ICollection<string> Locations { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var pickUpDate = DateTime.TryParseExact(this.PickUpDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var pickRentDate);

            var returnDate = DateTime.TryParseExact(this.ReturnDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var returnRentDate);

            if (pickRentDate.Date >= returnRentDate.Date || pickRentDate.Date < DateTime.UtcNow.Date)
            {
                yield return new ValidationResult(PickupError);
            }

            if (returnRentDate.Date < DateTime.UtcNow.Date)
            {
                yield return new ValidationResult(ReturnError);
            }
        }
    }
}

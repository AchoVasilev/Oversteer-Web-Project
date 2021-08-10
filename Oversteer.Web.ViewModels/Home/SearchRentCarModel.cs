namespace Oversteer.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Oversteer.Web.Infrastructure;

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
            var pickUpDate = DateTimeParser.ParseDate(this.PickUpDate);

            var returnDate = DateTimeParser.ParseDate(this.ReturnDate);

            if (pickUpDate.Date >= returnDate.Date || pickUpDate.Date < DateTime.UtcNow.Date)
            {
                yield return new ValidationResult(PickupError);
            }

            if (returnDate.Date < DateTime.UtcNow.Date)
            {
                yield return new ValidationResult(ReturnError);
            }
        }
    }
}

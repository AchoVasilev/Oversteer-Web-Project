namespace Oversteer.Web.Data.Constants
{
    public static class ErrorMessages
    {
        public const string DescriptionLengthError = "The field Description must be a string with a minimum length of {2}.";

        public const string CarBrandDoesntExist = "This car brand does not exist.";
        public const string CarModelDoesntExist = "This car model does not exist.";
        public const string CarTypeDoesntExist = "This car type does not exist.";
        public const string CarFuelTypeDoesntExist = "This fuel type does not exist.";
        public const string CarTransmissionTypeDoesntExist = "This transmission type does not exist.";
        public const string CarColorDoesntExist = "This color does not exist.";
        public const string CarInvalidYear = "Invalid year. The value should be between {0} and {1}.";
        public const string CarDoesntExist = "This car doesn't exist";

        public const string IvalidURL = "The URL you have entered is invalid";

        public class UserErrors
        {
            public const string InvalidNameLength = "Invalid name. The length should be between {0} and {1}.";
            public const string InvalidPhoneNumberLength = "Invalid phone number. The length should be between {0} and {1}.";
            public const string InvalidPhoneNumber = "Invalid phone number.";

            public const string CarNotInCompany = "This car does not belong to this company";
        }
    }
}

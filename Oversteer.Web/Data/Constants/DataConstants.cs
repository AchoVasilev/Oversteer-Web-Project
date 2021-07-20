namespace Oversteer.Models.Constants
{
    public static class DataConstants
    {
        public const string SpecifyDecimalColumnType = "decimal(18,2)";
        public const int IdMaxLength = 40;

        public const int PassworMinLength = 8;

        public const int MobilePhoneLength = 10;

        public const int CountryCodeMaxLength = 10;

        public const int MaxRating = 6;
        public const int MinRating = 1;

        public class Cars
        {
            public const int DescriptionMinLength = 6;
            public const int CarBrandNameMaxValue = 20;
            public const int CarModelMaxValue = 100;
            public const int CarModelMinValue = 1;
            public const int CarYearMinValue = 1900;
            public const int CarDescriptionMinValue = 8;
            public const int CarSeatMinCount = 1;
            public const int CarSeatMaxCount = 10;
            public const int CarSeatMaxLength = 4;
            public const int CarYearLength = 4;
        }

        public class Companies
        {
        }

        public class Users
        {
            public const int PhoneNumberMinLength = 6;
            public const int PhoneNumberMaxLength = 30;
            public const int NameMinLength = 2;
            public const int NameMaxLength = 30;
            public const string PhoneNumberRegularExpression = @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*";
        }
    }
}

namespace Oversteer.Services.DateTime
{
    using System;
    using System.Globalization;

    public class DateTimeParserService : IDateTimeParserService
    {
        public DateTime TryParseExact(string date)
        {
            var parse = DateTime.TryParseExact(date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var rentDate);

            return rentDate;
        }

        public DateTime Parse(string date)
            => DateTime.Parse(date);
    }
}

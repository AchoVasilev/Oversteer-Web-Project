namespace Oversteer.Web.Infrastructure
{
    using System;
    using System.Globalization;

    public static class DateTimeParser
    {
        public static DateTime ParseDate(string date)
        {
            var parse = DateTime.TryParseExact(date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var rentDate);

            return rentDate;
        }
    }
}

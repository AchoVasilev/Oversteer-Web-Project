namespace Oversteer.Services.DateTime
{
    using System;

    public interface IDateTimeParserService
    {
        DateTime TryParseExact(string date);

        DateTime Parse(string date);
    }
}

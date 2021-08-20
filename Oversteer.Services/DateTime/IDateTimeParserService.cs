namespace Oversteer.Services.DateTime
{
    using System;

    public interface IDateTimeParserService
    {
        DateTime ParseDate(string date);
    }
}

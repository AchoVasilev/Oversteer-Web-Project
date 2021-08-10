namespace Oversteer.Web.Infrastructure.CustomAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Oversteer.Data.Common.Constants.ErrorMessages;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class CurrentYearMaxValueAttribute : RangeAttribute
    {
        public CurrentYearMaxValueAttribute(int minimum) : base(minimum, DateTime.UtcNow.Year)
        {
            this.ErrorMessage = string.Format(CarInvalidYear, minimum, DateTime.UtcNow.Year);
        }
    }
}

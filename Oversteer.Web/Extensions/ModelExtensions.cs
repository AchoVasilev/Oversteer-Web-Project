namespace Oversteer.Web.Extensions
{
    using Oversteer.Web.ViewModels.Cars;

    public static class ModelExtensions
    {
        public static string ToFriendlyUrl(this ICarModel car)
        {
            return string.Join("-", car.BrandName, car.ModelName, car.ModelYear);
        }
    }
}

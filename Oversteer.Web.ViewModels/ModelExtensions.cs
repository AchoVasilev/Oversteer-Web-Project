namespace Oversteer.Web.ViewModels
{
    using Oversteer.Web.ViewModels.Cars;

    public static class ModelExtensions
    {
        public static string ToFriendlyUrl(this ICarModel car)
             => string.Join("-", car.BrandName, car.ModelName, car.ModelYear);
    }
}

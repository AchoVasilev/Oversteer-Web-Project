namespace Oversteer.Data.Common.Constants
{
    public static class WebConstants
    {
        public const string AdministratorRoleName = "Administrator";

        public class ApiRoutes
        {
            public const string StatisticsApiControllerRoute = "api/statistics";
            public const string CarsApiControllerRoute = "api/cars";
            public const string CarsApiControllerBrandsRoute = "/brands";
            public const string CarsApiControllerModelsRoute = "/models";
        }

        public class SignalR
        {
            public const string SignalRMessageForNewOrder = "Someone just rented a car {0} for {1} day/s.";
            public const string SignalRMethodNewOrder = "NotifyOrders";
        }

        public class Caching
        {
            public const string CarBrandsCacheKey = "carBrandsCacheKey";
            public const string CarModelsCacheKey = "carModelsCacheKey";
            public const string CarColorsCacheKey = "carColorsCacheKey";
            public const string CarFuelTypesCacheKey = "carFuelTypesCacheKey";
            public const string CarTransmissionTypesCacheKey = "carTransmissionTypesCacheKey";
            public const string CarTypesCacheKey = "carTypesCacheKey";

            public const string LatestCarCacheKey = "LatestCarsCacheKey";
            public const string TotalCarsCacheKey = "TotalCarsCacheKey";
        }
    }
}

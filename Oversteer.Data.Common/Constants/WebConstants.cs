﻿namespace Oversteer.Data.Common.Constants
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
    }
}

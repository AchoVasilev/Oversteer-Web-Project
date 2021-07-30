﻿namespace Oversteer.Web.Models.Cars
{
    public class ListCarFormModel
    {
        public int Id { get; init; }

        public string BrandName { get; init; }

        public string ModelName { get; init; }

        public int ModelYear { get; init; }

        public string CarTypeName { get; init; }

        public string ColorName { get; init; }

        public string FuelName { get; init; }

        public string Url { get; init; }

        public string TransmissionName { get; init; }

        public decimal DailyPrice { get; init; }

        public int CompanyId { get; init; }
    }
}

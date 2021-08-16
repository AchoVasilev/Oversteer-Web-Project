namespace Oversteer.Services.Statistics
{
    using System.Linq;

    using Oversteer.Data;
    using Oversteer.Web.ViewModels.Api.Statistics;

    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDbContext data;

        public StatisticsService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public StatisticsViewModel Total()
        {
            var totalCars = this.data.Cars
                .Where(x => !x.IsDeleted)
                .Count();

            var totalUsers = this.data.Users.Count();

            var totalRents = this.data.Rentals.Count();

            var totalCompanies = this.data.Companies.Count();

            return new StatisticsViewModel()
            {
                TotalCars = totalCars,
                TotalUsers = totalUsers,
                TotalCompanies = totalCompanies,
                TotalRents = totalRents
            };
        }
    }
}

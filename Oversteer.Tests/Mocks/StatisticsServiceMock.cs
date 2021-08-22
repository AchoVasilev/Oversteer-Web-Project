namespace Oversteer.Tests.Mock
{

    using Moq;

    using Oversteer.Services.Statistics;
    using Oversteer.Web.ViewModels.Api.Statistics;

    public class StatisticsServiceMock
    {
        public static IStatisticsService Instance
        {
            get
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock.Setup(x => x.Total())
                    .Returns(new StatisticsViewModel()
                    {
                        TotalCars = 5,
                        TotalCompanies = 1,
                        TotalRents = 10,
                        TotalUsers = 15
                    });

                return statisticsServiceMock.Object;
            }
        }
    }
}

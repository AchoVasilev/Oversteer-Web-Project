namespace Oversteer.Tests.Controllers
{
    using Oversteer.Tests.Mock;
    using Oversteer.Web.Controllers.Api;

    using Xunit;

    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldReturnTotalStatistics()
        {
            var statisticsController = new StatisticsApiController(StatisticsServiceMock.Instance);

            var result = statisticsController.GetStatistics();

            Assert.NotNull(result);
            Assert.Equal(5, result.TotalCars);
            Assert.Equal(1, result.TotalCompanies);
            Assert.Equal(10, result.TotalRents);
            Assert.Equal(15, result.TotalUsers);
        }
    }
}

namespace Oversteer.Web.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Statistics;
    using Oversteer.Web.ViewModels.Api.Statistics;

    using static Oversteer.Data.Common.Constants.WebConstants.ApiRoutes;

    [ApiController]
    [Route(StatisticsApiControllerRoute)]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsApiController(IStatisticsService statisticsService)
            => this.statisticsService = statisticsService;

        [HttpGet]
        public StatisticsViewModel GetStatistics()
            => this.statisticsService.Total();
    }
}

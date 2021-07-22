namespace Oversteer.Web.Services.Home
{
    using System.Linq;

    using Oversteer.Web.Data;
    using Oversteer.Web.Services.Contracts;

    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext data;

        public HomeService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public int GetTotalCarsCount()
            => this.data.Cars
                .Where(x => !x.IsDeleted)
                .Count();
    }
}

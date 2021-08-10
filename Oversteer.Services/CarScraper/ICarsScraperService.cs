namespace Oversteer.Services.CarScraper
{
    using System.Threading.Tasks;

    public interface ICarsScraperService
    {
        Task PopulateDatabaseWithCarBrandsAndModels();
    }
}

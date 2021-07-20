namespace Oversteer.Web.Services.Contracts
{
    using System.Threading.Tasks;

    public interface ICarsScraperService
    {
        Task PopulateDatabaseWithCarBrandsAndModels();
    }
}

namespace Oversteer.Services.Rentals
{
    using System.Threading.Tasks;

    using Oversteer.Web.ViewModels.Rents;

    public interface IRentingService
    {
        Task<bool> CreateOrderAsync(CreateRentFormModel model, string email);
    }
}

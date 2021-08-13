namespace Oversteer.Services.Rentals
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Oversteer.Web.ViewModels.Rents;

    public interface IRentingService
    {
        Task<bool> CreateOrderAsync(CreateRentFormModel model, string userId);

        IEnumerable<RentsDto> GetAllUserRents(string userId);

        ICollection<RentsDto> GetAllCompanyRents();

        Task<RentDetailsModel> GetDetailsAsync(string rentId);

        Task<bool> CancelAsync(string id);

        Task<bool> FinishAsync(string rentId);
    }
}

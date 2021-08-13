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

        Task<RentDetailsModel> GetDetails(string rentId);

        Task<bool> Cancel(string id);
    }
}

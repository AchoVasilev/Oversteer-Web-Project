namespace Oversteer.Services.Rentals
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Oversteer.Data.Models.Rentals;
    using Oversteer.Web.ViewModels.Rents;

    public interface IRentingService
    {
        Task<bool> CreateRentAsync(RentFormModel model, string userId);

        Task<RentDetailsModel> GetDetailsAsync(string rentId);

        Task<bool> CancelAsync(string id);

        Task<bool> FinishAsync(string rentId);

        Task<bool> DeleteAsync(string rentId);

        Task<Rental> GetRentByIdAsync(string id);

        Task<bool> EditRentAsync(string id, string clientFirstName, string clientLastName, string clientEmail, decimal price);

        Task<bool> IsValidFeedbackRequestAsync(string orderId, string customerEmail);

        Task<bool> UserFinishedRentsAsync(string username);

        Task<bool> DeleteFeedbackFromRentAsync(int feedbackId);

        IEnumerable<RentsDto> GetAllUserRents(string userId);

        ICollection<RentsDto> GetCurrentCompanyRents(int companyId);

        ICollection<RentsDto> GetAllCompaniesRents();
    }
}

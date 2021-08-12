namespace Oversteer.Services.Clients
{
    using System.Threading.Tasks;

    using Oversteer.Data.Models.Users;

    public interface IClientsService
    {
        Task<Client> RegisterUserAsync(string firstName, string surName, string lastName, string phoneNumber, string userId);

        string GetClientUserIdByEmailAsync(string email);

        int GetClientIdByEmailAsync(string email);

        Task<int> GetClientIdByUserId(string userId);
    }
}

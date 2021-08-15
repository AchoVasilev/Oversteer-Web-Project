namespace Oversteer.Services.Clients
{
    using System.Threading.Tasks;

    using Oversteer.Data.Models.Users;

    public interface IClientsService
    {
        Task<Client> RegisterUserAsync(string firstName, string surName, string lastName, string phoneNumber, string userId);

        Task<int> GetClientIdByUserIdAsync(string userId);

        Task<string> GetClientFirstNameAsync(string userId);

        Task<string> GetClientSurnameAsync(string userId);

        Task<string> GetClientLastNameAsync(string userId);

        Task<bool> SetClientFirstNameAsync(string userId, string firstName);

        Task<bool> SetClientSurnameAsync(string userId, string surname);

        Task<bool> SetClientLastNameAsync(string userId, string lastName);

        string GetClientUserIdByEmailAsync(string email);

        int GetClientIdByEmailAsync(string email);
    }
}
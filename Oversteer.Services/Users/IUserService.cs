namespace Oversteer.Services.Users
{
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<string> GetUserLastNameAsync(string userId);

        Task<string> GetUserMiddleNameAsync(string userId);

        Task<string> GetUserFirstNameAsync(string userId);

        Task<bool> SetUserFirstNameAsync(string userId, string firstName);

        Task<bool> SetUserMiddleNameAsync(string userId, string middleName);

        Task<bool> SetUserLastNameAsync(string userId, string lastName);
    }
}

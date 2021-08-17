namespace Oversteer.Services.Companies.Account
{
    using System.Threading.Tasks;

    public interface ICompanyAccountService
    {
        Task<bool> SetCompanyName(string userId, string name);

        Task<bool> SetCompanyDescription(string userId, string description);

        Task<bool> SetPhoneNumberAsync(int companyId, string phoneNumber);
    }
}

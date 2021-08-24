namespace Oversteer.Services.Companies.Account
{
    using System.Threading.Tasks;

    public interface ICompanyAccountService
    {
        Task<bool> SetCompanyNameAsync(string userId, string name);

        Task<bool> SetCompanyDescriptionAsync(string userId, string description);

        Task<bool> SetPhoneNumberAsync(int companyId, string phoneNumber);
    }
}

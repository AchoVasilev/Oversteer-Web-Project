namespace Oversteer.Services.Clients
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Oversteer.Data.Models.Users;
    using Oversteer.Data;

    public class ClientsService : IClientsService
    {
        private readonly ApplicationDbContext data;
        private readonly UserManager<ApplicationUser> userManager;

        public ClientsService(ApplicationDbContext data, UserManager<ApplicationUser> userManager)
        {
            this.data = data;
            this.userManager = userManager;
        }

        public async Task<Client> RegisterUserAsync(string firstName, string surName, string lastName, string phoneNumber, string userId)
        {
            var client = new Client()
            {
                FirstName = firstName,
                Surname = surName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                UserId = userId
            };

            await data.Clients.AddAsync(client);
            await data.SaveChangesAsync();

            return client;
        }

        public string GetClientUserIdByEmailAsync(string email)
        {
            var user = this.userManager.FindByEmailAsync(email).GetAwaiter().GetResult();

            return user.Id;
        }

        public int GetClientIdByEmailAsync(string email)
        {
            var user = this.userManager.FindByEmailAsync(email).GetAwaiter().GetResult();

            return user.ClientId;
        }
    }
}

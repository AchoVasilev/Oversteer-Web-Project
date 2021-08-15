namespace Oversteer.Services.Clients
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Oversteer.Data.Models.Users;
    using Oversteer.Data;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

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

        public async Task<int> GetClientIdByUserIdAsync(string userId)
            => await this.data.Clients
                        .Where(x => x.UserId == userId)
                        .Select(x => x.Id)
                        .FirstOrDefaultAsync();

        public async Task<string> GetClientFirstNameAsync(string userId)
            => await this.data.Clients
                              .Where(x => x.UserId == userId)
                              .Select(x => x.FirstName)
                              .FirstOrDefaultAsync();

        public async Task<string> GetClientSurnameAsync(string userId)
            => await this.data.Clients
                      .Where(x => x.UserId == userId)
                      .Select(x => x.Surname)
                      .FirstOrDefaultAsync();

        public async Task<string> GetClientLastNameAsync(string userId)
            => await this.data.Clients
                    .Where(x => x.UserId == userId)
                    .Select(x => x.LastName)
                    .FirstOrDefaultAsync();

        public async Task<bool> SetClientFirstNameAsync(string userId, string firstName)
        {
            var client = await this.data.Clients
                                  .Where(x => x.UserId == userId)
                                  .FirstOrDefaultAsync();

            if (client == null)
            {
                return false;
            }

            client.FirstName = firstName;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SetClientSurnameAsync(string userId, string surname)
        {
            var client = await this.data.Clients
                                  .Where(x => x.UserId == userId)
                                  .FirstOrDefaultAsync();

            if (client == null)
            {
                return false;
            }

            client.Surname = surname;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SetClientLastNameAsync(string userId, string lastName)
        {
            var client = await this.data.Clients
                                  .Where(x => x.UserId == userId)
                                  .FirstOrDefaultAsync();

            if (client == null)
            {
                return false;
            }

            client.LastName = lastName;

            await this.data.SaveChangesAsync();

            return true;
        }
    }
}

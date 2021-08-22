namespace Oversteer.Services.Users
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Oversteer.Data;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;

        public UserService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public async Task<string> GetUserFirstNameAsync(string userId)
            => await this.data.Users
                                .Where(x => x.Id == userId)
                                .Select(x => x.FirstName)
                                .FirstOrDefaultAsync();

        public async Task<string> GetUserMiddleNameAsync(string userId)
            => await this.data.Users
                        .Where(x => x.Id == userId)
                        .Select(x => x.MiddleName)
                        .FirstOrDefaultAsync();

        public async Task<string> GetUserLastNameAsync(string userId)
            => await this.data.Users
                        .Where(x => x.Id == userId)
                        .Select(x => x.LastName)
                        .FirstOrDefaultAsync();

        public async Task<bool> SetUserFirstNameAsync(string userId, string firstName)
        {
            var user = await this.data.Users
                        .Where(x => x.Id == userId)
                        .FirstOrDefaultAsync();

            if (user is null)
            {
                return false;
            }

            user.FirstName = firstName;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SetUserMiddleNameAsync(string userId, string middleName)
        {
            var user = await this.data.Users
                        .Where(x => x.Id == userId)
                        .FirstOrDefaultAsync();

            if (user is null)
            {
                return false;
            }

            user.MiddleName = middleName;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SetUserLastNameAsync(string userId, string lastName)
        {
            var user = await this.data.Users
                        .Where(x => x.Id == userId)
                        .FirstOrDefaultAsync();

            if (user is null)
            {
                return false;
            }

            user.LastName = lastName;

            await this.data.SaveChangesAsync();

            return true;
        }
    }
}
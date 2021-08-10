namespace Oversteer.Services.Cities
{
    using System.Linq;
    using System.Threading.Tasks;

    using Oversteer.Data;
    using Oversteer.Data.Models.Users;
    using Oversteer.Web.ViewModels.Cities;

    public class ZipCodesService : IZipCodesService
    {
        private readonly ApplicationDbContext data;

        public ZipCodesService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public async Task<int> CreateAsync(ZipCodeFormModel model, int cityId)
        {
            var zipCode = new ZipCode()
            {
                CityId = cityId,
                Code = model.Code
            };

            await this.data.ZipCodes.AddAsync(zipCode);
            await this.data.SaveChangesAsync();

            return zipCode.Id;
        }

        public bool ZipCodeIsInCity(int cityId, int zipCode)
        {
            var exists = this.data.ZipCodes
                .Any(x => x.CityId == cityId && x.Code == zipCode);

            if (!exists)
            {
                return false;
            }

            return true;
        }

        public int GetZipCodeIdByCity(int cityId, int? zipCode)
            => this.data.ZipCodes
                    .Where(x => x.Code == zipCode && x.CityId == cityId)
                    .Select(x => x.Id)
                    .FirstOrDefault();

        public bool ZipHasUser(string userId)
        {
            var zipCodes = this.data.ZipCodes.ToList();

            foreach (var zipCode in zipCodes)
            {
                var userHasZip = zipCode.Users.Any(x => x.Id == userId);

                if (userHasZip == true)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

namespace Oversteer.Services.Rentals
{
    using System.Threading.Tasks;

    using Oversteer.Data;
    using Oversteer.Data.Models.Enumerations;
    using Oversteer.Data.Models.Rentals;
    using Oversteer.Services.Cars;
    using Oversteer.Services.Clients;
    using Oversteer.Services.Companies;
    using Oversteer.Web.Infrastructure;
    using Oversteer.Web.ViewModels.Rents;

    public class RentingService : IRentingService
    {
        private readonly ApplicationDbContext data;
        private readonly IClientsService clientsService;
        private readonly ILocationService locationService;
        private readonly ICarsService carsService;

        public RentingService(
            ApplicationDbContext data,
            IClientsService clientsService,
            ILocationService locationService,
            ICarsService carsService)
        {
            this.data = data;
            this.clientsService = clientsService;
            this.locationService = locationService;
            this.carsService = carsService;
        }

        public async Task<bool> CreateOrderAsync(CreateRentFormModel model, string email)
        {
            var clientId = this.clientsService.GetClientIdByEmailAsync(email);

            var pickupLocationId = await this.locationService.GetLocationIdByNameAsync(model.StartLocation);

            if (clientId == 0)
            {
                return false;
            }

            var pickUpDate = DateTimeParser.ParseDate(model.PickUpDate);

            var returnDate = DateTimeParser.ParseDate(model.ReturnDate);

            var isRented = await this.carsService.IsRentedAsync(pickUpDate, returnDate, model.CarId);

            if (isRented)
            {
                return false;
            }

            var days = (pickUpDate - returnDate).Days;
            var totalPrice = model.Price * days;

            var order = new Rental()
            {
                ClientId = clientId,
                CarId = model.CarId,
                CompanyId = model.CompanyId,
                StartDate = pickUpDate,
                ReturnDate = returnDate,
                PickUpLocationId = pickupLocationId,
                DropOffLocationId = model.LocationId,
                Price = totalPrice,
                OrderStatus = OrderStatus.Active
            };

            var rentCar = await this.carsService.RentCarAsync(pickUpDate, returnDate, model.CarId);

            if (rentCar)
            {
                return false;
            }

            await this.data.Rentals.AddAsync(order);
            await this.data.SaveChangesAsync();

            return true;
        }
    }
}
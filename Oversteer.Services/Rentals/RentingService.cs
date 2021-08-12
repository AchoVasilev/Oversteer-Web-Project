namespace Oversteer.Services.Rentals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

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
        private readonly IMapper mapper;

        public RentingService(
            ApplicationDbContext data,
            IClientsService clientsService,
            ILocationService locationService,
            ICarsService carsService, 
            IMapper mapper)
        {
            this.data = data;
            this.clientsService = clientsService;
            this.locationService = locationService;
            this.carsService = carsService;
            this.mapper = mapper;
        }

        public async Task<bool> CreateOrderAsync(CreateRentFormModel model, string userId)
        {
            var clientId = await this.clientsService.GetClientIdByUserId(userId);

            var pickupLocationId = await this.locationService.GetLocationIdByNameAsync(model.StartLocation);
            var dropOffLocationId = await this.locationService.GetLocationIdByNameAsync(model.ReturnLocation);

            if (clientId == 0 || pickupLocationId == 0 || dropOffLocationId == 0)
            {
                return false;
            }

            var pickUpDate = DateTime.Parse(model.StartDate);

            var returnDate = DateTime.Parse(model.EndDate);

            var isRented = await this.carsService.IsRentedAsync(pickUpDate, returnDate, model.CarId);

            if (isRented)
            {
                return false;
            }

            var days = (returnDate - pickUpDate).Days;
            var totalPrice = model.Price * days;

            var order = new Rental()
            {
                ClientId = clientId,
                CarId = model.CarId,
                CompanyId = model.CompanyId,
                StartDate = pickUpDate,
                ReturnDate = returnDate,
                PickUpLocationId = pickupLocationId,
                DropOffLocationId = dropOffLocationId,
                Price = totalPrice,
                OrderStatus = OrderStatus.Active
            };

            var rentCar = await this.carsService.RentCarAsync(pickUpDate, returnDate, model.CarId);

            if (!rentCar)
            {
                return false;
            }

            await this.data.Rentals.AddAsync(order);
            await this.data.SaveChangesAsync();

            return true;
        }

        public IEnumerable<RentsDto> GetAllUserRents(string userId)
        {
            var clientId = this.clientsService.GetClientIdByUserId(userId).GetAwaiter().GetResult();

            if (clientId == 0)
            {
                return new List<RentsDto>();
            }

            var rents = this.data.Rentals
                            .OrderByDescending(x => x.CreatedOn)
                            .ProjectTo<RentsDto>(this.mapper.ConfigurationProvider)
                            .ToList();

            return rents;
        }
    }
}
namespace Oversteer.Services.Rentals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Microsoft.EntityFrameworkCore;

    using Oversteer.Data;
    using Oversteer.Data.Models.Enumerations;
    using Oversteer.Data.Models.Rentals;
    using Oversteer.Services.Cars;
    using Oversteer.Services.Companies;
    using Oversteer.Web.ViewModels.Rents;

    public class RentingService : IRentingService
    {
        private readonly ApplicationDbContext data;
        private readonly ILocationService locationService;
        private readonly ICarsService carsService;
        private readonly IMapper mapper;

        public RentingService(
            ApplicationDbContext data,
            ILocationService locationService,
            ICarsService carsService,
            IMapper mapper)
        {
            this.data = data;
            this.locationService = locationService;
            this.carsService = carsService;
            this.mapper = mapper;
        }

        public async Task<bool> CreateOrderAsync(RentFormModel model, string userId)
        {
            var pickupLocationId = await this.locationService.GetLocationIdByNameAsync(model.StartLocation);
            var dropOffLocationId = await this.locationService.GetLocationIdByNameAsync(model.ReturnLocation);

            if (userId == null || pickupLocationId == 0 || dropOffLocationId == 0)
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
                UserId = userId,
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
            if (userId == null)
            {
                return new List<RentsDto>();
            }

            var rents = this.data.Rentals
                            .Where(x => !x.IsDeleted)
                            .OrderByDescending(x => x.CreatedOn)
                            .ProjectTo<RentsDto>(this.mapper.ConfigurationProvider)
                            .ToList();

            return rents;
        }

        public async Task<RentDetailsModel> GetDetailsAsync(string rentId)
            => await this.data.Rentals
                            .Where(x => x.Id == rentId)
                            .ProjectTo<RentDetailsModel>(this.mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync();

        public ICollection<RentsDto> GetAllCompanyRents() 
            => this.data.Rentals
                            .Where(x => !x.IsDeleted)
                            .OrderByDescending(x => x.CreatedOn)
                            .ProjectTo<RentsDto>(this.mapper.ConfigurationProvider)
                            .ToList();

        public async Task<bool> CancelAsync(string rentId)
        {
            var rent = await this.GetRentByIdAsync(rentId);

            if (rent == null)
            {
                return false;
            }

            rent.OrderStatus = OrderStatus.Canceled;

            this.CancelRentDays(rent);

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> FinishAsync(string rentId)
        {
            var rent = await this.GetRentByIdAsync(rentId);

            if (rent is null)
            {
                return false;
            }

            var isLocationChanged = await this.carsService.ChangeLocationAsync(rent.CarId, rent.DropOffLocationId);

            if (!isLocationChanged)
            {
                return false;
            }

            rent.OrderStatus = OrderStatus.Finished;
            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(string rentId)
        {
            var rent = await this.GetRentByIdAsync(rentId);

            if (rent is null)
            {
                return false;
            }

            if (rent.OrderStatus != OrderStatus.Canceled)
            {
                this.CancelRentDays(rent);
            }

            rent.IsDeleted = true;
            rent.DeletedOn = DateTime.UtcNow;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<Rental> GetRentByIdAsync(string id)
        {
            var rent = await this.data.Rentals
                .Where(x => x.Id == id && !x.IsDeleted)
                .FirstOrDefaultAsync();

            return rent;
        }

        public async Task<bool> EditRentAsync(string id, string clientFirstName, string clientLastName, string clientEmail, decimal price)
        {
            var rent = await this.GetRentByIdAsync(id);

            if (rent is null)
            {
                return false;
            }

            rent.User.FirstName = clientFirstName;
            rent.User.LastName = clientLastName;
            rent.User.Email = clientEmail;
            rent.Price = price;

            await this.data.SaveChangesAsync();

            return true;
        }

        private void CancelRentDays(Rental order)
        {
            for (var dt = order.StartDate; dt <= order.ReturnDate; dt = dt.AddDays(1))
            {
                var rentDay = order.Car.RentDays.FirstOrDefault(x => x.RentDate.Date == dt);
                this.data.CarRentDays.Remove(rentDay);
            }
        }
    }
}
﻿namespace Oversteer.Web.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Oversteer.Models.Add;
    using Oversteer.Models.Cars;
    using Oversteer.Models.Others;
    using Oversteer.Models.Users;
    using Oversteer.Web.Data.Cars;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BankCard> BankCards { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<CompanyService> CompanyServices { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<CountryCode> CountryCodes { get; set; }

        public DbSet<ZipCode> ZipCodes { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<CarBrand> CarBrands { get; set; }

        public DbSet<Car> Cars { get; set; }

       // public DbSet<CarImage> CarImages { get; set; }

        public DbSet<CarType> CarTypes { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Fuel> Fuels { get; set; }

        public DbSet<CarAdd> CarAdds { get; set; }

        public DbSet<Destination> Destinations { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Rental> Rentals { get; set; }

        public DbSet<Transmission> Transmissions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.HasOne(x => x.ZipCode)
                    .WithMany(x => x.Users)
                    .HasForeignKey(x => x.ZipCodeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);

                entity.HasOne(x => x.CountryCode)
                    .WithMany(x => x.Users)
                    .HasForeignKey(x => x.CountryCodeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);

                entity.HasOne(x => x.Country)
                    .WithMany(x => x.ApplicationUsers)
                    .HasForeignKey(x => x.CountryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.City)
                    .WithMany(x => x.Users)
                    .HasForeignKey(x => x.CityId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Country>()
               .HasOne(x => x.CountryCode)
               .WithMany(x => x.Countries)
               .HasForeignKey(x => x.CountryCodeId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Destination>(entity =>
               {
                   entity.HasOne(x => x.Country)
                       .WithMany(x => x.Destinations)
                       .HasForeignKey(x => x.CountryId)
                       .OnDelete(DeleteBehavior.Restrict);

                   entity.HasOne(x => x.City)
                       .WithMany(x => x.Destinations)
                       .HasForeignKey(x => x.CityId)
                       .OnDelete(DeleteBehavior.Restrict);
               });

            builder.Entity<Rental>(entity =>
            {
                entity.HasOne(x => x.Place)
                    .WithMany(x => x.Rentals)
                    .HasForeignKey(x => x.PlaceId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Car>(entity =>
            {
                entity.HasOne(x => x.Fuel)
                    .WithMany(x => x.Cars)
                    .HasForeignKey(x => x.FuelId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Color)
                    .WithMany(x => x.Cars)
                    .HasForeignKey(x => x.ColorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Transmission)
                    .WithMany(x => x.Cars)
                    .HasForeignKey(x => x.TransmissionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(builder);
        }
    }
}

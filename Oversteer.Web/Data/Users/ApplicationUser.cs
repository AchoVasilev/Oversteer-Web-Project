﻿namespace Oversteer.Models.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Identity;
    using Oversteer.Models.Others;

    public class ApplicationUser : IdentityUser
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        [ForeignKey(nameof(CountryCode))]
        public int CountryCodeId { get; set; }

        public virtual CountryCode CountryCode { get; set; }

        [ForeignKey(nameof(City))]
        public int CityId { get; set; }

        public virtual City City { get; set; }

        [ForeignKey(nameof(ZipCode))]
        public int ZipCodeId { get; set; }

        public virtual ZipCode ZipCode { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();

        public virtual ICollection<Client> Clients { get; set; } = new HashSet<Client>();

        public virtual ICollection<Company> Companies { get; set; } = new HashSet<Company>();
    }
}
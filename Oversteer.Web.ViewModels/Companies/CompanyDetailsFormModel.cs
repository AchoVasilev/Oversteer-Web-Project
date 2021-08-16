namespace Oversteer.Web.ViewModels.Companies
{
    using System.Collections.Generic;

    using Oversteer.Web.ViewModels.Feedbacks;

    public class CompanyDetailsFormModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string PhoneNumber { get; init; }

        public string Description { get; init; }

        public int CarsCount { get; init; }

        public int ClientsCount { get; set; }

        public virtual ICollection<FeedbackViewModel> Feedbacks { get; init; }

        public virtual ICollection<CompanyServiceFormModel> CompanyServices { get; init; }

        public virtual ICollection<CompanyLocationFormModel> Locations { get; init; }
    }
}

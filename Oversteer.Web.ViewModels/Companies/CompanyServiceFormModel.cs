namespace Oversteer.Web.ViewModels.Companies
{
    using System.ComponentModel.DataAnnotations;

    using static Oversteer.Data.Common.Constants.DataConstants.Companies;

    public class CompanyServiceFormModel
    {
        public int Id { get; init; }

        [Required]
        [StringLength(ServiceNameMaxLength, MinimumLength = ServiceNameMinLength)]
        public string Name { get; init; }
    }
}

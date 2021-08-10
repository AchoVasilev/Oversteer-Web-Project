namespace Oversteer.Web.ViewModels.Companies
{
    using System.ComponentModel.DataAnnotations;

    using static Oversteer.Data.Common.Constants.DataConstants.Companies;

    public class CreateCompanyServiceFormModel
    {
        [Required]
        [StringLength(ServiceNameMaxLength, MinimumLength = ServiceNameMinLength)]
        public string Name { get; set; }
    }
}

namespace Oversteer.Web.ViewModels.Cities
{
    using System.ComponentModel.DataAnnotations;

    using static Oversteer.Data.Common.Constants.DataConstants.Cities;

    public class ZipCodeFormModel
    {
        [Range(ZipCodeMinLength, ZipCodeMaxLength)]
        public int Code { get; init; }
    }
}
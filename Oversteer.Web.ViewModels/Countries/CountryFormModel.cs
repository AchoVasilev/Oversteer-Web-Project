namespace Oversteer.Web.ViewModels.Countries
{
    using Oversteer.Web.ViewModels.Cities;

    public class CountryFormModel
    {
        public int Id { get; init; }

        public CityFormModel City { get; init; }
    }
}

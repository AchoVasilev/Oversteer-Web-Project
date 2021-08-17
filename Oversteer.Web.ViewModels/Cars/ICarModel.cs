namespace Oversteer.Web.ViewModels.Cars
{
    public interface ICarModel
    {
        public string BrandName { get; }

        public string ModelName { get; }

        public int ModelYear { get; }
    }
}

namespace Oversteer.Web.Dto
{
    using Newtonsoft.Json;

    public class CarBrandDto
    {
        [JsonProperty("value")]
        public string Name { get; set; }

        public string Title { get; set; }

        public CarModelDto[] Models { get; set; }
    }
}

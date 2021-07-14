namespace Oversteer.Web.Dto
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class CarBrandDto
    {
        [JsonProperty("value")]
        public string Name { get; set; }

        public string Title { get; set; }

        public ICollection<CarModelDto> Models { get; set; }
    }
}

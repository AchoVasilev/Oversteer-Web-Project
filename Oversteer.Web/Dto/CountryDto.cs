namespace Oversteer.Web.Dto
{
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    public class CountryDto
    {
        [Required]
        [JsonProperty("COUNTRY")]
        public string Name { get; init; }
    }
}

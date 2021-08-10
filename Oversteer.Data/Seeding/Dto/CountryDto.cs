namespace Oversteer.Data.Seeding.Dto
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

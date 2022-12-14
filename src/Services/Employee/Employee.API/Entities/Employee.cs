using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Employee.API.Entities
{
    public class EmployeeModel
    {

        [JsonProperty("id")]
        public string? Id { get; set; }
        [Required]
        [JsonProperty("userId")]
        public string? UserId { get; set; }
        [Required]
        [JsonProperty("region")]
        public string? Region { get; set; }
        [JsonProperty("level")]
        public int Level { get; set; } = 1;
        [JsonProperty("title")]
        public string? Title { get; set; }
        [JsonProperty("division")]
        public string? Division { get; set; }
        [JsonProperty("position")]
        public string? Position { get; set; }
        [JsonProperty("designation")]
        public string? Designation { get; set; }
    }
}

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
        public string UserId { get; set; } = null!;
        [JsonProperty("contactNumber")]
        public string? ContactNumber { get; set; } 
        [JsonProperty("email")]
        public string? Email { get; set; } 
        [Required]
        [JsonProperty("branch")]
        public string? Branch { get; set; }
        [JsonProperty("city")]
        public string? City { get; set; }
        [JsonProperty("province")]
        public string? Province { get; set; }
        [JsonProperty("region")]
        public string? Region { get; set; }
        [JsonProperty("title")]
        public string? Title { get; set; }
        [JsonProperty("department")]
        public string? Department { get; set; }
        [JsonProperty("position")]
        public string? Position { get; set; }
        [JsonProperty("designation")]
        public string? Designation { get; set; }
        [JsonProperty("dateHired")]
        public DateTime? DateHired { get; set; }
        [JsonProperty("sss")]
        public string? SSS { get; set; }
        [JsonProperty("phic")]
        public string? PHIC { get; set; }
        [JsonProperty("pagibig")]
        public string? PAGIBIG { get; set; }
        [JsonProperty("tin")]
        public string? TIN { get; set; }
        [JsonProperty("bankName")]
        public string? BankName { get; set; }
        [JsonProperty("accountName")]
        public string? AccountName { get; set; }
		[JsonProperty("accountNumber")]
        public string? AccountNumber { get; set; }
    }
}

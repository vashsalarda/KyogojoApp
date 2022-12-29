using Newtonsoft.Json;

namespace Employee.API.DTOs
{
    public class CreateUserRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; } = null!;

        [JsonProperty("firstName")]
        public string FirstName { get; set; } = null!;

        [JsonProperty("lastName")]
        public string LastName { get; set; } = null!;

        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; } = null!;

        [JsonProperty("country")]
        public string Country { get; set; } = null!;

        [JsonProperty("province")]
        public string Province { get; set; } = null!;

        [JsonProperty("city")]
        public string City { get; set; } = null!;

        [JsonProperty("role")]
        public string[] Role { set; get; } = null!;

        [JsonProperty("designation")]
        public string Designation { get; set; } = null!;

        [JsonProperty("password")]
        public string Password { get; set; } = null!;
    }

    public class UpdateUserRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; } = null!;

        [JsonProperty("firstName")]
        public string FirstName { get; set; } = null!;

        [JsonProperty("lastName")]
        public string LastName { get; set; } = null!;

        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; } = null!;

        [JsonProperty("country")]
        public string Country { get; set; } = null!;

        [JsonProperty("province")]
        public string Province { get; set; } = null!;

        [JsonProperty("city")]
        public string City { get; set; } = null!;

        [JsonProperty("role")]
        public string[] Role { set; get; } = null!;

        [JsonProperty("designation")]
        public string Designation { get; set; } = null!;

    }

    public class UserResponse
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; } = null!;

        [JsonProperty("firstName")]
        public string FirstName { get; set; } = null!;

        [JsonProperty("lastName")]
        public string LastName { get; set; } = null!;

        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; } = null!;

        [JsonProperty("country")]
        public string Country { get; set; } = null!;

        [JsonProperty("province")]
        public string Province { get; set; } = null!;

        [JsonProperty("city")]
        public string City { get; set; } = null!;

        [JsonProperty("role")]
        public string[] Role { set; get; } = null!;

        [JsonProperty("designation")]
        public string Designation { get; set; } = null!;

    }

}
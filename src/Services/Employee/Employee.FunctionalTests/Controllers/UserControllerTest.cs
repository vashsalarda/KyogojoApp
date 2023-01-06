using Employee.API.Entities;
using System.Text;

namespace Employee.FunctionalTests.Controllers
{
    public class UserControllerTest : BaseControllerTests
    {
        public UserControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetUsers_ReturnsAllRecords()
        {
            int expectedCount = 10;
            var client = this.GetNewClient();
            var response = await client.GetAsync("/api/v1/users");
            response.EnsureSuccessStatusCode();

            var strResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<EmployeeModel>>(strResponse)?.ToList();

            var statusCode = response.StatusCode.ToString();
            Assert.Equal("OK", statusCode);
            Assert.True(result?.Count == expectedCount);
        }

        [Fact]
        public async Task GetUserById_UserExists_ReturnsCorrectUser()
        {
            var id = "1003";
            var client = this.GetNewClient();
            var response = await client.GetAsync($"/api/v1/users/{id}");
            response.EnsureSuccessStatusCode();

            var strResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<User>(strResponse);
            var statusCode = response.StatusCode.ToString();

            Assert.Equal("OK", statusCode);
            Assert.Equal(id, result?.Id);
            Assert.NotNull(result?.Id);
            Assert.NotNull(result?.Email);
            Assert.IsType<string>(result.Id);
            Assert.IsType<string>(result.Email);
            Assert.IsType<string>(result.LastName);
            Assert.IsType<string>(result.FirstName);
            Assert.IsType<string>(result.MobileNumber);
            Assert.IsType<string>(result.Country);
            Assert.IsType<string>(result.State);
            Assert.IsType<string>(result.City);
            Assert.IsType<string[]>(result.Role);
            Assert.IsType<string>(result.Designation);
            Assert.IsType<string>(result.Password);
        }

        [Theory]
        [InlineData("1000")]
        [InlineData("1011")]
        public async Task GetUserById_UserDoesntExist_ReturnsNotFound(string id)
        {
            var client = this.GetNewClient();
            var response = await client.GetAsync($"/api/v1/users/{id}");

            var statusCode = response.StatusCode.ToString();

            Assert.Equal("NotFound", statusCode);
        }

        [Fact]
        public async Task PostUser_ReturnsCreatedUser()
        {
            var client = this.GetNewClient();

            // Create employee
            var request = new User
            {
                Email = "vashful.v3@gmail.com",
                MobileNumber = "09271478417",
                LastName = "Salarda",
                FirstName = "Vash",
                City = "Malaybalay",
                State = "Buk",
                Country = "PH",
                Role = new[] { "Admin" },
                Designation = "Problem",
                Password = "Reversed123"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response1 = await client.PostAsync("/api/v1/users", stringContent);
            response1.EnsureSuccessStatusCode();

            var stringResponse1 = await response1.Content.ReadAsStringAsync();
            var newUser = JsonConvert.DeserializeObject<User>(stringResponse1);
            var statusCode1 = response1.StatusCode.ToString();

            Assert.Equal("Created", statusCode1);

            // Get created employee

            var response2 = await client.GetAsync($"/api/v1/users/{newUser?.Id}");
            response2.EnsureSuccessStatusCode();

            var stringResponse2 = await response2.Content.ReadAsStringAsync();
            var result2 = JsonConvert.DeserializeObject<User>(stringResponse2);
            var statusCode2 = response2.StatusCode.ToString();

            Assert.Equal("Created", statusCode1);
            Assert.NotNull(result2?.Id);
            Assert.Equal(newUser?.Id, result2?.Id);
            Assert.Equal(newUser?.Email, result2?.Email);
            Assert.Equal(newUser?.FirstName, result2?.FirstName);
            Assert.Equal(newUser?.LastName, result2?.LastName);
            Assert.Equal(newUser?.MobileNumber, result2?.MobileNumber);
            Assert.Equal(newUser?.Country, result2?.Country);
            Assert.Equal(newUser?.State, result2?.State);
            Assert.Equal(newUser?.City, result2?.City);
            Assert.Equal(newUser?.Role, result2?.Role);
            Assert.Equal(newUser?.Designation, result2?.Designation);
            Assert.Equal(newUser?.Password, result2?.Password);
        }

        [Fact]
        public async Task PatchUser_ReturnsUpdatedUser()
        {
            var client = this.GetNewClient();

            // Update user

            var id = "1005";
            var request = new User
            {
                Email = "admin.v2@moonshot.com",
                FirstName = "JM",
                LastName = "Grills",
                MobileNumber = "0927-147-8417",
                City = "Malaybalay",
                State = "MisOr",
                Country = "PHI",
                Role = new[] { "Admin" },
                Designation = "Problem"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response1 = await client.PatchAsync($"/api/v1/users/{id}", stringContent);
            response1.EnsureSuccessStatusCode();

            var statusCode1 = response1.StatusCode.ToString();
            Assert.Equal("NoContent", statusCode1);

            // Get updated user

            var response2 = await client.GetAsync($"/api/v1/users/{id}");
            response2.EnsureSuccessStatusCode();

            var stringResponse2 = await response2.Content.ReadAsStringAsync();
            var result2 = JsonConvert.DeserializeObject<User>(stringResponse2);
            var statusCode2 = response2.StatusCode.ToString();

            Assert.Equal("OK", statusCode2);
            Assert.Equal(id, result2?.Id);
            Assert.Equal(request.Email, result2?.Email);
            Assert.Equal(request.FirstName, result2?.FirstName);
            Assert.Equal(request.LastName, result2?.LastName);
            Assert.Equal(request.MobileNumber, result2?.MobileNumber);
            Assert.Equal(request.State, result2?.State);
            Assert.Equal(request.City, result2?.City);
            Assert.Equal(request.Country, result2?.Country);
            Assert.Equal(request.Role, result2?.Role);
            Assert.Equal(request.Designation, result2?.Designation);
        }

        [Fact]
        public async Task DeleteUserById_ReturnsNoContent()
        {
            var client = this.GetNewClient();
            var id = "1010";

            // Delete user

            var response1 = await client.DeleteAsync($"/api/v1/users/{id}");

            var statusCode1 = response1.StatusCode.ToString();

            Assert.Equal("NoContent", statusCode1);

            // Get deleted user

            var response2 = await client.GetAsync($"/api/v1/users/{id}");

            var statusCode2 = response2.StatusCode.ToString();

            Assert.Equal("NotFound", statusCode2);
        }
    }
}

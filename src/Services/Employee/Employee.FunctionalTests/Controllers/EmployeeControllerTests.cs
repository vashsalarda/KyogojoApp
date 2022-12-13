using Newtonsoft.Json;
using Employee.API.Entities;
using Employee.FunctionalTests.Models;
using System.Text;

namespace Employee.FunctionalTests.Controllers
{
    public class EmployeeControllerTests : BaseControllerTests
    {
        public EmployeeControllerTests(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetEmployees_ReturnsAllRecords()
        {
            var client = this.GetNewClient();
            var response = await client.GetAsync("/api/v1/employees");
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<EmployeeModel>>(stringResponse)?.ToList();
            var statusCode = response.StatusCode.ToString();
            Assert.Equal("OK", statusCode);
            Assert.True(result?.Count == 10);
        }

        [Fact]
        public async Task GetEmployeeById_EmployeeExists_ReturnsCorrectEmployee()
        {
            var id = "1001";
            var client = this.GetNewClient();
            var response = await client.GetAsync($"/api/v1/employees/{id}");
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<EmployeeModel>(stringResponse);
            var statusCode = response.StatusCode.ToString();

            Assert.Equal("OK", statusCode);
            Assert.Equal(id, result?.Id);
            Assert.NotNull(result?.Title);
            Assert.IsType<string>(result.Region);
            Assert.IsType<string>(result.Position);
        }

        [Theory]
        [InlineData("1000")]
        [InlineData("1011")]
        public async Task GetEmployeeById_EmployeeDoesntExist_ReturnsNotFound(string id)
        {
            var client = this.GetNewClient();
            var response = await client.GetAsync($"/api/v1/employees/{id}");

            var statusCode = response.StatusCode.ToString();

            Assert.Equal("NotFound", statusCode);
        }

        [Fact]
        public async Task PostEmployee_ReturnsCreatedEmployee()
        {
            var client = this.GetNewClient();

            // Create employee

            var request = new EmployeeModel
            {
                UserId = "1012",
                Region = "X",
                Title = "Hello"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response1 = await client.PostAsync("/api/v1/employees", stringContent);
            response1.EnsureSuccessStatusCode();

            var stringResponse1 = await response1.Content.ReadAsStringAsync();
            var newEmployee = JsonConvert.DeserializeObject<EmployeeModel>(stringResponse1);
            var statusCode1 = response1.StatusCode.ToString();

            Assert.Equal("Created", statusCode1);

            // Get created employee

            var response2 = await client.GetAsync($"/api/v1/employees/{newEmployee?.Id}");
            response2.EnsureSuccessStatusCode();

            var stringResponse2 = await response2.Content.ReadAsStringAsync();
            var result2 = JsonConvert.DeserializeObject<EmployeeModel>(stringResponse2);
            var statusCode2 = response2.StatusCode.ToString();

            Assert.Equal("OK", statusCode2);
            Assert.Equal(newEmployee?.Id, result2?.Id);
            Assert.Equal(newEmployee?.UserId, result2?.UserId);
            Assert.Equal(newEmployee?.Region, result2?.Region);
            Assert.Equal(newEmployee?.Title, result2?.Title);
        }

        [Fact]
        public async Task PostEmployee_InvalidData_ReturnsErrors()
        {
            var client = this.GetNewClient();

            // Create employee

            var request = new EmployeeModel
            {
                // UserId = "1001",
                // Region = "X",
                Title = "Hi"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/v1/employees", stringContent);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var badRequest = JsonConvert.DeserializeObject<BadRequestModel>(stringResponse);
            var statusCode = response.StatusCode.ToString();

            Assert.Equal("BadRequest", statusCode);
            Assert.NotNull(badRequest?.Title);
            Assert.NotNull(badRequest?.Errors);
            Assert.Equal(2, badRequest.Errors.Count);
            Assert.Contains(badRequest.Errors.Keys, k => k == "UserId");
            Assert.Contains(badRequest.Errors.Keys, k => k == "Region");
        }


        [Fact]
        public async Task PatchEmployee_ReturnsUpdatedEmployee()
        {
            var client = this.GetNewClient();

            // Update employee

            var id = "1001";
            var request = new EmployeeModel
            {
                UserId = "1002",
                Region = "X",
                Title = "Hi"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response1 = await client.PatchAsync($"/api/v1/employees/{id}", stringContent);
            response1.EnsureSuccessStatusCode();

            var stringResponse1 = await response1.Content.ReadAsStringAsync();
            var statusCode1 = response1.StatusCode.ToString();
            Assert.Equal("NoContent", statusCode1);

            // Get updated employee

            var response2 = await client.GetAsync($"/api/v1/employees/{id}");
            response2.EnsureSuccessStatusCode();

            var stringResponse2 = await response2.Content.ReadAsStringAsync();
            var result2 = JsonConvert.DeserializeObject<EmployeeModel>(stringResponse2);
            var statusCode2 = response2.StatusCode.ToString();

            Assert.Equal("OK", statusCode2);
            Assert.Equal(id, result2?.Id);
            Assert.Equal(request.UserId, result2?.UserId);
            Assert.Equal(request.Region, result2?.Region);
            Assert.Equal(request.Title, result2?.Title);
        }

        [Fact]
        public async Task DeleteEmployeeById_ReturnsNoContent()
        {
            var client = this.GetNewClient();
            var id = "1002";

            // Delete employee

            var response1 = await client.DeleteAsync($"/api/v1/employees/{id}");

            var statusCode1 = response1.StatusCode.ToString();

            Assert.Equal("NoContent", statusCode1);

            // Get deleted employee

            var response2 = await client.GetAsync($"/api/v1/employees/{id}");

            var statusCode2 = response2.StatusCode.ToString();

            Assert.Equal("NotFound", statusCode2);
        }
    }
}
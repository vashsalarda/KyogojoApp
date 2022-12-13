using Dapper;
using Employee.API.Entities;
using Employee.API.Repositories.Interfaces;
using Npgsql;

namespace Employee.API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _configuration;

        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<IEnumerable<EmployeeModel>> GetEmployees()
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var customers = await connection.QueryAsync<EmployeeModel>
                ("SELECT * FROM Employees");

            return customers;
        }

        public async Task<EmployeeModel?> GetEmployee(string id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var customer = await connection.QueryFirstOrDefaultAsync<EmployeeModel>
                ("SELECT * FROM Employees WHERE Id = @Id", new { Id = id });

            return customer;
        }

        public async Task<bool> CreateEmployee(EmployeeModel customer)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected =
                await connection.ExecuteAsync
                    ("INSERT INTO Employees (Id, UserId, Region, Level, Title, Division, Position, Designation) VALUES (@Id, @UserId, @Region, @Level, @Title, @Division, @Position, @Designation)",
                        new { 
                            Id = customer.Id,
                            UserId = customer.UserId,
                            Region = customer.Region,
                            Level = customer.Level,
                            Title = customer.Title,
                            Division = customer.Division,
                            Position = customer.Position,
                            Designation = customer.Designation
                        });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> UpdateEmployee(EmployeeModel employee, string id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                ("UPDATE Employees SET UserId=@UserId, Region=@Region, Level=@Level, Title=@Title, Position=@Position, Designation=@Designation WHERE Id=@Id",
                    new {
                        Id = id,
                        UserId = employee.UserId,
                        Region = employee.Region,
                        Level = employee.Level,
                        Title = employee.Title,
                        Division = employee.Division,
                        Position = employee.Position,
                        Designation = employee.Designation
                    });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> DeleteEmployee(string id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("DELETE FROM Employees WHERE Id = @id",
                new { Id = id });

            if (affected == 0)
                return false;

            return true;
        }
    }
}

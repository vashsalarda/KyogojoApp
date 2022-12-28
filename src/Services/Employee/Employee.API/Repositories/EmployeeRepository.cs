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
                    ("INSERT INTO Employees (Id, UserId, Branch, Province, Region, Title, Department, Position, Designation, DateHired) VALUES (@Id, @UserId, @Branch, @Province, @Region, @Title, @Department, @Position, @Designation, @DateHired)",
                        new { 
                            Id = customer.Id,
                            UserId = customer.UserId,
                            Branch = customer.Branch,
                            Province = customer.Province,
                            Region = customer.Region,
                            Title = customer.Title,
                            Department = customer.Department,
                            Position = customer.Position,
                            Designation = customer.Designation,
                            DateHired = customer.DateHired
                        });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> UpdateEmployee(EmployeeModel employee, string id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                ("UPDATE Employees SET UserId=@UserId, Branch=@Branch, Province=@Province, Region=@Region, Title=@Title, Department=@Department, Position=@Position, Designation=@Designation WHERE Id=@Id",
                    new {
                        Id = id,
                        UserId = employee.UserId,
                        Branch = employee.Branch,
                        City = employee.City,
                        Province = employee.Province,
                        Region = employee.Region,
                        Title = employee.Title,
                        Department = employee.Department,
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

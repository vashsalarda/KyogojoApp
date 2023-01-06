using Bogus;
using Employee.API.Entities;
using Employee.API.Data;
using Employee.API.Services;
using System.Text.RegularExpressions;

namespace Employee.SharedDatabaseSetup
{
    public static class DatabaseSetup
    {

        private static readonly Regex sWhitespace = new(@"\s+");
        public static void SeedData(EmployeeContext context)
        {
            context.Employees.RemoveRange(context.Employees);
            context.Users.RemoveRange(context.Users);

            var employeeStartId = 1001;
            var userIdStartId = 1001;
            var userStartId = 1001;
            var userEmailStartId = 1001;
            var fakeEmployees = new Faker<EmployeeModel>()
                .RuleFor(o => o.Id, f => (employeeStartId++).ToString())
                .RuleFor(o => o.UserId, f => (userIdStartId++).ToString())
                .RuleFor(o => o.Branch, f => "Main")
                .RuleFor(o => o.City, f => "Malaybalay")
                .RuleFor(o => o.State, f => "BUK")
                .RuleFor(o => o.Region, f => "X")
                .RuleFor(o => o.Title, f => "Engr.")
                .RuleFor(o => o.Department, f => "Admin")
                .RuleFor(o => o.Position, f => "Head")
                .RuleFor(o => o.Designation, f => "Ache")
                .RuleFor(o => o.DateHired, f => DateTime.Now.ToUniversalTime());

            var employees = fakeEmployees.Generate(10);

            string password = Hashing.HashPassword(sWhitespace.Replace("Reversed123", ""));
            var role = new[]
            {
                "Admin"
            };

            string email = "admin";

            var fakeUsers = new Faker<User>()
                .RuleFor(o => o.Id, f => (userStartId++).ToString())
                .RuleFor(o => o.Email, f => (email + "_" + userEmailStartId++ + "@moonshot.com").ToString())
                .RuleFor(o => o.FirstName, f => "Admin")
                .RuleFor(o => o.LastName, f => "Kyogojo")
                .RuleFor(o => o.MobileNumber, f => "09566725103")
                .RuleFor(o => o.Country, f => "PH")
                .RuleFor(o => o.City, f => "Malaybalay")
                .RuleFor(o => o.State, f => "Buk")
                .RuleFor(o => o.Role, f => role)
                .RuleFor(o => o.Designation, f => "Admin")
                .RuleFor(o => o.Password, f => password);

            var users = fakeUsers.Generate(10);

            context.AddRange(employees);
            context.AddRange(users);

            context.SaveChanges();
        }
    }
}
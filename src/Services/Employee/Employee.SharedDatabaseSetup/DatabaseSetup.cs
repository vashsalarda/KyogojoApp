﻿using Bogus;
using Employee.API.Entities;
using Employee.API.Data;

namespace Employee.SharedDatabaseSetup
{
    public static class DatabaseSetup
    {
        public static void SeedData(EmployeeContext context)
        {
            context.Employees.RemoveRange(context.Employees);

            var employeeIdStart = 1001;
            var fakeEmployees = new Faker<EmployeeModel>()
                .RuleFor(o => o.Id, f => (employeeIdStart++).ToString())
                .RuleFor(o => o.UserId, f => "UserId")
                .RuleFor(o => o.Region, f => "X")
                .RuleFor(o => o.Title, f => "Hello")
                .RuleFor(o => o.Division, f => "Bukidnon")
                .RuleFor(o => o.Position, f => "Head")
                .RuleFor(o => o.Designation, f => "Ache");

            var employees = fakeEmployees.Generate(10);

            context.AddRange(employees);

            context.SaveChanges();
        }
    }
}
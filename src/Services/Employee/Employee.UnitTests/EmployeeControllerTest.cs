using Employee.API.Controllers;
using Employee.API.Entities;
using Employee.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Employee.UnitTests;

public class EmployeeControllerTest
{

    private readonly Mock<IEmployeeRepository> _mockRepository;
    private readonly Mock<ILogger<EmployeeController>> _mockLogger;
    private readonly List<EmployeeModel> _mockList;

    public EmployeeControllerTest()
    {
        _mockRepository = new Mock<IEmployeeRepository>();
        _mockLogger = new Mock<ILogger<EmployeeController>>();
        _mockList = new()
        {
            new EmployeeModel()
            {
                Id = "1001",
                UserId = "1001",
                Region = "X",
                Level = 1,
                Title = "Admin",
                Division = "Internal",
                Position = "Admin",
                Designation = "Admin"
            },
            new EmployeeModel()
            {
                Id = "1002",
                UserId = "1002",
                Region = "X",
                Level = 1,
                Title = "Admin",
                Division = "Internal",
                Position = "Admin",
                Designation = "Admin"
            },
        };
    }

    [Fact]
    public async void Get_Employees_Success()
    {
        //Arrange
        _mockRepository.Setup(repo => repo.GetEmployees()).ReturnsAsync(_mockList);

        //Act
        var expectedTotalItems = 2;
        var controller = new EmployeeController(_mockRepository.Object, _mockLogger.Object);
        var result = await controller.GetEmployees();
        var resultType = (OkObjectResult?)result?.Result;
        var resultList = resultType?.Value as List<EmployeeModel>;
        int totalRes = resultList?.Count ?? 0;
        var StatusCode = resultType?.StatusCode;
        var employeeList = resultType?.Value;

        //Assert
        Assert.NotNull(result);
        Assert.Equal(StatusCode, (int)System.Net.HttpStatusCode.OK);
        Assert.IsType<List<EmployeeModel>>(employeeList);
        Assert.Equal(expectedTotalItems, totalRes);
    }

    [Fact]
    public async void Get_Employee_Success()
    {
        // Arrange
        string id = "1001";
        _mockRepository.Setup(repo => repo.GetEmployee(It.IsAny<string>()))
            .Returns(Task.FromResult(_mockList.Where(i => i.UserId == id)
                .AsEnumerable()
                .FirstOrDefault()));

        // Act
        var controller = new EmployeeController(_mockRepository.Object, _mockLogger.Object);
        var result = await controller.GetEmployee(id);
        var resultType = result.Result;
        var resultObj = result.Result as OkObjectResult;
        var resultItem = resultObj?.Value as EmployeeModel;
        var Id = resultItem?.UserId?.ToString();
        var StatusCode = resultObj?.StatusCode;

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(resultItem);
        Assert.IsAssignableFrom<EmployeeModel>(resultItem);
        Assert.Equal(id, Id);
        Assert.Equal((int)System.Net.HttpStatusCode.OK, StatusCode);
    }

    [Fact]
    public async void Create_Employee_Success()
    {
        // Arrange
        EmployeeModel user = new()
        {
            Id = "1003",
            UserId = "1003",
            Region = "X",
            Level = 1,
            Title = "Admin",
            Division = "Internal",
            Position = "Admin",
            Designation = "Admin"
        };
        _mockRepository.Setup(repo => repo.CreateEmployee(It.IsAny<EmployeeModel>())).Returns(Task.FromResult(true));

        // Act
        var controller = new EmployeeController(_mockRepository.Object, _mockLogger.Object);
        var result = await controller.CreateEmployee(user);
        var resultValue = result.Result as CreatedAtActionResult;
        var resultObj = resultValue?.Value as EmployeeModel;
        var StatusCode = resultValue?.StatusCode;

        // Assert
        Assert.NotNull(result);
        var employeeObj = Assert.IsType<EmployeeModel>(resultObj);
        Assert.IsType<string>(employeeObj.Id);
        Assert.Equal("1003", employeeObj.UserId);
        Assert.Equal("1003", employeeObj?.UserId);
        Assert.Equal("X", employeeObj?.Region);
        Assert.Equal(1, employeeObj?.Level);
        Assert.Equal("Internal", employeeObj?.Division);
        Assert.Equal("Admin", employeeObj?.Position);
        Assert.Equal("Admin", employeeObj?.Designation);
        Assert.Equal((int)System.Net.HttpStatusCode.Created, StatusCode);
    }

    [Fact]
    public async void Update_Employee_Success()
    {
        // Arrange
        string id = "1003";
        EmployeeModel user = new()
        {
            Region = "X",
            Level = 1,
            Title = "Admin",
            Division = "Internal",
            Position = "Admin",
            Designation = "Admin"
        };
        _mockRepository.Setup(repo => repo.UpdateEmployee(It.IsAny<EmployeeModel>(), It.IsAny<string>())).Returns(Task.FromResult(true));

        // Act
        var controller = new EmployeeController(_mockRepository.Object, _mockLogger.Object);
        var result = await controller.UpdateEmployee(id, user);
        var resultValue = result as NoContentResult;
        var StatusCode = resultValue?.StatusCode;

        // Assert
        Assert.NotNull(result);
        Assert.Equal((int)System.Net.HttpStatusCode.NoContent, StatusCode);
    }

    [Fact]
    public async void Delete_Employee_Success()
    {
        // Arrange
        string id = "1003";
        _mockRepository.Setup(repo => repo.DeleteEmployee(It.IsAny<string>())).Returns(Task.FromResult(true));

        // Act
        var controller = new EmployeeController(_mockRepository.Object, _mockLogger.Object);
        var result = await controller.DeleteEmployee(id);
        var resultValue = result as NoContentResult;
        var StatusCode = resultValue?.StatusCode;

        // Assert
        Assert.NotNull(result);
        Assert.Equal((int)System.Net.HttpStatusCode.NoContent, StatusCode);
    }
}
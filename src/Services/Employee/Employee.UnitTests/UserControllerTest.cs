using Employee.API.Controllers;
using Employee.API.DTOs;
using Employee.API.Entities;
using Employee.API.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Employee.UnitTests;

public class UserControllerTest
{

    private readonly Mock<IUserRepository> _mockRepository;
    private readonly Mock<ILogger<UserController>> _mockLogger;
    private readonly List<User> _mockList;

    public UserControllerTest()
    {
        _mockRepository = new Mock<IUserRepository>();
        _mockLogger = new Mock<ILogger<UserController>>();
        _mockList = new()
        {
            new User()
            {
                Id = "1001",
                FirstName = "Vash",
                LastName = "Ful",
                Email = "vashful@pm-moonshot.com",
                MobileNumber = "8080",
                Country = "PH",
                Province = "BUK",
                City = "Malaybalay"
            },
            new User()
            {
                Id = "1002",
                FirstName = "Jo",
                LastName = "Test",
                Email = "jp.test@pm-moonshot.com",
                MobileNumber = "911",
                Country = "PH",
                Province = "BUK",
                City = "Malaybalay"
            },
            new User()
            {
                Id = "1003",
                FirstName = "Niño",
                LastName = "Paul",
                Email = "nino.paul@pm-moonshot.com",
                MobileNumber = "117",
                Country = "PH",
                Province = "BUK",
                City = "Kibawe"
            }
        };

    }

    [Fact]
    public async void Get_Users_Success()
    {
        //Arrange
        _mockRepository.Setup(repo => repo.GetUsers()).ReturnsAsync(_mockList);

        //Act
        var expectedTotalItems = 3;
        var controller = new UserController(_mockRepository.Object, _mockLogger.Object);
        var result = await controller.GetUsers();
        var resultType = (OkObjectResult?)result?.Result;
        var resultList = resultType?.Value as List<User>;
        int totalRes = resultList?.Count ?? 0;

        //Assert
        Assert.NotNull(result);
        Assert.IsType<List<User>>(resultType?.Value);
        Assert.Equal((result?.Result as OkObjectResult)?.StatusCode, (int)System.Net.HttpStatusCode.OK);
        Assert.Equal(expectedTotalItems, totalRes);
    }

    [Fact]
    public async void Get_User_Success()
    {
        // Arrange
        string id = "1001";
        _mockRepository.Setup(repo => repo.GetUser(It.IsAny<string>()))
            .Returns(Task.FromResult(_mockList.Where(i => i.Id == id)
                .AsEnumerable()
                .FirstOrDefault()));

        // Act
        var controller = new UserController(_mockRepository.Object, _mockLogger.Object);
        var result = await controller.GetUser(id);
        var resultType = result.Result;
        var resultObj = result.Result as OkObjectResult;
        var resultItem = resultObj?.Value as User;

        // Assert
        Assert.NotNull(resultItem);
        Assert.IsAssignableFrom<User>(resultItem);
        var Id = resultItem?.Id?.ToString();
        var StatusCode = resultObj?.StatusCode;
        Assert.Equal(StatusCode, (int)System.Net.HttpStatusCode.OK);
        Assert.Equal(id, Id);
    }

    [Fact]
    public async void Create_User_Success()
    {
        // Arrange
        User user = new()
        {
            FirstName = "JM",
            LastName = "Grills",
            Email = "jm.grills@pm-moonshot.com",
            MobileNumber = "4438",
            Country = "PH",
            Province = "MISOR",
            City = "CDO",
            Password = "DeleteTableUser"
        };
        _mockRepository.Setup(repo => repo.CreateUser(It.IsAny<User>())).Returns(Task.FromResult(true));

        // Act
        var controller = new UserController(_mockRepository.Object, _mockLogger.Object);
        var result = await controller.CreateUser(user);
        var resultValue = result.Result as CreatedAtActionResult;
        var resultObj = resultValue?.Value as User;
        var StatusCode = resultValue?.StatusCode;
    
        // Assert
        var customerObj = Assert.IsType<User>(resultObj);
        Assert.Equal(StatusCode, (int)System.Net.HttpStatusCode.Created);
        Assert.IsType<string>(customerObj?.Id);
        Assert.Equal("JM", customerObj?.FirstName);
        Assert.Equal("Grills", customerObj?.LastName);
        Assert.Equal("jm.grills@pm-moonshot.com", customerObj?.Email);
        Assert.Equal("4438", customerObj?.MobileNumber);
        Assert.Equal("PH", customerObj?.Country);
        Assert.Equal("MISOR", customerObj?.Province);
        Assert.Equal("CDO", customerObj?.City);
        
    }

    [Fact]
    public async void Update_User_Success()
    {
        // Arrange
        string id = "1003";
        UpdateUserRequest user = new()
        {
            FirstName = "JM",
            LastName = "Grills",
            Email = "jm.grills@pm-moonshot.com",
            MobileNumber = "4438",
            Country = "PH",
            Province = "MISOR",
            City = "CDO"
        };
        _mockRepository.Setup(repo => repo.UpdateUser(It.IsAny<User>(), It.IsAny<string>())).Returns(Task.FromResult(true));


        // Act
        var controller = new UserController(_mockRepository.Object, _mockLogger.Object);
        var result = await controller.UpdateUser(id, user);
        var resultValue = result as NoContentResult;
        var StatusCode = resultValue?.StatusCode;

        // Assert
        Assert.Equal(StatusCode, (int)System.Net.HttpStatusCode.NoContent);
    }

    [Fact]
    public async void Delete_User_Success()
    {
        // Arrange
        string id = "1003";
        _mockRepository.Setup(repo => repo.DeleteUser(It.IsAny<string>())).Returns(Task.FromResult(true));

        // Act
        var controller = new UserController(_mockRepository.Object, _mockLogger.Object);
        var result = await controller.DeleteUser(id);
        var resultValue = result as NoContentResult;
        var StatusCode = resultValue?.StatusCode;

        // Assert
        Assert.Equal(StatusCode, (int)System.Net.HttpStatusCode.NoContent);
    }
}
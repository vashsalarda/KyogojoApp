using Microsoft.AspNetCore.Mvc;
using Employee.API.Entities;
using System.Net;
using Employee.API.Repositories.Interfaces;

namespace Employee.API.Controllers;

[ApiController]
[Route("api/v1/employees")]
public class EmployeeController : ControllerBase
{

    private readonly IEmployeeRepository _repository;
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(IEmployeeRepository repository, ILogger<EmployeeController> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet(Name = "GetEmployees")]
    [ProducesResponseType(typeof(IEnumerable<EmployeeModel>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<EmployeeModel>>> GetEmployees()
    {
        var users = await _repository.GetEmployees();
        return Ok(users);
    }

    [HttpGet("{id}", Name = "GetEmployee")]
    [ProducesResponseType(typeof(EmployeeModel), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<EmployeeModel>> GetEmployee(string id)
    {
        var user = await _repository.GetEmployee(id);

        if (user == null)
        {
            _logger.LogError($"Employee with id: {id}, not found.");
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    [ProducesResponseType(typeof(EmployeeModel), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<EmployeeModel>> CreateEmployee(EmployeeModel user)
    {
        user.Id = Guid.NewGuid().ToString();

        await _repository.CreateEmployee(user);

        return CreatedAtAction("GetEmployee", new { id = user.Id }, user);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateEmployee(string id, EmployeeModel payload)
    {
        EmployeeModel user = new();

        if (payload.UserId != null)
            user.UserId = payload.UserId;
        if (payload.Region != null)
            user.Region = payload.Region;
        if (payload.Title != null)
            user.Title = payload.Title;
        if (payload.Department != null)
            user.Department = payload.Department;
        if (payload.Position != null)
            user.Position = payload.Position;
        if (payload.Designation != null)
            user.Designation = payload.Designation;

        if (await _repository.UpdateEmployee(user, id))
            return NoContent();
        else
            return NotFound();
    }

    [HttpDelete("{id}", Name = "DeleteEmployee")]
    [ProducesResponseType(typeof(EmployeeModel), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteEmployee(string id)
    {
        if (await _repository.DeleteEmployee(id))
            return NoContent();
        else
            return NotFound();
    }
}


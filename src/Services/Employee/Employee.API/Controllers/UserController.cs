using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Employee.API.DTOs;
using Employee.API.Entities;
using Employee.API.Repositories.Interfaces;
using Employee.API.Services;

namespace Employee.API.Controllers
{
	[ApiController]
	[Route("api/v1/users")]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository _repository;
		private readonly ILogger<UserController> _logger;
		private static readonly Regex sWhitespace = new(@"\s+");

		public UserController(IUserRepository repository, ILogger<UserController> logger)
		{
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<IEnumerable<User>>> GetUsers()
		{
			var users = await _repository.GetUsers();
			return Ok(users);
		}

		[HttpGet("{id}", Name = "GetUser")]
		[ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<User>> GetUser(string id)
		{
			var user = await _repository.GetUser(id);

			if (user == null)
			{
				_logger.LogError($"User with id: {id}, not found.");
				return NotFound();
			}

			return Ok(user);
		}

		[HttpGet("get-user-by-email/{email}", Name = "GetUserByEmail")]
		[ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<User>> GetUserByEmail(string email)
		{
			var user = await _repository.GetUserByEmail(email);
			if (user == null)
			{
				_logger.LogError($"User with email: {email}, not found.");
				return NotFound();
			}

			return Ok(user);
		}

		[HttpPost]
		[ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<User>> CreateUser(User user)
		{
			user.Id = Guid.NewGuid().ToString();
			user.Password = Hashing.HashPassword(sWhitespace.Replace(user.Password, ""));

			await _repository.CreateUser(user);

			return CreatedAtAction("GetUser", new { id = user.Id }, user);
		}

		[HttpPatch("{id}")]
		public async Task<IActionResult> UpdateUser(string id, UpdateUserRequest payload)
		{
			User user = new();

			if (payload.Email != null)
				user.Email = payload.Email;
			if (payload.FirstName != null)
				user.FirstName = payload.FirstName;
			if (payload.LastName != null)
				user.LastName = payload.LastName;
			if (payload.MobileNumber != null)
				user.MobileNumber = payload.MobileNumber;
			if (payload.Country != null)
				user.Country = payload.Country;
			if (payload.Province != null)
				user.Province = payload.Province;
			if (payload.City != null)
				user.City = payload.City;
			if (payload.Role != null)
				user.Role = payload.Role;
			if (payload.Designation != null)
				user.Designation = payload.Designation;

			if (await _repository.UpdateUser(user, id))
				return NoContent();
			else
				return NotFound();
		}

		[HttpDelete("{id}", Name = "DeleteUser")]
		[ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> DeleteUser(string id)
		{
			if (await _repository.DeleteUser(id))
				return NoContent();
			else
				return NotFound();
		}

	}
}

using Microsoft.AspNetCore.Mvc;
using Socially.Domain.Models;
using Socially.Application.Services.Users;
using Socially.Contracts.User;

namespace Socially.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;

        }
        // Get: api/UserController
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser(User user)
        {
            try
            {
                await _userService.CreateUserAsync(user);
                return Ok("User created successfully");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); // Return 409 Conflict with the exception message
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the user");
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                var user = await _userService.LoginAsync(loginRequest.Username, loginRequest.Password);
                if (user == null)
                    return NotFound("User not found or incorrect credentials");

                // You can generate and return JWT token here if needed

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
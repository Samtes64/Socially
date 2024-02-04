using Microsoft.AspNetCore.Mvc;
using Socially.Domain.Models;
using Socially.Application.Services.Users;

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
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            await _userService.CreateUserAsync(user);
            return Ok();
        }

           }
}
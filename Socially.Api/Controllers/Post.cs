using Microsoft.AspNetCore.Mvc;
using Socially.Domain.Models;
using Socially.Application.Services.Users;
using Socially.Contracts.User;
using Socially.Infrastructure;
using System.Threading.Tasks;

namespace Socially.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        [MiddlewareFilter(typeof(JwtMiddleware))] // Apply the middleware
        public async Task<IActionResult> CreatePost([FromBody] Post post)
        {
            try
            {
                // Extract the username from the JWT token
                string username = HttpContext.Items["Username"].ToString();
                post.Username = username;

               await _postService.CreatePostAsync(post); // Await the asynchronous operation

                return CreatedAtAction(nameof(CreatePost), new { id = post.Id }, post); // Construct the CreatedAtAction result
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

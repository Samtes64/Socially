using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Socially.Domain.Models;

namespace Socially.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ILogger<Post> _logger;

        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost("create")]
        // [MiddlewareFilter(typeof(JwtMiddleware))]
        public async Task<IActionResult> CreatePost(Post post)
        {
            // Access the username from HttpContext.Items set by JwtMiddleware
            // string username = HttpContext.Items["Username"]?.ToString();

            await _postService.CreatePostAsync(post);
            return Ok("Post created successfully");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await _postService.GetAllAsync();
            return Ok(posts);
        }
    }
}

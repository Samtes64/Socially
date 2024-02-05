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

        [HttpGet("{id:length(24)}", Name = "GetPost")]
        public ActionResult<Post> GetPost(string id)
        {
            var post = _postService.GetPostById(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        [HttpGet("byUserUsername/{userUsername}")]
        public ActionResult<List<Post>> GetPostsByUserId(string userUsername)
        {
            var posts = _postService.GetPostsByUserUsername(userUsername);

            if (posts == null)
            {
                return NotFound();
            }

            return posts;
        }
    }
}

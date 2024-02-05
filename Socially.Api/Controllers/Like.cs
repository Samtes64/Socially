// LikesController.cs
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Socially.Application.Services.Likes;

namespace Socially.API.Controllers
{
    [ApiController]
    [Route("api/likes")]
    public class LikesController : ControllerBase
    {
        private readonly ILikesService _likesService;

        public LikesController(ILikesService likesService)
        {
            _likesService = likesService;
        }

        [HttpPost]
        public async Task<IActionResult> ToggleLikeAsync([FromBody] LikeRequest likeRequest)
        {
            try
            {
                var isLiked = await _likesService.ToggleLikeAsync(likeRequest.PostId, likeRequest.UserId);
                return Ok(new { Liked = isLiked });
            }
            catch
            {
                return StatusCode(500, "An error occurred while toggling like.");
            }
        }
    }

    public class LikeRequest
    {
        public string PostId { get; set; }
        public string UserId { get; set; }
    }
}

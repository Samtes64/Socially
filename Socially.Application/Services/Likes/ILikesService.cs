// ILikesService.cs
using System.Threading.Tasks;

namespace Socially.Application.Services.Likes
{
    public interface ILikesService
    {
        Task<bool> ToggleLikeAsync(string postId, string userId);
    }
}

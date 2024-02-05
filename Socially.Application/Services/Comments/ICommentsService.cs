// ILikesService.cs
using System.Threading.Tasks;
using Socially.Domain.Models;

namespace Socially.Application.Services.Comments
{
    public interface ICommentsService
    {
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(string postId);
        Task<Comment> CreateCommentAsync(Comment comment);

        Task DeleteCommentAsync(string commentId);
    }
}

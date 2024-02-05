// CommentsController.cs
using Microsoft.AspNetCore.Mvc;
using Socially.Application.Services.Comments;
using Socially.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/comments")]
public class CommentsController : ControllerBase
{
    private readonly ICommentsService _commentsService;

    public CommentsController(ICommentsService commentsService)
    {
        _commentsService = commentsService;
    }

    [HttpGet("{postId}")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByPostId(string postId)
    {
        try
        {
            var comments = await _commentsService.GetCommentsByPostIdAsync(postId);
            return Ok(comments);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Comment>> CreateComment(Comment comment)
    {
        try
        {
            var createdComment = await _commentsService.CreateCommentAsync(comment);
            return CreatedAtAction(nameof(GetCommentsByPostId), new { postId = createdComment.PostId }, createdComment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpDelete("{commentId}")]
    public async Task<IActionResult> DeleteComment(string commentId)
    {
        try
        {
            await _commentsService.DeleteCommentAsync(commentId);
            return Ok("DELETED SUCCESSFULLY");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}

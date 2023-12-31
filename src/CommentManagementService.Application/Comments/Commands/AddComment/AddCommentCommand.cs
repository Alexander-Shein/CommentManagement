using EmpCore.Application.Commands;
using EmpCore.Domain;

namespace CommentManagementService.Application.Comments.Commands.AddComment;

public class AddCommentCommand : Command<Result<long>>
{
    public Guid PublishedBlogPostId { get; }
    public string CommentorId { get; }
    public string CommentorUserName { get; }
    public string Message { get; }
    
    public AddCommentCommand(
        Guid publishedBlogPostId,
        string commentorId,
        string commentorUserName,
        string message)
    {
        PublishedBlogPostId = publishedBlogPostId;
        CommentorId = commentorId;
        CommentorUserName = commentorUserName;
        Message = message;
    }
}
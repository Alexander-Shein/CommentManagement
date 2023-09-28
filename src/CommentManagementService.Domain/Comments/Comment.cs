using CommentManagementService.Domain.BlogPosts;
using CommentManagementService.Domain.Comments.ValueObjects;
using EmpCore.Domain;

namespace CommentManagementService.Domain.Comments;

public class Comment : AggregateRoot<long>
{
    private Comment() { }

    public PublishedBlogPost PublishedBlogPost { get; private set; }
    public Commentor Commentor { get; private set; }
    public Comment? ParentComment { get; private set; }
    public Message Message { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    internal Comment(
        PublishedBlogPost publishedBlogPost,
        Commentor commentor,
        Message message,
        Comment? parentComment = null)
    {
        Contracts.Require(publishedBlogPost != null);
        Contracts.Require(commentor != null);
        Contracts.Require(message != null);
        
        var now = DateTime.UtcNow;

        PublishedBlogPost = publishedBlogPost;
        Commentor = commentor;
        Message = message;
        ParentComment = parentComment;
        CreatedAt = UpdatedAt = now;
    }

    public Result<Comment> Reply(Commentor commentor, Message message)
    {
        Contracts.Require(commentor != null);
        Contracts.Require(message != null);
        
        var comment = new Comment(PublishedBlogPost, commentor, message, this);
        return comment;
    }
}